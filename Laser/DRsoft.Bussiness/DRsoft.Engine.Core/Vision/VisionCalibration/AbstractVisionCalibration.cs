using System;
using DRsoft.Engine.Model.Enum;
using DRsoft.Runtime.Core.Platform.Exceptions;
using DRsoft.Runtime.Core.Platform.Logging;
using DRsoft.Runtime.Core.Platform.Plugin;
using DRsoft.Runtime.Core.Platform.Vision;

namespace DRsoft.Engine.Core.Vision.VisionCalibration
{
    public abstract partial class AbstractVisionCalibration : MessageBase, IVisionHandler, IPlugin, IDisposable
    {
        #region 属性

        public VisionCalibrationConfig Config;
        public string SendMessage;
        #endregion

        public abstract string Scope { get; }
        public event EventHandler<PlatformException> OnErrored;

        #region 相机信息交互

        public abstract void SendMsg(string sendmsg);

        public abstract bool CommandAction(VisionCalib_Command command);

        #endregion

        public AbstractVisionCalibration(VisionCalibrationConfig config) : base()
        {
            Config = config;
            Initialize();
        }

        public virtual bool Initialize()
        {
            if (IsConnected) return true;
            return StartCommunication();
        }

        /// <summary>
        /// 启动TCPIP通讯
        /// </summary>
        /// <returns></returns>
        public virtual bool StartCommunication()
        {
            return OpenConnection(Config.IpAddress, Config.Port);
        }

        /// <summary>
        /// 停止TCPIP通讯
        /// </summary>
        /// <returns></returns>
        public virtual void StopCommunication()
        {
            CloseConnection();
        }

        public new void Dispose()
        {
            if (IsConnected)
            {
                DisconnectAndStop();
            }
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
    }
}