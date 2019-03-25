using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Net.FileTransfer
{
    /// <summary>
    /// 文件区块的抽象集合
    /// 之所以说抽象是因为该集合并不存储实际的区块(缓存区除外)
    /// 而是通过一个索引器来读写文件
    /// 并提供磁盘缓存
    /// </summary>
    public class FileBlockCollection
    {
        #region private fields
        /// <summary>
        /// 与该区块关联的传输对象
        /// </summary>
        private Transfer _task;

        /// <summary>
        /// 与该区块关联的FileStream
        /// </summary>
        private FileStream _fileStream;
        #endregion

        #region internal fields
        internal bool _EnabledIOBuffer;

        /// <summary>
        /// 磁盘缓存区
        /// </summary>
        internal Dictionary<int, FileBlock> _IOBuffer;

        /// <summary>
        /// 获取或设置一个值,该值指示是否启用磁盘缓存
        /// </summary>
        internal bool EnabledIOBuffer
        {
            get { return _EnabledIOBuffer; }
            set
            {
                _EnabledIOBuffer = value;
                if (value)
                    _IOBuffer = new Dictionary<int, FileBlock>();
                else
                {
                    if (_task is FileReceiver)
                        WriteAllBlock();
                    _IOBuffer = null;
                }
            }
        }

        internal int _IOBufferSize;
        #endregion

        #region constructor
        /// <summary>
        /// 指定上传或下载任务初始化区块集合
        /// </summary>
        /// <param name="task"></param>
        public FileBlockCollection(Transfer task)
        {
            _task = task;
            _fileStream = _task.FileStream;
            _IOBufferSize = Consts.DefaultIOBufferSize;
        }
        #endregion

        #region public methods
        /// <summary>
        /// 获取已接收或已发送的区块序号列表
        /// </summary>
        public List<int> Finished { get { return _task._FinishedBlock; } }
        /// <summary>
        /// 获取已存在(Hash成功)的区块序号列表
        /// </summary>
        public List<int> Exist
        {
            get
            {
                if (_task is FileReceiver)
                    return ((FileReceiver)_task)._ExistBlock;
                else
                    return null;
            }
        }
        /// <summary>
        /// 获取被丢弃的区块序号列表
        /// </summary>
        public List<int> Cast
        {
            get
            {
                if (_task is FileReceiver)
                    return ((FileReceiver)_task)._EastBlock;
                else
                    return null;
            }
        }
        /// <summary>
        /// 获取总区块数
        /// </summary>
        public int Count { get { return _task._TotalBlockCount; } }
        /// <summary>
        /// 获取有效区块数(已存在+已接收)
        /// </summary>
        public int CountValid
        {
            get
            {
                if (_task is FileReceiver)
                    return _task._FinishedBlock.Count + ((FileReceiver)_task)._ExistBlock.Count;
                else
                    return _task._FinishedBlock.Count;

            }
        }
        /// <summary>
        /// 将缓存中的区块全部写入磁盘
        /// </summary>
        /// <returns>写入的区块数量</returns>
        public int WriteAllBlock()
        {
            if (!_EnabledIOBuffer)
                return -1;
            int count = 0;
            lock (_IOBuffer)
            {
                foreach (var b in _IOBuffer)
                {
                    b.Value.Write();
                    count++;
                }
                if (count != _IOBuffer.Count)
                    throw new IOException("Can not Write All FileBlocks!");
                _IOBuffer.Clear();
            }
            return count;
        }
        /// <summary>
        /// 读取数据以填充缓存
        /// </summary>
        /// <param name="StartIndex">起始区块</param>
        /// <returns>读取的区块数量</returns>
        public int FillIOBuffer(int StartIndex)
        {
            int Index;
            lock (_IOBuffer)
            {
                _IOBuffer.Clear();
                for (Index = StartIndex; _IOBuffer.Count < _IOBufferSize && Index < _task.Blocks.Count; Index++)
                {
                    _IOBuffer.Add(Index, new FileBlock(_task, Index, true));
                }
            }
            return Index - StartIndex;
        }
        /// <summary>
        /// 异步填充缓存
        /// </summary>
        /// <param name="StartIndex">起始区块</param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public IAsyncResult BeginFillIOBuffer(int StartIndex, AsyncCallback callback, object state)
        {
            return new Delegate_Int_Int(FillIOBuffer).BeginInvoke(StartIndex, callback, state);
        }
        /// <summary>
        /// 写入区块
        /// </summary>
        /// <param name="value">区块对象</param>
        public void Write(FileBlock value)
        {
            if (_EnabledIOBuffer)
            {
                if (_IOBuffer.Count >= _IOBufferSize)
                    WriteAllBlock();
                lock (_IOBuffer)
                    _IOBuffer.Add(value.Index, value);
            }
            else
                value.Write();
        }
        /// <summary>
        /// 读取或写入区块
        /// </summary>
        /// <param name="BlockIndex">区块序号</param>
        public FileBlock this[int BlockIndex]
        {
            get
            {
                FileBlock output;
                if (_EnabledIOBuffer)
                {

                    bool IsInBuf;
                    lock (_IOBuffer)
                        IsInBuf = _IOBuffer.TryGetValue(BlockIndex, out output);
                    if (IsInBuf)
                        return output;
                    else
                    {
                        output = new FileBlock(_task, BlockIndex, true);
                        BeginFillIOBuffer(BlockIndex + 1, null, null);
                    }
                }
                else
                    output = new FileBlock(_task, BlockIndex, true);
                return output;
            }
            set
            {
                if (BlockIndex != value.Index)
                    throw new FileBlockException("Bad Index!", FileBlockException.ErrorCode.BadIndex);
                Write(value);
            }
        }
        #endregion
    }
}
