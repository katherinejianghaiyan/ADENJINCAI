using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Configuration;

using log4net;
using log4net.Config;
using log4net.Appender;


namespace Luoyi.Common
{
    /// <summary>
    /// 日志管理类(各种方法用于记录不同级别的日志，本质上没有什么区别，只是可以结合配置中的Level决定最终是否记录该级别的日志，以达到灵活性)
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// 日志配置文件路径  
        /// </summary>
        private const string APP_SETTING_PATH_LOG_CONFIG = "LogConfigPath";


        /// <summary>
        /// 静态锁
        /// </summary>
        private static object lockHelper = new object();

        /// <summary>
        /// 日志管理器
        /// </summary>
        private static ILog _log = null;

        /// <summary>
        /// 初始化日志管理器
        /// </summary>
        private static void Init()
        {
            //配置文件路径
            FileInfo configFile = new FileInfo(string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings[APP_SETTING_PATH_LOG_CONFIG]));

            if (!configFile.Exists)
            {
                CreateConfigFile(configFile);
            }

            XmlConfigurator.ConfigureAndWatch(configFile);
            _log = LogManager.GetLogger("MyLogger");
        }

        /// <summary>
        /// 创建配置文件
        /// </summary>
        /// <param name="fileInfo">文件信息</param>
        private static void CreateConfigFile(FileInfo fileInfo)
        {
            using (StreamWriter sw = File.CreateText(fileInfo.FullName))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                sw.WriteLine("<configuration>");
                sw.WriteLine("  <log4net>");
                sw.WriteLine("    <root>");
                sw.WriteLine("      <level value=\"ALL\"/>");
                sw.WriteLine("      <appender-ref ref=\"RollingFileAppender\"/>");
                sw.WriteLine("    </root>");
                sw.WriteLine("    <appender name=\"RollingFileAppender\" type=\"log4net.Appender.RollingFileAppender\">");
                sw.WriteLine("      <param name=\"File\" value=\"Logs/\"/>");
                sw.WriteLine("      <param name=\"AppendToFile\" value=\"true\"/>");
                sw.WriteLine("      <param name=\"RollingStyle\" value=\"Date\"/>");
                sw.WriteLine("      <param name=\"DatePattern\" value=\"yyyy-MM-dd&quot;.log&quot;\"/>");
                sw.WriteLine("      <param name=\"MaximumFileSize \" value=\"1MB\"/>");
                sw.WriteLine("      <param name=\"MaxSizeRollBackups\" value=\"10\"/>");
                sw.WriteLine("      <param name=\"StaticLogFileName\" value=\"false\"/>");
                sw.WriteLine("      <lockingModel type=\"log4net.Appender.FileAppender+MinimalLock\" />");
                sw.WriteLine("      <layout type=\"log4net.Layout.PatternLayout\">");
                sw.WriteLine("        <param name=\"ConversionPattern\" value=\"[时间]:%d%n[级别]:%p%n[内容]:%m%n%n\"/>");
                sw.WriteLine("      </layout>");
                sw.WriteLine("    </appender>");
                sw.WriteLine("  </log4net>");
                sw.WriteLine("</configuration>");
                sw.Close();
            }
        }

        public static ILog MyLog
        {
            get
            {
                if (_log == null)
                {
                    lock (lockHelper)
                    {
                        if (_log == null)
                            Init();
                    }
                }
                return _log;
            }
        }
        /// <summary>
        /// 致命的、毁灭性的(用户记录一些程序运行时出现的致命错误)
        /// </summary>
        /// <param name="message">日志摘要</param>
        public static void Fatal(string message)
        {
            Fatal(message, null);
        }

        /// <summary>
        /// 致命的、毁灭性的(用户记录一些程序运行时出现的致命错误)
        /// </summary>
        /// <param name="message">日志摘要</param>
        /// <param name="exception">异常</param>
        public static void Fatal(string message, Exception exception)
        {
            if (MyLog.IsFatalEnabled)
            {
                MyLog.Fatal(message, exception);
                //可以增加发送短信、邮件
            }
        }
        /// <summary>
        /// 错误(用户记录一些程序运行时出现错误)
        /// </summary>
        /// <param name="message">日志摘要</param>
        public static void Error(string message)
        {
            Error(message, null);
        }

        /// <summary>
        /// 错误(用户记录一些程序运行时出现错误)
        /// </summary>
        /// <param name="message">日志摘要</param>
        /// <param name="exception">异常</param>
        public static void Error(string message, Exception exception)
        {
            if (MyLog.IsErrorEnabled)
            {
                MyLog.Error(message, exception);
            }
        }

        /// <summary>
        /// 警告、注意、通知(用于记录一些需要引起注意问题)
        /// </summary>
        /// <param name="message">日志摘要</param>
        public static void Warn(string message)
        {
            Warn(message, null);
        }

        /// <summary>
        /// 警告、注意、通知(用于记录一些需要引起注意问题)
        /// </summary>
        /// <param name="message">日志摘要</param>
        /// <param name="exception">异常</param>
        public static void Warn(string message, Exception exception)
        {
            if (MyLog.IsWarnEnabled)
            {
                MyLog.Warn(message, exception);
            }
        }

        /// <summary>
        /// 信息(用于记录一些辅助信息)
        /// </summary>
        /// <param name="message">日志摘要</param>
        public static void Info(string message)
        {
            Info(message, null);
        }

        /// <summary>
        /// 信息(用于记录一些辅助信息)
        /// </summary>
        /// <param name="message">日志摘要</param>
        /// <param name="exception">异常</param>
        public static void Info(string message, Exception exception)
        {
            if (MyLog.IsInfoEnabled)
            {
                MyLog.Info(message, exception);
            }
        }

        /// <summary>
        /// 调试(用于记录一些调试时才需要记录的日志)
        /// </summary>
        /// <param name="message">日志摘要</param>
        public static void Debug(string message)
        {
            Debug(message, null);
        }

        /// <summary>
        /// 调试(用于记录一些调试时才需要记录的日志)
        /// </summary>
        /// <param name="message">日志摘要</param>
        /// <param name="exception">异常</param>
        public static void Debug(string message, Exception exception)
        {
            if (MyLog.IsDebugEnabled)
            {
                MyLog.Debug(message, exception);
            }
        }

    }
}