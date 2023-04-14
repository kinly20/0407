using System;
using Autofac;
using DRsoft.Engine.Core.Vision.VisionCalibration;
using DRsoft.Engine.Plugin.Calibrate.Configurations;
using DRsoft.Engine.Plugin.Calibrate.Implementation;
using DRsoft.Runtime.Core.Platform.Ioc;
using DRsoft.Runtime.Core.Platform.Vision;

namespace DRsoft.Engine.Plugin.Calibrate
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
        public static ContainerBuilder UseCalibrate(this ContainerBuilder builder, Action<ConfigOption> option)
        {
            //1、读取配置,根据配置将实例注入到容器(以实例)
            var config = new ConfigOption();
            if (option != null)
            {
                option.Invoke(config);
            }
            //2、构建实例并注入容器
            var configOption = new VisionCalibrationConfig()
            {
                IpAddress = config.IpAddress,
                Port = config.Port
            };
            var provider = new CalibratePlugin(configOption);
            
            IocContainerBuilder.RegisterSingleInstance<IVisionHandler>(builder, config.Key, provider);
            
            //3、实例写入容器
            VisionProviderManager.AddOrUpdate(config, provider);

            
            return builder;
        }
    }
}
