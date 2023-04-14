using System;

namespace DRsoft.Engine.Model.Const
{
    /// <summary>
    /// 事件广播节点定义
    /// </summary>
    public static class EventBroadcastNodeDefine
    {
        /// <summary>
        /// Host身份ID
        /// </summary>
        public static Guid HostIdentity = new Guid("00000000-0000-0000-0000-100010001000");

        /// <summary>
        /// Window身份ID
        /// </summary>
        public static Guid WindowIdentity = new Guid("10000000-0000-0000-0000-100010001000");

        /// <summary>
        /// 引擎身份ID
        /// </summary>
        public static Guid EngineIdentity = new Guid("20000000-0000-0000-0000-100010001000");

        /// <summary>
        /// 控制器身份ID
        /// </summary>
        public static Guid PluginControllerIdentity = new Guid("30000000-0000-0000-0000-100010001000");

        /// <summary>
        /// 相机模块身份ID
        /// </summary>
        public static Guid PluginCameraIdentity = new Guid("30000000-0000-0000-0000-200010001000");

        /// <summary>
        /// 激光模块身份ID
        /// </summary>
        public static Guid PluginLaserIdentity = new Guid("30000000-0000-0000-0000-300010001000");

        /// <summary>
        /// 坐标转换模块身份ID
        /// </summary>
        public static Guid PluginCoordIdentity = new Guid("30000000-0000-0000-0000-400010001000");

        /// <summary>
        /// 视觉标定模块身份ID
        /// </summary>
        public static Guid VisionIdentity = new Guid("30000000-0000-0000-0000-500010001000");

        /// <summary>
        /// 窗口模块身份ID
        /// </summary>
        public static Guid PluginWindowIdentity = new Guid("30000000-0000-0000-0000-600010001000");
    }
}