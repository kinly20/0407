using System;
using DRsoft.Runtime.Core.Platform.Plugin;

namespace DRsoft.Engine.Core.Interface
{
    /// <summary>
    /// 控制引擎接口
    /// </summary>
    public interface IEngine : IPlugin, IDisposable
    {
        /// <summary>
        /// 运行
        /// </summary>
        public void RunEngine();

        /// <summary>
        /// 暂停
        /// </summary>
        public void PauseEngine();
        /// <summary>
        /// 恢复
        /// </summary>
        public void ResumeEngine();

        /// <summary>
        /// 停止生产
        /// </summary>
        public void StopProduction();

        /// <summary>
        /// 启用生产模式
        /// </summary>
        /// <param name="prodType"></param>
        /// <param name="nextTubeCreation"></param>
        /// <param name="rollId"></param>
        /// <param name="selectedUwRoll"></param>
        /// <param name="prodParam"></param>
        public void EnableProduction();
        /// <summary>
        /// 错误确认
        /// </summary>
        public void ErrorAclnowledge();

    }
}
