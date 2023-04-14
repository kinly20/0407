using System;
using DRsoft.Engine.Core.Interface;
using DRsoft.Runtime.Core.Platform.Exceptions;
using DRsoft.Runtime.Core.Platform.Plugin;
using DRsoftProperty = DRsoft.Runtime.Core.Platform;

namespace DRsoft.Engine.Core.Control.AbstractController
{
    /// <summary>
    /// 
    /// </summary>
    public abstract partial class AbstractController : IController, IPlugin, DRsoftProperty.Bind.INotifyPropertyChanged
    {
        /// <summary>
        /// 释放函数
        /// </summary>
        public virtual void Dispose()
        {
            try
            {
                ControllerConnectedFlag = false;
                PlcHander?.Dispose();
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo() { Message = ex.Message, Scope = Scope }, ex);
#if DEBUG
                throw;
#endif
            }
        }
    }
}