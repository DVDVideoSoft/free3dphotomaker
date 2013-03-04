using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

using DVDVideoSoft.Utils;

namespace DVDVideoSoft.Controls
{
    //public class ValueChangedEventArgs
    /// <summary>
    /// Based on Greg Ellis's article (http://www.codeproject.com/KB/miscctrl/customscrollbar.aspx)
    /// </summary>
    [Designer(typeof(ScrollbarControlDesigner))]
    public sealed class Scroller : UserControl, ILoggable
    {
        #region Events

        public event EventHandler ValueChanged;
        public event EventHandler<EventArgs<WindowsRelatedTypes.Direction>> ScrollOne;

        #endregion

        #region Private members

        private static ILogWriter Log;

        private enum ScrollerArea
        {
            None,
            UpArrow,
            UpChannel,
            Thumb,
            DownChannel,
            DownArrow,
        }

        private Brush brush = null;
        private Color channelColor = Color.FromArgb(238, 238, 238);

        private float value = 0;
        private float largeChange = 20;
        private float smallChange = 1;
        private float maximum = 5;
        private int frameSize = 5;

        private int clickPointY;

        private int thumbTopY = 0;

        private bool thumbDown = false;

        private float lastValue = -1;

        private int thumbHeight;

        #endregion

        #region Images

        private Image upArrowImage = null;
        private Image downArrowImage = null;

        private Image thumbTopImage = null;
        private Image thumbTopSpanImage = null;
        private Image thumbBottomImage = null;
        private Image thumbBottomSpanImage = null;
        private Image thumbMiddleImage = null;

        private Image GetThumbTop()
        {
            if (this.thumbTopImage == null)
                this.thumbTopImage = Properties.Resources.ThumbTop;
            return this.thumbTopImage;
        }

        private Image GetThumbBottom()
        {
            if (this.thumbBottomImage == null)
                this.thumbBottomImage = Properties.Resources.ThumbBottom;
            return this.thumbBottomImage;
        }

        private Image GetTopSpan()
        {
            if (this.thumbTopSpanImage == null)
                this.thumbTopSpanImage = Properties.Resources.ThumbSpanTop;
            return this.thumbTopSpanImage;
        }

        private Image GetBottomSpan()
        {
            if (this.thumbBottomSpanImage == null)
                this.thumbBottomSpanImage = Properties.Resources.ThumbSpanBottom;
            return this.thumbBottomSpanImage;
        }

        private Image GetThumbMiddle()
        {
            if (this.thumbMiddleImage == null)
                this.thumbMiddleImage = Properties.Resources.ThumbMiddle;
            return this.thumbMiddleImage;
        }

        private Image GetUpArrow()
        {
            if (this.upArrowImage == null)
                this.upArrowImage = Properties.Resources.ScrollUp;
            return this.upArrowImage;
        }

        private Image GetDownArrow()
        {
            if (this.downArrowImage == null)
                this.downArrowImage = Properties.Resources.ScrollDown;
            return this.downArrowImage;
        }

        #endregion

        #region Initialization

        public Scroller()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            this.brush = new SolidBrush(this.channelColor);

            this.Width = GetUpArrow().Width;
            base.MinimumSize = new Size(GetUpArrow().Width, GetUpArrow().Height + GetDownArrow().Height + this.ThumbHeight);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Scroller
            // 
            this.Name = "Scroller";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(Scroller_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(Scroller_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(Scroller_MouseUp);
            this.ResumeLayout(false);
        }

        public void SetLogger(ILogWriter log)
        {
            Log = log;
        }

        #endregion

        #region Private methods

        private int GetTrackHeight()
        {
            int n = this.Height - (GetUpArrow().Height + GetDownArrow().Height);
            return this.Height - (GetUpArrow().Height + GetDownArrow().Height);
        }

        public void SetThumbHeight(float value)
        {
            this.ThumbHeight = (int)(value * GetTrackHeight());
        }

        private int ThumbHeight
        {
            get
            {
                return this.thumbHeight;
            }

            set
            {
                this.thumbHeight = value;

                int trackHeight = GetTrackHeight();
                if (thumbHeight > trackHeight)
                    thumbHeight = trackHeight;
                if (thumbHeight < 56)
                    thumbHeight = 56;
            }
        }

        private int ThumbTopY
        {
            get
            {
                int th = GetTrackHeight();
                //float mid = 
                int pixelRange = (GetTrackHeight() - this.ThumbHeight);

                if (pixelRange > 0 && this.thumbTopY > pixelRange)
                    this.thumbTopY = pixelRange;
                return this.thumbTopY;
            }
        }

        private ScrollerArea GetClickArea(Point pt)
        {
            int thumbBottom = GetUpArrow().Height + this.ThumbTopY + this.ThumbHeight;
            Rectangle upArrowRect = new Rectangle(new Point(1, 0), new Size(GetUpArrow().Width, GetUpArrow().Height));
            Rectangle downArrowRect = new Rectangle(new Point(1, GetUpArrow().Height + GetTrackHeight()), new Size(GetUpArrow().Width, GetUpArrow().Height));
            Rectangle upChannelRect = new Rectangle(new Point(1, GetUpArrow().Height), new Size(GetUpArrow().Width, this.ThumbTopY));
            Rectangle downChannelRect = new Rectangle(new Point(1, thumbBottom), new Size(GetUpArrow().Width, this.Height - thumbBottom));
            Rectangle thumbrect = new Rectangle(new Point(1, GetUpArrow().Height + this.ThumbTopY), new Size(GetThumbMiddle().Width, this.ThumbHeight));

            if (thumbrect.Contains(pt))
                return ScrollerArea.Thumb;
            else if (upArrowRect.Contains(pt))
                return ScrollerArea.UpArrow;
            else if (downArrowRect.Contains(pt))
                return ScrollerArea.DownArrow;
            else if (upChannelRect.Contains(pt))
                return ScrollerArea.UpChannel;
            else if (downChannelRect.Contains(pt))
                return ScrollerArea.DownChannel;

            return ScrollerArea.None;
        }
        private void MoveThumb(int y)
        {
            int pixelRange = GetTrackHeight() - this.ThumbHeight;

            if (pixelRange <= 0)
                return;

            if (this.thumbDown) {
                int newThumbTop = y - GetUpArrow().Height - this.clickPointY;

                if (newThumbTop > pixelRange)
                    this.thumbTopY = pixelRange;
                else
                    this.thumbTopY = newThumbTop;

                if (this.thumbTopY < 0)
                    this.thumbTopY = 0;

                //figure out value
                float fPerc = (float)this.thumbTopY / (float)pixelRange * (float)Maximum;
                this.value = ((float)this.thumbTopY / (float)pixelRange * (float)Maximum);

                Invalidate();
                Application.DoEvents();
                if (this.value != this.lastValue) {
                    this.lastValue = this.value;

                    if (ValueChanged != null)
                        ValueChanged(this, new EventArgs());
                }
                Application.DoEvents();
            }
        }

        private void OnScrollParametersChanged()
        {
            int pixelRange = GetTrackHeight() - this.ThumbHeight;
            float f = ((float)pixelRange / (this.maximum)) * this.value;
            this.thumbTopY = (int)f;
            if (this.thumbTopY < 0)
                this.thumbTopY = 0;
            if (this.thumbTopY > GetTrackHeight() - this.ThumbHeight / 2)
                this.thumbTopY = GetTrackHeight() - this.ThumbHeight / 2;

            this.largeChange = this.maximum / 5;
        }

        #endregion

        #region Overrides

        protected override void OnPaint(PaintEventArgs e)
        {
            Image topThumb = GetThumbTop();
            Image topSpan = GetTopSpan();
            Image bottomThumb = GetThumbBottom();
            Image bottomSpan = GetBottomSpan();
            Image middleThumb = GetThumbMiddle();
            Image upArrow = GetUpArrow();
            Image downArrow = GetDownArrow();

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            if (upArrow != null)
                e.Graphics.DrawImage(upArrow, new Rectangle(new Point(0, 0), new Size(this.Width, GetUpArrow().Height)));

            //draw channel
            e.Graphics.FillRectangle(this.brush, new Rectangle(0, GetUpArrow().Height, this.Width, (this.Height - GetDownArrow().Height)));

            //draw thumb
            int thumbHeight = this.ThumbHeight;

            float spanHeightF = ((float)thumbHeight - (middleThumb.Height + topThumb.Height + bottomThumb.Height)) / 2.0f;
            int spanHeight = (int)spanHeightF;

            int nTop = this.ThumbTopY;
            nTop += GetUpArrow().Height;

            if (this.Enabled) {
                //draw top
                e.Graphics.DrawImage(topThumb, new Rectangle(0, nTop, this.Width - 1, topThumb.Height));
                nTop += topThumb.Height;

                //draw top span
                Rectangle rect = new Rectangle(0, nTop, this.Width - 1, spanHeight);
                e.Graphics.DrawImage(topSpan, 0.0f, (float)nTop, (float)this.Width - 1.0f, (float)spanHeightF * 2);
                nTop += spanHeight;

                //draw middle
                e.Graphics.DrawImage(middleThumb, new Rectangle(0, nTop, this.Width - 1, middleThumb.Height));
                nTop += middleThumb.Height;

                //draw bottom span
                e.Graphics.DrawImage(bottomSpan, new Rectangle(0, nTop, this.Width - 1, spanHeight));
                //e.Graphics.FillRectangle(Brushes.Aqua, new Rectangle(0, nTop, this.Width - 1, partialSpanHeight));
                e.Graphics.DrawImage(bottomSpan, 0.0f, (float)nTop, (float)this.Width - 1.0f, (float)spanHeightF * 2);
                nTop += spanHeight;
                // and bottom thumb
                e.Graphics.DrawImage(bottomThumb, new Rectangle(0, nTop, this.Width - 1, bottomThumb.Height));
                nTop += bottomThumb.Height;
            }
            //e.Graphics.DrawString(this.value.ToString(), new Font(this.Font.FontFamily, 6.0f), Brushes.Black, 0, 20);
            //draw down arrow
            if (downArrow != null)
                e.Graphics.DrawImage(downArrow, new Rectangle(new Point(0, (this.Height - GetDownArrow().Height)), new Size(this.Width, GetDownArrow().Height)));
        }

        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set
            {
                base.AutoSize = value;
                if (base.AutoSize)
                    this.Width = this.upArrowImage.Width;
            }
        }

        #endregion

        #region Properties

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("LargeChange")]
        public float LargeChange
        {
            get { return this.largeChange; }
            set
            {
                this.largeChange = value;
                Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("SmallChange")]
        public float SmallChange
        {
            get { return this.smallChange; }
            set
            {
                this.smallChange = value;
                Invalidate();
            }
        }

        public int FrameSize
        {
            get { return this.frameSize; }
            set
            {
                this.frameSize = value;
                OnScrollParametersChanged();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("Maximum")]
        public float Maximum
        {
            get { return this.maximum; }
            set
            {
                this.maximum = value;
                OnScrollParametersChanged();
                Invalidate();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Behavior"), Description("Value")]
        public float Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                OnScrollParametersChanged();

                Invalidate();

                if (this.value != this.lastValue) {
                    this.lastValue = value;

                    if (ValueChanged != null)
                        ValueChanged(this, new EventArgs());
                }
                this.lastValue = value;
            }
        }

        private void ScrollByOne(bool down)
        {
            WindowsRelatedTypes.Direction offset = down ? WindowsRelatedTypes.Direction.Down : WindowsRelatedTypes.Direction.Up;

            if (ScrollOne != null)
                ScrollOne(this, new EventArgs<WindowsRelatedTypes.Direction>(offset));

            //this.value shall be change from the outside
            this.lastValue = value;
            OnScrollParametersChanged();

            Invalidate();
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Channel Color")]
        public Color ChannelColor
        {
            get { return this.channelColor; }
            set { this.channelColor = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Up Arrow Graphic")]
        public Image UpArrowImage
        {
            get { return this.upArrowImage; }
            set { this.upArrowImage = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Down Arrow Graphic")]
        public Image DownArrowImage
        {
            get { return this.downArrowImage; }
            set { this.downArrowImage = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Top thumb image")]
        public Image ThumbTopImage
        {
            get { return this.thumbTopImage; }
            set { this.thumbTopImage = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Top span image")]
        public Image ThumbTopSpanImage
        {
            get { return this.thumbTopSpanImage; }
            set { this.thumbTopSpanImage = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Bottom thumb image")]
        public Image ThumbBottomImage
        {
            get { return this.thumbBottomImage; }
            set { this.thumbBottomImage = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Bottom span image")]
        public Image ThumbBottomSpanImage
        {
            get { return this.thumbBottomSpanImage; }
            set { this.thumbBottomSpanImage = value; }
        }

        [EditorBrowsable(EditorBrowsableState.Always), Browsable(true), DefaultValue(false), Category("Skin"), Description("Middle thumb image")]
        public Image ThumbMiddleImage
        {
            get { return this.thumbMiddleImage; }
            set { this.thumbMiddleImage = value; }
        }

        #endregion

        #region Event handlers

        private void Scroller_MouseDown(object sender, MouseEventArgs e)
        {
            ScrollerArea clickArea = GetClickArea(new Point(e.X, e.Y));

            if (clickArea == ScrollerArea.Thumb) {
                this.clickPointY = e.Y - this.GetUpArrow().Height - this.ThumbTopY;
                this.thumbDown = true;
                return;
            }

            float offset = 0;
            switch (clickArea) {
                case ScrollerArea.UpArrow:
                    if (this.value != 0)
                        ScrollByOne(false);
                    break;
                case ScrollerArea.DownArrow:
                    if (this.value < this.maximum - 1)
                        ScrollByOne(true);
                    break;
                case ScrollerArea.UpChannel:
                    offset = this.largeChange * -1;
                    break;
                case ScrollerArea.DownChannel:
                    offset = this.largeChange;
                    break;
                case ScrollerArea.Thumb:
                    return;
            }

            if (this.value + offset < 0)
                this.Value = 0;
            else if (this.value + offset >= this.maximum)
                this.Value = this.maximum;
            else if (offset != 0)
                this.Value += offset;
        }

        private void Scroller_MouseUp(object sender, MouseEventArgs e)
        {
            this.thumbDown = false;
        }

        private void Scroller_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left) {
                if (this.thumbDown == true)
                    MoveThumb(e.Y);
            }
        }

        #endregion
    }

    internal class ScrollbarControlDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        public override SelectionRules SelectionRules
        {
            get
            {
                SelectionRules selectionRules = base.SelectionRules;
                PropertyDescriptor propDescriptor = TypeDescriptor.GetProperties(this.Component)["AutoSize"];
                if (propDescriptor != null) {
                    bool autoSize = (bool)propDescriptor.GetValue(this.Component);
                    if (autoSize)
                        selectionRules = SelectionRules.Visible | SelectionRules.Moveable | SelectionRules.BottomSizeable | SelectionRules.TopSizeable;
                    else
                        selectionRules = SelectionRules.Visible | SelectionRules.AllSizeable | SelectionRules.Moveable;
                }
                return selectionRules;
            }
        }
    }
}
