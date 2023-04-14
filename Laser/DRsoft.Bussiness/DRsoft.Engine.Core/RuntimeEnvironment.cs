using DRsoft.Engine.Model.Const;
using DRsoft.Runtime.Core.Platform.Config;

namespace DRsoft.Engine.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class RuntimeEnvironment
    {
        /// <summary>
        /// PLC
        /// </summary>
        public static string PluginPlc = PluginKeyDefine.PLCBeckhoff;

        /// <summary>
        /// 引擎
        /// </summary>
        public static string Engine = PluginKeyDefine.EngineProduct;

        /// <summary>
        /// 控制器
        /// </summary>
        public static string Controller = PluginKeyDefine.ControllerBeckhoff;

        /// <summary>
        /// 功率计
        /// </summary>
        public static string PowerMeter=PluginKeyDefine.PowerMeter;

        /// <summary>
        /// 视觉算法插件
        /// </summary>
        public static string VisualDispose = PluginKeyDefine.VisualDisposeHikvision;

        /// <summary>
        /// 标定插件
        /// </summary>
        public static string SingleStep = PluginKeyDefine.EngineSingleStep;

        /// <summary>
        /// 视觉算法插件
        /// </summary>
        public static string VisionCalibration = PluginKeyDefine.Calibrate;

        /// <summary>
        /// 视觉生产通讯插件
        /// </summary>
        public static string?[] VisionProduction = PluginKeyDefine.VisionProduction;

        /// <summary>
        /// Quartz调度任务业务插件
        /// </summary>
        public static string RuntimeQuartzMonitoring = "DRSoft.Dynamic.Monitoring.Plugin";

        /// <summary>
        /// Quartz调度任务PLC监听
        /// </summary>
        public static string RuntimeQuartzMonitoringPlc = "DRSoft.Dynamic.Monitoring.PLC";

        /// <summary>
        /// Quartz调度任务Controller监听
        /// </summary>
        public static string RuntimeQuartzMonitoringController = "DRSoft.Dynamic.Monitoring.Controller";

        /// <summary>
        /// Quartz调度任务相机视觉监听
        /// </summary>
        public static string RuntimeQuartzMonitoringCameraVisual = "DRSoft.Dynamic.Monitoring.CameraVisual";

        /// <summary>
        /// Quartz调度任务UI监听
        /// </summary>
        public static string RuntimeQuartzUiMonitor = "DRSoft.Dynamic.Monitoring.UI";

        /// <summary>
        /// 数据库插件
        /// </summary>
        public static string DataBase = "DRSoft.Dynamic.DataBase";

        /// <summary>
        /// 定时监听默认等待时间
        /// </summary>
        public static int RecurringJobWaitTime = 800;
    }

    /// <summary>
    /// 插件切换
    /// </summary>
    public class PluginWrap : ConfigEventBase
    {
    }
}