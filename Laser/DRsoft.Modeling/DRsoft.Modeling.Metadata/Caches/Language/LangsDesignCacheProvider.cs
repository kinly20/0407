using DRsoft.Modeling.Metadata.Models.Language;
using System;
using System.Collections.Generic;

namespace DRsoft.Modeling.Metadata.Caches.Language
{
    internal sealed class LangsDesignCacheProvider : BaseLangsCacheProvider
    {
        /// <summary>
        /// 运行时缓存提供类
        /// </summary>
        private readonly BaseCacheProvider<Langs> _runtimeCacheProvider = CacheProviderFactory<Langs>.Create(CacheType.Runtime);

        public LangsDesignCacheProvider()
        {
            _matcheProvider = CacheProviderFactory<LangMatch>.Create(CacheType.Design);
        }
        public override void Init()
        {
            SyncAllRuntimeToDesign();
            FileOperator.LoadDesignMetadatas(AddOrUpdate);
        }
        /// <summary>
        /// 重新加载
        /// </summary>
        public override void Reload()
        {
            _runtimeCacheProvider.Reload();
            base.Reload();
        }


        /// <summary>
        /// 同步所有运行时缓存到设计时
        /// </summary>
        private void SyncAllRuntimeToDesign()
        {
            CacheLock.EnterWriteLock();
            try
            {
                List<Langs> list = _runtimeCacheProvider.FindAll();
                foreach (Langs metadata in list)
                {
                    Langs metadataClone = metadata.Clone();
                    Cache[metadata.Id] = metadataClone;
                    SyncMatches(metadataClone);
                }
            }
            finally
            {
                CacheLock.ExitWriteLock();
            }
        }

        public override void SyncRuntimeToDesign(Guid metadataId)
        {
            Langs metadata = _runtimeCacheProvider.FindById(metadataId);
            var clone = metadata.Clone();
            AddOrUpdate(clone);
        }

        public override void SyncDesignToRuntime(Guid metadataId)
        {
            Langs metadata;
            if (TryFindById(metadataId, out metadata))
            {
                var clone = metadata.Clone();
                _runtimeCacheProvider.AddOrUpdate(clone);
            }
        }

        /// <summary>
        /// 通过Id查找Hash
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <returns></returns>
        public override long FindHashById(Guid metadataId)
        {
            CacheLock.EnterReadLock();
            try
            {
                if (CacheHash.TryGetValue(metadataId, out var hash) == false)
                {
                    hash = FileOperator.GetFileHash(metadataId, true);
                    //设计时场景如果读取不到，读运行时
                    if (hash == 0)
                    {
                        hash = FileOperator.GetFileHash(metadataId, false);
                    }
                    CacheHash[metadataId] = hash;
                }
                return hash;
            }
            finally
            {
                CacheLock.ExitReadLock();
            }
        }
    }
}
