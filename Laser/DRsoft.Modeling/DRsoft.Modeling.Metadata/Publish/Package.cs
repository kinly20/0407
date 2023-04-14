using System;
using System.Collections.Generic;

namespace DRsoft.Modeling.Metadata.Publish
{
    /// <summary>
    /// 发布日志信息
    /// </summary>
    [Serializable]
    public class Package
    {

        /// <summary>
        /// 包ID
        /// </summary>
        public Guid MetadataId { get; set; }

        /// <summary>
        /// 批次名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 发布说明
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 批次时间
        /// </summary>
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// 是否已同步到云端
        /// </summary>
        public bool? Synchronized { get; set; }

        /// <summary>
        /// 包详情
        /// </summary>
        public List<PackageInfo> Infos { get; set; }

        /// <summary>
        /// 子系统Code
        /// </summary>
        public string AppCode { get; set; }
    }

    internal class UploadInfo
    {
        public string AppCode { get; set; }
        public byte[] PackageContents { get; set; }
        public List<Guid> PackageGuidList { get; set; }
        public List<string> Comments { get; set; }
    }
}
