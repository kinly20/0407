using DRsoft.Modeling.Metadata.FileOperators;
using DRsoft.Modeling.Metadata.Status;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml.Serialization;
using DRsoft.Modeling.Metadata.Tools;

namespace DRsoft.Modeling.Metadata.Publish
{
    /// <summary>
    /// 包信息
    /// </summary>
    [Serializable]
    public class PackageInfo
    {
        /// <summary>
        /// 元数据Id
        /// </summary>
        public Guid MetadataId { get; set; }

        /// <summary>
        /// 父级数据Id，不保存到状态文件中，每次展示时自动计算。
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 表示是否虚拟节点，这种节点不应该被提交
        /// </summary>
        public bool IsVirtual { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public MetadataType TypeName { get; set; }

        /// <summary>
        /// 类型名称文本
        /// </summary>
        [XmlIgnore]
        public string TypeNameText => TypeName.GetDescription();

        /// <summary>
        /// 控件类型
        /// </summary>
        public string ControlType { get; set; }

        /// <summary>
        /// 元数据状态
        /// </summary>
        public StatusType Status { get; set; }

        /// <summary>
        /// 元数据状态文本
        /// </summary>
        [XmlIgnore]
        public string StatusText => Status.GetDescription();

        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// 用于FunctionPage的Url
        /// </summary>
        [XmlIgnore]
        public string Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute("metadataStatus")]
        [JsonProperty("metadataStatus")]
        public MetaDataStatus MetadataStatus { get; set; }
    }

    /// <summary>
    /// 包信息扩展
    /// </summary>
    public static class PackageInfoExtensions
    {
        /// <summary>
        /// 获取元数据的文件夹
        /// </summary>
        /// <param name="packageInfo">包信息</param>
        /// <returns></returns>
        public static string GetMetadataFolder(this PackageInfo packageInfo)
        {
            // todo 何泽云(补齐其它类型)
            string folder = string.Empty;
            if (packageInfo.TypeName == MetadataType.Engine)
            {
                folder = "Engine";
            }
            else
            {
                folder = packageInfo.ControlType;
            }
            return folder;
        }

        /// <summary>
        /// 获取更新包中元数据的文件地址，默认是相对路径
        /// </summary>
        /// <param name="packageInfo"></param>
        /// <param name="isRelativePath">是否相对路径</param>
        public static string GetPackageFilePath(this PackageInfo packageInfo, bool isRelativePath = true)
        {
            string prefix = string.Empty;
            if (isRelativePath)
                prefix = MetadataFileConstants.ProductDirectory;
            else
                prefix = Path.Combine(MetadataFileConstants.MetadataDirectory, MetadataFileConstants.ProductDirectory);

            return $@"{prefix}\{packageInfo.GetMetadataFolder()}\{packageInfo.MetadataId}{MetadataFileConstants.RuntimeFileExtension}";
        }
    }
}
