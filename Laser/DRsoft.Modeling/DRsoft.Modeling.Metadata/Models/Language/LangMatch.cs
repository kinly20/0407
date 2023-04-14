using DRsoft.Modeling.Metadata.Caches;
using DRsoft.Modeling.Metadata.Caches.Language;
using DRsoft.Modeling.Metadata.Interfaces;
using DRsoft.Modeling.Metadata.Status;
using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace DRsoft.Modeling.Metadata.Models.Language
{
    /// <summary>
    /// 资源匹配
    /// </summary>
    [Serializable]
    [CacheProviderType(typeof(LangMatchRuntimeCacheProvider), typeof(LangMatchDesignCacheProvider))]
    public sealed class LangMatch : ICloneable<LangMatch>, IModelKey
    {
        /// <summary>
        /// 键
        /// </summary>
        [XmlAttribute("Key")]
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// 所属 0 页面 1 模块 2 业务参数 3 自定义 4 桌面部件  5 业务组件
        /// </summary>
        [XmlAttribute("Mode")]
        [JsonProperty("mode")]
        public int Mode { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [XmlText]
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [XmlAttribute("orderNumber")]
        [JsonProperty("orderNumber")]
        public int OrderNumber { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        [XmlIgnore()]
        [JsonProperty("chsName")]
        public string ChsName { get; set; }


        /// <summary>
        /// 元数据状态（产品，扩展）
        /// </summary>
        [XmlAttribute]
        [JsonProperty("metadataStatus")]
        public MetaDataStatus MetadataStatus { get; set; }


        /// <summary>
        /// 忽略
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        public Guid ParentId { get; set; }

        /// <summary>
        /// 忽略
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        public Guid Id { get; set; }
        /// <summary>
        /// 忽略
        /// </summary>
        [XmlIgnore]
        [JsonIgnore]
        public string MetadataVersion { get; set; }


        /// <summary>
        /// 系统功能编码
        /// </summary>
        [XmlAttribute]
        [JsonProperty("functionCode")]
        public string FunctionCode { get; set; }


        /// <summary>
        /// 深拷贝成员到目标对象上。
        /// </summary>
        /// <param name="dest">目标对象</param>
        public void CopyTo(LangMatch dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.Key = Key;
            dest.Mode = Mode;
            dest.Value = Value;
            dest.ChsName = ChsName;
            dest.MetadataStatus = MetadataStatus;
            dest.OrderNumber = OrderNumber;
            dest.FunctionCode = FunctionCode;
        }

        /// <summary>
        /// 深拷贝对象
        /// </summary>
        /// <returns>对象实例</returns>
        public LangMatch Clone()
        {
            var dest = new LangMatch();
            CopyTo(dest);
            return dest;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
