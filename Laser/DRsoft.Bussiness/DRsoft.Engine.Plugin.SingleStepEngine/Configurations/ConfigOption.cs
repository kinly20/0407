using System.Threading;
using DRsoft.Engine.Model.Const;
using DRsoft.Runtime.Core.Platform.Ioc;

namespace DRsoft.Engine.Plugin.Engine.SingleStep.Configurations
{
    public class ConfigOption : IkeyConfig
    {
        public string Key { get; set; } = PluginKeyDefine.EngineSingleStep;
        public SynchronizationContext Context { get; set; }
    }
}