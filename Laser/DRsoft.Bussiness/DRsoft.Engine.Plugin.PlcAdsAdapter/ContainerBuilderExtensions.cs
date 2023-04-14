using Autofac;
using DRsoft.Engine.Plugin.PlcAdsAdapter.Configurations;
using DRsoft.Engine.Plugin.PlcAdsAdapter.Map;
using DRsoft.Runtime.Core.Platform.Ioc;
using DRsoft.Runtime.Core.Platform.Plc;
using System;
using System.Collections.Generic;
using PLC = DRsoft.Engine.Plugin.PlcAdsAdapter.Implementation;

namespace DRsoft.Engine.Plugin.PlcAdsAdapter
{
    /// <summary>
    /// 
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// 使用倍福PLC控制
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ContainerBuilder UsePlcBeckhoff(this ContainerBuilder builder, Action<List<AdsConfigOption>> configOptionBuild)
        {
            //1、读取配置,根据配置将PLC的实例注入到容器(以实例)
            var configOptions = new List<AdsConfigOption>();
            if (configOptionBuild != null)
            {
                configOptionBuild.Invoke(configOptions);
            }

            //2、加载上下位数据类型转换配置信息
            var mapConfig = new MapConfigProvider();
            mapConfig.Init();

            var mapperProvider = new MapperProvider(mapConfig);

            //3、构建实例并注入容器
            configOptions.ForEach(config =>
            {
                var provider = new PLC.PlcAdsAdapter(config, mapperProvider);
                if (!config.DelayInit) provider.Initialize();

                IocContainerBuilder.RegisterSingleInstance<IPlcHandler>(builder, config.Key, provider);

                PlcProviderManager.AddOrUpdate(config, provider);
            });

            return builder;
        }
    }
}
