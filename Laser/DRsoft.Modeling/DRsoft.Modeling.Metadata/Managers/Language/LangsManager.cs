using DRsoft.Modeling.Metadata.Models.Language;
using DRsoft.Modeling.Metadata.Status;
using DRsoft.Modeling.Metadata.WorkMode;
using DRsoft.Runtime.Core.Common;
using Newtonsoft.Json;
using System;
using System.Linq;
using DRsoft.Runtime.Core.Platform.Timing;

namespace DRsoft.Modeling.Metadata.Managers.Language
{
    /// <summary>
    /// 多语言
    /// </summary>
    internal sealed class LangsManager : BaseManager<Langs>, IMetadataManager
    {
        private static volatile LangsManager s_Instance;
        private static readonly object s_LockObject = new object();

        /// <summary>
        /// 获取一个管理类的实例
        /// </summary>
        /// <returns>实例</returns>
        public static LangsManager GetInstance()
        {
            if (s_Instance == null)
            {
                lock (s_LockObject)
                {
                    if (s_Instance == null)
                    {
                        s_Instance = new LangsManager();
                    }
                }
            }

            return s_Instance;
        }
        /// <summary>
        /// 元数据新增
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public override Guid Insert(Langs metadata)
        {
            if (metadata == null) throw new ArgumentNullException(nameof(metadata));
            if (metadata.Id == Guid.Empty)
                metadata.Id = GuidHelper.NewSeqGuid();
            //多语言排序
            metadata.LangMatches = metadata.LangMatches.OrderBy(x => x.Key).ToList();
            //这里只存在新增情况
            if (WorkMode != EnumWorkMode.Product)
            {
                metadata.MetadataStatus = MetaDataStatus.Customize;
                metadata.LangMatches.ToList().ForEach(item =>
                {
                    item.MetadataStatus = MetaDataStatus.Customize;
                });
            }
            metadata.CreatedOn = Clock.Now;
            metadata.CreatedBy = CurrentUserName;
            metadata.ModifiedOn = Clock.Now;
            metadata.ModifiedBy = CurrentUserName;
            // 生成设计时元数据文件
            FileOperator.SaveDesignFile(metadata);
            // 添加到设计时缓存
            CacheProvider.AddOrUpdate(metadata);
            // 添加到缓存状态列表，状态为：新增
            StatusManager.AddOrUpdate(GetStatusItem(metadata, StatusType.AppendNew));

            return metadata.Id;
        }

        public override void Update(Langs metadata)
        {
            //多语言排序
            metadata.LangMatches = metadata.LangMatches.OrderBy(x => x.Key).ToList();
            // 更新最后修改人，最后修改时间
            metadata.ModifiedBy = CurrentUserName;
            metadata.ModifiedOn = Clock.Now;
            if (WorkMode != EnumWorkMode.Product)
            {
                Langs temp = metadata.Clone();
                FileOperator.SaveDesignFile(temp);
            }
            else
            {
                FileOperator.SaveDesignFile(metadata);
            }
            // 更新元数据缓存
            CacheProvider.AddOrUpdate(metadata);
            // 更新缓存状态列表，状态为修改
            StatusManager.AddOrUpdate(GetStatusItem(metadata, StatusType.Modified));

        }

        public string GetFilePath(MetadataStatusItem item, bool isDesign)
        {
            return FileOperator.GetFilePath(item.MetadataId, isDesign);
        }

        public void Submit(MetadataStatusItem item, string checkInMessage)
        {
            if (item.Status == StatusType.Deleted || item.Status == StatusType.Restore)
            {
                // 删除设计时文件
                FileOperator.DeleteDesignFile(item.MetadataId);
                // 删除文件，并签入
                FileOperator.ConfirmDelete(item.MetadataId, checkInMessage);
                // 从缓存中移除
                CacheProvider.Remove(item.MetadataId);
            }
            else
            {
                // 将设计时缓存保存到运行时
                CacheProvider.SyncDesignToRuntime(item.MetadataId);
                // 保存到文件
                FileOperator.Submit(item.MetadataId, checkInMessage);
            }

            // 从状态列表中删除
            StatusManager.Remove(item.MetadataId);
        }

        /// <summary>
        /// 更新原数据
        /// </summary>
        /// <param name="jsonData"></param>
        /// <param name="isSubmit"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Update(string jsonData, bool isSubmit)
        {
            if (string.IsNullOrEmpty(jsonData))
            {
                throw new ArgumentNullException(nameof(jsonData));
            }
            Langs metadata = JsonConvert.DeserializeObject<Langs>(jsonData, JsonSetting);
            Update(metadata);
        }

        /// <summary>
        ///  签出，会同步更新运行时缓存，设计时缓存
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        public void CheckOut(Guid metadataId)
        {
            // 校验签出，如果被锁定会抛出异常
            StatusManager.ValidateCheckOut(metadataId, CurrentUserGuid);
            if (StatusManager.GetStatusById(metadataId) == StatusType.Normal)
            {
                // 签出运行时文件，生成设计时文件
                Langs metadata = FileOperator.CheckOut(metadataId);
                // 刷新二开运行时
                Langs runtimeMetadata = metadata.Clone();
                // 更新运行时缓存
                RuntimeCacheProvider.AddOrUpdate(runtimeMetadata);
                // 更新最后修改人，最后修改时间
                metadata.ModifiedBy = CurrentUserName;
                metadata.ModifiedOn = Clock.Now;
                //更新设计时缓存
                CacheProvider.AddOrUpdate(metadata);
                // 添加到缓存状态列表，类型为控件，状态为修改
                StatusManager.AddOrUpdate(GetStatusItem(metadata, StatusType.Modified));
            }
        }

        public override void Delete(Guid metadataId)
        {
            StatusManager.ValidateDelete(metadataId, CurrentUserGuid);

            var metadata = CacheProvider.FindById(metadataId);

            //元数据为新增 或 不存在运行时
            if (StatusManager.GetStatusById(metadataId) == StatusType.AppendNew || FileOperator.HasRuntimeFile(metadataId) == false)
            {
                // 删除设计时文件
                FileOperator.DeleteDesignFile(metadataId);
                // 删除设计时缓存
                CacheProvider.Remove(metadataId);
                // 从状态缓存列表中删除
                StatusManager.Remove(metadataId);
            }
            else
            {
                // 签出元数据文件
                FileOperator.Delete(metadataId);

                metadata.ModifiedBy = CurrentUserName;
                metadata.ModifiedOn = Clock.Now;

                StatusType status = StatusType.Deleted;
                if (metadata.MetadataStatus == MetaDataStatus.Extend)
                    status = StatusType.Restore;

                // 添加到缓存状态列表，状态为删除
                StatusManager.AddOrUpdate(GetStatusItem(metadata, status));
            }
        }

        public DateTime GetModifiedOn(Guid metadataId)
        {
            Langs metadata;
            if (RuntimeCacheProvider.TryFindById(metadataId, out metadata) == false)
            {
                metadata = CacheProvider.FindById(metadataId);
            }
            return metadata.ModifiedOn;
        }

        public override MetadataStatusItem GetStatusItem(Langs metadata, StatusType status)
        {
            return new MetadataStatusItem
            {
                MetadataId = metadata.Id,
                TypeName = MetadataType.Langs,
                ControlType = "Langs",
                ModifiedBy = metadata.ModifiedBy,
                ModifiedGuid = CurrentUserGuid,
                ModifiedOn = metadata.ModifiedOn,
                Status = status
            };
        }


    }
}
