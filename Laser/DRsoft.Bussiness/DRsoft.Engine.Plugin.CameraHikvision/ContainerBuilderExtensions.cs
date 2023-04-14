using System;
using Autofac;
using DRsoft.Engine.Core.Camera;
using DRsoft.Engine.Plugin.CameraHikvision.Configurations;
using DRsoft.Engine.Plugin.CameraHikvision.Implementation;
using DRsoft.Runtime.Core.Platform.Camera;
using DRsoft.Runtime.Core.Platform.Ioc;

namespace DRsoft.Engine.Plugin.CameraHikvision
{
    /// <summary>
    /// 
    /// </summary>
    public static class ContainerBuilderExtensions
    {
        /// <summary>
        /// 使用海康相机视觉插件
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static ContainerBuilder UseHikvision(this ContainerBuilder builder, Action<HikvisionConfigOption> configOptionBuild)
        {
            //1、读取配置,根据配置将海康相机的实例注入到容器(以实例)
            var config = new HikvisionConfigOption();
            if (configOptionBuild != null)
            {
                configOptionBuild.Invoke(config);
            }
            var HikvisionConfig = new CameraConfig()
            {
                IpAddress = config.IpAddress,
                Port = config.Port,
            };
            //2、构建实例并注入容器
            var provider = new VisualDisposeHikvision(HikvisionConfig);

            IocContainerBuilder.RegisterSingleInstance<IVisualHandler>(builder, config.Key, provider);
            //3、实例写入容器
            CameraVisualProviderManager.AddOrUpdate(config, provider);

            return builder;
        }
    }
}
