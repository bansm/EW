using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EW.Core.Context
{
    /// <summary>
    /// EW框架上下文，用于构造EW框架运行环境
    /// </summary>
    [Serializable]
    public class EWContext : Dictionary<string, object>
    {
        private const string CallContextKey = "_EW_CallContext";
        private const string OperatorKey = "_EW_Context_Operator";

        /// <summary>
        /// 初始化一个<see cref="OSharpContext"/>类型的新实例
        /// </summary>
        public EWContext()
        { }

        /// <summary>
        /// 初始化一个<see cref="OSharpContext"/>类型的新实例
        /// </summary>
        protected EWContext(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }

        /// <summary>
        /// 获取或设置 当前上下文
        /// </summary>
        public static EWContext Current
        {
            get
            {
                EWContext context = CallContext.LogicalGetData(CallContextKey) as EWContext;
                if (context != null)
                {
                    return context;
                }
                context = new EWContext();
                CallContext.LogicalSetData(CallContextKey, context);
                return context;
            }
            set
            {
                if (value == null)
                {
                    CallContext.FreeNamedDataSlot(CallContextKey);
                    return;
                }
                CallContext.LogicalSetData(CallContextKey, value);
            }
        }

        /// <summary>
        /// 获取 当前操作者
        /// </summary>
        public Operator Operator
        {
            get
            {
                if (!ContainsKey(OperatorKey))
                {
                    this[OperatorKey] = new Operator();
                }
                return this[OperatorKey] as Operator;
            }
        }
    }
}
