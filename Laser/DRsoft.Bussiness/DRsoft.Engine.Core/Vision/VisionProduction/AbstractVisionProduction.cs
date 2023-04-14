using DRsoft.Engine.Model.Enum;
using DRsoft.Engine.Model.Vision;
using DRsoft.Runtime.Core.Platform.Exceptions;
using DRsoft.Runtime.Core.Platform.Logging;
using DRsoft.Runtime.Core.Platform.Plugin;
using DRsoft.Runtime.Core.Platform.Vision;
using System;
using System.Collections.Generic;

namespace DRsoft.Engine.Core.Vision.VisionProduction
{
    public abstract class AbstractVisionProduction : MessageBase, IVisionHandler, IPlugin, IDisposable
    {
        #region 属性

        public string Send;
        
        public string _receiveMsg;
        public int _lastLongmenCCDPos;
        #endregion

        private LaserPadPosition shootDoneData = new LaserPadPosition();
        private LaserPadPosition padPosData = new LaserPadPosition();
        private LaserPadPosition silicaData = new LaserPadPosition();

        public LaserPadPosition ShootDoneData
        {
            get { return shootDoneData; }
            set { shootDoneData = value; }
        }

        public LaserPadPosition PadPosData
        {
            get { return padPosData; }
            set { padPosData = value; }
        }

        public LaserPadPosition SilicaData
        {
            get { return silicaData; }
            set { silicaData = value; }
        }

        public string ReceiveMsg
        {
            set { _receiveMsg = value; }
            get { return _receiveMsg; }
        }

        private List<string> _cameraDataList=new List<string>();
        public List<string> cameraDataList
        {
            get { return _cameraDataList; }
            set { cameraDataList = value; }
        }
        public abstract string Scope { get; }
        public event EventHandler<PlatformException> OnErrored;

        public AbstractVisionProduction() : base()
        {
        }

        #region 视觉信息交互

        public abstract void SendMsg(string Send);
        public abstract bool CommandAction(VisionProduction_Command Command, SendCommand para);

        #endregion

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

        public abstract bool Initialize();

        public abstract bool StartCommunication();

        public abstract void StopCommunication();
    }
}