using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DVDVideoSoft.Controls
{
    public partial class RangeSelector : UserControl
    {
       
        public enum SliderType
        {
            TrackBar,
            RangeTrackBar,
            RangeSelector
        };

        public delegate void PositionChangedDelegate();
        public event PositionChangedDelegate PositionChanged;

        public delegate void RangeFromChangedDelegate(out string Pos);
        public event RangeFromChangedDelegate RangeFromChanged;

        public delegate void RangeToChangedDelegate(out string Pos);
        public event RangeToChangedDelegate RangeToChanged;

        private SliderType type = SliderType.RangeTrackBar;

        
        private int minimum = 0;
        private int maximum = 100;

        private int rangeFrom=0;
        private int rangeTo=100;

        private int intersectionFrom=0;

        public int IntersectionFrom
        {
            get { return intersectionFrom; }
            set { if (value >= rangeFrom) { intersectionFrom = value; Invalidate(); } }
        }
        private int intersectionTo=0;

        public int IntersectionTo
        {
            get { return intersectionTo; }
            set { if (value <= rangeTo) { intersectionTo = value; Invalidate(); } }
        }

        private int position = 0;

        private bool showCurrentPosMarker = false;

        public bool ShowCurrentPosMarker
        {
            get { return showCurrentPosMarker; }
            set { showCurrentPosMarker = value; Invalidate(); }
        }

        private float tickSize = 1;

        private int thumbRadius = 7;
        private int areaHeight = 9;
        private int areaOffset = 0;
        private int markerSize = 5;

        public int MarkerSize
        {
            get { return markerSize; }
            set { markerSize = value; UpdateAll(); }
        }

        public int AreaOffset
        {
            get { return areaOffset; }
            set 
            { 
                areaOffset = value;
                UpdateAll();
            }
        }
        private Color areaBorderColor = Color.Gray;
        private Color thumbColor = Color.Gray;
        private Color rangeColor = Color.FromArgb(0, 148, 255);
        private Color areaInternalColor = Color.White;
        private Color intersectionColor = Color.Yellow;

        public Color IntersectionColor
        {
            get { return intersectionColor; }
            set { intersectionColor = value; Invalidate(); }
        }
        private bool showRangePos = true;

        public bool ShowRangePos
        {
            get { return showRangePos; }
            set { showRangePos = value; }
        }
        private string rangeToPosStr="";
        private string rangeFromPosStr="";
        private RectangleF rangeFromStrRect=new RectangleF();
        private RectangleF rangeToStrRect=new RectangleF();

        private NumericUpDown rangeEditFrom;
        private NumericUpDown rangeEditTo;

        private bool editable = true;

        public bool Editable
        {
            get { return editable; }
            set { editable = value; }
        }

        public Color RangeColor
        {
            get { return rangeColor; }
            set { rangeColor = value; Invalidate(); }
        }

        public Color ThumbColor
        {
            get { return thumbColor; }
            set { thumbColor = value; Invalidate(); }
        }

        public int RangeTo
        {
            get { return rangeTo; }
            set 
            { 
                rangeTo = value;

                OnRangeToChanged();
                Invalidate();
            }
        }

        public int RangeFrom
        {
            get { return rangeFrom; }
            set 
            { 
                rangeFrom = value;

                OnRangeFromChanged();
                Invalidate();
            }
        }

        public SliderType Type
        {
            get { return type; }
            set 
            {
                type = value;
                RecalcTickSize();
                OnRangeFromChanged();
                OnRangeToChanged();
                
                Invalidate();
            }
        }

        public int Minimum
        {
            get { return minimum; }
            set 
            { 
                minimum = value;
                rangeEditFrom.Minimum = minimum;
                rangeEditTo.Minimum = minimum;

                if (rangeFrom < minimum)
                    rangeFrom = minimum;
                if (position < minimum)
                    position = minimum;
                UpdateAll();
            }
        }

        public int Maximum
        {
            get { return maximum; }
            set 
            { 
                maximum = value;
                rangeEditFrom.Maximum = maximum;
                rangeEditTo.Maximum = maximum;
                if (rangeTo > maximum)
                    rangeTo = maximum;
                if (position > maximum)
                    position = maximum;
                UpdateAll();
            }
        }

        public int Position
        {
            get { return position; }
            set { if (value >= minimum && value <= maximum) position = value; Invalidate(); }
        }

        public int ThumbRadius
        {
            get { return thumbRadius; }
            set { thumbRadius = value; RecalcTickSize(); UpdateAll(); }
        }

        public Color AreaBorderColor
        {
            get { return areaBorderColor; }
            set { areaBorderColor = value; Invalidate(); }
        }

        public Color AreaInternalColor
        {
            get { return areaInternalColor; }
            set { areaInternalColor = value; Invalidate(); }
        }

        public int AreaHeight
        {
            get { return areaHeight; }
            set { if (value > 0) areaHeight = value; UpdateAll(); }
        }

        private bool leftMouseDown = false;
        private bool rangeFromSelected = false;
        private bool rangeToSelected = false;

        public RangeSelector()
        {
            InitializeComponent();
            rangeEditFrom = new NumericUpDown();
            rangeEditFrom.Parent = this;
            rangeEditFrom.Size = new Size(rangeEditFrom.Width / 2, rangeEditFrom.Height);
            rangeEditFrom.Hide();
            rangeEditFrom.KeyDown += new KeyEventHandler(rangeEditFrom_KeyDown);
            rangeEditFrom.Leave += new EventHandler(rangeEditFrom_Leave);

            rangeEditTo = new NumericUpDown();
            rangeEditTo.Parent = this;
            rangeEditTo.Size = new Size(rangeEditTo.Width / 2, rangeEditTo.Height);
            rangeEditTo.Hide();
            rangeEditTo.KeyDown += new KeyEventHandler(rangeEditTo_KeyDown);
            rangeEditTo.Leave += new EventHandler(rangeEditTo_Leave);
            
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            ResizeRedraw = true;

            RecalcTickSize();
            RecalcRangeRectangles();
        }

        void rangeEditTo_Leave(object sender, EventArgs e)
        {
            rangeEditTo_KeyDown(rangeEditTo, new KeyEventArgs(Keys.Enter));
        }

        void rangeEditFrom_Leave(object sender, EventArgs e)
        {
            rangeEditFrom_KeyDown(rangeEditFrom, new KeyEventArgs(Keys.Enter));
        }

        void rangeEditTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RangeTo = (int)rangeEditTo.Value;
                rangeEditTo.Hide();
                Invalidate();
            }

            if (e.KeyCode == Keys.Escape)
            {
                rangeEditTo.Hide();
                Invalidate();
            }
        }

        void rangeEditFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RangeFrom = (int)rangeEditFrom.Value;
                rangeEditFrom.Hide();
                Invalidate();
            }

            if (e.KeyCode == Keys.Escape)
            {
                rangeEditFrom.Hide();
                Invalidate();
            }
        }

        private void OnRangeFromChanged()
        {
            string res="";
            if (RangeFromChanged != null)
                RangeFromChanged(out res);

            if (editable)
                rangeFromPosStr = rangeFrom.ToString();
            else
                rangeFromPosStr = res;

            RecalcRangeRectangles();
        }

        private void OnRangeToChanged()
        {
            string res = "";
            if (RangeToChanged != null)
                RangeToChanged(out res);

            if (editable)
                rangeToPosStr = rangeTo.ToString();
            else
                rangeToPosStr = res;

            RecalcRangeRectangles();
        }

        private void OnPositionChanged()
        {
            if (PositionChanged != null)
                PositionChanged();
        }

        private void RecalcTickSize()
        {
            if(type!=SliderType.RangeSelector)
                tickSize = (float)(Width - thumbRadius * 2-areaOffset*2);
            else
                tickSize = (float)(Width - markerSize * 2 - areaOffset * 2);

            if(Maximum-Minimum>0)
                tickSize/= (float)(Maximum - Minimum);
        }

        private int CalcPosition(Point loc)
        {
            int pos=0;
            if(type!=SliderType.RangeSelector)
                pos=(int)Math.Round((loc.X - thumbRadius - areaOffset) / tickSize);
            else
                pos = (int)Math.Round((loc.X - markerSize - areaOffset) / tickSize);

            return pos;
        }

        public PointF GetThumbPosition()
        {
            PointF res=new PointF();

            float x = areaOffset + position * tickSize;


            switch (type)
            {
                case SliderType.RangeSelector:
                    res = new PointF(x+markerSize, Height - (float)areaHeight / 2-3);
                    break;
                case SliderType.RangeTrackBar:
                    res = new PointF(x + thumbRadius >= Width-thumbRadius ? Width - thumbRadius - 2 : x + thumbRadius, Height - ((float)areaHeight / 2 > thumbRadius ? (float)areaHeight / 2 : thumbRadius) - 3);
                    break;
                case SliderType.TrackBar:
                    res = new PointF(x + thumbRadius >= Width-thumbRadius ? Width - thumbRadius - 2 : x + thumbRadius, Height - ((float)areaHeight / 2 > thumbRadius ? (float)areaHeight / 2 : thumbRadius) - 3);
                    break;
            }

            return res;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            RecalcTickSize();
            RecalcRangeRectangles();

            if(rangeEditFrom.Visible)
                rangeEditFrom.Location = new Point((int)rangeFromStrRect.Left, (int)rangeFromStrRect.Bottom - rangeEditFrom.Height - 2);
            if(rangeEditTo.Visible)
                rangeEditTo.Location = new Point((int)rangeToStrRect.Left + rangeEditTo.Width > Width ? Width - rangeEditTo.Width : (int)rangeToStrRect.Left, (int)rangeToStrRect.Bottom - rangeEditTo.Height - 2);
        }

        public PointF GetRangePositions()
        {
            if(type==SliderType.RangeSelector)
                return new PointF(areaOffset + markerSize + rangeFrom * tickSize, areaOffset + markerSize + rangeTo * tickSize);
            if(type==SliderType.RangeTrackBar)
                return new PointF(areaOffset + thumbRadius + rangeFrom * tickSize, areaOffset + rangeTo * tickSize + thumbRadius);

            return new PointF();
        }

        public PointF GetIntersectionPositions()
        {
            return new PointF(areaOffset + thumbRadius + intersectionFrom * tickSize, areaOffset + intersectionTo * tickSize + thumbRadius);
        }

        public RectangleF GetAreaRect()
        {
            PointF tp = GetThumbPosition();
            if(type!=SliderType.RangeSelector)
                return new RectangleF(new PointF(thumbRadius + areaOffset, tp.Y - (float)areaHeight / 2), new SizeF(Width - thumbRadius * 2 /*- 2*/- areaOffset * 2, areaHeight));
            else
                return new RectangleF(new PointF(areaOffset, tp.Y - (float)areaHeight / 2), new SizeF(Width - areaOffset * 2, areaHeight));
        }

        public void UpdateRangeHelpers(string From, string To)
        {
            if (!editable)
            {
                rangeFromPosStr = From;
                rangeToPosStr = To;
            }
        }

        private void UpdateAll()
        {
            RecalcTickSize();
            RecalcRangeRectangles();
            Invalidate();
        }

        void DrawMarkerLeft(Graphics gr)
        {
            PointF tp = GetThumbPosition();
            PointF rangePos = GetRangePositions();
            int radius = 4;
            //if (rangePos.X + radius > rangePos.Y) return;

            float diameter = radius * 2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF baseRect = new RectangleF(new PointF(rangePos.X-markerSize, tp.Y - (float)areaHeight / 2), new SizeF(markerSize-radius, areaHeight - 1));
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            // top left arc 
            path.AddArc(arc, 180, 90);

            // bottom left arc
            arc.X = baseRect.Left;
            arc.Y = baseRect.Bottom-diameter;
            path.AddArc(arc, 90, 90);

            baseRect.Location = new PointF(baseRect.Left + radius, baseRect.Top);
            path.AddRectangle(baseRect);

            path.CloseFigure();

            System.Drawing.Drawing2D.LinearGradientBrush lBrM = new System.Drawing.Drawing2D.LinearGradientBrush(baseRect, ControlPaint.LightLight(rangeColor), ControlPaint.DarkDark(rangeColor), System.Drawing.Drawing2D.LinearGradientMode.Vertical);

            gr.FillPath(lBrM/*new SolidBrush(ControlPaint.Dark(rangeColor))*/, path);

            lBrM.Dispose();
            path.Dispose();
        }

        void DrawMarkerRight(Graphics gr)
        {
            PointF tp = GetThumbPosition();
            PointF rangePos = GetRangePositions();
            int radius = 4;
            //if (rangePos.Y - radius < rangePos.X) return;

            float diameter = radius * 2.0F;
            SizeF sizeF = new SizeF(diameter, diameter);
            RectangleF baseRect = new RectangleF(new PointF(rangePos.Y, tp.Y - (float)areaHeight / 2), new SizeF(markerSize-radius, areaHeight - 1));
            RectangleF arc = new RectangleF(baseRect.Location, sizeF);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();

            // top left arc 
            // top right arc 
            arc.X = baseRect.Right-radius;
            path.AddArc(arc, 270, 90);

            // bottom right arc 
            arc.Y = baseRect.Bottom-diameter;
            path.AddArc(arc, 0, 90);
            //baseRect.Location = new PointF(rangePos.Y, baseRect.Top);
            path.AddRectangle(baseRect);

            path.CloseFigure();

            System.Drawing.Drawing2D.LinearGradientBrush lBrM = new System.Drawing.Drawing2D.LinearGradientBrush(baseRect, ControlPaint.LightLight(rangeColor), ControlPaint.DarkDark(rangeColor), System.Drawing.Drawing2D.LinearGradientMode.Vertical);

            gr.FillPath(lBrM/*new SolidBrush(ControlPaint.Dark(rangeColor))*/, path);

            lBrM.Dispose();
            path.Dispose();
        }

        private void DrawRange(Graphics gr)
        {
            PointF tp = GetThumbPosition();
            PointF rangePos = GetRangePositions();

            System.Drawing.Drawing2D.LinearGradientBrush lBrIn = new System.Drawing.Drawing2D.LinearGradientBrush(new RectangleF(new PointF(rangePos.X + 4, tp.Y - (float)areaHeight / 2 + 1), new SizeF(rangePos.Y - rangePos.X - 8, areaHeight - 2)), ControlPaint.LightLight(rangeColor), rangeColor, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            System.Drawing.Drawing2D.GraphicsPath rangeOuter = DVDVideoSoft.Utils.GraphicsUtils.GetRoundedRect(new RectangleF(new PointF(rangePos.X, tp.Y - (float)areaHeight / 2 + 1), new SizeF(rangePos.Y - rangePos.X /*- 2*/, areaHeight - 2)), 4);

            if(type==SliderType.RangeTrackBar)
                gr.FillPath(lBrIn, rangeOuter);
            else
                if(type==SliderType.RangeSelector)
                    gr.FillRectangle(lBrIn, new RectangleF(new PointF(rangePos.X, tp.Y - (float)areaHeight / 2 + 1), new SizeF(rangePos.Y - rangePos.X /*- 2*/, areaHeight - 2)));

            lBrIn.Dispose();
            rangeOuter.Dispose();

            if (intersectionFrom < intersectionTo)
            {
                PointF intersectPos = GetIntersectionPositions();
                System.Drawing.Drawing2D.LinearGradientBrush lBrI = new System.Drawing.Drawing2D.LinearGradientBrush(new RectangleF(new PointF(intersectPos.X, tp.Y - (float)areaHeight / 2 + 1), new SizeF(intersectPos.Y - intersectPos.X /*- 2*/, areaHeight - 2)), ControlPaint.LightLight(intersectionColor), intersectionColor, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                System.Drawing.Drawing2D.GraphicsPath intersection = DVDVideoSoft.Utils.GraphicsUtils.GetRoundedRect(new RectangleF(new PointF(intersectPos.X, tp.Y - (float)areaHeight / 2 + 1), new SizeF(intersectPos.Y - intersectPos.X /*- 2*/, areaHeight - 2)), 4);
                gr.FillPath(lBrI/*new SolidBrush(intersectionColor)*/, intersection);
                intersection.Dispose();
                lBrI.Dispose();
            }

            if (type == SliderType.RangeSelector)
            {
                DrawMarkerLeft(gr);
                DrawMarkerRight(gr);
            }
        }

        private void DrawThumb(Graphics gr)
        {
            PointF tp = GetThumbPosition();
            RectangleF thumbRect=new RectangleF( tp.X - thumbRadius, tp.Y - thumbRadius, thumbRadius * 2, thumbRadius * 2);
            System.Drawing.Drawing2D.LinearGradientBrush lBr = new System.Drawing.Drawing2D.LinearGradientBrush(thumbRect,ControlPaint.LightLight( ControlPaint.LightLight(thumbColor)), /*ControlPaint.Dark(*/thumbColor/*)*/, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            gr.FillEllipse(lBr,thumbRect);
            gr.DrawEllipse(new Pen(thumbColor), thumbRect);
            lBr.Dispose();
        }

        int refSize = 4;
        int refOffsetL = 0;
        int refOffsetR = 0;

        private void RecalcRangeRectangles()
        {
            PointF tp = GetThumbPosition();
            PointF rangePos = GetRangePositions();
            
            Graphics gr = CreateGraphics();
            if (rangeFromPosStr.Length > 0)
            {
                SizeF strS = gr.MeasureString(rangeFromPosStr, Font);
                if (rangePos.X - strS.Width - refSize + refOffsetR > 0)
                {
                    rangeFromStrRect = new RectangleF(new PointF(rangePos.X - strS.Width - refSize + refOffsetR, GetThumbPosition().Y - (float)areaHeight / 2 - strS.Height - refSize), strS);
                }
                else
                {
                    rangeFromStrRect = new RectangleF(new PointF(rangePos.X + refSize + refOffsetL, GetThumbPosition().Y - (float)areaHeight / 2 - strS.Height - refSize), strS);
                }
            }

            if (rangeToPosStr.Length > 0)
            {

                SizeF strS = gr.MeasureString(rangeToPosStr, Font);

                if (rangePos.Y + strS.Width + refSize + refOffsetL < Width)
                {
                    rangeToStrRect = new RectangleF(new PointF(rangePos.Y + refSize + refOffsetL, tp.Y - (float)areaHeight / 2 - strS.Height - refSize), strS);
                }
                else
                {
                    rangeToStrRect = new RectangleF(new PointF(rangePos.Y - strS.Width - refSize + refOffsetR, tp.Y - (float)areaHeight / 2 - strS.Height - refSize), strS);
                }
            }

            if (rangeFromStrRect.IntersectsWith(rangeToStrRect))
            {
                if (rangeToStrRect.Left - rangeFromStrRect.Width-2 > areaOffset + thumbRadius + refSize + refOffsetL)
                {
                    rangeFromStrRect.Location = new PointF(rangeToStrRect.Left - rangeFromStrRect.Width-2, rangeFromStrRect.Top);
                }
                else
                {
                    rangeToStrRect.Location = new PointF(rangeFromStrRect.Right+2, rangeToStrRect.Top);
                }

                if (rangeFromStrRect.Right + rangeToStrRect.Width+2 < Width - areaOffset - thumbRadius - refSize - refOffsetR)
                {
                    rangeToStrRect.Location = new PointF(rangeFromStrRect.Right+2, rangeToStrRect.Top);
                }
                else
                {
                    rangeFromStrRect.Location = new PointF(rangeToStrRect.Left-rangeFromStrRect.Width-2, rangeFromStrRect.Top);
                }
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            /*if (Focused)
                ControlPaint.DrawFocusRectangle(e.Graphics, ClientRectangle);*/

            if (Width == 0 || Height == 0) return;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            PointF tp = GetThumbPosition();

            System.Drawing.Drawing2D.LinearGradientBrush lBr = new System.Drawing.Drawing2D.LinearGradientBrush(new RectangleF(new PointF(thumbRadius + areaOffset, tp.Y - (float)areaHeight / 2), new SizeF(Width - thumbRadius * 2 /*- 2*/- areaOffset * 2, areaHeight)),areaInternalColor/* Color.White*/,/*ControlPaint.Dark(areaInternalColor)*/ Color.LightGray, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            
            System.Drawing.Drawing2D.GraphicsPath pth = DVDVideoSoft.Utils.GraphicsUtils.GetRoundedRect(GetAreaRect(), 4);
            Pen p = new Pen(areaBorderColor, 1);
            e.Graphics.FillPath(lBr/*new SolidBrush(areaInternalColor)*/, pth);

            lBr.Dispose();

            if (type == SliderType.RangeTrackBar || type==SliderType.RangeSelector)
            {
                if (rangeFrom < rangeTo)
                {
                    DrawRange(e.Graphics);

                    PointF rangePos = GetRangePositions();
                    if (showRangePos)
                    {
                        if (rangeFromPosStr.Length > 0)
                        {
                            e.Graphics.DrawLine(new Pen(areaBorderColor, 1), new PointF(rangeFromStrRect.Left, tp.Y - (float)areaHeight / 2 - refSize), new PointF(rangeFromStrRect.Right, tp.Y - (float)areaHeight / 2 - refSize));

                            if (rangePos.X < rangeFromStrRect.Left + rangeFromStrRect.Width/2)
                            {
                                e.Graphics.DrawLine(new Pen(areaBorderColor, 1), new PointF(rangeFromStrRect.Left, tp.Y - (float)areaHeight / 2 - refSize), new PointF(rangePos.X+refOffsetL, tp.Y - (float)areaHeight / 2));
                            }
                            else
                            {
                                e.Graphics.DrawLine(new Pen(areaBorderColor, 1), new PointF(rangeFromStrRect.Right, tp.Y - (float)areaHeight / 2 - refSize), new PointF(rangePos.X+refOffsetR, tp.Y - (float)areaHeight / 2));
                            }

                            e.Graphics.DrawString(rangeFromPosStr, Font, Brushes.Black, rangeFromStrRect.Left, rangeFromStrRect.Top);
                        }

                        if (rangeToPosStr.Length > 0)
                        {

                            e.Graphics.DrawLine(new Pen(areaBorderColor, 1), new PointF(rangeToStrRect.Left, tp.Y - (float)areaHeight / 2 - refSize), new PointF(rangeToStrRect.Right, tp.Y - (float)areaHeight / 2 - refSize));

                            if (rangePos.Y < rangeToStrRect.Left + rangeToStrRect.Width / 2)
                            {
                                e.Graphics.DrawLine(new Pen(areaBorderColor, 1), new PointF(rangeToStrRect.Left, tp.Y - (float)areaHeight / 2 - refSize), new PointF(rangePos.Y + refOffsetL, tp.Y - (float)areaHeight / 2));
                            }
                            else
                            {
                                e.Graphics.DrawLine(new Pen(areaBorderColor, 1), new PointF(rangeToStrRect.Right, tp.Y - (float)areaHeight / 2 - refSize), new PointF(rangePos.Y + refOffsetR, tp.Y - (float)areaHeight / 2));
                            }

                            e.Graphics.DrawString(rangeToPosStr, Font, Brushes.Black, rangeToStrRect.Left, rangeToStrRect.Top);
                        }
                    }

                    if (type == SliderType.RangeSelector && showCurrentPosMarker)
                    {
                        e.Graphics.FillPolygon(new SolidBrush(areaBorderColor),new PointF[]{new PointF(tp.X,tp.Y-(float)areaHeight/2),new PointF(tp.X-4,tp.Y-(float)areaHeight/2-4),new PointF(tp.X+4,tp.Y-(float)areaHeight/2-4),new PointF(tp.X,tp.Y-(float)areaHeight/2)});
                    }
                }
            }

            e.Graphics.DrawPath(p, pth);
            p.Dispose();

            pth.Dispose();

            if (type != SliderType.RangeSelector)
                DrawThumb(e.Graphics);
                
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Left)
                Position--;
            if (e.Control && e.KeyCode == Keys.Right)
                Position++;

            Invalidate();
            base.OnKeyDown(e);
        }

        private bool IsInArea(Point loc)
        {
            RectangleF area = GetAreaRect();
            return area.Contains(loc);
        }

        private bool IsMouseOnLeftRange(Point loc)
        {
            PointF range=GetRangePositions();
            PointF tp = GetThumbPosition();
            return (loc.X > range.X - markerSize && loc.X < range.X + 2 && IsInArea(loc));
        }

        private bool IsMouseOnRightRange(Point loc)
        {
            PointF range = GetRangePositions();
            PointF tp = GetThumbPosition();
            return (loc.X > range.Y - 2 && loc.X < range.Y + markerSize && IsInArea(loc));
        }

        int mPos = 0;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                base.OnMouseDown(e);
                return;
            }

            //if (editable && rangeEditFrom.Visible)
            //{
            //    rangeEditFrom_KeyDown(rangeEditFrom, new KeyEventArgs(Keys.Enter));
            //}

            //if (editable && rangeEditTo.Visible)
            //{
            //    rangeEditTo_KeyDown(rangeEditTo, new KeyEventArgs(Keys.Enter));
            //}

            if (type == SliderType.RangeSelector && IsMouseOnLeftRange(e.Location))
            {
                rangeFromSelected = true;
                rangeToSelected = false;
            }

            if (type == SliderType.RangeSelector && IsMouseOnRightRange(e.Location))
            {
                rangeToSelected = true;
                rangeFromSelected = false;
            }

            if (type == SliderType.RangeSelector && rangeFromStrRect.Contains(e.Location))
            {
                mPos = rangeFrom;
            }

            if (type == SliderType.RangeSelector && rangeToStrRect.Contains(e.Location))
            {
                mPos = rangeTo;
            }

            if (type == SliderType.RangeTrackBar || type == SliderType.TrackBar)
            {
                PointF tp = GetThumbPosition();
                //if (((double)(e.Location.X - tp.X) * (e.Location.X - tp.X) + (double)(e.Location.Y - tp.Y) * (e.Location.Y - tp.Y)) <= thumbRadius * thumbRadius && e.Button == MouseButtons.Left)
                    leftMouseDown = true;
//                else
                {
                    if (IsInArea(e.Location))
                    {
                        position = CalcPosition(e.Location);
                        Invalidate();
                        OnPositionChanged();
                    }
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            rangeFromSelected = rangeToSelected = false;

            if (editable && rangeFromStrRect.Contains(e.Location) && mPos==rangeFrom)
            {
                rangeEditTo.Hide();
                rangeEditFrom.Maximum = rangeTo > minimum ? rangeTo - 1 : rangeTo;
                rangeEditFrom.Value = rangeFrom;
                rangeEditFrom.Location = new Point((int)rangeFromStrRect.Left, (int)rangeFromStrRect.Bottom - rangeEditFrom.Height - 2);
                rangeEditFrom.Show();
                rangeEditFrom.Focus();
                //return;
            }

            if (editable && rangeToStrRect.Contains(e.Location) && mPos==rangeTo)
            {
                rangeEditFrom.Hide();
                rangeEditTo.Minimum = rangeFrom < maximum ? rangeFrom + 1 : rangeFrom;
                rangeEditTo.Value = rangeTo;
                rangeEditTo.Location = new Point((int)rangeToStrRect.Left + rangeEditTo.Width > Width ? Width - rangeEditTo.Width : (int)rangeToStrRect.Left, (int)rangeToStrRect.Bottom - rangeEditTo.Height - 2);
                rangeEditTo.Show();
                rangeEditTo.Focus();
                //return;
            }

            if (e.Button == MouseButtons.Left)
                leftMouseDown = false;

            Cursor = Cursors.Default;
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (type!=SliderType.TrackBar && editable && (rangeFromStrRect.Contains(e.Location) || rangeToStrRect.Contains(e.Location)))
                Cursor = Cursors.Hand;
            else
                if (type == SliderType.RangeSelector && (IsMouseOnLeftRange(e.Location) || IsMouseOnRightRange(e.Location) || rangeFromSelected || rangeToSelected))
                    Cursor = Cursors.SizeWE;
                else
                    Cursor = Cursors.Default;

            if (type == SliderType.RangeSelector)
            {
                if (rangeFromSelected)
                {
                    int rFrom = CalcPosition(e.Location);
                    if (rFrom > maximum)
                        rFrom = maximum;
                    if (rFrom < minimum)
                        rFrom = minimum;

                    if(rFrom<rangeTo)
                        RangeFrom = rFrom;
                }
                if (rangeToSelected)
                {
                    int rTo = CalcPosition(e.Location);

                    if (rTo > maximum)
                        rTo = maximum;
                    if (rTo < minimum)
                        rTo = minimum;

                    if(rTo>rangeFrom)
                        RangeTo = rTo;
                }

                if (rangeEditFrom.Visible)
                {
                    rangeEditFrom.Value = rangeFrom;
                    rangeEditFrom.Location = new Point((int)rangeFromStrRect.Left, (int)rangeFromStrRect.Bottom - rangeEditFrom.Height - 2);
                }
                if (rangeEditTo.Visible)
                {
                    rangeEditTo.Value = rangeTo;
                    rangeEditTo.Location = new Point((int)rangeToStrRect.Left + rangeEditTo.Width > Width ? Width - rangeEditTo.Width : (int)rangeToStrRect.Left, (int)rangeToStrRect.Bottom - rangeEditTo.Height - 2);
                }
            }
            else
            {
                if (leftMouseDown)
                {

                    position = CalcPosition(e.Location);
                    if (position > maximum)
                        position = maximum;
                    if (position < minimum)
                        position = minimum;
                    
                    OnPositionChanged();
                }
            }

            Invalidate();
            
            base.OnMouseMove(e);
        }
    }
}
