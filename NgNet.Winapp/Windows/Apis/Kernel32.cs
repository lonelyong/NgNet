using System;
using System.Runtime.InteropServices;

namespace NgNet.Windows.Apis
{
    public class Kernel32
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]

        public static extern bool SetProcessWorkingSetSize(IntPtr process, UIntPtr minimumWorkingSetSize, UIntPtr maximumWorkingSetSize);
        /// <summary>
        /// LoadLibrary函数将指定的可执行模块映射到调用进程的地址空间
        /// </summary>
        /// <param name="lpLibFileName">可执行模块(dll or exe)的名字：以null结束的字符串指针. 该名称是可执行模块的文件名，与模块本身存储的,用关键字LIBRARY在模块定义文件(.def)中指定的名称无关,</param>
        /// <returns>如果执行成功, 返回模块的句柄<br></br><br>如果执行失败, 返回 NULL. 如果要获取更多的错误信息, 请调用Marshal.GetLastWin32Error.</br></returns>
        [DllImport("kernel32.dll", EntryPoint = "LoadLibraryA", CharSet = CharSet.Ansi)]
        public static extern IntPtr LoadLibrary(string lpLibFileName);

        /// <summary>
        /// FreeLibrary函数将装载的dll引用计数器减一，当引用计数器的值为0后，模块将从调用进程的地址空间退出，模块的句柄将不可再用
        /// </summary>
        /// <param name="hLibModule">dll模块的句柄. LoadLibrary 或者 GetModuleHandle 函数返回该句柄</param>
        /// <returns>如果执行成功, 返回值为非0<br></br><br>如果失败，返回值为0. 如果要获取更多的错误信息，请调用Marshal.GetLastWin32Error.</br></returns>
        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary", CharSet = CharSet.Ansi)]
        public static extern int FreeLibrary(IntPtr hLibModule);

        /// <summary>
        /// GetProcAddress 函数获取外部函数的入口地址，或者从指定的DLL获取变量信息
        /// </summary>
        /// <param name="hModule">Dll的句柄，包含了函数或者变量，LoadLibrary 或 GetModuleHandle 函数返回该句柄 </param>
        /// <param name="lpProcName">以null结束的字符串指针，包含函数或者变量名，或者函数的顺序值，如果该参数是一个顺序值，它必须是低序字be in the low-order word,高序字(the high-order)必须为0</param>
        /// <returns>如果执行成功, 返回值为外部函数或变量的地址<br></br><br>如果执行失败，返回值为NULL. 如果要获取更多错误信息，请调用Marshal.GetLastWin32Error.</br></returns>
        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress", CharSet = CharSet.Ansi)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport( "kernel32.dll" )]
        public static extern bool AllocConsole();
        [DllImport( "kernel32.dll" )]
        public static extern bool FreeConsole();
    }
}
