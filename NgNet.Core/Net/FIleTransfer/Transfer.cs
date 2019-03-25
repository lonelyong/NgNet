using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace NgNet.Net.FileTransfer
{
    /// <summary>
    /// 上传和接受的基类
    /// </summary>
    public abstract class Transfer : IDisposable
    {
        #region private fields
        /// <summary>
        /// 上一个区块完成的时间
        /// </summary>
        private DateTime _priorBlockTime;  
        #endregion

        #region internal fields
        /// <summary>
        /// 总区块数
        /// </summary>
        internal int _TotalBlockCount;
        /// <summary>
        /// 已完成的区块
        /// </summary>
        internal List<int> _FinishedBlock;
        #endregion

        #region protected fields
        /// <summary>
        /// 文件写入输出流
        /// </summary>
        protected FileStream _FileStream;
        /// <summary>
        /// 最后一个区块的大小
        /// </summary>
        protected int _LastBlockSize;
        /// <summary>
        /// 
        /// </summary>
        protected byte[] _ReceiveBuffer;
        /// <summary>
        /// 数据传输使用的套接字
        /// </summary>
        protected internal Socket _Socket;
        /// <summary>
        /// 区块集合
        /// </summary>
        protected FileBlockCollection _Blocks;
        /// <summary>
        /// 同步
        /// </summary>
        protected AutoResetEvent _AutoResetEvent = new AutoResetEvent(false);
        /// <summary>
        /// 异步方法发生异常时返回该异常
        /// </summary>
        protected Func<Exception, Exception> _AsyncReturnException;
        #endregion

        #region public properties
        /// <summary>
        /// 获取或设置一个值,该值指示是否启用磁盘缓存
        /// </summary>
        public bool EnabledIOBuffer
        {
            get { return _Blocks._EnabledIOBuffer; }
            set { _Blocks.EnabledIOBuffer = value; }
        }
        /// <summary>
        /// 获取或设置磁盘缓存的大小(单位:区块数)
        /// </summary>
        public int IOBufferSize
        {
            get { return _Blocks._IOBufferSize; }
            set
            {
                if (!_Blocks._EnabledIOBuffer)
                    throw new InvalidOperationException("IOBuffer is not enabled!");
                _Blocks._IOBufferSize = value;
            }
        }
        /// <summary>
        /// 获取当前磁盘缓存中的区块数
        /// </summary>
        public int CurrentIOBufferSize
        {
            get
            {
                if (!_Blocks._EnabledIOBuffer)
                    return 0;
                return _Blocks._IOBuffer.Count;
            }
        }
        /// <summary>
        /// 获取或设置该传输的目标连接
        /// </summary>
        public Socket Socket
        {
            get { return _Socket; }
            set
            {
                if (value.ProtocolType != ProtocolType.Tcp)
                    OnErrorOccurred(new Exception("Socket Protocol must be TCP"));
                _Socket = value;
                _Socket.ReceiveBufferSize = _Socket.SendBufferSize = Consts.NetBlockMaxSize;
            }
        }
        /// <summary>
        /// 获取与此传输关联的文件流
        /// </summary>
        public FileStream FileStream { get { return _FileStream; } }
        /// <summary>
        /// 获取或设置文件路径
        /// </summary>
        public string FileDirectory { get; set; }
        /// <summary>
        /// 获取或设置文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 获取或设置文件名(包括路径)
        /// </summary>
        public string FilePath
        {
            get
            {
                return FileDirectory.TrimEnd('\\') + "\\" + FileName;
            }
            set
            {
                int i = value.LastIndexOf('\\');
                if (i > 0)
                    FileDirectory = value.Substring(0, i);
                else
                    FileDirectory = Environment.CurrentDirectory;
                FileName = value.Substring(i + 1);
            }
        }
        /// <summary>
        /// 获取或设置绑定的数据
        /// </summary>
        public object Tag { get; set; }
        #endregion

        #region events
        /// <summary>
        /// 一个区块完成时发生
        /// </summary>
        public event BlockFinishedEventHandler BlockFinished;
        /// <summary>
        /// 全部完成时发生
        /// </summary>
        public event EventHandler AllFinished;
        /// <summary>
        /// 连接中断时发生
        /// </summary>
        public event EventHandler ConnectLost;
        /// <summary>
        /// 出现错误时发生
        /// </summary>
        public event TransferErrorOccurEventHandler ErrorOccurred;
        /// <summary>
        /// 用户停止文件传输时发生
        /// </summary>
        public event TransferStopedEventHandler TransferStoped;
        #endregion

        #region public properties
        /// <summary>
        /// 获取一个值,该值指示传输是否正在进行
        /// </summary>
        public bool IsAlive { get; private set; }
        /// <summary>
        /// 获取传输开始的时间
        /// </summary>
        public DateTime StartTime { get; protected set; }
        /// <summary>
        /// 获取已用时
        /// </summary>
        public TimeSpan TimePast { get { return DateTime.Now - StartTime; } }
        /// <summary>
        /// 获取估计剩余时间
        /// </summary>
        public abstract TimeSpan TimeRemaining { get; }
        /// <summary>
        /// 获取平均速率(区块/秒)
        /// </summary>
        public double BlockAverSpeed
        {
            get
            {
                return _FinishedBlock.Count / TimePast.TotalSeconds;
            }
        }
        /// <summary>
        /// 获取平均速率(字节/秒)
        /// </summary>
        public double ByteAverSpeed
        {
            get
            {
                return BlockAverSpeed * Consts.BlockSize;
            }
        }
        /// <summary>
        /// 获取平均速率(千字节/秒)
        /// </summary>
        public double KByteAverSpeed
        {
            get
            {
                return ByteAverSpeed / 1024;
            }
        }
        /// <summary>
        /// 获取瞬时速率(字节/秒)
        /// </summary>
        public double ByteSpeed
        {
            get; private set;
        }
        /// <summary>
        /// 获取瞬时速率(千字节/秒)
        /// </summary>
        public double KByteSpeed
        {
            get
            {
                return ByteSpeed / 1024;
            }
        }
        /// <summary>
        /// 获取文件总长度
        /// </summary>
        public long TotalSize
        {
            get
            {
                return (_TotalBlockCount - 1) * (long)Consts.BlockSize + _LastBlockSize;
            }
        }
        /// <summary>
        /// 获取已完成的数据长度
        /// </summary>
        public abstract long FinishedSize { get; }
        /// <summary>
        /// 获取进度值(%)
        /// </summary>
        public double Progress
        {
            get
            {
                return ((double)FinishedSize / TotalSize) * 100;
            }
        }
        /// <summary>
        /// 获取该传输的区块集合
        /// </summary>
        public FileBlockCollection Blocks { get { return _Blocks; } }
        #endregion

        #region constructor
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Transfer()
        {
            _AsyncReturnException = ex => ex;
            _FinishedBlock = new List<int>();
            _Blocks = new FileBlockCollection(this);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="fileName">文件名</param>
        public Transfer(string filePath, string fileName) : this()
        {
            FileDirectory = filePath;
            FileName = fileName;
        }
        #endregion

        #region private methods
        ///// <summary>
        ///// 异步中止传输,不关闭Socket
        ///// </summary>
        //protected void Stop()
        //{
        //    new Delegate_Void_Bool(Stop).BeginInvoke(false, null, null);
        //}
        /// <summary>
        ///  （无 try）
        /// </summary>
        /// <param name="ar"></param>
        private void sendCallback(IAsyncResult ar)
        {
            if (ar.AsyncState is Exception)
                throw (ar.AsyncState as Exception);
            else
            {
                try
                {
                    _Socket.EndSend( ar );
                }
                catch (Exception)
                {
                  
                }
                if (ar.AsyncState != null)
                {
                    if (ar.AsyncState is int)
                    {
                        OnBlockFinished((int)ar.AsyncState);
                    }
                }
            }
        }
        /// <summary>
        /// (有try)（IAsyncResult返回类型异常单独处理）异步发送字符串并使用指定的的回调方法和参数
        /// </summary>
        private IAsyncResult beginSendString(string str, AsyncCallback callback, object state)
        {
            if (!IsAlive)
                return _AsyncReturnException.BeginInvoke(new InvalidOperationException(Consts.INFO_ISNOTALIVE), null, null);
            byte[] _send = str.ToBytes();
            try
            {
                return _Socket.BeginSend(_send, 0, _send.Length, SocketFlags.None, callback, state);
            }
            catch (SocketException ex)
            {
                return _AsyncReturnException.BeginInvoke(ex, null, null);
            }
            catch (Exception ex)
            {
                return _AsyncReturnException.BeginInvoke(ex, null, null);
            }
        }
        #endregion

        #region protected methods 
        protected void InnerStop(bool shutdownSocket)
        {
            IsAlive = false;
            if (shutdownSocket)
            {
                try
                {
                    _Socket.Shutdown( SocketShutdown.Both );
                    _Socket.Close();
                }
                catch
                {
                    
                }
            }
            _FileStream.Close();
        }
        /// <summary>
        /// （无try）初始化接收缓存
        /// </summary>
        protected void InitReceiveBuffer()
        {
            _ReceiveBuffer = new byte[_Socket.ReceiveBufferSize];
        }
        /// <summary>
        /// 开始异步接收
        /// </summary>
        protected abstract IAsyncResult BeginReceive();
        /// <summary>
        /// （无try）当一块文件传输完成后发生
        /// </summary>
        /// <param name="BlockIndex"></param>
        protected virtual void OnBlockFinished(int BlockIndex)
        {
            if (!_FinishedBlock.Exists(a => a == BlockIndex))
                _FinishedBlock.Add(BlockIndex);
            if (BlockIndex == _TotalBlockCount - 1)
                ByteSpeed = _LastBlockSize / (DateTime.Now - _priorBlockTime).TotalSeconds;
            else
                ByteSpeed = Consts.BlockSize / (DateTime.Now - _priorBlockTime).TotalSeconds;
            _priorBlockTime = DateTime.Now;
            BlockFinished?.Invoke(this, new BlockFinishedEventArgs(BlockIndex));
        }
        /// <summary>
        /// 连接中断
        /// </summary>
        protected virtual void OnConnectLost()
        {
            if (!IsAlive)
                return;
            InnerStop(true);
            ConnectLost?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 文件传输完成
        /// </summary>
        protected virtual void OnAllFinished()
        {
            AllFinished?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 发生错误
        /// </summary>
        /// <param name="innerException"></param>
        protected virtual void OnErrorOccurred(Exception innerException)
        {
            InnerStop(true);
            TransferErrorOccurEventArgs _e = new TransferErrorOccurEventArgs(innerException);
            ErrorOccurred?.Invoke(this, _e);
        }
        /// <summary>
        /// 用户停止传输
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnTransferStoped(TransferStopedEventArgs e)
        {
            TransferStoped?.Invoke(this, e);
        }
        #endregion

        #region public methods
        /// <summary>
        /// 开始传输
        /// </summary>
        public virtual void Start()
        {
            IsAlive = true;
            StartTime = DateTime.Now;
            _AutoResetEvent.Set();
        }
        /// <summary>
        /// 中止传输
        /// </summary>
        /// <param name="shutdownSocket">是否关闭Socket</param>
        public virtual void Stop(bool shutdownSocket)
        {
            InnerStop(shutdownSocket);
            OnTransferStoped(new TransferStopedEventArgs());
        }       
        /// <summary>
        /// （无try）同步发送字符串
        /// </summary>
        public int SendString(string str)
        {
            return _Socket.EndSend( beginSendString( str, null, null ) );
        }
        /// <summary>
        /// （无try）异步发送字符串并使用默认的回调方法
        /// </summary>
        public IAsyncResult SendStringAsync(string str)
        {
            return beginSendString(str, sendCallback, null);
        }
        #endregion

        #region IDisposable
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _FileStream.Close();
            _Socket.Close();
        }
        #endregion
    }
}
