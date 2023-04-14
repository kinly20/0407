using DRsoft.Modeling.Metadata.Models;
using DRsoft.Modeling.Metadata.Status;
using System;

namespace DRsoft.Modeling.Metadata.Managers
{
    /// <summary>
    /// 元数据管理类接口
    /// </summary>
    internal interface IMetadataManager
    {
        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <returns></returns>
        string GetFilePath(MetadataStatusItem item,bool isDesign);

        /// <summary>
        /// 提交元数据
        /// </summary>
        /// <param name="item">元数据状态对象</param>
        /// <param name="checkInMessage">签入信息</param>
        void Submit(MetadataStatusItem item, string checkInMessage);

        /// <summary>
        /// 保存元数据
        /// </summary>
        /// <param name="jsonData">json字符串</param>
        /// <param name="isSubmit">是否提交</param>
        void Update(string jsonData, bool isSubmit);

        /// <summary>
        /// 获取元数据对象
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <returns>元数据对象</returns>
        BaseMetadataItem GetMetadataItemEx(Guid metadataId);

        /// <summary>
        /// 获取元数据对象
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <returns>元数据对象</returns>
        object GetMetadataItem(Guid metadataId);

        /// <summary>
        /// 签出元数据
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        void CheckOut(Guid metadataId);

        /// <summary>
        /// 撤销签出元数据
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        void CancelCheckOut(Guid metadataId);

        /// <summary>
        /// 删除元数据
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        void Delete(Guid metadataId);
        
        /// <summary>
        /// 重新加载全部元数据
        /// </summary>
        void Reload();

        /// <summary>
        /// 获取运行时最后修改时间
        /// </summary>
        /// <param name="metadataId">元数据Id</param>
        /// <returns>运行时最后修改时间</returns>
        DateTime GetModifiedOn(Guid metadataId);

        /// <summary>
        /// 获取元数据的子系统Code
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        string GetAppCode(string filepath);
    }
}
