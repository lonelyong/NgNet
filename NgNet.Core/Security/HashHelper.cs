using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace NgNet.Security
{
    public static class HashHelper
    {
        #region HashText
        #region MD5
        /// <summary>
        /// 获取文本32位MD5
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetTextMD5_32(string text)
        {
            if (string.IsNullOrEmpty(text)) { return string.Empty; }
            MD5 md5 = MD5.Create();
            byte[] bytValue = Encoding.UTF8.GetBytes(text);
            byte[] bytHash = md5.ComputeHash(bytValue);
            md5.Clear();
            string sHash = ConvertHashBytes(bytHash);
            return sHash;
        }
        /// <summary>
        /// 获取文本16位MD5
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetTextMD5(string text)
        {
            if (string.IsNullOrEmpty(text)) { return string.Empty; }
            string sHash = GetTextMD5_32(text).Substring(8, 16);
            return sHash;
        }
        #endregion

        #region SHA
        /// <summary>
        /// 获取文本SHA1
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetTextSHA_1(string text)
        {
            if (string.IsNullOrEmpty(text)) { return ""; }
            System.Security.Cryptography.SHA1CryptoServiceProvider SHA1CSP = new System.Security.Cryptography.SHA1CryptoServiceProvider();

            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] bytHash = SHA1CSP.ComputeHash(bytValue);
            SHA1CSP.Clear();

            //根据计算得到的Hash码翻译为SHA-1码
            string sHash = ConvertHashBytes(bytHash);
            //根据大小写规则决定返回的字符串
            return sHash.ToLower();
        }
        /// <summary>
        /// 获取文本SHA256
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetTextSHA_256(string text)
        {
            if (string.IsNullOrEmpty(text)) { return ""; }
            System.Security.Cryptography.SHA256CryptoServiceProvider SHA256CSP = new System.Security.Cryptography.SHA256CryptoServiceProvider();

            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] bytHash = SHA256CSP.ComputeHash(bytValue);
            SHA256CSP.Clear();

            //根据计算得到的Hash码翻译为SHA-1码
            string sHash = ConvertHashBytes(bytHash);
            //根据大小写规则决定返回的字符串
            return sHash.ToLower();
        }
        /// <summary>
        /// 获取文本SHA384
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetTextSHA_384(string text)
        {
            if (string.IsNullOrEmpty(text)) { return ""; }
            System.Security.Cryptography.SHA384CryptoServiceProvider SHA384CSP = new System.Security.Cryptography.SHA384CryptoServiceProvider();
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] bytHash = SHA384CSP.ComputeHash(bytValue);
            SHA384CSP.Clear();

            //根据计算得到的Hash码翻译为SHA-1码
            string sHash = ConvertHashBytes(bytHash);
            //根据大小写规则决定返回的字符串
            return sHash.ToLower();
        }
        /// <summary>
        /// 获取文本SHA512
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetTextSHA_512(string text)
        {
            if (string.IsNullOrEmpty(text)) { return string.Empty; }
            System.Security.Cryptography.SHA512CryptoServiceProvider SHA512CSP
             = new System.Security.Cryptography.SHA512CryptoServiceProvider();

            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(text);
            byte[] bytHash = SHA512CSP.ComputeHash(bytValue);
            SHA512CSP.Clear();

            //根据计算得到的Hash码翻译为SHA-1码
            string sHash = ConvertHashBytes(bytHash);
            //根据大小写规则决定返回的字符串
            return sHash.ToLower();
        }

        public static string ConvertHashBytes(byte[] bytHash)
        {

            #region method one
            /*/根据计算得到的Hash码翻译为16进制码
            string sHash = "", sTemp = "";
            for (int counter = 0; counter < bytHash.Count(); counter++)
            {
                long i = bytHash[counter] / 16;
                if (i > 9)
                {
                    sTemp = ((char)(i - 10 + 0x41)).ToString();
                }
                else
                {
                    sTemp = ((char)(i + 0x30)).ToString();
                }
                i = bytHash[counter] % 16;
                if (i > 9)
                {
                    sTemp += ((char)(i - 10 + 0x41)).ToString();
                }
                else
                {
                    sTemp += ((char)(i + 0x30)).ToString();
                }
                sHash += sTemp;
            }
            return sHash;
             **/
            #endregion

            #region method two
            StringBuilder sb = new StringBuilder();
            if (bytHash == null)
                return string.Empty;
            else
                foreach (var byt in bytHash)
                {
                    sb.Append(byt.ToString("X2"));
                }
            return sb.ToString();
            #endregion
        }



        #endregion
        #endregion

        #region HashFile
        /// <summary>
        /// 计算文件的 MD5 值
        /// </summary>
        /// <param name="path">要计算 MD5 值的文件名和路径</param>
        /// <returns>MD5 值16进制字符串</returns>
        public static string GetFileMD5(string path)
        {
            if (System.IO.File.Exists(path) == false)
                return string.Empty;

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            MD5 md5 = MD5Cng.Create();
            byte[] hashByts = md5.ComputeHash(fs);
            return ConvertHashBytes(hashByts);
        }
        /// <summary>
        /// 计算文件的 sha1 值
        /// </summary>
        /// <param name="path">要计算 MD5 值的文件名和路径</param>
        /// <returns>MD5 值16进制字符串</returns>
        public static string GetFileSHA1(string path)
        {
            if (System.IO.File.Exists(path) == false)
                return string.Empty;

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            SHA1 sha1 = SHA1Cng.Create();
            byte[] hashByts = sha1.ComputeHash(fs);
            return ConvertHashBytes(hashByts);
        }
        /// <summary>
        /// 计算文件的 sha256 值
        /// </summary>
        /// <param name="path">要计算 MD5 值的文件名和路径</param>
        /// <returns>MD5 值16进制字符串</returns>
        public static string GetFileSHA256(string path)
        {
            if (System.IO.File.Exists(path) == false)
                return string.Empty;

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            SHA256 sha256 = SHA256Cng.Create();
            byte[] hashByts = sha256.ComputeHash(fs);
            return ConvertHashBytes(hashByts);
        }
        /// <summary>
        /// 计算文件的 sha384 值
        /// </summary>
        /// <param name="path">要计算 MD5 值的文件名和路径</param>
        /// <returns>MD5 值16进制字符串</returns>
        public static string GetFileSHA384(string path)
        {
            if (System.IO.File.Exists(path) == false)
                return string.Empty;

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            SHA384 sha384 = SHA384Cng.Create();
            byte[] hashByts = sha384.ComputeHash(fs);
            return ConvertHashBytes(hashByts);
        }
        /// <summary>
        /// 计算文件的 sha512 值
        /// </summary>
        /// <param name="path">要计算 MD5 值的文件名和路径</param>
        /// <returns>MD5 值16进制字符串</returns>
        public static string GetFileSHA512(string path)
        {
            if (System.IO.File.Exists(path) == false)
                return string.Empty;

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            SHA512 sha512 = SHA512Cng.Create();
            byte[] hashByts = sha512.ComputeHash(fs);
            return ConvertHashBytes(hashByts);
        }
        /// <summary>
        /// 字节数组转换为16进制表示的字符串
        /// </summary>
        #endregion
    }
}
