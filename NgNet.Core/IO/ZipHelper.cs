using System;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

namespace NgNet.IO
{
    #region <class - Zip>
    public class ZipHelper
    {
        /// <summary>
        /// 打包
        /// </summary> 
        /// <param name="filename"> 压缩后的文件名(包含物理路径)</param>
        /// <param name="directory">待压缩的文件夹(包含物理路径)</param>
        public static void PackFiles(string filename, string directory)
        {
            FastZip fz = new FastZip();
            fz.CreateEmptyDirectories = true;
            fz.CreateZip(filename, directory, true, "");
            fz = null;
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="file">待解压文件名(包含物理路径)</param>
        /// <param name="dir"> 解压到哪个目录中(包含物理路径)</param>
        public static bool UnpackFiles(string file, string dir)
        {
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }
            ZipInputStream s = new ZipInputStream(System.IO.File.OpenRead(file));
            ZipEntry theEntry;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                string directoryName = System.IO.Path.GetDirectoryName(theEntry.Name);
                string fileName = System.IO.Path.GetFileName(theEntry.Name);
                if (directoryName != String.Empty)
                {
                    System.IO.Directory.CreateDirectory(dir + directoryName);
                }
                if (fileName != String.Empty)
                {
                    System.IO.FileStream streamWriter = System.IO.File.Create(dir + theEntry.Name);
                    int size = 2048;
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        size = s.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            break;
                        }
                    }
                    streamWriter.Close();
                }
            }
            s.Close();
            return true;
        }

        #region 私有方法
        /// <summary>
        /// 递归压缩文件夹方法
        /// </summary>
        private static bool ZipFileDictory(string FolderToZip, ZipOutputStream s, string ParentFolderName)
        {
            bool res = true;
            string[] folders, filenames;
            ZipEntry entry = null;
            System.IO.FileStream fs = null;
            Crc32 crc = new Crc32();
            try
            {
                entry = new ZipEntry(System.IO.Path.Combine(ParentFolderName, System.IO.Path.GetFileName(FolderToZip) + "/"));
                s.PutNextEntry(entry);
                s.Flush();
                filenames = System.IO.Directory.GetFiles(FolderToZip);
                foreach (string file in filenames)
                {
                    fs = System.IO.File.OpenRead(file);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    entry = new ZipEntry(System.IO.Path.Combine(ParentFolderName, System.IO.Path.GetFileName(FolderToZip) + "/" + System.IO.Path.GetFileName(file)));
                    entry.DateTime = DateTime.Now;
                    entry.Size = fs.Length;
                    fs.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    s.PutNextEntry(entry);
                    s.Write(buffer, 0, buffer.Length);
                }
            }
            catch
            {
                res = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs = null;
                }
                if (entry != null)
                {
                    entry = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            folders = System.IO.Directory.GetDirectories(FolderToZip);
            foreach (string folder in folders)
            {
                if (!ZipFileDictory(folder, s, System.IO.Path.Combine(ParentFolderName, System.IO.Path.GetFileName(FolderToZip))))
                {
                    return false;
                }
            }
            return res;
        }

        /// <summary>
        /// 压缩目录
        /// </summary>
        /// <param name="FolderToZip">待压缩的文件夹，全路径格式</param>
        /// <param name="ZipedFile">压缩后的文件名，全路径格式</param>
        private static bool ZipFileDictory(string FolderToZip, string ZipedFile, int level)
        {
            bool res;
            if (!System.IO.Directory.Exists(FolderToZip))
            {
                return false;
            }
            ZipOutputStream s = new ZipOutputStream(System.IO.File.Create(ZipedFile));
            s.SetLevel(level);
            res = ZipFileDictory(FolderToZip, s, "");
            s.Finish();
            s.Close();
            return res;
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="FileToZip">要进行压缩的文件名</param>
        /// <param name="ZipedFile">压缩后生成的压缩文件名</param>
        private static bool ZipFile(string FileToZip, string ZipedFile, int level)
        {
            if (!System.IO.File.Exists(FileToZip))
            {
                throw new System.IO.FileNotFoundException("指定要压缩的文件: " + FileToZip + " 不存在!");
            }
            System.IO.FileStream ZipFile = null;
            ZipOutputStream ZipStream = null;
            ZipEntry ZipEntry = null;
            bool res = true;
            try
            {
                ZipFile = System.IO.File.OpenRead(FileToZip);
                byte[] buffer = new byte[ZipFile.Length];
                ZipFile.Read(buffer, 0, buffer.Length);
                ZipFile.Close();

                ZipFile = System.IO.File.Create(ZipedFile);
                ZipStream = new ZipOutputStream(ZipFile);
                ZipEntry = new ZipEntry(System.IO.Path.GetFileName(FileToZip));
                ZipStream.PutNextEntry(ZipEntry);
                ZipStream.SetLevel(level);

                ZipStream.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                res = false;
            }
            finally
            {
                if (ZipEntry != null)
                {
                    ZipEntry = null;
                }
                if (ZipStream != null)
                {
                    ZipStream.Finish();
                    ZipStream.Close();
                }
                if (ZipFile != null)
                {
                    ZipFile.Close();
                    ZipFile = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            return res;
        }
        #endregion

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="FileToZip">待压缩的文件目录</param>
        /// <param name="ZipedFile">生成的目标文件</param>
        /// <param name="level">压缩比</param>
        public static bool Zip(String FileToZip, String ZipedFile, int level)
        {
            if (System.IO.Directory.Exists(FileToZip))
            {
                return ZipFileDictory(FileToZip, ZipedFile, level);
            }
            else if (System.IO.File.Exists(FileToZip))
            {
                return ZipFile(FileToZip, ZipedFile, level);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="FileToUpZip">待解压的文件</param>
        /// <param name="ZipedFolder">解压目标存放目录</param>
        public static void UnZip(string FileToUpZip, string ZipedFolder)
        {
            if (!System.IO.File.Exists(FileToUpZip))
            {
                return;
            }
            if (!System.IO.Directory.Exists(ZipedFolder))
            {
                System.IO.Directory.CreateDirectory(ZipedFolder);
            }
            ZipInputStream s = null;
            ZipEntry theEntry = null;
            string fileName;
            System.IO.FileStream streamWriter = null;
            try
            {
                s = new ZipInputStream(System.IO.File.OpenRead(FileToUpZip));
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.Name != String.Empty)
                    {
                        fileName = System.IO.Path.Combine(ZipedFolder, theEntry.Name);
                        if (fileName.EndsWith("/") || fileName.EndsWith("\\"))
                        {
                            System.IO.Directory.CreateDirectory(fileName);
                            continue;
                        }
                        streamWriter = System.IO.File.Create(fileName);
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter = null;
                }
                if (theEntry != null)
                {
                    theEntry = null;
                }
                if (s != null)
                {
                    s.Close();
                    s = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
        }

        #region winrar
        #region 私有变量
        String the_rar;
        Microsoft.Win32.RegistryKey the_Reg;
        Object the_Obj;
        String the_Info;
        System.Diagnostics.ProcessStartInfo the_StartInfo;
        System.Diagnostics.Process the_Process;
        #endregion

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="zipname">要解压的文件名</param>
        /// <param name="zippath">要压缩的文件目录</param>
        /// <param name="dirpath">初始目录</param>
        public void RarEnZip(string zipname, string zippath, string dirpath)
        {
            try
            {
                the_Reg = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@"Applications\WinRAR.exe\Shell\Open\Command");
                the_Obj = the_Reg.GetValue("");
                the_rar = the_Obj.ToString();
                the_Reg.Close();
                the_rar = the_rar.Substring(1, the_rar.Length - 7);
                the_Info = " a    " + zipname + "  " + zippath;
                the_StartInfo = new System.Diagnostics.ProcessStartInfo();
                the_StartInfo.FileName = the_rar;
                the_StartInfo.Arguments = the_Info;
                the_StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                the_StartInfo.WorkingDirectory = dirpath;
                the_Process = new System.Diagnostics.Process();
                the_Process.StartInfo = the_StartInfo;
                the_Process.Start();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="zipname">要解压的文件名</param>
        /// <param name="zippath">要解压的文件路径</param>
        public void RarDeZip(string zipname, string zippath)
        {
            try
            {
                the_Reg = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(@"Applications\WinRar.exe\Shell\Open\Command");
                the_Obj = the_Reg.GetValue("");
                the_rar = the_Obj.ToString();
                the_Reg.Close();
                the_rar = the_rar.Substring(1, the_rar.Length - 7);
                the_Info = " X " + zipname + " " + zippath;
                the_StartInfo = new System.Diagnostics.ProcessStartInfo();
                the_StartInfo.FileName = the_rar;
                the_StartInfo.Arguments = the_Info;
                the_StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                the_Process = new System.Diagnostics.Process();
                the_Process.StartInfo = the_StartInfo;
                the_Process.Start();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
    #endregion
}
