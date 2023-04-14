using DRsoft.Modeling.Metadata.Exceptions;
using DRsoft.Modeling.Metadata.FileOperators;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;


namespace DRsoft.Modeling.Metadata.Status
{
    /// <summary>
    /// 元数据状态管理类
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1001")]
    internal class MetadataStatusManager
    {
        /// <summary>
        /// 缓存锁
        /// </summary>
        private readonly ReaderWriterLockSlim _cacheLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        /// <summary>
        /// 缓存
        /// </summary>
        private Dictionary<Guid, MetadataStatusItem> _cache = new Dictionary<Guid, MetadataStatusItem>();
        /// <summary>
        /// 缓存文件路径
        /// </summary>
        private readonly string _filePath = Path.Combine(MetadataFileConstants.MetadataDirectory, MetadataFileConstants.ProductDirectory,$"MetadataStatus{MetadataFileConstants.RuntimeFileExtension}");

        private static volatile MetadataStatusManager s_Instance;
        private static readonly object s_LockObject = new object();

        private MetadataStatusManager()
        { }

        /// <summary>
        /// 获取一个元数据状态管理类的实例
        /// </summary>
        /// <returns>元数据状态管理类的实例</returns>
        public static MetadataStatusManager GetInstance()
        {
            if (s_Instance == null)
            {
                lock (s_LockObject)
                {
                    if (s_Instance == null)
                    {
                        s_Instance = new MetadataStatusManager();
                        s_Instance.Init();
                    }
                }
            }
            return s_Instance;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            _cacheLock.EnterReadLock();
            try
            {
                if (MetadataStore.Exists(_filePath))
                {
                    List<MetadataStatusItem> items = MetadataStore.Get<List<MetadataStatusItem>>(_filePath);
                    //List<MetadataStatusItem> items = XmlHelper.XmlDeserializeFromFile<List<MetadataStatusItem>>(_filePath);
                    if (items.Count > 0)
                    {
                        foreach (MetadataStatusItem item in items)
                        {
                            _cache[item.MetadataId] = item;
                        }
                    }
                }
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 重新刷新缓存
        /// </summary>
        public void Reload()
        {
            _cache.Clear();
            Init();
        }

        /// <summary>
        /// 新增或修改
        /// </summary>
        /// <param name="item">元数据状态对象</param>
        public void AddOrUpdate(MetadataStatusItem item)
        {
            _cacheLock.EnterUpgradeableReadLock();
            try
            {
                try
                {
                    _cacheLock.EnterWriteLock();
                    MetadataStatusItem oldItem;
                    // 如果已存在缓存，并且状态为新增，则保持状态不变
                    if (_cache.TryGetValue(item.MetadataId, out oldItem) && oldItem.Status == StatusType.AppendNew)
                    {
                        item.Status = StatusType.AppendNew;
                    }

                    _cache[item.MetadataId] = item;
                    SaveToFile();
                }
                finally
                {
                    _cacheLock.ExitWriteLock();
                }
            }
            finally
            {
                _cacheLock.ExitUpgradeableReadLock();
            }
        }

        /// <summary>
        /// 删除元数据状态
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        public void Remove(Guid metadataId)
        {
            _cacheLock.EnterUpgradeableReadLock();
            try
            {
                try
                {
                    _cacheLock.EnterWriteLock();
                    _cache.Remove(metadataId);
                    SaveToFile();
                }
                finally
                {
                    _cacheLock.ExitWriteLock();
                }
            }
            finally
            {
                _cacheLock.ExitUpgradeableReadLock();
            }
        }

        /// <summary>
        /// 校验签出，如果被锁定会抛出异常
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <param name="userId">用户Id</param>
        public void ValidateCheckOut(Guid metadataId, string userId)
        {
            MetadataStatusItem status = Validata(metadataId, userId);
            if (status != null)
            {
                throw new MetadataLockedException($"当前元数据被{status.ModifiedBy}签出锁定，不允许编辑！");
            }
        }

        /// <summary>
        /// 判断能否签出
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <param name="userId">用户Id</param>
        public bool TryValidateCheckOut(Guid metadataId, string userId)
        {
            MetadataStatusItem status = Validata(metadataId, userId);
            if (status != null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 校验删除，如果被锁定会抛出异常
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <param name="userId">用户Id</param>
        public void ValidateDelete(Guid metadataId, string userId)
        {
            MetadataStatusItem status = Validata(metadataId, userId);
            if (status != null)
            {
                throw new MetadataLockedException($"当前元数据被{status.ModifiedBy}签出锁定，不允许删除！");
            }
        }

        private MetadataStatusItem Validata(Guid metadataId, string userId)
        {
            MetadataStatusItem status = FindById(metadataId);
            if (status != null && string.Equals(status.ModifiedGuid, userId, StringComparison.OrdinalIgnoreCase) == false)
            {
                return status;
            }
            return null;
        }

        /// <summary>
        /// 获取元数据状态
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <returns>元数据状态</returns>
        public StatusType GetStatusById(Guid metadataId)
        {
            MetadataStatusItem item = FindById(metadataId);
            if (item == null)
                return StatusType.Normal;

            return item.Status;
        }



        /// <summary>
        /// 获取元数据状态
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <param name="userGuid">用户名Id</param>
        /// <returns>元数据状态</returns>
        public StatusType GetStatusById(Guid metadataId,string userGuid)
        {
            MetadataStatusItem item = FindById(metadataId);
            if (item == null)
                return StatusType.Normal;
            // 本人签出锁定，则返回对应状态；非本人直接返回已提交状态，前段会显示签出&编辑按钮
            // 点击按钮签出&编辑按钮，会提示“当前元数据被XXX签出锁定，不允许编辑！”
            if (string.Equals(item.ModifiedGuid, userGuid, StringComparison.OrdinalIgnoreCase))
                return item.Status;

            return StatusType.Normal;
        }

        /// <summary>
        /// 通过Id查找单个状态对象
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <returns>单个状态对象</returns>
        public MetadataStatusItem FindById(Guid metadataId)
        {
            _cacheLock.EnterReadLock();
            try
            {
                MetadataStatusItem item;
                _cache.TryGetValue(metadataId, out item);
                return item;
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 获取元数据状态列表
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns>元数据状态列表</returns>
        public List<MetadataStatusItem> FindAll(Predicate<MetadataStatusItem> predicate = null)
        {
            _cacheLock.EnterReadLock();
            try
            {
                if (predicate == null)
                    return _cache.Values.ToList();

                return _cache.Values.Where(predicate.Invoke).ToList();
            }
            finally
            {
                _cacheLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 保存为文件
        /// </summary>
        private void SaveToFile()
        {
            List<MetadataStatusItem> list = _cache.Values.ToList();
            MetadataStore.Store(_filePath, list);
        }
    }
}
