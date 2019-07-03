using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ConsoleTest
{
    [Flags]
    enum tenum : int
    {
        A= -2,
        B= -4
    }
    class Program
    {

        static object syncRoot = new object();
        static bool lockTaken = false;
        static int x = 0;
        static Func<int> func = () => {
            System.Threading.Thread.Sleep(2000);
            bool lockTaken = false;
            Monitor.Enter(syncRoot, ref lockTaken);
            x++;
            Monitor.Exit(syncRoot);
            return x;
        };
        static void Main(string[] args)
        {
            NgNet.UI.Forms.HotMessageBox hotMessageBox = new NgNet.UI.Forms.HotMessageBox(100000);
            Console.Read();
            hotMessageBox.Show("sdfs的房价");
            NgNet.UI.Forms.MessageBox.Show("12345");
			var str = "Abc<li>001</li>adasdf<li>a2</li>dadfasf<li>003</li>";
			var pattern = @"<li>(?<content>.*?)</li>";
			var regex = new Regex(pattern);
			var mcs = Regex.Matches(str, pattern, RegexOptions.ECMAScript);
			foreach (Match item in mcs)
			{
				Console.WriteLine(item.Groups["content"].Value);
			}
			Console.Read();
			return;
            //Interlocked.Increment(ref x);
            //Task.Factory.StartNew(()=> { Monitor.TryEnter(syncRoot, 500, ref lockTaken); Thread.Sleep(5000);Monitor.Exit(syncRoot); });
            //test();
            //Console.ReadLine();
            //var fso = File.OpenRead("");
            //var fsw = File.OpenWrite("");
            //var gz = new System.IO.Compression.DeflateStream(fsw, System.IO.Compression.CompressionMode.Compress);
            //fso.CopyTo(fsw);

            //testTcp();
            //Console.Read();
            //new SortTest().Test();
            //new TestXml().Test();
            Console.WriteLine(NgNet.UI.ScreenHelper.PrimaryScreen.DpiX + "|" + NgNet.UI.ScreenHelper.PrimaryScreen.DpiY + "|" +NgNet.UI.ScreenHelper.PrimaryScreen.ScaleX + "|" + NgNet.UI.ScreenHelper.PrimaryScreen.ScaleY);

            Console.WriteLine(Math.Sqrt((Math.Pow(2160, 2) + Math.Pow(3840, 2))) / 144);
            new TestNgNetUi().Test();

        }
        static void test()
        {
            func.BeginInvoke(ar => {
                Console.WriteLine(func.EndInvoke(ar));
                test();
            }, null);
        }

        static void testSpinLock()
        {
            SpinLock spinLock = new SpinLock();
            
        }

        static void testTcp()
        {
            TcpListener tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 13003);
            tcpListener.Start();
            while (true)
            {
                var client = tcpListener.AcceptTcpClient();
                var stream = client.GetStream();
                var read = new byte[1024];
                int len = stream.Read(read, 0 ,1024);
                Array.Clear(read, len, read.Length - len);
                var name = Encoding.Default.GetString(read);
                Console.WriteLine(name);
                var bytes = Encoding.Default.GetBytes($"{name}上线了");
                stream.Write(bytes, 0, bytes.Length);
                client.Close();
            }
        }

        static void startSocketServer()
        {
            Socket socket = new Socket( SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 13002));
            socket.Listen(6);
            while (true)
            {
                var client = socket.Accept();
            }
        }
    }
}
