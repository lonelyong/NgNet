using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace NgNet.Windows.Apis
{
    public class User32
    {
        [DllImport("user32.dll", EntryPoint = "AnimateWindow")]
        public static extern bool AnimateWindow(IntPtr handle, int ms, int flags);

        [DllImport("user32.dll")]
        public static extern int SetWindowRgn(IntPtr hwnd, int hRgn, Boolean bRedraw);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr PostMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern int GetWindowLong(HandleRef hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLong(HandleRef hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern bool AppendMenu(IntPtr hMenu, Int32 wFlags, Int32 wIDNewItem, string lpNewItem);

        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SetParent")]
        public static extern int SetParent(int hWndChild, int hWndNewParent);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern long CloseWindow(IntPtr hWnd);

        /// <summary>
        /// ExitWindowEx函数要么注销当前用户,关闭系统, 要么关闭系统然后重新启动. 它发送 WM_QUERYENDSESSION 给所有的应用程序，决定是否可以停止它们的操作.
        /// </summary>
        /// <param name="uFlags">指定关闭的类型.</param>
        /// <param name="dwReserved">该参数忽略.</param>
        /// <returns>如果执行成功，返回非0.<br></br><br>如果执行失败，返回0. 如果要获取更多的错误信息, 请调用 Marshal.GetLastWin32Error.</br></returns>
        [DllImport("user32.dll", EntryPoint = "ExitWindowsEx", CharSet = CharSet.Ansi)]
        public static extern int ExitWindowsEx(int uFlags, int dwReserved);

        /// <summary>
        /// Provides access to function required to delete handle. This method is used internally
        /// and is not required to be called separately.
        /// </summary>
        /// <param name="hIcon">Pointer to icon handle.</param>
        /// <returns>N/A</returns>
        [DllImport("user32.dll")]
        public static extern int DestroyIcon(IntPtr hIcon);

        /// <summary>
        /// 注册快捷键
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="id">序号</param>
        /// <param name="modifiers"></param>
        /// <param name="vk"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int modifiers, Keys vk);

        /// <summary>
        /// 取消注册快捷键
        /// </summary>
        /// <param name="hWnd">注册到的句柄</param>
        /// <param name="id">快捷键ID</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);
        /// <summary>
        /// 锁定当前账户
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.Dll")]
        public static extern bool LockWorkStation();
        /// <summary>
        /// FormatMessage格式化消息字符串. 该函数需要一个已定义的消息参数作为输入. 消息的定义从一个缓冲区传入函数，它也可以从一个已载入模块的消息表资源中获取. 调用者也可以要求函数搜索系统的消息表来查找消息定义. 函数根据消息ID号和语言ID号从消息表资源中查找消息定义. 函数最终将格式化的消息文本拷贝到输出缓冲区, 要求处理任何内嵌的顺序.
        /// </summary>
        /// <param name="dwFlags">指定格式化处理和如何翻译 lpSource 参数. dwFlags的低字节指定函数如何处理输出缓冲区的换行. 低字节也可以指定格式化后的输出缓冲区的最大宽度.</param>
        /// <param name="lpSource">指定消息定义的位置. 此参数的类型依据 dwFlags 参数的设定.</param>
        /// <param name="dwMessageId">指定消息的消息标志ID. 如果 dwFlags 参数包含 FORMAT_MESSAGE_FROM_STRING ，则该参数被忽略.</param>
        /// <param name="dwLanguageId">指定消息的语言ID. 如果 dwFlags 参数包含 FORMAT_MESSAGE_FROM_STRING，则该参数被忽略.</param>
        /// <param name="lpBuffer">用来放置格式化消息(以null结束)的缓冲区.如果 dwFlags 参数包括 FORMAT_MESSAGE_ALLOCATE_BUFFER, 本函数将使用LocalAlloc函数定位一个缓冲区，然后将缓冲区指针放到 lpBuffer 指向的地址.</param>
        /// <param name="nSize">如果没有设置 FORMAT_MESSAGE_ALLOCATE_BUFFER 标志, 此参数指定了输出缓冲区可以容纳的TCHARs最大个数. 如果设置了 FORMAT_MESSAGE_ALLOCATE_BUFFER 标志，则此参数指定了输出缓冲区可以容纳的TCHARs 的最小个数. 对于ANSI文本, 容量为bytes的个数; 对于Unicode 文本, 容量为字符的个数.</param>
        /// <param name="Arguments">数组指针,用于在格式化消息中插入信息. 格式字符串中的 A %1 指示参数数组中的第一值; a %2 表示第二个值; 以此类推.</param>
        /// <returns>如果执行成功, 返回值为存储在输出缓冲区的TCHARs个数, 包括了null结束符.<br></br><br>如果执行失败, 返回值为0. 如果要获取更多的错误信息, 请调用 Marshal.GetLastWin32Error.</br></returns>
        [DllImport("user32.dll", EntryPoint = "FormatMessageA", CharSet = CharSet.Ansi)]
        public static extern int FormatMessage(int dwFlags, IntPtr lpSource, int dwMessageId, int dwLanguageId, StringBuilder lpBuffer, int nSize, int Arguments);

        [DllImport("user32")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        [DllImport("user32", EntryPoint = "GetWindowLong")]
        public static extern int GetWindowLong(IntPtr hwnd, int nIndex);
        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll")]
        public static extern bool FlashWindow(IntPtr hWnd, bool bInvert);
    }
}
