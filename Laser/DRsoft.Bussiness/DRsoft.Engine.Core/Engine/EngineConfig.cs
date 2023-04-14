using DRsoft.Engine.Core.Camera;
using DRsoft.Engine.Core.Control;
using DRsoft.Engine.Core.Vision.VisionCalibration;
using DRsoft.Engine.Core.Vision.VisionProduction;
using DRsoft.Engine.Model.PC;
using DRsoft.Runtime.Core.Platform.Config;

namespace DRsoft.Engine.Core.Engine
{
    /// <summary>
    /// 引擎配置
    /// </summary>
    public class EngineConfig : ConfigEventBase, IConfigExt<EngineConfig>
    {
        /// <summary>
        /// 开启某些日志记录 调试
        /// </summary>
        public bool IsVerbose { get; set; }

        /// <summary>
        /// 刷新频率(毫秒) config
        /// </summary>
        public int RefreshRate { get; set; } = 75;

        public StPCParam PcParamConfig { get; set; }

        /// <summary>
        /// 控制器配置
        /// </summary>
        public ControllerConfig ControllerConfig { get; set; }

        /// <summary>
        /// AOI相机配置
        /// </summary>
        public CameraConfig CameraConfig { get; set; }

        /// <summary>
        /// 视觉生产参数
        /// </summary>
        public VisionProductionConfig ProductionConfig { get; set; }

        /// <summary>
        /// 标定参数
        /// </summary>
        public VisionCalibrationConfig CalibrationConfig { get; set; }
        /// <summary>
        /// 组件Id
        /// </summary>
        public string GroupId { get; set; }
        /// <summary>
        /// 硅胶膜Id
        /// </summary>
        public string SilicaId { get; set; }
        /// <summary>
        /// 硅胶膜是否清洗过
        /// </summary>
        public bool IsSilicaWashed { get; set; }
        /// <summary>
        /// 脏污位置是否焊接
        /// </summary>
        public bool IsDirtyPosMarked { get; set; }
        public EngineConfig()
        {
            PcParamConfig = new StPCParam();
            ControllerConfig = new ControllerConfig();
            CameraConfig = new CameraConfig();
            ProductionConfig = new VisionProductionConfig();
            CalibrationConfig = new VisionCalibrationConfig();
        }

        public bool Changed(EngineConfig obj)
        {
            if (obj == null) return false;
            return obj.IsVerbose != IsVerbose ||
                   obj.RefreshRate != RefreshRate ||
                   obj.PcParamConfig.Changed(PcParamConfig) ||
                   obj.ControllerConfig.Changed(ControllerConfig) ||
                   obj.CameraConfig.Changed(this.CameraConfig) ||
                   obj.ProductionConfig.Changed(this.ProductionConfig) ||
                   obj.CalibrationConfig.Changed(CalibrationConfig) ||
                   obj.GroupId != GroupId ||
                   obj.SilicaId != SilicaId ||
                   obj.IsSilicaWashed != IsSilicaWashed ||
                   obj.IsDirtyPosMarked != IsDirtyPosMarked;
        }

        /// <summary>
        /// 拷贝
        /// </summary>
        /// <returns></returns>
        public EngineConfig Clone()
        {
            return new EngineConfig
            {
                IsVerbose = IsVerbose,
                RefreshRate = RefreshRate,
                PcParamConfig = PcParamConfig.Clone(),
                ControllerConfig = ControllerConfig.Clone(),
                CameraConfig = CameraConfig.Clone(),
                ProductionConfig = ProductionConfig.Clone(),
                CalibrationConfig = CalibrationConfig.Clone(),
                GroupId = GroupId,
                SilicaId = SilicaId,
                IsSilicaWashed = IsSilicaWashed,
                IsDirtyPosMarked = IsDirtyPosMarked
        };
        }
    }
}