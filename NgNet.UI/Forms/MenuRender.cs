using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace NgNet.UI.Forms
{
    public class MenuRender : ToolStripProfessionalRenderer
    {
        #region public properties
        /// <summary>
        /// 获取或设置一个值，该值指示是否绘制边框
        /// </summary>
        public bool DrawBorder { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示是否绘制选中项边框
        /// </summary>
        public bool DrawSelectedItemBorder { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示边框的宽度
        /// </summary>
        public int BorderWidth { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示背景渐变色的方向
        /// </summary>
        public float GradualAngle { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示选中项背景渐变色的方向
        /// </summary>
        public float SelectedItemGradualAngle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public MenuRenderColors Colors { get; set; }
        #endregion

        #region private fileds
        private int MarginWidth = 0;
        private Blend blendToolstripBackground = new Blend();
        private float[] posiToolStrip = new float[5] { 0f, 0.2f, 0.5f, 0.7f, 1f };
        private float[] factorToolStrip = new float[5] { 0f, 0.6f, 1f, 0.6f, 0f };

        private Blend blendImageMargin = new Blend();
        private float[] posiImageMargin = new float[5] { 0f, 0.1f, 0.2f, 0.9f, 1f };
        private float[] factorImageMargin = new float[5] { 0f, 0.1f, 0.2f, 0.9f, 1f };

        private Blend blendMenuItemBackground = new Blend();
        private float[] posiMenuitem = new float[5] { 0f, 0.3f, 0.5f, 0.8f, 1f };
        private float[] factorMenuitem = new float[5] { 0f, 0.5f, 1f, 0.5f, 0f };

        private SolidBrush solidBrush = new SolidBrush(Color.AliceBlue);
        private Rectangle rect = new Rectangle();
        private Pen pen = new Pen(Color.AliceBlue);
        private Point p0 = new Point(0, 0);//左上
        private Point p1 = new Point(0, 0);//右上
        private Point p2 = new Point(0, 0);//左下
        private Point p3 = new Point(0, 0);//右下 
        private Point p4 = new Point(0, 0);//重合左
        private Point p5 = new Point(0, 0);//重合右
        #endregion

        #region constructor  
        /// <summary>
        /// 菜单样式
        /// </summary>
        /// <param name="mrc">管理颜色的类</param>
        public MenuRender(MenuRenderColors mrc)
        {
            init();
            Colors = mrc;
        }

        /// <summary>
        /// 以默认的样式创建新Render实例
        /// </summary>
        public MenuRender() : this(new MenuRenderColors())
        {
        }

        private void init()
        {
            blendToolstripBackground.Positions = posiToolStrip;
            blendToolstripBackground.Factors = factorToolStrip;

            blendMenuItemBackground.Positions = posiMenuitem;
            blendMenuItemBackground.Factors = factorMenuitem;

            blendImageMargin.Positions = posiImageMargin;
            blendImageMargin.Factors = factorImageMargin;

            DrawBorder = true;
            DrawSelectedItemBorder = true;
            BorderWidth = 1;
            GradualAngle = 45f;
            SelectedItemGradualAngle = 135;
        }
        #endregion

        #region override
        /// <summary>
        /// 渲染整个背景
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            solidBrush.Color = Colors.DropDownBackStartColor;
            //如果是下拉
            if (e.ToolStrip is ToolStripDropDown)
            {
                FillLineGradient(e.Graphics, e.AffectedBounds, Colors.DropDownBackStartColor, Colors.DropDownBackEndColor, GradualAngle, blendToolstripBackground);
            }
            //如果是菜单项
            else if (e.ToolStrip is MenuStrip)
            {
                FillLineGradient(e.Graphics, e.AffectedBounds, Colors.MainMenuStartColor, Colors.MainMenuEndColor, GradualAngle, blendToolstripBackground);
            }
            else
            {
                base.OnRenderToolStripBackground(e);
            }
        }
        /// <summary>
        /// 渲染下拉左侧图标区域
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            this.MarginWidth = e.AffectedBounds.Width;
            pen.Color = Colors.DropDownItemBorderColor;
            FillLineGradient(e.Graphics, e.AffectedBounds, Colors.MarginStartColor, Colors.MarginEndColor, SelectedItemGradualAngle, blendImageMargin);
        }
        /// <summary>
        /// 渲染菜单项的背景
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.ToolStrip is MenuStrip)
            {
                //如果被选中或被按下
                if (e.Item.Selected || e.Item.Pressed)
                {
                    rect.X = 0;
                    rect.Y = 0;
                    rect.Width = e.Item.Width;
                    rect.Height = e.Item.Height;
                    FillLineGradient(e.Graphics, rect, Colors.MenuItemStartColor, Colors.MenuItemEndColor, SelectedItemGradualAngle, blendMenuItemBackground);

                    if (DrawSelectedItemBorder)
                    {
                        pen.Color = Colors.MenuItemBorderColor;
                        //Point p0 = new Point(0, 0);//左上
                        p1.X = e.Item.Width - 1;//右上
                        p2.Y = e.Item.Height - 1;//左下
                        p3.X = p1.X;//右下
                        p3.Y = p2.Y;
                        e.Graphics.DrawLine(pen, p0, p1);
                        e.Graphics.DrawLine(pen, p0, p2);
                        e.Graphics.DrawLine(pen, p1, p3);
                        if (e.Item.Selected)
                            e.Graphics.DrawLine(pen, p2, p3);
                    }
                }
                else
                    base.OnRenderMenuItemBackground(e);
            }
            else if (e.ToolStrip is ToolStripDropDown)
            {
                if (e.Item.Selected)
                {
                    rect.X = 0;
                    rect.Y = 0;
                    rect.Width = e.Item.Size.Width;
                    rect.Height = e.Item.Size.Height;
                    FillLineGradient(e.Graphics, rect, Colors.DropDownItemStartColor, Colors.DropDownItemEndColor, SelectedItemGradualAngle, blendToolstripBackground);
                    if (DrawSelectedItemBorder)
                    {
                        pen.Color = Colors.DropDownItemBorderColor;
                        rect.X = 2;
                        rect.Y = 0;
                        rect.Width = e.Item.Width - 4;
                        rect.Height = e.Item.Height - 1;
                        e.Graphics.DrawRectangle(pen, rect);
                    }
                }
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }
        /// <summary>
        /// 渲染菜单项的分隔线
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            e.Graphics.DrawLine(new Pen(Colors.SeparatorColor), this.MarginWidth, 2, e.Item.Width, 2);
        }
        /// <summary>
        /// 渲染边框
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            if (!DrawBorder) return;
            if (e.ToolStrip is ToolStripDropDown)
            {
                pen.Color = Colors.DropDownBorderColor;
                //p0 = new Point(0, 0);//左上
                p1.X = e.AffectedBounds.Width - 1;//右上 
                p2.Y = e.AffectedBounds.Height - 1;//左下
                p3.X = e.AffectedBounds.Width - 1;//右下
                p3.Y = e.AffectedBounds.Height - 1;
                p4.X = e.ConnectedArea.Left - 1;//重合左
                p5.X = e.ConnectedArea.Right;//重合右

                e.Graphics.DrawLine(pen, p0, p2);//left
                e.Graphics.DrawLine(pen, p1, p3);//right
                e.Graphics.DrawLine(pen, p2, p3);//bottom

                e.Graphics.DrawLine(pen, p5, p1);//top-right
                e.Graphics.DrawLine(pen, p0, p4);//top-left
            }
            else if (e.ToolStrip is MenuStrip)
            {
                pen.Color = Colors.MainMenuBorderColor;
            }
            else
            {
                base.OnRenderToolStripBorder(e);
            }
        }
        /// <summary>
        /// 渲染箭头
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            e.ArrowColor = Colors.ArrowColor;//设置为红色，当然还可以 画出各种形状
            base.OnRenderArrow(e);
        }
        /// <summary>
        /// 字体颜色
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = Colors.FontColor;
            base.OnRenderItemText(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderItemCheck(ToolStripItemImageRenderEventArgs e)
        {
            base.OnRenderItemCheck(e);
        }
        /// <summary>
        /// 填充线性渐变
        /// </summary>
        /// <param name="g">画布</param>
        /// <param name="rect">填充区域</param>
        /// <param name="startcolor">开始颜色</param>
        /// <param name="endcolor">结束颜色</param>
        /// <param name="angle">角度</param>
        /// <param name="blend">对象的混合图案</param>
        private void FillLineGradient(Graphics g, Rectangle rect, Color startcolor, Color endcolor, float angle, Blend blend)
        {
            LinearGradientBrush linebrush = new LinearGradientBrush(rect, startcolor, endcolor, angle);
            if (blend != null)
            {
                linebrush.Blend = blend;
            }
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(rect);
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.FillPath(linebrush, path);
        }
        #endregion;
    }
}
