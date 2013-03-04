using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace DVDVideoSoft.Controls
{
    [DefaultEvent("Click")]
    public class HintPanel : /*UserControl*/Panel, DVDVideoSoft.Utils.IDvsControl
    {
        #region Designer

            private System.ComponentModel.Container components = null;

            /// <summary>
            /// Initialize the component with it's default settings.
            /// </summary>
            public HintPanel()
            {
                InitializeComponent();

                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                this.SetStyle(ControlStyles.DoubleBuffer, true);
                this.SetStyle(ControlStyles.ResizeRedraw, true);
                this.SetStyle(ControlStyles.Selectable, false);
                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                this.SetStyle(ControlStyles.UserPaint, true);
                this.BackColor = Color.FromArgb(249, 251, 218);
                //this.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
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
                    this.SuspendLayout();
                    // 
                    // HintPanel
                    // 
                    this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(251)))), ((int)(((byte)(218)))));
                    this.Name = "HintPanel";
                    this.Size = new System.Drawing.Size(100, 32);
                    this.Paint += new System.Windows.Forms.PaintEventHandler(this.HintPanel_Paint);
                    this.SizeChanged += new EventHandler(HintPanel_SizeChanged);

                    this.textLabel = new Label();
                    this.textLabel.Location = new Point(1, 1);
                    this.textLabel.Size = new Size(this.Width - 2, this.Height - 2);
                    this.textLabel.TextAlign = ContentAlignment.MiddleLeft;
                    this.Controls.Add(this.textLabel);

                    this.ResumeLayout(false);
                }

                void HintPanel_SizeChanged(object sender, EventArgs e)
                {
                    AdjustSize();
                }

            #endregion

        #endregion

        #region Private members

        //private bool normalDpiMode = true; //TODO:
        private Label textLabel;

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

            #region Text

            [Category("Text"),
             Description("Hint text."),
             Localizable(true),
             Browsable(true),
             System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)
            ]
            public override string Text
            {
                get { return this.textLabel.Text; }
                set
                {
                    this.textLabel.Text = value;
                    float f = this.textLabel.CreateGraphics().MeasureString(this.textLabel.Text, this.Font).Width;
                    int n = GetTextLeft() + (int)f;
                    int requiredW = GetTextLeft() + (int)GetGraphics().MeasureString(this.textLabel.Text, this.Font).Width;

                    this.Width = requiredW + this.Padding.Left;
                    AdjustSize();
                    Invalidate();
                }
            }

            public void AdjustSize()
            {
                int left = GetTextLeft();
                this.textLabel.Location = new Point(left, 1);
                this.textLabel.Size = new Size(this.Width - left - 1, this.Height - 2);
                //if (!this.normalDpiMode) //TODO:
            }

            private ContentAlignment textAlign = ContentAlignment.MiddleCenter;
            protected ContentAlignment TextAlign
            {
                get { return this.textAlign; }
                set
                {
                    this.textAlign = value;
                    Invalidate();
                }
            }

            [Category("Appearance"),
                DefaultValue(typeof(int), "0"),
                Description("Horizontal text offset in pixels.")]
            public int HorizontalTextOffset { get; set; }
    
            #endregion
        
            #region Appearance

                private Image image;

                [Category("Image"), DefaultValue(null), Description("The normal state image.")]
                public Image Image
                {
                    get { return this.image; }
                    set
                    {
                        this.image = value;
                        AdjustSize();
                        Refresh();
                    }
                }

            #endregion

        #endregion

        #region Drawing

        private Graphics graphics = null;

        Graphics GetGraphics()
            {
                if (this.graphics == null)
                    this.graphics = this.CreateGraphics();

                return this.graphics;
            }

        private void DrawBackground(Graphics g)
        {
            //g.FillRectangle(new SolidBrush(Color.FromArgb(255; 255; 223)), this.ClientRectangle);
            g.DrawRectangle(new Pen(Color.FromArgb(214, 215, 207)), new Rectangle(0, 0, this.Width - 1, this.Height - 1));
        }

        private int GetTextLeft()
        {
            int n = this.Padding.Left + this.HorizontalTextOffset + (this.Image != null ? this.Image.Width : 0);
            return this.Padding.Left + this.HorizontalTextOffset + (this.Image != null ? this.Image.Width : 0);
        }

        private void DrawText(Graphics g)
        {
        }

        private void DrawImage(Graphics g)
        {
            if (this.Image == null)
                return;
            g.DrawImage(this.image,
                        new Rectangle(this.Padding.Left, this.Padding.Top, this.Image.Width, this.Image.Height));
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

        private void HintPanel_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            DrawBackground(e.Graphics);
            DrawImage(e.Graphics);
            //DrawText(e.Graphics);
        }

        #endregion
    }
}
