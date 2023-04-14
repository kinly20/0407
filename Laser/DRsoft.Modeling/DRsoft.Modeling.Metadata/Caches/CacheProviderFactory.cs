using DRsoft.Modeling.Metadata.Config;
using DRsoft.Modeling.Metadata.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace DRsoft.Modeling.Metadata.Caches
{
    /// <summary>
    /// 缓存提供类的工厂
    /// </summary>
    /// <typeparam name="T">元数据类型</typeparam>
    public static class CacheProviderFactory<T> where T : IModelKey
    {
        private static ConcurrentDictionary<Type, CacheValueFactory> s_dictRuntime = new ConcurrentDictionary<Type, CacheValueFactory>();
        private static ConcurrentDictionary<Type, CacheValueFactory> s_dictDesign = new ConcurrentDictionary<Type, CacheValueFactory>();

        private class CacheValueFactory
        {
            private readonly Type _metadataType;
            private readonly CacheType _type;
            private object _lock = new object();
            private bool _inited;

            private BaseCacheProvider<T> _cacheProvider;
            public CacheValueFactory(Type metadataType, CacheType type)
            {
                _metadataType = metadataType;
                _type = type;

            }

            public BaseCacheProvider<T> Provider
            {
                get
                {
                    if (_inited == false)
                    {
                        lock (_lock)
                        {
                            if (_inited == false)
                            {
                                var cacheType = CacheProviderTypeAttribute(_metadataType);
                                BaseCacheProvider<T> instance;
                                if (_type == CacheType.Runtime)
                                {
                                    instance =(BaseCacheProvider<T>)Activator.CreateInstance(cacheType.RuntimeCacheProvider);
                                }
                                else if (_type == CacheType.Design)
                                {
                                    instance = (BaseCacheProvider<T>)Activator.CreateInstance(cacheType.DesignCacheProvider);
                                }
                                else
                                {
                                    throw new Exception($"不能获取 {_type.ToString()} 状态的元数据。");
                                }
                                instance.Init();

                                _cacheProvider = instance;

                                _inited = true;
                            }
                        }
                    }
                    return _cacheProvider;
                }
            }
        }
        
        /// <summary>
        /// 创建一个缓存提供类的实例
        /// </summary>
        /// <returns></returns>
        public static BaseCacheProvider<T> Create()
        {
            return Create(CacheType.Runtime);
        }

        /// <summary>
        /// 创建一个缓存提供类的实例
        /// </summary>
        /// <param name="type">缓存类型</param>
        /// <returns>缓存提供类的实例</returns>
        public static BaseCacheProvider<T> Create(CacheType type)
        {
            Type targetType = typeof(T);

            BaseCacheProvider<T> instance = null;
            switch (type)
            {
                //运行时
                case CacheType.Runtime:
                    instance = s_dictRuntime.GetOrAdd(targetType, new CacheValueFactory(targetType, type)).Provider;
                    break;
                //设计时
                case CacheType.Design:
                    instance = s_dictDesign.GetOrAdd(targetType, new CacheValueFactory(targetType, type)).Provider;
                    break;
                case CacheType.RuntimeOrDesign:
                    if (IsPreview() || IsDesign())
                    {
                        return Create(CacheType.Design);
                    }
                    return Create(CacheType.Runtime);
                    break;
            }

            return instance;
        }

        /// <summary>
        /// 是否预览模式
        /// </summary>
        /// <returns></returns>
        public static bool IsPreview()
        {
            return RuntimeMode.IsPreView;
        }

        /// <summary>
        /// 是否设计时
        /// </summary>
        /// <returns></returns>
        private static bool IsDesign()
        {
            return RuntimeMode.IsDesign;
        }

        private static CacheProviderTypeAttribute CacheProviderTypeAttribute(Type targetType)
        {
            var cacheType = targetType.GetCustomAttribute<CacheProviderTypeAttribute>();
            if (cacheType == null)
            {
                throw new InvalidOperationException($"{targetType.Name}未标记MetadataCacheTypeAttribute属性。");
            }
            return cacheType;
        }
    }
}
