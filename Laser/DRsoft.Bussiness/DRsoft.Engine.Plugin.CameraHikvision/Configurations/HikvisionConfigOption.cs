using DRsoft.Runtime.Core.Platform.Camera;
using System.Threading;
using DRsoft.Engine.Model.Const;

namespace DRsoft.Engine.Plugin.CameraHikvision.Configurations
{
    /// <summary>
    /// 海康相机视觉算法配置选项
    /// </summary>
    public class HikvisionConfigOption : IVisualConfig
    {
        /// <summary>
        /// 配置选项Key,全局唯一
        /// </summary>
        public string Key { get; set; } = PluginKeyDefine.VisualDisposeHikvision;

        public string IpAddress { get; set; }
        public int Port { get; set; }

        /// <summary>
        /// 同步上下文（备用）
        /// </summary>
        public SynchronizationContext? Context { get; set; }
    }
}