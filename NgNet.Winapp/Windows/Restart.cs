using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Windows
{
    class Restart
    {
        #region struct
        /// <summary>
        /// An LUID is a 64-bit value guaranteed to be unique only on the system on which it was generated. The uniqueness of a locally unique identifier (LUID) is guaranteed only until the system is restarted.
        /// 本地唯一标志是一个64位的数值，它被保证在产生它的系统上唯一！LUID的在机器被重启前都是唯一的
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct LUID
        {
            /// <summary>
            /// The low order part of the 64 bit value.
            /// 本地唯一标志的低32位
            /// </summary>
            public int LowPart;
            /// <summary>
            /// The high order part of the 64 bit value.
            /// 本地唯一标志的高32位
            /// </summary>
            public int HighPart;
        }
        /// <summary>
        /// The LUID_AND_ATTRIBUTES structure represents a locally unique identifier (LUID) and its attributes.
        /// LUID_AND_ATTRIBUTES 结构呈现了本地唯一标志和它的属性
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct LUID_AND_ATTRIBUTES
        {
            /// <summary>
            /// Specifies an LUID value.
            /// </summary>
            public LUID pLuid;
            /// <summary>
            /// Specifies attributes of the LUID. This value contains up to 32 one-bit flags. Its meaning is dependent on the definition and use of the LUID.
            /// 指定了LUID的属性，其值可以是一个32位大小的bit 标志，具体含义根据LUID的定义和使用来看
            /// </summary>
            public int Attributes;
        }
        /// <summary>
        /// The TOKEN_PRIVILEGES structure contains information about a set of privileges for an access token.
        /// TOKEN_PRIVILEGES 结构包含了一个访问令牌的一组权限信息：即该访问令牌具备的权限
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct TOKEN_PRIVILEGES
        {
            /// <summary>
            /// Specifies the number of entries in the Privileges array.
            /// 指定了权限数组的容量
            /// </summary>
            public int PrivilegeCount;
            /// <summary>
            /// Specifies an array of LUID_AND_ATTRIBUTES structures. Each structure contains the LUID and attributes of a privilege.
            /// 指定一组的LUID_AND_ATTRIBUTES 结构，每个结构包含了LUID和权限的属性
            /// </summary>
            public LUID_AND_ATTRIBUTES Privileges;
        }
        #endregion

        #region const
        /// <summary>用来在访问令牌中启用和禁用一个权限</summary>
        private const int TOKEN_ADJUST_PRIVILEGES = 0x20;

        /// <summary>用来查询一个访问令牌</summary>
        private const int TOKEN_QUERY = 0x8;

        /// <summary>权限启用标志</summary>
        private const int SE_PRIVILEGE_ENABLED = 0x2;

        /// <summary>指定了函数需要为请求消息查找系统消息表资源 </summary>
        private const int FORMAT_MESSAGE_FROM_SYSTEM = 0x1000;

        /// <summary>
        /// 强制停止进程。当设置了该标志后，系统不会发送WM_QUERYENDSESSION 和 WM_ENDSSESSION消息，这会使应用程序丢失数据。
        /// 因此，除非紧急，你要慎用该标志
        /// </summary>
        private const int EWX_FORCE = 4;
        #endregion

        #region dll import

        /// <summary>
        /// SetSuspendState函数关闭电源挂起系统，根据休眠参数设置，系统将挂起或者休眠，如果ForceFlag为真，系统将立即停止所有操作，如果为假，系统将征求所有应用程序和驱动程序意见后才会这么做
        /// </summary>
        /// <param name="Hibernate">休眠参数，如果为true，系统进入修改，如果为false，系统挂起</param>
        /// <param name="ForceCritical">强制挂起. 如果为TRUE, 函数向每个应用程序和驱动广播一个 PBT_APMSUSPEND 事件, 然后立即挂起所有操作。如果为 FALSE, 函数向每个应用程序广播一个 PBT_APMQUERYSUSPEND 事件，征求挂起</param>
        /// <param name="DisableWakeEvent">如果为 TRUE, 系统禁用所有唤醒事件，如果为 FALSE, 所有系统唤醒事件继续有效</param>
        /// <returns>如果执行成功，返回非0<br></br><br>如果执行失败, 返回0. 如果要获取更多错误信息，请调用 Marshal.GetLastWin32Error.</br></returns>
        [DllImport("powrprof.dll", EntryPoint = "SetSuspendState", CharSet = CharSet.Ansi)]
        private static extern int SetSuspendState(int Hibernate, int ForceCritical, int DisableWakeEvent);

        /// <summary>
        /// OpenProcessToken 函数与进程关联的访问令牌
        /// </summary>
        /// <param name="ProcessHandle">打开访问令牌进程的句柄</param>
        /// <param name="DesiredAccess">一个访问符，指定需要的访问令牌的访问类型。这些访问类型与访问令牌自定义的访问控制列表(DACL)比较后，决定哪些访问是允许的，哪些是拒绝的</param>
        /// <param name="TokenHandle">句柄指针:标志了刚刚打开的由函数返回的访问令牌</param>
        /// <returns>如果执行成功，返回非0<br></br><br>如果执行失败，返回0. 如果要获取更多的错误信息, 请调用Marshal.GetLastWin32Error.</br></returns>
        [DllImport("advapi32.dll", EntryPoint = "OpenProcessToken", CharSet = CharSet.Ansi)]
        private static extern int OpenProcessToken(IntPtr ProcessHandle, int DesiredAccess, ref IntPtr TokenHandle);

        /// <summary>
        /// LookupPrivilegeValue函数返回本地唯一标志LUID，用于在指定的系统上代表特定的权限名
        /// </summary>
        /// <param name="lpSystemName">以null结束的字符串指针，标志了在其上查找权限名的系统名称. 如果设置为null, 函数将试图查找指定系统的权限名.</param>
        /// <param name="lpName">以null结束的字符串指针，指定了在Winnt.h头文件中定义的权限名. 例如, 该参数可以是一个常量 SE_SECURITY_NAME, 或者对应的字符串 "SeSecurityPrivilege".</param>
        /// <param name="lpLuid">接收本地唯一标志LUID的变量指针，通过它可以知道由lpSystemName 参数指定的系统上的权限.</param>
        /// <returns>如果执行成功，返回非0<br></br><br>如果执行失败，返回0，如果要获取更多的错误信息，请调用Marshal.GetLastWin32Error.</br></returns>
        [DllImport("advapi32.dll", EntryPoint = "LookupPrivilegeValueA", CharSet = CharSet.Ansi)]
        private static extern int LookupPrivilegeValue(string lpSystemName, string lpName, ref LUID lpLuid);

        /// <summary>
        /// AdjustTokenPrivileges 函数可以启用或禁用指定访问令牌的权限. 在一个访问令牌中启用或禁用一个权限需要 TOKEN_ADJUST_PRIVILEGES 访问权限.
        /// </summary>
        /// <param name="TokenHandle">需要改变权限的访问令牌句柄. 句柄必须含有对令牌的 TOKEN_ADJUST_PRIVILEGES 访问权限. 如果 PreviousState 参数非null, 句柄还需要有 TOKEN_QUERY 访问权限.</param>
        /// <param name="DisableAllPrivileges">执行函数是否禁用访问令牌的所有权限. 如果参数值为 TRUE, 函数将禁用所有权限并忽略 NewState 参数. 如果其值为 FALSE, 函数将根据NewState参数指向的信息改变权限.</param>
        /// <param name="NewState">一个 TOKEN_PRIVILEGES 结构的指针，指定了一组权限以及它们的属性. 如果 DisableAllPrivileges 参数为 FALSE, AdjustTokenPrivileges 函数将启用或禁用访问令牌的这些权限. 如果你为一个权限设置了 SE_PRIVILEGE_ENABLED 属性, 本函数将启用该权限; 否则, 它将禁用该权限. 如果 DisableAllPrivileges 参数为 TRUE, 本函数忽略此参数.</param>
        /// <param name="BufferLength">为PreviousState参数指向的缓冲区用字节设置大小. 如果PreviousState 参数为 NULL，此参数可以为0</param>
        /// <param name="PreviousState">一个缓冲区指针，被函数用来填充 TOKENT_PRIVILEGES结构，它包含了被函数改变的所有权限的先前状态. 此参数可以为 NULL.</param>
        /// <param name="ReturnLength">一个变量指针，指示了由PreviousState参数指向的缓冲区的大小.如果 PreviousState 参数为 NULL，此参数可以为NULL .</param>
        /// <returns>如果执行成功，返回非0. 如果要检测函数是否调整了指定的权限, 请调用 Marshal.GetLastWin32Error.</returns>
        [DllImport("advapi32.dll", EntryPoint = "AdjustTokenPrivileges", CharSet = CharSet.Ansi)]
        private static extern int AdjustTokenPrivileges(IntPtr TokenHandle, int DisableAllPrivileges, ref TOKEN_PRIVILEGES NewState, int BufferLength, ref TOKEN_PRIVILEGES PreviousState, ref int ReturnLength);
        #endregion

        #region protect methods
        protected static void ExitWindows(int how, bool force)
        {
            EnableToken("SeShutdownPrivilege");
            if (force)
                how = how | EWX_FORCE;
            if (Apis.User32.ExitWindowsEx(how, 0) == 0)
                throw new Exception(FormatError(Marshal.GetLastWin32Error()));
        }

        /// <summary>
        /// 启用指定的权限
        /// </summary>
        /// <param name="privilege">要启用的权限</param>
        /// <exception cref="PrivilegeException">表明在申请相应权限时发生了错误</exception>
        protected static void EnableToken(string privilege)
        {
            if (!CheckEntryPoint("advapi32.dll", "AdjustTokenPrivileges"))
                return;
            IntPtr tokenHandle = IntPtr.Zero;
            LUID privilegeLUID = new LUID();
            TOKEN_PRIVILEGES newPrivileges = new TOKEN_PRIVILEGES();
            TOKEN_PRIVILEGES tokenPrivileges;
            if (OpenProcessToken(Process.GetCurrentProcess().Handle, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref tokenHandle) == 0)
                throw new Exception(FormatError(Marshal.GetLastWin32Error()));
            if (LookupPrivilegeValue("", privilege, ref privilegeLUID) == 0)
                throw new Exception(FormatError(Marshal.GetLastWin32Error()));
            tokenPrivileges.PrivilegeCount = 1;
            tokenPrivileges.Privileges.Attributes = SE_PRIVILEGE_ENABLED;
            tokenPrivileges.Privileges.pLuid = privilegeLUID;
            int size = 4;
            if (AdjustTokenPrivileges(tokenHandle, 0, ref tokenPrivileges, 4 + (12 * tokenPrivileges.PrivilegeCount), ref newPrivileges, ref size) == 0)
                throw new Exception(FormatError(Marshal.GetLastWin32Error()));
        }

        /// <summary>
        /// 锁定当前账户
        /// </summary>
        /// <param name="args"></param>
        protected static void LockWindows()
        {
            Apis.User32.LockWorkStation();
        }

        /// <summary>
        /// 挂起或休眠系统
        /// </summary>
        /// <param name="hibernate">True表示休眠，否则表示挂起系统</param>
        /// <param name="force">True表示强制退出</param>
        /// <exception cref="PlatformNotSupportedException">如果系统不支持，将抛出PlatformNotSupportedException.</exception>
        protected static void SuspendSystem(bool hibernate, bool force)
        {
            if (!CheckEntryPoint("powrprof.dll", "SetSuspendState"))
                throw new PlatformNotSupportedException("The SetSuspendState method is not supported on this system!");
            SetSuspendState((int)(hibernate ? 1 : 0), (int)(force ? 1 : 0), 0);
        }

        /// <summary>
        /// 检测本地系统上是否存在一个指定的方法入口
        /// </summary>
        /// <param name="library">包含指定方法的库文件</param>
        /// <param name="method">指定方法的入口</param>
        /// <returns>如果存在指定方法，返回True，否则返回False</returns>
        protected static bool CheckEntryPoint(string library, string method)
        {
            IntPtr libPtr = Apis.Kernel32.LoadLibrary(library);
            if (!libPtr.Equals(IntPtr.Zero))
            {
                if (!Apis.Kernel32.GetProcAddress(libPtr, method).Equals(IntPtr.Zero))
                {
                    Apis.Kernel32.FreeLibrary(libPtr);
                    return true;
                }
                Apis.Kernel32.FreeLibrary(libPtr);
            }
            return false;
        }

        /// <summary>
        /// 将错误号转换为错误消息
        /// </summary>
        /// <param name="number">需要转换的错误号</param>
        /// <returns>代表指定错误号的字符串.</returns>
        protected static string FormatError(int number)
        {
            StringBuilder buffer = new StringBuilder(255);
            Apis.User32.FormatMessage(FORMAT_MESSAGE_FROM_SYSTEM, IntPtr.Zero, number, 0, buffer, buffer.Capacity, 0);
            return buffer.ToString();
        }
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
            ExitWindows((int)how, force);
        }
        #endregion
    }
}
