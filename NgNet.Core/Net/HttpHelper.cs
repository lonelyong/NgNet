using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;

namespace NgNet.Net 
{
    public class HttpHelper
    {
        public NameValueCollection Headers { get; set; }

        public string BaseUrl { get; set; }

        public static void AddHeaders(WebRequest req, NameValueCollection headers)
        {
            if (headers != null)
            {
                foreach (var item in headers.AllKeys)
                {
                    req.Headers.Add(item, headers[item]);
                }
            }
        }

        public static HttpRequestResult<TResult> SendRequest<TResult>(string url, HttpMethod method, object data, Dictionary<string, string> query, string contentType, NameValueCollection headers) where TResult:new()
        {
            if (string.IsNullOrEmpty(contentType))
            {
                throw new Exception($"{nameof(contentType)}不能为空");
            }
            HttpRequestResult<TResult> result;
            var req = WebRequest.Create(url) as HttpWebRequest;
            req.Method = method.ToString();
            req.ContentType = contentType;
            req.AllowAutoRedirect = true;
            req.TransferEncoding = Encoding.UTF8.EncodingName;
            AddHeaders(req, headers);
            if (data != null)
            {
                var dataType = data.GetType();
                byte[] dataBytes = null;
                if(dataType == typeof(string))
                {
                    dataBytes = Encoding.UTF8.GetBytes(data.ToString());
                }
                else if (dataType == typeof(byte[]))
                {
                    dataBytes = (byte[])data;
                }
                else if(dataType == typeof(Stream))
                {
                    var streamData = (Stream)data;
                    dataBytes = new byte[streamData.Length];
                    streamData.Write(dataBytes, 0, dataBytes.Length);
                }
                else if(dataType == typeof(Image))
                {
                    var imageData = (Image)data;
                    using (var memStream = new MemoryStream())
                    {
                        imageData.Save(memStream, imageData.RawFormat);
                        dataBytes = new byte[memStream.Length];
                        memStream.Write(dataBytes, 0, dataBytes.Length);
                    }
                }
                else if (dataType.IsClass)
                {
                    switch (contentType)
                    {
                        case Mimes.APPLICATION_JSON:
                            
                            break;
                        case Mimes.APPLICATION_XML:

                            break;
                        case Mimes.MULTIPORT_FORMDATA:

                            break;
                        default:
                            throw new Exception("此类型的数据不支持");
                    }
                }
                else 
                {
                    throw new Exception($"不支持发送此类型的数据（{data.GetType().FullName}）");
                }
                var reqStream = req.GetRequestStream();
                reqStream.Write(dataBytes, 0, dataBytes.Length);
                reqStream.Dispose();
            }
            HttpWebResponse rep;
            try
            {
                rep = req.GetResponse() as HttpWebResponse;
            }
            catch (Exception e)
            {
                result = new HttpRequestResult<TResult>(e);
                return result;
            }
            var repStream = rep.GetResponseStream();
            var repBytes = new byte[repStream.Length];
            repStream.Read(repBytes, 0, repBytes.Length);
            result = new HttpRequestResult<TResult>(repBytes, rep.ContentType, rep.ContentEncoding);
            rep.Dispose();
            return result;
        }
    }
}
