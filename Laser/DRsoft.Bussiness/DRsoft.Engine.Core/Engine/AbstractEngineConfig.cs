using System.Threading;
using DRsoft.Engine.Core.Control;
using DRsoft.Engine.Core.Interface;
using DRsoft.Engine.Model.Const;
using DRsoft.Runtime.Core.Platform.Events;

namespace DRsoft.Engine.Core.Engine
{
    /// <summary>
    /// 运行引擎配置变更事件
    /// </summary>
    public abstract partial class AbstractEngine : IEngine
    {
        /// <summary>
        /// 引擎配置事件管理器
        /// </summary>
        protected PubSubEvent<EngineConfig> EngineConfigEventManager;

        /// <summary>
        /// 坐标配方配置事件管理器
        /// </summary>
        protected PubSubEvent<ControllerConfig> ControllerConfigEventManager;

        /// <summary>
        /// 引擎配置文件控制锁
        /// </summary>
        static ReaderWriterLockSlim _engineConfigReadWriterLock = new ReaderWriterLockSlim();

        /// <summary>
        /// 注册配置文件变更响应
        /// </summary>
        public virtual void RegisterConfigChangedEventManager()
        {
            EngineConfigEventManager = Aggregator.GetEvent<PubSubEvent<EngineConfig>>();
            ControllerConfigEventManager = Aggregator.GetEvent<PubSubEvent<ControllerConfig>>();

            //1、订阅引擎配置变更事件(界面=>引擎)
            EngineConfigEventManager.Subscribe((config) => { NotifyFromFrontToEngine(config); },
                filterConfig => EventBroadcastNodeDefine.WindowIdentity == filterConfig.Ower &&
                                filterConfig.Subscriber.Contains(EventBroadcastNodeDefine.EngineIdentity));

            //2、订阅控制器配方变更事件(控制器插件=>引擎)
            ControllerConfigEventManager.Subscribe((config) => { Config.ControllerConfig = config; },
                filterConfig => EventBroadcastNodeDefine.PluginControllerIdentity == filterConfig.Ower &&
                                filterConfig.Subscriber.Contains(EventBroadcastNodeDefine.EngineIdentity));
        }

        /// <summary>
        /// 界面修改配置通知Engine变更
        /// </summary>
        /// <param name="config"></param>
        public virtual void NotifyFromFrontToEngine(EngineConfig config)
        {
            if (config == null) return;

            var curConfig = config.Clone();

            //控制器配置发生变更
            if (curConfig.ControllerConfig != null)
            {
                curConfig.ControllerConfig.Reset();
                curConfig.ControllerConfig.SetOwer(EventBroadcastNodeDefine.EngineIdentity);
                curConfig.ControllerConfig.AddSubscriber(EventBroadcastNodeDefine.PluginControllerIdentity);
                Config.ControllerConfig = curConfig.ControllerConfig;
                ControllerConfigEventManager.Publish(curConfig.ControllerConfig);
            }
        }

        /// <summary>
        /// Engine配置变更通知前端响应
        /// </summary>
        /// <param name="config"></param>
        public virtual void NotifyFromEngineToFront(EngineConfig config)
        {
            _engineConfigReadWriterLock.EnterWriteLock();

            // 设置接收者
            config.Reset();
            config.SetOwer(EventBroadcastNodeDefine.EngineIdentity);
            config.AddSubscriber(EventBroadcastNodeDefine.WindowIdentity);
            Config = config;
            _engineConfigReadWriterLock.ExitWriteLock();
            // 发布
            EngineConfigEventManager.Publish(config);
        }
    }
}