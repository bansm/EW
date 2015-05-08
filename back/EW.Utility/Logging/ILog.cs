using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EW.Utility.Logging
{/// <summary>
    /// 表示日志实例的接口
    /// </summary>
    public interface ILog : ILogger
    {
        #region 属性

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Trace"/>级别的日志
        /// </summary>
        bool IsTraceEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Debug"/>级别的日志
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Info"/>级别的日志
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Warn"/>级别的日志
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Error"/>级别的日志
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        /// 获取 是否允许<see cref="LogLevel.Fatal"/>级别的日志
        /// </summary>
        bool IsFatalEnabled { get; }

        #endregion

    }
}