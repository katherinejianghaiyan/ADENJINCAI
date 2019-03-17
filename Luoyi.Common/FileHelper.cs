using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;


namespace Luoyi.Common
{
    /// <summary>
    /// �ı��ļ��Ӵ��̶�ȡ��д��
    /// </summary>
    public class FileHelper
    {       

        /// <summary>
        /// ���ļ��ж�ȡ�ı�����
        /// </summary>
        /// <param name="filePath">�ı�·��</param>
        /// <returns>�����ļ��е�����</returns>
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
        /// д���ı��ļ�����Ĭ�ϱ���
        /// </summary>
        /// <param name="filePath">�ı��ļ�·�������ļ���</param>
        /// <param name="content">д������</param>
        public static void Write(string filePath, string content)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.Default))
            {
                sw.Write(content);
                sw.Close();
            }
        }
        /// <summary>
        /// д���ı��ļ�����UFT8��ʽ����
        /// </summary>
        /// <param name="filePath">�ı��ļ�·�������ļ���</param>
        /// <param name="content">д������</param>
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
        /// �����ļ�
        /// </summary>
        /// <param name="sourceFileName">Դ�ļ�·��</param>
        /// <param name="destFileName">Ŀ���ļ�·��</param>
        /// <param name="overwrite">�Ƿ񸲸��Ѵ����ļ�</param>
        /// <returns></returns>
        public static bool BackupFile(string sourceFileName, string destFileName, bool overwrite)
        {
            bool flag;
            if (!File.Exists(sourceFileName))
            {
                throw new FileNotFoundException(sourceFileName + "�ļ������ڣ�");
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
        /// �����ļ����ļ�
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
        /// ɾ���ļ����ļ�
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
        /// ����ļ�����޸�ʱ��
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
        /// ȡ���ļ���ֻ������
        /// </summary>
        /// <param name="file"></param>
        public static void RemoveFileReadOnlyAttribute(string file)
        {
            File.SetAttributes(file, File.GetAttributes(file) & ~FileAttributes.ReadOnly);
        }

        /// <summary>
        /// �ϴ��ļ���������
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
