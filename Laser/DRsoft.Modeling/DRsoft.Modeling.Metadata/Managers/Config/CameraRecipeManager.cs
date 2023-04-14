using System;
using System.IO;
using DRsoft.Modeling.Metadata.FileOperators;
using DRsoft.Modeling.Metadata.Models.Config;
using DRsoft.Modeling.Metadata.Status;
using DRsoft.Modeling.Metadata.WorkMode;
using DRsoft.Runtime.Core.Common;
using DRsoft.Runtime.Core.Platform.Timing;
using Newtonsoft.Json;


namespace DRsoft.Modeling.Metadata.Managers.Config
{
    /// <summary>
    /// 元数据管理
    /// </summary>
    internal class CameraRecipeManager : BaseManager<CamRecipeConfig>, IMetadataManager
    {
        /// <summary>
        /// 实例
        /// </summary>
        private static volatile CameraRecipeManager s_Instance;

        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object s_LockObject = new object();

        public static CameraRecipeManager GetInstance()
        {
            if (s_Instance == null)
            {
                lock (s_LockObject)
                {
                    if (s_Instance == null)
                    {
                        s_Instance = new CameraRecipeManager();
                    }
                }
            }
            return s_Instance;
        }

        public override Guid Insert(CamRecipeConfig metadata)
        {
            if (metadata == null) throw new ArgumentNullException(nameof(metadata));

            if (metadata.Id == Guid.Empty)
                metadata.Id = GuidHelper.NewSeqGuid();

            if (WorkMode != EnumWorkMode.Product)
            {
                metadata.MetadataStatus = MetaDataStatus.Customize;
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

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="metadata"></param>
        public override void Update(CamRecipeConfig metadata)
        {
            // 更新最后修改人，最后修改时间
            metadata.ModifiedBy = CurrentUserName;
            metadata.ModifiedOn = Clock.Now;

            //产品元数据保存产品完整字段z
            FileOperator.SaveDesignFile(metadata);
            // 更新元数据缓存
            CacheProvider.AddOrUpdate(metadata);
            // 更新缓存状态列表，状态为修改
            StatusManager.AddOrUpdate(GetStatusItem(metadata, StatusType.Modified));
        }

        /// <summary>
        /// 更新
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
            CamRecipeConfig metadata = JsonConvert.DeserializeObject<CamRecipeConfig>(jsonData, JsonSetting);
            Update(metadata);
        }

        /// <summary>
        /// 签出文件
        /// </summary>
        /// <param name="metadataId"></param>
        public void CheckOut(Guid metadataId)
        {
            // 校验签出，如果被锁定会抛出异常
            StatusManager.ValidateCheckOut(metadataId, CurrentUserGuid);
            if (StatusManager.GetStatusById(metadataId) == StatusType.Normal)
            {
                // 签出运行时文件，生成设计时文件
                CamRecipeConfig metadata = FileOperator.CheckOut(metadataId);
                // 刷新二开运行时
                CamRecipeConfig runtimeMetadata = metadata.Clone();
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
        private string targetPath = "";
        public void CreatRecipeFile(Guid OriginalGuid, Guid newGuid,out string targetPath)
        {
            string startpath = AppDomain.CurrentDomain.BaseDirectory;
            string sourcePath= startpath + GetFilePath(OriginalGuid, false);
            string path1 = sourcePath.Substring(0, sourcePath.LastIndexOf("\\"));
            targetPath = path1 +"\\"+ newGuid.ToString() + ".metadata.config";
            CamRecipeConfig metadata = FileOperator.LoadMetadata(sourcePath);
            // 更新最后修改人，最后修改时间,Id
            metadata.ModifiedBy = CurrentUserName;
            metadata.ModifiedOn = Clock.Now;
            metadata.Id = newGuid;
            MetadataStore.Store(targetPath, metadata);
        }
        public void DeleteRecipeFile(string filepath)
        {
            MetadataStore.Delete(filepath);
        }
        public void CopyFile(string sourcePath, string targetPath)
        {
            //源路径为空
            if (sourcePath == null)
            {
                throw new ArgumentNullException(nameof(sourcePath));
            }
            //目标路径为空
            if (targetPath == null)
            {
                throw new ArgumentNullException(nameof(targetPath));
            }
            //获取路径目录
            string dir = Path.GetDirectoryName(targetPath);

            //如果目录为空
            if (dir == null)
            {
                throw new InvalidOperationException($"文件路径{targetPath}不正确。");
            }
            //如果不存在目录，则创建目录
            if (Directory.Exists(dir) == false)
            {
                Directory.CreateDirectory(dir);
            }
            //如果存在目标文件
            if (File.Exists(targetPath))
            {
                File.SetAttributes(targetPath, FileAttributes.Normal);
            }
            MetadataStore.Copy(sourcePath, targetPath);
        }
        public override void Delete(Guid metadataId)
        {
            //判断是否被锁定
            StatusManager.ValidateDelete(metadataId, CurrentUserGuid);
            var metadata = CacheProvider.FindById(metadataId);

            if (StatusManager.GetStatusById(metadataId) == StatusType.AppendNew ||
                FileOperator.HasRuntimeFile(metadataId) == false)
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
                {
                    status = StatusType.Restore;
                }
                // 添加到缓存状态列表，类型为控件，状态为删除
                StatusManager.AddOrUpdate(GetStatusItem(metadata, status));
            }
        }
        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isDesign"></param>
        /// <returns></returns>
        public string GetFilePath(MetadataStatusItem item, bool isDesign)
        {
            return FileOperator.GetFilePath(item.MetadataId, isDesign);
        }
        public string GetFilePath(Guid id, bool isDesign)
        {
            return FileOperator.GetFilePath(id, isDesign);
        }
        /// <summary>
        /// 获取最后修改的时间
        /// </summary>
        /// <param name="metadataId"></param>
        /// <returns></returns>
        public DateTime GetModifiedOn(Guid metadataId)
        {
            CamRecipeConfig metadata;
            if (RuntimeCacheProvider.TryFindById(metadataId, out metadata) == false)
            {
                metadata = CacheProvider.FindById(metadataId);
            }
            return metadata.ModifiedOn;
        }

        /// <summary>
        /// 获取元数据的状态
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public override MetadataStatusItem GetStatusItem(CamRecipeConfig metadata, StatusType status)
        {
            return new MetadataStatusItem
            {
                MetadataId = metadata.Id,
                Name = $"Engine",
                TypeName = MetadataType.Engine,
                ControlType = "Engine",
                ModifiedBy = metadata.ModifiedBy,
                ModifiedGuid = CurrentUserGuid,
                ModifiedOn = metadata.ModifiedOn,
                Status = status
            };
        }
        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="item"></param>
        /// <param name="checkInMessage"></param>
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
    }
}
