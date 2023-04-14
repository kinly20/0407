using System.Threading;
using DRsoft.Engine.Model.Const;
using DRsoft.Runtime.Core.Platform.Ioc;

namespace DRsoft.Engine.Plugin.Control.Beckhoff.Configurations
{
    /// <summary>
    /// 控制器配置选项
    /// </summary>
    public class ConfigOption : IkeyConfig
    {
        public string Key { get; set; } = PluginKeyDefine.ControllerBeckhoff;
        public SynchronizationContext Context { get; set; }
    }
}