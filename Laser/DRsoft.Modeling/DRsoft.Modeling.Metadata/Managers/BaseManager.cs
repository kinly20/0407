using DRsoft.Modeling.Metadata.Caches;
using DRsoft.Modeling.Metadata.FileOperators;
using DRsoft.Modeling.Metadata.Interfaces;
using DRsoft.Modeling.Metadata.Models;
using DRsoft.Modeling.Metadata.Status;
using DRsoft.Modeling.Metadata.WorkMode;
using DRsoft.Runtime.Core.Common.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace DRsoft.Modeling.Metadata.Managers
{
    internal abstract class BaseManager<T> where T : IModelKey
    {
        /// <summary>
        /// 设计时缓存提供类
        /// </summary>
        protected BaseCacheProvider<T> CacheProvider => CacheProviderFactory<T>.Create(CacheType.Design);
        /// <summary>
        /// 运行时缓存提供类
        /// </summary>
        protected BaseCacheProvider<T> RuntimeCacheProvider => CacheProviderFactory<T>.Create(CacheType.Runtime);
        /// <summary>
        /// 文件操作类
        /// </summary>
        protected IFileOperator<T> FileOperator = FileOperatorFactory.Create<T>();
        /// <summary>
        /// 状态管理类
        /// </summary>
        protected MetadataStatusManager StatusManager => MetadataStatusManager.GetInstance();
        /// <summary>
        /// 设置元数据JSON序列化所需要的格式{1.为了使用抽象类,需要生成TypeName,2.为了与以前老版本前端代码兼容,枚举序列化为字符串,小写字母开头.}
        /// </summary>
        protected JsonSerializerSettings JsonSetting => new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Converters = new List<JsonConverter>
            {
                new StringEnumConverter
                {
                    // CamelCaseText = true,
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                new XmlCdataJsonConverter()
            },

        };

        /// <summary>
        /// 工作模式
        /// </summary>
        protected EnumWorkMode WorkMode => EnumWorkMode.Product;

        private string _currentUserName;
        /// <summary>
        /// 当前用户名
        /// </summary>
        public string CurrentUserName
        {
            get => _currentUserName;
            internal set => _currentUserName = value;
        }
        /// <summary>
        /// 当前用户编码
        /// </summary>
        public string CurrentUserGuid => new Guid().ToString();

        /// <summary>
        /// 设置前缀
        /// </summary>
        /// <param name="metadata"></param>
        public virtual void SetPrefix(T metadata)
        {

        }
        /// <summary>
        /// 新增元数据
        /// </summary>
        /// <param name="metadata">元数据</param>
        public abstract Guid Insert(T metadata);

        /// <summary>
        /// 修改元数据
        /// </summary>
        /// <param name="metadata">元数据对象</param>
        public abstract void Update(T metadata);

        public abstract void Delete(Guid metadataId);

        /// <summary>
        /// 撤销签出元数据
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        public void CancelCheckOut(Guid metadataId)
        {
            var status = StatusManager.GetStatusById(metadataId);
            // 如果是新增的文件,则直接删除设计时,并删除设计时缓存
            if (status == StatusType.AppendNew)
            {
                FileOperator.DeleteDesignFile(metadataId);

                CacheProvider.Remove(metadataId);
            }
            else
            {
                // 撤销签出运行时文件，删除设计时文件
                FileOperator.CancelCheckOut(metadataId);
                // 还原模式下使用文件刷新缓存
                // 因为执行产品还原时会将缓存置为产品元数据
                // 然后撤销读到的也就还是产品，但其实是有二开内容的
                if (status == StatusType.Restore)
                {
                    var runtimeMetadata = FileOperator.LoadMetadata(FileOperator.GetFilePath(metadataId, false));
                    // 更新运行时缓存
                    RuntimeCacheProvider.AddOrUpdate(runtimeMetadata);
                }
                T entity;
                if (RuntimeCacheProvider.TryFindById(metadataId, out entity))
                {
                    // 同步运行时到设计时(仅限拥有运行时)
                    CacheProvider.SyncRuntimeToDesign(metadataId);
                }
            }
            // 从缓存状态列表中删除
            StatusManager.Remove(metadataId);
        }

        /// <summary>
        /// 通过元数据对象获取元数据状态对象
        /// </summary>
        /// <param name="metadata">元数据</param>
        /// <param name="status">状态</param>
        /// <returns>元数据状态对象</returns>
        public abstract MetadataStatusItem GetStatusItem(T metadata, StatusType status);

        /// <summary>
        /// 获取控件元数据
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <returns>控件元数据</returns>
        public BaseMetadataItem GetMetadataItemEx(Guid metadataId)
        {
            return new BaseMetadataItem
            {
                Metadata = CacheProvider.FindById(metadataId),
                Status = StatusManager.GetStatusById(metadataId, CurrentUserGuid)
            };
        }

        /// <summary>
        /// 获取控件元数据
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <returns>控件元数据</returns>
        public object GetMetadataItem(Guid metadataId)
        {
            return new
            {
                item = CacheProvider.FindById(metadataId),
                status = StatusManager.GetStatusById(metadataId, CurrentUserGuid)
            };
        }

        /// <summary>
        /// 重新加载全部元数据
        /// </summary>
        public void Reload()
        {
            CacheProvider.Reload();
        }

        public string GetAppCode(string filepath)
        {
            return FileOperator.GetAppCode(filepath);
        }
    }    
}
