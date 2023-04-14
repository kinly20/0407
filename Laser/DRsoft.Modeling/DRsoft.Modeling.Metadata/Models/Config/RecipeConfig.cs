using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using DRsoft.Modeling.Metadata.Caches;
using DRsoft.Modeling.Metadata.Interfaces;
using DRsoft.Modeling.Metadata.Status;
using Newtonsoft.Json;

namespace DRsoft.Modeling.Metadata.Models.Config
{
    [Serializable]
    [XmlType("RecipeConfig")]
    [CacheProviderType(typeof(RuntimeCacheProvider<RecipeConfig>), typeof(DesignCacheProvider<RecipeConfig>), Directory = "RecipeConfig")]
    public class RecipeConfig : IModelKey, ICloneable<RecipeConfig>
    {
        /// <summary>
        /// 选择的配方名称
        /// </summary>
        [XmlElement("SelectRecipeName")]
        [JsonProperty("SelectRecipeName")]
        public string? SelectRecipeName { get; set; }
        [XmlArray("LisRecipeNote")]
        [XmlArrayItem("RecipeNote")]
        [JsonProperty("LisRecipeNote")]
        public List<RecipeNote> LisRecipeNote { get; set; } = new List<RecipeNote>();

        #region
        [XmlElement("Language")]
        [JsonProperty("Language")]
        public string? Language { get; set; }

        [XmlAttribute("id")]
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [XmlAttribute("MetadataVersion")]
        [JsonProperty("MetadataVersion")]
        public string? MetadataVersion { get; set; }
        [XmlAttribute("MetadataStatus")]
        [JsonProperty("MetadataStatus")]
        public MetaDataStatus MetadataStatus { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [XmlAttribute]
        [JsonProperty("createdBy")]
        public string? CreatedBy { get; set; }
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
        public string? ModifiedBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [XmlAttribute]
        [JsonProperty("modifiedOn")]
        public DateTime ModifiedOn { get; set; }
        public RecipeConfig Clone()
        {
            var clone = new RecipeConfig();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        public void CopyTo(RecipeConfig dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.Id = this.Id;
            dest.MetadataVersion = this.MetadataVersion;
            dest.MetadataStatus = this.MetadataStatus;
            dest.Language = this.Language;
            dest.SelectRecipeName = this.SelectRecipeName;
            dest.LisRecipeNote = LisRecipeNote.Select(x => x.Clone<RecipeNote>()).ToList();
        }
    }

    [Serializable]
    public class RecipeNote : ICloneable<RecipeNote>
    {
        [XmlAttribute("Name")]
        [JsonProperty("Name")]
        public string? Name { get; set; }

        [XmlAttribute("Id")]
        [JsonProperty("Id")]
        public Guid[] Id { get; set; } = new Guid[3]{new Guid(),new Guid(),new Guid()};

        [XmlAttribute("Path")]
        [JsonProperty("Path")]
        public string[]? Path { get; set; } = new string[3]{"","",""};

        [XmlAttribute("Order")]
        [JsonProperty("Order")]
        public int Order { get; set; }//配方序号，界面表格用
        public RecipeNote Clone()
        {
            var clone = new RecipeNote();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        public void CopyTo(RecipeNote dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.Name = this.Name;
            dest.Id = this.Id;
            dest.Path = this.Path;
            dest.Order = this.Order;
        }
    }
}
