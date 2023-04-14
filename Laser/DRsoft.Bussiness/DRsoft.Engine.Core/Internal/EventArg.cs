using System;

namespace DRsoft.Engine.Core.Internal
{
    /// <summary>
    /// 运行引擎事件参数
    /// </summary>
    public class EngineEventArgs: EventArgs
    {
        /// <summary>
        /// 事件编号（不是句柄）
        /// </summary>
        public int Handle;
    }
}
