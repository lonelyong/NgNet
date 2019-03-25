
namespace NgNet.IO
{
    #region enum
    /// <summary>
    /// 文件长度的单位
    /// </summary>
    public enum LengthUnit : int 
    {
        /// <summary>
        /// 字节
        /// </summary>
        B = 0,
        /// <summary>
        /// 千字节
        /// </summary>
        KB = 1,
        /// <summary>
        /// 百万字节
        /// </summary>
        MB = 2,
        /// <summary>
        /// 十亿字节
        /// </summary>
        GB = 3,
        /// <summary>
        /// 万亿字节
        /// </summary>
        TB = 4 };

    public enum PathType : int
    {
        Floder = 0,
        File = 1,
        Unknown = -1
    }
    #endregion
}
