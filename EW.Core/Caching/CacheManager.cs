using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EW.Utility.Extensions;


namespace EW.Core.Caching
{
    /// <summary>
    /// 缓存操作管理器
    /// </summary>
    public static class CacheManager
    {
        private static readonly object LockObj = new object();


        static CacheManager()
        {
            Caches = new ConcurrentDictionary<string, ICache>();

        }
        internal static ConcurrentDictionary<string, ICache> Caches { get; private set; }
        /// <summary>
        /// 获取指定区域的缓存执行者实例
        /// </summary>
        public static ICache GetCacher(string region)
        {
            region.CheckNotNullOrEmpty("region");
            ICache cache;
            if (Caches.TryGetValue(region, out cache))
            {
                return cache;
            }
            cache = new RuntimeMemoryCache(region);
            Caches[region] = cache;
            return cache;
        }

        /// <summary>
        /// 获取指定类型的缓存执行者实例
        /// </summary>
        public static ICache GetCacher(Type type)
        {
            type.CheckNotNull("type");
            return GetCacher(type.FullName);
        }
        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void Clear()
        {

            foreach (var item in Caches)
            {
                item.Value.Clear();
            }
        }
        /// <summary>
        /// 移出区域缓存
        /// </summary>
        /// <param name="region"></param>
        public static void Remove(string region)
        {
            ICache cache;
            Caches.TryGetValue(region, out cache);
            if (cache != null)
            {
                cache.Clear();
            }
        }
    }
}