using System;
using System.Windows;
using System.Windows.Forms;

using System.Collections.Generic;
using System.ComponentModel;

using System.Data;
using System.Drawing;

using System.Text;
using System.Xml;

using System.Threading.Tasks;
using System.Threading;

using Yardkeeper.Interface;

namespace Yardkeeper
{    
    /*
     *      
     *      The code and logic in this application are proprietary developed and owned by Skills Netherlands - Copyright 2016.
     *      Since the code was intended as exam material, redistribution and/or adaption are strictly forbidden unless instructed otherwise by the owner.
     * 
     *          Project: Yardkeeper
     *                
     *          Version: 1.23
     *          Dob: 12-2015
     *          Doc: 02-2016
     * 
     *          Purpose: commercial agricultural irrigation control ( simulated and simplified )
     *      
     * 
     *      Some hints to the layout of this application.
     *  
     *      -   incorporates 3 custom written .NET controls ( please review the toolbox ).
     *      -   custom controls may contain properties currently not in use. Mainly due to laziness in cleaning up earlier tests ;)
     *      -   the clock control powers the update logic for the ui, syncing everything every minute.
     *      -   the clock automatically syncs up with the desktop time to enable easy testing of schedule rules.
     *      -   storage solution is two XML files that are compile-time exported to the output directory. ( data/schedule.xml )
     *      -   both xml files are disposed after read and may be freely adapted WHILE THE APPLICATION IS IN USE.
     *      -   the configuration class intentionally uses a deprecated XML interface, XmlDataDocument ( scheduled for replacement; xDocument )
     *      -   the final version of the code was written in under nine hours, thus simulating a reasonable timeframe for contestants.
     *      
     *      App logic:
     * 
     *      Main Form - > Clock.cs - > Interface update methods < -- > Configuration < -- > XML 
     *      v       < - User input                                                           A
     *      |                                                                                |
     *      |---------- > Eventhandlers Main.cs - > Scheduler.cs < -- > XML -----------------|
     *      |                        
     *      |                        
     *      |---------- > Yard.cs < -- > Sprinkler.cs
     * 
     * 
     */

    public partial class Main : Form
    {
        #region Class vars, Getters/Setters.

        // Enumeration for component index in the scheduler.
        private const int SCHEDULE_TYPE_COMPONENT = 0;        
        private const int SCHEDULE_CATEGORY_COMPONENT = 1;
        
        // Since the amount of available categories is variable, store their values.
        private const string SCHEDULE_COMPONENT_OPT_NONE = "Select...";
        private const string SCHEDULE_CATEGORY_COMPONENT_OPT_DAYS = "at days";
        private const string SCHEDULE_CATEGORY_COMPONENT_OPT_TIME = "at time";
        private const string SCHEDULE_CATEGORY_COMPONENT_OPT_TEMPERATURE = "at temperature >";
        private const string SCHEDULE_CATEGORY_COMPONENT_OPT_HUMIDITY = "at humidity <";
        private const string SCHEDULE_CATEGORY_COMPONENT_OPT_RAIN = "when its raining";


        private Configuration config;

        private double temperature, dewpoint, humidity, pressure;

        private string[] scheduleSpecControlNames = new string[ 4 ] { 
                
            "cbScheduleRuleSpecDays_",
            "dtpScheduleRuleSpecTime_",
            "nudScheduleRuleSpecTemperature_",
            "nudScheduleRuleSpecHumidity_"
        };

        private const int SCHEDULE_SPEC_DAYS_CONTROL_INDEX = 0;
        private const int SCHEDULE_SPEC_TIME_CONTROL_INDEX = 1;
        private const int SCHEDULE_SPEC_TEMPERATURE_CONTROL_INDEX = 2;
        private const int SCHEDULE_SPEC_HUMIDITY_CONTROL_INDEX = 3;

        private const int SCHEDULE_SPEC_VALUE_DAYSOFWEEK = 1;
        private const int SCHEDULE_SPEC_VALUE_WEEKEND = 2;

        private bool isUpdating = false;
        private bool isRunning = false;
        private bool isRaining = false;

        public const string SPRINKLER_STATE_ACTIVE = "Active";
        public const string SPRINKLER_STATE_INACTIVE = "Inactive";

        private DateTime autoOff;

        #endregion


        #region Main Form construction

        public Main()
        {   
            // Register all controls from the designer to the form.
            InitializeComponent();

            // A class that interfaces with the configuration files.
            this.config = new Configuration();         

            // Setup the Main form to open the application centered on the default screen.
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Flow Software Solutions | Yardkeeper | v. " + Application.ProductVersion.ToString();

            SetupScheduler();

            // Set the refreshrate of the clock to 60 seconds.
            this.clkTimeOfDay.RefreshRate = 60;

            // Subscribe the update routine for the interface to the update event of the clock in the interface.
            // This synchronizes the update interval of the interface through the clock.
            // Do note, this saves a lot of time and allows for a clean code layout.
            this.clkTimeOfDay.UpdateUI += UpdateYard;
            this.clkTimeOfDay.UpdateUI += UpdateSprinklers;
            this.clkTimeOfDay.UpdateUI += UpdateConditions;
            this.clkTimeOfDay.UpdateUI += UpdateScheduler;
        }


        private void SetupScheduler()
        {
            isUpdating = true;

            ComboBox[] controls;
            string[] values;

            // Determine the dropdown values for the interface.
            values = new string[ 3 ] { 

                SCHEDULE_COMPONENT_OPT_NONE,
                "Run",
                "Don't run"
            }; 

            // Setup the RuleType ComboBox.
            controls = new ComboBox[ 4 ] { 
                cbScheduleRuleType_1,
                cbScheduleRuleType_2,
                cbScheduleRuleType_3,
                cbScheduleRuleType_4
            };
             
            // Fill the combobox with the desired values.
            foreach ( ComboBox cb in controls )
            {
                cb.Items.Clear();

                cb.Enabled = true;
                cb.Items.AddRange( values );

                cb.SelectedIndex = 0;
            }


            values = new string[ 1 ] {

			    SCHEDULE_COMPONENT_OPT_NONE
            };

            controls = new ComboBox[ 4 ] { 
                cbScheduleRuleCategory_1,
                cbScheduleRuleCategory_2,
                cbScheduleRuleCategory_3,
                cbScheduleRuleCategory_4
            };

            foreach ( ComboBox cb in controls )
            {
                cb.Items.Clear();

                cb.Enabled = true;
                cb.Items.AddRange( values );

                cb.SelectedIndex = 0;
            }


            values = new string[ 3 ] {

			    SCHEDULE_COMPONENT_OPT_NONE,
			    "Weekdays",
			    "Weekend"
            };

            controls = new ComboBox[ 4 ] { 
                cbScheduleRuleSpecDays_1,
                cbScheduleRuleSpecDays_2,
                cbScheduleRuleSpecDays_3,
                cbScheduleRuleSpecDays_4
            };

            foreach ( ComboBox cb in controls )
            {
                cb.Items.Clear();

                cb.Enabled = true;
                cb.Items.AddRange( values );

                cb.SelectedIndex = 0;
            }


            isUpdating = false;
        }

        #endregion

        #region Interface update routines.

        /// <summary>
        ///     An eventhandler that updates the yard pane by reading its configuration from the sensors xml file.
        /// </summary>
        private void UpdateYard( object sender, EventArgs e )
        {   
            config.SetFile( Configuration.File.WITH_SENSOR_DATA );

            double width, length;

            // Xml node containing information about the yard.
            string hive = "yard";

            if ( config.NodeExists( hive ) )
            {
                XmlNode node = config.GetValuesByNodeName( hive ).Item( 0 );
                
                width = Utilities.GetNumericValueFromText( node.SelectSingleNode( "width" ).InnerText );
                length = Utilities.GetNumericValueFromText( node.SelectSingleNode( "length" ).InnerText );

                ThreadSafeUpdate( lblYardName, node.SelectSingleNode( "name" ).InnerText );
                ThreadSafeUpdate( yrdMiniMap, new RealLifeSize( width, length ) );

                return;
            }

            throw new ArgumentException( "Cannot find the requested node: " + hive + " in the configuration XML!" );
        }

        /// <summary>
        ///     An eventhandler that updates the sprinklers in the yard by reading their configuration from the sensors xml file.
        /// </summary>
        private void UpdateSprinklers( object sender, EventArgs e )
        {
            XmlNodeList nodes;
            XmlNode node;
            
            Sprinkler[] sprinklers;
            Sprinkler s;

            config.SetFile( Configuration.File.WITH_SENSOR_DATA );
            
            // Xml node containing the sprinklers telemetry.
            string hive = "unit";

            if ( config.NodeExists( hive ) )
            {
                nodes = config.GetValuesByNodeName( hive );
                sprinklers = new Sprinkler[ nodes.Count ];

                // The application has a variable amount of connected units.
                // For each unit, its position, range and angle are stored.
                for ( int i = 0; i < nodes.Count; i++ )
                {
                    node = nodes[i];

                    s = new Sprinkler();
                    s.Text = node.SelectSingleNode( "name" ).InnerText;

                    //<position>
                    //    <x>15.4</x>
                    //    <y>22.3</y>
                    //</position>

                    s.Location = new Point(

                        ( int ) Utilities.GetNumericValueFromText( node.SelectSingleNode( "position" ).ChildNodes.Item( 0 ).InnerText ),
                        ( int ) Utilities.GetNumericValueFromText( node.SelectSingleNode( "position" ).ChildNodes.Item( 1 ).InnerText )
                    );

                    //<range>
                    //    <min>-360</min>
                    //    <max>360</max>					
                    //    <step>1</step>
                    //</range>

                    s.MinimumAngle = ( int ) Utilities.GetNumericValueFromText( node.SelectSingleNode( "range" ).ChildNodes.Item( 0 ).InnerText );                    
                    s.MaximumAngle = ( int ) Utilities.GetNumericValueFromText( node.SelectSingleNode( "range" ).ChildNodes.Item( 1 ).InnerText );
                    s.Step = ( int ) Utilities.GetNumericValueFromText( node.SelectSingleNode( "range" ).ChildNodes.Item( 2 ).InnerText );

                    //<reach>2.5</reach>
                    
                    s.CenterSize = 2;
                    s.Size = new Size( 
                        ( int ) Utilities.GetNumericValueFromText( node.SelectSingleNode( "reach" ).InnerText ),
                        ( int ) Utilities.GetNumericValueFromText( node.SelectSingleNode( "reach" ).InnerText )
                    );

                    //<angle>70</angle>

                    s.Angle = ( int ) Utilities.GetNumericValueFromText( node.SelectSingleNode( "angle" ).InnerText );

                    sprinklers[ i ] = s;
                }

                ThreadSafeUpdate( yrdMiniMap, sprinklers );

                return;
            }

            throw new ArgumentException( "Cannot find the requested node: " + hive + " in the configuration XML!" );
        }

        /// <summary>
        ///     An eventhandler that updates the conditions pane by reading its configuration from the sensors xml file.
        /// </summary>
        private void UpdateConditions( object sender, EventArgs e )
        {
            config.SetFile( Configuration.File.WITH_SENSOR_DATA );

            string name, value, suffix;

            // Xml node containing the conditions telemetry.
            string hive = "sensor";

            if ( config.NodeExists( hive ) )
            {
                XmlNodeList nodes = config.GetValuesByNodeName( hive );

                // The application uses five different sensors:
                // temperature, dew point, humidity, atmospheric pressure and the general conditions.
                for ( int i = 0; i < nodes.Count; i++ )
                {
                    XmlNode node = nodes[i];

                    // <name>Forecast</name>
                    // <value>Cloud+rain</value>

                    name = node.SelectSingleNode( "name" ).InnerText;
                    value = node.SelectSingleNode( "value" ).InnerText;
                    suffix = null;

                    // <name>Temperature</name>
                    // <value>25.8</value>
                    // <suffix>C</suffix>

                    if ( name.Trim().ToLower() != "forecast" )
                    {
                        suffix = " " + node.SelectSingleNode( "suffix" ).InnerText;
                    }
                    
                    switch ( name )
                    {
                        case "Temperature":
                
                            temperature = Utilities.GetNumericValueFromText( value );
                            ThreadSafeUpdate( lblTemperatureValue, value + suffix );
                        break;

                        case "Dew point":
                
                            dewpoint = Utilities.GetNumericValueFromText( value );
                            ThreadSafeUpdate( lblDewpointValue, value + suffix );
                        break;

                        case "Humidity":
                
                            humidity = Utilities.GetNumericValueFromText( value );
                            ThreadSafeUpdate( lblHumidityValue, value + suffix );
                        break;

                        case "Atmospheric pressure":
                
                            pressure = Utilities.GetNumericValueFromText( value );
                            ThreadSafeUpdate( lblPressureValue, value + suffix );
                        break;

                        case "Forecast":
                            object image = Yardkeeper.Properties.Resources.ResourceManager.GetObject( value );

                            if ( image != null )
                            {
                                isRaining = value.ToLower().Contains( "rain" ) ?  true : false;
                                ThreadSafeUpdate( pbForecast, ( Image ) image );
                            }
                            
                        break;
                    }
                }

                return;
            }

            throw new ArgumentException( "Cannot find the requested node: " + hive + " in the configuration XML!" );
        }

        /// <summary>
        ///     An eventhandler that updates the scheduler pane by reading its configuration from the sensors xml file.
        /// </summary>
        private void UpdateScheduler( object sender, EventArgs e )
        {
            XmlNodeList nodes;
            XmlNode node;

            int value;

            int rulesActive = 0;
            int rulesBroken = 0;
            
            Control control;

            config.SetFile( Configuration.File.WITH_SCHEDULE_DATA );

            string hive = "rule";

            if ( config.NodeExists( hive ) )
            {
                // Lock the application from saving changes to XML while updating.
                isUpdating = true;

                // Load the Xml nodes containing the schedule rules.
                nodes = config.GetValuesByNodeName( hive );

                for ( int i = 1; i <= nodes.Count; i++ )
                {
                    node = nodes[ ( i - 1 ) ];
                    
                    // <rule>
                    //     <type>1</type>
                    //     <category>2</category>
                    //     <specifics>12:30</specifics>
                    // </rule>

                    value = ( int ) Utilities.GetNumericValueFromText( node.SelectSingleNode( "type" ).InnerText );
                    if ( value > 0 )
                    {
                        ThreadSafeUpdate( 
                            ( ComboBox ) pnlScheduleManager.Controls[ "cbScheduleRuleType_" + i ],
                            value
                        );

                        value = ( int ) Utilities.GetNumericValueFromText( node.SelectSingleNode( "category" ).InnerText );
                        if ( value > 0 )
                        {
                            ThreadSafeUpdate( 
                                ( ComboBox ) pnlScheduleManager.Controls[ "cbScheduleRuleCategory_" + i ], 
                                value
                            );

                            
                            if ( node.SelectSingleNode( "specifics" ).InnerText.ToString().Length > 0 )
                            {
                                // The previous operation has automatically loaded the correct control, 
                                // therefore check to see which control on the current row is visible.
                                for ( int ii = 0; ii < scheduleSpecControlNames.Length; ii++ )
                                {
                                    control = ( Control ) 
                                        pnlScheduleManager.Controls[ scheduleSpecControlNames[ ii ] + i ];

                                    if ( control.Visible )
                                    {
                                        ++rulesActive;

                                        switch ( ii )
                                        {
                                            //"cbScheduleRuleSpecDays_",
                                            // ComboBox with days of week / weekend selection.
                                            case SCHEDULE_SPEC_DAYS_CONTROL_INDEX:

                                                int day = ( int ) Utilities.GetNumericValueFromText( node.SelectSingleNode( "specifics" ).InnerText );

                                                switch ( DateTime.Now.DayOfWeek )
                                                {
                                                    case DayOfWeek.Monday:
                                                    case DayOfWeek.Tuesday:
                                                    case DayOfWeek.Wednesday:
                                                    case DayOfWeek.Thursday:
                                                    case DayOfWeek.Friday:

                                                        if ( day == SCHEDULE_SPEC_VALUE_DAYSOFWEEK )
                                                        {
                                                            ++rulesBroken;
                                                        }

                                                    break;

                                                    default: 
                                                    
                                                        if ( day == SCHEDULE_SPEC_VALUE_WEEKEND )
                                                        {
                                                            ++rulesBroken;
                                                        }

                                                    break;
                                                }

                                                ThreadSafeUpdate( 
                                                    ( ComboBox ) pnlScheduleManager.Controls[ "cbScheduleRuleSpecDays_" + i ], 
                                                    day
                                                );
                                            break;

                                
                                            // "dtpScheduleRuleSpecTime_",
                                            // DatetimePicker with format HH:mm
                                            case SCHEDULE_SPEC_TIME_CONTROL_INDEX:

                                                string[] parts = node.SelectSingleNode( "specifics" ).InnerText.Split( ':' );
                    
                                                DateTime start = DateTime.Now;
                                                TimeSpan time = new TimeSpan( int.Parse( parts[ 0 ] ), int.Parse( parts[ 1 ] ), 0);

                                                start = start.Date + time;
                                                DateTime threshold = start.AddMinutes( 2D );

                                                if ( DateTime.Now >= start && DateTime.Now < threshold )
                                                {
                                                    ++rulesBroken;
                                                }
                                            

                                                ThreadSafeUpdate( 
                                                    ( DateTimePicker ) pnlScheduleManager.Controls[ "dtpScheduleRuleSpecTime_" + i ], 
                                                    start
                                                );
                                            break;


                                            //"nudScheduleRuleSpecTemperature_",
                                            // NumericUpDown
                                            case SCHEDULE_SPEC_TEMPERATURE_CONTROL_INDEX:
                                    
                                                double temp = ( double ) Utilities.GetNumericValueFromText( node.SelectSingleNode( "specifics" ).InnerText );

                                                if ( temperature > temp )
                                                {
                                                    ++rulesBroken;
                                                }


                                                ThreadSafeUpdate(
                                                    ( NumericUpDown ) pnlScheduleManager.Controls[ "dtpScheduleRuleSpec`Temperature_" + i ],
                                                    ( decimal ) temp
                                                );
                                            break;


                                            //"nudScheduleRuleSpecHumidity_"
                                            // NumericUpDown
                                            case SCHEDULE_SPEC_HUMIDITY_CONTROL_INDEX:
                                    
                                                double humid = ( double ) Utilities.GetNumericValueFromText( node.SelectSingleNode( "specifics" ).InnerText );

                                                if ( humidity < humid )
                                                {
                                                    ++rulesBroken;
                                                }

                                                ThreadSafeUpdate(
                                                    ( NumericUpDown ) pnlScheduleManager.Controls[ "nudScheduleRuleSpecHumidity_" + i ],
                                                    ( decimal ) humid
                                                );
                                            break;

                                            default: 

                                                throw new IndexOutOfRangeException( "Could not match an element to a rule!" );
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if ( !isRunning && rulesActive > 0 && rulesActive == rulesBroken )
                {
                    // activate sprinklers.
                    ThreadSafeUpdate( yrdMiniMap, true );
                    isRunning = true;

                    autoOff = DateTime.Now.AddMinutes( 30D );

                    UpdateTelemetry();
                }

                // Validate the automatic shutdown after 30 minutes rule. And check to see whether isRunning condition is still valid.
                if ( isRunning && DateTime.Now >= autoOff || ( rulesActive > 0 && rulesActive > rulesBroken ) )
                {
                    ThreadSafeUpdate( yrdMiniMap, false );
                    isRunning = false;

                    UpdateTelemetry();
                }


                isUpdating = false;
                return;
            }

            throw new ArgumentException( "Cannot find the requested node: " + hive + " in the configuration XML!" );
        }


        private void UpdateTelemetry()
        {
            XmlTextWriter writer = new XmlTextWriter( @"data/update.xml", System.Text.Encoding.UTF8 );
            writer.WriteStartDocument( true );
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            
            writer.WriteStartElement( "yardkeeper" );
            
                writer.WriteStartElement("sync");
                writer.WriteString( DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() );
                writer.WriteEndElement();

            int status = isRunning ? 1 : 0;

                writer.WriteStartElement( "telemetry" );
                    writer.WriteStartElement( "status" );
                    writer.WriteString( status.ToString() );
                    writer.WriteEndElement();
                writer.WriteEndElement();
            
            writer.WriteEndElement();

            writer.WriteEndDocument();
            writer.Close();
        }

        #endregion

        #region Eventhandlers


        /// <summary>
        ///     Schedule Type Changed EventHandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // The sending element is one of four activation type dropdowns.
            ComboBox cb = ( ComboBox ) sender;            

            // Since the row number is appended to the name, we can determine which row raised the event.
            int rule = int.Parse( cb.Name.Substring( cb.Name.Length - 1, 1 ) ) - 1;
            int value = cb.SelectedIndex;

            
            if ( !isUpdating )
            {
                config.Save( rule, "/yardkeeper/scheduler/rules/rule/type", value );
            }

            ++rule;

            // Load the Category dropdown on the same row.
            cb = ( ComboBox ) pnlScheduleManager.Controls[ "cbScheduleRuleCategory_" + rule ];

            // Switch the value for the activation type dropdown to 
            // determine which categories are valid for that option.
            switch( value )
            {
                // "Don't Run",
                case 2:
                    
                    cb.Items.Clear();
                    cb.Items.AddRange(new string[ 5 ] {

                        SCHEDULE_COMPONENT_OPT_NONE,
                        SCHEDULE_CATEGORY_COMPONENT_OPT_TIME,
                        SCHEDULE_CATEGORY_COMPONENT_OPT_TEMPERATURE,
                        SCHEDULE_CATEGORY_COMPONENT_OPT_HUMIDITY,
                        SCHEDULE_CATEGORY_COMPONENT_OPT_RAIN
                    });

                break;

                // "Select...",
                // "Run",
                default: 

                    cb.Items.Clear();
                    cb.Items.AddRange(new string[ 5 ] {

                        SCHEDULE_COMPONENT_OPT_NONE,
                        SCHEDULE_CATEGORY_COMPONENT_OPT_DAYS,
                        SCHEDULE_CATEGORY_COMPONENT_OPT_TIME,
                        SCHEDULE_CATEGORY_COMPONENT_OPT_TEMPERATURE,
                        SCHEDULE_CATEGORY_COMPONENT_OPT_HUMIDITY,
                    });

                break;
            }

            // Reset the category dropdown to "Select..." to prevent incorrect selections.
            cb.SelectedIndex = 0;

            // Hide the last control on this row to prevent incorrect selections.
            Schedule_ClearRuleSpecType( rule );
        }


        /// <summary>
        ///     Schedule Category Changed EventHandler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleCategory_SelectedIndexChanged( object sender, EventArgs e )
        {
            ComboBox cb = ( ComboBox ) sender;
            DateTimePicker dtp; 
            NumericUpDown nud;

            int rule = int.Parse( cb.Name.Substring( cb.Name.Length - 1, 1 ) ) - 1;
            string value = cb.Text;

            
            if ( !isUpdating )
            {
                config.Save( rule, "/yardkeeper/scheduler/rules/rule/category", cb.SelectedIndex );
            }

            ++rule;

            // Quick and dirty solution, the initial dropdown acts as an anchor point for the control that needs to be loaded.
            // Instead of constructing the required controls from the code, this allows them to be placed randomly on the parent panel.
            cb = ( ComboBox ) 
                pnlScheduleManager.Controls[ scheduleSpecControlNames[ SCHEDULE_SPEC_DAYS_CONTROL_INDEX ] + rule ];

            // Hide the last control of this row to prevent incorrect selections.
            Schedule_ClearRuleSpecType( rule );

            // Then determine which control should be shown.
            switch ( value )
            {
                // "at day",
                case SCHEDULE_CATEGORY_COMPONENT_OPT_DAYS:

                    cb.Visible = true;
                break;

                // "at time",
                case SCHEDULE_CATEGORY_COMPONENT_OPT_TIME:

                    dtp = ( DateTimePicker ) 
                        pnlScheduleManager.Controls[ scheduleSpecControlNames[ SCHEDULE_SPEC_TIME_CONTROL_INDEX ] + rule ];

                    dtp.Top = cb.Top;
                    dtp.Left = cb.Left;
                    dtp.Width = cb.Width;

                    dtp.Visible = true;

                break;

                // "at temperature",
                case SCHEDULE_CATEGORY_COMPONENT_OPT_TEMPERATURE:

                    nud = ( NumericUpDown ) 
                        pnlScheduleManager.Controls[ scheduleSpecControlNames[ SCHEDULE_SPEC_TEMPERATURE_CONTROL_INDEX ] + rule ]; 

                    nud.Top = cb.Top;
                    nud.Left = cb.Left;
                    nud.Width = cb.Width;

                    nud.Visible = true;

                break;

                // "at humidity",
                case SCHEDULE_CATEGORY_COMPONENT_OPT_HUMIDITY:
                    
                    nud = ( NumericUpDown ) 
                        pnlScheduleManager.Controls[ scheduleSpecControlNames[ SCHEDULE_SPEC_HUMIDITY_CONTROL_INDEX ] + rule ]; 

                    nud.Top = cb.Top;
                    nud.Left = cb.Left;
                    nud.Width = cb.Width;

                    nud.Visible = true;

                break;

                // "Select...",
                // "when it rains"
                default:

                    // No action required.
                break;
            }
        }


        /// <summary>
        ///     Schedule Spec Changed EventHandler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleSpec_ValueChanged( object sender, EventArgs e )
        {
            // The sending element is one of four activation type dropdowns.
            Control control = ( Control ) sender;            

            // Since the row number is appended to the name, we can determine which row raised the event.
            int rule = int.Parse( control.Name.Substring( control.Name.Length - 1, 1 ) ) - 1;
            string value = control.Text.Trim();

            
            if ( !isUpdating )
            {
                config.Save( rule, "/yardkeeper/scheduler/rules/rule/specifics", value );
            }
        }


        /// <summary>
        ///     Schedule Spec Changed EventHandler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScheduleSpec_SelectedIndexChanged( object sender, EventArgs e )
        {
            // The sending element is one of four activation type dropdowns.
            ComboBox cb = ( ComboBox ) sender;            

            // Since the row number is appended to the name, we can determine which row raised the event.
            int rule = int.Parse( cb.Name.Substring( cb.Name.Length - 1, 1 ) ) - 1;
            int value = cb.SelectedIndex;

            
            if ( !isUpdating )
            {
                config.Save( rule, "/yardkeeper/scheduler/rules/rule/specifics", value );
            }
        }


        private void Schedule_ClearRuleSpecType( int rule )
        {
            foreach ( string name in scheduleSpecControlNames )
            {
                pnlScheduleManager.Controls[ name + rule ].Visible = false;   
            }
        }


        #endregion

        #region Interface cross-thread control access

        /// <summary>
        ///     Method that determines a controls status within the application.
        ///     Used to determine whether it's safe to write a change to the interface controls.
        /// </summary>
        /// <param name="control">The control to check for.</param>
        /// <returns>bool</returns>
        private bool GetHandleStatus( Control control )
        {
            return ( control != null && control.IsHandleCreated && !control.IsDisposed );
        }

        /// <summary>
        ///     Method that updates a ui element thread-safe.
        ///     Used to update the text value of a label.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="value"></param>
        private void ThreadSafeUpdate( Label control, string value )
        {
            if ( GetHandleStatus( control ) )
            {
                Invoke(( MethodInvoker ) delegate() {
                     
                    control.Text = value;
                });
            }
        }

        /// <summary>
        ///     Method that updates a ui element thread-safe.
        ///     Used to update the image of the forecast.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="value"></param>
        private void ThreadSafeUpdate( PictureBox control, Image value )
        {
            if ( GetHandleStatus( control ) )
            {
                Invoke(( MethodInvoker ) delegate() {
                     
                    control.Image = value;
                });
            }
        }

        /// <summary>
        ///     Method that updates a ui element thread-safe.
        ///     Used to update the Yards dimensions.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="value"></param>
        private void ThreadSafeUpdate( Yard control, RealLifeSize value )
        {
            if ( GetHandleStatus( control ) )
            {
                Invoke(( MethodInvoker ) delegate() {

                    control.Dimensions = value;
                });
            }
        }

        /// <summary>
        ///     Method that updates a ui element thread-safe.
        ///     Used to set the sprinkler controls in the Yard.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="value"></param>
        private void ThreadSafeUpdate( Yard control, Sprinkler[] value )
        {
            if ( GetHandleStatus( control ) )
            {
                Invoke(( MethodInvoker ) delegate() {

                    foreach ( Sprinkler s in value )
                    {
                        s.BackColor = control.BackColor;
                        s.ForeColor = Color.LightYellow;
                    }

                    control.SetControls( value );
                });
            }
        }

        /// <summary>
        ///     Method that updates a ui element thread-safe.
        ///     Used to control the active state of the sprinklers.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="value"></param>
        private void ThreadSafeUpdate( Yard control, bool value )
        {
            if ( GetHandleStatus( control ) )
            {
                Invoke(( MethodInvoker ) delegate() {

                    if ( value )
                    {
                        control.ActivateControls();
                        lblScheduleSprinklerState.Text = SPRINKLER_STATE_ACTIVE;
                        lblScheduleSprinklerState.ForeColor = Color.FromArgb( 0, 0, 192, 0 );
                    }
                    else
                    {
                        control.DeactivateControls();
                        lblScheduleSprinklerState.Text = SPRINKLER_STATE_INACTIVE;
                        lblScheduleSprinklerState.ForeColor = Color.FromArgb( 0, 192, 64, 0 );
                    }
                });
            }
        }

        /// <summary>
        ///     Method that updates a ui element thread-safe.
        ///     Used to update the selected index of a dropdown in the scheduler pane.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="value"></param>
        private void ThreadSafeUpdate( ComboBox control, int value )
        {
            if ( GetHandleStatus( control ) )
            {
                Invoke(( MethodInvoker ) delegate() {

                    control.SelectedIndex = value;
                });
            }
        }


        private void ThreadSafeUpdate( DateTimePicker control, DateTime value )
        {
            if ( GetHandleStatus( control ) )
            {
                Invoke(( MethodInvoker ) delegate() {

                    control.Value = value;
                });
            }
        }

        
        private void ThreadSafeUpdate( NumericUpDown control, decimal value )
        {
            if ( GetHandleStatus( control ) )
            {
                Invoke(( MethodInvoker ) delegate() {

                    control.Value = value;
                });
            }
        }


        
        #endregion


        // Snippet to prevent flickers in the rendered controls, 
        // due to the GDI holepunching the child controls from this container.
        protected override CreateParams CreateParams
        {    
            get 
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
            
                return cp;
            }
        }
    }
}
