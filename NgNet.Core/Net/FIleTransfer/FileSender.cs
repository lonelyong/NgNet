using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Net.FileTransfer
{
    /// <summary>
    /// 发送端
    /// 传输前发送端创建该类实例
    /// 设置必要属性后
    /// 调用Start()方法开始传输
    /// </summary>
    public class FileSender : Transfer
    {
        #region public properties
        /// <summary>
        /// 获取估计剩余时间
        /// </summary>
        public override TimeSpan TimeRemaining
        {
            get
            {
                int _blockRemaining = _TotalBlockCount - _FinishedBlock.Count;
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
                return (long)_FinishedBlock.Count * Consts.BlockSize;
            }
        }
        #endregion

        #region private methods
        /// <summary>
        /// （有try）
        /// </summary>
        /// <param name="ar"></param>
        private void receiveCallback(IAsyncResult ar)
        {
            if (ar.AsyncState is SocketException)
            {
                if (IsAlive) OnConnectLost();
            }
            else if (ar.AsyncState is Exception)
            {
                if (IsAlive) OnErrorOccurred(ar.AsyncState as Exception);
            }
            else
            {
                bool _continueReceive = false;
                int _count = 0;
                try
                {
                    _count = _Socket.EndReceive(ar);
                }
                catch (SocketException)
                {
                    if (IsAlive) OnConnectLost();
                }
                catch (Exception ex)
                {
                    if (IsAlive) OnErrorOccurred(ex);
                }
                if (_count == 0)
                    return;
                switch (_ReceiveBuffer[0])
                {
                    case Consts.StringHeader:
                        try
                        {
                            _continueReceive = onCommandReceived(_ReceiveBuffer.ToFTString());
                        }
                        catch (SocketException)
                        {
                            if (IsAlive) OnConnectLost();
                        }
                        catch (Exception ex)
                        {
                            if (IsAlive) OnErrorOccurred(ex);
                        }
                        break;
                    default:
                        throw new FormatException(Consts.INFO_BADHEADER);
                }
                if (_continueReceive)
                    BeginReceive();
            }
        }
        /// <summary>
        /// （无try）命令处理
        /// </summary>
        /// <param name="str">收到的命令</param>
        /// <returns>是否继续接收</returns>
        private bool onCommandReceived(string str)
        {
            bool _continueReceive = true;
            string[] _msgs = str.Split(' ');
            if (_msgs[0] == Consts.CMD_EXIT)
            {
                _continueReceive = false;
                OnAllFinished();
                InnerStop(true);
            }
            else if (_msgs[0] == Consts.CMD_GET)
            {
                if (_msgs[1] == Consts.CMD_FILEBLOCK)
                {
                    int _blockIndex;
                    if (!int.TryParse(_msgs[2], out _blockIndex))
                        throw new FormatException($"{Consts.INFO_BADBLOCKINDEX}{_msgs[2]}");
                    sendBlock(_blockIndex);
                }
                else if (_msgs[1] == Consts.CMD_BLOCKHASH)
                {
                    int _blockIndex;
                    if (!int.TryParse(_msgs[2], out _blockIndex))
                        throw new FormatException($"{Consts.INFO_BADBLOCKINDEX}{_msgs[2]}");
                    byte[] _hash = _Blocks[_blockIndex].DataHash;
                    SendStringAsync(string.Format("{0} {1} {2}", Consts.CMD_BLOCKHASH, _blockIndex, BitConverter.ToInt32(_hash, 0)));
                }
                else if (_msgs[1] == Consts.CMD_FILENAME)
                {
                    SendStringAsync($"{Consts.CMD_SET} {Consts.CMD_FILENAME} {FileName.DoReplace()}");
                }
                else if (_msgs[1] == Consts.CMD_TOTALBLOCK)
                {
                    SendStringAsync($"{Consts.CMD_SET} {Consts.CMD_TOTALBLOCK} {_TotalBlockCount}");
                }
                else if (_msgs[1] == Consts.CMD_LASTBLOCKSIZE)
                {
                    SendStringAsync($"{Consts.CMD_SET} {Consts.CMD_LASTBLOCKSIZE} {_LastBlockSize}");
                }
                else
                    throw new FormatException($"{Consts.INFO_BADCOMMAND} {_msgs[1]}");
            }
            else
                throw new FormatException($"{Consts.INFO_BADCOMMAND} {_msgs[1]}");

            return _continueReceive;
        }
        /// <summary>
        /// （无try）同步发送区块
        /// </summary>
        /// <param name="blockIndex">区块序号</param>
        /// <returns>发送的数据长度</returns>
        private int sendBlock(int blockIndex)
        {
            int ret = _Socket.EndSend(beginSendBlock(blockIndex, null, null));
            OnBlockFinished(blockIndex);
            return ret;
        }
        /// <summary>
        /// （无try）异步发送区块并使用指定的回调方法和参数
        /// </summary>
        /// <param name="blockIndex">区块序号</param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        private IAsyncResult beginSendBlock(int blockIndex, AsyncCallback callback, object state)
        {
            if (!IsAlive)
                return _AsyncReturnException.BeginInvoke(new InvalidOperationException(Consts.INFO_ISNOTALIVE), null, null);
            if (blockIndex >= _TotalBlockCount)
                return _AsyncReturnException.BeginInvoke(new ArgumentOutOfRangeException(Consts.INFO_BADBLOCKINDEX), null, null);

            byte[] _send = _Blocks[blockIndex].GetBytes();
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

        /// <summary>
        /// （无try）开始异步接收
        /// </summary>
        protected override IAsyncResult BeginReceive()
        {
            try
            {
                InitReceiveBuffer();
                return _Socket.BeginReceive(_ReceiveBuffer, 0, _ReceiveBuffer.Length, SocketFlags.None, receiveCallback, null);
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
        /// （无try）开始传输
        /// </summary>
        public override void Start()
        {
            _FileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            _TotalBlockCount = (int)(_FileStream.Length / Consts.BlockSize) + 1;
            _LastBlockSize = (int)(_FileStream.Length - (long)(_TotalBlockCount - 1) * Consts.BlockSize);
            base.Start();
            BeginReceive();
        }
        #endregion
    }
}
