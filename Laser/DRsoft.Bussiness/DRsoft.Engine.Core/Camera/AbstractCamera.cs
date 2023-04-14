using DRsoft.Engine.Model.Enum;
using DRsoft.Runtime.Core.Platform.Camera;
using DRsoft.Runtime.Core.Platform.Events;
using DRsoft.Runtime.Core.Platform.Logging;
using DRsoft.Runtime.Core.Platform.Plugin;
using DRsoft.Runtime.Core.Platform.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using DRsoft.Engine.Core.Internal;

namespace DRsoft.Engine.Core.Camera
{
    /// <summary>
    /// 采图完成回调委托
    /// </summary>
    /// <param name="info"></param>
    /// <param name="grabedResult"></param>
    public delegate void ImageGrabedBackDelegate(CameraInterfaceCommandType info, string GrabedBufferResult);

    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractCameraVisual : MessageBase, IVisualHandler, IPlugin, IDisposable
    {
        #region 属性

        protected bool IsInitOk = false;
        public CameraConfig SystemConfig;
        protected bool Exit = false;
        public Dictionary<string, string> MassageDic;
        public abstract string Scope { get; }
        public string Test { get; set; }

        public event ImageGrabedBackDelegate OnImageGrabedBack;

        public event EventHandler<PlatformException> OnErrored;

        #endregion

        public AbstractCameraVisual(CameraConfig Config) : base()
        {
            SystemConfig = Config;
        }

        /// <summary>
        /// 设置事件聚合器
        /// </summary>
        /// <param name="aggreator"></param>
        public override void SetEventAggregator(IEventAggregator aggreator)
        {
            base.SetEventAggregator(aggreator);
            PubSubEventManager = aggreator.GetEvent<PubSubEvent<EngineEventArgs>>();
        }

        /// <summary>
        /// 初始化，设置配置
        /// </summary>
        public virtual bool InitVisualDispose()
        {
            IsInitOk = false;
            try
            {
                if (!OpenConnection()) return false;
                return true;
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = "AOI相机引擎初始化失败" }, ex);
                return false;
            }
        }

        protected override void OnConnected()
        {
            base.OnConnected();
            UpdateConfig();
            IsInitOk = true;
        }

        protected override void OnDisconnected()
        {
            base.OnDisconnected();
        }

        protected override void OnError(SocketError error)
        {
            base.OnError(error);
        }

        public virtual bool OpenConnection()
        {
            return OpenConnection(SystemConfig.IpAddress, SystemConfig.Port);
        }

        public virtual void UpdateConfig()
        {
            if (!IsInitOk) return;
        }

        public override void OnMessageRecvBack(string ReceiveMessage)
        {
            try
            {
                var message = ReceiveMessage.Replace("\n", "");
                if (message == null) return;
                OnImageGrabedBack(CameraInterfaceCommandType.CommandType_Picture_Grabed, message);
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = ex.Message }, ex);
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

        /// <summary>
        /// 资源释放
        /// </summary>
        public virtual void Dispose()
        {
            Exit = true;
            //取消VisualComputing任务
            base.TokenSource?.Cancel();

            //关闭连接
            base.CloseConnection();
        }

        public void Monitoring()
        {
        }
    }
}