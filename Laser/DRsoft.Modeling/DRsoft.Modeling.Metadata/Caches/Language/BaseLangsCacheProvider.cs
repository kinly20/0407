using DRsoft.Modeling.Metadata.Exceptions;
using DRsoft.Modeling.Metadata.Models.Language;
using System;

namespace DRsoft.Modeling.Metadata.Caches.Language
{
    internal abstract class BaseLangsCacheProvider : BaseCacheProvider<Langs>
    {
        /// <summary>
        /// 属性设计时缓存提供类
        /// </summary>
        protected BaseCacheProvider<LangMatch> _matcheProvider;

        #region Overrides of BaseCacheProvider<MetadataEntity>

        /// <summary>
        /// 新增或更新缓存
        /// </summary>
        /// <param name="metadata">元数据</param>
        public override void AddOrUpdate(Langs metadata)
        {
            //写锁
            CacheLock.EnterWriteLock();
            try
            {
                Cache[metadata.Id] = metadata;

                SyncMatches(metadata);
                //移除Hash缓存
                CacheHash.Remove(metadata.Id);
            }
            finally
            {
                CacheLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 删除元数据缓存
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        public override void Remove(Guid metadataId)
        {
            CacheLock.EnterWriteLock();
            try
            {
                Langs metadata;

                if (Cache.ContainsKey(metadataId) == false)
                {
                    throw new MetadataNotFoundException(metadataId, typeof(Langs).Name);
                }
                else
                    metadata = Cache[metadataId];

                _matcheProvider.FindAll(p => p.ParentId == metadataId).ForEach(p => _matcheProvider.Remove(p.Id));

                CacheHash.Remove(metadata.Id);
            }
            finally
            {
                CacheLock.ExitWriteLock();
            }
        }

        #endregion


        public void SyncMatches(Langs metadata)
        {
            //同步多语言元数据匹配列表
            if (metadata.LangMatches != null)
            {
                //将缓存清空
                var matches = _matcheProvider.FindAll();
                var list = matches.FindAll(p => p.ParentId == metadata.Id);
                list.ForEach(p => _matcheProvider.Remove(p.Id));
                //更新缓存
                foreach (LangMatch matche in metadata.LangMatches)
                {
                    matche.ParentId = metadata.Id;
                    matche.Id = Guid.NewGuid();
                    _matcheProvider.AddOrUpdate(matche);
                }
            }
        }
    }
}