using DRsoft.Modeling.Metadata.Interfaces;
using System;

namespace DRsoft.Modeling.Metadata.Caches
{
    /// <summary>
    /// 运行时缓存提供类
    /// </summary>
    /// <typeparam name="T">元数据类型</typeparam>
    internal sealed class RuntimeCacheProvider<T> : BaseCacheProvider<T> where T : IModelKey
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            FileOperator.LoadRuntimeMetadatas(AddOrUpdate);
        }

        /// <summary>
        /// 运行时不需要实现此接口
        /// </summary>
        /// <param name="metadataId"></param>
        public override void SyncRuntimeToDesign(Guid metadataId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 运行时不需要实现此接口
        /// </summary>
        /// <param name="metadataId"></param>
        public override void SyncDesignToRuntime(Guid metadataId)
        {
            throw new NotImplementedException();
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
                    hash = FileOperator.GetFileHash(metadataId, false);
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
