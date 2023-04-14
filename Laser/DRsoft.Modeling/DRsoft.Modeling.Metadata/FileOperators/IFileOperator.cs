using DRsoft.Modeling.Metadata.Interfaces;
using System;

namespace DRsoft.Modeling.Metadata.FileOperators
{
    /// <summary>
    /// 文件操作接口
    /// </summary>
    /// <typeparam name="T">元数据类型</typeparam>
    public interface IFileOperator<T> where T : IModelKey
    {
        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="metadataId"></param>
        /// <param name="isDesign"></param>
        /// <returns></returns>
        string GetFilePath(Guid metadataId, bool isDesign);
     //  void CopyFile(string sourcePath, string targetPath);//新增
        /// <summary>
        /// 是否仅存在设计时文件
        /// </summary>
        /// <param name="metadataId">元数据id</param>
        /// <returns>是否仅存在设计时文件</returns>
        bool HasRuntimeFile(Guid metadataId);

        /// <summary>
        /// 加载运行时元数据
        /// </summary>
        /// <param name="action">回调方法</param>
        void LoadRuntimeMetadatas(Action<T> action);

        /// <summary>
        /// 加载设计时元数据
        /// </summary>
        /// <param name="action">回调方法</param>
        void LoadDesignMetadatas(Action<T> action);

        /// <summary>
        /// 保存设计时文件
        /// </summary>
        /// <param name="metadata">元数据对象</param>
        void SaveDesignFile(T metadata);

        /// <summary>
        /// 签出元数据文件
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <returns>元数据对象</returns>
        T CheckOut(Guid metadataId);
        /// <summary>
        /// 撤销签出元数据文件
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        void CancelCheckOut(Guid metadataId);

        /// <summary>
        /// 删除元数据
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        void Delete(Guid metadataId);

        /// <summary>
        /// 删除设计时文件
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        void DeleteDesignFile(Guid metadataId);

        /// <summary>
        /// 确认删除元数据
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <param name="checkInMessage"></param>
        void ConfirmDelete(Guid metadataId, string checkInMessage);

        /// <summary>
        /// 提交元数据
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <param name="checkInMessage">签入信息</param>
        string Submit(Guid metadataId, string checkInMessage);

        /// <summary>
        /// 根据文件加载元数据
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <returns>元数据类型</returns>
        T LoadMetadata(string file);

        /// <summary>
        /// 是否产品目录存在该元数据
        /// </summary>
        /// <param name="metadataId"></param>
        /// <returns></returns>
        bool IsProduct(Guid metadataId);

        /// <summary>
        /// 读取元数据的子系统Code
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        string GetAppCode(string filepath);

        /// <summary>
        /// 获取元数据文件Hash
        /// </summary>
        /// <param name="metadataId"></param>
        /// <param name="isDesign"></param>
        /// <returns></returns>
        long GetFileHash(Guid metadataId, bool isDesign);
    }
}
