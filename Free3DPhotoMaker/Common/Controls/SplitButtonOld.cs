using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Drawing.Drawing2D;

namespace DVDVideoSoft.Controls
{
    public partial class SplitButtonOld : Button
    {
        #region Fields

        private bool doubleClickedEnabled;

        private bool alwaysDropDown;
        private bool alwaysHoverChange;

        private bool calculateSplitRect = true;
        private bool fillSplitHeight = true;
        private int splitHeight;
        private int splitWidth;

        private String normalImage;
        private String hoverImage;
        private String clickedImage;
        private String disabledImage;
        private String focusedImage;

        private ImageList defaultSplitImages;


        [Browsable(true)]
        [Category("Action")]
        [Description("Occurs when the button part of the SplitButton is clicked.")]
        public event EventHandler ButtonClick;

        [Browsable(true)]
        [Category("Action")]
        [Description("Occurs when the button part of the SplitButton is clicked.")]
        public event EventHandler ButtonDoubleClick;

        #endregion
        #region Properties
        [Category("Behavior")]
        [Description("Indicates whether the double click event is raised on the SplitButton")]
        [DefaultValue(false)]
        public bool DoubleClickedEnabled
        {
            get { return this.doubleClickedEnabled; }
            set { this.doubleClickedEnabled = value; }
        }

        [Category("Split Button")]
        [Description("Indicates whether the SplitButton always shows the drop down menu even if the button part of the SplitButton is clicked.")]
        [DefaultValue(false)]
        public bool AlwaysDropDown
        {
            get { return this.alwaysDropDown; }
            set { this.alwaysDropDown = value; }
        }

        [Category("Split Button")]
        [Description("Indicates whether the SplitButton always shows the Hover image status in the split part even if the button part of the SplitButton is hovered.")]
        [DefaultValue(false)]
        public bool AlwaysHoverChange
        {
            get { return this.alwaysHoverChange; }
            set { this.alwaysHoverChange = value; }
        }

        [Category("Split Button")]
        [Description("Indicates whether the split rectange must be calculated (basing on Split image size)")]
        [DefaultValue(true)]
        public bool CalculateSplitRect
        {
            get { return this.calculateSplitRect; }
            set
            {
                bool flag1 = this.calculateSplitRect;

                this.calculateSplitRect = value;

                if (flag1 != this.calculateSplitRect)
                {
                    if (this.splitWidth > 0 && this.splitHeight > 0)
                    {
                        InitDefaultSplitImages(true);
                    }
                }
            }
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
                this.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [fill split height].
        /// </summary>
        /// <value><c>true</c> if [fill split height]; otherwise, <c>false</c>.</value>
        [Category("Split Button")]
        [Description("Indicates whether the split height must be filled to the button height even if the split image height is lower.")]
        [DefaultValue(true)]
        public bool FillSplitHeight
        {
            get { return this.fillSplitHeight; }
            set { this.fillSplitHeight = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [Category("Split Button")]
        [Description("The split height (ignored if CalculateSplitRect is setted to true).")]
        [DefaultValue(0)]
        public int SplitHeight
        {
            get { return this.splitHeight; }
            set
            {
                this.splitHeight = value;

                if (!this.calculateSplitRect)
                {
                    if (this.splitWidth > 0 && this.splitHeight > 0)
                        InitDefaultSplitImages(true);
                }
            }
        }

        [Category("Split Button")]
        [Description("The split width (ignored if CalculateSplitRect is setted to true).")]
        [DefaultValue(0)]
        public int SplitWidth
        {
            get { return this.splitWidth; }
            set
            {
                this.splitWidth = value;

                if (!this.calculateSplitRect)
                {
                    if (this.splitWidth > 0 && this.splitHeight > 0)
                        InitDefaultSplitImages(true);
                }
            }
        }

        [Category("Split Button Images")]
        [Description("The Normal status image name in the ImageList.")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true), RefreshProperties(RefreshProperties.Repaint), TypeConverter(typeof(ImageKeyConverter))]
        public String NormalImage
        {
            get { return this.normalImage; }
            set { this.normalImage = value; }
        }

        [Category("Split Button Images")]
        [Description("The Hover status image name in the ImageList.")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true), RefreshProperties(RefreshProperties.Repaint), TypeConverter(typeof(ImageKeyConverter))]
        public String HoverImage
        {
            get { return this.hoverImage; }
            set { this.hoverImage = value; }
        }

        [Category("Split Button Images")]
        [Description("The Clicked status image name in the ImageList.")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true), RefreshProperties(RefreshProperties.Repaint), TypeConverter(typeof(ImageKeyConverter))]
        public String ClickedImage
        {
            get { return this.clickedImage; }
            set { this.clickedImage = value; }
        }

        [Category("Split Button Images")]
        [Description("The Disabled status image name in the ImageList.")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true), RefreshProperties(RefreshProperties.Repaint), TypeConverter(typeof(ImageKeyConverter))]
        public String DisabledImage
        {
            get { return this.disabledImage; }
            set { this.disabledImage = value; }
        }

        [Category("Split Button Images")]
        [Description("The Focused status image name in the ImageList.")]
        [DefaultValue("")]
        [Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true), RefreshProperties(RefreshProperties.Repaint), TypeConverter(typeof(ImageKeyConverter))]
        public String FocusedImage
        {
            get { return this.focusedImage; }
            set { this.focusedImage = value; }
        }

        #endregion
        #region Construction

        public SplitButtonOld()
        {
            this.ImageAlign = ContentAlignment.MiddleRight;
            this.TextAlign = ContentAlignment.MiddleLeft;

            InitializeComponent();
        }


        #endregion
        #region Methods

        protected override void OnCreateControl()
        {
            InitDefaultSplitImages();

            if (this.ImageList == null)
                this.ImageList = this.defaultSplitImages;

            if (Enabled)
                SetSplit(this.normalImage);
            else
                SetSplit(this.disabledImage);
            
            base.OnCreateControl();
        }

        private void InitDefaultSplitImages()
        {
            InitDefaultSplitImages(false);
        }

        private void InitDefaultSplitImages(bool refresh)
        {
            if (String.IsNullOrEmpty(this.normalImage))
                this.normalImage = "Normal";

            if (String.IsNullOrEmpty(this.hoverImage))
                this.hoverImage = "Hover";

            if (String.IsNullOrEmpty(this.clickedImage))
                this.clickedImage = "Clicked";

            if (String.IsNullOrEmpty(this.disabledImage))
                this.disabledImage = "Disabled";

            if (String.IsNullOrEmpty(this.focusedImage))
                this.focusedImage = "Focused";

            if (this.defaultSplitImages == null)
                this.defaultSplitImages = new ImageList();

            if (this.defaultSplitImages.Images.Count == 0 || refresh)
            {
                Color markColor = Color.FromArgb(255, 80, 80, 80);

                if (this.defaultSplitImages.Images.Count > 0)
                    this.defaultSplitImages.Images.Clear();

                try
                {
                    int w = 0;
                    int h = 0;

                    if (!this.calculateSplitRect && this.splitWidth > 0)
                        w = this.splitWidth + 2;
                    else
                        w = 30;

                    if (!CalculateSplitRect && SplitHeight > 0)
                        h = SplitHeight;
                    else
                        h = Height;

                    h -= 8;

                    this.defaultSplitImages.ImageSize = new Size(w, h);

                    int mw = w / 2;
                    mw += (mw % 2);
                    int mh = h / 2;

                    Pen fPen = new Pen(markColor, 1);
                    SolidBrush fBrush = new SolidBrush(markColor);

                    Bitmap imgN = new Bitmap(w, h);
                    Graphics g = Graphics.FromImage(imgN);

                    g.CompositingQuality = CompositingQuality.HighQuality;

                    g.DrawLine(SystemPens.ButtonShadow, new Point(1, 1), new Point(1, h - 2));
                    g.DrawLine(SystemPens.ButtonFace, new Point(2, 1), new Point(2, h));

                    g.FillPolygon(fBrush, new Point[] { new Point(mw - 3, mh - 1), 
                                                         new Point(mw + 6, mh - 1),
                                                         new Point(mw + 1, mh + 4) });

                    g.Dispose();

                    Bitmap imgH = new Bitmap(w, h);
                    g = Graphics.FromImage(imgH);

                    g.CompositingQuality = CompositingQuality.HighQuality;

                    g.DrawLine(SystemPens.ButtonShadow, new Point(1, 1), new Point(1, h - 2));
                    g.DrawLine(SystemPens.ButtonFace, new Point(2, 1), new Point(2, h));

                    g.FillPolygon(fBrush, new Point[] { new Point(mw - 3, mh - 2), 
                                                         new Point(mw + 4, mh - 2),
                                                         new Point(mw, mh + 2) });

                    g.Dispose();

                    Bitmap imgC = new Bitmap(w, h);
                    g = Graphics.FromImage(imgC);

                    g.CompositingQuality = CompositingQuality.HighQuality;

                    g.DrawLine(SystemPens.ButtonShadow, new Point(1, 1), new Point(1, h - 2));
                    g.DrawLine(SystemPens.ButtonFace, new Point(2, 1), new Point(2, h));

                    //g.FillPolygon(fBrush, new Point[] { new Point(mw - 2, mh - 1), 
                    //                                     new Point(mw + 3, mh - 1),
                    //                                     new Point(mw, mh + 2) });
                    g.FillPolygon(fBrush, new Point[] { new Point(mw - 4, mh), 
                                                         new Point(mw + 5, mh),
                                                         new Point(mw, mh + 5) });
                    g.Dispose();

                    Bitmap imgD = new Bitmap(w, h);
                    g = Graphics.FromImage(imgD);

                    g.CompositingQuality = CompositingQuality.HighQuality;

                    g.DrawLine(SystemPens.GrayText, new Point(1, 1), new Point(1, h - 2));

                    g.FillPolygon(new SolidBrush(SystemColors.GrayText), new Point[] { new Point(mw - 2, mh - 1), 
                                                         new Point(mw + 3, mh - 1),
                                                         new Point(mw, mh + 2) });

                    g.Dispose();

                    Bitmap imgF = new Bitmap(w, h);
                    g = Graphics.FromImage(imgF);

                    g.CompositingQuality = CompositingQuality.HighQuality;

                    g.DrawLine(SystemPens.ButtonShadow, new Point(1, 1), new Point(1, h - 2));
                    g.DrawLine(SystemPens.ButtonFace, new Point(2, 1), new Point(2, h));

                    g.FillPolygon(fBrush, new Point[] { new Point(mw - 2, mh - 1), 
                                                         new Point(mw + 3, mh - 1),
                                                         new Point(mw, mh + 2) });

                    g.Dispose();

                    fPen.Dispose();
                    fBrush.Dispose();

                    this.defaultSplitImages.Images.Add(this.normalImage, imgN);
                    this.defaultSplitImages.Images.Add(this.hoverImage, imgH);
                    this.defaultSplitImages.Images.Add(this.clickedImage, imgC);
                    this.defaultSplitImages.Images.Add(this.disabledImage, imgD);
                    this.defaultSplitImages.Images.Add(this.focusedImage, imgF);
                }
                catch { }
            }
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            if (this.alwaysDropDown || this.alwaysHoverChange || MouseInSplit())
            {
                if (Enabled)
                    SetSplit(this.hoverImage);
            }
            else
            {
                if (Enabled)
                    SetSplit(this.normalImage);
            }

            base.OnMouseMove(mevent);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (Enabled)
                SetSplit(this.normalImage);

            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (this.alwaysDropDown || MouseInSplit())
            {
                if (Enabled)
                {
                    SetSplit(this.clickedImage);

                    if (this.ContextMenuStrip != null && this.ContextMenuStrip.Items.Count > 0)
                        this.ContextMenuStrip.Show(this, new Point(0, Height));
                }
            }
            else
            {
                if (Enabled)
                    SetSplit(this.normalImage);
            }

            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (this.alwaysDropDown || this.alwaysHoverChange || MouseInSplit())
            {
                if (Enabled)
                    SetSplit(this.hoverImage);
            }
            else
            {
                if (Enabled)
                    SetSplit(this.normalImage);
            }

            base.OnMouseUp(mevent);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (!Enabled)
                SetSplit(this.disabledImage);
            else
            {
                if (MouseInSplit())
                    SetSplit(this.hoverImage);
                else
                    SetSplit(this.normalImage);
            }

            base.OnEnabledChanged(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (Enabled)
                SetSplit(this.focusedImage);

            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (Enabled)
                SetSplit(this.normalImage);

            base.OnLostFocus(e);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (!MouseInSplit() && !this.alwaysDropDown)
            {
                if (ButtonClick != null)
                    ButtonClick(this, e);
            }
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            if (this.doubleClickedEnabled)
            {
                base.OnDoubleClick(e);

                if (!MouseInSplit() && !this.alwaysDropDown)
                {
                    if (ButtonClick != null)
                        ButtonDoubleClick(this, e);
                }
            }
        }

        private void SetSplit(String imageName)
        {
            if (imageName != null && ImageList != null && ImageList.Images.ContainsKey(imageName))
                this.ImageKey = imageName;
        }

        public bool MouseInSplit()
        {
            return PointInSplit(PointToClient(MousePosition));
        }

        public bool PointInSplit(Point pt)
        {
            Rectangle splitRect = GetImageRect(this.normalImage);

            if (!this.calculateSplitRect)
            {
                splitRect.Width = this.splitWidth;
                splitRect.Height = this.splitHeight;
            }

            return splitRect.Contains(pt);
        }

        public Rectangle GetImageRect(String imageKey)
        {
                Image currImg = GetImage(imageKey);

                if (currImg != null)
                {
                    int x = 0,
                        y = 0,
                        w = currImg.Width+1,
                        h = currImg.Height+1;

                    if (w > this.Width)
                        w = this.Width;

                    if (h > this.Width)
                        h = this.Width;

                    switch (ImageAlign)
                    {
                        case ContentAlignment.TopLeft:
                            {
                                x = 0;
                                y = 0;

                                break;
                            }
                        case ContentAlignment.TopCenter:
                            {
                                x = (this.Width - w) / 2;
                                y = 0;

                                if ((this.Width - w) % 2 > 0)
                                {
                                    x += 1;
                                }

                                break;
                            }
                        case ContentAlignment.TopRight:
                            {
                                x = this.Width - w;
                                y = 0;

                                break;
                            }
                        case ContentAlignment.MiddleLeft:
                            {
                                x = 0;
                                y = (this.Height - h) / 2;

                                if ((this.Height - h) % 2 > 0)
                                {
                                    y += 1;
                                }

                                break;
                            }
                        case ContentAlignment.MiddleCenter:
                            {
                                x = (this.Width - w) / 2;
                                y = (this.Height - h) / 2;

                                if ((this.Width - w) % 2 > 0)
                                {
                                    x += 1;
                                }
                                if ((this.Height - h) % 2 > 0)
                                {
                                    y += 1;
                                }

                                break;
                            }
                        case ContentAlignment.MiddleRight:
                            {
                                x = this.Width - w;
                                y = (this.Height - h) / 2;

                                if ((this.Height - h) % 2 > 0)
                                {
                                    y += 1;
                                }

                                break;
                            }
                        case ContentAlignment.BottomLeft:
                            {
                                x = 0;
                                y = this.Height - h;

                                if ((this.Height - h) % 2 > 0)
                                {
                                    y += 1;
                                }

                                break;
                            }
                        case ContentAlignment.BottomCenter:
                            {
                                x = (this.Width - w) / 2;
                                y = this.Height - h;

                                if ((this.Width - w) % 2 > 0)
                                {
                                    x += 1;
                                }

                                break;
                            }
                        case ContentAlignment.BottomRight:
                            {
                                x = this.Width - w;
                                y = this.Height - h;

                                break;
                            }
                    }

                    if (this.fillSplitHeight && h < this.Height)
                        h = this.Height;

                    if (x > 0)
                        x -= 1;
                    if (y > 0)
                        y -= 1;

                    return new Rectangle(x, y, w, h);
                }

            return Rectangle.Empty;
        }

        private Image GetImage(String imageName)
        {
            if (this.ImageList != null && this.ImageList.Images.ContainsKey(imageName))
                return this.ImageList.Images[imageName];

            return null;
        }

        #endregion
    }
}
