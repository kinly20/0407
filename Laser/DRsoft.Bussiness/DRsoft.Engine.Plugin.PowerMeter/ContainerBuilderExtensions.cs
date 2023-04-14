using System;
using Autofac;
using DRsoft.Engine.Plugin.PowerMeter.Configuration;
using DRsoft.Runtime.Core.Platform.Ioc;
using DRsoft.Engine.Plugin.PowerMeter.Implementation;
using DRsoft.Engine.Core.PowerMeter;
using DRsoft.Runtime.Core.Platform.Power;

namespace DRsoft.Engine.Plugin.PowerMeter
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
        public static ContainerBuilder UsePowerMeter(this ContainerBuilder builder, Action<ConfigOption> option)
        {
            //1、读取配置,根据配置将视觉生产通讯的实例注入到容器(以实例)
            var configOption = new ConfigOption();
            if (option != null)
            {
                option.Invoke(configOption);
            }

            //2、构建实例并注入容器
            var PowerMeterControlConfig = new PowerMeterControlConfig()
            {
                PortName = configOption.PortName,
                BaudRate = configOption.BaudRate,
                DataBits = configOption.DataBits,
                StopBits = configOption.StopBits,
                Parity = configOption.Parity,
                WriteTimeout = configOption.WriteTimeout,
                ReadTimeout = configOption.ReadTimeout,
            };
            var provider = new PowerMeterControlPlugin(PowerMeterControlConfig);

            IocContainerBuilder.RegisterSingleInstance<IPowerMeter>(builder, configOption.Key, (IPowerMeter)provider);

            //3、实例写入容器
            PowerMeterProviderManager.AddOrUpdate(configOption, (IPowerMeter)provider);

            return builder;
        }
    }
}
