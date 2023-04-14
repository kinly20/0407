using DRsoft.Modeling.Metadata.Status;
using Newtonsoft.Json;
using System;

namespace DRsoft.Modeling.Metadata.Models
{
    /// <summary>
    /// 基类获取元数据
    /// </summary>
    [Serializable]
    public class BaseMetadataItem 
    {

        /// <summary>
        /// 元数据
        /// </summary>
        [JsonProperty("item")]
        public Object Metadata { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("status")]
        public StatusType Status { get; set; }

        /// <summary>
        /// 模板实例id
        /// </summary>
        [JsonProperty("instanceTemplateId")]
        public string InstanceTemplateId { get; set; }
    }
}
