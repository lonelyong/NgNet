using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace NgNet.UI.Forms
{
    [Description("This is the columns collection")]
    //[TypeConverterAttribute(typeof(ColumnsTypeConverter))]
    [Editor(typeof(ColumnCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
    public class TreeListColumnCollection : IList<TreeListColumn>, IList
    {
        ColumnHeaderPainter m_painter = new ColumnHeaderPainter();
        CollumnSetting m_options;
        TreeList m_owner;
        List<TreeListColumn> m_columns = new List<TreeListColumn>();
        List<TreeListColumn> m_visibleCols = new List<TreeListColumn>();
        Dictionary<string, TreeListColumn> m_columnMap = new Dictionary<string, TreeListColumn>();

        [Browsable(false)]
        public CollumnSetting Options
        {
            get { return m_options; }
        }
        [Browsable(false)]
        public ColumnHeaderPainter Painter
        {
            get { return m_painter; }
            set { m_painter = value; }
        }
        [Browsable(false)]
        public TreeList Owner
        {
            get { return m_owner; }
        }
        [Browsable(false)]
        public Font Font
        {
            get { return m_owner.Font; }
        }
        [Browsable(false)]
        public TreeListColumn[] VisibleColumns
        {
            get { return m_visibleCols.ToArray(); }
        }
        [Browsable(false)]
        public int ColumnsWidth
        {
            get
            {
                int width = 0;
                foreach (TreeListColumn col in m_visibleCols)
                {
                    if (col.AutoSize)
                        width += col.CalculatedAutoSize;
                    else
                        width += col.Width;
                }
                return width;
            }
        }
        public TreeListColumnCollection(TreeList owner)
        {
            m_owner = owner;
            m_options = new CollumnSetting(owner);
        }
        public TreeListColumn this[int index]
        {
            get
            {
                return m_columns[index];
            }
            set
            {
                m_columns[index] = value;
            }
        }
        public TreeListColumn this[string fieldname]
        {
            get
            {
                TreeListColumn col;
                m_columnMap.TryGetValue(fieldname, out col);
                return col;
            }
        }
        public void SetVisibleIndex(TreeListColumn col, int index)
        {
            m_visibleCols.Remove(col);
            if (index >= 0)
            {
                if (index < m_visibleCols.Count)
                    m_visibleCols.Insert(index, col);
                else
                    m_visibleCols.Add(col);
            }
            RecalcVisibleColumsRect();
        }
        public HitInfo CalcHitInfo(Point point, int horzOffset)
        {
            HitInfo info = new HitInfo();
            info.Column = CalcHitColumn(point, horzOffset);
            if ((info.Column != null) && (point.Y < Options.HeaderHeight))
            {
                info.HitType |= HitInfo.eHitType.kColumnHeader;
                int right = info.Column.CalculatedRect.Right - horzOffset;
                if (info.Column.AutoSize == false)
                {
                    if (point.X >= right - 4 && point.X <= right)
                        info.HitType |= HitInfo.eHitType.kColumnHeaderResize;
                }
            }
            return info;
        }
        public TreeListColumn CalcHitColumn(Point point, int horzOffset)
        {
            if (point.X < Options.LeftMargin)
                return null;
            foreach (TreeListColumn col in m_visibleCols)
            {
                int left = col.CalculatedRect.Left - horzOffset;
                int right = col.CalculatedRect.Right - horzOffset;
                if (point.X >= left && point.X <= right)
                    return col;
            }
            return null;
        }
        public void RecalcVisibleColumsRect()
        {
            RecalcVisibleColumsRect(false);
        }
        public void RecalcVisibleColumsRect(bool isDoingColumnResizing)
        {
            if (IsInitializing)
                return;
            int x = 0;//m_leftMargin;
            if (m_owner.RowOptions.ShowHeader)
                x = m_owner.RowOptions.HeaderWidth;
            int y = 0;
            int h = Options.HeaderHeight;
            int index = 0;
            foreach (TreeListColumn col in m_columns)
            {
                col.internalVisibleIndex = -1;
                col.internalIndex = index++;
            }

            // calculate size requierd by fix columns and auto adjusted columns
            // at the same time calculate total ratio value
            int widthFixedColumns = 0;
            int widthAutoSizeColumns = 0;
            float totalRatio = 0;
            foreach (TreeListColumn col in m_visibleCols)
            {
                if (col.AutoSize)
                {
                    widthAutoSizeColumns += col.AutoSizeMinSize;
                    totalRatio += col.AutoSizeRatio;
                }
                else
                    widthFixedColumns += col.Width;
            }
            /*
			CommonTools.Tracing.WriteLine(0, "\nRecalcVisibleColumsRect");
			CommonTools.Tracing.WriteLine(0, "WidthRequired, auto {0}, fixed {1}, total {2}", 
				widthAutoSizeColumns,
				widthFixedColumns,
				widthAutoSizeColumns + widthFixedColumns);
			*/
            int clientWidth = m_owner.ClientRectangle.Width - m_owner.RowHeaderWidth();
            // find ratio 'unit' value
            float remainingWidth = clientWidth - (widthFixedColumns + widthAutoSizeColumns);
            float ratioUnit = 0;
            if (totalRatio > 0 && remainingWidth > 0)
                ratioUnit = remainingWidth / totalRatio;
            /*
			CommonTools.Tracing.WriteLine(0, "ClientWidth {0}", clientWidth);
			CommonTools.Tracing.WriteLine(0, "RemainngWidth {0}, TotalRatio {1},  RatioUnits {2}", 
				remainingWidth,
				totalRatio,
				ratioUnit);
			*/
            for (index = 0; index < m_visibleCols.Count; index++)
            {
                TreeListColumn col = m_visibleCols[index];
                int width = col.Width;
                if (col.AutoSize)
                {
                    // if doing column resizing then keep adjustable columns fixed at last width
                    if (isDoingColumnResizing)
                        width = col.CalculatedAutoSize;
                    else
                        width = col.AutoSizeMinSize + (int)(ratioUnit * col.AutoSizeRatio) - 1;
                    col.CalculatedAutoSize = width;
                }
                col.internalCalculatedRect = new Rectangle(x, y, width, h);
                col.internalVisibleIndex = index;
                x += width;
            }
            Invalidate();
        }
        public void Draw(Graphics dc, Rectangle rect, int horzOffset)
        {
            foreach (TreeListColumn col in m_visibleCols)
            {
                Rectangle r = col.CalculatedRect;
                r.X -= horzOffset;
                if (r.Left > rect.Right)
                    break;
                col.Draw(dc, m_painter, r);
            }
            // drwa row header filler
            if (m_owner.RowOptions.ShowHeader)
            {
                Rectangle r = new Rectangle(0, 0, m_owner.RowOptions.HeaderWidth, Options.HeaderHeight);
                m_painter.DrawHeaderFiller(dc, r);
            }
        }
        public void AddRange(IEnumerable<TreeListColumn> columns)
        {
            foreach (TreeListColumn col in columns)
                Add(col);
        }
        /// <summary>
        /// AddRange(Item[]) is required for the designer.
        /// </summary>
        /// <param name="columns"></param>
        public void AddRange(TreeListColumn[] columns)
        {
            foreach (TreeListColumn col in columns)
                Add(col);
        }
        public void Add(TreeListColumn item)
        {
            bool designmode = Owner.DesignMode;
            if (!designmode)
            {
                Debug.Assert(m_columnMap.ContainsKey(item.Fieldname) == false);
                Debug.Assert(item.Owner == null, "column.Owner == null");
            }
            else
            {
                m_columns.Remove(item);
                m_visibleCols.Remove(item);
            }

            item.Owner = this;
            m_columns.Add(item);
            m_visibleCols.Add(item);
            m_columnMap[item.Fieldname] = item;
            RecalcVisibleColumsRect();
            //return item;
        }
        public void Clear()
        {
            m_columnMap.Clear();
            m_columns.Clear();
            m_visibleCols.Clear();
        }
        public bool Contains(TreeListColumn item)
        {
            return m_columns.Contains(item);
        }
        [Browsable(false)]
        public int Count
        {
            get { return m_columns.Count; }
        }
        [Browsable(false)]
        public bool IsReadOnly
        {
            get { return false; }
        }
        #region IList<TreeListColumn> Members

        public int IndexOf(TreeListColumn item)
        {
            return m_columns.IndexOf(item);
        }

        public void Insert(int index, TreeListColumn item)
        {
            m_columns.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            m_columns.RemoveAt(index);
        }

        #endregion
        #region ICollection<TreeListColumn> Members


        public void CopyTo(TreeListColumn[] array, int arrayIndex)
        {
            m_columns.CopyTo(array, arrayIndex);
        }

        public bool Remove(TreeListColumn item)
        {
            return m_columns.Remove(item);
        }

        #endregion
        #region IEnumerable<TreeListColumn> Members

        public IEnumerator<TreeListColumn> GetEnumerator()
        {
            return m_columns.GetEnumerator();
        }

        #endregion
        #region IEnumerable Members
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
        #region IList Members
        int IList.Add(object value)
        {
            Add((TreeListColumn)value);
            return Count - 1;
        }
        bool IList.Contains(object value)
        {
            return Contains((TreeListColumn)value);
        }
        int IList.IndexOf(object value)
        {
            return IndexOf((TreeListColumn)value);
        }
        void IList.Insert(int index, object value)
        {
            Insert(index, (TreeListColumn)value);
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        void IList.Remove(object value)
        {
            Remove((TreeListColumn)value);
        }

        object IList.this[int index]
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }
        #endregion
        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public bool IsSynchronized
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public object SyncRoot
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion

        internal bool DesignMode
        {
            get
            {
                if (m_owner != null)
                    return m_owner.DesignMode;
                return false;
            }
        }
        internal void Invalidate()
        {
            if (m_owner != null)
                m_owner.Invalidate();
        }
        bool IsInitializing = false;
        internal void BeginInit()
        {
            IsInitializing = true;
        }
        internal void EndInit()
        {
            IsInitializing = false;
            RecalcVisibleColumsRect();
        }
    }
}
