using DRsoft.Modeling.Metadata.Interfaces;
using System;
using System.IO;

namespace DRsoft.Modeling.Metadata.FileOperators
{
    /// <summary>
    /// 产品文件操作类
    /// </summary>
    /// <typeparam name="T">元数据类型</typeparam>
    internal class ProductFileOperator<T> : BaseFileOperator<T>, IFileOperator<T> where T : IModelKey
    {
        /// <summary>
        /// 加载运行时元数据
        /// </summary>
        /// <param name="action">回调方法</param>
        public void LoadRuntimeMetadatas(Action<T> action)
        {
            LoadMetadata(MetadataFileConstants.ProductDirectory,  false, action);
        }
        /// <summary>
        /// 加载设计时元数据
        /// </summary>
        /// <param name="action">回调方法</param>
        public void LoadDesignMetadatas(Action<T> action)
        {
            LoadMetadata(MetadataFileConstants.ProductDirectory,  true, action);
        }
        /// <summary>
        /// 获取元数据文件路径
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <param name="isDesign">是否设计时</param>
        /// <returns>元数据文件路径</returns>
        public override string GetFilePath(Guid metadataId, bool isDesign)
        {
            //产品
            string fileExtension = isDesign
                ? MetadataFileConstants.DesignFileExtension
                : MetadataFileConstants.RuntimeFileExtension;
            return Path.Combine(MetadataFileConstants.MetadataDirectory, MetadataFileConstants.ProductDirectory, MetadataDirectory,
                $"{metadataId}{fileExtension}");
        }

        /// <summary>
        /// 签出元数据文件
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <returns>元数据对象</returns>
        public virtual T CheckOut(Guid metadataId)
        {
            string filePath = GetFilePath(metadataId, false);
            string designFilePath = GetFilePath(metadataId, true);

            // 签出运行时文件
            //VersionManager.CheckOutFile(filePath);=

            // 拷贝运行时到设计时
            CopyFile(filePath, designFilePath);

            T metadata = MetadataStore.Get<T>(filePath);
            return metadata;
        }

        /// <summary>
        /// 撤销签出元数据文件
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <returns>元数据对象</returns>
        public void CancelCheckOut(Guid metadataId)
        {
            string filePath = GetFilePath(metadataId, false);
            string designFilePath = GetFilePath(metadataId, true);

            // 撤销签出运行时文件
            //VersionManager.CancelCheckOut(filePath);
            // 删除设计时元数据文件
            base.DeleteMetadataFile(designFilePath);
        }

        /// <summary>
        /// 删除元数据
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        public void Delete(Guid metadataId)
        {
            string filePath = GetFilePath(metadataId, false);

            // 签出运行时文件
            //VersionManager.CheckOutFile(filePath);
            base.DeleteMetadataFile(filePath);
        }

        /// <summary>
        /// 产品模式直接保存文件
        /// </summary>
        /// <param name="metadata"></param>
        public override void SaveDesignFile(T metadata)
        {
            string filePath = GetFilePath(metadata.Id, true);

            if (Directory.Exists(Path.GetDirectoryName(filePath))==false)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }
            MetadataStore.Store(filePath, metadata);
        }
    }
}
