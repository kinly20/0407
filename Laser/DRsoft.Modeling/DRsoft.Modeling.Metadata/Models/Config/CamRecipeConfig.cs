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
    [XmlType("CameraRecipeConfig")]
    [CacheProviderType(typeof(RuntimeCacheProvider<CamRecipeConfig>), typeof(DesignCacheProvider<CamRecipeConfig>), Directory = "CameraRecipeConfig")]
    public class CamRecipeConfig : IModelKey, ICloneable<CamRecipeConfig>
    {
        [XmlElement("CamRecipe")]
        [JsonProperty("CamRecipe")]
        public CamRecipe CamRecipe { get; set; } = new CamRecipe();      
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
        public CamRecipeConfig Clone()
        {
            var clone = new CamRecipeConfig();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        #endregion
        public void CopyTo(CamRecipeConfig dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.Id = this.Id;
            dest.MetadataVersion = this.MetadataVersion;
            dest.MetadataStatus = this.MetadataStatus;
            dest.Language = this.Language;
            dest.CamRecipe = CamRecipe.Clone<CamRecipe>();           
        }
    }
    [Serializable]
    public class CamRecipe : ICloneable<CamRecipe>
    {
        /// <summary>
        /// 膜参数
        /// </summary>
        [XmlElement("CarrierConfig")]
        [JsonProperty("CarrierConfig")]
        public CarrierConfig CarrierConfig { get; set; }       
        /// <summary>
        /// 拉膜的参数设置
        /// </summary>
        [XmlElement("CarrierMoveParams")]
        [JsonProperty("CarrierMoveParams")]
        public CarrierMoveParams CarrierMoveParams { get; set; }
        [XmlElement("WaferParameter")]
        [JsonProperty("WaferParameter")]
        public CamRecipeWafer WaferParameter { get; set; }
        [XmlElement("SegmentParameter")]
        [JsonProperty("SegmentParameter")]
        public CamRecipeSegment SegmentParameter { get; set; }
        //膜基准点
        [XmlElement("SegmentAdvBase")]
        [JsonProperty("SegmentAdvBase")]
        public SegmentAdvBase SegmentAdvBase { get; set; }
        public CamRecipe()
        {
            CarrierConfig = new CarrierConfig();           
            WaferParameter = new CamRecipeWafer();
            SegmentParameter = new CamRecipeSegment();
            SegmentAdvBase = new SegmentAdvBase();
        }
        public CamRecipe Clone()
        {
            var clone = new CamRecipe();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }

        public void CopyTo(CamRecipe dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.CarrierConfig = CarrierConfig.Clone<CarrierConfig>();            
            dest.WaferParameter = WaferParameter.Clone<CamRecipeWafer>();
            dest.SegmentParameter= SegmentParameter.Clone<CamRecipeSegment>();
            dest.SegmentAdvBase = SegmentAdvBase.Clone<SegmentAdvBase>();
            dest.CarrierMoveParams = CarrierMoveParams.Clone<CarrierMoveParams>();
        }
    }
    [Serializable]
    public class CarrierConfig : ICloneable<CarrierConfig>
    {
        [XmlAttribute("SegmentLength")]
        [JsonProperty("SegmentLength")]
        public double SegmentLength { get; set; }
        [XmlAttribute("WafersPerSegment")]
        [JsonProperty("WafersPerSegment")]
        public int WafersPerSegment { get; set; }
        [XmlAttribute("WafersBeforeFnC")]
        [JsonProperty("WafersBeforeFnC")]
        public int WafersBeforeFnC { get; set; }
        //[XmlAttribute("PartsPerWafer")]
        //[JsonProperty("PartsPerWafer")]
        //public PrintRelativePartType PartsPerWafer { get; set; }
        [XmlAttribute("PartsPerWafer")]
        [JsonProperty("PartsPerWafer")]
        public int PartsPerWafer { get; set; }
        [XmlAttribute("DefaultPitch")]
        [JsonProperty("DefaultPitch")]
        public double DefaultPitch { get; set; }
        [XmlAttribute("TrenchesPerSegment")]
        [JsonProperty("TrenchesPerSegment")]
        public int TrenchesPerSegment { get; set; }
        [XmlAttribute("FirstUsableTrench")]
        [JsonProperty("FirstUsableTrench")]
        public int FirstUsableTrench { get; set; }
        [XmlAttribute("LastUsableTrench")]
        [JsonProperty("LastUsableTrench")]
        public int LastUsableTrench { get; set; }
        [XmlAttribute("NumberOfTrenchesBetweenLines")]
        [JsonProperty("NumberOfTrenchesBetweenLines")]
        public int NumberOfTrenchesBetweenLines { get; set; }
        [XmlAttribute("NumberOfTrenchesInSegment")]
        [JsonProperty("NumberOfTrenchesInSegment")]
        public int NumberOfTrenchesInSegment { get; set; }
        [XmlAttribute("SegmentShiftCorrection")]
        [JsonProperty("SegmentShiftCorrection")]
        public double SegmentShiftCorrection { get; set; }
        [XmlAttribute("TentativeShiftCorrectionHi")]
        [JsonProperty("TentativeShiftCorrectionHi")]
        public int TentativeShiftCorrectionHi { get; set; }
        [XmlAttribute("TentativeShiftCorrectionLo")]
        [JsonProperty("TentativeShiftCorrectionLo")]
        public int TentativeShiftCorrectionLo { get; set; }
        [XmlAttribute("ExtraDeltaShiftCorrection")]
        [JsonProperty("ExtraDeltaShiftCorrection")]
        public double ExtraDeltaShiftCorrection { get; set; }
        [XmlAttribute("PrintLength")]
        [JsonProperty("PrintLength")]
        public double PrintLength { get; set; }
        [XmlAttribute("DefaultPrintLength")]
        [JsonProperty("DefaultPrintLength")]
        public double DefaultPrintLength { get; set; }
        [XmlAttribute("NumberOfLinesPerWafer")]
        [JsonProperty("NumberOfLinesPerWafer")]
        public int NumberOfLinesPerWafer { get; set; }
        [XmlAttribute("SegmentMarks")]
        [JsonProperty("SegmentMarks")]
        public int SegmentMarks { get; set; }
        [XmlAttribute("CurrentPALType")]
        [JsonProperty("CurrentPALType")]
        public int CurrentPALType { get; set; }
        /// <summary>
        /// PAL拍照次数
        /// </summary>
        [XmlAttribute("PALNumOfShots")]
        [JsonProperty("PALNumOfShots")]
        public ushort PALNumOfShots { get; set; }
        /// <summary>
        /// 默认硅片pitch
        /// </summary>
        [XmlAttribute("DefaultWaferPitch")]
        [JsonProperty("DefaultWaferPitch")]
        public double DefaultWaferPitch { get; set; }
        /// <summary>
        /// 膜的宽度
        /// </summary>
        [XmlAttribute("SegmentWidth")]
        [JsonProperty("SegmentWidth")]
        public double SegmentWidth { get; set; }

        public CarrierConfig Clone()
        {
            var clone = new CarrierConfig();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        public void CopyTo(CarrierConfig dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.SegmentLength = this.SegmentLength;
            dest.WafersPerSegment = WafersPerSegment;
            dest.WafersBeforeFnC = this.WafersBeforeFnC;
            dest.PartsPerWafer = this.PartsPerWafer;
            dest.DefaultPitch = DefaultPitch;
            dest.TrenchesPerSegment = TrenchesPerSegment;
            dest.FirstUsableTrench = FirstUsableTrench;
            dest.LastUsableTrench = LastUsableTrench;
            dest.NumberOfTrenchesBetweenLines = NumberOfTrenchesBetweenLines;
            dest.NumberOfTrenchesInSegment = NumberOfTrenchesInSegment;
            dest.SegmentShiftCorrection = SegmentShiftCorrection;
            dest.ExtraDeltaShiftCorrection = ExtraDeltaShiftCorrection;
            dest.TentativeShiftCorrectionHi = TentativeShiftCorrectionHi;
            dest.TentativeShiftCorrectionLo = TentativeShiftCorrectionLo;
            dest.PrintLength = PrintLength;
            dest.DefaultPrintLength = DefaultPrintLength;
            dest.NumberOfLinesPerWafer = NumberOfLinesPerWafer;
            dest.SegmentMarks = SegmentMarks;
            dest.CurrentPALType = CurrentPALType;
            dest.PALNumOfShots = PALNumOfShots;
            dest.DefaultWaferPitch = DefaultWaferPitch;
            dest.SegmentWidth = SegmentWidth;
        }
    }
    [Serializable]
    public class CarrierMoveParams : ICloneable<CarrierMoveParams>
    {
        /// <summary>
        /// 允许由于AM拍照失败，连续拉膜多少次
        /// </summary>
        [XmlAttribute("SegmentSkipMax")]
        [JsonProperty("SegmentSkipMax")]
        public int SegmentSkipMax { get; set; }
        /// <summary>
        /// 拉膜多少次，开启AM拍照
        /// </summary>
        [XmlAttribute("NumSegmentShifts")]
        [JsonProperty("NumSegmentShifts")]
        public int NumSegmentShifts { get; set; }

        public CarrierMoveParams Clone()
        {
            var clone = new CarrierMoveParams();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        public void CopyTo(CarrierMoveParams dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.SegmentSkipMax = this.SegmentSkipMax;
            dest.NumSegmentShifts = NumSegmentShifts;
        }
    }
    [Serializable]
    public class CamRecipeWafer : ICloneable<CamRecipeWafer>
    {
        /// <summary>
        /// 硅片尺寸
        /// </summary>
        [XmlElement("WaferSize")]
        [JsonProperty("WaferSize")]
        public MDCMD WaferSize { get; set; }
        /// <summary>
        /// 尺寸最大允许误差um
        /// </summary>
        [XmlElement("WaferSizeMaxVar")]
        [JsonProperty("WaferSizeMaxVar")]
        public MDCMD WaferSizeMaxVar { get; set; }
        /// <summary>
        /// 开始区域主栅
        /// </summary>
        [XmlElement("StartBusbar")]
        [JsonProperty("StartBusbar")]
        public WaferBusBar StartBusbar { get; set; }
        /// <summary>
        /// 结束区域主栅
        /// </summary>
        [XmlElement("EndBusbar")]
        [JsonProperty("EndBusbar")]
        public WaferBusBar EndBusbar { get; set; }
        /// <summary>
        /// 主栅宽度最大允许误差
        /// </summary>
        [XmlElement("BBWidMaxVar")]
        [JsonProperty("BBWidMaxVar")]
        public int BBWidMaxVar { get; set; }
        /// <summary>
        /// 主栅间距最大允许误差
        /// </summary>
        [XmlElement("BBDistMaxVar")]
        [JsonProperty("BBDistMaxVar")]
        public int BBDistMaxVar { get; set; }
        /// <summary>
        /// 主栅长度最大允许误差
        /// </summary>
        [XmlElement("BBLenMaxVar")]
        [JsonProperty("BBLenMaxVar")]
        public int BBLenMaxVar { get; set; }
        /// <summary>
        /// Pad个数
        /// </summary>
        [XmlElement("WafernPadCMD")]
        [JsonProperty("WafernPadCMD")]
        public int WafernPadCMD { get; set; }
        /// <summary>
        /// 不同的Pad尺寸个数
        /// </summary>
        [XmlElement("WaferPadnSizeNum")]
        [JsonProperty("WaferPadnSizeNum")]
        public int WaferPadnSizeNum { get; set; }
        [XmlArray("LisWaferPadSize")]
        [XmlArrayItem("WaferPadSize")]
        [JsonProperty("LisWaferPadSize")]
        public List<MDCMD> LisWaferPadSize { get; set; }
        /// <summary>
        ///  pad尺寸及中间的间距
        /// </summary>
        [XmlElement("PadSizeNumDist2Next")]
        [JsonProperty("PadSizeNumDist2Next")]
        public PadSize PadSizeNumDist2Next { get; set; }
        /// <summary>
        /// 触角个数
        /// </summary>
        [XmlElement("WaferProtrusions")]
        [JsonProperty("WaferProtrusions")]
        public int WaferProtrusions { get; set; }
        /// <summary>
        /// 触角周期
        /// </summary>
        [XmlElement("ProtrusionPeriod")]
        [JsonProperty("ProtrusionPeriod")]
        public double ProtrusionPeriod { get; set; }
        /// <summary>
        /// 触角尺寸
        /// </summary>
        [XmlElement("ProtrusionSize")]
        [JsonProperty("ProtrusionSize")]
        public MDCMD ProtrusionSize { get; set; }
        /// <summary>
        /// 十字mark线长
        /// </summary>
        [XmlElement("FidCrossLen")]
        [JsonProperty("FidCrossLen")]
        public MDCMD FidCrossLen { get; set; }
        /// <summary>
        /// 十字mark线宽
        /// </summary>
        [XmlElement("FidCrossWid")]
        [JsonProperty("FidCrossWid")]
        public MDCMD FidCrossWid { get; set; }
        /// <summary>
        /// 十字mark间距
        /// </summary>
        [XmlElement("FidCrossDist")]
        [JsonProperty("FidCrossDist")]
        public MDCMD FidCrossDist { get; set; }
        /// <summary>
        /// 圆形mark直径
        /// </summary>
        [XmlElement("FidCircleDiameter")]
        [JsonProperty("FidCircleDiameter")]
        public int FidCircleDiameter { get; set; }
        /// <summary>
        /// 圆形mark间距
        /// </summary>
        [XmlElement("FidCircleDist")]
        [JsonProperty("FidCircleDist")]
        public MDCMD FidCircleDist { get; set; }
        /// <summary>
        /// SE根数
        /// </summary>
        [XmlElement("SENfingers")]
        [JsonProperty("SENfingers")]
        public int SENfingers { get; set; }
        /// <summary>
        /// SE周期
        /// </summary>
        [XmlElement("SEFingerPeriod")]
        [JsonProperty("SEFingerPeriod")]
        public double SEFingerPeriod { get; set; }
        /// <summary>
        /// SE线宽
        /// </summary>
        [XmlElement("SEFingerWid")]
        [JsonProperty("SEFingerWid")]
        public int SEFingerWid { get; set; }
        /// <summary>
        /// SE周期最大允许误差
        /// </summary>
        [XmlElement("SEFingerPeriodMaxVar")]
        [JsonProperty("SEFingerPeriodMaxVar")]
        public int SEFingerPeriodMaxVar { get; set; }
        public CamRecipeWafer()
        {
            WaferSize=new MDCMD();
            WaferSizeMaxVar=new MDCMD();
            StartBusbar=new WaferBusBar();
            EndBusbar=new WaferBusBar();
            LisWaferPadSize=new List<MDCMD>();
            PadSizeNumDist2Next=new PadSize();
            ProtrusionSize=new MDCMD();
            FidCrossLen=new MDCMD();
            FidCrossWid=new MDCMD();
            FidCrossDist=new MDCMD();
            FidCircleDist=new MDCMD();
        }
        public CamRecipeWafer Clone()
        {
            var clone = new CamRecipeWafer();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        public void CopyTo(CamRecipeWafer dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.WaferSize=WaferSize.Clone<MDCMD>();
            dest.WaferSizeMaxVar= WaferSizeMaxVar.Clone<MDCMD>();
            dest.StartBusbar= StartBusbar.Clone<WaferBusBar>();
            dest.EndBusbar= EndBusbar.Clone<WaferBusBar>() ;
            dest.BBWidMaxVar = this.BBWidMaxVar;          
            dest.BBDistMaxVar= this.BBDistMaxVar;
            dest.BBLenMaxVar = this.BBLenMaxVar;
            dest.WafernPadCMD = this.WafernPadCMD;
            dest.WaferPadnSizeNum = WaferPadnSizeNum;
            dest.LisWaferPadSize=LisWaferPadSize.Select(x => x.Clone<MDCMD>()).ToList();
            dest.PadSizeNumDist2Next = PadSizeNumDist2Next.Clone<PadSize>();
            dest.WaferProtrusions=this.WaferProtrusions;
            dest.ProtrusionPeriod = ProtrusionPeriod;
            dest.ProtrusionSize= ProtrusionSize.Clone<MDCMD>();
            dest.FidCrossLen = FidCrossLen.Clone<MDCMD>();
            dest.FidCrossWid=FidCrossWid.Clone<MDCMD>();
            dest.FidCrossDist=FidCrossDist.Clone<MDCMD>();
            dest.FidCircleDiameter = FidCircleDiameter;
            dest.FidCircleDist= FidCircleDist.Clone<MDCMD>();
            dest.SENfingers = SENfingers;
            dest.SEFingerPeriod = SEFingerPeriod;           
            dest.SEFingerWid = SEFingerWid;
            dest.SEFingerPeriodMaxVar = SEFingerPeriodMaxVar;
        }
    }
    [Serializable]
    public class CamRecipeSegment : ICloneable<CamRecipeSegment>
    {
        /// <summary>
        /// 膜中心离相机中心的距离 um
        /// </summary>
        [XmlElement("SegmCenter2CamerasCenter")]
        [JsonProperty("SegmCenter2CamerasCenter")]
        public MDCMD SegmCenter2CamerasCenter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("SegmLocToler")]
        [JsonProperty("SegmLocToler")]
        public MDCMD SegmLocToler { get; set; }
        /// <summary>
        /// 总槽数
        /// </summary>
        [XmlElement("TotalTrenches")]
        [JsonProperty("TotalTrenches")]
        public int TotalTrenches { get; set; }
        /// <summary>
        /// 开始区域的槽线长度 um
        /// </summary>
        [XmlElement("StartTrenchLen")]
        [JsonProperty("StartTrenchLen")]
        public int StartTrenchLen { get; set; }
        /// <summary>
        /// 结束区域的槽线长度 um
        /// </summary>
        [XmlElement("EndTrenchLen")]
        [JsonProperty("EndTrenchLen")]
        public int EndTrenchLen { get; set; }
        /// <summary>
        ///  槽线周期 um
        /// </summary>
        [XmlElement("TrenchPeriod")]
        [JsonProperty("TrenchPeriod")]
        public int TrenchPeriod { get; set; }
        /// <summary>
        ///  槽线宽度 um
        /// </summary>
        [XmlElement("TrenchWid")]
        [JsonProperty("TrenchWid")]
        public int TrenchWid { get; set; }
        /// <summary>
        ///  槽线周期最大允许误差 um
        /// </summary>
        [XmlElement("TrenchPeriodMaxVar")]
        [JsonProperty("TrenchPeriodMaxVar")]
        public int TrenchPeriodMaxVar { get; set; }
        /// <summary>
        /// ROI开始查找位置 pixel
        /// </summary>
        [XmlElement("RoiStartPel")]
        [JsonProperty("RoiStartPel")]
        public MDCMD RoiStartPel { get; set; }
        /// <summary>
        /// ROI查找步进长度 pixel
        /// </summary>
        [XmlElement("RoiStepPel")]
        [JsonProperty("RoiStepPel")]
        public MDCMD RoiStepPel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("SearchHalfRangeDep")]
        [JsonProperty("SearchHalfRangeDep")]
        public MDCMD SearchHalfRangeDep { get; set; }
        /// <summary>
        /// 边缘最大允许误差 um
        /// </summary>
        [XmlElement("EdgeAlongMaxVar")]
        [JsonProperty("EdgeAlongMaxVar")]
        public MDCMD EdgeAlongMaxVar { get; set; }
        /// <summary>
        ///  一半查找范围？	um
        /// </summary>
        [XmlElement("HalfTeleRange")]
        [JsonProperty("HalfTeleRange")]
        public int HalfTeleRange { get; set; }
        /// <summary>
        ///  找槽线时的ROI个数
        /// </summary>
        [XmlElement("TrenchMapNroi")]
        [JsonProperty("TrenchMapNroi")]
        public int TrenchMapNroi { get; set; }
        /// <summary>
        ///  mark短线的个数
        /// </summary>
        [XmlElement("SegmNmarkLines")]
        [JsonProperty("SegmNmarkLines")]
        public int SegmNmarkLines { get; set; }
        /// <summary>
        ///  mark短线集合
        /// </summary>
        [XmlElement("SegmMarkLines")]
        [JsonProperty("SegmMarkLines")]
        public SegmMarkLines SegmMarkLines { get; set; }
        /// <summary>
        ///  开始区域短线mark到把边缘的距离
        /// </summary>
        [XmlElement("StartLineMarkLenEnd2End")]
        [JsonProperty("StartLineMarkLenEnd2End")]
        public int StartLineMarkLenEnd2End { get; set; }
        /// <summary>
        ///  结束区域短线mark到把边缘的距离
        /// </summary>
        [XmlElement("EndLineMarkLenEnd2End")]
        [JsonProperty("EndLineMarkLenEnd2End")]
        public int EndLineMarkLenEnd2End { get; set; }
        /// <summary>
        ///  Bath mark的个数
        /// </summary>
        [XmlElement("SegmNbathLines")]
        [JsonProperty("SegmNbathLines")]
        public int SegmNbathLines { get; set; }
        /// <summary>
        ///  Bath mark的集合
        /// </summary>
        [XmlElement("SegmBathLines")]
        [JsonProperty("SegmBathLines")]
        public SegmBathLines SegmBathLines { get; set; }
        /// <summary>
        /// Bath的宽，高
        /// </summary>
        [XmlElement("BathWidHeight")]
        [JsonProperty("BathWidHeight")]
        public MDCMD BathWidHeight { get; set; }
        /// <summary>
        ///  光斑宽度
        /// </summary>
        [XmlElement("SegmBathLowGL")]
        [JsonProperty("SegmBathLowGL")]
        public int SegmBathLowGL { get; set; }
        /// <summary>
        ///  是否使用光斑宽度
        /// </summary>
        [XmlElement("UseSpotWid")]
        [JsonProperty("UseSpotWid")]
        public bool UseSpotWid { get; set; }
        public CamRecipeSegment()
        {
            SegmCenter2CamerasCenter=new MDCMD();
            SegmLocToler=new MDCMD();
            RoiStartPel=new MDCMD();
            RoiStepPel=new MDCMD();
            SearchHalfRangeDep=new MDCMD();
            EdgeAlongMaxVar=new MDCMD();
            SegmMarkLines=new SegmMarkLines();
            SegmBathLines=new SegmBathLines();
            BathWidHeight=new MDCMD();
        }
        public CamRecipeSegment Clone()
        {
            var clone = new CamRecipeSegment();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        public void CopyTo(CamRecipeSegment dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.SegmCenter2CamerasCenter = SegmCenter2CamerasCenter.Clone<MDCMD>();
            dest.SegmLocToler=SegmLocToler.Clone<MDCMD>();
            dest.TotalTrenches = TotalTrenches;
            dest.StartTrenchLen= StartTrenchLen;
            dest.EndTrenchLen= EndTrenchLen;
            dest.TrenchPeriod = TrenchPeriod;
            dest.TrenchWid = TrenchWid;
            dest.TrenchPeriodMaxVar= TrenchPeriodMaxVar;
            dest.RoiStartPel= RoiStartPel.Clone<MDCMD>();
            dest.RoiStepPel= RoiStepPel.Clone<MDCMD>();
            dest.SearchHalfRangeDep= SearchHalfRangeDep.Clone<MDCMD>();
            dest.EdgeAlongMaxVar= EdgeAlongMaxVar.Clone<MDCMD>();
            dest.HalfTeleRange = HalfTeleRange;
            dest.TrenchMapNroi = TrenchMapNroi;
            dest.SegmNmarkLines = SegmNmarkLines;
            dest.SegmMarkLines = SegmMarkLines.Clone<SegmMarkLines>();
            dest.StartLineMarkLenEnd2End = StartLineMarkLenEnd2End;
            dest.EndLineMarkLenEnd2End= EndLineMarkLenEnd2End;
            dest.SegmNbathLines= SegmNbathLines;
            dest.SegmBathLines= SegmBathLines.Clone<SegmBathLines>();
            dest.BathWidHeight = BathWidHeight.Clone<MDCMD>();
            dest.SegmBathLowGL = SegmBathLowGL;
            dest.UseSpotWid = UseSpotWid;
        }
    }
    [Serializable]
    public class MDCMD : ICloneable<MDCMD>// 单位um
    {
        [XmlAttribute("MD")]
        [JsonProperty("MD")]
        public decimal MD { get; set; }
        [XmlAttribute("CMD")]
        [JsonProperty("CMD")]
        public decimal CMD { get; set; }
        public MDCMD Clone()
        {
            var clone = new MDCMD();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        public void CopyTo(MDCMD dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.MD = MD;
            dest.CMD = CMD;
        }
    }
    [Serializable]
    public class WaferBusBar : ICloneable<WaferBusBar>
    {
        /// <summary>
        /// 开始区域主栅根数
        /// </summary>
        [XmlAttribute("nBbMd")]
        [JsonProperty("nBbMd")]
        public int nBbMd { get; set; }
        /// <summary>
        /// 开始区域主栅宽度
        /// </summary>
        [XmlAttribute("BbWid")]
        [JsonProperty("BbWid")]
        public int BbWid { get; set; }
        /// <summary>
        /// 开始区域主栅间距
        /// </summary>
        [XmlAttribute("BbDistMd")]
        [JsonProperty("BbDistMd")]
        public int BbDistMd { get; set; }
        /// <summary>
        /// 开始区域主栅长度
        /// </summary>
        [XmlAttribute("BbLenCmd")]
        [JsonProperty("BbLenCmd")]
        public int BbLenCmd { get; set; }
        public WaferBusBar Clone()
        {
            var clone = new WaferBusBar();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        public void CopyTo(WaferBusBar dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.nBbMd = nBbMd;
            dest.BbWid = BbWid;
            dest.BbDistMd = BbDistMd;
            dest.BbLenCmd = BbLenCmd;
        }
    }
    [Serializable]
    public class PadSize : ICloneable<PadSize>
    {
        [XmlAttribute("Str")]
        [JsonProperty("Str")]
        public string Str { get; set; }
        public PadSize Clone()
        {
            var clone = new PadSize();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        public void CopyTo(PadSize dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.Str = this.Str;
        }
    }
    [Serializable]
    public class SegmMarkLines : ICloneable<SegmMarkLines>
    {
        [XmlAttribute("LisInt")]
        [JsonProperty("LisInt")]
        public List<int> LisInt { get; set; }=new List<int>();
        public SegmMarkLines Clone()
        {
            var clone = new SegmMarkLines();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        public void CopyTo(SegmMarkLines dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.LisInt = LisInt;
        }
    }
    [Serializable]
    public class SegmBathLines : ICloneable<SegmBathLines>
    {
        [XmlAttribute("LisInt")]
        [JsonProperty("LisInt")]
        public List<int> LisInt { get; set; } = new List<int>();
        public SegmBathLines Clone()
        {
            var clone = new SegmBathLines();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        public void CopyTo(SegmBathLines dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.LisInt = LisInt;
        }
    }
    [Serializable]
    public class SegmentAdvBase : ICloneable<SegmentAdvBase>
    {
        [XmlAttribute("IsCalcSuc")]
        [JsonProperty("IsCalcSuc")]
        public bool IsCalcSuc { get; set; }
        [XmlAttribute("dbX")]
        [JsonProperty("dbX")]
        public double dbX { get; set; }
        [XmlAttribute("dbY")]
        [JsonProperty("dbY")]
        public double dbY { get; set; }
        public SegmentAdvBase Clone()
        {
            var clone = new SegmentAdvBase();
            CopyTo(clone);
            return clone;
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
        public void CopyTo(SegmentAdvBase dest)
        {
            if (dest == null)
            {
                throw new ArgumentNullException(nameof(dest));
            }
            dest.IsCalcSuc = IsCalcSuc;
            dest.dbX = dbX;
            dest.dbY = dbY;
        }
    }
}
