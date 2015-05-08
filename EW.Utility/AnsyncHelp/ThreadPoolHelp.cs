using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EW.Utility.AnsyncHelp
{
    /// <summary>
    /// 线程池操作类
    /// </summary>
    public class ThreadPoolHelp
    {

        private const int workerThreads = 10, completionPortThreads = 10;

        public static void QueueUserWorkItem(WaitCallback callback, Object model)
        {
            //ManualResetEvent eventX = new ManualResetEvent(false);
            ThreadPool.SetMaxThreads(workerThreads, completionPortThreads);
            ThreadPool.QueueUserWorkItem(callback, model);
        }
        public static void QueueUserWorkItem(WaitCallback callback)
        {
            QueueUserWorkItem(callback, null);
        }

    }
}
