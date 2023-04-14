using DRsoft.Engine.Core;
using DRsoft.Runtime.Core.Platform.Ioc;
using DRsoft.Runtime.Core.Platform.Module;
using DRsoft.Runtime.Core.Platform.Plc;
using DRsoft.Runtime.Core.Platform.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Engine
{
    public class EnginePreLoadBoostStrap
    {
        /// <summary>
        /// 预热
        /// </summary>
        public static void PreLoad()
        {
            //1、初始化元数据
            ServiceProviderManager.Instance.ServiceProvider.GetRequiredService<IModelingProvider>()?.InitMetadata();
            //2、PLC预加载
            var plcHandler =
                ServiceProviderManager.Instance.ServiceProvider.GetService<IPlcHandler>(RuntimeEnvironment.PluginPlc);
            if (plcHandler != null && !plcHandler.IsReady) plcHandler.Initialize();
            //3、模块预加载
            ServiceProviderManager.Instance.ServiceProvider.GetRequiredService<IModuleProvider>()?.Initialize();
        }
    }
}