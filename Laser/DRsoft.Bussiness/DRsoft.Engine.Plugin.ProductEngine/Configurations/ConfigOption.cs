using DRsoft.Runtime.Core.Platform.Ioc;
using System.Threading;
using DRsoft.Engine.Model.Const;

namespace DRsoft.Engine.Plugin.Engine.Product.Configurations
{
    public class ConfigOption : IkeyConfig
    {
        public string Key { get; set; } = PluginKeyDefine.EngineProduct;
        public SynchronizationContext Context { get; set; }
    }
}
