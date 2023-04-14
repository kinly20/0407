// ReSharper disable All

using DRsoft.Engine.Core;
using DRsoft.Runtime.Core.DataBase.Common.Configuration;
using RJCP.IO.Ports;

namespace Engine.Configurations
{
    /// <summary>
    /// 
    /// </summary>
    public class AppRunConfig
    {
        public PlcConfig? Plc { get; set; }
        public CameraConfig? Camera { get; set; }
        public VisionConfig?[] Vision { get; set; } = new VisionConfig?[RuntimeEnvironment.VisionCalibration.Length];
        public CalibrateConfig? Calibrate { get; set; }
        public SystemConfig? SysConfig { get; set; }

        public PowerMeterConfig? PowerMeter { get; set; }
    }

    /// <summary>
    /// PLC配置
    /// </summary>
    public class PlcConfig : BaseConfigOption
    {
        public int OverTime { get; set; } = 2000;
        public string Prefix { get; set; } = "";
        public bool EnableHeartTime { get; set; }
        public bool DelayInit { get; set; } = true;
    }

    public class PowerMeterConfig:BaseConfigOption
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }

        public int DataBits { get; set; }

        public StopBits StopBits { get; set; }

        public Parity Parity { get; set; }

        public int WriteTimeout { get; set; }

        public int ReadTimeout { get; set; }
    }
    /// <summary>
    /// 相机配置
    /// </summary>
    public class CameraConfig : BaseConfigOption
    {
    }

    /// <summary>
    /// 视觉配置
    /// </summary>
    public class VisionConfig : BaseConfigOption
    {
    }

    /// <summary>
    /// 标定配置
    /// </summary>
    public class CalibrateConfig : BaseConfigOption
    {
    }

    public class BaseConfigOption
    {
        public string? Manufacturer { get; set; }
        public string? IdentificationCode { get; set; }
        public string? Ip { get; set; }
        public int Port { get; set; }
    }

    public class SystemConfig
    {
        public const string Position = "SysConfig";
        public DbConfig[] DbConfig { get; set; }
        public string DefaultPassword { get; set; }
        public static SystemConfig Config;
    }
}