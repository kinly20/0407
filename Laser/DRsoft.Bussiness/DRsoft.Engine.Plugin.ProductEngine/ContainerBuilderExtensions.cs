using Autofac;
using DRsoft.Engine.Plugin.Engine.Product.Configurations;
using DRsoft.Engine.Plugin.Engine.Product.Implementation;
using DRsoft.Engine.Core.Interface;
using DRsoft.Runtime.Core.Platform.Ioc;
using System;

namespace DRsoft.Engine.Plugin.Engine.Product
{
    /// <summary>
    /// 
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// 生产引擎
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ContainerBuilder UseEngineProduct(this ContainerBuilder builder, Action<ConfigOption> option)
        {
            var configOption = new ConfigOption();
            option?.Invoke(configOption);

            IocContainerBuilder.RegisterType<IEngine, ProductEngine>(builder, configOption.Key);
            return builder;
        }
    }
}
