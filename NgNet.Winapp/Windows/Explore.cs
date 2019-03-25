namespace NgNet.Windows
{
    public class Explore
    {
        #region private fields
        private const int SW_SHOW = 5;
        private const uint SEE_MASK_INVOKEIDLIST = 12;
        #endregion

        #region proptected fields

        #endregion

        #region public properties

        #endregion

        #region constructor
        public Explore()
        {

        }
        #endregion

        #region private methods

        #endregion

        #region public methods
        /// <summary>
        /// 显示路径指定文件（夹）的属性
        /// </summary>
        /// <param name="path"></param>
        public static void ShowPropertiesDialog(string path)
        {
            Apis.Shell32.SHELLEXECUTEINFO info = new Apis.Shell32.SHELLEXECUTEINFO();
            info.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = path;
            info.nShow = SW_SHOW;
            info.fMask = SEE_MASK_INVOKEIDLIST;
            Apis.Shell32.ShellExecuteEx(ref info);
        }
        #endregion
    }
}
