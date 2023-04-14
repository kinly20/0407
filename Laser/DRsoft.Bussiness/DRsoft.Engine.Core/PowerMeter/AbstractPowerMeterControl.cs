using System;
using DRsoft.Runtime.Core.Platform.Exceptions;
using DRsoft.Runtime.Core.Platform.Logging;
using DRsoft.Runtime.Core.Platform.Plugin;
using DRsoft.Runtime.Core.Platform.Power;

namespace DRsoft.Engine.Core.PowerMeter
{
    public abstract class AbstractPowerMeterControl : IPowerMeter, IPlugin, IDisposable
    {
        /// <summary>
        /// 日志
        /// </summary>
        protected ILog Log;


        public PowerMeterControlConfig Config;
        public abstract string Scope { get; }
        public event EventHandler<PlatformException>? OnErrored;

        public AbstractPowerMeterControl(PowerMeterControlConfig config) : base()
        {
            Log = LogProvider.GetLogger(this.GetType());
            Config = config;
        }


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

        public void Dispose()
        {
        }

        public abstract void Initial();

        public abstract void StopCommunication();

        public abstract void Read();

        public abstract void Write(string msg);

        public abstract double Receive { get;}

        public abstract bool IsOpen { get; }
    }
}