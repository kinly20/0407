using DRsoft.Engine.Core.Engine;
using DRsoft.Engine.Core.Vision.VisionCalibration;
using DRsoft.Engine.Model.Const;
using DRsoft.Runtime.Core.Platform.Events;

namespace Engine
{
    /// <summary>
    /// 引擎配置参数管理器
    /// </summary>
    internal class EngineConfigManager
    {
        private static EngineConfigManager? _instance;
        private IEventAggregator? _aggregator;
        private static readonly object SLockObject = new object();

        public static EngineConfigManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SLockObject)
                    {
                        _instance = new EngineConfigManager();
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        public void SetEventAggregator(IEventAggregator arg)
        {
            _aggregator = arg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public void Changed(EngineConfig config)
        {
            if (config == null || _aggregator == null) return;

            config.Reset();
            config.SetOwer(EventBroadcastNodeDefine.WindowIdentity);
            config.AddSubscriber(EventBroadcastNodeDefine.EngineIdentity);

            new TaskFactory().StartNew((obj) =>
            {
                var curConfig = obj as EngineConfig;
                if (curConfig == null) return;

                // 发布变更事件
                _aggregator.GetEvent<PubSubEvent<EngineConfig>>().Publish(curConfig);
            }, config);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public void Changed(VisionCalibrationConfig config)
        {
            if (config == null || _aggregator == null) return;

            config.Reset();
            config.SetOwer(EventBroadcastNodeDefine.WindowIdentity);
            config.AddSubscriber(EventBroadcastNodeDefine.EngineIdentity);

            new TaskFactory().StartNew((obj) =>
            {
                var curConfig = obj as VisionCalibrationConfig;
                if (curConfig == null) return;

                // 发布变更事件
                _aggregator.GetEvent<PubSubEvent<VisionCalibrationConfig>>().Publish(curConfig);
            }, config);
        }
    }
}