using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DRsoft.Modeling.Metadata.Status
{
    /// <summary>
    /// 发布列表数据实体
    /// </summary>
    public class MetadataStatusItemExt : MetadataStatusItem
    {
        /// <summary>
        /// 
        /// </summary>
        public MetadataStatusItemExt()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        public MetadataStatusItemExt(MetadataStatusItem status)
        {
            this.Url = status.Url;
            this.ControlType = status.ControlType;
            this.TypeName = status.TypeName;
            this.TypeNameText = status.TypeNameText;
            this.GroupId = status.GroupId;
            this.IsVirtual = status.IsVirtual;
            this.MetadataId = status.MetadataId;
            this.ModifiedBy = status.ModifiedBy;
            this.ModifiedGuid = status.ModifiedGuid;
            this.ModifiedOn = status.ModifiedOn;
            this.Name = status.Name;
            this.ParentId = status.ParentId;
            this.Status = status.Status;
            this.StatusText = status.StatusText;
        }
        /// <summary>
        /// 控件状态
        /// </summary>
        [XmlAttribute("metadataStatus")]
        [JsonProperty("metadataStatus")]
        public MetaDataStatus MetadataStatus { get; set; }

        /// <summary>
        /// 应用编码
        /// </summary>
        [XmlAttribute("appCode")]
        [JsonProperty("appCode")]
        public string AppCode { get; set; }

        /// <summary>
        /// 业务单元名称
        /// </summary>
        [XmlAttribute("functionName")]
        [JsonProperty("functionName")]
        public string FunctionName { get; set; }

        /// <summary>
        /// 业务单元Guid
        /// </summary>
        [XmlAttribute("functionGuid")]
        [JsonProperty("functionGuid")]
        public Guid FunctionGuid { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>
        [XmlIgnore]
        [JsonProperty("extendData")]
        public Dictionary<string, object> ExtendData { get; set; }

    }
}
