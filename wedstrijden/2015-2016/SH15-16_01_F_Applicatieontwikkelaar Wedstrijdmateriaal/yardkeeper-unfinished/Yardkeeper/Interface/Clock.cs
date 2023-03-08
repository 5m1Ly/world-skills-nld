using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;

using System.Threading.Tasks;

namespace Yardkeeper.Interface
{
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public partial class Clock : Control
    {   
        private System.Timers.Timer tmrClockPulse;
        private BackgroundWorker bwInterfaceUpdateWorker;

        public delegate void UpdateInterfaceEventHandler( object sender, EventArgs e );
        public event UpdateInterfaceEventHandler UpdateUI;

        #region Getters/Setters

        /// <summary>
        ///     The value to display initially.
        /// </summary>
        [
            Description( "The value to display initially." ),
            Category( "Appearance" )
        ]
        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if ( value != base.Text )
                {
                    base.Text = value;

                    this.Repaint();
                }
            }
        }

        private int refreshRate = 1000;

        /// <summary>
        ///     The rate at which the display is updated ( seconds ).
        /// </summary>
        [
            Description( "The rate at which the display is updated ( seconds )." ),
            Category( "Appearance" )
        ]
        public int RefreshRate
        {
            get
            {
                return refreshRate / 1000;
            }
            set
            {
                if ( value != refreshRate / 1000 )
                {
                    if ( value > 60 || value < 1 )
                    {
                        throw new ArgumentOutOfRangeException( "This value needs to be set between 1 and 60!" );
                    }

                    if ( DateTime.Now.Second > value )
                    {
                        tmrClockPulse.Interval = ( DateTime.Now.Second - value ) * 1000;
                    }

                    tmrClockPulse.Enabled = true;

                    refreshRate = value * 1000;

                    this.Repaint();
                }
            }
        }


        private string format = "dd-mm-yyyy | HH:MM";

        /// <summary>
        ///     The format used to display the time and / or date.
        /// </summary>
        [
            Description( "The format used to display the time and / or date." ),
            Category( "Appearance" )
        ]
        public string Format
        {
            get
            {
                return format;
            }
            set
            {
                if ( value != format )
                {
                    format = value;

                    this.Repaint();
                }
            }
        }

        #endregion


        public Clock()
        {
            InitializeComponent();

            Text = GetTimeString();
            
            // Start the backgroundworker.
            bwInterfaceUpdateWorker = new BackgroundWorker();
            bwInterfaceUpdateWorker.DoWork += InterfaceUpdateWorker_DoWork;

            tmrClockPulse = new System.Timers.Timer();
            tmrClockPulse.Elapsed += ClockPulse_Sync;        

            // Refresh immediately.
            RefreshRate = 1;
        }


        public string GetTimeString()
        {
            string output = format;
            
            output = output.Replace( "dd", DateTime.Now.Day.ToString().PadLeft( 2, '0' ) );
            output = output.Replace( "d", DateTime.Now.Day.ToString() );
            output = output.Replace( "mm", DateTime.Now.Month.ToString().PadLeft( 2, '0' ) );
            output = output.Replace( "m", DateTime.Now.Month.ToString() );
            output = output.Replace( "yyyy", DateTime.Now.Year.ToString() );
            output = output.Replace( "HH", DateTime.Now.Hour.ToString().PadLeft( 2, '0' ) );
            output = output.Replace( "MM", DateTime.Now.Minute.ToString().PadLeft( 2, '0' ) );
            output = output.Replace( "SS", DateTime.Now.Second.ToString().PadLeft( 2, '0' ) );

            return output;
        }

        private void InterfaceUpdateWorker_DoWork( object sender, DoWorkEventArgs e )
        {
            // Small check to prevent updating if the application is starting up or shutting down.
            if ( IsHandleCreated && !IsDisposed )
            {
                Invoke( ( MethodInvoker ) delegate() { 
                    
                    Text = GetTimeString();
                });

                if ( UpdateUI != null )
                {
                    // Propagate the call towards a public event to allow calling code to subscribe to it.
                    UpdateUI( sender, e );
                }
            }
        }

        private void ClockPulse_Sync( object sender, System.Timers.ElapsedEventArgs e )
        {
            bwInterfaceUpdateWorker.RunWorkerAsync();

            tmrClockPulse.Enabled = false;
            tmrClockPulse.Interval = refreshRate;
            
            tmrClockPulse.Elapsed -= ClockPulse_Sync;
            tmrClockPulse.Elapsed += ClockPulse_UpdateUI;
            tmrClockPulse.Enabled = true;
        }

        private void ClockPulse_UpdateUI( object sender, System.Timers.ElapsedEventArgs e )
        {
            bwInterfaceUpdateWorker.RunWorkerAsync();
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            Graphics gfx = e.Graphics;
            
            // Setup GDI to draw as accurate as smoothly as possible, accuracy is not critical in this case.
            gfx.InterpolationMode = InterpolationMode.NearestNeighbor;
            gfx.SmoothingMode = SmoothingMode.HighQuality;
            gfx.PixelOffsetMode = PixelOffsetMode.None;

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            // Format the text according to the parents settings and draw it to the center of the control.
            gfx.DrawString( 
                Text,
                base.Font,
                new SolidBrush( base.ForeColor ),
                new Rectangle( new Point( 0, 0 ), Size ),
                format
            );
        }

        protected override void OnPaintBackground( PaintEventArgs e )
        {
            Graphics gfx = e.Graphics;

            if ( BackgroundImage != null )
            {
                gfx.DrawImage( BackgroundImage, new Point( 0, 0 ) );
            }           
            else
            {
                gfx.Clear( BackColor );
            }
        }

        protected virtual void Repaint()
        {
            // Signal GDI to invalidate the cached image.
            // This causes the control to be repainted.
            this.Invalidate();
        }
    }
}