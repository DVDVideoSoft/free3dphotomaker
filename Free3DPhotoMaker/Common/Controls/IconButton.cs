using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace DVDVideoSoft.Controls
{
    /// <summary>
    /// Transparent button with transparent background.
    /// Based on VistaButton http://www.codeproject.com/KB/buttons/VistaButton.aspx by Tom 'Xasthom'
    /// </summary>
    [DefaultEvent("Click")]
    public class IconButton : System.Windows.Forms.UserControl, DVDVideoSoft.Utils.IDvsControl
    {
        #region Designer

            private System.ComponentModel.Container components = null;

            /// <summary>
            /// Initialize the component with it's default settings.
            /// </summary>
            public IconButton()
            {
                InitializeComponent();

                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                this.SetStyle(ControlStyles.DoubleBuffer, true);
                this.SetStyle(ControlStyles.ResizeRedraw, true);
                this.SetStyle(ControlStyles.Selectable, false);
                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                this.SetStyle(ControlStyles.UserPaint, true);

                this.BackColor = Color.Transparent;
                this.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            }

            /// <summary>
            /// Release resources used by the control.
            /// </summary>
            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    if(components != null)
                        components.Dispose();
                }
                base.Dispose(disposing);
            }

            #region Component Designer generated code

                private void InitializeComponent()
                {
                    // 
                    // IconButton
                    // 
                    this.Name = "IconButton";
                    this.Size = new System.Drawing.Size(100, 32);
                    this.Paint += new System.Windows.Forms.PaintEventHandler(this.IconButton_Paint);
                    this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.IconButton_KeyUp);
                    this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.IconButton_KeyDown);
                    this.MouseEnter += new System.EventHandler(this.IconButton_MouseEnter);
                    this.MouseLeave += new System.EventHandler(this.IconButton_MouseLeave);
                    this.MouseUp +=new MouseEventHandler(IconButton_MouseUp);
                    this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.IconButton_MouseDown);
                    //this.GotFocus +=new EventHandler(IconButton_MouseEnter);
                    //this.LostFocus += new EventHandler(IconButton_MouseLeave);
                    //this.Resize +=new EventHandler(IconButton_Resize);
                }

            #endregion

        #endregion
        
        #region Enums
        
            /// <summary>
            /// A private enumeration that determines the mouse state in relation to the current instance of the control.
            /// </summary>
            enum State { None, Hover, Pressed };

            /// <summary>
            /// A public enumeration that determines whether the button background is painted when the mouse is not inside the ClientArea.
            /// </summary>
            public enum Style
            {
                Default, 
                Flat
            };

        #endregion

        #region Properties

            #region Localization

            public void OnSystemThemeChanged(bool visualStylesEnabled)
            {
            }

            public void SetBackground(Bitmap background)
            {
            }
            
            #endregion

            #region Private Variables

            private bool normalDpiMode = true;
        
                private bool calledbykey = false;
                private State _state = State.None;
                private State state
                {
                    get { return this._state; }
                    set { this._state = value; }
                }

            #endregion

            #region Text

                public void AdjustSize()
                {
                    if (this.AutoSize && System.Reflection.Assembly.GetEntryAssembly() != null)
                        this.Width = GetTextLeft() +
                                        GetGraphics().MeasureString(this.Text, this.Font).ToSize().Width +
                                        9 +
                                        (this.DrawComboButton ? 7 : 0) +
                                        (this.normalDpiMode ? 0 : 3);
                }

                [Category("Text"),
                 Localizable(true),
                 Browsable(true),
                 EditorBrowsable(EditorBrowsableState.Always),
                 DesignerSerializationVisibility(DesignerSerializationVisibility.Visible),
                 System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)
                ]
                public override string Text
                {
                    get { return base.Text; }
                    set
                    {
                        base.Text = value;
                        
                        AdjustSize();
                        Invalidate();
                    }
                }

                private Color GetTextColor()
                {
                    if ((this.state == State.Hover || this.state == State.Pressed || this.PersistentHover) && !this.HoverColor.IsEmpty)
                        return Color.White;

                    return this.ForeColor;
                }

                public bool Highlighted { get; set; }

                public bool DrawFocusOnPressed { get; set; }

                private ContentAlignment textAlign = ContentAlignment.MiddleCenter;
                /// <summary>
                /// The alignment of the button text that is displayed on the control.
                /// </summary>
                [Category("Text"), 
                 DefaultValue(typeof(ContentAlignment),"MiddleCenter"),
                 Description("The alignment of the button text that is displayed on the control.")]
                public ContentAlignment TextAlign
                {
                    get { return this.textAlign; }
                    set
                    {
                        this.textAlign = value;
                        Invalidate();
                    }
                }
    
            #endregion

            #region Images

                private Image imageNorm;
                private Image imageOver;
                private Image imageDown;

                private Image imageNormA;
                private Image imageOverA;
                private Image imageDownA;

                [Category("Image"), DefaultValue(null), Description("The alternate normal state image.")]
                public Image ImageNormA
                {
                    get { return this.imageNormA; }
                    set
                    {
                        this.imageNormA = value;
                        Refresh();
                    }
                }

                [Category("Image"), DefaultValue(null), Description("The (basic) normal state image.")]
                public Image ImageNorm
                {
                    get { return this.imageNorm; }
                    set
                    {
                        this.imageNorm = value;
                        Refresh();
                    }
                }

                public Image GetImageNorm()
                {
                    return this.alternateState ? this.imageNormA : this.imageNorm;
                }

                [Category("Image"), DefaultValue(null), Description("The hover state image.")]
                public Image ImageOver
                {
                    get { return this.imageOver != null ? this.imageOver : this.imageNorm; }
                    set { this.imageOver = value; Refresh(); }
                }

                [Category("Image"), DefaultValue(null), Description("The alternate hover state image.")]
                public Image ImageOverA
                {
                    get { return this.imageOverA != null ? this.imageOverA : this.imageNormA; }
                    set { this.imageOverA = value; Refresh(); }
                }

                public Image GetImageOver()
                {
                    if (this.alternateState)
                        return this.imageOverA != null ? this.imageOverA : this.imageNormA;
                    else
                        return this.imageOver != null ? this.imageOver : this.imageNorm;
                }

                [Category("Image"), DefaultValue(null), Description("The pressed state image.")]
                public Image ImageDown
                {
                    get { return this.imageDown != null ? this.imageDown : this.imageNorm; }
                    set { this.imageDown = value; Refresh(); }
                }

                [Category("Image"), DefaultValue(null), Description("The alternate pressed state image.")]
                public Image ImageDownA
                {
                    get { return this.imageDownA != null ? this.imageDownA : this.imageNormA; }
                    set { this.imageDownA = value; Refresh(); }
                }

                public Image GetImageDown()
                {
                    if (this.alternateState)
                        return this.imageDownA != null ? this.imageDownA : this.imageNormA;
                    else
                        return this.imageDown != null ? this.imageDown : this.imageNorm;
                }

                private ContentAlignment mImageAlign = ContentAlignment.MiddleLeft;
                /// <summary>
                /// The alignment of the image in relation to the button.
                /// </summary>
                [Category("Image"), 
                 DefaultValue(typeof(ContentAlignment),"MiddleLeft"),
                 Description("The alignment of the image  in relation to the button.")]
                public ContentAlignment ImageAlign
                {
                    get { return mImageAlign; }
                    set { mImageAlign = value; Refresh(); }
                }

                private Size imageSize = new Size(24,24);
                /// <summary>
                /// The size of the image to be displayed on the button. This property defaults to 24x24.
                /// </summary>
                [Category("Image"), 
                 DefaultValue(typeof(Size),"24, 24"),
                 Description("The size of the image to be displayed on the button. This property defaults to 24x24.")]
                public Size ImageSize
                {
                    get { return imageSize; }
                    set { imageSize = value; Refresh(); }
                }

                /// <summary>
                /// The alignment of the image in relation to the text.
                /// </summary>
                [Category("Image"),
                 DefaultValue(typeof(TextImageRelation), "ImageBeforeText"),
                 Description("Text and image relation.")]
                public TextImageRelation TextImageRelation { get; set; }
    
            #endregion
        
            #region Appearance

                /// <summary>
                /// Text horizontal indent in pixels.
                /// </summary>
                [Category("Appearance"), 
                 DefaultValue(typeof(int),"0"),
                 Description("Text horizontal offset in pixels.")]
                public int TextHOffset { get; set; }

                /// <summary>
                /// Text vertical indent in pixels.
                /// </summary>
                [Category("Appearance"), 
                 DefaultValue(typeof(int),"0"),
                 Description("Text vertical offset in pixels.")]
                public int TextVOffset { get; set; }
                            
                private Style buttonStyle = Style.Default;
                /// <summary>
                /// Sets whether the button background is drawn while the mouse is outside of the client area.
                /// </summary>
                [Category("Appearance"), 
                 DefaultValue(typeof(Style),"Default"),
                 Description("Sets whether the button background is drawn while the mouse is outside of the client area.")]
                public Style ButtonStyle
                {
                    get { return buttonStyle; }
                    set { buttonStyle = value; Refresh(); }
                }

                private int cornerRadius = 8;
                /// <summary>
                /// The radius for the button corners. The  greater this value is, the more 'smooth' the corners are.
                /// This property should not be greater than half of the controls height.
                /// </summary>
                [Category("Appearance"), 
                 DefaultValue(8),
                 Description("The radius for the button corners. The greater this value is, the more 'smooth' the corners are. This property should not be greater than half of the controls height.")]
                public int CornerRadius
                {
                    get { return cornerRadius; }
                    set { cornerRadius = value; Refresh(); }
                }

                private Color highlightColor = Color.White;
                /// <summary>
                /// The color of the highlight on the top of the button.
                /// </summary>
                [Category("Appearance"), 
                 DefaultValue(typeof(Color), "White"),
                 Description("The color of the highlight on the top of the button.")]
                public Color HighlightColor
                {
                    get { return highlightColor; }
                    set { this.highlightColor = value; Refresh(); }
                }

                private Color textShadowColor = Color.Transparent;
                /// <summary>
                /// The color of the highlight on the top of the button.
                /// </summary>
                [Category("Appearance"), 
                 DefaultValue(typeof(Color), "Transparent"),
                 Description("The button text shadow color.")]
                public Color TextShadowColor
                {
                    get { return textShadowColor; }
                    set { this.textShadowColor = value; Refresh(); }
                }

                /// <summary>
                /// The color of the highlighted text.
                /// </summary>
                [Category("Appearance"),
                 DefaultValue(typeof(Color), "Transparent"),
                 Description("The text color in hover state.")]
                public Color TextHighlightColor { get; set; }

                [Category("Appearance"),
                 DefaultValue(typeof(Color), "Transparent"),
                 Description("The background color for the button in hover state.")]
                public Color HoverColor { get; set; }

                private bool drawButtonBackground;
                [Category("Appearance"),
                 DefaultValue(typeof(bool), "false"),
                 Description("To draw or not to draw, what's the question.")]
                public bool DrawButtonBackground
                {
                    get { return this.drawButtonBackground; }
                    set { this.drawButtonBackground = value; }
                }

                [Category("Appearance"),
                 DefaultValue(typeof(bool), "false"),
                 Description("Defines whether the button has rounded corners.")]
                public bool RoundedBorders { get; set; }
        
                [Category("Appearance"),
                 DefaultValue(typeof(bool), "false"),
                 Description("Defines whether to draw pressed state on click.")]
                public bool DrawDownInPressedState { get; set; }

                /// <summary>
                /// Whether to draw a combo-like triangle in the right corner in hover mode.
                /// </summary>
                [Category("Appearance"),
                 DefaultValue(typeof(bool), "false"),
                 Description("Whether to draw a combo-like triangle in the right corner in hover mode.")]
                public bool DrawComboButton { get; set; }

                private bool persistentHover;
                /// <summary>
                /// If true, the button will not loose hover state on mouse leave.
                /// </summary>
                [Category("Appearance"),
                 DefaultValue(typeof(bool), "false"),
                 Description("If true, the button will not loose hover state on mouse leave.")]
                public bool PersistentHover
                {
                    get { return this.persistentHover; }
                    set
                    {
                        bool oldPersistentHover = this.persistentHover;
                        this.persistentHover = value;

                        if (!value && (oldPersistentHover || this.state == State.Hover))
                        {
                            IconButton_MouseLeave(this, new EventArgs());
                            //this.state = State.None;
                            //this.Refresh();
                        }
                        //this.Refresh();
                    }
                }

            #endregion

        #endregion

        #region Public members

                public void AdjustOnBackgroundChange(bool finalStage)
                {
                    this.Visible = finalStage;
                    this.BackColor = Color.Transparent;
                    //if (finalStage)
                    //    this.BackColor = Color.Transparent;
                    //else
                    //    this.BackColor = this.Parent.BackColor;
                }

        #endregion

        #region Functions

            private GraphicsPath RoundRect(RectangleF r, float r1, float r2, float r3, float r4)
            {
                float x = r.X, y = r.Y, w = r.Width, h = r.Height;
                GraphicsPath rr = new GraphicsPath();
                rr.AddBezier(x, y + r1, x, y, x + r1, y, x + r1, y);
                rr.AddLine(x + r1, y, x + w - r2, y);
                rr.AddBezier(x + w - r2, y, x + w, y, x + w, y + r2, x + w, y + r2);
                rr.AddLine(x + w, y + r2, x + w, y + h - r3);
                rr.AddBezier(x + w, y + h - r3, x + w, y + h, x + w - r3, y + h, x + w - r3, y + h);
                rr.AddLine(x + w - r3, y + h, x + r4, y + h);
                rr.AddBezier(x + r4, y + h, x, y + h, x, y + h - r4, x, y + h - r4);
                rr.AddLine(x, y + h - r4, x, y + r1);
                return rr;
            }

            private StringFormat StringFormatAlignment(ContentAlignment textalign)
            {
                StringFormat sf = new StringFormat();
                switch (textalign)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.TopRight:
                        sf.LineAlignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.MiddleRight:
                        sf.LineAlignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.BottomCenter:
                    case ContentAlignment.BottomRight:
                        sf.LineAlignment = StringAlignment.Far;
                        break;
                }
                switch (textalign)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.BottomLeft:
                        sf.Alignment = StringAlignment.Near;
                        break;
                    case ContentAlignment.TopCenter:
                    case ContentAlignment.MiddleCenter:
                    case ContentAlignment.BottomCenter:
                        sf.Alignment = StringAlignment.Center;
                        break;
                    case ContentAlignment.TopRight:
                    case ContentAlignment.MiddleRight:
                    case ContentAlignment.BottomRight:
                        sf.Alignment = StringAlignment.Far;
                        break;
                }
                return sf;
            }

        #endregion

        #region Drawing

            private Graphics graphics = null;
            /// <summary>
            /// Avoid multiple CreateGraphics().
            /// </summary>
            Graphics GetGraphics()
            {
                if (this.graphics == null)
                    this.graphics = this.CreateGraphics();

                return this.graphics;
            }

            private bool alternateState;

            [Category("Image"), DefaultValue(null),
             Description("To draw the normal or an alternate state image."),
             System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
            public bool AlternateState
            {
                get { return this.alternateState; } 
                set
                {
                    this.alternateState = value;
                    Refresh();
                }
            }

            /// <summary>
            /// Draws the background for the control using the background image and the BaseColor.
            /// </summary>
            /// <param name="g">The graphics object used in the paint event.</param>
            private void DrawBackground(Graphics g)
            {
                Rectangle r;

                if (this.state == State.Hover || this.state == State.Pressed || this.persistentHover)
                {
                    if (!this.HoverColor.IsEmpty)
                    {
                        //g.ResetClip();
                        using (SolidBrush sb = new SolidBrush(this.HoverColor))
                        {
                            g.SmoothingMode = SmoothingMode.None;
                            g.FillRectangle(sb, this.ClientRectangle);
                        }
                    }
                    return;
                }
                else if (state == State.Pressed && this.DrawFocusOnPressed)
                {
                    if (!this.RoundedBorders)
                    {
                        //                        g.ResetClip();
                        using (SolidBrush sb = new SolidBrush(this.HighlightColor))
                        {
                            g.FillRectangle(sb, this.ClientRectangle);
                        }
                    }
                    else
                    {
                        r = new Rectangle(0, 0, this.Width - 2, this.Height - 2);

                        using (GraphicsPath rr = RoundRect(r, CornerRadius, CornerRadius, CornerRadius, CornerRadius))
                        {
                            using (Pen pen = new Pen(Color.FromArgb(83, 125, 147)))
                            {
                                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                                g.DrawPath(pen, rr);
                            }
                        }
                    }
                }
            }

            private int GetTextLeft()
            {
                int left = 0;
                if (this.TextImageRelation != System.Windows.Forms.TextImageRelation.Overlay)
                    left = this.ImageRect.Right;
                
                return left + (this.ImageSize.Width > 0 ? 3 : 0) + this.Padding.Left + this.TextHOffset;
            }

            /// <summary>
            /// Draws the text for the button.
            /// </summary>
            /// <param name="g">The graphics object used in the paint event.</param>
            private void DrawText(Graphics g)
            {
                StringFormat sf = StringFormatAlignment(this.TextAlign);
                int left = GetTextLeft();

                Rectangle r = new Rectangle(left, this.TextVOffset, this.Width - left, this.Height);

                if (this.state == State.Pressed && this.DrawDownInPressedState)
                    r.Offset(1, 1);

                Font fontOfTheState = null;
                if (this.state == State.Hover || this.state == State.Pressed || this.persistentHover && (this.Font.Style | FontStyle.Underline) == this.Font.Style)
                    fontOfTheState = new Font(this.Font, FontStyle.Regular | (((this.Font.Style | FontStyle.Bold) == this.Font.Style) ? FontStyle.Bold : FontStyle.Regular));
                else
                    fontOfTheState = this.Font;

                if (this.TextShadowColor != Color.Transparent)
                {
                    Rectangle backTextRect = r;
                    backTextRect.Offset(1, 1);
                    g.DrawString(this.Text, this.Font, new SolidBrush(this.TextShadowColor), backTextRect, sf);
                }

                Color currentTextColor = this.GetTextColor();
                //if (!this.Enabled)
                //    currentTextColor = Color.Gray;
                //else 
                if ((this.state == State.Hover && this.TextImageRelation == System.Windows.Forms.TextImageRelation.ImageBeforeText && this.HighlightColor != Color.Transparent) || this.Highlighted)
                    currentTextColor = this.TextHighlightColor;

                g.DrawString(this.Text, fontOfTheState, new SolidBrush(currentTextColor), r, sf);
            }

            private Rectangle ImageRect
            {
                get { return new Rectangle(0, 0, this.ImageSize.Width, this.ImageSize.Height); }
            }

            /// <summary>
            /// Draws the image for the button
            /// </summary>
            /// <param name="g">The graphics object used in the paint event.</param>
            private void DrawImage(Graphics g)
            {
                if (this.ImageNorm == null)
                    return;

                //int y = (this.Height - this.ImageSize.Height) / 2;
                Rectangle r = new Rectangle(this.Padding.Left, this.Padding.Top, this.ImageSize.Width, this.ImageSize.Height);
                //new Rectangle(ImageRect.Location, ImageRect.Size);//

                switch (this.ImageAlign)
                {
                    //case ContentAlignment.TopCenter:
                    //    r = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2,8,this.ImageSize.Width,this.ImageSize.Height);
                    //    break;
                    //case ContentAlignment.TopRight:
                    //    r = new Rectangle(this.Width - 8 - this.ImageSize.Width,8,this.ImageSize.Width,this.ImageSize.Height);
                    //    break;
                    case ContentAlignment.MiddleLeft:
                        r = new Rectangle(0, this.Height / 2 - this.ImageSize.Height / 2, this.ImageSize.Width, this.ImageSize.Height);
                        break;
                    //case ContentAlignment.MiddleCenter:
                    //    r = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2,this.Height / 2 - this.ImageSize.Height / 2,this.ImageSize.Width,this.ImageSize.Height);
                    //    break;
                    //case ContentAlignment.MiddleRight:
                    //    r = new Rectangle(this.Width - 8 - this.ImageSize.Width,this.Height / 2 - this.ImageSize.Height / 2,this.ImageSize.Width,this.ImageSize.Height);
                    //    break;
                    //case ContentAlignment.BottomLeft:
                    //    r = new Rectangle(0, this.Height - 8 - this.ImageSize.Height,this.ImageSize.Width,this.ImageSize.Height);
                    //    break;
                    //case ContentAlignment.BottomCenter:
                    //    r = new Rectangle(this.Width / 2 - this.ImageSize.Width / 2,this.Height - 8 - this.ImageSize.Height,this.ImageSize.Width,this.ImageSize.Height);
                    //    break;
                    //case ContentAlignment.BottomRight:
                    //    r = new Rectangle(this.Width - 8 - this.ImageSize.Width,this.Height - 8 - this.ImageSize.Height,this.ImageSize.Width,this.ImageSize.Height);
                    //    break;
                }

                if (this.state == State.Pressed && this.DrawDownInPressedState)
                    r.Offset(1, 1);

                Image currentStateImage = this.GetImageNorm();
                //g.DrawImage(this.GetImageNorm(), r);
                //if (!this.Enabled)
                //    currentStateImage = this.imageDisa;
                if (this.state == State.Hover || this.persistentHover)
                    currentStateImage = this.GetImageOver();
                else if (this.state == State.Pressed)
                    currentStateImage = this.GetImageDown();

                if ((this.state == State.Hover || this.persistentHover) && this.DrawComboButton)
                {
                    Point[] points = { new Point(this.Width - 14, this.Height / 2 - 2), new Point(this.Width - 6, this.Height / 2 - 2), new Point(this.Width - 10, this.Height / 2 + 2) };
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.FillPolygon(this.state == State.None && !this.persistentHover ? Brushes.Black : Brushes.White, points);
                }

                //if (this.Padding.Top > 0)
                //    r.Offset(0, this.Padding.Top);
                g.DrawImage(currentStateImage, r);
            }

        #endregion

        #region Private routines

        private void IconButton_Paint(object sender, PaintEventArgs e)
        {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                DrawBackground(e.Graphics);

                DrawImage(e.Graphics);
                DrawText(e.Graphics);
        }

        #region Mouse and Keyboard Events

        private void IconButton_MouseEnter(object sender, EventArgs e)
        {
            this.state = State.Hover;
			Refresh();
        }

        private void IconButton_MouseLeave(object sender, EventArgs e)
        {
            this.state = State.None;
            Refresh();
        }
        private void IconButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.Enabled)
            {
                this.inDelayedPressedState = true;
                this.state = State.Pressed;
                Refresh();
            }
        }

        private bool inDelayedPressedState = false;

        private void IconButton_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (inDelayedPressedState)
                {
                    this.state = State.None;
                    this.inDelayedPressedState = false;
                    Refresh();
                    return;
                }
                state = !this.HoverColor.IsEmpty ? State.Hover : State.None;
                Refresh();
                if (calledbykey == true)
                {
                    this.OnClick(EventArgs.Empty);
                    calledbykey = false;
                }

                if (this.ContextMenuStrip != null && this.ContextMenuStrip.Items.Count > 0)
                    this.ContextMenuStrip.Show(this, new Point(0, Height));
            }
        }

        private void IconButton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MouseEventArgs m = new MouseEventArgs(MouseButtons.Left,0,0,0,0);
                IconButton_MouseDown(sender, m);
            }
        }

        private void IconButton_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                MouseEventArgs m = new MouseEventArgs(MouseButtons.Left,0,0,0,0);
                calledbykey = true;
                IconButton_MouseUp(sender, m);
            }
        }

        #endregion

        #endregion
    }
}
