using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EW.Core.Caching
{
    /// <summary>
    /// 缓存操作约定
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 获取 缓存区域名称，可作为缓存键标识，给缓存管理带来便利
        /// </summary>
        string Region { get; }
        /// <summary>
        /// 从缓存中获取数据
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>获取的数据</returns>
        object Get(string key);


        /// <summary>
        /// 从缓存中获取强类型数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns>获取的强类型数据</returns>
        T Get<T>(string key);

        /// <summary>
        /// 获取当前缓存对象中的所有数据
        /// </summary>
        /// <returns>所有数据的集合</returns>
        IEnumerable<DictionaryEntry> GetAll();

        /// <summary>
        /// 获取当前缓存中的所有数据
        /// </summary>
        /// <typeparam name="T">项数据类型</typeparam>
        /// <returns>所有数据的集合</returns>
        IEnumerable<T> GetAll<T>();

        /// <summary>
        /// 使用默认配置添加或替换缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        void Set(string key, object value);

        /// <summary>
        /// 添加或替换缓存项并设置绝对过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        /// <param name="absoluteExpiration">绝对过期时间，过了这个时间点，缓存即过期</param>
        void Set(string key, object value, DateTime absoluteExpiration);

        /// <summary>
        /// 添加或替换缓存项并设置相对过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存数据</param>
        /// <param name="slidingExpiration">滑动过期时间，在此时间内访问缓存，缓存将继续有效</param>
        void Set(string key, object value, TimeSpan slidingExpiration);

        /// <summary>
        /// 得到缓存对象
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="cachePopulate">得到默认缓存值的方法delegate() { return MyMothod(); }</param>
        /// <param name="slidingExpiration">缓存时间</param>
        /// <param name="absoluteExpiration">超期时间</param>
        /// <returns></returns>
        T GetCacheItem<T>(String key, Func<T> cachePopulate, TimeSpan? slidingExpiration = null, DateTime? absoluteExpiration = null);
        /// <summary>
        /// 得到缓存对象
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">缓存key</param>
        /// <param name="DefaultValue">默认缓存值</param>
        /// <param name="slidingExpiration">缓存时间</param>
        /// <param name="absoluteExpiration">超期时间</param>
        /// <returns></returns>
        T GetCacheItem<T>(String key, Object DefaultValue, TimeSpan? slidingExpiration = null, DateTime? absoluteExpiration = null);

        /// <summary>
        /// 移除指定键的缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        void Remove(string key);

        /// <summary>
        /// 清空缓存
        /// </summary>
        void Clear();
    }
}