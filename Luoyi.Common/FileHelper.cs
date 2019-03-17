using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;


namespace Luoyi.Common
{
    /// <summary>
    /// 文本文件从磁盘读取、写入
    /// </summary>
    public class FileHelper
    {       

        /// <summary>
        /// 从文件中读取文本内容
        /// </summary>
        /// <param name="filePath">文本路径</param>
        /// <returns>返回文件中的内容</returns>
        public static string Read(string filePath)
        {
            string result = string.Empty;
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.Default))
                {
                    result = sr.ReadToEnd();
                    sr.Close();
                    return result;
                }
            }
            return result;
        }

        /// <summary>
        /// 写入文本文件，按默认编码
        /// </summary>
        /// <param name="filePath">文本文件路径包括文件名</param>
        /// <param name="content">写入内容</param>
        public static void Write(string filePath, string content)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
            {
                sw.Write(content);
                sw.Close();
            }
        }
        /// <summary>
        /// 写入文本文件，以UFT8格式编码
        /// </summary>
        /// <param name="filePath">文本文件路径包括文件名</param>
        /// <param name="content">写入内容</param>
        /// <param name="UTF8"></param>
        public static void Write(string filePath, string content, bool UTF8)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                sw.Write(content);
                sw.Close();
            }
        }

        /// <summary>
        /// 备份文件
        /// </summary>
        /// <param name="sourceFileName">源文件路径</param>
        /// <param name="destFileName">目标文件路径</param>
        /// <param name="overwrite">是否覆盖已存在文件</param>
        /// <returns></returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            bool flag;
            if (!File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "文件不存在！");
            }
            if (!overwrite && File.Exists(destFileName))
            {
                return false;
            }
            try
            {
                File.Copy(sourceFileName, destFileName, true);
                flag = true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return flag;
        }

        /// <summary>
        /// 拷贝文件夹文件
        /// </summary>
        /// <param name="srcDir"></param>
        /// <param name="dstDir"></param>
        public static void CopyDirFiles(string srcDir, string dstDir)
        {
            if (Directory.Exists(srcDir))
            {
                foreach (string str in Directory.GetFiles(srcDir))
                {
                    string destFileName = Path.Combine(dstDir, Path.GetFileName(str));
                    File.Copy(str, destFileName, true);
                }

                foreach (string str3 in Directory.GetDirectories(srcDir))
                {
                    string str4 = str3.Substring(str3.LastIndexOf(@"\") + 1);
                    CopyDirFiles(Path.Combine(srcDir, str4), Path.Combine(dstDir, str4));
                }
            }
        }

        /// <summary>
        /// 删除文件夹文件
        /// </summary>
        /// <param name="dir"></param>
        public static void DeleteDirFiles(string dir)
        {
            if (Directory.Exists(dir))
            {
                foreach (string str in Directory.GetFiles(dir))
                {
                    File.Delete(str);
                }
                foreach (string str2 in Directory.GetDirectories(dir))
                {
                    Directory.Delete(str2, true);
                }
            }
        }

        /// <summary>
        /// 获得文件最后修改时间
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static DateTime GetFileLastWriteTime(string file)
        {
            if (File.Exists(file))
            {
                return File.GetLastWriteTime(file);
            }
            return DateTime.MaxValue;
        }

        /// <summary>
        /// 取消文件的只读属性
        /// </summary>
        /// <param name="file"></param>
        public static void RemoveFileReadOnlyAttribute(string file)
        {
            File.SetAttributes(file, File.GetAttributes(file) & ~FileAttributes.ReadOnly);
        }

        /// <summary>
        /// 上传文件到服务器
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool UploadFile(string filePath)
        {
            var flist = HttpContext.Current.Request.Files;
            if (flist!=null && flist.Count>0)
            {
                for (int i = 0; i < flist.Count; i++)
                {
                    try
                    {
                        var c = flist[i];
                        filePath = Path.Combine(filePath, c.FileName);

                        c.SaveAs(filePath);
                    }
                    catch (Exception exception)
                    {

                        throw exception;
                    }
                }
            }
            
            return true;

        }
    }
}
