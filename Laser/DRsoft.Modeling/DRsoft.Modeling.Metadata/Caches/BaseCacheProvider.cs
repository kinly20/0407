using DRsoft.Modeling.Metadata.Exceptions;
using DRsoft.Modeling.Metadata.FileOperators;
using DRsoft.Modeling.Metadata.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Linq;

namespace DRsoft.Modeling.Metadata.Caches
{
    /// <summary>
    /// 缓存基类
    /// </summary>
    /// <typeparam name="T">元数据类型</typeparam>
    [SuppressMessage("Microsoft.Design", "CA1001")]
    [Serializable]
    public abstract class BaseCacheProvider<T> where T : IModelKey
    {
        /// <summary>
        /// 缓存锁
        /// </summary>
        [NonSerialized]
        protected readonly ReaderWriterLockSlim CacheLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        /// <summary>
        /// 缓存
        /// </summary>
        protected Dictionary<Guid, T> Cache = new Dictionary<Guid, T>();

        /// <summary>
        /// 缓存文件Hash
        /// </summary>
        protected Dictionary<Guid, long> CacheHash = new Dictionary<Guid, long>();

        /// <summary>
        /// 文件操作类
        /// </summary>
        protected IFileOperator<T> FileOperator => FileOperatorFactory.Create<T>();

        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// 重新加载
        /// </summary>
        public virtual void Reload()
        {
            CacheLock.EnterWriteLock();
            try
            {
                Cache.Clear();
                Init();
            }
            finally
            {
                CacheLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadata"></param>
        protected virtual void OnAdded(T metadata)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadata"></param>
        protected virtual void OnRemoved(T metadata)
        {
            //移除Hash缓存
            CacheHash.Remove(metadata.Id);
        }
        /// <summary>
        /// 新增或更新缓存
        /// </summary>
        /// <param name="metadata">元数据</param>
        public virtual void AddOrUpdate(T metadata)
        {
            CacheLock.EnterWriteLock();
            try
            {
                if (Cache.TryGetValue(metadata.Id, out var removedMetadata))
                {
                    OnRemoved(removedMetadata);
                }

                Cache[metadata.Id] = metadata;
                OnAdded(metadata);
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
        public virtual void Remove(Guid metadataId)
        {
            CacheLock.EnterWriteLock();
            try
            {
                T metadata;
                var isMetadata = Cache.TryGetValue(metadataId, out metadata);
                if (Cache.Remove(metadataId) == false)
                {
                    //throw new MetadataNotFoundException(metadataId, typeof(T).Name);  
                }
                else
                {
                    if (isMetadata)
                    {
                        OnRemoved(metadata);
                    }
                }
            }
            finally
            {
                CacheLock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 同步运行时缓存到设计时
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        public abstract void SyncRuntimeToDesign(Guid metadataId);

        /// <summary>
        /// 同步设计时缓存到运行时
        /// </summary>
        /// <param name="metadataId"></param>
        public abstract void SyncDesignToRuntime(Guid metadataId);

        /// <summary>
        /// 查找单个元数据(设计时排除了删除的元数据)
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns>单个元数据</returns>
        public T Find(Predicate<T> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            CacheLock.EnterReadLock();
            try
            {
                return FindInternal(predicate);
            }
            finally
            {
                CacheLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 查找单个元数据(设计时排除了删除的元数据)
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <param name="isHasDeleted">是否包含删除的元数据</param>
        /// <returns>单个元数据</returns>
        internal T Find(Predicate<T> predicate, bool isHasDeleted)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            CacheLock.EnterReadLock();
            try
            {
                if (isHasDeleted)
                    return Cache.Values.FirstOrDefault(predicate.Invoke);
                else
                    return FindInternal(predicate);
            }
            finally
            {
                CacheLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 查找多个元数据集合
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns>元数据集合</returns>
        public List<T> FindAll(Predicate<T> predicate = null)
        {
            CacheLock.EnterReadLock();
            try
            {
                return FindAllInternal(predicate);
            }
            finally
            {
                CacheLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 通过Id查找元数据
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <returns></returns>
        public T FindById(Guid metadataId)
        {
            CacheLock.EnterReadLock();
            try
            {
                T metadata;
                if (Cache.TryGetValue(metadataId, out metadata) == false)
                {
                    throw new MetadataNotFoundException(metadataId, typeof(T).Name);
                }
                return metadata;
            }
            finally
            {
                CacheLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 视图通过Id查找元数据
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <param name="metadata">元数据</param>
        /// <returns></returns>
        public bool TryFindById(Guid metadataId, out T metadata)
        {
            CacheLock.EnterReadLock();
            try
            {
                return Cache.TryGetValue(metadataId, out metadata);
            }
            finally
            {
                CacheLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 内部查询单个
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        protected virtual T FindInternal(Predicate<T> predicate)
        {
            return Cache.Values.FirstOrDefault(predicate.Invoke);
        }

        /// <summary>
        /// 内部查询多个实体集合
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns>多个实体集合</returns>
        protected virtual List<T> FindAllInternal(Predicate<T> predicate)
        {
            if (predicate == null)
                return Cache.Values.ToList();

            return Cache.Values.Where(predicate.Invoke).ToList();
        }

        /// <summary>
        /// 通过Id查找Hash
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <returns></returns>
        public virtual long FindHashById(Guid metadataId)
        {
            CacheLock.EnterReadLock();
            try
            {
                CacheHash.TryGetValue(metadataId, out var hash);
                return hash;
            }
            finally
            {
                CacheLock.ExitReadLock();
            }
        }
    }
}
