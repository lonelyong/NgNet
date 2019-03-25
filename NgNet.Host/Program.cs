using System;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using NgNet.Cli;
using static System.Console;
using System.Linq;
using System.Text.RegularExpressions;

namespace NgNet.Host
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Run(args);
        }

        private static void Run(string[] args)
        {
            CmdOptions options = new CmdOptions();
            if (!Parser.Default.ParseArguments(args, options))
            {
                return;
            }
            string _dll = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\{options.Reference}.dll";
            if (!File.Exists(_dll))
            {
                WriteLine($"目标不存在<{_dll}>");
                return;
            }
            Assembly assembly;
            try
            {
                assembly = Assembly.LoadFile(_dll);
            }
            catch (Exception ex)
            {
                WriteLine($"目标dll加载失败<{ex.Message}>");
                return;
            }
            Type clsType = null;
            MethodInfo mi = null;
            object instance = null;
            object[] pms = null;
            try
            {
                pms = options.GetParams();
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
                return;
            }
            try
            {
                clsType = assembly.GetType(options.Class);
            }
            catch (Exception ex)
            {
                WriteLine($"获取Class类型失败<{ex.Message}>");
                return;
            }
          
            if (options.Static)
            {
                try
                {
                    mi = clsType.GetMethod(options.Method, BindingFlags.Public | BindingFlags.Static);
                }
                catch (Exception ex)
                {
                    WriteLine($"获取方法信息失败<{ex.Message}>");
                    return;
                }
            }
            else
            {
                try
                {
                    instance = Activator.CreateInstance(clsType);
                }
                catch (Exception ex)
                {
                    WriteLine($"创建 {options.Class} 的实例失败<{ex.Message}>");
                    return;
                }
                try
                {
                    mi = clsType.GetMethod(options.Method, BindingFlags.Public | BindingFlags.Instance);
                }
                catch (Exception ex)
                {
                    WriteLine($"获取方法信息失败<{ex.Message}>");
                    return;
                }
            }
            if(pms != null)
            {
                try
                {
                    var pds = mi.GetParameters();
                    if (pds.Length != pms.Length)
                    {
                        WriteLine("参数个数不匹配");
                        return;
                    }
                    for (int i = 0; i < pms.Length; i++)
                    {
                        var pv = pms[i];
                        var pd = pds[i];
                        pms[i] = Convert.ChangeType(pv, pd.ParameterType);
                    }
                }
                catch (Exception ex)
                {
                    WriteLine($"参数类型错误<{ex.Message}>");
                    return;
                }
            }
            try
            {
                mi.Invoke(instance, pms);
            }
            catch (Exception ex)
            {
                WriteLine($"调用方法失败<{ex.Message}>");
                return;
            }
        }
    }
}
