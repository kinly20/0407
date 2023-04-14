using System.Linq.Expressions;
using System.Reflection;
using DRsoft.Engine.Model.Const;
using DRsoft.Runtime.Core.Platform.Ioc;
using DRsoft.Runtime.Core.Platform.Scheduler;
using DRsoft.Runtime.Core.ScheduleQuartz.Implementation;
/* Unmerged change from project 'Engine (netcoreapp3.1)'
Before:
using System.Linq.Expressions;
After:
using System.Linq.Expressions;
using Engine;
using Engine;
using Engine;
*/

namespace Engine
{
    /// <summary>
    /// QuartzScheduler之Job启动类
    /// </summary>
    public class QuartzSchedulerBootStrap
    {
        private static QuartzSchedulerBootStrap _bootStrap;

        public static QuartzSchedulerBootStrap Instance
        {
            get
            {
                if (_bootStrap == null)
                    _bootStrap = new QuartzSchedulerBootStrap();
                return _bootStrap;
            }
        }

        private static readonly object SchedulerLock = new object();
        private static readonly List<MethodInfo> QuartzMethods;

        /// <summary>
        /// 任务配置项
        /// </summary>
        private static readonly List<TaskConfig> TaskConfigs = new List<TaskConfig>();

#pragma warning disable
        static QuartzSchedulerBootStrap()
#pragma warning restore
        {
            QuartzMethods = typeof(QuartzScheduler).GetMethods().ToList();
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="taskConfigOption"></param>
        /// <returns></returns>
        public QuartzSchedulerBootStrap Config(Action<List<TaskConfig>> taskConfigOption)
        {
            // 设置任务配置
            lock (SchedulerLock)
            {
                TaskConfigs.Clear();
            }

            taskConfigOption?.Invoke(TaskConfigs);
            return this;
        }

        /// <summary>
        /// Job
        /// </summary>
        public void RecurringJob()
        {
            // 执行job
            var curScheduler = GetQuartzScheduler();
            // 反射方法AddRecurringJob
            var methodInfo = QuartzMethods.FirstOrDefault(r => r.Name == "AddRecurringJob" && r.IsGenericMethod);

            TaskConfigs.FindAll(r => r.FireType == TaskFireEnum.Default).ForEach(item =>
            {
                var curMethodInfo = methodInfo?.MakeGenericMethod(item.JobType);
                curMethodInfo?.Invoke(curScheduler, new object[] { item });
            });
        }

        /// <summary>
        /// 按指定条件创建Job
        /// </summary>
        public void RecurringJob(Expression<Func<List<TaskConfig>, List<TaskConfig>>> expression)
        {
            var curTaskConfigs = expression.Compile().Invoke(TaskConfigs);
            if (!curTaskConfigs.Any()) return;

            // 执行job
            var curScheduler = GetQuartzScheduler();
            // 反射方法AddRecurringJob
            var methodInfo = QuartzMethods.FirstOrDefault(r => r.Name == "AddRecurringJob" && r.IsGenericMethod);

            curTaskConfigs.ForEach(item =>
            {
                var curMethodInfo = methodInfo?.MakeGenericMethod(item.JobType);
                curMethodInfo?.Invoke(curScheduler, new object[] { item });
            });
        }


        /// <summary>
        /// 回收
        /// </summary>
        public void Recovery()
        {
            GetQuartzScheduler().RemoveJob(TaskConfigs.FindAll(r => r.FireType == TaskFireEnum.Default)
                .Select(r => r.Key).ToList());
        }

        /// <summary>
        /// 按指定条件回收
        /// </summary>
        /// <param name="expression"></param>
        public void Recovery(Expression<Func<List<TaskConfig>, List<TaskConfig>>> expression)
        {
            var curTaskConfigs = expression.Compile().Invoke(TaskConfigs);
            if (!curTaskConfigs.Any()) return;

            GetQuartzScheduler().RemoveJob(curTaskConfigs.Select(r => r.Key).ToList());
        }

        /// <summary>
        /// 获取调度器
        /// </summary>
        /// <returns></returns>
        private ITaskScheduler GetQuartzScheduler()
        {
            return ServiceProviderManager.Instance.ServiceProvider.GetService<ITaskScheduler>(PluginKeyDefine
                .SchedulerQuartz) ?? new QuartzScheduler();
        }
    }
}