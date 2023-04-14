using DRsoft.Runtime.Core.Platform.Events;
using DRsoft.Runtime.Core.Platform.Logging;
using DRsoft.Runtime.Core.Platform.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DRsoft.Engine.Core.Interface;
using DRsoft.Engine.Core.Internal;
using DRsoft.Engine.Model.Enum;

namespace DRsoft.Engine.Core.Camera
{
    /// <summary>
    /// 相机构建器
    /// </summary>
    public class DefaultCameraBuilder : ICamera, IDisposable
    {
        #region 属性

        protected ILog Log;

        /// <summary>
        /// 相机配置
        /// </summary>
        public CameraConfig CameraConfig;

        /// <summary>
        /// 运行引擎配置事件管理器
        /// </summary>
        protected PubSubEvent<EngineEventArgs>? PubSubEventManager;

        /// <summary>
        /// 视觉接口
        /// </summary>
        protected AbstractCameraVisual VisualDispose;

        /// <summary>
        /// 相机配置事件管理器
        /// </summary>
        protected PubSubEvent<CameraConfig> CameraConfigEventManager;

        private Dictionary<CameraInterfaceCommandType, List<string>> _grabbedResultDic =
            new Dictionary<CameraInterfaceCommandType, List<string>>();

        /// <summary>
        /// 相机配置变更事件
        /// </summary>
        public event EventHandler<PlatformException> OnErrored;

        public string Scope => "DefaultCameraBuilder";

        static ReaderWriterLockSlim _grabResultWriteLock = new ReaderWriterLockSlim();
        static ReaderWriterLockSlim _ipResultWriteLock = new ReaderWriterLockSlim();
        static bool FirstStart = true;

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="config"></param>
        /// <param name="visualDispose"></param>
        public DefaultCameraBuilder(IEventAggregator aggregator, AbstractCameraVisual visualDispose)
        {
            VisualDispose = visualDispose;
            Log = LogProvider.GetLogger(GetType());
            PubSubEventManager = aggregator.GetEvent<PubSubEvent<EngineEventArgs>>();
            CameraConfigEventManager = aggregator.GetEvent<PubSubEvent<CameraConfig>>();

            //视觉算法处理回调事件
            if (FirstStart)
            {
                VisualDispose.OnImageGrabedBack += OnImageGrabedBack;
                FirstStart = false;
            }
        }

        public bool IsConnected()
        {
            return VisualDispose.IsConnected;
        }


        /// <summary>
        /// 视觉算法相关初始化
        /// </summary>
        public virtual void Initialize()
        {
            VisualDispose?.InitVisualDispose();
        }

        #region 配置相关

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public bool UpdateConfig(CameraConfig config)
        {
            try
            {
                CameraConfig = config;
                SetVisualConfig();
                return true;
            }
            catch (Exception ex)
            {
                Log.ErrorException("CameraConfig 更新异常", ex);
                return false;
            }
        }

        #endregion

        public void OnImageGrabedBack(CameraInterfaceCommandType info, string GrabedBufferResult)
        {
            try
            {
                _grabResultWriteLock.EnterWriteLock();
                if (_grabbedResultDic.ContainsKey(info))
                {
                    _grabbedResultDic[info].Add(GrabedBufferResult);
                }
                else
                {
                    _grabbedResultDic.Add(info, new List<string> { GrabedBufferResult });
                }

                if (info == CameraInterfaceCommandType.CommandType_Picture_Grabed)
                {
                    Log.Info($"AOI相机图像采集完成: Time{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
                    //PubSubEventManager?.Publish(new EngineEventArgs { Handle = DRSoftEventDefine.EVENT_AOI_PICGrabed });
                }
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = "OnImageGrabedBack 发生异常" }, ex);
            }
            finally
            {
                _grabResultWriteLock.ExitWriteLock();
            }
        }

        public bool GetAOIGrabbedResult(out string result)
        {
            return GetGrabbedResult(CameraInterfaceCommandType.CommandType_Picture_Grabed, out result);
        }

        /// <summary>
        /// 获取采集结果
        /// </summary>
        /// <param name="info"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private bool GetGrabbedResult(CameraInterfaceCommandType info, out string result)
        {
            result = string.Empty;
            try
            {
                _grabResultWriteLock.EnterReadLock();
                if (_grabbedResultDic.ContainsKey(info))
                {
                    if (_grabbedResultDic[info].Count > 0)
                    {
                        result = _grabbedResultDic[info].First();
                        _grabbedResultDic[info].RemoveAt(0);
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = "GetGrabbedResult 发生异常" }, ex);
                return false;
            }
            finally
            {
                _grabResultWriteLock.ExitReadLock();
            }
        }

        /// <summary>
        /// 触发异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        protected virtual void TriggerException(ErrorInfo errorInfo, System.Exception ex)
        {
            if (errorInfo.Level == ErrorLevel.Interrupt)
                Log.ErrorException(errorInfo.Message, ex);
            else
                Log.WarnException(errorInfo.Message, ex);

            OnErrored?.Invoke(this, new PlatformException(errorInfo, ex));
        }

        #region Test

        public void SetVisualConfig()
        {
            //VisualDispose.SystemConfig = CameraConfig;
        }

        #endregion

        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            VisualDispose?.Dispose();
        }
    }
}