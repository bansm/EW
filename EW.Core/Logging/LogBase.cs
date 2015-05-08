using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EW.Core.Logging
{
    /// <summary>
    ///  日志基础
    /// </summary>
    public class LogBase
    {
        public String ID { get; set; }
        public LogBase()
        {
            ID = Guid.NewGuid().ToString("N");
        }
    }
}
