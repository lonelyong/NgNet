namespace NgNet.Windows
{
    public class Current
    {
        #region private fields

        #endregion

        #region proptected fields

        #endregion

        #region public properties

        #endregion

        #region constructor
        public Current()
        {

        }
        #endregion

        #region private methods

        #endregion

        #region public methods
        /// <summary>
        /// 退出Windows,如果需要,申请相应权限
        /// </summary>
        /// <param name="how">重启选项,指示如何退出Windows</param>
        /// <param name="force">True表示强制退出</param>
        /// <exception cref="PrivilegeException">当申请权限时发生了一个错误</exception>
        /// <exception cref="PlatformNotSupportedException">系统不支持则引发异常</exception>
        public static void ExitWindows(RestartOptions how, bool force)
        {
            Restart.ExitWindows(how, force);
        }
        #endregion
    }
}
