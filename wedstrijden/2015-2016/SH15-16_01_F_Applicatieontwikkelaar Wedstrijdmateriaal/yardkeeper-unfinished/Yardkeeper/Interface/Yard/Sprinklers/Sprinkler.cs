using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using System.Drawing;
using System.Drawing.Drawing2D;

using System.Threading.Tasks;

namespace Yardkeeper
{
    public partial class Sprinkler : Control
    {
        #region Getter / Setters and class vars.

        private Timer telemetryFaker;
        
        private bool incrementSweepAngle;

        /// <summary>
        ///     Whether the animation needs to run.
        /// </summary>
        [
            Description( "Whether the animation needs to run." ),
            Category( "Behavior" )
        ]
        public bool IsRunning { get; set; }
        
        /// <summary>
        ///     Animation update rate.
        /// </summary>
        [
            Description( "Animation update rate." ),
            Category( "Behavior" )
        ]
        public int RefreshRate { get; set; }
 
        /// <summary>
        ///     The amount of water this sprinkler is able to process.
        /// </summary>
        [
            Description( "The amount of water this sprinkler is able to process." ),
            Category( "Data" )
        ]
        public double IntakeRate { get; set; }
       
        #region Sprinkler range and directional control vars.

        /// <summary>
        ///     The distance this sprinkler is able to cover.
        /// </summary>
        [
            Description( "The distance this sprinkler is able to cover." ),
            Category( "Control" )
        ]
        public double Range { get; set; }

        private int angle;
        
        /// <summary>
        ///     The angle this sprinkler is currently aimed at.
        /// </summary>
        [
            Description( "The angle this sprinkler is currently aimed at." ),
            Category( "Control" )
        ]
        public int Angle
        {
            get
            {
                return angle;
            }
            set
            {
                if ( value != angle )
                {
                    if ( angle > maximumAngle )
                    {
                        value = minimumAngle;
                    }

                    // Override the angle in case there is less than one 
                    // step of space remaining within the preset range.
                    if ( angle + step >= maximumAngle )
                    {
                        // Effectively, this enforces the minimum and maximum range of the projection.
                        angle = ( maximumAngle - step );
                    }

                    angle = value;

                    this.Repaint();
                }
            }
        }

        private int minimumAngle;
        
        /// <summary>
        ///     Minimum spraying angle.
        /// </summary>
        [
            Description( "Minimum spraying angle." ),
            Category( "Control" )
        ]
        public int MinimumAngle
        {
            get
            {
                return minimumAngle;
            }
            set
            {
                if ( value != minimumAngle )
                {
                    if ( minimumAngle < 0 )
                    {
                        throw new ArgumentOutOfRangeException( "Minimum angle needs to be higher than 0!" );
                    }

                    if ( maximumAngle != 0 && value > maximumAngle )
                    {
                        throw new ArgumentOutOfRangeException( "Mimimum angle cannot be higher than the Maximum angle!" );
                    }

                    minimumAngle = value;

                    this.Repaint();
                }
            }
        }

        private int maximumAngle;

        /// <summary>
        ///     Maximum spraying angle.
        /// </summary>
        [
            Description( "Maximum spraying angle." ),
            Category( "Control" )
        ]
        public int MaximumAngle
        {
            get
            {
                return maximumAngle;
            }
            set
            {
                if ( value != maximumAngle )
                {
                    if ( maximumAngle > 360 )
                    {
                        throw new ArgumentOutOfRangeException( "Maximum angle is 360 degrees!" );
                    }

                    if ( minimumAngle > value )
                    {
                        throw new ArgumentOutOfRangeException( "Mimimum angle cannot be higher than the Maximum angle!" );
                    }

                    maximumAngle = value;

                    this.Repaint();
                }
            }
        }

        private int step;
        
        /// <summary>
        ///     The size of the spread this sprinkler produces.
        /// </summary>
        [
            Description( "The size of the spread this sprinkler produces." ),
            Category( "Control" )
        ]    
        public int Step
        {
            get
            {
                return step;
            }
            set
            {
                if ( step > ( maximumAngle - minimumAngle ) )
                {
                    throw new ArgumentOutOfRangeException( "Step size cannot exceed the available range!" );
                }

                step = value;

                this.Repaint();
            }
        }

        #endregion

        #region Flag values to customize the appearance of this sprinkler.

        private bool drawRange = true;

        /// <summary>
        ///     Whether the range of this sprinkler is drawn around the center.
        /// </summary>
        [
            Description( "Whether the range of this sprinkler is drawn around the center." ),
            Category( "Appearance" )
        ]
        public bool DrawRange 
        { 
            get 
            {
                return drawRange;
            }
            set
            {
                if ( value != drawRange )
                {
                    drawRange = value;

                    this.Repaint();
                }
            }
        }
        
        private bool drawRangeProjection = true;
        
        /// <summary>
        ///     Whether the current range of this sprinkler is drawn.
        /// </summary>
        [
            Description( "Whether the current range of this sprinkler is drawn." ),
            Category( "Appearance" )
        ]        
        public bool DrawRangeProjection
        {
            get 
            {
                return drawRangeProjection;
            }
            set
            {
                if ( value != drawRangeProjection )
                {
                    drawRangeProjection = value;

                    this.Repaint();
                }
            }
        }

        private bool drawCenter = true;
        
        /// <summary>
        ///     Whether the actual location of this sprinkler is drawn in the center.
        /// </summary>
        [
            Description( "Whether the actual location of this sprinkler is drawn in the center." ),
            Category( "Appearance" )
        ]        
        public bool DrawCenter
        {
            get 
            {
                return drawCenter;
            }
            set
            {
                if ( value != drawCenter )
                {
                    drawCenter = value;

                    this.Repaint();
                }
            }
        }

        #endregion

        #region Appearance and customization.

        private int centerSize = 5;
        
        /// <summary>
        ///     The size of the center circle in pixels.
        /// </summary>
        [
            Description( "The size of the center circle in pixels." ),
            Category( "Appearance" )
        ]  
        public int CenterSize
        {
            get
            {
                return centerSize;
            }
            set
            {
                if ( value != centerSize )
                {
                    centerSize = value;

                    this.Repaint();
                }
            }
        }

        private int paintWeight = 1;

        private Pen foreColorPen;
        private SolidBrush centerFillBrush;

        private Pen rangeColorPen;
        private SolidBrush rangeFillBrush;

        /// <summary>
        ///     The color used to draw the control.
        /// </summary>
        [
            Description( "The color used to draw the control."),
            Category( "Appearance" )
        ]
        public new Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                if ( value != base.ForeColor )
                {
                    base.ForeColor = value;
                    centerFillBrush = new SolidBrush( 
                        Color.FromArgb(

                            128,
                            value.R, 
                            value.G,
                            value.B
                        ) 
                    );
                    foreColorPen = new Pen( value, paintWeight );

                    rangeFillBrush = new SolidBrush( 
                        centerFillBrush.Color
                    );
                    rangeColorPen = new Pen( value, paintWeight );



                    this.Repaint();
                }
            }
        }

        #endregion

        #endregion

        public Sprinkler()
        {
            InitializeComponent();

            ForeColor = Color.DimGray;
            IsRunning = false;

            RefreshRate = 350;

            telemetryFaker = new Timer();
            telemetryFaker.Interval = RefreshRate;
            telemetryFaker.Tick += telemetryFaker_Tick;
        }

        #region Control state behavior

        public void Activate()
        {
            if ( IsRunning )
            {
                return;
            }

            telemetryFaker.Enabled = true;
            IsRunning = true;
        }

        public void Shutdown()
        {
            if ( IsRunning )
            {
                telemetryFaker.Enabled = false;
                IsRunning = false;
            }
        }

        private void telemetryFaker_Tick( object sender, EventArgs e )
        {
            if ( angle == minimumAngle )
            {
                incrementSweepAngle = true;
            }
            
            if ( angle == MaximumAngle )
            {
                incrementSweepAngle = false;
            }

            
            if ( incrementSweepAngle )
            {
                ++angle;
            }
            else
            {
                --angle;
            } 

            this.Repaint();
        }

        #endregion


        #region Control paint behavior

        protected override void OnPaint( PaintEventArgs e )
        {
            Graphics gfx = e.Graphics;
            int width, height;

            // Setup GDI to draw as accurate as smoothly as possible, accuracy is not critical in this case.
            gfx.InterpolationMode = InterpolationMode.HighQualityBilinear;
            gfx.SmoothingMode = SmoothingMode.AntiAlias;
            gfx.PixelOffsetMode = PixelOffsetMode.None;
            
            // Further draw logic here.

            width = ClientRectangle.Width - paintWeight;
            height = ClientRectangle.Height - paintWeight;
            

            // Draw the yield perimeter around the center.

            if ( drawRange )
            {
                gfx.DrawEllipse( 
                    foreColorPen,
                    0,  
                    0, 
                    width,
                    width
                );
            }

            // Draw the center of the control ( the filled dot in the center ).

            gfx.FillEllipse( 
                centerFillBrush, 
                ( width - ( centerSize - paintWeight ) ) / 2,  
                ( height - ( centerSize - paintWeight ) ) / 2, 
                centerSize,
                centerSize
            );

            gfx.DrawEllipse( 
                foreColorPen,
                ( width - ( centerSize - paintWeight ) ) / 2,  
                ( height - ( centerSize - paintWeight ) ) / 2, 
                centerSize,
                centerSize
            );

            if ( drawRangeProjection )
            {
                // For some obscure reason the starting angle is measured
                // relative to the x-axis and therefore off by 90 degrees.
                int startingAngle = angle - ( 90 + ( step / 2 ) );
                int sweepAngle = step;

                gfx.FillPie( 
                    rangeFillBrush,
                    0,
                    0,
                    width,
                    width,
                    startingAngle,
                    sweepAngle
                );
            }
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


        protected override void SetClientSizeCore( int x, int y )
        {
            if ( x != base.Width || y != base.Height )
            {
                if ( x <= centerSize )
                {
                    throw new ArgumentOutOfRangeException( "The minimum width for this control is the centerSize!" );
                }

                if ( y <= centerSize )
                {
                    throw new ArgumentOutOfRangeException( "The minimum height for this control is the centerSize!" );
                }

                if ( y >= x )
                {
                    x = y;
                }

                base.Width = x;
                base.Height = x;

                this.Repaint();
            }
        }


        private void Repaint()
        {
            Invalidate();
        }

        #endregion
    }
}