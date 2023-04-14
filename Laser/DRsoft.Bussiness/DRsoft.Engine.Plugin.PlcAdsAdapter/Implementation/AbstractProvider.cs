using System;
using System.Linq;
using TwinCAT.Ads;
using DRsoft.Runtime.Core.Platform.Logging;
using DRsoft.Runtime.Core.Platform.Plc;
using DRsoft.Runtime.Core.Platform.Exceptions;
using System.Collections.Generic;
using DRsoft.Engine.Plugin.PlcAdsAdapter.Map;
using DRsoft.Engine.Plugin.PlcAdsAdapter.Configurations;

namespace DRsoft.Engine.Plugin.PlcAdsAdapter.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractProvider : IPlcHandler, IDisposable
    {
        private bool isConnected;
        protected bool Ready { get; set; }
        protected AdsConfigOption Config { get; set; }
        protected TcAdsClient TcAdsClient { get; set; }
        protected MapperProvider MapperProvider { get; set; }
        protected int CycleTime { get; set; } = 100;
        protected int DelayTime { get; set; }
        public string Scope => "Plugin.PlcAdsAdapter";
        protected ILog Log = null;

        protected static object LockObj = new object();
        public event EventHandler<PlcNotifyArg> OnNotificationed;
        public event EventHandler<PlcEventArg>? OnConnected;
        public event EventHandler<PlcException> OnErrored;

        public bool IsReady => Ready;

        public bool IsConnected => isConnected;

        public bool StartFlag
        {
            get => Ready;
            set => Ready = value;
        }

        public int State
        {
            get
            {
                if (TcAdsClient != null)
                {
                    TcAdsClient.TryReadState(out var stateInfo);
                    return (int)stateInfo.AdsState;
                }

                return 0;
            }
        }

        public string Error
        {
            get
            {
                if (TcAdsClient != null)
                {
                    var error = TcAdsClient.TryReadState(out var stateInfo);
                    return $"{(int)error}";
                }

                return "";
            }
        }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Prefix { get; set; }

        public AbstractProvider(AdsConfigOption config, MapperProvider provider)
        {
            Config = config;
            MapperProvider = provider;
            Log = LogProvider.GetLogger(GetType());
        }

        public void Connect()
        {
            lock (LockObj)
            {
                if (TcAdsClient == null)
                {
                    TcAdsClient = new TcAdsClient();
                    TcAdsClient.AdsNotificationEx -= new AdsNotificationExEventHandler(AdsNotification);
                    TcAdsClient.AdsNotificationEx += new AdsNotificationExEventHandler(AdsNotification);
                }

                try
                {
                    var count = Config.Retry;
                    do
                    {
                        count--;
                        TcAdsClient.Connect(Config.AmsNetId, Config.Port);
                        //读取PLC状态值如果异常表示未连接
                        var stateInfo = TcAdsClient.ReadState();
                        if (stateInfo.AdsState == AdsState.Run && stateInfo.DeviceState == 0 ||
                            stateInfo.AdsState == AdsState.Error) //临时加入Error,仿真环境异常导致.不影响模拟,待测试完删除
                        {
                            isConnected = true;
                        }
                        else
                        {
                            isConnected = false;
                        }
                    } while (!IsConnected && count > 0);
                }
                catch (System.Exception ex)
                {
                    isConnected = false;

                    //PLC连接异常，Level置为ErrorLevel.Interrupt
                    TriggerException(new ErrorInfo
                    {
                        Level = ErrorLevel.Interrupt,
                        Type = ErrorType.ConnectError,
                        Message = "PlcAdsAdapter连接异常！",
                        Scope = Scope
                    }, ex);
                }
            }
        }

        public abstract void Initialize();

        public abstract T Read<T>(PlcContext contex);

        public abstract bool Write(PlcContext context);

        /// <summary>
        /// 初始化各个变量句柄
        /// </summary>
        protected virtual void IniteVariableHandle()
        {
            string handleStr = "";
            try
            {
                Ready = true;
                var handleIndex = 0;
                var isSimulated = Config.Simulated;
                var variableList = MapperProvider.Provider.FindAll();
                variableList.ForEach(variable =>
                {
                    handleIndex++;
                    // 创建变量句柄
                    handleStr = $"{Config.Prefix}{variable.Key}";
                    variable.Handle = !isSimulated ? TcAdsClient.CreateVariableHandle(handleStr) : handleIndex;
                    // 是否启用监听
                    if (variable.EnableNotify)
                    {
                        var customerData = new object();
                        var notifyHandle = !isSimulated
                            ? TcAdsClient.AddDeviceNotificationEx($"{Config.Prefix}{variable.Key}",
                                AdsTransMode.OnChange, CycleTime, DelayTime, customerData, variable.XwType)
                            : handleIndex;
                        variable.NotifyHandle = notifyHandle;
                    }

                    MapperProvider.Provider.AddOrUpdate(variable);
                });
            }
            catch (System.Exception ex)
            {
                Ready = false;

                TriggerException(new ErrorInfo
                {
                    Type = ErrorType.CreateHandleError,
                    Message = "PLC创建变量句柄失败！" + handleStr,
                    Scope = Scope
                }, ex);
            }
        }

        public virtual MapConfig MapConfig(PlcContext context)
        {
            return MapperProvider.Provider.FindByKey(context.VarName);
        }

        public virtual void Dispose()
        {
            if (!IsConnected) return;

            try
            {
                //1、删除句柄
                var mapConfigs = MapperProvider.Provider.FindAll();
                mapConfigs.ForEach(config =>
                {
                    TcAdsClient?.DeleteVariableHandle(config.Handle);
                    if (config.EnableNotify)
                        TcAdsClient?.DeleteDeviceNotification(config.NotifyHandle);
                });

                //2、取消通知事件
                isConnected = false;
                Ready = false;
                OnNotificationed = null;
                if (TcAdsClient != null)
                {
                    TcAdsClient.AdsNotificationEx -= new AdsNotificationExEventHandler(AdsNotification);
                    TcAdsClient.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                Log.ErrorException("PlcAdsAdapter资源释放异常", ex);
            }
        }

        /// <summary>
        /// 监听变量值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void AdsNotification(object sender, AdsNotificationExEventArgs e)
        {
            var mapConfig = MapperProvider.Provider.Find(x => x.NotifyHandle == e.NotificationHandle);

            OnNotificationed?.Invoke(sender, new PlcNotifyArg
            {
                VarName = mapConfig.Key,
                Handle = e.NotificationHandle,
                Value = e.Value,
                Args = e
            });
        }

        /// <summary>
        /// 添加变量监听
        /// </summary>
        /// <param name="variableName"></param>
        public abstract void AddNotification(string variableName);

        /// <summary>
        /// 心跳监控
        /// </summary>
        public virtual void Monitoring()
        {
            // 模拟状态不启用监听
            if (Config.Simulated || !Config.EnableHeartTime)
                return;
            try
            {
                var mapConfig = MapperProvider.Provider.FindAll().FirstOrDefault();
                if (TcAdsClient == null)
                    isConnected = false;
                else
                {
                    if (mapConfig != null)
                    {
                        TcAdsClient.ReadAny(mapConfig.Handle, mapConfig.XwType);
                    }
                    else
                        isConnected = TcAdsClient.IsConnected;
                }

                isConnected = true;
            }
            catch (System.Exception ex)
            {
                if (IsConnected)
                {
                    ErrorInfo errorInfo = new ErrorInfo
                    {
                        Type = ErrorType.ConnectError,
                        Message = "PLC连接失败！"
                    };
                    TriggerException(errorInfo, ex);
                }

                isConnected = false;
            }
        }

        /// <summary>
        /// 触发异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        protected virtual void TriggerException(ErrorInfo errorInfo, System.Exception ex)
        {
            var adsEx = ex as AdsErrorException;
            var plcEx = new PlcException(errorInfo, ex);
            plcEx.ErrorInfo.ErrorCode = $"{adsEx?.ErrorCode}";
            plcEx.ErrorInfo.Scope = Scope;

            OnErrored?.Invoke(this, plcEx);

            Log.ErrorException(errorInfo.Message, ex);
        }

        public abstract Dictionary<string, object> ReadAll();
    }
}