using DRsoft.Modeling.Metadata.Caches;
using DRsoft.Modeling.Metadata.Caches.Language;
using DRsoft.Modeling.Metadata.Interfaces;
using DRsoft.Modeling.Metadata.Status;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DRsoft.Modeling.Metadata.Models.Language
{
    /// <summary>
    /// 多语言元数据
    /// </summary>
    [Serializable]
    [CacheProviderType(typeof(LangsRuntimeCacheProvider), typeof(LangsDesignCacheProvider), "Langs")]
    public sealed class Langs : IModelKey, ICloneable<Langs>
    {
        /// <summary>
        /// 多语言资源ID
        /// </summary>
        [XmlAttribute("id")]
        [JsonProperty("id")]
        public Guid Id { get; set; }


        /// <summary>
        /// 系统编码
        /// </summary>
        [XmlAttribute("application")]
        [JsonProperty("application")]
        public string Application { get; set; }

        /// <summary>
        /// 语言类型 en 英文 zh-CHS 中文 zh-CHT 繁体
        /// </summary>
        [XmlElement("Language")]
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// 资源匹配集合
        /// </summary>
        [XmlArray("List")]
        [XmlArrayItem("string")]
        [JsonProperty("langMatches")]
        public List<LangMatch> LangMatches { get; set; }

        /// <summary>
        /// 元数据状态
        /// </summary>
        [XmlAttribute]
        [JsonProperty("MetadataStatus")]
        public MetaDataStatus MetadataStatus { get; set; }

        /// <summary>
        /// 元数据版本
        /// </summary>
        /// <remarks>
        /// </remarks>
        [XmlAttribute]
        [JsonProperty("Metadataversion")]
        public string MetadataVersion { get; set; }


        /// <summary>
        /// 创建人
        /// </summary>
        [XmlAttribute]
        [JsonProperty("createdBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [XmlAttribute]
        [JsonProperty("createdOn")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [XmlAttribute]
        [JsonProperty("modifiedBy")]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        [XmlAttribute]
        [JsonProperty("modifiedOn")]
        public DateTime ModifiedOn { get; set; }

        /// <summary>
        /// 深拷贝成员到目标对象上。
        /// </summary>
        /// <param name="dest">目标对象</param>
        public void CopyTo(Langs dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.Id = Id;
            dest.CreatedBy = CreatedBy;
            dest.CreatedOn = CreatedOn;
            dest.ModifiedBy = ModifiedBy;
            dest.ModifiedOn = ModifiedOn;
            dest.MetadataStatus = MetadataStatus;
            dest.MetadataVersion = MetadataVersion;
            // dest.LangMatches = LangMatches?.Clone();
            dest.Language = Language;
            dest.Application = Application;
        }

        /// <summary>
        /// 深拷贝对象
        /// </summary>
        /// <returns>对象实例</returns>
        public Langs Clone()
        {
            var dest = new Langs();
            CopyTo(dest);
            return dest;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }

    /// <summary>
    /// 多语言的类型
    /// </summary>
    public static class LanguageCategory
    {
        /// <summary>
        /// 简体中文
        /// </summary>
        public static readonly string Chs = "zh-CHS";

        /// <summary>
        /// 英文
        /// </summary>
        public static readonly string En = "en";

        /// <summary>
        /// 繁体中文
        /// </summary>
        public static readonly string Cht = "zh-CHT";
    }
}