using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EW.Utility.Extensions
{ /// <summary>
    /// 程序集扩展操作类
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// 获取程序集的文件版本
        /// </summary>
        public static Version GetFileVersion(this Assembly assembly)
        {
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
            return new Version(info.FileVersion);
        }

        /// <summary>
        /// 获取程序集的产品版本
        /// </summary>
        public static Version GetProductVersion(this Assembly assembly)
        {
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
            return new Version(info.ProductVersion);
        }
        /// <summary>
        /// 为磁盘上的物理文件提供版本信息
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static FileVersionInfo GetProductVersionInfo(this Assembly assembly)
        {
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(assembly.Location);
            return info;
        }
    }
}