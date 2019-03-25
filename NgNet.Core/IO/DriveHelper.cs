
namespace NgNet.IO
{
    #region <class - hDrive>
    public class DriveHelper
    {
        public static string GetDriveTypeDescription(System.IO.DriveType driveType)
        {
            string dt = string.Empty;
            switch (driveType)
            {
                case System.IO.DriveType.CDRom:
                    dt = "只读光盘";
                    break;
                case System.IO.DriveType.Fixed:
                    dt = "本地磁盘";
                    break;
                case System.IO.DriveType.Network:
                    dt = "网络磁盘";
                    break;
                case System.IO.DriveType.NoRootDirectory:
                    dt = "<错误>无效根路径";
                    break;
                case System.IO.DriveType.Ram:
                    dt = "随机存取内存";
                    break;
                case System.IO.DriveType.Removable:
                    dt = "可移动磁盘";
                    break;
                case System.IO.DriveType.Unknown:
                    dt = "未知类型";
                    break;
                default:
                    dt = "未知类型";
                    break;
            }
            return dt;
        }
    }
    #endregion
}
