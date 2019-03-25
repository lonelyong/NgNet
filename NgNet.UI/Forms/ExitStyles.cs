namespace NgNet.UI.Forms
{
    public enum ExitStyles : int
    {
        /// <summary>
        /// 直接执行关闭，不进行选择
        /// </summary>
        ExitDirectly = 0,
        /// <summary>
        /// 直接执行最小化，不进行选择
        /// </summary>
        MinsizeDirectly = 1,
        /// <summary>
        /// 选择是关闭还是最小化，默认选中关闭
        /// </summary>
        ExitChoose = 2,
        /// <summary>
        /// 选择是关闭还是最小化，默认选中最小化
        /// </summary>
        MinsizeChoose = 4,
        /// <summary>
        /// 操作取消
        /// </summary>
        None = 8
    }
}
