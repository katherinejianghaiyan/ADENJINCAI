using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Luoyi.Common
{
    /// <summary>
    /// ���ô���IP���ݿ�(QQWry.dat)��ѯIP���ڵ�
    /// �ŵ㣺��ѯ�ٶȿ졢�������IP��
    /// </summary>
    public static class IPScanner
    {
        private static string dataPath = HttpContext.Current.Server.MapPath("/Plugin/qqwry.dat");
        private static string ip;
        private static string country;
        private static string local;

        private static long firststartIP = 0;
        private static long laststartIP = 0;
        private static FileStream objfs = null;
        private static long startIP = 0;
        private static long endIP = 0;
        private static int countryFlag = 0;
        private static long endIPOff = 0;
        //private static string errMsg = null;

        #region ����ƥ������
        private static int QQwry()
        {
            long ip_Int = IPToInt(ip);
            int nRet = 0;
            if (ip_Int >= IPToInt("127.0.0.0") && ip_Int <= IPToInt("127.255.255.255"))
            {
                country = "�����ڲ����ص�ַ";
                local = "";
                nRet = 1;
            }
            else if ((ip_Int >= IPToInt("0.0.0.0") && ip_Int <= IPToInt("2.255.255.255")) || (ip_Int >= IPToInt("64.0.0.0") && ip_Int <= IPToInt("126.255.255.255")) || (ip_Int >= IPToInt("58.0.0.0") && ip_Int <= IPToInt("60.255.255.255")))
            {
                country = "���籣����ַ";
                local = "";
                nRet = 1;
            }
            objfs = new FileStream(dataPath, FileMode.Open, FileAccess.Read);
            try
            {
                //objfs.Seek(0,SeekOrigin.Begin);
                objfs.Position = 0;
                byte[] buff = new Byte[8];
                objfs.Read(buff, 0, 8);
                firststartIP = buff[0] + buff[1] * 256 + buff[2] * 256 * 256 + buff[3] * 256 * 256 * 256;
                laststartIP = buff[4] * 1 + buff[5] * 256 + buff[6] * 256 * 256 + buff[7] * 256 * 256 * 256;
                long recordCount = Convert.ToInt64((laststartIP - firststartIP) / 7.0);
                if (recordCount <= 1)
                {
                    country = "FileDataError";
                    objfs.Close();
                    return 2;
                }
                long rangE = recordCount;
                long rangB = 0;
                long recNO = 0;
                while (rangB < rangE - 1)
                {
                    recNO = (rangE + rangB) / 2;
                    GetstartIP(recNO);
                    if (ip_Int == startIP)
                    {
                        rangB = recNO;
                        break;
                    }
                    if (ip_Int > startIP)
                        rangB = recNO;
                    else
                        rangE = recNO;
                }
                GetstartIP(rangB);
                GetendIP();
                if (startIP <= ip_Int && endIP >= ip_Int)
                {
                    GetCountry();
                    local = local.Replace("������һ��Ҫ���̨�壡������", "");
                }
                else
                {
                    nRet = 3;
                    country = "δ֪";
                    local = "";
                }
                objfs.Close();
                return nRet;
            }
            catch
            {
                return 1;
            }

        }
        #endregion

        #region ����IP�Ƿ�Ϸ�
        /// <summary>
        /// ����IP�Ƿ�Ϸ�
        /// </summary>
        /// <param name="checkIP">��Ҫ�����IP</param>
        /// <returns>True �Ϸ�/False �Ƿ�</returns>
        public static bool CheckIP(string checkIP)
        {
            string pattern = @"(((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))\.){3}((\d{1,2})|(1\d{2})|(2[0-4]\d)|(25[0-5]))";
            Regex objRe = new Regex(pattern + "IP");
            Match objMa = objRe.Match(checkIP + "IP");
            if (objMa.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region IP�ַ���ת����Int����
        /// <summary>
        /// ��IP�ַ���ת����Int����
        /// </summary>
        /// <param name="ip">IP�ַ���</param>
        /// <returns>Int����</returns>
        public static long IPToInt(string ip)
        {
            char[] dot = new char[] { '.' };
            string[] ipArr = ip.Split(dot);
            if (ipArr.Length == 3)
                ip = ip + ".0";
            ipArr = ip.Split(dot);

            long ip_Int = 0;
            long p1 = long.Parse(ipArr[0]) * 256 * 256 * 256;
            long p2 = long.Parse(ipArr[1]) * 256 * 256;
            long p3 = long.Parse(ipArr[2]) * 256;
            long p4 = long.Parse(ipArr[3]);
            ip_Int = p1 + p2 + p3 + p4;
            return ip_Int;
        }
        #endregion

        #region Int����ת����IP�ַ���
        /// <summary>
        /// ��Int����ת����IP�ַ���
        /// </summary>
        /// <param name="ip_Int"></param>
        /// <returns></returns>
        public static string IntToIP(long ip_Int)
        {
            long seg1 = (ip_Int & 0xff000000) >> 24;
            if (seg1 < 0)
                seg1 += 0x100;
            long seg2 = (ip_Int & 0x00ff0000) >> 16;
            if (seg2 < 0)
                seg2 += 0x100;
            long seg3 = (ip_Int & 0x0000ff00) >> 8;
            if (seg3 < 0)
                seg3 += 0x100;
            long seg4 = (ip_Int & 0x000000ff);
            if (seg4 < 0)
                seg4 += 0x100;
            string ip = seg1.ToString() + "." + seg2.ToString() + "." + seg3.ToString() + "." + seg4.ToString();

            return ip;
        }
        #endregion

        #region ��ȡ��ʼIP��Χ
        private static long GetstartIP(long recNO)
        {
            long offSet = firststartIP + recNO * 7;
            //objfs.Seek(offSet,SeekOrigin.Begin);
            objfs.Position = offSet;
            byte[] buff = new Byte[7];
            objfs.Read(buff, 0, 7);

            endIPOff = Convert.ToInt64(buff[4].ToString()) + Convert.ToInt64(buff[5].ToString()) * 256 + Convert.ToInt64(buff[6].ToString()) * 256 * 256;
            startIP = Convert.ToInt64(buff[0].ToString()) + Convert.ToInt64(buff[1].ToString()) * 256 + Convert.ToInt64(buff[2].ToString()) * 256 * 256 + Convert.ToInt64(buff[3].ToString()) * 256 * 256 * 256;
            return startIP;
        }
        #endregion

        #region ��ȡ����IP
        private static long GetendIP()
        {
            //objfs.Seek(endIPOff,SeekOrigin.Begin);
            objfs.Position = endIPOff;
            byte[] buff = new Byte[5];
            objfs.Read(buff, 0, 5);
            endIP = Convert.ToInt64(buff[0].ToString()) + Convert.ToInt64(buff[1].ToString()) * 256 + Convert.ToInt64(buff[2].ToString()) * 256 * 256 + Convert.ToInt64(buff[3].ToString()) * 256 * 256 * 256;
            countryFlag = buff[4];
            return endIP;
        }
        #endregion

        #region ��ȡ����/����ƫ����
        private static string GetCountry()
        {
            switch (countryFlag)
            {
                case 1:
                case 2:
                    country = GetFlagStr(endIPOff + 4);
                    local = (1 == countryFlag) ? " " : GetFlagStr(endIPOff + 8);
                    break;
                default:
                    country = GetFlagStr(endIPOff + 4);
                    local = GetFlagStr(objfs.Position);
                    break;
            }
            return " ";
        }
        #endregion

        #region ��ȡ����/�����ַ���
        private static string GetFlagStr(long offSet)
        {
            int flag = 0;
            byte[] buff = new Byte[3];
            while (1 == 1)
            {
                //objfs.Seek(offSet,SeekOrigin.Begin);
                objfs.Position = offSet;
                flag = objfs.ReadByte();
                if (flag == 1 || flag == 2)
                {
                    objfs.Read(buff, 0, 3);
                    if (flag == 2)
                    {
                        countryFlag = 2;
                        endIPOff = offSet - 4;
                    }
                    offSet = Convert.ToInt64(buff[0].ToString()) + Convert.ToInt64(buff[1].ToString()) * 256 + Convert.ToInt64(buff[2].ToString()) * 256 * 256;
                }
                else
                {
                    break;
                }
            }
            if (offSet < 12)
                return " ";
            objfs.Position = offSet;
            return GetStr();
        }
        #endregion

        #region GetStr
        private static string GetStr()
        {
            byte lowC = 0;
            byte upC = 0;
            string str = "";
            byte[] buff = new byte[2];
            while (1 == 1)
            {
                lowC = (Byte)objfs.ReadByte();
                if (lowC == 0)
                    break;
                if (lowC > 127)
                {
                    upC = (byte)objfs.ReadByte();
                    buff[0] = lowC;
                    buff[1] = upC;
                    System.Text.Encoding enc = System.Text.Encoding.GetEncoding("GB2312");
                    str += enc.GetString(buff);
                }
                else
                {
                    str += (char)lowC;
                }
            }
            return str;
        }
        #endregion

        #region ��ȡIP��ַ
        /// <summary>
        /// ����IP��ѯ���ڵ�
        /// </summary>
        /// <param name="scanIP">��ѯIP</param>
        /// <returns>���ڵ�</returns>
        public static string IPLocation(string scanIP)
        {
            if (CheckIP(scanIP) == true)
            {
                ip = scanIP;
                QQwry();
                country = country.Trim().Replace("CZ88.NET", "");
                local = local.Trim().Replace("CZ88.NET", "");
                string location = country + " " + local;
                return location.Trim();
            }
            else
            {
                return "�Ƿ���IP��ַ";
            }
        }
        #endregion

    }
}
