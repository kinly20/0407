using DRsoft.Modeling.Metadata.FileOperators;
using DRsoft.Modeling.Metadata.Publish;
using DRsoft.Modeling.Metadata.Status;
using DRsoft.Modeling.Metadata.VersionManagers;
using DRsoft.Runtime.Core.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Transactions;
using DRsoft.Modeling.Metadata.Tools;
using DRsoft.Runtime.Core.Platform.Timing;

namespace DRsoft.Modeling.Metadata.Managers
{
    /// <summary>
    /// 本地提交事件参数
    /// </summary>
    public class LocalSubmitEventArgs : EventArgs
    {
        /// <summary>
        /// 文件
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 成员
        /// </summary>
        public MetadataStatusItem Item { get; set; }
    }


    /// <summary>
    /// 提交管理
    /// </summary>
    public abstract class MetadataSubmit
    {
        /// <summary>
        /// 批次
        /// </summary>
        protected string Batch { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        protected string Message { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        protected string Email { get; set; }

        /// <summary>
        /// 事物范围
        /// </summary>
        protected TransactionScopeOption ScopeOption = TransactionScopeOption.Required;

        /// <summary>
        /// 版本管理对象
        /// </summary>
        protected readonly IVersionManager _manager = VersionManagerFactory.Create();

        /// <summary>
        /// 本地提交之后触发
        /// </summary>
        public event EventHandler<LocalSubmitEventArgs> AfterLocalSubmit;

        /// <summary>
        /// 本地提交之前触发
        /// </summary>
        public event EventHandler<LocalSubmitEventArgs> BeforeLocalSubmit;

        /// <summary>
        /// 第一次机会
        /// </summary>
        protected virtual bool FirstChance()
        {
            Batch = GuidHelper.NewSeqGuid().ToString();
            return true;
        }

        /// <summary>
        /// 发布
        /// </summary>
        public Guid Start(List<MetadataStatusItemExt> items, string appCode, string message, string email = "")
        {
            //初始化变量
            this.Message = message;
            this.Email = email;
            Guid packageId = Guid.Empty;
            //发布到本地
            packageId = PublishToLocal(items, message, appCode);
            //遍历是否虚拟的属性
            List<MetadataStatusItemExt> datas = items.Where(data => data.IsVirtual == false).ToList();
            //第一次机会
            if (FirstChance())
            {
                List<LocalSubmitEventArgs> list = LocalSubmit(datas, message);

                //提交发布之前撤销产品文件（如果是二开扩展，需要撤销产品元数据）
                CancelProductFile(items);
                //版本提交
                VersionSubmit(list);
                //最后一次机会
                LastChance();
                //推送到云端
                PushToCloud(datas, message);
            }

            return packageId;
        }

        /// <summary>
        /// 取消签出产品元数据(仅二开扩展)
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private void CancelProductFile(List<MetadataStatusItemExt> items)
        {
            //获取产品元数据目录
            var floder = Path.Combine(MetadataFileConstants.MetadataDirectory, MetadataFileConstants.ProductDirectory);
            items.Where(t => t.IsVirtual == false && t.MetadataStatus == MetaDataStatus.Extend)
                .ToList()
                .ForEach(item =>
                {
                    var file = Path.Combine(floder, item.ControlType, item.MetadataId + MetadataFileConstants.RuntimeFileExtension);
                    //检查产品元数据状态,如果是签出状态，则撤销
                    var status = _manager.GetFileState(file);
                    if (status == 2)
                    {
                        _manager.CancelCheckOut(file);
                    }
                });
        }

        /// <summary>
        /// 本地提交
        /// </summary>
        private List<LocalSubmitEventArgs> LocalSubmit(List<MetadataStatusItemExt> items, string message)
        {
            var list = new List<LocalSubmitEventArgs>();
            foreach (var item in items)
            {
                var factory = MetadataManagerFactory.Create(item.ControlType);
                var args = new LocalSubmitEventArgs
                {
                    Item = item,
                    FilePath = factory.GetFilePath(item, false)
                };

                var handler = Volatile.Read(ref BeforeLocalSubmit);
                handler?.Invoke(null, args);

                factory.Submit(item, message);

                list.Add(args);

                handler = Volatile.Read(ref AfterLocalSubmit);
                handler?.Invoke(null, args);
            }
            return list;
        }

        /// <summary>
        /// 版本提交
        /// </summary>
        /// <param name="items"></param>
        protected virtual void VersionSubmit(List<LocalSubmitEventArgs> items)
        {
            var files = items.ConvertToDictionary(
                item => item.FilePath,
                item => (item.Item.Status == StatusType.Deleted || item.Item.Status == StatusType.Restore) ? 0 : 1);

            _manager.Submit(files, Message);
        }

        /// <summary>
        /// 发布到发布清单
        /// </summary>
        /// <param name="list"></param>
        /// <param name="comment"></param>
        /// <param name="appCode"></param>
        private Guid PublishToLocal(List<MetadataStatusItemExt> list, string comment, string appCode)
        {
            Package package = new Package();

            DateTime dtmNow = Clock.Now;
            package.MetadataId = GuidHelper.NewSeqGuid();
            package.Name = $"更新包_{dtmNow:yyyyMMddHHmm}";
            package.ModifiedOn = dtmNow;
            package.Comment = comment;
            package.Synchronized = false;
            package.AppCode = appCode;

            package.Infos = new List<PackageInfo>();

            foreach (var item in list)
            {
                var packageinfo = new PackageInfo()
                {
                    MetadataId = item.MetadataId,
                    ParentId = item.ParentId.HasValue ? item.ParentId.Value.ToString() : null,
                    IsVirtual = item.IsVirtual,
                    Name = item.Name,
                    TypeName = item.TypeName,
                    ControlType = item.ControlType,
                    Status = item.Status,
                    ModifiedBy = item.ModifiedBy,
                    ModifiedOn = item.ModifiedOn,
                    MetadataStatus = item.MetadataStatus
                };
                package.Infos.Add(packageinfo);
            }

            PackageManager pm = new PackageManager();
            pm.PublicPackage(package);

            return package.MetadataId;
        }

        /// <summary>
        /// 推送到云端
        /// </summary>
        /// <param name="list"></param>
        /// <param name="comment"></param>
        private void PushToCloud(List<MetadataStatusItemExt> list, string comment)
        { }

        /// <summary>
        /// 最后一次机会
        /// </summary>
        protected virtual void LastChance()
        {
            //CacheManager.Remove("_metadata");
        }
    }

    /// <summary>
    /// 离线模式元数据提交
    /// </summary>
    public class OfflineMetadataSubmit : MetadataSubmit
    {
    }

    /// <summary>
    /// Git元数据提交
    /// </summary>
    public class GitMetadataSubmit : MetadataSubmit
    {
    }
}

