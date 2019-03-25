using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace NgNet.UI.Forms
{
    /// <summary>
    /// Item背景渲染
    /// </summary>
	public class VisualStyleItemBackground // can't find system provided visual style for this.
	{
		//http://www.ookii.org/misc/vsstyle.h
		//http://msdn2.microsoft.com/en-us/library/bb773210(VS.85).aspx
		enum ITEMSTATES
		{
			LBPSI_HOT = 1,
			LBPSI_HOTSELECTED = 2,
			LBPSI_SELECTED = 3,
			LBPSI_SELECTEDNOTFOCUS = 4,
		};

		enum LISTBOXPARTS
		{
			LBCP_BORDER_HSCROLL = 1,
			LBCP_BORDER_HVSCROLL = 2,
			LBCP_BORDER_NOSCROLL = 3,
			LBCP_BORDER_VSCROLL = 4,
			LBCP_ITEM = 5,
		};

        #region private fields
        private Pen _pen;
        #endregion

        #region public properties
        /// <summary>
        /// 获取或设置选中项的背景色
        /// </summary>
        public Color SelectedItemBackColor { get; set; }
        /// <summary>
        /// 获取或设置非选中项的背景色
        /// </summary>
        public Color BackColor { get; set; }
        /// <summary>
        /// 获取或设置项的状态（选中与非选中）
        /// </summary>
        public TreeListItemState ItemState { get; set; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public VisualStyleItemBackground()
		{
            ItemState = TreeListItemState.Inactive;
            BackColor = Color.Azure;
            SelectedItemBackColor = Color.Green;
            _pen = new Pen(BackColor);
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="dc"></param>
        /// <param name="r"></param>
		public void DrawBackground(Control owner, Graphics dc, Rectangle r)
		{
			IntPtr themeHandle = Windows.Apis.Uxtheme.OpenThemeData(owner.Handle, "Explorer");
			if (themeHandle != IntPtr.Zero)
			{
                Windows.Apis.Uxtheme.DrawThemeBackground(themeHandle, dc.GetHdc(), (int)LISTBOXPARTS.LBCP_ITEM, (int)ITEMSTATES.LBPSI_SELECTED, r, r);
				dc.ReleaseHdc();
                Windows.Apis.Uxtheme.CloseThemeData(themeHandle);
				return;
			}

			_pen.Color = ItemState == TreeListItemState.Normal ?  SelectedItemBackColor : BackColor;
			GraphicsPath path = new GraphicsPath();
			path.AddLine(r.Left + 2, r.Top, r.Right - 2, r.Top);
			path.AddLine(r.Right, r.Top + 2, r.Right, r.Bottom - 2);
			path.AddLine(r.Right - 2, r.Bottom, r.Left + 2, r.Bottom);
			path.AddLine(r.Left, r.Bottom - 2, r.Left, r.Top + 2);
			path.CloseFigure();
			dc.DrawPath(_pen, path);

			r.Inflate(-1, -1);
			LinearGradientBrush brush = new LinearGradientBrush(r, Color.White, Color.FromArgb(90, SelectedItemBackColor), 90);
			dc.FillRectangle(brush, r);
			// for some reason in some cases the 'white' end of the gradient brush is drawn with the starting color
			// therefore this redraw of the 'top' line of the rectangle
			dc.DrawLine(Pens.White, r.Left + 1, r.Top, r.Right - 1, r.Top);
			brush.Dispose();
			path.Dispose();
		}
	}

    /// <summary>
    /// Item绘制
    /// </summary>
	public class CellPainter
	{
        #region protected properties
        /// <summary>
        /// 获取Cell所属的TreeList
        /// </summary>
        protected TreeList Owner { get; }
        #endregion

        #region internal properties
        internal VisualStyleItemBackground VisualStyleItemBackground { get; }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        public CellPainter(TreeList owner)
		{
            VisualStyleItemBackground = new VisualStyleItemBackground();
            VisualStyleItemBackground.BackColor = owner.BackColor;
            VisualStyleItemBackground.SelectedItemBackColor = NgNet.Drawing.ColorHelper.GetSimilarColor(owner.BackColor, true, Level.Level6);
			Owner = owner;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="nodeRect"></param>
        /// <param name="node"></param>
		public virtual void DrawSelectionBackground(Graphics dc, Rectangle nodeRect, TreeListNode node)
		{
			if (Owner.NodesSelection.Contains(node) || Owner.FocusedNode == node)
			{
				if (!Application.RenderWithVisualStyles)
				{
					// have to fill the solid background only before the node is painted
					dc.FillRectangle(SystemBrushes.FromSystemColor(SystemColors.Highlight), nodeRect);
				}
				else
				{
					// have to draw the transparent background after the node is painted

                    VisualStyleItemBackground.ItemState = Owner.Focus() ? TreeListItemState.Normal : TreeListItemState.Inactive;
                    VisualStyleItemBackground.DrawBackground(Owner, dc, nodeRect);
				}
			}
			if (Owner.Focused && (Owner.FocusedNode == node))
			{
				nodeRect.Height += 1;
				nodeRect.Inflate(-1,-1);
				ControlPaint.DrawFocusRectangle(dc, nodeRect);
			}
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="cellRect"></param>
        /// <param name="node"></param>
        /// <param name="column"></param>
        /// <param name="format"></param>
        /// <param name="data"></param>
		public virtual void PaintCell(Graphics dc, 
			Rectangle cellRect, 
			TreeListNode node, 
			TreeListColumn column, 
			TextFormatting format, 
			object data)
		{
			if (format.BackColor != Color.Transparent)
			{
				Rectangle r = cellRect;
				r.X = column.CalculatedRect.X;
				r.Width = column.CalculatedRect.Width;
				SolidBrush brush = new SolidBrush(format.BackColor);
				dc.FillRectangle(brush, r);
				brush.Dispose();
			}
			if (data != null)
			{
				cellRect = new Rectangle(cellRect.X + format.Padding.Left, cellRect.Y + format.Padding.Top, cellRect.Width - format.Padding.Left - format.Padding.Right, cellRect.Height - format.Padding.Top - format.Padding.Bottom);
                //dc.DrawRectangle(Pens.Black, cellRect);

                Color color = format.ForeColor;
				if (Owner.FocusedNode == node && Application.RenderWithVisualStyles  == false)
					color = SystemColors.HighlightText;
				TextFormatFlags flags= TextFormatFlags.EndEllipsis | format.GetFormattingFlags();
				TextRenderer.DrawText(dc, data.ToString(), Owner.Font, cellRect, color, flags);
			}
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="glyphRect"></param>
        /// <param name="node"></param>
		public virtual void PaintCellPlusMinus(Graphics dc, Rectangle glyphRect, TreeListNode node)
		{
			if (!Application.RenderWithVisualStyles)
			{
				return;
			}

			VisualStyleElement element = VisualStyleElement.TreeView.Glyph.Closed;
			if (node.Expanded)
				element = VisualStyleElement.TreeView.Glyph.Opened;

			if (VisualStyleRenderer.IsElementDefined(element))
			{
				VisualStyleRenderer renderer = new VisualStyleRenderer(element);
				renderer.DrawBackground(dc, glyphRect);
			}
		}
	}
    /// <summary>
    /// 绘制列标题
    /// </summary>
	public class ColumnHeaderPainter
    {
        #region private fields
        private Color _verticalGridLineColor = Color.Green;
        private Pen _verticalGridLinePen;
        #endregion

        #region public properties
        /// <summary>
        /// 获取或设置垂直格线的颜色
        /// </summary>
        public Color VerticalGridLineColor
        {
            get { return _verticalGridLineColor; }
            set {
                _verticalGridLineColor = value;
                _verticalGridLinePen.Color = value;
            }
        }
        #endregion

        #region constructor
        /// <summary>
        /// 
        /// </summary>
        public ColumnHeaderPainter()
        {
            _verticalGridLinePen = new Pen(VerticalGridLineColor);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="r"></param>
        public virtual void DrawHeaderFiller(Graphics dc, Rectangle r)
		{
			if (!Application.RenderWithVisualStyles)
			{
				ControlPaint.DrawButton(dc, r, ButtonState.Flat);
				return;
			}
			VisualStyleElement element = VisualStyleElement.Header.Item.Normal;
			if (VisualStyleRenderer.IsElementDefined(element))
			{
				VisualStyleRenderer renderer = new VisualStyleRenderer(element);
				renderer.DrawBackground(dc, r);
			}
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="cellRect"></param>
        /// <param name="column"></param>
        /// <param name="format"></param>
        /// <param name="isHot"></param>
		public virtual void DrawHeader(Graphics dc, Rectangle cellRect, TreeListColumn column, TextFormatting format, bool isHot)
		{
			if (!Application.RenderWithVisualStyles)
			{
				ControlPaint.DrawButton(dc, cellRect, ButtonState.Flat);
				return;
			}
			VisualStyleElement element = VisualStyleElement.Header.Item.Normal;
			if (isHot)
				element = VisualStyleElement.Header.Item.Hot;
			if (VisualStyleRenderer.IsElementDefined(element))
			{
				VisualStyleRenderer renderer = new VisualStyleRenderer(element);
				renderer.DrawBackground(dc, cellRect);

				if (format.BackColor != Color.Transparent)
				{
					SolidBrush brush = new SolidBrush(format.BackColor);
					dc.FillRectangle(brush, cellRect);
					brush.Dispose();
				}
				cellRect = new Rectangle(cellRect.X + format.Padding.Left, cellRect.Y + format.Padding.Top, cellRect.Width - format.Padding.Left - format.Padding.Right, cellRect.Height - format.Padding.Top - format.Padding.Bottom);
                //dc.DrawRectangle(Pens.Black, cellRect);

                Color color = format.ForeColor;
				TextFormatFlags flags= TextFormatFlags.EndEllipsis | format.GetFormattingFlags();
				TextRenderer.DrawText(dc, column.Caption, column.Font, cellRect, color, flags);
			}
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="dc"></param>
        /// <param name="r"></param>
        /// <param name="hScrollOffset"></param>
		public virtual void DrawVerticalGridLines(TreeListColumnCollection columns, Graphics dc, Rectangle r, int hScrollOffset)
		{
			foreach (TreeListColumn col in columns.VisibleColumns)
			{
				int rightPos = col.CalculatedRect.Right - hScrollOffset;
				if (rightPos < 0)
					continue;
				dc.DrawLine(_verticalGridLinePen, rightPos, r.Top, rightPos, r.Bottom);
			}
		}
	}
    /// <summary>
    /// 行绘制
    /// </summary>
	public class RowPainter
	{
        #region private fields
        private Color _horizontalGridLineColor = Color.Green;
        private Pen _horizontalGridLinePen;
        #endregion

        #region public properties
        /// <summary>
        /// 获取或设置水平格线的颜色
        /// </summary>
        public Color HorizontalGridLineColor
        {
            get { return _horizontalGridLineColor; }
            set
            {
                _horizontalGridLineColor = value;
                _horizontalGridLinePen.Color = value;
            }
        }
        #endregion

        #region constructor
        /// <summary>
        /// 无参初始化PowPainter对象
        /// </summary>
        public RowPainter()
        {
            _horizontalGridLinePen = new Pen(_horizontalGridLineColor);
        }
        #endregion

        /// <summary>
        /// 绘制行标题
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="r"></param>
        /// <param name="isHot"></param>
        public void DrawHeader(Graphics dc, Rectangle r, bool isHot)
		{
			if (!Application.RenderWithVisualStyles)
			{
				ControlPaint.DrawButton(dc, r, ButtonState.Flat);
				return;
			}

			VisualStyleElement element = VisualStyleElement.Header.Item.Normal;
			if (isHot)
				element = VisualStyleElement.Header.Item.Hot;
			if (VisualStyleRenderer.IsElementDefined(element))
			{
				VisualStyleRenderer renderer = new VisualStyleRenderer(element);
				renderer.DrawBackground(dc, r);
			}
		}
        /// <summary>
        /// 绘制水平间隔线
        /// </summary>
        /// <param name="dc"></param>
        /// <param name="r"></param>
		public void DrawHorizontalGridLine(Graphics dc, Rectangle r)
		{
			dc.DrawLine(_horizontalGridLinePen, r.Left, r.Bottom, r.Right, r.Bottom);
		}
	}
}
