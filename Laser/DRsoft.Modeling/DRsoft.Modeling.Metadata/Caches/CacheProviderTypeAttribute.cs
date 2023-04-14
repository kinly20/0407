using System;

namespace DRsoft.Modeling.Metadata.Caches
{
    /// <summary>
    /// 缓存提供类型标记
    /// </summary>
    internal class CacheProviderTypeAttribute : Attribute
    {
        /// <summary>
        /// 运行时缓存提供类
        /// </summary>
        public Type RuntimeCacheProvider { get; set; }
        /// <summary>
        /// 设计时缓存提供类
        /// </summary>
        public Type DesignCacheProvider { get; set; }
        /// <summary>
        /// 元数据存储的文件目录
        /// </summary>
        public string Directory { get; set; }

        public CacheProviderTypeAttribute(Type runtimeCacheProvider, Type designCacheProvider, string directory = null)
        {
            RuntimeCacheProvider = runtimeCacheProvider;
            DesignCacheProvider = designCacheProvider;
            Directory = directory;
        }
    }
}
