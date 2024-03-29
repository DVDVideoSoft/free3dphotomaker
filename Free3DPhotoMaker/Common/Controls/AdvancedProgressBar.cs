/*
 * 
 * Advanced Progress Bar - by Gil Schmidt.
 * 
 * - This was written for a p2p project called Kibutz.
 * 
 * - The first version only included a basic rectangle progress bar 
 *   with "3d" abilitys and percent display.
 * 
 * - I decided adding dynamic shape function so it could be used
 *   with any shape (you can add more shapes in CreateRegion function).
 * 
 * - Thanks goes to CustomPanel - by The Man from U.N.C.L.E.
 *   (http://www.codeproject.com/vb/net/custompanel.asp)
 *   his code helped a little.
 * 
 * [ Contact me at Gil_Smdt@hotmail.com. ]
 * 
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;

using System.Data;
using System.Windows.Forms;

namespace DVDVideoSoft.Controls
{
	[Description("Addvanced Progress Bar.")]
	[ToolboxBitmap(typeof(ProgressBar))]
	[Designer(typeof(AdvancedProgressBarDesigner))]

	public class AdvancedProgressBar : System.Windows.Forms.Control
	{
		private System.ComponentModel.Container components = null;

		public AdvancedProgressBar()
		{
			InitializeComponent();

			SetStyle(
				ControlStyles.AllPaintingInWmPaint |
				ControlStyles.ResizeRedraw |
				ControlStyles.DoubleBuffer |
				ControlStyles.UserPaint |
				ControlStyles.SupportsTransparentBackColor,
				true
				);

			base.Size = new Size(200,100);
			this.BackColor = Color.Transparent;
			this._Color1 = Color.Black;
			this._Color2 = Color.White;
			this._BorderColor = Color.OrangeRed;
			this._BorderSize = 1;
			this._BorderDisplay = true;
			this._PercentDisplay = true;
			this.ForeColor = Color.Green;
			this.Font = new Font("Tahoma",8);
			this._Position = 0;
			this.ProgressBarPath.AddRectangle(new Rectangle(0,0,this.Width,this.Height));
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		private void InitializeComponent()
		{
			// 
			// AdvancedProgressBar
			// 
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(192)), ((System.Byte)(0)));
			this.Size = new System.Drawing.Size(176, 32);
			this.Resize += new System.EventHandler(this.AdvancedProgressBar_Resize);
		}

		#endregion

		private Color _BorderColor,_Color1,_Color2,_ProgressBarBackColor;
		private bool _PercentDisplay,_BorderDisplay;
		private int _BorderSize;
		private int _Position;
		private Region ProgressBarRegion;
		private GraphicsPath ProgressBarPath = new GraphicsPath();
		private FillingType _FillingMethod;
		private FillDirection _GradientMethod;
		private ProgressBarShape _Shape;
		private LinearGradientMode GradientMode;

		[Description("Shape of the progress bar.")]
		[Category("AddvancedProgressBar")]
		[RefreshProperties(RefreshProperties.All)]

		public ProgressBarShape Shape 
		{
			get	{ return _Shape; }
			set	{ _Shape = value; this.Refresh(); }
		}

		[Description("Method of filling for the progress bar.")]
		[Category("AddvancedProgressBar")]
		[RefreshProperties(RefreshProperties.All)]

		public FillingType FillingMethod 
		{
			get	{ return _FillingMethod; }
			set	{ _FillingMethod = value; this.Refresh(); }
		}
 
		[Description("Back color of the progress bar.")]
		[Category("AddvancedProgressBar")]
		[RefreshProperties(RefreshProperties.All)]

		public Color ProgressBarBackColor
		{
			get	{ return _ProgressBarBackColor; }
			set	{ _ProgressBarBackColor = value; this.Refresh(); }
		}

		[Description("First color that's used for gradient.")]
		[Category("AddvancedProgressBar")]
		[RefreshProperties(RefreshProperties.All)]

		public Color Color1 
		{
			get	{ return _Color1; }
			set	{ _Color1 = value; this.Refresh(); }
		}

		[Description("Second color that's used for gradient.")]
		[Category("AddvancedProgressBar")]
		[RefreshProperties(RefreshProperties.All)]

		public Color Color2 
		{
			get	{ return _Color2; }
			set	{ _Color2 = value; this.Refresh(); }
		}

		[Description("Enables / disables the percent display.")]
		[Category("AddvancedProgressBar")]
		[RefreshProperties(RefreshProperties.All)]

		public bool PercentDisplay
		{
			get	{ return _PercentDisplay; }
			set	{ _PercentDisplay = value; this.Refresh(); }
		}

		[Description("Enables / disables the progress bar border.")]
		[Category("AddvancedProgressBar")]
		[RefreshProperties(RefreshProperties.All)]

		public bool BorderDisplay 
		{
			get	{ return _BorderDisplay; }
			set	{ _BorderDisplay = value; this.Refresh(); }
		}
		
		[Description("The size of the progress bar border.")]
		[Category("AddvancedProgressBar")]
		[RefreshProperties(RefreshProperties.All)]

		public int BorderSize 
		{
			get	{ return _BorderSize; }
			set	{ _BorderSize = value; this.Refresh(); }
		}
		
		[Description("The color of the progress bar border.")]
		[Category("AddvancedProgressBar")]
		[RefreshProperties(RefreshProperties.All)]
		
		public Color BorderColor 
		{
			get	{ return _BorderColor; }
			set	{ _BorderColor = value; this.Refresh(); }
		}
		
		[Description("The position value of the progress bar.")]
		[Category("AddvancedProgressBar")]
		[RefreshProperties(RefreshProperties.All)]

		public int Position
		{
			get { return _Position; }
			set 
			{ 
				if (value > 100) return; 
				if (value < 0) return;
				_Position = value;
                Text = _Position + "%";
				this.Invalidate();
			}
		}
		
		[Description("The style of the progress bar.")]
		[Category("AddvancedProgressBar")]
		[RefreshProperties(RefreshProperties.All)]

		public FillDirection GradientMethod
		{
			get { return _GradientMethod; }
			set 
			{ 
				switch (value)
				{
					case FillDirection.Horizontal: 
						GradientMode = LinearGradientMode.Horizontal; 
						break;

					case FillDirection.Vertical: 
						GradientMode = LinearGradientMode.Vertical; 
						break;
				}

				_GradientMethod = value; 
				this.Refresh(); 
			}
			
		}
		
		public enum ProgressBarShape
		{
			Rectangle,
			Ellipse,
			RoundedRectangle,
			Triangle,
			MagenDavid,
			Snake,
		}

		public enum FillingType
		{
			Normal,
			Reverse,
			TopToBottom,
			BottomToTop,
		}

		public enum FillDirection
		{
			Horizontal,
			Vertical,
			Gradient3D,
		}

		private enum FillRegion
		{
			Top,
			Bottom,
			Left,
			Right,
			EmptyTop,
			EmptyBottom,
			EmptyLeft,
			EmptyRight,
			Total,
		}

		private enum RegionDirection
		{
			Horizontal,
			Vertical,
		}

		public void DrawProgressBar()
		{
			this.Invalidate();
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
			ProgressBarRegion = CreateRegion();
			DrawBackground(e.Graphics);
			DrawFilling(e.Graphics);
			
			if (_BorderDisplay) DrawBorder(e.Graphics);
			if (_PercentDisplay) DrawPercent(e.Graphics);
		}


		private Region CreateRegion()
		{
			Point[] Coordinates;
			int CornerWidth,CornerHeight;

			ProgressBarPath.Reset();
			ProgressBarPath.CloseAllFigures();

			switch (_Shape)
			{
				case ProgressBarShape.Rectangle:
					ProgressBarPath.AddRectangle(new Rectangle(0,0,this.Width,this.Height));
					break;

				case ProgressBarShape.Ellipse:
					ProgressBarPath.AddEllipse(0,0,this.Width,this.Height);
					break;

				case ProgressBarShape.RoundedRectangle:
					CornerWidth = this.Width / 20;
					CornerHeight = (int)(this.Height / 2);

					ProgressBarPath.AddArc(_BorderSize,_BorderSize,CornerWidth,CornerHeight,180,90);
					ProgressBarPath.AddArc(this.Width - CornerWidth - _BorderSize,_BorderSize,CornerWidth,CornerHeight,270,90);
					ProgressBarPath.AddArc(this.Width - CornerWidth - _BorderSize,this.Height - CornerHeight - _BorderSize,CornerWidth,CornerHeight,0,90);
					ProgressBarPath.AddArc(_BorderSize,this.Height - CornerHeight - _BorderSize,CornerWidth,CornerHeight,90,90);
					ProgressBarPath.CloseFigure();
					break;

				case ProgressBarShape.Triangle:
					Coordinates = new Point[]
						{
							new Point(_BorderSize,this.Height - _BorderSize),
							new Point(this.Width - _BorderSize,this.Height - _BorderSize),
							new Point(this.Width/2,_BorderSize)
						};

					ProgressBarPath.AddPolygon(Coordinates);
					ProgressBarPath.CloseFigure();
					break;

				case ProgressBarShape.MagenDavid:
					GraphicsPath a = new GraphicsPath();
					int RibWidth = this.Width / 6;
					int RibHeight = this.Height / 4;

					Coordinates = new Point[]
						{
							new Point(this.Width/2,0),
							new Point(this.Width/2 + RibWidth,RibHeight),
							new Point(this.Width/2 + RibWidth * 3,RibHeight),
							new Point(this.Width/2 + RibWidth * 2,RibHeight * 2),
							new Point(this.Width/2 + RibWidth * 3,RibHeight * 3),
							new Point(this.Width/2 + RibWidth,RibHeight * 3),
							new Point(this.Width/2,RibHeight * 4),
							new Point(this.Width/2 - RibWidth,RibHeight * 3),
							new Point(this.Width/2 - RibWidth * 3,RibHeight * 3),
							new Point(this.Width/2 - RibWidth * 2,RibHeight * 2),
							new Point(this.Width/2 - RibWidth * 3,RibHeight),
							new Point(this.Width/2 - RibWidth,RibHeight),
							new Point(this.Width/2,0),
						};
					ProgressBarPath.AddLines(Coordinates);
					break;

				case ProgressBarShape.Snake:
					CornerWidth = this.Width / 6;
					CornerHeight = this.Height / 3;

					Coordinates = new Point[]
						{
							new Point(1,1),
							new Point(CornerWidth,CornerHeight),
							new Point(CornerWidth*2,2),
							new Point(CornerWidth*3,CornerHeight),
							new Point(CornerWidth*4,3),
							
							new Point(this.Width-4,this.Height-4),

							new Point(CornerWidth*4 + 20,CornerHeight*2),
							new Point(CornerWidth*3,CornerHeight*3-1),
							new Point(CornerWidth*2,CornerHeight*2),
							new Point(CornerWidth,CornerHeight*3-1),
							new Point(CornerWidth/2,CornerHeight*2),
							new Point(1,1),
					};
				
					ProgressBarPath.AddClosedCurve(Coordinates,0.5f);
					ProgressBarPath.CloseFigure();
					break;
			}
			return new Region(ProgressBarPath);
		}

		private void DrawBackground(Graphics G)
		{
			SolidBrush BackgroundBrush = new SolidBrush(_ProgressBarBackColor);
			G.FillPath(BackgroundBrush,ProgressBarPath);
			BackgroundBrush.Dispose();
		}

		private void DrawFilling(Graphics G)
		{
			if (_GradientMethod == FillDirection.Gradient3D)
				ShapeFilling3D(G);
			else
				ShapeFilling(G);

			Region BackgroundRegion = new Region();
			BackgroundRegion.Exclude(ProgressBarRegion);
			G.FillRegion(new SolidBrush(Color.Transparent),BackgroundRegion);
		}

		private void DrawBorder(Graphics G)
		{
			if (_BorderSize == 0) return;

			switch (_Shape)
			{
				case ProgressBarShape.Rectangle:
					G.DrawRectangle(new Pen(_BorderColor,_BorderSize),0,0,this.Width - _BorderSize,this.Height - _BorderSize);
					break;
				
				case ProgressBarShape.Ellipse:

					G.DrawEllipse(new Pen(_BorderColor,_BorderSize),
								  0 + _BorderSize/2,
								  0 + _BorderSize/2,
								  this.ClientRectangle.Width - _BorderSize + 0.5f,
								  this.ClientRectangle.Height - _BorderSize + 0.5f);
					break;

				default:
					G.DrawPath(new Pen(_BorderColor,_BorderSize),ProgressBarPath);					
					break;
			}
		}

		private void DrawPercent(Graphics G)
		{
            SizeF TextSize = G.MeasureString(Text,Font);
            SolidBrush TextBrush = new SolidBrush(ForeColor);
            float Left, Top;
            

            Left = (Width - TextSize.Width) / 2;
            Top = (Height - TextSize.Height) / 2;
                 
            G.DrawString(
                Text,
                Font,
                TextBrush,
                new PointF(Left, Top));

            TextBrush.Dispose();
		}

		private void ShapeFilling3D(Graphics G)
		{
			Region TempRegion = ProgressBarRegion.Clone();

			switch (_FillingMethod)
			{
				case FillingType.Normal:
					//top
					TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyRight,RegionDirection.Horizontal));
					TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyBottom,RegionDirection.Horizontal));
					FillShapeRegion(G,TempRegion,FillRegion.Top,_Color1,_Color2,LinearGradientMode.Vertical);
					//buttom
					TempRegion = ProgressBarRegion.Clone();
					TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyRight,RegionDirection.Horizontal));
					TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyTop,RegionDirection.Horizontal));
					FillShapeRegion(G,TempRegion,FillRegion.Bottom,_Color2,_Color1,LinearGradientMode.Vertical);
					break;

				case FillingType.Reverse:
					//top
					TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyLeft,RegionDirection.Horizontal));
					TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyBottom,RegionDirection.Horizontal));
					FillShapeRegion(G,TempRegion,FillRegion.Top,_Color1,_Color2,LinearGradientMode.Vertical);
					//buttom
					TempRegion = ProgressBarRegion.Clone();
					TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyLeft,RegionDirection.Horizontal));
					TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyTop,RegionDirection.Horizontal));
					FillShapeRegion(G,TempRegion,FillRegion.Bottom,_Color2,_Color1,LinearGradientMode.Vertical);
					break;

				case FillingType.TopToBottom:
					//left
					TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyRight,RegionDirection.Vertical));
					TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyBottom,RegionDirection.Vertical));
					FillShapeRegion(G,TempRegion,FillRegion.Left,_Color1,_Color2,LinearGradientMode.Horizontal);
					//right
					TempRegion = ProgressBarRegion.Clone();
					TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyLeft,RegionDirection.Vertical));
					TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyBottom,RegionDirection.Vertical));
					FillShapeRegion(G,TempRegion,FillRegion.Right,_Color2,_Color1,LinearGradientMode.Horizontal);
					break;

				case FillingType.BottomToTop:
					//left
					TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyRight,RegionDirection.Vertical));
					TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyTop,RegionDirection.Vertical));
					FillShapeRegion(G,TempRegion,FillRegion.Left,_Color1,_Color2,LinearGradientMode.Horizontal);
					//right
					TempRegion = ProgressBarRegion.Clone();
					TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyLeft,RegionDirection.Vertical));
					TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyTop,RegionDirection.Vertical));
					FillShapeRegion(G,TempRegion,FillRegion.Right,_Color2,_Color1,LinearGradientMode.Horizontal);
					break;
			}
		}

		private void ShapeFilling(Graphics G)
		{
			Region TempRegion = ProgressBarRegion.Clone();

			if (_Position < 100) 
				switch (_FillingMethod)
				{
					case FillingType.Normal:
						TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyRight,RegionDirection.Horizontal));
						break;

					case FillingType.Reverse:
						TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyLeft,RegionDirection.Horizontal));
						break;

					case FillingType.TopToBottom:
						TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyBottom,RegionDirection.Vertical));
						break;

					case FillingType.BottomToTop:
						TempRegion.Exclude(RectangleByRegion(FillRegion.EmptyTop,RegionDirection.Vertical));
						break;
				}
			
			FillShapeRegion(G,TempRegion,FillRegion.Total,_Color2,_Color1,GradientMode);
		}

		private void FillShapeRegion(Graphics G,Region DestRegion,FillRegion RegionLocation,Color ColorA,Color ColorB,LinearGradientMode GradientType)
		{
			if (DestRegion.GetBounds(G).Width == 0 || DestRegion.GetBounds(G).Height == 0) return;

			LinearGradientBrush FillingBrush;
			Rectangle DestRectangle = RectangleByRegion(RegionLocation,RegionDirection.Horizontal);

			FillingBrush = new LinearGradientBrush(
				DestRectangle,
				ColorA,
				ColorB,
				GradientType);

			G.FillRegion(FillingBrush,DestRegion);
			FillingBrush.Dispose();
		}

		private Rectangle RectangleByRegion(FillRegion SelectedRegion,RegionDirection Direction)
		{
			float DividerWidth = (float) this.Width / 100;
			float DividerHeight = (float) this.Height / 100;

			int EmptyWidth = this.Width - (int) (_Position * DividerWidth);
			int EmptyHeight = this.Height - (int) (_Position * DividerHeight);

			if (Direction == RegionDirection.Horizontal)
				EmptyHeight = this.Height/2;
			else
				EmptyWidth = this.Width/2;

			switch (SelectedRegion)
			{
				case FillRegion.Top:
					return new Rectangle(0,0,this.Width,this.Height/2 + 1);					

				case FillRegion.Bottom:
					return new Rectangle(0,this.Height/2,this.Width,this.Height/2);					

				case FillRegion.Left:
					return new Rectangle(0,0,this.Width/2 + 2,this.Height);

				case FillRegion.Right:
					return new Rectangle(this.Width/2 - 1,0,this.Width/2 + 2,this.Height);
			}

			switch (SelectedRegion)
			{

				case FillRegion.EmptyTop:
					return new Rectangle(0,0,this.Width,EmptyHeight);					

				case FillRegion.EmptyBottom:
					return new Rectangle(0,this.Height - EmptyHeight,this.Width,EmptyHeight);					

				case FillRegion.EmptyLeft:
					return new Rectangle(0,0,EmptyWidth,this.Height);

				case FillRegion.EmptyRight:
					return new Rectangle(this.Width - EmptyWidth,0,EmptyWidth,this.Height);
			}
			return new Rectangle(0,0,this.Width,this.Height);
		}

		private void AdvancedProgressBar_Resize(object sender, System.EventArgs e)
		{
			DrawProgressBar();
		}
	}
}
