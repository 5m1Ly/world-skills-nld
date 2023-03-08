using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

using System.Drawing;
using System.Drawing.Drawing2D;

using System.Threading.Tasks;

namespace Yardkeeper
{
    public partial class Yard : Control
    {

        #region Variables, getters/setters.

        #region Metrics

        private RealLifeSize dimensions = new RealLifeSize( 1, 1 );

        /// <summary>
        ///     Real life size of the yard in meters or yards.
        /// </summary>
        [
            Description( "Real life size of the yard in meters or yards." ),
            Category( "Appearance" ),
            Browsable( true )
        ]
        public RealLifeSize Dimensions
        {
            get
            {
                return dimensions;
            }
            set
            {
                if ( value != dimensions )
                {
                    double width, length;
                    
                    width = value.Width;
                    length = value.Length;
                    
                    // Rotate 90 degrees in order to draw as big as possible.
                    if ( width > length )
                    {
                        length = value.Width;
                        width = value.Length;
                    }

                    // Calculate internal drawing scale, 1m = scale ( px );
                    scale = new Point(
                        
                        ( Parent.Width > width ) ? ( int ) ( Parent.Width / width ) : ( int ) ( width / Parent.Width ),
                        ( Parent.Height > length ) ? ( int ) ( Parent.Height / length ) : ( int ) ( length / Parent.Height )
                    );
                    
                    // Apply internal drawing scale to the size of the yard.
                    base.Width = ( int ) ( width * scale.X );
                    base.Height = ( int ) ( length * scale.Y );
                    
                    base.Location = new Point ( 
                    
                        ( int ) ( ( Parent.Width - Width ) / 2 ),
                        ( int ) ( ( Parent.Height - Height ) / 2 )
                    );

                    // Redraw the yard.
                    this.Repaint();
                }
            }
        }

        private Point scale;

        [
            Description( "Width of the control."),
            Category( "Appearance" )
        ]
        public new int Width 
        { 
            get
            {
                return base.Width;
            }
            set
            {
                if ( value != base.Width )
                {
                    base.Width = value;

                    Repaint();
                }
            } 
        }


        [
            Description( "Height of the control."),
            Category( "Appearance" )
        ]
        public new int Height
        { 
            get
            {
                return base.Height;
            }
            set
            {
                if ( value != base.Height )
                {
                    base.Height = value;

                    Repaint();
                }
            } 
        }

        #endregion

        #region Graphical appearance settings

        private SolidBrush backColorBrush;
        private Pen backColorPen;

        /// <summary>
        ///     Controls the background color of the yard.
        /// </summary>
        [
            Description( "Background color of the yard."),
            Category( "Appearance" )
        ]
        public new Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                if ( value != base.BackColor )
                {
                    base.BackColor = value;
                    backColorBrush = new SolidBrush( base.BackColor );
                    backColorPen = new Pen( backColorBrush );

                    this.Repaint();
                }
            }
        }

        private Color borderColor;
        private SolidBrush borderColorBrush;
        private Pen borderColorPen;

        /// <summary>
        ///     Controls the background color of the yard.
        /// </summary>
        [
            Description( "Border color of the yard."),
            Category( "Appearance" )
        ]
        public Color BorderColor
        {
            get
            {
                return borderColor;
            }
            set
            {
                if ( value != borderColor )
                {
                    borderColor = value;
                    borderColorBrush = new SolidBrush( borderColor );
                    borderColorPen = new Pen( borderColorBrush );
                    
                    this.Repaint();
                }
            }
        }

        private byte borderWidth = 3;
        public byte BorderWidth
        {
            get
            {
                return borderWidth;
            }
            set
            {
                if ( value != borderWidth )
                {
                    borderWidth = value;
                    borderColorPen.Width = ( float ) value;

                    this.Repaint();
                }
            }
        }

        #endregion 

        #endregion

        #region Control setup logic

        public Yard()
        {
            InitializeComponent();

            this.BackColor = Color.DimGray;
            this.BorderColor = Color.Black;
        }

        
        public Size GetBounds()
        {
            return new Size(
                ClientRectangle.Width - ( borderWidth * 2 ),
                ClientRectangle.Height - ( borderWidth * 2 )
            );
        }

        #endregion

        #region Control child element placement

        public ControlCollection GetControls()
        {
            return this.Controls;
        }

        public void ResetControls()
        {
            this.Controls.Clear();
        }

        public bool SetControls( Control[] controls )
        {
            foreach ( Control ctrl in controls )
            {
                if ( !SetControl( ctrl ) )
                {
                    return false;
                }
            }

            return true;
        }


        public bool SetControl( Control control )
        {
            control.Location = new Point( 
                ( int ) ( ( control.Location.X / 10 ) * scale.X ) + borderWidth,
                ( int ) ( ( control.Location.Y / 10 ) * scale.Y ) + borderWidth
            );

            control.Size = new Size(
                
                ( int ) ( control.Size.Width * scale.X ),
                ( int ) ( control.Size.Height * scale.X )
            );
            
            if ( !IsValidControlPosition( control ) )
            {
                return false;
            }

            this.Controls.Add( control );

            return true;
        }
        

        public Point GetRandomControlPosition()
        {
            Random rnd = new Random();
            return new Point( 
                rnd.Next( GetBounds().Width ), 
                rnd.Next( GetBounds().Height )
            );
        }


        public bool IsValidControlPosition( Control control )
        {
            int boundsX = GetBounds().Width;
            int boundsY = GetBounds().Height;
            Rectangle requested, existing;

            // Check the requested placement against the bounds of the yard.
            if ( 
                ( control.Top < 0 || control.Top + control.Height > boundsY ) ||
                ( control.Left + control.Width < 0 || control.Left + control.Width > boundsX )
            )
            {
                // Out of bounds.
                return false;
            }
            
            requested = new Rectangle( 

                control.Left, 
                control.Top, 
                control.Width, 
                control.Height 
            );

            // Iterate existing sprinklers to check for collisions.
            foreach ( Control ctrl in this.Controls )
            {
		        existing = new Rectangle( ctrl.Left, ctrl.Top, ctrl.Width, ctrl.Height );
		        if ( requested.IntersectsWith( existing ) )
		        {
                    // Collides with existing sprinkler.
			        return false;
		        }
            }

            return true;
        }

        #endregion

        #region Control paint behavior

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


        protected override void OnPaint( PaintEventArgs e )
        {
            Graphics gfx = e.Graphics;
            Rectangle rect;
            Point location = new Point ( 0, 0 );
                
            Size size = new Size( 
                
                Width, 
                Height 
            );

            // Setup GDI to draw as accurate as smoothly as possible, accuracy is not critical in this case.
            gfx.SmoothingMode = SmoothingMode.AntiAlias;
            gfx.PixelOffsetMode = PixelOffsetMode.None;
            
            if ( borderWidth > 0 )
            {
                size.Width = ClientRectangle.Width - 1;
                size.Height = ClientRectangle.Height - 1;

                rect = new Rectangle( location, size );
                gfx.DrawRectangle( borderColorPen, rect );

                // If a border is used, set the drawable area accordingly.

                location.X = location.X + borderWidth;
                location.Y = location.Y + borderWidth;

                size.Width -= ( borderWidth * 2 );
                size.Height -= ( borderWidth * 2 );
            }

            // Further draw logic here.
        }

        private void Repaint()
        {
            Invalidate();
        }


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

        #endregion
    }
}
