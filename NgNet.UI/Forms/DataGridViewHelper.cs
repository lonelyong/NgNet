using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace NgNet.UI.Forms
{
    public class DataGridViewHelper
    {
        #region private fields

        #endregion

        #region constructor

        #endregion

        #region private methods
        private static void dataGridview_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridView _dataGridView = sender as DataGridView;
            if (e.RowIndex >= _dataGridView.FirstDisplayedScrollingRowIndex
                && e.RowIndex < _dataGridView.FirstDisplayedScrollingRowIndex + _dataGridView.DisplayedRowCount(true))
                using (SolidBrush b = new SolidBrush(e.InheritedRowStyle.ForeColor))
                {
                    string _index = (e.RowIndex + 1).ToString();
                    e.Graphics.DrawString(_index, e.InheritedRowStyle.Font, b, e.RowBounds.Left, e.RowBounds.Top + ((e.RowBounds.Height - e.InheritedRowStyle.Font.Height) >> 1));
                }
        }
        #endregion

        #region public methods
        /// <summary>
        /// 设置序号列
        /// </summary>
        /// <param name="dataGridView">列所在的DataGridView</param>
        /// <param name="column">要设置的列</param>
        public static void SetIndexColumn(DataGridView dataGridView, DataGridViewColumn column)
        {
            dataGridView.RowPostPaint -= new DataGridViewRowPostPaintEventHandler(dataGridview_RowPostPaint);
            dataGridView.RowPostPaint += new DataGridViewRowPostPaintEventHandler(dataGridview_RowPostPaint);
        }
        /// <summary>
        /// 将DataGridView列集合转换为数组集合
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        public static DataGridViewColumn[] Columns2Array(DataGridView dataGridView)
        {
            var _query = from DataGridViewColumn item in dataGridView.Columns select item;
            return _query.ToArray();
        }
        /// <summary>
        /// 将DataGridView可见列集合转换为数组集合
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        public static DataGridViewColumn[] VisibleColumns2Array(DataGridView dataGridView)
        {
            var _query = from DataGridViewColumn item in dataGridView.Columns where item.Visible select item;
            return _query.ToArray();
        }
        #endregion
    }
}
