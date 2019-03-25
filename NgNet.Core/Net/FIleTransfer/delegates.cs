using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Net.FileTransfer
{
    #region 一些委托
    public delegate void BlockFinishedEventHandler(object sender, BlockFinishedEventArgs e);
    public delegate void TransferStopedEventHandler(object sender, TransferStopedEventArgs e);
    public delegate void TransferErrorOccurEventHandler(object sender, TransferErrorOccurEventArgs e);
    public delegate int Delegate_Int_Int(int value);
    #endregion

    #region 事件参数类
    public class BlockFinishedEventArgs : EventArgs
    {
        public int BlockIndex { get; set; }
        public BlockFinishedEventArgs(int BlockIndex) { this.BlockIndex = BlockIndex; }
    }
    public class TransferErrorOccurEventArgs : EventArgs
    {
        public Exception InnerException { get; set; }
        /// <summary>
        /// 指示是否继续运行
        /// </summary>
        public bool Continue { get; set; }
        public TransferErrorOccurEventArgs(Exception innerException)
        {
            InnerException = innerException;
            Continue = false;
        }
    }
    public class TransferStopedEventArgs : EventArgs
    {

    }
    #endregion
}
