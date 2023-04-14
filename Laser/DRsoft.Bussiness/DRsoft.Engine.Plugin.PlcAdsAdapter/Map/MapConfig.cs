using DRsoft.Runtime.Core.Platform.Cache;
using System;

namespace DRsoft.Engine.Plugin.PlcAdsAdapter.Map
{
    /// <summary>
    /// 映射信息
    /// </summary>
    public class MapConfig : ICacheKey
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 上位机数据体类型
        /// </summary>
        public string SwFullType { get; set; }

        /// <summary>
        /// 上位类型
        /// </summary>
        internal Type SwType { get; set; }

        /// <summary>
        /// 下位机数据体类型
        /// </summary>
        public string XwFullType { get; set; }

        /// <summary>
        /// 下位类型
        /// </summary>
        internal Type XwType { get; set; }

        /// <summary>
        /// 变量句柄
        /// </summary>
        internal int Handle { get; set; }

        /// <summary>
        /// 监听句柄
        /// </summary>
        internal int NotifyHandle { get; set; }

        /// <summary>
        /// 是否开启监听
        /// </summary>
        public bool EnableNotify { get; set; }

        /// <summary>
        /// 必须启用映射
        /// 1：简单类型不用映射
        /// 2：相同类型不用映射（非类）
        /// </summary>
        public bool ShouldMap { get; set; }
    }
}