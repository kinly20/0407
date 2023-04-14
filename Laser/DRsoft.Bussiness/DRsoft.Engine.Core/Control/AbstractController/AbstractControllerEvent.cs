using System;
using System.ComponentModel;
using System.Linq;
using DRsoft.Engine.Core.Interface;
using DRsoft.Runtime.Core.Platform.Events;
using DRsoft.Runtime.Core.Platform.Exceptions;
using DRsoft.Runtime.Core.Platform.Logging;
using DRsoft.Runtime.Core.Platform.Plc;
using DRsoft.Runtime.Core.Platform.Plugin;
using DRsoftProperty = DRsoft.Runtime.Core.Platform;

namespace DRsoft.Engine.Core.Control.AbstractController
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AbstractController : IController, IPlugin, DRsoftProperty.Bind.INotifyPropertyChanged
    {
        /// <summary>
        /// 插件异常事件
        /// </summary>
        public event EventHandler<PlatformException> OnErrored;

        protected PubSubEvent<ControllerConfig> ControllerConfigEventManager;
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 订阅PLChangdler事件
        /// </summary>
        protected void ControllerNotify()
        {
            PlcHander.OnErrored -= PlcHander_OnErrored;
            PlcHander.OnErrored += PlcHander_OnErrored;
            PlcHander.OnConnected -= PlcHander_OnConnected;
            PlcHander.OnConnected += PlcHander_OnConnected;
            PlcHander.OnNotificationed -= PlcHandler_OnNotificationed;
            PlcHander.OnNotificationed += PlcHandler_OnNotificationed;
            PropertyChanged += AbstractController_PropertyChanged;
        }

        /// <summary>
        /// 响应变量值变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void PlcHandler_OnNotificationed(object? sender, PlcNotifyArg e);

        /// <summary>
        /// 响应PLC连接完成事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void PlcHander_OnConnected(object? sender, PlcEventArg e);

        /// <summary>
        /// 监控异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        public abstract void PlcHander_OnErrored(object? sender, PlcException e);

        /// <summary>
        /// 触发异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        protected virtual void TriggerException(ErrorInfo errorInfo, Exception ex)
        {
            OnErrored?.Invoke(this, new PlatformException(errorInfo, ex));
            Log.ErrorException(errorInfo.Message, ex);
        }

        /// <summary>
        /// 触发属性变更
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AbstractController_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            var property = this.GetType().GetProperties().FirstOrDefault(r => r.Name == e.PropertyName);
            if (property == null) return;

            property.GetValue(sender);
        }
    }
}