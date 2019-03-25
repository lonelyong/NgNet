using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Net.FileTransfer
{
    /// <summary>
    /// 
    /// </summary>
    public class FileBlockException : Exception
    {
        public enum ErrorCode
        {
            BadHeader,
            BadIndex,
            IllegalFileBlockSize,
            ChecksumError,
        }


        #region public properties
        public ErrorCode Code { get; set; }
        #endregion

        #region contractor
        public FileBlockException(string message, ErrorCode ErrorCode)
            : base(message)
        {
            Code = ErrorCode;
        }
        #endregion
    }
}
