using System;
using System.Xml.Serialization;
using DRsoft.Modeling.Metadata.Caches;
using DRsoft.Modeling.Metadata.Interfaces;
using DRsoft.Modeling.Metadata.Status;
using Newtonsoft.Json;

namespace DRsoft.Modeling.Metadata.Models.Config
{
    [Serializable]
    [XmlType("EngineConfig")]
    [CacheProviderType(typeof(RuntimeCacheProvider<EngineConfig>), typeof(DesignCacheProvider<EngineConfig>), Directory = "EngineConfig")]
    public class EngineConfig : IModelKey, ICloneable<EngineConfig>
    {
        /// <summary>
        /// 开启某些日志记录
        /// </summary>
        [XmlElement("IsVerbose")]
        [JsonProperty("IsVerbose")]
        public bool IsVerbose { get; set; }

        /// <summary>
        /// 刷新频率(毫秒)
        /// </summary>
        [XmlElement("RefreshRate")]
        [JsonProperty("RefreshRate")]
        public int RefreshRate { get; set; } = 75;
        /// <summary>
        /// PC保存参数
        /// </summary>
        [XmlElement("PcParam")]
        [JsonProperty("PcParam")]
        public StPcParam PcParam { get; set; } = new StPcParam();

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
        public EngineConfig Clone()
        {
            var clone = new EngineConfig();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion
        public void CopyTo(EngineConfig dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.Id = this.Id;
            dest.MetadataVersion = this.MetadataVersion;
            dest.MetadataStatus = this.MetadataStatus;
            dest.Language = this.Language;
            dest.IsVerbose = this.IsVerbose;
            dest.RefreshRate = this.RefreshRate;
            dest.PcParam = this.PcParam.Clone<StPcParam>();
        }
    }
    [Serializable]
    public class StPcParam : ICloneable<StPcParam>
    {
        [XmlAttribute("ProductionType")]
        [JsonProperty("ProductionType")]
        public int ProductionType { get; set; }

        [XmlAttribute("MarkingPath")]
        [JsonProperty("MarkingPath")]
        public string? MarkingPath { get; set; }

        [XmlAttribute("MarkingNamePrefix")]
        [JsonProperty("MarkingNamePrefix")]
        public string? MarkingNamePrefix { get; set; }

        [XmlAttribute("LogOutTime")]
        [JsonProperty("LogOutTime")]
        public int LogOutTime { get; set; }

        [XmlAttribute("PowerMeterMeasurePos1X")]
        [JsonProperty("PowerMeterMeasurePos1X")]
        public double PowerMeterMeasurePos1X { get; set; }

        [XmlAttribute("PowerMeterMeasurePos1Y")]
        [JsonProperty("PowerMeterMeasurePos1Y")]
        public double PowerMeterMeasurePos1Y { get; set; }

        [XmlAttribute("PowerMeterMeasurePos2X")]
        [JsonProperty("PowerMeterMeasurePos2X")]
        public double PowerMeterMeasurePos2X { get; set; }

        [XmlAttribute("PowerMeterMeasurePos2Y")]
        [JsonProperty("PowerMeterMeasurePos2Y")]
        public double PowerMeterMeasurePos2Y { get; set; }

        [XmlAttribute("PowerMeterMeasurePos3X")]
        [JsonProperty("PowerMeterMeasurePos3X")]
        public double PowerMeterMeasurePos3X { get; set; }

        [XmlAttribute("PowerMeterMeasurePos3Y")]
        [JsonProperty("PowerMeterMeasurePos3Y")]
        public double PowerMeterMeasurePos3Y { get; set; }

        [XmlAttribute("PowerMeterMeasurePos4X")]
        [JsonProperty("PowerMeterMeasurePos4X")]
        public double PowerMeterMeasurePos4X { get; set; }

        [XmlAttribute("PowerMeterMeasurePos4Y")]
        [JsonProperty("PowerMeterMeasurePos4Y")]
        public double PowerMeterMeasurePos4Y { get; set; }

        [XmlAttribute("PowerMeterMeasurePos5X")]
        [JsonProperty("PowerMeterMeasurePos5X")]
        public double PowerMeterMeasurePos5X { get; set; }

        [XmlAttribute("PowerMeterMeasurePos5Y")]
        [JsonProperty("PowerMeterMeasurePos5Y")]
        public double PowerMeterMeasurePos5Y { get; set; }

        [XmlAttribute("PowerMeterMeasurePos6X")]
        [JsonProperty("PowerMeterMeasurePos6X")]
        public double PowerMeterMeasurePos6X { get; set; }

        [XmlAttribute("PowerMeterMeasurePos6Y")]
        [JsonProperty("PowerMeterMeasurePos6Y")]
        public double PowerMeterMeasurePos6Y { get; set; }

        [XmlAttribute("PowerMeterMeasurePos7X")]
        [JsonProperty("PowerMeterMeasurePos7X")]
        public double PowerMeterMeasurePos7X { get; set; }

        [XmlAttribute("PowerMeterMeasurePos7Y")]
        [JsonProperty("PowerMeterMeasurePos7Y")]
        public double PowerMeterMeasurePos7Y { get; set; }

        [XmlAttribute("PowerMeterMeasurePos8X")]
        [JsonProperty("PowerMeterMeasurePos8X")]
        public double PowerMeterMeasurePos8X { get; set; }

        [XmlAttribute("PowerMeterMeasurePos8Y")]
        [JsonProperty("PowerMeterMeasurePos8Y")]
        public double PowerMeterMeasurePos8Y { get; set; }

        [XmlAttribute("PowerMeterMeasurePos9X")]
        [JsonProperty("PowerMeterMeasurePos9X")]
        public double PowerMeterMeasurePos9X { get; set; }

        [XmlAttribute("PowerMeterMeasurePos9Y")]
        [JsonProperty("PowerMeterMeasurePos9Y")]
        public double PowerMeterMeasurePos9Y { get; set; }

        [XmlAttribute("PowerMeterMeasurePos10X")]
        [JsonProperty("PowerMeterMeasurePos10X")]
        public double PowerMeterMeasurePos10X { get; set; }

        [XmlAttribute("PowerMeterMeasurePos10Y")]
        [JsonProperty("PowerMeterMeasurePos10Y")]
        public double PowerMeterMeasurePos10Y { get; set; }

        [XmlAttribute("PowerMeterMeasurePos11X")]
        [JsonProperty("PowerMeterMeasurePos11X")]
        public double PowerMeterMeasurePos11X { get; set; }

        [XmlAttribute("PowerMeterMeasurePos11Y")]
        [JsonProperty("PowerMeterMeasurePos11Y")]
        public double PowerMeterMeasurePos11Y { get; set; }

        [XmlAttribute("PowerMeterMeasurePos12X")]
        [JsonProperty("PowerMeterMeasurePos12X")]
        public double PowerMeterMeasurePos12X { get; set; }

        [XmlAttribute("PowerMeterMeasurePos12Y")]
        [JsonProperty("PowerMeterMeasurePos12Y")]
        public double PowerMeterMeasurePos12Y { get; set; }

        [XmlAttribute("PowerMeterInterval")]
        [JsonProperty("PowerMeterInterval")]
        public int PowerMeterInterval { get; set; }

        [XmlAttribute("Laser1Power")]
        [JsonProperty("Laser1Power")]
        public double Laser1Power { get; set; }

        [XmlAttribute("Laser1Freq")]
        [JsonProperty("Laser1Freq")]
        public double Laser1Freq { get; set; }

        [XmlAttribute("Laser2Power")]
        [JsonProperty("Laser2Power")]
        public double Laser2Power { get; set; }

        [XmlAttribute("Laser2Freq")]
        [JsonProperty("Laser2Freq")]
        public double Laser2Freq { get; set; }

        [XmlAttribute("Laser3Power")]
        [JsonProperty("Laser3Power")]
        public double Laser3Power { get; set; }

        [XmlAttribute("Laser3Freq")]
        [JsonProperty("Laser3Freq")]
        public double Laser3Freq { get; set; }

        [XmlAttribute("Laser4Power")]
        [JsonProperty("Laser4Power")]
        public double Laser4Power { get; set; }

        [XmlAttribute("Laser4Freq")]
        [JsonProperty("Laser4Freq")]
        public double Laser4Freq { get; set; }

        [XmlAttribute("Laser5Power")]
        [JsonProperty("Laser5Power")]
        public double Laser5Power { get; set; }

        [XmlAttribute("Laser5Freq")]
        [JsonProperty("Laser5Freq")]
        public double Laser5Freq { get; set; }

        [XmlAttribute("Laser6Power")]
        [JsonProperty("Laser6Power")]
        public double Laser6Power { get; set; }

        [XmlAttribute("Laser6Freq")]
        [JsonProperty("Laser6Freq")]
        public double Laser6Freq { get; set; }

        [XmlAttribute("Laser7Power")]
        [JsonProperty("Laser7Power")]
        public double Laser7Power { get; set; }

        [XmlAttribute("Laser7Freq")]
        [JsonProperty("Laser7Freq")]
        public double Laser7Freq { get; set; }

        [XmlAttribute("Laser8Power")]
        [JsonProperty("Laser8Power")]
        public double Laser8Power { get; set; }

        [XmlAttribute("Laser8Freq")]
        [JsonProperty("Laser8Freq")]
        public double Laser8Freq { get; set; }

        [XmlAttribute("Laser9Power")]
        [JsonProperty("Laser9Power")]
        public double Laser9Power { get; set; }

        [XmlAttribute("Laser9Freq")]
        [JsonProperty("Laser9Freq")]
        public double Laser9Freq { get; set; }

        [XmlAttribute("Laser10Power")]
        [JsonProperty("Laser10Power")]
        public double Laser10Power { get; set; }

        [XmlAttribute("Laser10Freq")]
        [JsonProperty("Laser10Freq")]
        public double Laser10Freq { get; set; }

        [XmlAttribute("Laser11Power")]
        [JsonProperty("Laser11Power")]
        public double Laser11Power { get; set; }

        [XmlAttribute("Laser11Freq")]
        [JsonProperty("Laser11Freq")]
        public double Laser11Freq { get; set; }

        [XmlAttribute("Laser12Power")]
        [JsonProperty("Laser12Power")]
        public double Laser12Power { get; set; }

        [XmlAttribute("Laser12Freq")]
        [JsonProperty("Laser12Freq")]
        public double Laser12Freq { get; set; }

        [XmlAttribute("PowerMeterMeasureHl")]
        [JsonProperty("PowerMeterMeasureHl")]
        public double PowerMeterMeasureHl { get; set; }

        [XmlAttribute("PowerMeterMeasureLl")]
        [JsonProperty("PowerMeterMeasureLl")]
        public double PowerMeterMeasureLl { get; set; }

        [XmlAttribute("PowerMeterRatio")]
        [JsonProperty("PowerMeterRatio")]
        public double PowerMeterRatio { get; set; }

        [XmlAttribute("PowerMeterPercent")]
        [JsonProperty("PowerMeterPercent")]
        public double PowerMeterPercent { get; set; }

        [XmlAttribute("IsSilicaWashed")]
        [JsonProperty("IsSilicaWashed")]
        public bool IsSilicaWashed { get; set; }

        [XmlAttribute("IsDirtyPosMarked")]
        [JsonProperty("IsDirtyPosMarked")]
        public bool IsDirtyPosMarked { get; set; }

        [XmlAttribute("VibraOfs1X")]
        [JsonProperty("VibraOfs1X")]
        public double VibraOfs1X { get; set; }

        [XmlAttribute("VibraOfs1Y")]
        [JsonProperty("VibraOfs1Y")]
        public double VibraOfs1Y { get; set; }

        [XmlAttribute("VibraOfs1A")]
        [JsonProperty("VibraOfs1A")]
        public double VibraOfs1A { get; set; }

        [XmlAttribute("VibraOfs2X")]
        [JsonProperty("VibraOfs2X")]
        public double VibraOfs2X { get; set; }

        [XmlAttribute("VibraOfs2Y")]
        [JsonProperty("VibraOfs2Y")]
        public double VibraOfs2Y { get; set; }

        [XmlAttribute("VibraOfs2A")]
        [JsonProperty("VibraOfs2A")]
        public double VibraOfs2A { get; set; }

        [XmlAttribute("VibraOfs3X")]
        [JsonProperty("VibraOfs3X")]
        public double VibraOfs3X { get; set; }

        [XmlAttribute("VibraOfs3Y")]
        [JsonProperty("VibraOfs3Y")]
        public double VibraOfs3Y { get; set; }

        [XmlAttribute("VibraOfs3A")]
        [JsonProperty("VibraOfs3A")]
        public double VibraOfs3A { get; set; }

        [XmlAttribute("VibraOfs4X")]
        [JsonProperty("VibraOfs4X")]
        public double VibraOfs4X { get; set; }

        [XmlAttribute("VibraOfs4Y")]
        [JsonProperty("VibraOfs4Y")]
        public double VibraOfs4Y { get; set; }

        [XmlAttribute("VibraOfs4A")]
        [JsonProperty("VibraOfs4A")]
        public double VibraOfs4A { get; set; }

        [XmlAttribute("VibraOfs5X")]
        [JsonProperty("VibraOfs5X")]
        public double VibraOfs5X { get; set; }

        [XmlAttribute("VibraOfs5Y")]
        [JsonProperty("VibraOfs5Y")]
        public double VibraOfs5Y { get; set; }

        [XmlAttribute("VibraOfs5A")]
        [JsonProperty("VibraOfs5A")]
        public double VibraOfs5A { get; set; }

        [XmlAttribute("VibraOfs6X")]
        [JsonProperty("VibraOfs6X")]
        public double VibraOfs6X { get; set; }

        [XmlAttribute("VibraOfs6Y")]
        [JsonProperty("VibraOfs6Y")]
        public double VibraOfs6Y { get; set; }

        [XmlAttribute("VibraOfs6A")]
        [JsonProperty("VibraOfs6A")]
        public double VibraOfs6A { get; set; }

        [XmlAttribute("VibraOfs7X")]
        [JsonProperty("VibraOfs7X")]
        public double VibraOfs7X { get; set; }

        [XmlAttribute("VibraOfs7Y")]
        [JsonProperty("VibraOfs7Y")]
        public double VibraOfs7Y { get; set; }

        [XmlAttribute("VibraOfs7A")]
        [JsonProperty("VibraOfs7A")]
        public double VibraOfs7A { get; set; }

        [XmlAttribute("VibraOfs8X")]
        [JsonProperty("VibraOfs8X")]
        public double VibraOfs8X { get; set; }

        [XmlAttribute("VibraOfs8Y")]
        [JsonProperty("VibraOfs8Y")]
        public double VibraOfs8Y { get; set; }

        [XmlAttribute("VibraOfs8A")]
        [JsonProperty("VibraOfs8A")]
        public double VibraOfs8A { get; set; }

        [XmlAttribute("VibraOfs9X")]
        [JsonProperty("VibraOfs9X")]
        public double VibraOfs9X { get; set; }

        [XmlAttribute("VibraOfs9Y")]
        [JsonProperty("VibraOfs9Y")]
        public double VibraOfs9Y { get; set; }

        [XmlAttribute("VibraOfs9A")]
        [JsonProperty("VibraOfs9A")]
        public double VibraOfs9A { get; set; }

        [XmlAttribute("VibraOfs10X")]
        [JsonProperty("VibraOfs10X")]
        public double VibraOfs10X { get; set; }

        [XmlAttribute("VibraOfs10Y")]
        [JsonProperty("VibraOfs10Y")]
        public double VibraOfs10Y { get; set; }

        [XmlAttribute("VibraOfs10A")]
        [JsonProperty("VibraOfs10A")]
        public double VibraOfs10A { get; set; }

        [XmlAttribute("VibraOfs11X")]
        [JsonProperty("VibraOfs11X")]
        public double VibraOfs11X { get; set; }

        [XmlAttribute("VibraOfs11Y")]
        [JsonProperty("VibraOfs11Y")]
        public double VibraOfs11Y { get; set; }

        [XmlAttribute("VibraOfs11A")]
        [JsonProperty("VibraOfs11A")]
        public double VibraOfs11A { get; set; }

        [XmlAttribute("VibraOfs12X")]
        [JsonProperty("VibraOfs12X")]
        public double VibraOfs12X { get; set; }

        [XmlAttribute("VibraOfs12Y")]
        [JsonProperty("VibraOfs12Y")]
        public double VibraOfs12Y { get; set; }

        [XmlAttribute("VibraOfs12A")]
        [JsonProperty("VibraOfs12A")]
        public double VibraOfs12A { get; set; }

        [XmlAttribute("CameraShootFailThresX")]
        [JsonProperty("CameraShootFailThresX")]
        public double CameraShootFailThresX { get; set; }

        [XmlAttribute("CameraShootFailThresY")]
        [JsonProperty("CameraShootFailThresY")]
        public double CameraShootFailThresY { get; set; }

        [XmlAttribute("CameraShootFailThresA")]
        [JsonProperty("CameraShootFailThresA")]
        public double CameraShootFailThresA { get; set; }
        public StPcParam Clone()
        {
            var clone = new StPcParam();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        public void CopyTo(StPcParam dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.ProductionType = ProductionType;
            dest.MarkingPath = MarkingPath;
            dest.MarkingNamePrefix = MarkingNamePrefix;
            dest.LogOutTime = LogOutTime;
            dest.PowerMeterMeasurePos1X = PowerMeterMeasurePos1X;
            dest.PowerMeterMeasurePos1Y = PowerMeterMeasurePos1Y;
            dest.PowerMeterMeasurePos2X = PowerMeterMeasurePos2X;
            dest.PowerMeterMeasurePos2Y = PowerMeterMeasurePos2Y;
            dest.PowerMeterMeasurePos3X = PowerMeterMeasurePos3X;
            dest.PowerMeterMeasurePos3Y = PowerMeterMeasurePos3Y;
            dest.PowerMeterMeasurePos4X = PowerMeterMeasurePos4X;
            dest.PowerMeterMeasurePos4Y = PowerMeterMeasurePos4Y;
            dest.PowerMeterMeasurePos5X = PowerMeterMeasurePos5X;
            dest.PowerMeterMeasurePos5Y = PowerMeterMeasurePos5Y;
            dest.PowerMeterMeasurePos6X = PowerMeterMeasurePos6X;
            dest.PowerMeterMeasurePos6Y = PowerMeterMeasurePos6Y;
            dest.PowerMeterMeasurePos7X = PowerMeterMeasurePos7X;
            dest.PowerMeterMeasurePos7Y = PowerMeterMeasurePos7Y;
            dest.PowerMeterMeasurePos8X = PowerMeterMeasurePos8X;
            dest.PowerMeterMeasurePos8Y = PowerMeterMeasurePos8Y;
            dest.PowerMeterMeasurePos9X = PowerMeterMeasurePos9X;
            dest.PowerMeterMeasurePos9Y = PowerMeterMeasurePos9Y;
            dest.PowerMeterMeasurePos10X = PowerMeterMeasurePos10X;
            dest.PowerMeterMeasurePos10Y = PowerMeterMeasurePos10Y;
            dest.PowerMeterMeasurePos11X = PowerMeterMeasurePos11X;
            dest.PowerMeterMeasurePos11Y = PowerMeterMeasurePos11Y;
            dest.PowerMeterMeasurePos12X = PowerMeterMeasurePos12X;
            dest.PowerMeterMeasurePos12Y = PowerMeterMeasurePos12Y;
            dest.PowerMeterInterval = PowerMeterInterval;
            dest.Laser1Power = Laser1Power;
            dest.Laser1Freq = Laser1Freq;
            dest.Laser2Power = Laser2Power;
            dest.Laser2Freq = Laser2Freq;
            dest.Laser3Power = Laser3Power;
            dest.Laser3Freq = Laser3Freq;
            dest.Laser4Power = Laser4Power;
            dest.Laser4Freq = Laser4Freq;
            dest.Laser5Power = Laser5Power;
            dest.Laser5Freq = Laser5Freq;
            dest.Laser6Power = Laser6Power;
            dest.Laser6Freq = Laser6Freq;
            dest.Laser7Power = Laser7Power;
            dest.Laser7Freq = Laser7Freq;
            dest.Laser8Power = Laser8Power;
            dest.Laser8Freq = Laser8Freq;
            dest.Laser9Power = Laser9Power;
            dest.Laser9Freq = Laser9Freq;
            dest.Laser10Power = Laser10Power;
            dest.Laser10Freq = Laser10Freq;
            dest.Laser11Power = Laser11Power;
            dest.Laser11Freq = Laser11Freq;
            dest.Laser12Power = Laser12Power;
            dest.Laser12Freq = Laser12Freq;
            dest.PowerMeterMeasureHl = PowerMeterMeasureHl;
            dest.PowerMeterMeasureLl = PowerMeterMeasureLl;
            dest.PowerMeterRatio = PowerMeterRatio;
            dest.PowerMeterPercent = PowerMeterPercent;
            dest.IsSilicaWashed = IsSilicaWashed;
            dest.IsDirtyPosMarked = IsDirtyPosMarked;
            dest.VibraOfs1X = VibraOfs1X;
            dest.VibraOfs1Y = VibraOfs1Y;
            dest.VibraOfs1A = VibraOfs1A;
            dest.VibraOfs2X = VibraOfs2X;
            dest.VibraOfs2Y = VibraOfs2Y;
            dest.VibraOfs2A = VibraOfs2A;
            dest.VibraOfs3X = VibraOfs3X;
            dest.VibraOfs3Y = VibraOfs3Y;
            dest.VibraOfs3A = VibraOfs3A;
            dest.VibraOfs4X = VibraOfs4X;
            dest.VibraOfs4Y = VibraOfs4Y;
            dest.VibraOfs4A = VibraOfs4A;
            dest.VibraOfs5X = VibraOfs5X;
            dest.VibraOfs5Y = VibraOfs5Y;
            dest.VibraOfs5A = VibraOfs5A;
            dest.VibraOfs6X = VibraOfs6X;
            dest.VibraOfs6Y = VibraOfs6Y;
            dest.VibraOfs6A = VibraOfs6A;
            dest.VibraOfs7X = VibraOfs7X;
            dest.VibraOfs7Y = VibraOfs7Y;
            dest.VibraOfs7A = VibraOfs7A;
            dest.VibraOfs8X = VibraOfs8X;
            dest.VibraOfs8Y = VibraOfs8Y;
            dest.VibraOfs8A = VibraOfs8A;
            dest.VibraOfs9X = VibraOfs9X;
            dest.VibraOfs9Y = VibraOfs9Y;
            dest.VibraOfs9A = VibraOfs9A;
            dest.VibraOfs10X = VibraOfs10X;
            dest.VibraOfs10Y = VibraOfs10Y;
            dest.VibraOfs10A = VibraOfs10A;
            dest.VibraOfs11X = VibraOfs11X;
            dest.VibraOfs11Y = VibraOfs11Y;
            dest.VibraOfs11A = VibraOfs11A;
            dest.VibraOfs12X = VibraOfs12X;
            dest.VibraOfs12Y = VibraOfs12Y;
            dest.VibraOfs12A = VibraOfs12A;
            dest.CameraShootFailThresX = CameraShootFailThresX;
            dest.CameraShootFailThresY = CameraShootFailThresY;
            dest.CameraShootFailThresA = CameraShootFailThresA;
        }
    }
}
