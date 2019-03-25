using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Net.FileTransfer
{
    /// <summary>
    /// 一些常量和扩展方法
    /// </summary>
    public static class Consts
    {
        #region consts
        /// <summary>
        /// 文件区块数据标头
        /// </summary>
        public const byte FILE_BLOCK_HEADER = 0;
        /// <summary>
        /// 字符串信息标头
        /// </summary>
        public const byte StringHeader = 1;
        /// <summary>
        /// 分块大小1MB
        /// </summary>
        public const int BlockSize = 1048576;
        /// <summary>
        /// 网络上传送的数据包最大大小
        /// </summary>
        public const int NetBlockMaxSize = BlockSize + 9;
        /// <summary>
        /// 默认磁盘缓存大小(单位:区块数)
        /// </summary>
        public const int DefaultIOBufferSize = 8;
        /// <summary>
        /// 空格
        /// </summary>
        public const string Space = " ";
        /// <summary>
        /// 空格替代符
        /// </summary>
        public const string SpaceReplacement = @"|";

        public const string GET_FILE_BLOCK = "GET FILEBLOCK {0}";

        public const string CMD_EXIT = "Exit";

        public const string CMD_GET = "GET";

        public const string CMD_SET = "SET";

        public const string CMD_BLOCKHASH = "BLOCKHASH";

        public const string CMD_FILEBLOCK = "FILEBLOCK";

        public const string CMD_FILENAME = "FILENAME";

        public const string CMD_TOTALBLOCK = "TOTALBLOCK";

        public const string CMD_LASTBLOCKSIZE = "LASTBLOCKSIZE";

        public const string INFO_BADBLOCKINDEX = "Bad block index";

        public const string INFO_BADCOMMAND = "Bad command";

        public const string INFO_ISNOTALIVE = "Is not alive";

        public const string INFO_BADHEADER = "Bad header";
        #endregion

        #region public static methods
        /// <summary>
        /// 获取校验值
        /// </summary>
        /// <param name="bytes">输入数据</param>
        /// <returns>输出的校验值</returns>
        public static byte[] GetHash(this byte[] bytes)
        {
            return BitConverter.GetBytes(CRC32.GetCRC32(bytes));
        }
        /// <summary>
        /// 比较两二进制数据内容是否完全相同(用于MD5值的比较)
        /// </summary>
        /// <param name="THIS">数据一</param>
        /// <param name="obj">数据二</param>
        public static bool BytesEqual(this byte[] THIS, byte[] obj)
        {
            if (THIS.Length != obj.Length)
                return false;
            for (int index = 0; index < obj.Length; index++)
            {
                if (THIS[index] != obj[index])
                    return false;
            }
            return true;
        }
        /// <summary>
        /// 将指令字符串转化为二进制数据并添加标头
        /// </summary>
        public static byte[] ToBytes(this string str_input)
        {
            byte[] strdata = Encoding.UTF8.GetBytes(str_input);
            byte[] output = new byte[1 + strdata.Length];
            output[0] = StringHeader;
            System.Array.Copy(strdata, 0, output, 1, strdata.Length);
            return output;
        }
        /// <summary>
        /// 将二进制数据转化为指令字符串
        /// </summary>
        public static string ToFTString(this byte[] bytes_input)
        {
            if (bytes_input[0] != StringHeader)
                throw new FormatException("Bad Header!");
            return Encoding.UTF8.GetString(bytes_input, 1, bytes_input.Length - 1).TrimEnd('\0');
        }
        /// <summary>
        /// 替换可能会对命令解析造成干扰的字符
        /// </summary>
        public static string DoReplace(this string str_input)
        {
            return str_input.Replace(Space, SpaceReplacement);
        }
        /// <summary>
        /// 还原被替换的字符
        /// </summary>
        public static string DeReplace(this string str_input)
        {
            return str_input.Replace(SpaceReplacement, Space);
        }
    }
    #endregion
}
