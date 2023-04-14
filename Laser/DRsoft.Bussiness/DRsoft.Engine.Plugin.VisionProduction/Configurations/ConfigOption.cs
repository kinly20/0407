using DRsoft.Runtime.Core.Platform.Ioc;
using System.Threading;

namespace DRsoft.Engine.Plugin.VisionProduction.Configurations
{
    public class ConfigOption : IkeyConfig
    {
        public string Key { get; set; } 
        public string IpAddress { get; set; }
        public int Port { get; set; }
        public SynchronizationContext Context { get; set; }
    }
}
