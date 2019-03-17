using System;

namespace Luoyi.Common
{
    /// <summary>
    /// GUID生成器
    /// </summary>
    public class GuidHelper
    {
        /// <summary>
        /// 生成具有时序性的GUID
        /// </summary>
        /// <returns>GUID</returns>
        public static Guid NewComb()
        {
            var guidArray = Guid.NewGuid().ToByteArray();
            var baseDate = new DateTime(1900, 1, 1);
            var now = DateTime.Now;
            // Get the days and milliseconds which will be used to build the byte string 
            var days = new TimeSpan(now.Ticks - baseDate.Ticks);
            var msecs = new TimeSpan(now.Ticks - (new DateTime(now.Year, now.Month, now.Day).Ticks));

            // Convert to a byte array 
            // Note that sql server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            var daysArray = BitConverter.GetBytes(days.Days);
            var msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            // Reverse the bytes to match sql servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }

        /// <summary> 
        ///  将具有时序性GUID 转换成时间信息 
        /// </summary> 
        /// <param name="guid">包含时间信息的 COMB </param> 
        /// <returns>时间</returns> 
        public static DateTime GetDateFromComb(Guid guid)
        {
            var baseDate = new DateTime(1900, 1, 1);
            var daysArray = new byte[4];
            var msecsArray = new byte[4];
            var guidArray = guid.ToByteArray();

            // Copy the date parts of the guid to the respective byte arrays. 
            Array.Copy(guidArray, guidArray.Length - 6, daysArray, 2, 2);
            Array.Copy(guidArray, guidArray.Length - 4, msecsArray, 0, 4);

            // Reverse the arrays to put them into the appropriate order 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Convert the bytes to ints 
            var days = BitConverter.ToInt32(daysArray, 0);
            var msecs = BitConverter.ToInt32(msecsArray, 0);

            var date = baseDate.AddDays(days);
            date = date.AddMilliseconds(msecs * 3.333333);

            return date;
        }
    }
}
