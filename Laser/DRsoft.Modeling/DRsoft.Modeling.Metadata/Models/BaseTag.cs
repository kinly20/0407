using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace DRsoft.Modeling.Metadata.Models
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class BaseTag
    {
        [XmlAttribute("MetadataTag")]
        [JsonProperty("MetadataTag")]
        public string MetadataTag { get; set; }
    }
}
