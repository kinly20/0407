using System;
using System.Collections.Generic;
using Autofac;
using DRsoft.Engine.Plugin.VisionProduction.Configurations;
using DRsoft.Engine.Plugin.VisionProduction.Implementation;
using DRsoft.Runtime.Core.Platform.Ioc;
using DRsoft.Runtime.Core.Platform.Vision;

namespace DRsoft.Engine.Plugin.VisionProduction
{
    /// <summary>
    /// 
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ContainerBuilder UseVisionProduction(this ContainerBuilder builder, Action<List<ConfigOption>> option)
        {
            //1、读取配置,根据配置将视觉生产通讯的实例注入到容器(以实例)
            var configOptions = new List<ConfigOption>();
            if (option != null)
            {
                option.Invoke(configOptions);
            }

            //2、构建实例并注入容器
            configOptions.ForEach(config =>
            {
                var provider = new VisionProductionPlugin(config);

                IocContainerBuilder.RegisterSingleInstance<IVisionHandler>(builder, config.Key, provider);

                //3、实例写入容器
                VisionProviderManager.AddOrUpdate(config, provider);
            });
            return builder;
        }
    }
}
