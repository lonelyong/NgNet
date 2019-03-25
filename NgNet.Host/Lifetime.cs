namespace NgNet.Host
{
    public enum Lifetime
    {
        /// <summary>
        /// 每次请求都创建一个实例
        /// </summary>
        Transient = 1,
        /// <summary>
        /// 
        /// </summary>
        Scoped = 2,
        Singleton = 3
    }
}
