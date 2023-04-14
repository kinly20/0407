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
    [XmlType("CameraConfig")]
    [CacheProviderType(typeof(RuntimeCacheProvider<CamConfig>), typeof(DesignCacheProvider<CamConfig>), Directory = "CameraConfig")]
    public class CamConfig : IModelKey, ICloneable<CamConfig>
    {
        private int ptpCameraNum;
        private string ipAddress;
        public bool IsChanged { get;  set; }
        /// <summary>
        /// 相机数量
        /// </summary>
        [XmlElement("PtpCameraNum")]
        [JsonProperty("PtpCameraNum")]
        public int PtpCameraNum
        {
            get => ptpCameraNum;
            set
            {
                if (value != ptpCameraNum)
                {
                    IsChanged = true;
                    ptpCameraNum = value;
                }                  
            }
        }
        [XmlElement("IpAddress")]
        [JsonProperty("IpAddress")]
        public string IpAddress { get => ipAddress;
            set
            {
                if(value!=ipAddress)
                {
                    IsChanged = true;
                    ipAddress = value;
                }
            } }
        [XmlElement("Port")]
        [JsonProperty("Port")]
        public int Port { get; set; }
        //相机标定使用的端口
        [XmlElement("CalibrationPort")]
        [JsonProperty("CalibrationPort")]
        public int CalibrationPort { get; set; }
        /// <summary>
        /// 铝膜标定 Tilt轴角度
        /// </summary>
        [XmlElement("TiltCalibAng")]
        [JsonProperty("TiltCalibAng")]
        public double TiltCalibAng { get; set; }        
        /// <summary>
        /// 相机标定 chuck盘
        /// </summary>
        [XmlElement("ChuckCalibName")]
        [JsonProperty("ChuckCalibName")]
        public int ChuckCalibName { get; set; }
        /// <summary>
        /// 标定类型
        /// </summary>
        [XmlElement("CalibType")]
        [JsonProperty("CalibType")]
        public int CalibType { get; set; }
        //标定
        [XmlElement("PALOffsetXY")]
        [JsonProperty("PALOffsetXY")]
        public double PALOffsetXY { get; set; }
        //标定
        [XmlElement("PALOffsetT")]
        [JsonProperty("PALOffsetT")]
        public double PALOffsetT { get; set; }
        //标定
        [XmlElement("PALOffsetTCount")]
        [JsonProperty("PALOffsetTCount")]
        public double PALOffsetTCount
        {
            get; 
            set;
        }
        //标定
        [XmlElement("CaliCycleTime")]
        [JsonProperty("CaliCycleTime")]
        public int CaliCycleTime
        {
            get;
            set;
        }
        [XmlArray("LisCamera")]
        [XmlArrayItem("CameraBase")]
        [JsonProperty("LisCamera")]
        public List<CameraBase> LisCamera { get; set; } = new List<CameraBase>();
        [XmlElement("Normal")]
        [JsonProperty("Normal")]
        public CamNormal Normal { get; set; }
        [XmlArray("LisSEModule")]
        [XmlArrayItem("RecipeModule")]
        [JsonProperty("LisSEModule")]
        public List<CamRecipeModule> LisSEModule { get; set; }
        [XmlArray("LisPALModule")]
        [XmlArrayItem("RecipeModule")]
        [JsonProperty("LisPALModule")]
        public List<CamRecipeModule> LisPALModule { get; set; }
        [XmlArray("LisAMModule")]
        [XmlArrayItem("RecipeModule")]
        [JsonProperty("LisAMModule")]
        public List<CamRecipeModule> LisAMModule { get; set; }
        [XmlArray("LisPQModule")]
        [XmlArrayItem("RecipeModule")]
        [JsonProperty("LisPQModule")]
        public List<CamRecipeModule> LisPQModule { get; set; }
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
        public CamConfig()
        {
            Normal = new CamNormal();
            LisSEModule = new List<CamRecipeModule>();
            LisPALModule = new List<CamRecipeModule>();
            LisAMModule = new List<CamRecipeModule>();
            LisPQModule = new List<CamRecipeModule>();
        }
        public CamConfig Clone()
        {
            var clone = new CamConfig();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        #endregion
        public void CopyTo(CamConfig dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.Id = this.Id;
            dest.MetadataVersion = this.MetadataVersion;
            dest.MetadataStatus = this.MetadataStatus;
            dest.Language = this.Language;
            dest.PtpCameraNum = this.PtpCameraNum;
            dest.IpAddress = this.IpAddress;
            dest.Port = this.Port;
            dest.CalibrationPort = this.CalibrationPort;
            dest.TiltCalibAng = this.TiltCalibAng;
            dest.ChuckCalibName= this.ChuckCalibName;
            dest.CalibType = this.CalibType;
            dest.PALOffsetXY= this.PALOffsetXY;
            dest.PALOffsetT= this.PALOffsetT;
            dest.PALOffsetTCount = this.PALOffsetTCount;
            dest.CaliCycleTime = this.CaliCycleTime;
            dest.LisCamera = LisCamera.Select(x => x.Clone<CameraBase>()).ToList();
            dest.Normal = Normal.Clone<CamNormal>();
            dest.LisSEModule = LisSEModule.Select(x => x.Clone<CamRecipeModule>()).ToList();
            dest.LisPALModule = LisPALModule.Select(x => x.Clone<CamRecipeModule>()).ToList();
            dest.LisAMModule = LisAMModule.Select(x => x.Clone<CamRecipeModule>()).ToList();
            dest.LisPQModule = LisPQModule.Select(x => x.Clone<CamRecipeModule>()).ToList();
        }
    }
    [Serializable]
    public class CameraBase : ICloneable<CameraBase>
    {
        /// <summary>
        /// 相机名称
        /// </summary>
        [XmlAttribute("Name")]
        [JsonProperty("Name")]
        public string Name { get; set; }
        /// <summary>
        /// 延时触发 us 
        /// </summary>
        [XmlAttribute("TriggerDelay")]
        [JsonProperty("TriggerDelay")]
        public int TriggerDelay { get; set; }
        /// <summary>
        /// 输出使能
        /// </summary>
        [XmlAttribute("OutputEnable")]
        [JsonProperty("OutputEnable")]
        public bool OutputEnable { get; set; }
        /// <summary>
        /// 输出线路持续时间 us
        /// </summary>
        [XmlAttribute("OutputDuration")]
        [JsonProperty("OutputDuration")]
        public int OutputDuration { get; set; }
        /// <summary>
        /// 输出线路延迟 us  
        /// </summary>
        [XmlAttribute("OutputDelay")]
        [JsonProperty("OutputDelay")]
        public int OutputDelay { get; set; }
        /// <summary>
        /// 输出线路预延迟 us
        /// </summary>
        [XmlAttribute("OutputPreDelay")]
        [JsonProperty("OutputPreDelay")]
        public int OutputPreDelay { get; set; }
        public CameraBase Clone()
        {
            var clone = new CameraBase();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        public void CopyTo(CameraBase dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.Name = this.Name;
            dest.TriggerDelay = this.TriggerDelay;
            dest.OutputEnable = this.OutputEnable;
            dest.OutputDuration = this.OutputDuration;
            dest.OutputDelay = this.OutputDelay;
            dest.OutputPreDelay = this.OutputPreDelay;
        }
    }
    [Serializable]
    public class CamNormal : ICloneable<CamNormal>
    {
        /// <summary>
        /// 功能类型 CheckBoardRecognize; FoilRecognize;CalibWaferRecognize;Production…
        /// 生产中   膜标定  硅片标定等
        /// </summary>
        [XmlAttribute("FunctionMode")]
        [JsonProperty("FunctionMode")]
        public string FunctionMode { get; set; }
        /// <summary>
        ///Pal工位数
        /// </summary>
        [XmlAttribute("PalNumber")]
        [JsonProperty("PalNumber")]
        public int PalNumber { get; set; }
        /// <summary>
        ///SE工位数
        /// </summary>
        [XmlAttribute("SENumber")]
        [JsonProperty("SENumber")]
        public int SENumber { get; set; }
        /// <summary>
        ///AM工位数
        /// </summary>
        [XmlAttribute("AMNumber")]
        [JsonProperty("AMNumber")]
        public int AMNumber { get; set; }
        /// <summary>
        ///PQ工位数
        /// </summary>
        [XmlAttribute("PQNumber")]
        [JsonProperty("PQNumber")]
        public int PQNumber { get; set; }
        public CamNormal Clone()
        {
            var clone = new CamNormal();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        public void CopyTo(CamNormal dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.FunctionMode = FunctionMode;
            dest.PalNumber = PalNumber;
            dest.SENumber = SENumber;
            dest.AMNumber = AMNumber;
            dest.PQNumber = PQNumber;
        }
    }
    [Serializable]
    public class CamRecipeModule : ICloneable<CamRecipeModule>
    {
        /// <summary>
        ///是否启用
        /// </summary>
        [XmlAttribute("Useable")]
        [JsonProperty("Useable")]
        public bool Useable { get; set; }
        /// <summary>
        /// 触发类型 H-硬触发，用实际抓图；G-硬触发，用原图；S-软触发，用实际抓图；F-软触发，用原图
        /// </summary>
        [XmlAttribute("TriggerType")]
        [JsonProperty("TriggerType")]
        public string TriggerType { get; set; }
        /// <summary>
        /// 出光相机数量
        /// </summary>
        [XmlAttribute("StartCameraNum")]
        [JsonProperty("StartCameraNum")]
        public int StartCameraNum { get; set; }
        /// <summary>
        /// 关光相机数量
        /// </summary>
        [XmlAttribute("EndCameraNum")]
        [JsonProperty("EndCameraNum")]
        public int EndCameraNum { get; set; }
        /// <summary>
        /// 出光方向相机名称
        /// </summary>
        [XmlAttribute("StartCameraName")]
        [JsonProperty("StartCameraName")]
        public List<string> StartCameraName { get; set; } = new List<string>();//
        /// <summary>
        /// 关光方向相机名称
        /// </summary>
        [XmlAttribute("EndCameraName")]
        [JsonProperty("EndCameraName")]
        public List<string> EndCameraName { get; set; } = new List<string>();
        /// <summary>
        /// 识别方式 Piece-一片；Double-两片…;SE;Mark
        /// </summary>
        [XmlAttribute("RecognizeMode")]
        [JsonProperty("RecognizeMode")]
        public string RecognizeMode { get; set; } = "";
        /// <summary>
        /// 灰度？阈值？	
        /// </summary>
        [XmlAttribute("MinAvStdMFprcnt")]
        [JsonProperty("MinAvStdMFprcnt")]
        public int MinAvStdMFprcnt { get; set; }
        public CamRecipeModule Clone()
        {
            var clone = new CamRecipeModule();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        public void CopyTo(CamRecipeModule dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.Useable = Useable;
            dest.TriggerType = TriggerType;
            dest.StartCameraNum = StartCameraNum;
            dest.EndCameraNum = EndCameraNum;
            dest.StartCameraName = StartCameraName;
            dest.EndCameraName = EndCameraName;
            dest.RecognizeMode = RecognizeMode;
            dest.MinAvStdMFprcnt = MinAvStdMFprcnt;
        }
    }
}
