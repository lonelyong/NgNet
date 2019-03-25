using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Data;
using NgNet;
using System.ComponentModel;
using System.Drawing;

namespace NgNet.UI.Forms
{
    /// <summary>
    /// ComboBox帮助类
    /// </summary>
    public class ComboBoxHelper
    {
        #region private fields
        private ComboBox _combo;

        private Rectangle _rect;//声明一个表示矩形的位置和大小类的对象
        private SolidBrush _sbBack = new SolidBrush(Color.Empty);
        private SolidBrush _sbBorder = new SolidBrush(Color.Empty);
        private SolidBrush _sbFore = new SolidBrush(Color.Empty);
        private Pen _penBorder = new Pen(Color.Empty, 1);

        private IThemeBase _theme = UI.Theme.Default;

        private bool _delegateDrawItem = false;

        private bool _dropdownAutoWidth = false;
        #endregion

        #region public properties
        /// <summary>
        /// 获取或设置主题
        /// </summary>
        public IThemeBase Theme
        {
            get { return _theme; }
            set
            {
                _theme = value;
                _combo.BackColor = Theme.BackColor;
                _combo.ForeColor = Theme.ForeColor;
                _combo.Refresh();
            }
        }
        /// <summary>
        /// 是否委托绘制项
        /// </summary>
        public bool DelegateDrawItem
        {
            get
            {
                return _delegateDrawItem;
            }
            set
            {
                _delegateDrawItem = value;
                if (value)
                {
                    _combo.DrawItem -= DrawItem;
                    _combo.DrawItem += DrawItem;
                    _combo.DrawMode = DrawMode.OwnerDrawFixed;
                }
                else
                {
                    _combo.DrawItem -= DrawItem;
                    _combo.DrawMode = DrawMode.Normal;
                }
            }

        }
        /// <summary>
        /// 下拉框自适应内容宽度
        /// </summary>
        public bool DropdownAutoWidth
        {
            get
            {
                return _dropdownAutoWidth;
            }
            set
            {
                _dropdownAutoWidth = value;
                if (value)
                {
                    _combo.DropDown -= combo_DropDown;
                    _combo.DropDown += combo_DropDown;
                }
                else
                {
                    _combo.DropDown -= combo_DropDown;
                }
            }
        }
        #endregion

        #region constructor

        /// <summary>
        /// 用指定的ComboBox实例化ComboBox帮助类
        /// </summary>
        /// <param name="cb"></param>
        public ComboBoxHelper(ComboBox cb)
        {
            if (cb == null)
                throw new Exception($"参数{cb}不能为NULL");
            _combo = cb;
            DelegateDrawItem = true;
            DropdownAutoWidth = true;
        }
        #endregion

        #region private methods
        private void combo_DropDown(object sender, EventArgs e)
        {
            Graphics g = null;
            Font font = null;
            try
            {
                int _width = _combo.Width;
                g = _combo.CreateGraphics();
                font = _combo.Font;

                //checks if a scrollbar will be displayed.
                //If yes, then get its width to adjust the size of the drop down list.
                int vertScrollBarWidth = (_combo.Items.Count > _combo.MaxDropDownItems) ? SystemInformation.VerticalScrollBarWidth : 0;

                int newWidth = _width;
                foreach (object s in _combo.Items)  //Loop through list items and check size of each items.
                {
                    if (s != null)
                    {
                        newWidth = (int)g.MeasureString(_combo.GetItemText(s), font).Width + vertScrollBarWidth;
                        if (_width < newWidth)
                            _width = newWidth;   //set the width of the drop down list to the width of the largest item.
                    }
                }
                _combo.DropDownWidth = _width;
            }
            catch
            { }
            finally
            {
                if (g != null)
                    g.Dispose();
            }
        }
        #endregion

        #region protected methods
        /// <summary>
        /// 自主绘制项外观
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void DrawItem(object sender, DrawItemEventArgs e)
        {
            //Graphics gComboBox = e.Graphics;//声明一个GDI+绘图图面类的对象
            _rect = e.Bounds;//声明一个表示矩形的位置和大小类的对象
            _sbBack.Color = Theme.BackColor;
            _sbBorder.Color = Theme.BorderColor;
            _sbFore.Color = Theme.ForeColor;
            _penBorder.Color = Theme.BorderColor;
            //Size imageSize = imageList1.ImageSize;//声明一个有序整数对的对象
            if (e.Index >= 0)//当绘制的索引项存在时
            {
                string _itemString = _combo.GetItemText(_combo.Items[e.Index]);//获取ComboBox控件索引项下的文本内容
                StringFormat stringFormat = new StringFormat();//定义一个封装文本布局信息类的对象
                stringFormat.Alignment = StringAlignment.Near;//设定文本的布局方式
                stringFormat.LineAlignment = StringAlignment.Center;

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected )//当绘制项有键盘加速键或者焦点可视化提示时
                {
                    e.Graphics.DrawRectangle(_penBorder, _rect.X, _rect.Y, _rect.Width - 1, _rect.Height - 1);
                    //e.Graphics.FillRectangle(_sb1, _rect);//用指定的颜色填充自定义矩形的内部
                    //imageList1.Draw(e.Graphics, rComboBox.Left, rComboBox.Top, e.Index);//在指定位置绘制指定索引的图片
                    e.Graphics.DrawString(_itemString, _combo.Font, _sbFore, _rect.Left /* + imageSize.Width*/, _rect.Top);//在指定的位置并且用指定的Font对象绘制指定的文本字符串
                    e.DrawFocusRectangle();//在指定的边界范围内绘制聚焦框
                }
                else//当绘制项没有键盘加速键和焦点可视化提示时
                {
                    e.Graphics.FillRectangle(_sbBack, _rect);//用指定的颜色填充自定义矩形的内部
                                                             //imageList1.Draw(e.Graphics, rComboBox.Left, rComboBox.Top, e.Index);//在指定位置绘制指定索引的图片
                    e.Graphics.DrawString(_itemString, _combo.Font, _sbFore, _rect.Left/* + imageSize.Width */, _rect.Top);//在指定的位置并且用指定的Font对象绘制指定的文本字符串
                    e.DrawFocusRectangle();//在指定的边界范围内绘制聚焦框
                }
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// 将指定的枚举类型添加到指定的ComboBox，此举会清空现有项
        /// </summary>
        /// <param name="enumType"></param>
        public void SetEnum(Type enumType)
        {
            SetEnum(_combo, enumType);
        }
        /// <summary>
        /// 将指定的枚举类型添加到指定的ComboBox，此举会清空现有项
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="nullDescription"></param>
        public void SetEnumNullable(Type enumType, string nullDescription)
        {
            SetEnumNullable(_combo, enumType, nullDescription);
        }

        /// <summary>
        /// 将指定的枚举集合添加到
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="enumArray"></param>
        public void SetEnumArray<TEnum>(IEnumerable<TEnum> enumArray) where TEnum : IComparable, new()
        {
            SetEnumArray(_combo, enumArray);
        }

        /// <summary>
        ///  将相同枚举不同项或项组合添加到组合框并在添加一个值为NULL的首项
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumArray"></param>
        /// <param name="nullDescription"></param>
        public void SetEnumArrayNullable<TEnum>(IEnumerable<TEnum> enumArray, string nullDescription) where TEnum : IComparable, new()
        {
            SetEnumArrayNullable(_combo, enumArray, nullDescription);
        }

        /// <summary>
        ///  指定TRUE和FALSE值的描述，将TRUE,FALSE添加到ComboBox
        /// </summary>
        /// <param name="trueDescription"></param>
        /// <param name="falseDescription"></param>
        public void SetBool(string trueDescription, string falseDescription)
        {
            SetBool(_combo, trueDescription, falseDescription);
        }
        #endregion

        #region static
        #region public methods
        /// <summary>
        /// 将指定的枚举类型添加到指定的ComboBox，此举会清空现有项
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="enumType"></param>
        public static void SetEnum(ComboBox combo, Type enumType)
        {
            combo.BeginUpdate();
            DataTable _dt =EnumHelper.GetEnumDescriptions(enumType);
            combo.DataSource = _dt;
            combo.ValueMember = EnumHelper.VALUE;
            combo.DisplayMember = EnumHelper.DESCRIPTION;
            combo.EndUpdate();
        }
        /// <summary>
        /// 将指定的枚举类型添加到指定的ComboBox，此举会清空现有项，并在首项添加一个空项
        /// </summary>
        /// <param name="combo"></param>
        /// <param name="enumType"></param>
        /// <param name="nullDescription"></param>
        public static void SetEnumNullable(ComboBox combo, Type enumType, string nullDescription)
        {
            DataTable _dt = EnumHelper.GetEnumDescriptions(enumType);
            DataRow _dr = _dt.NewRow();
            _dr[EnumHelper.DESCRIPTION] = nullDescription;
            _dr[EnumHelper.VALUE] = DBNull.Value;
            _dt.Rows.InsertAt(_dr, 0);
            combo.BeginUpdate();
            combo.DataSource = _dt;
            combo.ValueMember = EnumHelper.VALUE;
            combo.DisplayMember = EnumHelper.DESCRIPTION;
            combo.EndUpdate();
        }

        /// <summary>
        /// 将相同枚举不同项或项组合添加到组合框
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="combo"></param>
        /// <param name="enumArray"></param>
        public static void SetEnumArray<TEnum>(ComboBox combo, IEnumerable<TEnum> enumArray) where TEnum : IComparable, new ()
        {
            DataTable _dt = EnumHelper.GetEnumArrayDescriptions(enumArray);          
            combo.BeginUpdate();
            combo.DataSource = _dt;
            combo.ValueMember = EnumHelper.VALUE;
            combo.DisplayMember = EnumHelper.DESCRIPTION;
            combo.EndUpdate();
        }
        /// <summary>
        /// 将相同枚举不同项或项组合添加到组合框并在添加一个值为NULL的首项
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="combo"></param>
        /// <param name="enumArray"></param>
        public static void SetEnumArrayNullable<TEnum>(ComboBox combo, IEnumerable<TEnum> enumArray, string nullDescription) where TEnum:IComparable, new ()
        {
            DataTable _dt = EnumHelper.GetEnumArrayDescriptions(enumArray);
            DataRow _dr = _dt.NewRow();
            _dr[EnumHelper.DESCRIPTION] = nullDescription;
            _dr[EnumHelper.VALUE] = DBNull.Value;
            _dt.Rows.InsertAt(_dr, 0);
            combo.BeginUpdate();
            combo.DataSource = _dt;
            combo.ValueMember = EnumHelper.VALUE;
            combo.DisplayMember = EnumHelper.DESCRIPTION;
            combo.EndUpdate();
        }

        public static void SetBool(ComboBox combo, string trueDescription, string falseDescription)
        {
            DataTable _dt = new DataTable();
            DataColumn _dc = new DataColumn();
            _dc.ColumnName = EnumHelper.DESCRIPTION;
            _dt.Columns.Add(_dc);
            _dc = new DataColumn();
            _dc.ColumnName = EnumHelper.VALUE;
            _dt.Columns.Add(_dc);

            DataRow _dr = _dt.NewRow();
            _dr.ItemArray = new object[] { trueDescription, true };
            _dt.Rows.Add(_dr);
            _dr = _dt.NewRow();
            _dr.ItemArray = new object[] { falseDescription, false };
            _dt.Rows.Add(_dr);

            combo.DataSource = _dt;
            combo.DisplayMember = EnumHelper.DESCRIPTION;
            combo.ValueMember = EnumHelper.VALUE;
        }
        #endregion
        #endregion
    }
}
