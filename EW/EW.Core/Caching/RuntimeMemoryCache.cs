
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using EW.Utility.Extensions;


namespace EW.Core.Caching
{
    public class RuntimeMemoryCache : ICache
    {
        private readonly string _region;
        private readonly MemoryCache _cache;
        private static readonly Object _locker = new object();

        /// <summary>
        /// 初始化一个<see cref="RuntimeMemoryCache"/>类型的新实例
        /// </summary>
        public RuntimeMemoryCache(string region)
        {
            _region = region;
            _cache = MemoryCache.Default;
        }
        /// <summary>
        /// 获取 缓存区域名称，可作为缓存键标识，给缓存管理带来便利
        /// </summary>
        public string Region
        {
            get { return _region; }
        }
        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>获取的数据</returns>
        public object Get(string key)
        {
            key.CheckNotNull("key");
            string cacheKey = GetCacheKey(key);
            object value = _cache.Get(cacheKey);
            if (value == null)
            {
                return null;
            }

            return value;
        }

        /// <summary>
        /// 从缓存中获取强类型数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns>获取的强类型数据</returns>
        public T Get<T>(string key)
        {
            return (T)Get(key);
        }
        /// <summary>
        /// 获取当前缓存对象中的所有数据
        /// </summary>
        /// <returns>所有数据的集合</returns>
        public IEnumerable<DictionaryEntry> GetAll()
        {
            string token = string.Concat(Region, ":");
            return _cache.Where(m => m.Key.StartsWith(token)).Select(m => new DictionaryEntry() { Key = m.Value, Value = m.Value });
        }
        /// <summary>
        /// 获取当前缓存中的所有数据
        /// </summary>
        /// <typeparam name="T">项数据类型</typeparam>
        /// <returns>所有数据的集合</returns>
        public IEnumerable<T> GetAll<T>()
        {
            return GetAll().Cast<T>();
        }

        /// <summary>
        /// 使用默认配置添加或替换缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        public void Set(string key, object value)
        {
            key.CheckNotNull("key");
            value.CheckNotNull("value");
            string cacheKey = GetCacheKey(key);
            CacheItemPolicy policy = new CacheItemPolicy();
            _cache.Set(cacheKey, value, policy);
        }

        public void Set(string key, object value, DateTime absoluteExpiration)
        {
            key.CheckNotNull("key");
            value.CheckNotNull("value");
            string cacheKey = GetCacheKey(key);
            CacheItemPolicy policy = new CacheItemPolicy() { AbsoluteExpiration = absoluteExpiration };
            _cache.Set(cacheKey, value, policy);
        }

        public void Set(string key, object value, TimeSpan slidingExpiration)
        {
            key.CheckNotNull("key");
            value.CheckNotNull("value");
            string cacheKey = GetCacheKey(key);
            CacheItemPolicy policy = new CacheItemPolicy() { SlidingExpiration = slidingExpiration };
            _cache.Set(cacheKey, value, policy);
        }

        public T GetCacheItem<T>(string key, Func<T> cachePopulate, TimeSpan? slidingExpiration = null, DateTime? absoluteExpiration = null)
        {
            key.CheckNotNull("key");
            if (cachePopulate == null) throw new ArgumentNullException("cachePopulate");
            if (absoluteExpiration == null)
            {
                absoluteExpiration = DateTime.Now.AddMinutes(5);
            }
            if (slidingExpiration == null)
            {
                slidingExpiration = new TimeSpan(0, 5, 0);
            }
            string cacheKey = GetCacheKey(key);
            if (_cache.Get(cacheKey) == null)
            {
                lock (_locker)
                {
                    if (_cache.Get(cacheKey) == null)
                    {
                        var item = new CacheItem(key, cachePopulate());
                        var policy = CreatePolicy(slidingExpiration, absoluteExpiration);
                        _cache.Add(item, policy);
                    }
                }
            }

            return Get<T>(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="slidingExpiration">为null五分钟</param>
        /// <param name="absoluteExpiration">为null五分钟</param>
        /// <returns></returns>
        public T GetCacheItem<T>(string key, object value, TimeSpan? slidingExpiration = null, DateTime? absoluteExpiration = null)
        {
            key.CheckNotNull("key");
            value.CheckNotNull("value");
            if (absoluteExpiration == null)
            {
                absoluteExpiration = DateTime.Now.AddMinutes(5);
            }
            if (slidingExpiration == null)
            {
                slidingExpiration = new TimeSpan(0, 5, 0);
            }
            string cacheKey = GetCacheKey(key);
            if (_cache.Get(cacheKey) == null)
            {
                lock (_locker)
                {
                    if (_cache.Get(cacheKey) == null)
                    {
                        var item = new CacheItem(key, value);
                        var policy = CreatePolicy(slidingExpiration, absoluteExpiration);
                        _cache.Add(item, policy);
                    }
                }
            }

            return Get<T>(key);
        }

        public void Remove(string key)
        {

            key.CheckNotNull("key");
            string cacheKey = GetCacheKey(key);
            _cache.Remove(cacheKey);
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Clear()
        {
            string token = Region + ":";
            List<string> cacheKeys = _cache.Where(m => m.Key.StartsWith(token)).Select(m => m.Key).ToList();
            foreach (string cacheKey in cacheKeys)
            {
                _cache.Remove(cacheKey);
            }
        }

        #region 私有方法
        /// <summary>
        /// 获取缓存key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetCacheKey(string key)
        {
            return string.Concat(_region, ":", key, "@", key.GetHashCode());
        }
        private static CacheItemPolicy CreatePolicy(TimeSpan? slidingExpiration, DateTime? absoluteExpiration)
        {
            var policy = new CacheItemPolicy();

            if (absoluteExpiration.HasValue)
            {
                policy.AbsoluteExpiration = absoluteExpiration.Value;
            }
            else if (slidingExpiration.HasValue)
            {
                policy.SlidingExpiration = slidingExpiration.Value;
            }

            policy.Priority = CacheItemPriority.Default;

            return policy;
        }
        #endregion



    }
}
