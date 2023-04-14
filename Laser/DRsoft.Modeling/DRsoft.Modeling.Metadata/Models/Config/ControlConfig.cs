using System;
using System.Xml.Serialization;
using DRsoft.Modeling.Metadata.Caches;
using DRsoft.Modeling.Metadata.Interfaces;
using DRsoft.Modeling.Metadata.Status;
using Newtonsoft.Json;

namespace DRsoft.Modeling.Metadata.Models.Config
{
    [Serializable]
    [XmlType("ControllerConfig")]
    [CacheProviderType(typeof(RuntimeCacheProvider<ControlConfig>), typeof(DesignCacheProvider<ControlConfig>), Directory = "ControllerConfig")]

    public class ControlConfig : IModelKey, ICloneable<ControlConfig>
    {
        /// <summary>
        /// 控制器参数
        /// </summary>
        [XmlElement("ControllerParam")]
        [JsonProperty("ControllerParam")]
        public ST_Param ControllerParam { get; set; }
        [XmlElement("ParaAxisGantry11")]
        [JsonProperty("ParaAxisGantry11")]
        public StAxisParameter ParaAxisGantry11 { get; set; }
        [XmlElement("ParaAxisGantry12")]
        [JsonProperty("ParaAxisGantry12")]
        public StAxisParameter ParaAxisGantry12 { get; set; }
        [XmlElement("ParaAxisGantry21")]
        [JsonProperty("ParaAxisGantry21")]
        public StAxisParameter ParaAxisGantry21 { get; set; }
        [XmlElement("ParaAxisGantry22")]
        [JsonProperty("ParaAxisGantry22")]
        public StAxisParameter ParaAxisGantry22 { get; set; }
        [XmlElement("ParaAxisAlign11")]
        [JsonProperty("ParaAxisAlign11")]
        public StAxisParameter ParaAxisAlign11 { get; set; }
        [XmlElement("ParaAxisAlign12")]
        [JsonProperty("ParaAxisAlign12")]
        public StAxisParameter ParaAxisAlign12 { get; set; }
        [XmlElement("ParaAxisAlign21")]
        [JsonProperty("ParaAxisAlign21")]
        public StAxisParameter ParaAxisAlign21 { get; set; }
        [XmlElement("ParaAxisAlign22")]
        [JsonProperty("ParaAxisAlign22")]
        public StAxisParameter ParaAxisAlign22 { get; set; }
        [XmlElement("ParaAxisCamShutter1")]
        [JsonProperty("ParaAxisCamShutter1")]
        public StAxisParameter ParaAxisCamShutter1 { get; set; }
        [XmlElement("ParaAxisCamShutter2")]
        [JsonProperty("ParaAxisCamShutter2")]
        public StAxisParameter ParaAxisCamShutter2 { get; set; }
        [XmlElement("ParaAxisZ1")]
        [JsonProperty("ParaAxisZ1")]
        public StAxisParameter ParaAxisZ1 { get; set; }
        [XmlElement("ParaAxisZ2")]
        [JsonProperty("ParaAxisZ2")]
        public StAxisParameter ParaAxisZ2 { get; set; }
        [XmlElement("ParaAxisUwLift")]
        [JsonProperty("ParaAxisUwLift")]
        public StAxisParameter ParaAxisUwLift { get; set; }
        [XmlElement("ParaAxisUw")]
        [JsonProperty("ParaAxisUw")]
        public StAxisParameter ParaAxisUw { get; set; }
        [XmlElement("ParaAxisRwLift")]
        [JsonProperty("ParaAxisRwLift")]
        public StAxisParameter ParaAxisRwLift { get; set; }
        [XmlElement("ParaAxisRw")]
        [JsonProperty("ParaAxisRw")]
        public StAxisParameter ParaAxisRw { get; set; }
        [XmlElement("ParaAxisClean")]
        [JsonProperty("ParaAxisClean")]
        public StAxisParameter ParaAxisClean { get; set; }
        [XmlElement("ParaAxisPowerMeter")]
        [JsonProperty("ParaAxisPowerMeter")]
        public StAxisParameter ParaAxisPowerMeter { get; set; }
        [XmlElement("ParaAxisUwSteer")]
        [JsonProperty("ParaAxisUwSteer")]
        public StAxisParameter ParaAxisUwSteer { get; set; }
        [XmlElement("ParaAxisPeeling1")]
        [JsonProperty("ParaAxisPeeling1")]
        public StAxisParameter ParaAxisPeeling1 { get; set; }
        [XmlElement("ParaAxisStationABelt")]
        [JsonProperty("ParaAxisStationABelt")]
        public StAxisParameter ParaAxisStationABelt { get; set; }
        [XmlElement("ParaAxisPeeling2")]
        [JsonProperty("ParaAxisPeeling2")]
        public StAxisParameter ParaAxisPeeling2 { get; set; }
        [XmlElement("ParaAxisStationBBelt")]
        [JsonProperty("ParaAxisStationBBelt")]
        public StAxisParameter ParaAxisStationBBelt { get; set; }
        [XmlElement("ParaAxisRwSteer")]
        [JsonProperty("ParaAxisRwSteer")]
        public StAxisParameter ParaAxisRwSteer { get; set; }

        public ControlConfig()
        {
            ControllerParam = new ST_Param();
            ParaAxisGantry11 = new StAxisParameter();
            ParaAxisGantry12 = new StAxisParameter();
            ParaAxisGantry21 = new StAxisParameter();
            ParaAxisGantry22 = new StAxisParameter();
            ParaAxisAlign11 = new StAxisParameter();
            ParaAxisAlign12 = new StAxisParameter();
            ParaAxisAlign21 = new StAxisParameter();
            ParaAxisAlign22 = new StAxisParameter();
            ParaAxisCamShutter1 = new StAxisParameter();
            ParaAxisCamShutter2 = new StAxisParameter();
            ParaAxisZ1 = new StAxisParameter();
            ParaAxisZ2 = new StAxisParameter();
            ParaAxisUwLift = new StAxisParameter();
            ParaAxisUw = new StAxisParameter();
            ParaAxisRwLift = new StAxisParameter();
            ParaAxisRw = new StAxisParameter();
            ParaAxisClean = new StAxisParameter();
            ParaAxisPowerMeter = new StAxisParameter();
            ParaAxisUwSteer = new StAxisParameter();
            ParaAxisPeeling1 = new StAxisParameter();
            ParaAxisStationABelt = new StAxisParameter();
            ParaAxisPeeling2 = new StAxisParameter();
            ParaAxisStationBBelt = new StAxisParameter();
            ParaAxisRwSteer = new StAxisParameter();
        }
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
        public ControlConfig Clone()
        {
            var clone = new ControlConfig();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion
        public void CopyTo(ControlConfig dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.Id = this.Id;
            dest.MetadataVersion = this.MetadataVersion;
            dest.MetadataStatus = this.MetadataStatus;
            dest.Language = this.Language;
            dest.CreatedBy = this.CreatedBy;
            dest.CreatedOn = this.CreatedOn;
            dest.ModifiedBy = this.ModifiedBy;
            dest.ModifiedOn = this.ModifiedOn;
            dest.ControllerParam = this.ControllerParam.Clone();
            dest.ParaAxisGantry11 = this.ParaAxisGantry11.Clone();
            dest.ParaAxisGantry12 = this.ParaAxisGantry12.Clone();
            dest.ParaAxisGantry21 = this.ParaAxisGantry21.Clone();
            dest.ParaAxisGantry22 = this.ParaAxisGantry22.Clone();
            dest.ParaAxisAlign11 = this.ParaAxisAlign11.Clone();
            dest.ParaAxisAlign12 = this.ParaAxisAlign12.Clone();
            dest.ParaAxisAlign21 = this.ParaAxisAlign21.Clone();
            dest.ParaAxisAlign22 = this.ParaAxisAlign22.Clone();
            dest.ParaAxisCamShutter1 = this.ParaAxisCamShutter1.Clone();
            dest.ParaAxisCamShutter2 = this.ParaAxisCamShutter2.Clone();
            dest.ParaAxisZ1 = this.ParaAxisZ1.Clone();
            dest.ParaAxisZ2 = this.ParaAxisZ2.Clone();
            dest.ParaAxisUwLift = this.ParaAxisUwLift.Clone();
            dest.ParaAxisUw = this.ParaAxisUw.Clone();
            dest.ParaAxisRwLift = this.ParaAxisRwLift.Clone();
            dest.ParaAxisRw = this.ParaAxisRw.Clone();
            dest.ParaAxisClean = this.ParaAxisClean.Clone();
            dest.ParaAxisPowerMeter = this.ParaAxisPowerMeter.Clone();
            dest.ParaAxisUwSteer = this.ParaAxisUwSteer.Clone();
            dest.ParaAxisPeeling1 = this.ParaAxisPeeling1.Clone();
            dest.ParaAxisStationABelt = this.ParaAxisStationABelt.Clone();
            dest.ParaAxisPeeling2 = this.ParaAxisPeeling2.Clone();
            dest.ParaAxisStationBBelt = this.ParaAxisStationBBelt.Clone();
            dest.ParaAxisRwSteer = this.ParaAxisRwSteer.Clone();
        }
    }

    [Serializable]
    public class StAxisParameter : ICloneable<StAxisParameter>
    {
        /// <summary>
        /// 回零偏移
        /// </summary>
        [XmlAttribute("HomeOffset")]
        [JsonProperty("HomeOffset")]
        public float HomeOffset { get; set; }

        /// <summary>
        /// 相对距离
        /// </summary>
        [XmlAttribute("RelDistance")]
        [JsonProperty("RelDistance")]
        public float RelDistance { get; set; }

        /// <summary>
        /// 绝对位置1
        /// </summary>
        [XmlAttribute("AbsPosition1")]
        [JsonProperty("AbsPosition1")]
        public float AbsPosition1 { get; set; }

        /// <summary>
        /// 绝对位置2
        /// </summary>
        [XmlAttribute("AbsPosition2")]
        [JsonProperty("AbsPosition2")]
        public float AbsPosition2 { get; set; }

        /// <summary>
        /// 回零速度
        /// </summary>
        [XmlAttribute("HomeVelo")]
        [JsonProperty("HomeVelo")]
        public float HomeVelo { get; set; }

        /// <summary>
        /// 手动速度
        /// </summary>
        [XmlAttribute("ManualVelo")]
        [JsonProperty("ManualVelo")]
        public float ManualVelo { get; set; }

        /// <summary>
        /// 工作速度
        /// </summary>
        [XmlAttribute("WorkVelo")]
        [JsonProperty("WorkVelo")]
        public float WorkVelo { get; set; }

        /// <summary>
        /// 加速度
        /// </summary>
        [XmlAttribute("Acc")]
        [JsonProperty("Acc")]
        public float Acc { get; set; }

        /// <summary>
        /// 减速度
        /// </summary>
        [XmlAttribute("Dec")]
        [JsonProperty("Dec")]
        public float Dec { get; set; }

        public StAxisParameter Clone()
        {
            var clone = new StAxisParameter();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        public void CopyTo(StAxisParameter dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.HomeOffset = HomeOffset;
            dest.RelDistance = RelDistance;
            dest.AbsPosition1 = AbsPosition1;
            dest.AbsPosition2 = AbsPosition2;
            dest.HomeVelo = HomeVelo;
            dest.ManualVelo = ManualVelo;
            dest.WorkVelo = WorkVelo;
            dest.Acc = Acc;
            dest.Dec = Dec;
        }
    }

    [Serializable]
    public class ST_Param : ICloneable<ST_Param>
    {
        [XmlAttribute("Z11_RulerPos")]
        [JsonProperty("Z11_RulerPos")]
        public float Z11_RulerPos { get; set; }
        [XmlAttribute("Z12_RulerPos")]
        [JsonProperty("Z12_RulerPos")]
        public float Z12_RulerPos { get; set; }
        [XmlAttribute("Gantry1WaitPos")]
        [JsonProperty("Gantry1WaitPos")]
        public float Gantry1WaitPos { get; set; }
        [XmlAttribute("Gantry1StationAGrabPos")]
        [JsonProperty("Gantry1StationAGrabPos")]
        public float Gantry1StationAGrabPos { get; set; }
        [XmlAttribute("Gantry2StationAGrabPos")]
        [JsonProperty("Gantry2StationAGrabPos")]
        public float Gantry2StationAGrabPos { get; set; }
        [XmlAttribute("Gantry1StationAMark1Pos")]
        [JsonProperty("Gantry1StationAMark1Pos")]
        public float Gantry1StationAMark1Pos { get; set; }
        [XmlAttribute("Gantry1StationAMark2Pos")]
        [JsonProperty("Gantry1StationAMark2Pos")]
        public float Gantry1StationAMark2Pos { get; set; }
        [XmlAttribute("Gantry1StationAMark3Pos")]
        [JsonProperty("Gantry1StationAMark3Pos")]
        public float Gantry1StationAMark3Pos { get; set; }
        [XmlAttribute("Gantry1StationAMark4Pos")]
        [JsonProperty("Gantry1StationAMark4Pos")]
        public float Gantry1StationAMark4Pos { get; set; }
        [XmlAttribute("Gantry1StationAMark5Pos")]
        [JsonProperty("Gantry1StationAMark5Pos")]
        public float Gantry1StationAMark5Pos { get; set; }
        [XmlAttribute("Gantry1StationAMark6Pos")]
        [JsonProperty("Gantry1StationAMark6Pos")]
        public float Gantry1StationAMark6Pos { get; set; }
        [XmlAttribute("Gantry1StationAMark7Pos")]
        [JsonProperty("Gantry1StationAMark7Pos")]
        public float Gantry1StationAMark7Pos { get; set; }
        [XmlAttribute("Gantry1StationAMark8Pos")]
        [JsonProperty("Gantry1StationAMark8Pos")]
        public float Gantry1StationAMark8Pos { get; set; }
        [XmlAttribute("Gantry2StationAMark1Pos")]
        [JsonProperty("Gantry2StationAMark1Pos")]
        public float Gantry2StationAMark1Pos { get; set; }
        [XmlAttribute("Gantry2StationAMark2Pos")]
        [JsonProperty("Gantry2StationAMark2Pos")]
        public float Gantry2StationAMark2Pos { get; set; }
        [XmlAttribute("Gantry2StationAMark3Pos")]
        [JsonProperty("Gantry2StationAMark3Pos")]
        public float Gantry2StationAMark3Pos { get; set; }
        [XmlAttribute("Gantry2StationAMark4Pos")]
        [JsonProperty("Gantry2StationAMark4Pos")]
        public float Gantry2StationAMark4Pos { get; set; }
        [XmlAttribute("Gantry2StationAMark5Pos")]
        [JsonProperty("Gantry2StationAMark5Pos")]
        public float Gantry2StationAMark5Pos { get; set; }
        [XmlAttribute("Gantry2StationAMark6Pos")]
        [JsonProperty("Gantry2StationAMark6Pos")]
        public float Gantry2StationAMark6Pos { get; set; }
        [XmlAttribute("Gantry2StationAMark7Pos")]
        [JsonProperty("Gantry2StationAMark7Pos")]
        public float Gantry2StationAMark7Pos { get; set; }
        [XmlAttribute("Gantry2StationAMark8Pos")]
        [JsonProperty("Gantry2StationAMark8Pos")]
        public float Gantry2StationAMark8Pos { get; set; }
        [XmlAttribute("Peeling1StartPos")]
        [JsonProperty("Peeling1StartPos")]
        public float Peeling1StartPos { get; set; }
        [XmlAttribute("Peeling1EndPos")]
        [JsonProperty("Peeling1EndPos")]
        public float Peeling1EndPos { get; set; }
        [XmlAttribute("Z1_DownPos")]
        [JsonProperty("Z1_DownPos")]
        public float Z1_DownPos { get; set; }
        [XmlAttribute("Z1_UpPos")]
        [JsonProperty("Z1_UpPos")]
        public float Z1_UpPos { get; set; }
        [XmlAttribute("Z21_RulerPos")]
        [JsonProperty("Z21_RulerPos")]
        public float Z21_RulerPos { get; set; }
        [XmlAttribute("Z22_RulerPos")]
        [JsonProperty("Z22_RulerPos")]
        public float Z22_RulerPos { get; set; }
        [XmlAttribute("Gantry2WaitPos")]
        [JsonProperty("Gantry2WaitPos")]
        public float Gantry2WaitPos { get; set; }
        [XmlAttribute("Gantry1StationBGrabPos")]
        [JsonProperty("Gantry1StationBGrabPos")]
        public float Gantry1StationBGrabPos { get; set; }
        [XmlAttribute("Gantry2StationBGrabPos")]
        [JsonProperty("Gantry2StationBGrabPos")]
        public float Gantry2StationBGrabPos { get; set; }
        [XmlAttribute("Gantry1StationBMark1Pos")]
        [JsonProperty("Gantry1StationBMark1Pos")]
        public float Gantry1StationBMark1Pos { get; set; }
        [XmlAttribute("Gantry1StationBMark2Pos")]
        [JsonProperty("Gantry1StationBMark2Pos")]
        public float Gantry1StationBMark2Pos { get; set; }
        [XmlAttribute("Gantry1StationBMark3Pos")]
        [JsonProperty("Gantry1StationBMark3Pos")]
        public float Gantry1StationBMark3Pos { get; set; }
        [XmlAttribute("Gantry1StationBMark4Pos")]
        [JsonProperty("Gantry1StationBMark4Pos")]
        public float Gantry1StationBMark4Pos { get; set; }
        [XmlAttribute("Gantry1StationBMark5Pos")]
        [JsonProperty("Gantry1StationBMark5Pos")]
        public float Gantry1StationBMark5Pos { get; set; }
        [XmlAttribute("Gantry1StationBMark6Pos")]
        [JsonProperty("Gantry1StationBMark6Pos")]
        public float Gantry1StationBMark6Pos { get; set; }
        [XmlAttribute("Gantry1StationBMark7Pos")]
        [JsonProperty("Gantry1StationBMark7Pos")]
        public float Gantry1StationBMark7Pos { get; set; }
        [XmlAttribute("Gantry1StationBMark8Pos")]
        [JsonProperty("Gantry1StationBMark8Pos")]
        public float Gantry1StationBMark8Pos { get; set; }
        [XmlAttribute("Gantry2StationBMark1Pos")]
        [JsonProperty("Gantry2StationBMark1Pos")]
        public float Gantry2StationBMark1Pos { get; set; }
        [XmlAttribute("Gantry2StationBMark2Pos")]
        [JsonProperty("Gantry2StationBMark2Pos")]
        public float Gantry2StationBMark2Pos { get; set; }
        [XmlAttribute("Gantry2StationBMark3Pos")]
        [JsonProperty("Gantry2StationBMark3Pos")]
        public float Gantry2StationBMark3Pos { get; set; }
        [XmlAttribute("Gantry2StationBMark4Pos")]
        [JsonProperty("Gantry2StationBMark4Pos")]
        public float Gantry2StationBMark4Pos { get; set; }
        [XmlAttribute("Gantry2StationBMark5Pos")]
        [JsonProperty("Gantry2StationBMark5Pos")]
        public float Gantry2StationBMark5Pos { get; set; }
        [XmlAttribute("Gantry2StationBMark6Pos")]
        [JsonProperty("Gantry2StationBMark6Pos")]
        public float Gantry2StationBMark6Pos { get; set; }
        [XmlAttribute("Gantry2StationBMark7Pos")]
        [JsonProperty("Gantry2StationBMark7Pos")]
        public float Gantry2StationBMark7Pos { get; set; }
        [XmlAttribute("Gantry2StationBMark8Pos")]
        [JsonProperty("Gantry2StationBMark8Pos")]
        public float Gantry2StationBMark8Pos { get; set; }
        [XmlAttribute("Peeling2StartPos")]
        [JsonProperty("Peeling2StartPos")]
        public float Peeling2StartPos { get; set; }
        [XmlAttribute("Peeling2EndPos")]
        [JsonProperty("Peeling2EndPos")]
        public float Peeling2EndPos { get; set; }
        [XmlAttribute("Z2_DownPos")]
        [JsonProperty("Z2_DownPos")]
        public float Z2_DownPos { get; set; }
        [XmlAttribute("Z2_UpPos")]
        [JsonProperty("Z2_UpPos")]
        public float Z2_UpPos { get; set; }
        [XmlAttribute("CamShutter1Pos0")]
        [JsonProperty("CamShutter1Pos0")]
        public float CamShutter1Pos0 { get; set; }
        [XmlAttribute("CamShutter1Pos1")]
        [JsonProperty("CamShutter1Pos1")]
        public float CamShutter1Pos1 { get; set; }
        [XmlAttribute("CamShutter1Pos2")]
        [JsonProperty("CamShutter1Pos2")]
        public float CamShutter1Pos2 { get; set; }
        [XmlAttribute("CamShutter1Pos3")]
        [JsonProperty("CamShutter1Pos3")]
        public float CamShutter1Pos3 { get; set; }
        [XmlAttribute("CamShutter2Pos0")]
        [JsonProperty("CamShutter2Pos0")]
        public float CamShutter2Pos0 { get; set; }
        [XmlAttribute("CamShutter2Pos1")]
        [JsonProperty("CamShutter2Pos1")]
        public float CamShutter2Pos1 { get; set; }
        [XmlAttribute("CamShutter2Pos2")]
        [JsonProperty("CamShutter2Pos2")]
        public float CamShutter2Pos2 { get; set; }
        [XmlAttribute("CamShutter2Pos3")]
        [JsonProperty("CamShutter2Pos3")]
        public float CamShutter2Pos3 { get; set; }
        [XmlAttribute("UwLiftUpPos")]
        [JsonProperty("UwLiftUpPos")]
        public float UwLiftUpPos { get; set; }
        [XmlAttribute("RwLiftUpPos")]
        [JsonProperty("RwLiftUpPos")]
        public float RwLiftUpPos { get; set; }
        [XmlAttribute("ProcessTimes")]
        [JsonProperty("ProcessTimes")]
        public float ProcessTimes { get; set; }
        [XmlAttribute("GrabTimeOutSet")]
        [JsonProperty("GrabTimeOutSet")]
        public float GrabTimeOutSet { get; set; }
        [XmlAttribute("StationA_VacOkDelay")]
        [JsonProperty("StationA_VacOkDelay")]
        public float StationA_VacOkDelay { get; set; }
        [XmlAttribute("StationB_VacOkDelay")]
        [JsonProperty("StationB_VacOkDelay")]
        public float StationB_VacOkDelay { get; set; }
        [XmlAttribute("StationA_BlowDelay")]
        [JsonProperty("StationA_BlowDelay")]
        public float StationA_BlowDelay { get; set; }
        [XmlAttribute("StationB_BlowDelay")]
        [JsonProperty("StationB_BlowDelay")]
        public float StationB_BlowDelay { get; set; }
        [XmlAttribute("AutoLeaserMeasureNum")]
        [JsonProperty("AutoLeaserMeasureNum")]
        public float AutoLeaserMeasureNum { get; set; }
        [XmlAttribute("Gantry1PowerMeterPos")]
        [JsonProperty("Gantry1PowerMeterPos")]
        public float Gantry1PowerMeterPos { get; set; }
        [XmlAttribute("Gantry2PowerMeterPos")]
        [JsonProperty("Gantry2PowerMeterPos")]
        public float Gantry2PowerMeterPos { get; set; }
        [XmlAttribute("LeftOffset")]
        [JsonProperty("LeftOffset")]
        public float LeftOffset { get; set; }
        [XmlAttribute("MidOffset")]
        [JsonProperty("MidOffset")]
        public float MidOffset { get; set; }
        [XmlAttribute("RightOffset")]
        [JsonProperty("RightOffset")]
        public float RightOffset { get; set; }
        [XmlAttribute("PowerMeterMeasurePos1")]
        [JsonProperty("PowerMeterMeasurePos1")]
        public float PowerMeterMeasurePos1 { get; set; }
        [XmlAttribute("PowerMeterMeasurePos2")]
        [JsonProperty("PowerMeterMeasurePos2")]
        public float PowerMeterMeasurePos2 { get; set; }
        [XmlAttribute("PowerMeterMeasurePos3")]
        [JsonProperty("PowerMeterMeasurePos3")]
        public float PowerMeterMeasurePos3 { get; set; }
        [XmlAttribute("PowerMeterMeasurePos4")]
        [JsonProperty("PowerMeterMeasurePos4")]
        public float PowerMeterMeasurePos4 { get; set; }
        [XmlAttribute("PowerMeterMeasurePos5")]
        [JsonProperty("PowerMeterMeasurePos5")]
        public float PowerMeterMeasurePos5 { get; set; }
        [XmlAttribute("PowerMeterMeasurePos6")]
        [JsonProperty("PowerMeterMeasurePos6")]
        public float PowerMeterMeasurePos6 { get; set; }
        [XmlAttribute("UwTorqueSet")]
        [JsonProperty("UwTorqueSet")]
        public float UwTorqueSet { get; set; }
        [XmlAttribute("RwTorqueSet")]
        [JsonProperty("RwTorqueSet")]
        public float RwTorqueSet { get; set; }
        [XmlAttribute("TapeLength")]
        [JsonProperty("TapeLength")]
        public float TapeLength { get; set; }
        [XmlAttribute("StationPosADelay")]
        [JsonProperty("StationPosADelay")]
        public float StationPosADelay { get; set; }
        [XmlAttribute("StationPosBDelay")]
        [JsonProperty("StationPosBDelay")]
        public float StationPosBDelay { get; set; }

        [XmlAttribute("UwTorqueModeVeloLimt")]
        [JsonProperty("UwTorqueModeVeloLimt")]
        public float UwTorqueModeVeloLimt { get; set; }

        [XmlAttribute("RwTorqueModeVeloLimt")]
        [JsonProperty("RwTorqueModeVeloLimt")]
        public float RwTorqueModeVeloLimt { get; set; }

        [XmlAttribute("UwRadius_AnalogMax")]
        [JsonProperty("UwRadius_AnalogMax")]
        public float UwRadius_AnalogMax { get; set; }

        [XmlAttribute("UwRadius_AnalogMin")]
        [JsonProperty("UwRadius_AnalogMin")]
        public float UwRadius_AnalogMin { get; set; }

        [XmlAttribute("UwRadius_MeasurementMax")]
        [JsonProperty("UwRadius_MeasurementMax")]
        public float UwRadius_MeasurementMax { get; set; }

        [XmlAttribute("UwRadius_MeasurementMin")]
        [JsonProperty("UwRadius_MeasurementMin")]
        public float UwRadius_MeasurementMin { get; set; }

        [XmlAttribute("RwRadius_AnalogMax")]
        [JsonProperty("RwRadius_AnalogMax")]
        public float RwRadius_AnalogMax { get; set; }

        [XmlAttribute("RwRadius_AnalogMin")]
        [JsonProperty("RwRadius_AnalogMin")]
        public float RwRadius_AnalogMin { get; set; }

        [XmlAttribute("RwRadius_MeasurementMax")]
        [JsonProperty("RwRadius_MeasurementMax")]
        public float RwRadius_MeasurementMax { get; set; }

        [XmlAttribute("RwRadius_MeasurementMin")]
        [JsonProperty("RwRadius_MeasurementMin")]
        public float RwRadius_MeasurementMin { get; set; }

        [XmlAttribute("UwSteer_AnalogMax")]
        [JsonProperty("UwSteer_AnalogMax")]
        public float UwSteer_AnalogMax { get; set; }

        [XmlAttribute("UwSteer_AnalogMin")]
        [JsonProperty("UwSteer_AnalogMin")]
        public float UwSteer_AnalogMin { get; set; }

        [XmlAttribute("UwSteer_MeasurementMax")]
        [JsonProperty("UwSteer_MeasurementMax")]
        public float UwSteer_MeasurementMax { get; set; }

        [XmlAttribute("UwSteer_MeasurementMin")]
        [JsonProperty("UwSteer_MeasurementMin")]
        public float UwSteer_MeasurementMin { get; set; }

        [XmlAttribute("RwSteer_AnalogMax")]
        [JsonProperty("RwSteer_AnalogMax")]
        public float RwSteer_AnalogMax { get; set; }

        [XmlAttribute("RwSteer_AnalogMin")]
        [JsonProperty("RwSteer_AnalogMin")]
        public float RwSteer_AnalogMin { get; set; }

        [XmlAttribute("RwSteer_MeasurementMax")]
        [JsonProperty("RwSteer_MeasurementMax")]
        public float RwSteer_MeasurementMax { get; set; }

        [XmlAttribute("RwSteer_MeasurementMin")]
        [JsonProperty("RwSteer_MeasurementMin")]
        public float RwSteer_MeasurementMin { get; set; }

        [XmlAttribute("Ruler11_AnalogMax")]
        [JsonProperty("Ruler11_AnalogMax")]
        public float Ruler11_AnalogMax { get; set; }

        [XmlAttribute("Ruler11_AnalogMin")]
        [JsonProperty("Ruler11_AnalogMin")]
        public float Ruler11_AnalogMin { get; set; }

        [XmlAttribute("Ruler11_MeasurementMax")]
        [JsonProperty("Ruler11_MeasurementMax")]
        public float Ruler11_MeasurementMax { get; set; }

        [XmlAttribute("Ruler11_MeasurementMin")]
        [JsonProperty("Ruler11_MeasurementMin")]
        public float Ruler11_MeasurementMin { get; set; }

        [XmlAttribute("Ruler12_AnalogMax")]
        [JsonProperty("Ruler12_AnalogMax")]
        public float Ruler12_AnalogMax { get; set; }

        [XmlAttribute("Ruler12_AnalogMin")]
        [JsonProperty("Ruler12_AnalogMin")]
        public float Ruler12_AnalogMin { get; set; }

        [XmlAttribute("Ruler12_MeasurementMax")]
        [JsonProperty("Ruler12_MeasurementMax")]
        public float Ruler12_MeasurementMax { get; set; }

        [XmlAttribute("Ruler12_MeasurementMin")]
        [JsonProperty("Ruler12_MeasurementMin")]
        public float Ruler12_MeasurementMin { get; set; }

        [XmlAttribute("Ruler21_AnalogMax")]
        [JsonProperty("Ruler21_AnalogMax")]
        public float Ruler21_AnalogMax { get; set; }

        [XmlAttribute("Ruler21_AnalogMin")]
        [JsonProperty("Ruler21_AnalogMin")]
        public float Ruler21_AnalogMin { get; set; }

        [XmlAttribute("Ruler21_MeasurementMax")]
        [JsonProperty("Ruler21_MeasurementMax")]
        public float Ruler21_MeasurementMax { get; set; }

        [XmlAttribute("Ruler21_MeasurementMin")]
        [JsonProperty("Ruler21_MeasurementMin")]
        public float Ruler21_MeasurementMin { get; set; }

        [XmlAttribute("Ruler22_AnalogMax")]
        [JsonProperty("Ruler22_AnalogMax")]
        public float Ruler22_AnalogMax { get; set; }

        [XmlAttribute("Ruler22_AnalogMin")]
        [JsonProperty("Ruler22_AnalogMin")]
        public float Ruler22_AnalogMin { get; set; }

        [XmlAttribute("Ruler22_MeasurementMax")]
        [JsonProperty("Ruler22_MeasurementMax")]
        public float Ruler22_MeasurementMax { get; set; }

        [XmlAttribute("Ruler22_MeasurementMin")]
        [JsonProperty("Ruler22_MeasurementMin")]
        public float Ruler22_MeasurementMin { get; set; }

        [XmlAttribute("Align11WaitPos")]
        [JsonProperty("Align11WaitPos")]
        public float Align11WaitPos { get; set; }

        [XmlAttribute("Align12WaitPos")]
        [JsonProperty("Align12WaitPos")]
        public float Align12WaitPos { get; set; }

        [XmlAttribute("Align21WaitPos")]
        [JsonProperty("Align21WaitPos")]
        public float Align21WaitPos { get; set; }

        [XmlAttribute("Align22WaitPos")]
        [JsonProperty("Align22WaitPos")]
        public float Align22WaitPos { get; set; }

        [XmlAttribute("Z1_PeelingPos")]
        [JsonProperty("Z1_PeelingPos")]
        public float Z1_PeelingPos { get; set; }

        [XmlAttribute("Z2_PeelingPos")]
        [JsonProperty("Z2_PeelingPos")]
        public float Z2_PeelingPos { get; set; }

        [XmlAttribute("Gantry11BasePos")]
        [JsonProperty("Gantry11BasePos")]
        public float Gantry11BasePos { get; set; }

        [XmlAttribute("Gantry12BasePos")]
        [JsonProperty("Gantry12BasePos")]
        public float Gantry12BasePos { get; set; }

        [XmlAttribute("Gantry21BasePos")]
        [JsonProperty("Gantry21BasePos")]
        public float Gantry21BasePos { get; set; }

        [XmlAttribute("Gantry22BasePos")]
        [JsonProperty("Gantry22BasePos")]
        public float Gantry22BasePos { get; set; }

        public ST_Param Clone()
        {
            var clone = new ST_Param();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        public void CopyTo(ST_Param dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.Z11_RulerPos = Z11_RulerPos;
            dest.Z12_RulerPos = Z12_RulerPos;
            dest.Gantry1WaitPos = Gantry1WaitPos;
            dest.Gantry1StationAGrabPos = Gantry1StationAGrabPos;
            dest.Gantry2StationAGrabPos = Gantry2StationAGrabPos;
            dest.Gantry1StationAMark1Pos = Gantry1StationAMark1Pos;
            dest.Gantry1StationAMark2Pos = Gantry1StationAMark2Pos;
            dest.Gantry1StationAMark3Pos = Gantry1StationAMark3Pos;
            dest.Gantry1StationAMark4Pos = Gantry1StationAMark4Pos;
            dest.Gantry1StationAMark5Pos = Gantry1StationAMark5Pos;
            dest.Gantry1StationAMark6Pos = Gantry1StationAMark6Pos;
            dest.Gantry1StationAMark7Pos = Gantry1StationAMark7Pos;
            dest.Gantry1StationAMark8Pos = Gantry1StationAMark8Pos;
            dest.Gantry2StationAMark1Pos = Gantry2StationAMark1Pos;
            dest.Gantry2StationAMark2Pos = Gantry2StationAMark2Pos;
            dest.Gantry2StationAMark3Pos = Gantry2StationAMark3Pos;
            dest.Gantry2StationAMark4Pos = Gantry2StationAMark4Pos;
            dest.Gantry2StationAMark5Pos = Gantry2StationAMark5Pos;
            dest.Gantry2StationAMark6Pos = Gantry2StationAMark6Pos;
            dest.Gantry2StationAMark7Pos = Gantry2StationAMark7Pos;
            dest.Gantry2StationAMark8Pos = Gantry2StationAMark8Pos;
            dest.Peeling1StartPos = Peeling1StartPos;
            dest.Peeling1EndPos = Peeling1EndPos;
            dest.Z1_DownPos = Z1_DownPos;
            dest.Z1_UpPos = Z1_UpPos;
            dest.Z21_RulerPos = Z21_RulerPos;
            dest.Z22_RulerPos = Z22_RulerPos;
            dest.Gantry2WaitPos = Gantry2WaitPos;
            dest.Gantry1StationBGrabPos = Gantry1StationBGrabPos;
            dest.Gantry2StationBGrabPos = Gantry2StationBGrabPos;
            dest.Gantry1StationBMark1Pos = Gantry1StationBMark1Pos;
            dest.Gantry1StationBMark2Pos = Gantry1StationBMark2Pos;
            dest.Gantry1StationBMark3Pos = Gantry1StationBMark3Pos;
            dest.Gantry1StationBMark4Pos = Gantry1StationBMark4Pos;
            dest.Gantry1StationBMark5Pos = Gantry1StationBMark5Pos;
            dest.Gantry1StationBMark6Pos = Gantry1StationBMark6Pos;
            dest.Gantry1StationBMark6Pos = Gantry1StationBMark6Pos;
            dest.Gantry1StationBMark7Pos = Gantry1StationBMark7Pos;
            dest.Gantry1StationBMark7Pos = Gantry1StationBMark7Pos;
            dest.Gantry1StationBMark8Pos = Gantry1StationBMark8Pos;
            dest.Gantry2StationBMark1Pos = Gantry2StationBMark1Pos;
            dest.Gantry2StationBMark2Pos = Gantry2StationBMark2Pos;
            dest.Gantry2StationBMark3Pos = Gantry2StationBMark3Pos;
            dest.Gantry2StationBMark4Pos = Gantry2StationBMark4Pos;
            dest.Gantry2StationBMark5Pos = Gantry2StationBMark5Pos;
            dest.Gantry2StationBMark6Pos = Gantry2StationBMark6Pos;
            dest.Gantry2StationBMark7Pos = Gantry2StationBMark7Pos;
            dest.Gantry2StationBMark8Pos = Gantry2StationBMark8Pos;
            dest.Peeling2StartPos = Peeling2StartPos;
            dest.Peeling2EndPos = Peeling2EndPos;
            dest.Z2_DownPos = Z2_DownPos;
            dest.Z2_UpPos = Z2_UpPos;
            dest.CamShutter1Pos0 = CamShutter1Pos0;
            dest.CamShutter1Pos1 = CamShutter1Pos1;
            dest.CamShutter1Pos2 = CamShutter1Pos2;
            dest.CamShutter1Pos3 = CamShutter1Pos3;
            dest.CamShutter2Pos0 = CamShutter2Pos0;
            dest.CamShutter2Pos1 = CamShutter2Pos1;
            dest.CamShutter2Pos2 = CamShutter2Pos2;
            dest.CamShutter2Pos3 = CamShutter2Pos3;
            dest.UwLiftUpPos = UwLiftUpPos;
            dest.RwLiftUpPos = RwLiftUpPos;
            dest.ProcessTimes = ProcessTimes;
            dest.GrabTimeOutSet = GrabTimeOutSet;
            dest.StationA_VacOkDelay = StationA_VacOkDelay;
            dest.StationB_VacOkDelay = StationB_VacOkDelay;
            dest.StationA_BlowDelay = StationA_BlowDelay;
            dest.StationB_BlowDelay = StationB_BlowDelay;
            dest.AutoLeaserMeasureNum = AutoLeaserMeasureNum;
            dest.Gantry1PowerMeterPos = Gantry1PowerMeterPos;
            dest.Gantry2PowerMeterPos = Gantry2PowerMeterPos;
            dest.LeftOffset = LeftOffset;
            dest.MidOffset = MidOffset;
            dest.RightOffset = RightOffset;
            dest.PowerMeterMeasurePos1 = PowerMeterMeasurePos1;
            dest.PowerMeterMeasurePos2 = PowerMeterMeasurePos2;
            dest.PowerMeterMeasurePos3 = PowerMeterMeasurePos3;
            dest.PowerMeterMeasurePos4 = PowerMeterMeasurePos4;
            dest.PowerMeterMeasurePos5 = PowerMeterMeasurePos5;
            dest.PowerMeterMeasurePos6 = PowerMeterMeasurePos6;
            dest.UwTorqueSet = UwTorqueSet;
            dest.RwTorqueSet = RwTorqueSet;
            dest.TapeLength = TapeLength;
            dest.StationPosADelay = StationPosADelay;
            dest.StationPosBDelay = StationPosBDelay;
            dest.UwTorqueModeVeloLimt = UwTorqueModeVeloLimt;
            dest.RwTorqueModeVeloLimt = RwTorqueModeVeloLimt;
            dest.UwRadius_AnalogMax = UwRadius_AnalogMax;
            dest.UwRadius_AnalogMin = UwRadius_AnalogMin;
            dest.UwRadius_MeasurementMax = UwRadius_MeasurementMax;
            dest.UwRadius_MeasurementMin = UwRadius_MeasurementMin;
            dest.RwRadius_AnalogMax = RwRadius_AnalogMax;
            dest.RwRadius_AnalogMin = RwRadius_AnalogMin;
            dest.RwRadius_MeasurementMax = RwRadius_MeasurementMax;
            dest.RwRadius_MeasurementMin = RwRadius_MeasurementMin;
            dest.UwSteer_AnalogMax = UwSteer_AnalogMax;
            dest.UwSteer_AnalogMin = UwSteer_AnalogMin;
            dest.UwSteer_MeasurementMax = UwSteer_MeasurementMax;
            dest.UwSteer_MeasurementMin = UwSteer_MeasurementMin;
            dest.RwSteer_AnalogMax = RwSteer_AnalogMax;
            dest.RwSteer_AnalogMin = RwSteer_AnalogMin;
            dest.RwSteer_MeasurementMax = RwSteer_MeasurementMax;
            dest.RwSteer_MeasurementMin = RwSteer_MeasurementMin;
            dest.Ruler11_AnalogMax = Ruler11_AnalogMax;
            dest.Ruler11_AnalogMin = Ruler11_AnalogMin;
            dest.Ruler11_MeasurementMax = Ruler11_MeasurementMax;
            dest.Ruler11_MeasurementMin = Ruler11_MeasurementMin;
            dest.Ruler12_AnalogMax = Ruler12_AnalogMax;
            dest.Ruler12_AnalogMin = Ruler12_AnalogMin;
            dest.Ruler12_MeasurementMax = Ruler12_MeasurementMax;
            dest.Ruler12_MeasurementMin = Ruler12_MeasurementMin;
            dest.Ruler21_AnalogMax = Ruler21_AnalogMax;
            dest.Ruler21_AnalogMin = Ruler21_AnalogMin;
            dest.Ruler21_MeasurementMax = Ruler21_MeasurementMax;
            dest.Ruler21_MeasurementMin = Ruler21_MeasurementMin;
            dest.Ruler22_AnalogMax = Ruler22_AnalogMax;
            dest.Ruler22_AnalogMin = Ruler22_AnalogMin;
            dest.Ruler22_MeasurementMax = Ruler22_MeasurementMax;
            dest.Ruler22_MeasurementMin = Ruler22_MeasurementMin;
            dest.Align11WaitPos = Align11WaitPos;
            dest.Align12WaitPos = Align12WaitPos;
            dest.Align21WaitPos = Align21WaitPos;
            dest.Align22WaitPos = Align22WaitPos;
            dest.Z1_PeelingPos = Z1_PeelingPos;
            dest.Z2_PeelingPos = Z2_PeelingPos;
            dest.Gantry11BasePos = Gantry11BasePos;
            dest.Gantry12BasePos = Gantry12BasePos;
            dest.Gantry21BasePos = Gantry21BasePos;
            dest.Gantry22BasePos = Gantry22BasePos;

        }
    }
}
