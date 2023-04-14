using Autofac;
using Autofac.Extras.Quartz;
using DRsoft.Engine.Core;
using DRsoft.Engine.Model.Const;
using DRsoft.Engine.Plugin.Calibrate;
using DRsoft.Engine.Plugin.CameraHikvision;
using DRsoft.Engine.Plugin.Control.Beckhoff;
using DRsoft.Engine.Plugin.Engine.Product;
using DRsoft.Engine.Plugin.Engine.SingleStep;
using DRsoft.Engine.Plugin.PlcAdsAdapter;
using DRsoft.Engine.Plugin.PlcAdsAdapter.Configurations;
using DRsoft.Engine.Plugin.PowerMeter;
using DRsoft.Engine.Plugin.VisionProduction;
using DRsoft.Engine.Plugin.VisionProduction.Configurations;
using DRsoft.Runtime.Core.DataBase.Common.Extentions;
using DRsoft.Runtime.Core.DBService;
using DRsoft.Runtime.Core.Language;
using DRsoft.Runtime.Core.Log4net;
using DRsoft.Runtime.Core.Platform.Ioc;
using DRsoft.Runtime.Core.ScheduleQuartz;
using Engine.Configurations;
using Microsoft.Extensions.DependencyInjection;
using RJCP.IO.Ports;
using SystemConfig = DRsoft.Runtime.Core.DataBase.Common.Configuration.SystemConfig;
//using System.IO.Ports;

namespace Engine
{
    internal class ServiceProviderBootStrap
    {
        public static ServiceProviderFactory BuildFactory(AppRunConfig config)
        {
            return new ServiceProviderFactory((builder, service) =>
            {
                //控制器
                BuildPlc(builder, config);
                //相机视觉
                BuildCamera(builder, config);
                //任务调度器
                BuildSchedule(builder, service);
                //生产引擎
                BuildEngine(builder, config);
                //启用多语言
                builder.UseMultiLanguage(service);
                //启用数据库
                BuildDataBase(builder, config);
                //启用功率计
                BuildPowerMeter(builder, config);
            });
        }

        public static void BuildHostBuilder(IServiceCollection services)
        {
            // 日志记录
            services.UseLog4net();
        }

        private static void BuildPlc(ContainerBuilder builder, AppRunConfig config)
        {
            var context = System.Threading.SynchronizationContext.Current;

            #region 使用倍福(Ads方式)

            if (config.Plc != null && config.Plc.IdentificationCode == PluginKeyDefine.PLCBeckhoff)
            {
                builder.UsePlcBeckhoff(options =>
                {
                    if (config.Plc.Ip != null)
                        options.Add(new AdsConfigOption()
                        {
                            Key = PluginKeyDefine.PLCBeckhoff,
                            AmsNetId = config.Plc.Ip, //192.168.1.99.1.1
                            Port = config.Plc.Port, //851
                            Simulated = false,
                            Prefix = config.Plc.Prefix, //GVL_Init.
                            Context = context,
                            EnableHeartTime = config.Plc.EnableHeartTime,
                            DelayInit = config.Plc.DelayInit // true
                        });
                });
            }

            #endregion

            //倍福控制器
            if (config.Plc != null && config.Plc.Manufacturer == PluginKeyDefine.ControllerBeckhoff)
            {
                builder.UseControllerBeckhoff(option => { option.Key = PluginKeyDefine.ControllerBeckhoff; });
            }
        }

        private static void BuildCamera(ContainerBuilder builder, AppRunConfig config)
        {
            var context = System.Threading.SynchronizationContext.Current;

            #region 使用海康相机

            if (config.Camera != null && config.Camera.IdentificationCode == PluginKeyDefine.VisualDisposeHikvision)
            {
                builder.UseHikvision(option =>
                {
                    option.Key = PluginKeyDefine.VisualDisposeHikvision;
                    option.IpAddress = config.Camera.Ip!;
                    option.Port = config.Camera.Port;
                    option.Context = context;
                });
            }

            #endregion

            #region 视觉相关插件

            for (int i = 0; i < RuntimeEnvironment.VisionProduction.Length; i++)
            {
                if (config.Vision[i] != null && config.Vision[i]!.IdentificationCode == PluginKeyDefine.VisionProduction[i])
                {
                    builder.UseVisionProduction(options =>
                    {
                        if (config.Vision[i].Ip != null)
                            options.Add(new ConfigOption()
                            {
                                Key = PluginKeyDefine.VisionProduction[i],
                                IpAddress = config.Vision[i].Ip,
                                Port = config.Vision[i].Port,
                                Context = context!,
                            });

                    });
                }
            }
            #endregion
        }

        private static void BuildSchedule(ContainerBuilder builder, IServiceCollection service)
        {
            // 任务调度器使用Quartz
            builder.UseScheduleQuartz(service, (quartzConfigOption) =>
            {
                quartzConfigOption.Key = PluginKeyDefine.SchedulerQuartz;
                quartzConfigOption.MaxConcurrency = 10;
                // 动态注册QuartzJob
                builder.RegisterModule(new QuartzAutofacJobsModule(typeof(QuartzCommonJob).Assembly));
            }); //// 任务调度器使用Hangfire//builder.UseHangfireScheduler(config =>//{//    config.Key = PluginKeyDefine.SchedulerHangfire;//});
        }

        private static void BuildEngine(ContainerBuilder builder, AppRunConfig config)
        {
            //标定模式
            builder.UseCalibrate(option =>
            {
                option.Key = PluginKeyDefine.Calibrate;
                if (config.Calibrate != null)
                {
                    if (config.Calibrate.Ip != null) option.IpAddress = config.Calibrate.Ip; //192.168.3.2
                    option.Port = config.Calibrate.Port; // 5000
                }
            });
            //生产引擎
            builder.UseEngineProduct(option => { option.Key = PluginKeyDefine.EngineProduct; });
            //标定模式
            builder.UseEngineSingleStep(option => { option.Key = PluginKeyDefine.EngineSingleStep; });
        }

        private static void BuildDataBase(ContainerBuilder builder, AppRunConfig config, string? strName = null)
        {
            SystemConfig.Config.DbConfig = config.SysConfig.DbConfig;
            SystemConfig.Config.DefaultPassword = config.SysConfig.DefaultPassword;
            if (strName.IsNullOrEmpty())
            {
                var rdd = config.SysConfig.DbConfig.Where(r => r.Default == true);
                builder.UseDataBase(config.SysConfig.DbConfig.Where(r => r.Default == true).First());
            }
            else
            {
                builder.UseDataBase(config.SysConfig.DbConfig
                    .Where(w => w.Name.Equals(strName, StringComparison.OrdinalIgnoreCase)).First());
            }
        }

        private static void BuildPowerMeter(ContainerBuilder builder, AppRunConfig config)
        {
            var context = System.Threading.SynchronizationContext.Current;
            if (config.PowerMeter != null && config.PowerMeter.IdentificationCode == PluginKeyDefine.PowerMeter)
            {
                builder.UsePowerMeter(option =>
                {
                    option.Key = PluginKeyDefine.PowerMeter;
                    option.PortName = config.PowerMeter.PortName;
                    option.BaudRate = config.PowerMeter.BaudRate;
                    option.DataBits = config.PowerMeter.DataBits;
                    option.StopBits = (StopBits)config.PowerMeter.StopBits;
                    option.Parity = (Parity)config.PowerMeter.Parity;
                    option.WriteTimeout = config.PowerMeter.WriteTimeout;
                });
            }
        }
    }
}