using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading;
using DRsoft.Engine.Core.Vision.VisionCalibration;
using DRsoft.Runtime.Core.Platform.Camera;
using DRsoft.Runtime.Core.Platform.Host;
using DRsoft.Runtime.Core.Platform.Ioc;
using DRsoft.Runtime.Core.Platform.Plc;
using DRsoft.Runtime.Core.Platform.Scheduler;
using DRsoft.Runtime.Core.Platform.Vision;
using DRsoft.Runtime.Core.ScheduleQuartz.Implementation;
using Quartz;
using RuntimeEnvironment = DRsoft.Engine.Core.RuntimeEnvironment;

// ReSharper disable RedundantCatchClause
// ReSharper disable All


namespace Engine
{
    /// <summary>
    /// 
    /// </summary>
    internal class EngineBootStrap : HostBackgroundService
    {
        private IPlcHandler? _plcHandler;
        private IVisualHandler? _visualHandler;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="aggreator"></param>
        /// <param name="plcHandler"></param>
        /// <param name="visionHandler"></param>
        /// <param name="visualHandler"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public EngineBootStrap(IEventAggregator aggreator, IPlcHandler plcHandler,
            IVisionHandler visionHandler, IVisualHandler visualHandler)
        {
            _plcHandler = plcHandler;
            _visualHandler = visualHandler;
            var eventAggregator = aggreator;
            // 设置事件聚合器
            ((AbstractVisionCalibration)visionHandler)?.SetEventAggregator(eventAggregator);
            EngineConfigManager.Instance.SetEventAggregator(eventAggregator);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken"></param>
        protected override Task HostExecuteAsync(CancellationToken stoppingToken)
        {
            //预加载配置
            EnginePreLoadBoostStrap.PreLoad();
            //主窗体
            //ServiceProviderManager.Instance.ServiceProvider.GetRequiredService<MainWindow>()?.Show();
            //初始化Job配置
            InitBootStrapJobs();
            //返回
            return Task.CompletedTask;
        }

        /// <summary>
        /// 开启监听Job
        /// </summary>
        private void InitBootStrapJobs()
        {
            try
            {
                QuartzSchedulerBootStrap.Instance.Config((config) =>
                {
                    config.Add(new TaskConfig
                    {
                        Key = RuntimeEnvironment.RuntimeQuartzMonitoring,
                        DueTime = TimeSpan.FromSeconds(15),
                        Period = TimeSpan.FromMilliseconds(2000),
                        JobType = typeof(QuartzCommonJob),
                        FireType = TaskFireEnum.Always,
                        CallBack = () => PluginMonitor()
                    });
                });

                #region Hangfire只能调度到秒级

                //// 添加定时监听
                //_recurringJobs.AddOrUpdate("DRSoft.Dynamic.Monitoring.PLC", () => PLCHeartbeat(), "0/2 * * * * ?");
                //// 添加激光器状态监听
                //_recurringJobs.AddOrUpdate("DRSoft.Dynamic.Monitoring.LASER", () => LaserMonitor(), "0/3 * * * * ?");

                #endregion

                // 默认启动的监听
                QuartzSchedulerBootStrap.Instance.RecurringJob((x) =>
                    x.FindAll(r => r.FireType == TaskFireEnum.Always));
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwProc"></param>
        /// <returns></returns>
        [DllImport("psapi.dll")]
        private static extern int EmptyWorkingSet(IntPtr hwProc);

        /// <summary>
        /// 释放内存
        /// </summary>
        private void ClearMemory()
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                EmptyWorkingSet(Process.GetCurrentProcess().Handle);
            }
            catch (Exception)
            {
            }
            finally
            {
            }

            return;
        }

        /// <summary>
        /// 获取当前系统所耗内存
        /// </summary>
        private double CurrentMemory()
        {
            long usedMemory = Process.GetCurrentProcess().WorkingSet64;
            return Convert.ToDouble(usedMemory) / 1024 / 1024;
        }

        /// <summary>
        /// 业务插件监听
        /// </summary>
        public void PluginMonitor()
        {
            Thread.Sleep(1);

            //PLC心跳监听
            _plcHandler =
                ServiceProviderManager.Instance.ServiceProvider.GetService<IPlcHandler>(RuntimeEnvironment.PluginPlc);
            _plcHandler?.Monitoring();

            //相机视觉监听
            //_visualHandler = ServiceProviderManager.Instance.ServiceProvider.GetService<IVisualHandler>(RuntimeEnvironment.VisualDispose);
            _visualHandler?.Monitoring();

            if (CurrentMemory() > 100) ClearMemory();
        }
    }

    /// <summary>
    /// 通用Job处理类
    /// </summary>
    public class QuartzCommonJob : QuartzBaseJob
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task Execute(IJobExecutionContext context)
        {
            if (context.JobDetail.JobDataMap.ContainsKey("callback"))
            {
                var callbackExpress = context.JobDetail.JobDataMap["callback"] as Expression<Action>;
                if (callbackExpress != null)
                    callbackExpress.Compile().Invoke();
            }

            return Task.CompletedTask;
        }
    }
}