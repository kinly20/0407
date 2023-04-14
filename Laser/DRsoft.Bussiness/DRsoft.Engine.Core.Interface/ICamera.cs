using DRsoft.Runtime.Core.Platform.Plugin;
using System;

namespace DRsoft.Engine.Core.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICamera : IPlugin, IDisposable
    {
        /// <summary>
        /// 初始化相机参数
        /// </summary>
        public void Initialize();
    }
}
