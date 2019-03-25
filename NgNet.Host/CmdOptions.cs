using NgNet.Cli;
using NgNet.Cli.Text;
using System.Collections;
using System;
namespace NgNet.Host 
{
    class CmdOptions
    {
        [Option('r', "reference", Required = true, HelpText = "请输入目标DLL名称")]
        public string Reference { get; set; }

        [Option('c', "class", Required = true, HelpText = "请输入要调用的类的完全限定名")]
        public string Class { get; set; }

        [Option('m', "method", Required = true, HelpText = "请输入要调用的方法")]
        public string Method { get; set; }

        [Option('s', "static", Required = false, HelpText = "是否是静态方法，默认为false")]
        public bool Static { get; set; }

        [Option('p', "params", Required = false, HelpText = "方法参数，多个空格分开")]
        public string Parameters { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }

        public const string PARAM_HEADER = "$";

        public const string PARAM_HANDLE = "handle";

        public const string PARAM_NULL = "null";

        public object[] GetParams()
        {
            ArrayList arrayList = new ArrayList();
            if (string.IsNullOrEmpty(Parameters))
            {
                return null;
            }
            var array = Parameters.Split(new char[] { ' '}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in array)
            {
                if (item.StartsWith(PARAM_HEADER))
                {
                    var p = item.Substring(1);
                    switch (p)
                    {
                        case PARAM_HANDLE:
                            var t = Console.Title;
                            Console.Title = Guid.NewGuid().ToString("n");
                            arrayList.Add(WinApi.FindWindow("ConsoleWindowClass", Console.Title));
                            Console.Title = t;
                            break;
                        case PARAM_NULL:
                            arrayList.Add(null);
                            break;
                        default:
                            throw new Exception($"不存在变量{p}");
                    }
                }
                else
                {
                    arrayList.Add(item);
                }
            }
            return arrayList.ToArray();
        }
    }
}
