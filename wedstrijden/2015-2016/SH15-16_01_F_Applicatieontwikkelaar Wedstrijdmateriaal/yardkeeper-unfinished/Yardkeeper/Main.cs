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
    public partial class Main : Form
    {        
        private Configuration config;

        private double temperature, dewpoint, humidity, pressure;

        public Main()
        {   
            // Register all controls from the designer to the form.
            InitializeComponent();

            // Setup the Main form to open the application centered on the default screen.
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Flow Software Solutions | Yardkeeper | v. " + Application.ProductVersion.ToString();

            // this.clkTimeOfDay.Ref;
            
            // A class that interfaces with the configuration files.
            this.config = new Configuration();


            UpdateYard();
            UpdateSprinklers();
            UpdateConditions();
        }



        #region Interface update routines.

        /// <summary>
        ///     A method that updates the yard pane.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateYard()
        {
            // config.SetFile( Configuration.File.WITH_SENSOR_DATA );

            double width, length;

            // Xml node containing information about the yard.
            string hive = "yard";

            width = 40;
            length = 60;

            ThreadSafeUpdate( lblYardName, "My Backyard" );
            ThreadSafeUpdate( yrdMiniMap, new RealLifeSize( width, length ) );

            return;

            throw new ArgumentException( "Cannot find the requested node: " + hive + " in the configuration XML!" );
        }

        /// <summary>
        ///     A method that updates the sprinklers in the yard.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateSprinklers()
        {
            XmlNodeList nodes;
            XmlNode node;
            
            Random random = new Random();
            Sprinkler[] sprinklers;
            Sprinkler s;

            // Xml node containing the sprinklers telemetry.
            string hive = "unit";

            if ( 1 == 1 ) // || config.NodeExists( hive ) )
            {
                sprinklers = new Sprinkler[ 6 ];

                double[] tempX = new double[ 6 ] { 5.2, 15.4, 25.1, 15.4, 15.4, 25.1 };
                double[] tempY = new double[ 6 ] { 5.4, 5.2, 5.6, 25.3, 42.3, 43.2 };

                // The application has a variable amount of connected units.
                // For each unit, its position, range and angle are stored.
                for ( int i = 0; i < 6; i++ )
                {
                    s = new Sprinkler();
                    s.Text = "Unit " + i;

                    s.Location = new Point( 
                        
                        ( int ) tempX[ i ] * 10,
                        ( int ) tempY[ i ] * 10
                    );

                    s.MinimumAngle = 0;                    
                    s.MaximumAngle = 360;
                    s.Step = random.Next( 100, 145 );

                    // The center dot of the control.
                    s.CenterSize = 2;
                    s.Size = new Size( 
                        
                        8, 8
                    );

                    //<angle>70</angle>

                    s.Angle = random.Next( s.MinimumAngle, s.MaximumAngle );
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateConditions()
        {
            string name, value, suffix;

            // Xml node containing the conditions telemetry.
            string hive = "sensor";

            if ( 1 == 1 ) // || config.NodeExists( hive ) )
            {
                // XmlNodeList nodes = config.GetValuesByNodeName( hive );

                //// <suffix>C</suffix>

                //if ( name.Trim().ToLower() != "forecast" )
                //{
                //    suffix = " " + node.SelectSingleNode( "suffix" ).InnerText;
                //}

                temperature = 23.4;
                ThreadSafeUpdate( lblTemperatureValue, temperature + " C" );

                dewpoint = 18.3;
                ThreadSafeUpdate( lblDewpointValue, dewpoint + " C" );

                humidity = 67;
                ThreadSafeUpdate( lblHumidityValue, humidity + "%" );

                pressure = Utilities.GetNumericValueFromText( "1234" );
                ThreadSafeUpdate( lblPressureValue, pressure + " mbar" );
                
                ThreadSafeUpdate( pbForecast, global::Yardkeeper.Properties.Resources.conditions_cloud_sun );

                return;
            }

            throw new ArgumentException( "Cannot find the requested node: " + hive + " in the configuration XML!" );
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
            return ( control.IsHandleCreated && control != null && !control.IsDisposed );
        }

        /// <summary>
        ///     Method that updates a ui element thread-safe.
        ///     Used to update the text value of a label.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="value"></param>
        private void ThreadSafeUpdate( Label control, string value )
        {
            control.Text = value;
        }

        /// <summary>
        ///     Method that updates a ui element thread-safe.
        ///     Used to update the Yards dimensions.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="value"></param>
        private void ThreadSafeUpdate( Yard control, RealLifeSize value )
        {
             control.Dimensions = value;
        }

        /// <summary>
        ///     Method that updates a ui element thread-safe.
        ///     Used to set the sprinkler controls in the Yard.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="value"></param>
        private void ThreadSafeUpdate( Yard control, Sprinkler[] value )
        {
            foreach ( Sprinkler s in value )
            {
                s.BackColor = control.BackColor;
                s.ForeColor = Color.LightYellow;
                s.Activate();
            }

            control.SetControls( value );
        }

        /// <summary>
        ///     Method that updates a ui element thread-safe.
        ///     Used to control the active state of the sprinklers.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="value"></param>
        private void ThreadSafeUpdate( Sprinkler control, bool value )
        {
            if ( GetHandleStatus( control ) )
            {
                Invoke(( MethodInvoker ) delegate() {

                    if ( value )
                    {
                        control.Activate();
                    }
                    else
                    {
                        control.Shutdown();
                    }
                });
            }
        }

        /// <summary>
        ///     Method that updates a ui element thread-safe.
        ///     Used to update and load an the image of a picturebox control.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="value"></param>
        private void ThreadSafeUpdate( PictureBox control, Bitmap value )
        {
            control.Image = value;
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
