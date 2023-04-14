using DRsoft.Modeling.Metadata.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DRsoft.Modeling.Metadata.Caches
{
    /// <summary>
    /// 设计时缓存提供类(覆盖型)
    /// </summary>
    /// <typeparam name="T">元数据类型</typeparam>
    internal sealed class DesignCacheProvider<T> : BaseCacheProvider<T> where T : IModelKey, ICloneable<T>
    {
        /// <summary>
        /// 运行时缓存提供类的实例
        /// </summary>
        private readonly BaseCacheProvider<T> _provider = CacheProviderFactory<T>.Create(CacheType.Runtime);

        /// <summary>
        /// 初始化
        /// </summary>
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
            // 运行时重新加载
            _provider.Reload();
            // 设计时重新加载
            base.Reload();
        }

        /// <summary>
        /// 同步运行时缓存到设计时
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        public override void SyncRuntimeToDesign(Guid metadataId)
        {
            T metadata = _provider.FindById(metadataId);
            AddOrUpdate(metadata.Clone<T>());
        }

        /// <summary>
        /// 同步设计时缓存到运行时
        /// </summary>
        /// <param name="metadataId"></param>
        public override void SyncDesignToRuntime(Guid metadataId)
        {
            T metadata = FindById(metadataId);
            _provider.AddOrUpdate(metadata.Clone<T>());
        }

        /// <summary>
        /// 删除元数据缓存
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        public override void Remove(Guid metadataId)
        {
            // 删除设计时缓存
            base.Remove(metadataId);

            // 删除运行时缓存,存在才删除
            T metadata;
            if (_provider.TryFindById(metadataId, out metadata))
            {
                _provider.Remove(metadataId);
            }
        }
        
        protected override T FindInternal(Predicate<T> predicate)
        {
            //return Cache.Values.FirstOrDefault(t=> predicate(t) && CanFind(t));
            return Cache.Values.FirstOrDefault(t => predicate(t));

        }

        protected override List<T> FindAllInternal(Predicate<T> predicate)
        {
            //if (predicate == null)
            //    return Cache.Values.Where(CanFind).ToList();

            //return Cache.Values.Where(t=> predicate(t) && CanFind(t)).ToList();

            if (predicate == null)
                return Cache.Values.ToList();

            return Cache.Values.Where(t=> predicate(t)).ToList();
        }

        /// <summary>
        /// 同步运行时缓存到设计时
        /// </summary>
        private void SyncAllRuntimeToDesign()
        {
            List<T> list = _provider.FindAll();
            foreach (T metadata in list)
            {
                Cache[metadata.Id] = metadata.Clone<T>();
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
                    hash = FileOperator.GetFileHash(metadataId,true);
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
