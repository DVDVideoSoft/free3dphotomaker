//---------------------------------------------------------------------
// 
//  Copyright (c) Microsoft Corporation.  All rights reserved.
// 
//THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
//KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//PARTICULAR PURPOSE.
//---------------------------------------------------------------------
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
using System.Diagnostics;
using System.ComponentModel;

namespace DVDVideoSoft.DialogForms
{
	/// <summary>
	/// Summary description for TreeGridCell.
	/// </summary>
	public class TreeGridCell:DataGridViewTextBoxCell
	{
		private const int INDENT_WIDTH = 20;
		private const int INDENT_MARGIN = 5;
		private int glyphWidth;
		private int calculatedLeftPadding;
		internal bool IsSited;
		private Padding _previousPadding;
		private int _imageWidth = 0, _imageHeight = 0, _imageHeightOffset = 0;
		//private Rectangle _lastKnownGlyphRect;

		public TreeGridCell()
		{			
			glyphWidth = 15;
			calculatedLeftPadding = 0;
			this.IsSited = false;

		}

        public override object Clone()
        {
			TreeGridCell c = (TreeGridCell)base.Clone();
			
            c.glyphWidth = this.glyphWidth;
            c.calculatedLeftPadding = this.calculatedLeftPadding;

            return c;
        }

		internal protected virtual void UnSited()
		{
			// The row this cell is in is being removed from the grid.
			this.IsSited = false;
            DataGridViewCellStyle oStyle = this.Style.Clone();
            oStyle.Padding = _previousPadding;
			this.Style=oStyle;
		}

		internal protected virtual void Sited()
		{
			// when we are added to the DGV we can realize our style
			this.IsSited = true;

			// remember what the previous padding size is so it can be restored when unsiting
			this._previousPadding = this.Style.Padding;

			this.UpdateStyle();
		}		

		internal protected virtual void UpdateStyle(){
			// styles shouldn't be modified when we are not sited.
			if (this.IsSited == false) return;

			int level = this.Level;

			Padding p = this._previousPadding;
			Size preferredSize;

			using (Graphics g = this.OwningNode._grid.CreateGraphics() ) {
				preferredSize =this.GetPreferredSize(g, this.InheritedStyle, this.RowIndex, new Size(0, 0));
			}

			Image image = this.OwningNode.Image;

			if (image != null)
			{
				// calculate image size
				_imageWidth = image.Width+2;
				_imageHeight = image.Height+2;

			}
			else
			{
				_imageWidth = glyphWidth;
				_imageHeight = 0;
			}

            if (preferredSize.Height < _imageHeight)
            {
                DataGridViewCellStyle oStyle = this.Style.Clone();
                oStyle.Padding = new Padding(p.Left + (level * INDENT_WIDTH) + _imageWidth + INDENT_MARGIN,p.Top + (_imageHeight / 2), p.Right, p.Bottom + (_imageHeight / 2));
                _imageHeightOffset = 2;// (_imageHeight - preferredSize.Height) / 2;

                this.Style = oStyle;
            }
            else
            {
                DataGridViewCellStyle oStyle = this.Style.Clone();
                oStyle.Padding = new Padding(p.Left + (level * INDENT_WIDTH) + _imageWidth + INDENT_MARGIN,p.Top, p.Right, p.Bottom);

                this.Style = oStyle;
            }

			calculatedLeftPadding = ((level - 1) * glyphWidth) + _imageWidth + INDENT_MARGIN;
		}

		public int Level
		{
			get
			{
				TreeGridNode row = this.OwningNode;
				if (row != null)
				{
					return row.Level;
				}
				else
					return -1;
			}
		}

		protected virtual int GlyphMargin
		{
			get
			{
				return ((this.Level - 1) * INDENT_WIDTH) + INDENT_MARGIN;
			}
		}

		protected virtual int GlyphOffset
		{
			get
			{
				return ((this.Level - 1) * INDENT_WIDTH);
			}
		}

		protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
		{

            TreeGridNode node = this.OwningNode;
            if (node == null) return;

            Image image = node.Image;

            if (this._imageHeight == 0 && image != null) this.UpdateStyle();

			// paint the cell normally
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            // TODO: Indent width needs to take image size into account
			Rectangle glyphRect = new Rectangle(cellBounds.X + this.GlyphMargin, cellBounds.Y, INDENT_WIDTH, cellBounds.Height - 1);
			int glyphHalf = glyphRect.Width / 2;

			//TODO: This painting code needs to be rehashed to be cleaner
			int level = this.Level;

            //TODO: Rehash this to take different Imagelayouts into account. This will speed up drawing
			//		for images of the same size (ImageLayout.None)
			if (image != null)
			{
				Point pp;
				if (_imageHeight > cellBounds.Height)
                    pp = new Point(/*glyphRect.X + this.glyphWidth*/glyphRect.Right, cellBounds.Y + _imageHeightOffset);
				else
                    pp = new Point(/*glyphRect.X + this.glyphWidth*/glyphRect.Right, (cellBounds.Height / 2 - _imageHeight / 2) + cellBounds.Y);

				// Graphics container to push/pop changes. This enables us to set clipping when painting
				// the cell's image -- keeps it from bleeding outsize of cells.
				System.Drawing.Drawing2D.GraphicsContainer gc = graphics.BeginContainer();
				{
					graphics.SetClip(cellBounds);
					graphics.DrawImageUnscaled(image, pp);
				}
				graphics.EndContainer(gc);
			}

			// Paint tree lines			
            if (node._grid.ShowLines)
            {
                using (Pen linePen = new Pen(SystemBrushes.ControlDark, 1.0f))
                {
                    linePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    bool isLastSibling = node.IsLastSibling;
                    bool isFirstSibling = node.IsFirstSibling;
                    if (node.Level == 1)
                    {
                        // the Root nodes display their lines differently
                        if (isFirstSibling && isLastSibling)
                        {
                            // only node, both first and last. Just draw horizontal line
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                        }
                        else if (isLastSibling)
                        {
                            // last sibling doesn't draw the line extended below. Paint horizontal then vertical
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2);
                        }
                        else if (isFirstSibling)
                        {
                            // first sibling doesn't draw the line extended above. Paint horizontal then vertical
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.X + 4, cellBounds.Bottom);
                        }
                        else
                        {
                            // normal drawing draws extended from top to bottom. Paint horizontal then vertical
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top, glyphRect.X + 4, cellBounds.Bottom);
                        }
                    }
                    else
                    {
                        if (isLastSibling)
                        {
                            // last sibling doesn't draw the line extended below. Paint horizontal then vertical
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2);
                        }
                        else
                        {
                            // normal drawing draws extended from top to bottom. Paint horizontal then vertical
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top + cellBounds.Height / 2, glyphRect.Right, cellBounds.Top + cellBounds.Height / 2);
                            graphics.DrawLine(linePen, glyphRect.X + 4, cellBounds.Top, glyphRect.X + 4, cellBounds.Bottom);
                        }

                        // paint lines of previous levels to the root
                        TreeGridNode previousNode = node.Parent;
                        int horizontalStop = (glyphRect.X + 4) - INDENT_WIDTH;

                        while (!previousNode.IsRoot)
                        {
                            if (previousNode.HasChildren && !previousNode.IsLastSibling)
                            {
                                // paint vertical line
                                graphics.DrawLine(linePen, horizontalStop, cellBounds.Top, horizontalStop, cellBounds.Bottom);
                            }
                            previousNode = previousNode.Parent;
                            horizontalStop = horizontalStop - INDENT_WIDTH;
                        }
                    }

                }
            }

            if (node.HasChildren || node._grid.VirtualNodes)
            {
                // Paint node glyphs				
                if (Application.RenderWithVisualStyles)
                {
                    if (node.IsExpanded)
                        node._grid.rOpen.DrawBackground(graphics, new Rectangle(glyphRect.X, glyphRect.Y + (glyphRect.Height / 2) - 4, 10, 10));
                    else
                        node._grid.rClosed.DrawBackground(graphics, new Rectangle(glyphRect.X, glyphRect.Y + (glyphRect.Height / 2) - 4, 10, 10));
                }
                else
                {
                    if(node.IsExpanded)
                        DrawMinus(graphics,new Rectangle(glyphRect.X, glyphRect.Y + (glyphRect.Height / 2) - 4, 8, 8));
                    else
                        DrawPlus(graphics, new Rectangle(glyphRect.X, glyphRect.Y + (glyphRect.Height / 2) - 4, 8, 8));
                }
            }


		}

        private void DrawMinus(Graphics gr, Rectangle rc)
        {
            gr.FillRectangle(Brushes.White, rc);
            gr.DrawRectangle(Pens.Black,rc);
            gr.DrawLine(Pens.Black,new Point(rc.Left+(int)(rc.Width*0.3),rc.Top+rc.Height/2),new Point(rc.Right-(int)(rc.Width*0.3),rc.Top+rc.Height/2));
        }

        private void DrawPlus(Graphics gr, Rectangle rc)
        {
            DrawMinus(gr, rc);
            gr.DrawLine(Pens.Black,new Point(rc.Left+rc.Width/2,rc.Top+(int)(rc.Height*0.3)),new Point(rc.Left+rc.Width/2,rc.Bottom-(int)(rc.Height*0.3)));
        }
        protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
        {
            base.OnMouseUp(e);

            TreeGridNode node = this.OwningNode;
            if (node != null)
                node._grid._inExpandCollapseMouseCapture = false;
        }
		protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
		{
            Rectangle glyphRect = new Rectangle(ContentBounds.X + this.GlyphMargin, ContentBounds.Y, INDENT_WIDTH, ContentBounds.Height - 1);
            Rectangle plusRc;
            if (Application.RenderWithVisualStyles)
            {
                    plusRc=new Rectangle(glyphRect.X, glyphRect.Y + (glyphRect.Height / 2) - 4, 10, 10);
            }
            else
            {
                    plusRc= new Rectangle(glyphRect.X, glyphRect.Y + (glyphRect.Height / 2) - 4, 8, 8);
            }

            if (!plusRc.Contains(new Point(e.X + this.InheritedStyle.Padding.Left, e.Y)))
			{
				base.OnMouseDown(e);
			}
			else
			{
				// Expand the node
				//TODO: Calculate more precise location
				TreeGridNode node = this.OwningNode;
                if (node != null)
				{
                    node._grid._inExpandCollapseMouseCapture = true;
                    if (node.IsExpanded)
                        node.Collapse();
					else
                        node.Expand();
                }
			}
		}
		public TreeGridNode OwningNode
		{
			get { return base.OwningRow as TreeGridNode; }
		}
	}

	public class TreeGridColumn : DataGridViewTextBoxColumn
	{
		internal Image _defaultNodeImage;
		
		public TreeGridColumn()
		{		
			this.CellTemplate = new TreeGridCell();
		}

		// Need to override Clone for design-time support.
		public override object Clone()
		{
			TreeGridColumn c = (TreeGridColumn)base.Clone();
			c._defaultNodeImage = this._defaultNodeImage;
			return c;
		}

		public Image DefaultNodeImage
		{
			get { return _defaultNodeImage; }
			set { _defaultNodeImage = value; }
		}
	}

    public class DataGridViewProgressColumn : DataGridViewImageColumn
    {
        public DataGridViewProgressColumn()
        {
            CellTemplate = new DataGridViewProgressCell();
        }
    }

    public class DataGridViewProgressCell : DataGridViewImageCell
    {
        // Used to make custom cell consistent with a DataGridViewImageCell
        static Image emptyImage;
        static DataGridViewProgressCell()
        {
            emptyImage = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        }
        public DataGridViewProgressCell()
        {

            this.ValueType = typeof(int);
        }

        // Method required to make the Progress Cell consistent with the default Image Cell. 
        // The default Image Cell assumes an Image as a value, although the value of the Progress Cell is an int.
        protected override object GetFormattedValue(object value,
                            int rowIndex, ref DataGridViewCellStyle cellStyle,
                            TypeConverter valueTypeConverter,
                            TypeConverter formattedValueTypeConverter,
                            DataGridViewDataErrorContexts context)
        {
            return emptyImage;
        }

        protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            int progressVal=-1;


            if (value != null)

                progressVal = (int)value;
            
            float percentage = ((float)progressVal / 100.0f); // Need to convert to float before division; otherwise C# returns int which is 0 for anything but 100%.
            Brush backColorBrush = new SolidBrush(cellStyle.BackColor);
            Brush foreColorBrush = new SolidBrush(cellStyle.ForeColor);
            // Draws the cell grid
            base.Paint(g, clipBounds, cellBounds,
             rowIndex, cellState, value, formattedValue, errorText,
             cellStyle, advancedBorderStyle, (paintParts & ~DataGridViewPaintParts.ContentForeground));

            if (progressVal == -1)
                return;

            if (percentage > 0.0)
            {
                System.Drawing.Drawing2D.SmoothingMode old = g.SmoothingMode;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                //Rectangle grRect=new Rectangle(cellBounds.X + 2, cellBounds.Y+4/*+cellBounds.Height/4*/, Convert.ToInt32((percentage * cellBounds.Width - 4)), cellBounds.Height/*/2*/-8 );
                //System.Drawing.Drawing2D.LinearGradientBrush lBr = new System.Drawing.Drawing2D.LinearGradientBrush(grRect, Color.White, Color.Lime, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                //System.Drawing.Drawing2D.ColorBlend bl = new System.Drawing.Drawing2D.ColorBlend();

                //bl.Colors = new Color[] { Color.White, Color.FromArgb(87, 164, 182), Color.FromArgb(87, 164, 182) };
                //bl.Positions = new float[] { 0.0f, 0.5f, 1.0f };
                //lBr.InterpolationColors = bl;
                //System.Drawing.Drawing2D.GraphicsPath pth = DVDVideoSoft.Utils.GraphicsUtils.GetRoundedRect(grRect, 2);
                //g.FillPath(lBr, pth);
                ////g.FillRectangle(lBr, grRect);
                //g.SmoothingMode = old;
                //lBr.Dispose();
                //pth.Dispose();
                //g.DrawString(progressVal.ToString() + "%", cellStyle.Font, foreColorBrush, cellBounds.X + 6, cellBounds.Y + 2);

                Rectangle grRect = new Rectangle(cellBounds.X + 2, cellBounds.Y + 3, Convert.ToInt32((percentage * cellBounds.Width - 4)), cellBounds.Height - 6);
                System.Drawing.Drawing2D.GraphicsPath pth = DVDVideoSoft.Utils.GraphicsUtils.GetRoundedRect(grRect, 2);
                //g.FillRectangle(new SolidBrush(Color.FromArgb(163, 189, 242)),grRect);
                g.FillPath(new SolidBrush(Color.FromArgb(163, 189, 242)), pth);
                //g.DrawString(progressVal.ToString() + "%", cellStyle.Font, foreColorBrush, cellBounds.X + 6, cellBounds.Y + 2);
                g.SmoothingMode = old;
                

            }
        }
    }
}
