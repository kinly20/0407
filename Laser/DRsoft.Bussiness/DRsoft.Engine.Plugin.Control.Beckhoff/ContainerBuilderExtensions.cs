using Autofac;
using System;
using DRsoft.Engine.Core.Interface;
using DRsoft.Runtime.Core.Platform.Ioc;
using DRsoft.Engine.Plugin.Control.Beckhoff.Configurations;
using DRsoft.Engine.Plugin.Control.Beckhoff.Implementation;

namespace DRsoft.Engine.Plugin.Control.Beckhoff
{
    /// <summary>
    /// 
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// 控制器
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ContainerBuilder UseControllerBeckhoff(this ContainerBuilder builder, Action<ConfigOption> option)
        {
            // 设置配置选项
            var configOption = new ConfigOption();
            option?.Invoke(configOption);

            // 注册Controller
            IocContainerBuilder.RegisterType<IController, BeckhoffController>(builder, configOption.Key);
            return builder;
        }
    }
}
