using System;
using Autofac;
using DRsoft.Engine.Plugin.Engine.SingleStep.Configurations;
using DRsoft.Engine.Core.Interface;
using DRsoft.Engine.Plugin.Engine.SingleStep.Implementation;
using DRsoft.Runtime.Core.Platform.Ioc;

namespace DRsoft.Engine.Plugin.Engine.SingleStep
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
        public static ContainerBuilder UseEngineSingleStep(this ContainerBuilder builder, Action<ConfigOption> option)
        {
            var configOption = new ConfigOption();
            option?.Invoke(configOption);

            IocContainerBuilder.RegisterType<IEngine, SingleStepEngine>(builder, configOption.Key);
            return builder;
        }
    }
}