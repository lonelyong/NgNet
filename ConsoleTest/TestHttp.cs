using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;

namespace ConsoleTest
{
    class TestHttp : TestBase
    {
        void TestHttpClient()
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.BaseAddress = new Uri("https://ourl.link");
            httpClient.DefaultRequestHeaders.Add("header-name", "");
            httpClient.Timeout = TimeSpan.FromSeconds(3);
            httpClient.PostAsync("/apis/zip", null);
        }
        void TestHttpRequest()
        {
            System.Web.HttpRequest httpRequest = new HttpRequest("", "", "");
        }
        void TestHttpWebRequest()
        {
            System.Net.HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("");
        }
        void TestWebClient()
        {
            System.Net.WebClient webClient = new WebClient();
            webClient.BaseAddress = "";
        }
        public override void Test()
        {
            
        }
    }
}
