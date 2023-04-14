namespace DRsoft.Engine.Model.Const
{
    /// <summary>
    /// 插件Key定义
    /// </summary>
    public static class PluginKeyDefine
    {
        /// <summary>
        /// PLC(倍福)
        /// </summary>
        public static string PLCBeckhoff = "DRSoft.Plugin.PLC.Beckhoff";

        /// <summary>
        /// 功率计
        /// </summary>
        public static string PowerMeter = "DRSoft.Plugin.PowerMeter";

        /// <summary>
        /// 海康视觉Hikvision
        /// </summary>
        public static string VisualDisposeHikvision = "DRSoft.Plugin.VisualDispose.Hikvision";

        /// <summary>
        /// 巴世乐视觉Hikvision
        /// </summary>
        public static string VisualDisposeBasler = "DRSoft.Plugin.VisualDispose.Basler";

        /// <summary>
        /// 调度器Hangfire
        /// </summary>
        public static string SchedulerHangfire = "DRSoft.Plugin.Scheduler.Hangfire";

        /// <summary>
        /// 调度器Quartz
        /// </summary>
        public static string SchedulerQuartz = "DRSoft.Plugin.Scheduler.Quartz";

        /// <summary>
        /// 引擎:生产模式
        /// </summary>
        public static string EngineProduct = "DRSoft.Plugin.Engine.Product";

        /// <summary>
        /// 引擎:标定模式
        /// </summary>
        public static string Calibrate = "DRSoft.Plugin.Calibrate";

        /// <summary>
        /// 引擎:单步模式
        /// </summary>
        public static string EngineSingleStep = "DRSoft.Plugin.Engine.SingleStep";

        /// <summary>
        /// 控制器:倍福
        /// </summary>
        public static string ControllerBeckhoff = "DRSoft.Plugin.Controller.Beckhoff";

        /// <summary>
        /// 视觉生产通讯
        /// </summary>
        public static string?[] VisionProduction = { "DRSoft.Plugin.Vision.ProductionA","DRSoft.Plugin.Vision.ProductionB" };
}
}