using EW.Core.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EW.Core.Logging
{
    public class DataLog : LogBase
    {
        /// <summary>
        /// 初始化一个<see cref="DataLog"/>类型的新实例
        /// </summary>
        public DataLog()
        {

            Operator = EWContext.Current.Operator;
            OperateDate = DateTime.Now;
            LogItems = new List<DataLogItem>();
        }

        /// <summary>
        /// 获取或设置 实体名称
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// 获取 操作人
        /// </summary>
        public Operator Operator { get; set; }

        /// <summary>
        /// 获取或设置 操作类型
        /// </summary>
        public OperatingType OperateType { get; set; }

        /// <summary>
        /// 获取或设置 操作时间
        /// </summary>
        public DateTime OperateDate { get; set; }

        /// <summary>
        /// 获取或设置 操作明细
        /// </summary>
        public List<DataLogItem> LogItems { get; set; }

    }


    /// <summary>
    /// 实体数据日志操作类型
    /// </summary>
    public enum OperatingType
    {
        /// <summary>
        /// 查询
        /// </summary>
        Query = 0,

        /// <summary>
        /// 插入
        /// </summary>
        Insert = 10,

        /// <summary>
        /// 更新
        /// </summary>
        Update = 20,

        /// <summary>
        /// 删除
        /// </summary>
        Delete = 30
    }


    /// <summary>
    /// 实体操作日志明细
    /// </summary>
    public class DataLogItem
    {
        /// <summary>
        /// 获取或设置 字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 获取或设置 旧值
        /// </summary>
        public string OriginalValue { get; set; }

        /// <summary>
        /// 获取或设置 新值
        /// </summary>
        public string NewValue { get; set; }

    }
}