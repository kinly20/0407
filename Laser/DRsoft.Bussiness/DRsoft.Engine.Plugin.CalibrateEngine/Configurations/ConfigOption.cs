using System.Threading;
using DRsoft.Engine.Model.Const;
using DRsoft.Runtime.Core.Platform.Ioc;

namespace DRsoft.Engine.Plugin.Calibrate.Configurations
{
    public class ConfigOption : IkeyConfig
    {
        public string Key { get; set; } = PluginKeyDefine.Calibrate;
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public SynchronizationContext Context { get; set; }
    }
}