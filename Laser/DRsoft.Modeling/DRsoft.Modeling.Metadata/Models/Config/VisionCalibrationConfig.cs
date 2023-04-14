using System;
using System.Xml.Serialization;
using DRsoft.Modeling.Metadata.Caches;
using DRsoft.Modeling.Metadata.Interfaces;
using DRsoft.Modeling.Metadata.Status;
using Newtonsoft.Json;

namespace DRsoft.Modeling.Metadata.Models.Config
{
    [Serializable]
    [XmlType("VisionCalibrationConfig")]
    [CacheProviderType(typeof(RuntimeCacheProvider<VisionCalibrationConfig>), typeof(DesignCacheProvider<VisionCalibrationConfig>), Directory = "VisionCalibrationConfig")]
    public class VisionCalibrationConfig : IModelKey, ICloneable<VisionCalibrationConfig>
    {
        [XmlElement("IpAddress")]
        [JsonProperty("IpAddress")]
        public string IpAddress { get; set; } = string.Empty;
        [XmlElement("Port")]
        [JsonProperty("Port")]
        public int Port { get; set; }
        [XmlElement("CameraParaVisionSend")]
        [JsonProperty("CameraParaVisionSend")]
        public CameraParaVisionSend CameraParaVisionSend { get; set; } = new CameraParaVisionSend();
        #region
        [XmlElement("Language")]
        [JsonProperty("Language")]
        public string Language { get; set; }
        [XmlAttribute("id")]
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [XmlAttribute("MetadataVersion")]
        [JsonProperty("MetadataVersion")]
        public string MetadataVersion { get; set; }
        [XmlAttribute("MetadataStatus")]
        [JsonProperty("MetadataStatus")]
        public MetaDataStatus MetadataStatus { get; set; }
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
        public VisionCalibrationConfig Clone()
        {
            var clone = new VisionCalibrationConfig();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        #endregion
        public void CopyTo(VisionCalibrationConfig dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.Id = this.Id;
            dest.MetadataVersion = this.MetadataVersion;
            dest.MetadataStatus = this.MetadataStatus;
            dest.Language = this.Language;
            dest.IpAddress = this.IpAddress;
            dest.Port = this.Port;
            dest.CameraParaVisionSend = CameraParaVisionSend.Clone<CameraParaVisionSend>();
        }
    }
    [Serializable]
    public class CameraParaVisionSend : ICloneable<CameraParaVisionSend>
    {
        [XmlElement("Midexposure")]
        [JsonProperty("Midexposure")]
        public float Midexposure { get; set; }
        [XmlElement("Aexposure")]
        [JsonProperty("Aexposure")]
        public float Aexposure { get; set; }
        [XmlElement("Bexposure")]
        [JsonProperty("Bexposure")]
        public float Bexposure { get; set; }
        [XmlElement("Cexposure")]
        [JsonProperty("Cexposure")]
        public float Cexposure { get; set; }
        [XmlElement("Dexposure")]
        [JsonProperty("Dexposure")]
        public float Dexposure { get; set; }
        [XmlElement("midgain")]
        [JsonProperty("midgain")]
        public float midgain { get; set; }
        [XmlElement("Again")]
        [JsonProperty("Again")]
        public float Again { get; set; }
        [XmlElement("Bgain")]
        [JsonProperty("Bgain")]
        public float Bgain { get; set; }
        [XmlElement("Cgain")]
        [JsonProperty("Cgain")]
        public float Cgain { get; set; }
        [XmlElement("Dgain")]
        [JsonProperty("Dgain")]
        public float Dgain { get; set; }
        [XmlElement("InCheck")]
        [JsonProperty("InCheck")]
        public int InCheck { get; set; }
        [XmlElement("EnableSaveNG")]
        [JsonProperty("EnableSaveNG")]
        public int EnableSaveNG { get; set; }
        [XmlElement("Mcheck")]
        [JsonProperty("Mcheck")]
        public int Mcheck { get; set; }
        [XmlElement("MarkType")]
        [JsonProperty("MarkType")]
        public int MarkType { get; set; }
        [XmlElement("EnableWork")]
        [JsonProperty("EnableWork")]
        public int EnableWork { get; set; }
        [XmlElement("MarkWith")]
        [JsonProperty("MarkWith")]
        public float MarkWith { get; set; }
        [XmlElement("MarkHeight")]
        [JsonProperty("MarkHeight")]
        public float MarkHeight { get; set; }
        [XmlElement("minAccBlob")]
        [JsonProperty("minAccBlob")]
        public int minAccBlob { get; set; }
        [XmlElement("minAccLine")]
        [JsonProperty("minAccLine")]
        public int minAccLine { get; set; }
        [XmlElement("Thre")]
        [JsonProperty("Thre")]
        public float Thre { get; set; }
        [XmlElement("roisize")]
        [JsonProperty("roisize")]
        public int roisize { get; set; }

        public CameraParaVisionSend()
        {

        }
        public CameraParaVisionSend Clone()
        {
            var clone = new CameraParaVisionSend();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

        public void CopyTo(CameraParaVisionSend dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.Midexposure = this.Midexposure;
            dest.Aexposure = this.Aexposure;
            dest.Bexposure = this.Bexposure;
            dest.Cexposure = this.Cexposure;
            dest.Dexposure = this.Dexposure;
            dest.midgain = this.midgain;
            dest.Again = this.Again;
            dest.Bgain = this.Bgain;
            dest.Cgain = this.Cgain;
            dest.Dgain = this.Dgain;
            dest.InCheck = this.InCheck;
            dest.EnableSaveNG = this.EnableSaveNG;
            dest.Mcheck = this.Mcheck;
            dest.MarkType = this.MarkType;
            dest.EnableWork = this.EnableWork;
            dest.MarkWith = this.MarkWith;
            dest.MarkHeight = this.MarkHeight;
            dest.minAccBlob = this.minAccBlob;
            dest.minAccLine = this.minAccLine;
            dest.Thre = this.Thre;
            dest.roisize = this.roisize;
        }
    }
}
