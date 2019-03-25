using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;

namespace NgNet.Net.FileTransfer
{
    /// <summary>
    /// 接收端
    /// 传输前接收端创建该类实例
    /// 设置必要属性后
    /// 调用Start()方法开始传输
    /// </summary>
    public class FileReceiver : Transfer
    {
        #region private fields
        /// <summary>
        /// 下载线程
        /// </summary>
        private Thread _downThread;
        #endregion

        #region internal fields
        internal List<int> _ExistBlock;

        internal List<int> _EastBlock;
        #endregion

        #region public properties
        /// <summary>
        /// 获取估计剩余时间
        /// </summary>
        public override TimeSpan TimeRemaining
        {
            get
            {
                int _blockRemaining = _TotalBlockCount - _FinishedBlock.Count - _ExistBlock.Count;
                return TimeSpan.FromSeconds(_blockRemaining / BlockAverSpeed);
            }
        }
        /// <summary>
        /// 获取已完成的数据长度
        /// </summary>
        public override long FinishedSize
        {
            get
            {
                return (_FinishedBlock.Count + _ExistBlock.Count - 1) * Consts.BlockSize + _LastBlockSize;
            }
        }
        #endregion

        #region events
        /// <summary>
        /// 区块验证完成时发生
        /// </summary>
        public event BlockFinishedEventHandler BlockHashed;
        #endregion

        #region private methods
        /// <summary>
        /// （无try）接收字符串
        /// </summary>
        /// <returns></returns>
        private string receiveString()
        {
            int count = 0;
            count = _Socket.EndReceive(BeginReceive());
            if (count == 0)
                return null;
            else
                return _ReceiveBuffer.ToFTString();
        }
        /// <summary>
        /// （无try）接收文件区块
        /// </summary>
        /// <returns></returns>
        private FileBlock receiveBlock()
        {
            MemoryStream _mStream = new MemoryStream();
            while (true)
            {
                int _count = 0;
                _count = _Socket.EndReceive(BeginReceive());
                if (_count == 0)
                    throw new SocketException();
                _mStream.Write(_ReceiveBuffer, 0, _count);
                try
                {//接收到正确的区块则返回
                    return new FileBlock(this, _mStream.ToArray());
                }
                catch (FileBlockException ex)
                {//接收到不完整或错误的区块,若不完整则继续接收
                    if (_mStream.Length >= Consts.NetBlockMaxSize)
                        throw ex;//区块已达到指定大小但仍然错误,则抛出错误
                }
            }
        }
        /// <summary>
        /// （无try）从发送端获取文件名
        /// </summary>
        private void getFileName()
        {
            while (true)
            {
                SendString($"{Consts.CMD_GET} {Consts.CMD_FILENAME}");
                string[] _msgs = receiveString().Split(' ');
                if (_msgs[0] == Consts.CMD_SET && _msgs[1] == Consts.CMD_FILENAME)
                {
                    FileName = Consts.DeReplace(_msgs[2]);
                    break;
                }
            }
        }
        /// <summary>
        /// （无try）从发送端获取区块总数
        /// </summary>
        private void getBlockCount()
        {
            while (true)
            {
                SendString($"{Consts.CMD_GET} {Consts.CMD_TOTALBLOCK}");
                string[] _msgs = receiveString().Split(' ');
                if (_msgs[0] == Consts.CMD_SET && _msgs[1] == Consts.CMD_TOTALBLOCK)
                {
                    if (int.TryParse(_msgs[2], out _TotalBlockCount))
                        break;
                }
            }
        }
        /// <summary>
        /// （无try）从发送端获取最后一个区块的大小
        /// </summary>
        private void getLastBlockSize()
        {
            while (true)
            {
                SendString($"{Consts.CMD_GET} {Consts.CMD_LASTBLOCKSIZE}");
                string[] _msgs = receiveString().Split(' ');
                if (_msgs[0] == Consts.CMD_SET && _msgs[1] == Consts.CMD_LASTBLOCKSIZE)
                {
                    if (int.TryParse(_msgs[2], out _LastBlockSize))
                        break;
                }
            }
        }
        /// <summary>
        /// （最顶层）接收整个文件
        /// </summary>
        private void download()
        {
            if (string.IsNullOrEmpty(FileDirectory))//未指定路径时默认为接收程序所在路径
                FileDirectory = Environment.CurrentDirectory;
            #region //未指定文件名时从发送端获取
            if (string.IsNullOrWhiteSpace(FileName))
                try
                {
                    getFileName();
                }
                catch (SocketException)
                {
                    OnConnectLost();
                    return;
                }
                catch (Exception ex)
                {
                    OnErrorOccurred(ex);
                    return;
                }
            #endregion

            string _directory = Path.GetDirectoryName( FilePath );
            if (!Directory.Exists( _directory ))
            {
                try
                {
                    Directory.CreateDirectory(_directory);
                }
                catch (Exception ex)
                {
                    OnErrorOccurred(ex);
                    return;
                }
            }
            try
            {
                _FileStream = new FileStream( FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None );//temp
            }
            catch (Exception ex)
            {
                OnErrorOccurred( new Exception( $"无法将文件保存到 [{FilePath}]\n\n{ex.Message}"));
                return;
            }
            #region //获取区块总数以及最后一个区块大小
            try
            {
                getBlockCount();
                getLastBlockSize();
            }
            catch (SocketException)
            {
                OnConnectLost();
                return;
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex);
                return;
            }
            #endregion
            List<int> _blocksRemaining = hashFile();
            if (_FileStream.Length > TotalSize)//如果已存在的文件比目标文件长则截断它
                _FileStream.SetLength(TotalSize);

            StartTime = DateTime.Now;
            foreach (int index in _blocksRemaining)
            {
                FileBlock _block = null;
                #region get block   
                while (true)
                {
                    if (!IsAlive)
                        _AutoResetEvent.WaitOne();
                    try
                    {
                        SendString($"{Consts.CMD_GET} {Consts.CMD_FILEBLOCK} {index}");
                        _block = receiveBlock();
                        break;
                    }
                    catch (SocketException)
                    {
                        OnConnectLost();
                        return;
                    }
                    catch (FileBlockException)
                    {
                        //接收到错误的区块,抛弃该数据并重新请求
                        _EastBlock.Add(index);
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred(ex);
                        return;
                    }
                }
                #endregion

                #region write block
                while (true)
                {
                    if (IsAlive == false)
                        _AutoResetEvent.WaitOne();
                    try
                    {
                        _Blocks[index] = _block;//写入区块
                        OnBlockFinished(index);
                        break;
                    }
                    catch (IOException ex)
                    {
                        OnErrorOccurred(ex);
                        return;
                    }
                    catch (Exception ex)
                    {
                        OnErrorOccurred(ex);
                        return;
                    }
                }
                #endregion
            }
            #region finnished
            try
            {
                SendStringAsync(Consts.CMD_EXIT);
            }
            catch (SocketException)
            {
                OnConnectLost();
                return;
            }
            catch (Exception ex)
            {
                OnErrorOccurred(ex);
                return;
            }
            #endregion
            _Blocks.WriteAllBlock();
            OnAllFinished();
            InnerStop(true);
        }
        /// <summary>
        /// （无try）校验文件
        /// </summary>
        /// <returns>损坏或尚未下载的区块序号列表</returns>
        private List<int> hashFile()
        {
            _FileStream.Position = 0;
            _ExistBlock.Clear();
            for (int count = 0; _FileStream.Position < _FileStream.Length && count < _TotalBlockCount; count++)
            {
                //校验已存在的区块
                FileBlock TestBlock = new FileBlock(this, count, true);
                SendString($"{Consts.CMD_GET} {Consts.CMD_BLOCKHASH} {count}");
                string[] _msgs = receiveString().Split(' ');
                if (_msgs[0] == Consts.CMD_BLOCKHASH)
                {
                    if (Convert.ToInt32(_msgs[1]) == count)
                    {
                        if (BitConverter.ToInt32(TestBlock.DataHash, 0) == Convert.ToInt32(_msgs[2]))
                            _ExistBlock.Add(count);
                    }
                }
                BlockHashed?.Invoke(this, new BlockFinishedEventArgs(count));
            }
            int _maxExistBlockIndex;//已存在的区块最大序号
            try
            {
                _maxExistBlockIndex = _ExistBlock.Max();
            }
            catch
            {
                _maxExistBlockIndex = 0;
            }
            List<int> _blockRemaining = new List<int>();
            for (int index = 0; index < _TotalBlockCount;)
            {
                //计算仍需传输的区块
                if (index <= _maxExistBlockIndex)
                {
                    if (_ExistBlock.Exists(a => a == index))
                    {
                        index++;
                        continue;
                    }
                }
                _blockRemaining.Add(index++);
            }
            return _blockRemaining;
        }
        #endregion

        #region protected methods
        /// <summary>
        /// 开始异步接收, 返回类型为IAayncResult的异常要单独捕获
        /// </summary>
        protected override IAsyncResult BeginReceive()
        {

            try
            {
                InitReceiveBuffer();
                return _Socket.BeginReceive(_ReceiveBuffer, 0, _ReceiveBuffer.Length, SocketFlags.None, null, null);
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

        #region public methods
        /// <summary>
        /// 开始传输
        /// </summary>
        public override void Start()
        {
            base.Start();
            _EastBlock = new List<int>();
            _ExistBlock = new List<int>();
            _downThread = new Thread(download);
            _downThread.IsBackground = true;
            _downThread.Name = "Download";
            _downThread.Start();
        }
        /// <summary>
        /// 中止传输
        /// </summary>
        /// <param name="ShutDownSocket">是否关闭Socket</param>
        public override void Stop(bool ShutDownSocket)
        {
            if (_downThread != null)
                if (_downThread.ThreadState == ThreadState.Running || _downThread.ThreadState == ThreadState.Background)
                {
                    _downThread.Abort();
                    while (_downThread.ThreadState != ThreadState.Stopped)
                    {
                        _downThread.Abort();
                        Thread.Sleep(10);
                    }
                    base.Stop(ShutDownSocket);
                }     
        }
        #endregion
    }
}
