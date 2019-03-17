using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;

namespace Luoyi.Common
{
    /// <summary>
    /// 返回系统CPU占用百分比
    /// </summary>
    public class GetCPU
    {
        public static int GetCpuUsage()
        {
            return CpuUsage.Create().Query();
        }

        public abstract class CpuUsage
        {
            private static GetCPU.CpuUsage m_CpuUsage;

            protected CpuUsage()
            {
            }


            public static GetCPU.CpuUsage Create()
            {
                if (m_CpuUsage == null)
                {
                    if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                    {
                        if (Environment.OSVersion.Platform != PlatformID.Win32Windows)
                        {
                            throw new NotSupportedException();
                        }
                        m_CpuUsage = new GetCPU.CpuUsage9x();
                    }
                    else
                    {
                        m_CpuUsage = new GetCPU.CpuUsageNt();
                    }
                }
                return m_CpuUsage;
            }

            public abstract int Query();
        }

        internal sealed class CpuUsage9x : GetCPU.CpuUsage
        {
            private RegistryKey m_StatData;

            public CpuUsage9x()
            {
                try
                {
                    RegistryKey key = Registry.DynData.OpenSubKey(@"PerfStats\StartStat", false);
                    if (key == null)
                    {
                        throw new NotSupportedException();
                    }
                    key.GetValue(@"KERNEL\CPUUsage");
                    key.Close();
                    this.m_StatData = Registry.DynData.OpenSubKey(@"PerfStats\StatData", false);
                    if (this.m_StatData == null)
                    {
                        throw new NotSupportedException();
                    }
                }
                catch (NotSupportedException exception)
                {
                    throw exception;
                }
                catch (Exception exception2)
                {
                    throw new NotSupportedException("Error while querying the system information.", exception2);
                }
            }

            ~CpuUsage9x()
            {
                try
                {
                    this.m_StatData.Close();
                }
                catch
                {
                }
                try
                {
                    RegistryKey key = Registry.DynData.OpenSubKey(@"PerfStats\StopStat", false);
                    key.GetValue(@"KERNEL\CPUUsage", false);
                    key.Close();
                }
                catch
                {
                }
            }

            public override int Query()
            {
                int num;
                try
                {
                    num = (int)this.m_StatData.GetValue(@"KERNEL\CPUUsage");
                }
                catch (Exception exception)
                {
                    throw new NotSupportedException("Error while querying the system information.", exception);
                }
                return num;
            }
        }

        internal sealed class CpuUsageNt : GetCPU.CpuUsage
        {
            private const int NO_ERROR = 0;
            private long oldIdleTime;
            private long oldSystemTime;
            private double processorCount;
            private const int SYSTEM_BASICINFORMATION = 0;
            private const int SYSTEM_PERFORMANCEINFORMATION = 2;
            private const int SYSTEM_TIMEINFORMATION = 3;

            public CpuUsageNt()
            {
                byte[] lpStructure = new byte[0x20];
                byte[] buffer2 = new byte[0x138];
                byte[] buffer3 = new byte[0x2c];
                if (NtQuerySystemInformation(3, lpStructure, lpStructure.Length, IntPtr.Zero) != 0)
                {
                    throw new NotSupportedException();
                }
                if (NtQuerySystemInformation(2, buffer2, buffer2.Length, IntPtr.Zero) != 0)
                {
                    throw new NotSupportedException();
                }
                if (NtQuerySystemInformation(0, buffer3, buffer3.Length, IntPtr.Zero) != 0)
                {
                    throw new NotSupportedException();
                }
                this.oldIdleTime = BitConverter.ToInt64(buffer2, 0);
                this.oldSystemTime = BitConverter.ToInt64(lpStructure, 8);
                this.processorCount = buffer3[40];
            }

            [DllImport("ntdll")]
            private static extern int NtQuerySystemInformation(int dwInfoType, byte[] lpStructure, int dwSize, IntPtr returnLength);
            public override int Query()
            {
                byte[] lpStructure = new byte[0x20];
                byte[] buffer2 = new byte[0x138];
                if (NtQuerySystemInformation(3, lpStructure, lpStructure.Length, IntPtr.Zero) != 0)
                {
                    throw new NotSupportedException();
                }
                if (NtQuerySystemInformation(2, buffer2, buffer2.Length, IntPtr.Zero) != 0)
                {
                    throw new NotSupportedException();
                }
                double num = BitConverter.ToInt64(buffer2, 0) - this.oldIdleTime;
                double num2 = BitConverter.ToInt64(lpStructure, 8) - this.oldSystemTime;
                if (num2 != 0.0)
                {
                    num /= num2;
                }
                num = (100.0 - ((num * 100.0) / this.processorCount)) + 0.5;
                this.oldIdleTime = BitConverter.ToInt64(buffer2, 0);
                this.oldSystemTime = BitConverter.ToInt64(lpStructure, 8);
                return (int)num;
            }
        }
    }
}
