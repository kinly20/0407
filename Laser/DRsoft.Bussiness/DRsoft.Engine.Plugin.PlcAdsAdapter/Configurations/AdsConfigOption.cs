using DRsoft.Runtime.Core.Platform.Plc;
using System.Threading;

namespace DRsoft.Engine.Plugin.PlcAdsAdapter.Configurations
{
    /// <summary>
    /// 倍福配置选项
    /// </summary>
    public class AdsConfigOption : IPlcConfig
    {
        /// <summary>
        /// 配置选项Key,全局唯一
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Ads通讯节点Id
        /// </summary>
        public string AmsNetId { get; set; }

        /// <summary>
        /// Ads通讯端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 重试次数
        /// </summary>
        public int Retry { get; set; } = 2;

        /// <summary>
        /// 启用模拟状态
        /// </summary>
        public bool Simulated { get; set; }

        /// <summary>
        /// 心跳间隔
        /// </summary>
        public int HeartTime { get; set; } = 1000;

        /// <summary>
        /// </summary>
        /// 开启心跳
        public bool EnableHeartTime { get; set; } = true;

        /// <summary>
        /// 延迟加载
        /// </summary>
        public bool DelayInit { get; set; }

        /// <summary>
        /// 同步上下文（备用）
        /// </summary>
        public SynchronizationContext? Context { get; set; }
    }
}