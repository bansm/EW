using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EW.Utility.Common
{
    public class CommonHelp
    {
        public static Guid NewGUID
        {
            get
            {
                return Guid.NewGuid();
            }
        }


        /// <summary>
        /// 数据库理解字符串
        /// </summary>
        /// <param name="ConnName"></param>
        /// <returns></returns>
        public static string GetConnString(string ConnName)
        {
            if (ConfigurationManager.ConnectionStrings[ConnName] == null)
            {
                throw new Exception("查无'" + ConnName + "'数据库连接");
            }
            return ConfigurationManager.ConnectionStrings[ConnName].ConnectionString;
        }
        private static readonly object LockObj = new object();


        //public static ILog Log
        //{
        //    get
        //    {
        //        ILog log;
        //        try
        //        {
        //            log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));

        //        }
        //        catch (Exception)
        //        {
        //            RollingFileAppender appender = new RollingFileAppender
        //            {
        //                Name = "root",
        //                File = "logs\\log_",
        //                AppendToFile = true,
        //                LockingModel = new FileAppender.MinimalLock(),
        //                RollingStyle = RollingFileAppender.RollingMode.Date,
        //                DatePattern = "yyyyMMdd-HH\".log\"",
        //                StaticLogFileName = false,
        //                Threshold = Level.Debug,
        //                MaxSizeRollBackups = 10,
        //                Layout = new PatternLayout("%n[%d{yyyy-MM-dd HH:mm:ss.fff}] %-5p %c %t %w %n%m%n")
        //            };
        //            appender.ClearFilters();
        //            appender.AddFilter(new LevelMatchFilter { LevelToMatch = Level.Info });
        //            log4net.Config.BasicConfigurator.Configure(appender);
        //            appender.ActivateOptions();
        //        }
        //        log = log4net.LogManager.GetLogger("DMYBzgy");

        //        return log;
        //    }
        //}

    }
}
