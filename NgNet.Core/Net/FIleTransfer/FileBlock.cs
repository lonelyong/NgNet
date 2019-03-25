using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NgNet.Net.FileTransfer
{
    /// <summary>
    /// 文件区块
    /// </summary>
    public class FileBlock : IComparable<FileBlock>
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
        /// <summary>
        /// 文件数据
        /// </summary>
        private byte[] _data;
        /// <summary>
        /// 
        /// </summary>
        private byte[] _header = new byte[] { Consts.FILE_BLOCK_HEADER};
        #endregion

        #region public properties
        /// <summary>
        /// 获取或设置该区块的序号(该区块在文件中的位置)
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 获取该区块的数据长度
        /// </summary>
        public int DataLength { get; private set; }
        /// <summary>
        /// 获取该数据块的校验值
        /// </summary>
        public byte[] DataHash { get; private set; }
        #endregion

        #region consructor
        /// <summary>
        /// 构造函数
        /// 用于从文件读入区块
        /// </summary>
        /// <param name="task"></param>
        /// <param name="blockIndex">分块位置</param>
        /// <param name="readOnCreated">是否立即从文件读取数据</param>
        public FileBlock(Transfer task, int blockIndex, bool readOnCreated)
        {
            _task = task;
            _fileStream = _task.FileStream;
            Index = blockIndex;
            if (readOnCreated)
                Read(true);
        }
        /// <summary>
        /// 构造函数
        /// 用于从二进制数据读入区块
        /// </summary>
        /// <param name="task"></param>
        /// <param name="ReceivedData">输入的二进制数据</param>
        public FileBlock(Transfer task, byte[] ReceivedData)
        {
            _task = task;
            _fileStream = _task.FileStream;
            if (ReceivedData[0] != Consts.FILE_BLOCK_HEADER)
                throw new FileBlockException("Bad Header!", FileBlockException.ErrorCode.BadHeader);
            Index = BitConverter.ToInt32(ReceivedData, 1);
            DataLength = ReceivedData.Length - 9;
            if (DataLength > Consts.BlockSize)
                throw new FileBlockException("Illegal FileBlock Size!", FileBlockException.ErrorCode.IllegalFileBlockSize);
            _data = new byte[DataLength];
            DataHash = new byte[4];
            System.Array.Copy(ReceivedData, 5, DataHash, 0, 4);
            System.Array.Copy(ReceivedData, 9, _data, 0, DataLength);
            if (!DataHash.BytesEqual(_data.GetHash()))
                throw new FileBlockException("Error Hash!", FileBlockException.ErrorCode.ChecksumError);
        }
        #endregion

        #region public methods
        /// <summary>
        /// 从文件读入
        /// </summary>
        /// <param name="calcHashAfterRead">是否在读取后立即计算校验值</param>
        /// <returns>读取块的大小</returns>
        public int Read(bool calcHashAfterRead)
        {
            _data = new byte[Consts.BlockSize];
            lock (_fileStream)
            {
                _fileStream.Position = (long)Index * Consts.BlockSize;
                DataLength = _fileStream.Read(_data, 0, Consts.BlockSize);
            }
            if (_data.Length != DataLength)
            {
                byte[] old = _data;
                _data = new byte[DataLength];
                Array.Copy(old, _data, DataLength);
            }
            if (calcHashAfterRead)
                CalcHash();
            return DataLength;
        }
        /// <summary>
        /// 计算校验值
        /// </summary>
        /// <returns>校验值</returns>
        public byte[] CalcHash()
        {
            return DataHash = _data.GetHash();
        }
        /// <summary>
        /// 将该区块写入文件
        /// </summary>
        public void Write()
        {
            lock (_fileStream)
            {
                _fileStream.Position = (long)Index * Consts.BlockSize;
                _fileStream.Write(_data, 0, DataLength);
            }
        }
        /// <summary>
        /// 转化为二进制数据以传输
        /// </summary>
        /// <returns></returns>
        public byte[] GetBytes()
        {
            MemoryStream _mStream = new MemoryStream(9 + DataLength); // 9=1+4+4
            _mStream.Write(_header, 0, 1);
            _mStream.Write(BitConverter.GetBytes(Index), 0, 4);
            _mStream.Write(DataHash, 0, 4);
            _mStream.Write(_data, 0, DataLength);
            return _mStream.ToArray();
        }
        #endregion

        #region IComparable
        /// <summary>
        /// 比较两个区块的是否相等，根据区块的索引比较
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(FileBlock obj)
        {
            return (Index as IComparable<int>).CompareTo(obj.Index);
        }
        #endregion
    }
}
