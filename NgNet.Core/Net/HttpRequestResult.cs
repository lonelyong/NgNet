using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace NgNet.Net
{
    public class HttpRequestResult<TResult> where TResult : new() 
    {
        public bool IsSuccessful { get; }

        public TResult Result { get; }

        public byte[] ByteResult { get; } 

        public Exception Exception;

        public WebExceptionStatus? WebExceptionStatus { get; }

        public HttpRequestResult(Exception exception)
        {
            IsSuccessful = false;
            Exception = exception;
            if(exception is WebException)
            {
                WebExceptionStatus = (exception as WebException).Status;
            }
        }

        public HttpRequestResult(byte[] result, string contentType, string encoding)
        {
            ByteResult = result;
            switch (contentType)
            {
                case Mimes.APPLICATION_JSON:

                    break;
                case Mimes.APPLICATION_XML:

                    break;
                case Mimes.TEXT_XML:
                case Mimes.TEXT_PLAIN:
                case Mimes.TEXT_HTML:
                    if(typeof(TResult) == typeof(string))
                    {
                        Result = (TResult)(object)Encoding.GetEncoding(encoding).GetString(result);
                    }
                    break;
                default:
                    throw new Exception($"不支持的ContentType({contentType})");
            }
        }
    }
}
