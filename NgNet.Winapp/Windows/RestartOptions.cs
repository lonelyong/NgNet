using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Windows
{
    /// <summary>
    /// 枚举类型,指定可以允许的重启操作
    /// </summary>
    public enum RestartOptions
    {
        /// <summary>
        /// Shuts down all processes running in the security context of the process that called the ExitWindowsEx function. Then it logs the user off.
        /// 注销，关闭调用ExitWindowsEx()功能的进程安全上下文中所有运行的程序，然后用户退出登录
        /// </summary>
        [Description("注销")]
        LogOff = 0,
        /// <summary>
        /// Shuts down the system and turns off the power. The system must support the power-off feature.
        /// 关闭操作系统和电源，计算机必须支持软件控制电源
        /// </summary>
        [Description( "关机并关闭电源" )]
        PowerOff = 8,
        /// <summary>
        /// Shuts down the system and then restarts the system.
        /// 关闭系统然后重启
        /// </summary>
        [Description( "重启" )]
        Reboot = 2,
        /// <summary>
        /// Shuts down the system to a point at which it is safe to turn off the power. All file buffers have been flushed to disk, and all running processes have stopped. If the system supports the power-off feature, the power is also turned off.
        /// 关闭系统，等待合适的时刻关闭电源：当所有文件的缓存区被写入磁盘，所有运行的进程停止，如果系统支持软件控制电源，就关闭电源
        /// </summary>
        [Description( "关机" )]
        ShutDown = 1,
        /// <summary>
        /// Suspends the system.
        /// 挂起
        /// </summary>
        [Description( "挂起" )]
        Suspend = -1,
        /// <summary>
        /// Hibernates the system.
        /// 休眠
        /// </summary>
        [Description( "休眠" )]
        Hibernate = -2,
        /// <summary>
        /// 锁定
        /// </summary>
        [Description( "锁定" )]
        Lock = 4,
        /// <summary>
        /// 睡眠
        /// </summary>
        [Description( "睡眠" )]
        Sleep = -4
    }
}
