using System;
using DRsoft.Engine.Core.Interface;
using DRsoft.Engine.Core.Internal;
using DRsoft.Engine.Model.Const;
using DRsoft.Engine.Model.Enum;
using DRsoft.Runtime.Core.Platform.Events;
using DRsoft.Runtime.Core.Platform.Exceptions;
using DRsoft.Runtime.Core.Platform.Logging;
using DRsoft.Runtime.Core.Platform.Plc;
using DRsoft.Runtime.Core.Platform.Plugin;
using DRsoftProperty = DRsoft.Runtime.Core.Platform;

namespace DRsoft.Engine.Core.Control.AbstractController
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public abstract partial class AbstractController : IController, IPlugin, DRsoftProperty.Bind.INotifyPropertyChanged
    {
        /// <summary>
        /// 作用域
        /// </summary>
        public virtual string Scope { get; }

        /// <summary>
        /// 控制器配置
        /// </summary>
        protected ControllerConfig Config { get; set; }

        /// <summary>
        /// 日志操作
        /// </summary>
        protected ILog Log { get; set; }

        /// <summary>
        /// PLC操作
        /// </summary>
        public IPlcHandler PlcHander { get; set; }

        /// <summary>
        /// 是否已经初始化
        /// </summary>
        protected bool HasInitializated { get; set; }

        protected bool ServiceOperationDone;
        public string ControllerSwVersion = string.Empty;
        protected int PasteDryTime;

        protected static object SendCommandObject = new object();
        protected bool CommandsBusy;
        protected bool CommandIsWaiting;
        protected string LastErrorMessage;
        protected bool ErrorMessageReported;
        protected bool ErrorMessageReceived;
        protected bool SavedFaultFlag;
        protected bool SavedLowWarningFlag;
        protected bool SavedHighWarningFlag;
        protected bool NeedResetReportFeedback;
        protected bool NeedUpdateActionReportFeedback;
        protected bool PrintStateReported;
        protected bool DelayedWaferlessMode;

        /// <summary>
        /// AM相机接受下位机的TriggerCount
        /// </summary>
        protected int WaitForTrigger;

        protected string CurrentErrorMessage { get; set; } = "";
        protected string CurrentErrorDetails { get; set; } = "";
        protected bool InitializingFaultState;
        protected bool FirstFaultOnInit;
        protected bool InitCommandSent;
        protected bool InitIdleState;
        protected bool Chuck1PositionReported;
        protected bool Chuck2PositionReported;
        protected bool LastPalAlignmentFailed;

        protected double PalFinalX;
        protected double PalFinalY;
        protected double PalFinalT;
        protected bool PalPositionsUpdated;
        protected bool Ready2ForceFnCr2R;

        public static object RefreshPositionsObject = new object();
        public static object UpdateStatusObject = new object();

        protected bool ProductionConfiguration;
        protected bool WaitForPalData { get; set; }
        protected bool WaitForPqTrigger { get; set; }
        protected IEventAggregator Aggregator { get; set; }
        public PubSubEvent<EngineEventArgs> PubSubEventManager = null;
        protected bool CalibrationPositionsUpdated { get; set; }
        protected int CalibrationChuck;
        protected bool Verbose;
        public double DefaultWaferPitch;
        protected int SavedWafersPrintedBeforeFnc;

        protected bool ControllerConnectedFlag { get; set; }

        protected static object FeedbackWriteFeedback = new object();

        private bool alarmchange;

        public bool AlarmChange
        {
            get => alarmchange;
            set
            {
                if (alarmchange != value || alarmchange == true)
                {
                    ReadAlarmListLoop();
                }

                alarmchange = value;
            }
        }

        public AbstractController()
        {
            HasInitializated = false;
        }

        public AbstractController(IPlcHandler plcHandler, IEventAggregator aggregator, ControllerConfig config)
        {
            HasInitializated = false;
            InitializeController(plcHandler, aggregator, config);
        }

        /// <summary>
        /// 初始化控制器
        /// </summary>
        /// <param name="plcHandler"></param>
        /// <param name="aggregator"></param>
        /// <param name="config"></param>
        public virtual void InitializeController(IPlcHandler plcHandler, IEventAggregator aggregator,
            ControllerConfig config)
        {
            try
            {
                //避免重复初始化
                if (HasInitializated) return;

                PlcHander = plcHandler;

                Config = config;
                bool init = true;

                PubSubEventManager = aggregator.GetEvent<PubSubEvent<EngineEventArgs>>();
                ControllerConfigEventManager = aggregator.GetEvent<PubSubEvent<ControllerConfig>>();

                //订阅从Engine下发的控制器配方配置变更事件
                ControllerConfigEventManager.Subscribe(config => { UpdateControllerConfig(config); },
                    filterConfig => EventBroadcastNodeDefine.EngineIdentity == filterConfig.Ower &&
                                    filterConfig.Subscriber.Contains(EventBroadcastNodeDefine
                                        .PluginControllerIdentity));

                //初始化

                ClearActionReports();

                //把读写PLC的变量分类添加到readDic和writeDic，实现读写的统一控制
                AddDic();
                //控制器通知
                ControllerNotify();
                // 初始化完成
                HasInitializated = true;
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = ex.Message }, ex);
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public virtual bool InitializeControllerExt(bool sim)
        {
            return true;
        }

        public bool IsPlcConnected()
        {
            return PlcHander.IsConnected;
        }

        public void ReConnect()
        {
            PlcHander.Initialize();
        }
        /// <summary>
        /// 上报信息
        /// </summary>
        /// <param name="repStr"></param>
        public virtual void GetReport(string repStr)
        {
        }

        /// <summary>
        /// 下发指令到控制器
        /// </summary>
        public virtual void SendCommandToController(ref bool commandFlag)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public virtual void SendManualContoller(int index)
        {
        }

        /// <summary>
        /// 命令初始化
        /// </summary>
        public virtual void CommandInit()
        {
        }

        /// <summary>
        /// 手动命令
        /// </summary>
        public virtual void CommandAdjust(bool ForceOff)
        {
        }

        /// <summary>
        /// 生产命令
        /// </summary>
        public virtual void CommandProduction()
        {
        }

        public virtual void ClearActionReports()
        {
        }

        /// <summary>
        /// Abort命令
        /// </summary>
        /// <param name="reset"></param>
        public virtual void CommandAbort(bool reset)
        {
        }

        /// <summary>
        /// 错误确认指令
        /// </summary>
        public virtual void CommandErrAck()
        {
            try
            {
                CmdCommand.Alarm_Ack = true;
                SendCommandToController(ref CmdCommand.Alarm_Ack);
                if (Verbose)
                {
                    string msg = "Sending command: ErrAck";
                    Log.DebugFormat(msg);
                }
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = ex.Message, Scope = this.Scope }, ex);
            }
        }

        /// <summary>
        /// 调试命令
        /// </summary>
        public virtual void CommandService()
        {
            if (Verbose)
            {
                string msg = "Sending command: Service";
                Log.DebugFormat(msg);
            }

            ClearActionReports();
        }

        /// <summary>
        /// 停止命令
        /// </summary>
        public virtual void CommandStop()
        {
            CmdCommand.Stop = true;
            SendCommandToController(ref CmdCommand.Stop);
            if (Verbose)
            {
                string msg = "Sending command: Stop";
                Log.DebugFormat(msg);
            }
        }

        /// <summary>
        /// 紧急停止命令
        /// </summary>
        public virtual void EmergencyStop()
        {
            if (Verbose)
            {
                string msg = "Sending command: EmergencyStop";
                Log.DebugFormat(msg);
            }
        }

        /// <summary>
        /// 暂停命令
        /// </summary>
        public virtual void CommandPause()
        {
            if (Verbose)
            {
                string msg = "Sending command: Pause";
                Log.DebugFormat(msg);
            }
        }

        /// <summary>
        /// 恢复命令
        /// </summary>
        public virtual void CommandResume()
        {
            if (Verbose)
            {
                string msg = "Sending command: Resume";
                Log.DebugFormat(msg);
            }
        }

        /// <summary>
        /// 开启打印log
        /// </summary>
        public virtual void SetVerbose()
        {
            Verbose = true;
        }

        /// <summary>
        /// 通过命令
        /// </summary>
        public virtual void CommandPassThrough()
        {
        }

        /// <summary>
        /// 错误异常
        /// </summary>
        /// <param name="error"></param>
        public virtual void PcErrorMode(bool error)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="needed"></param>
        public virtual void MaintenanceNeeded(bool needed)
        {
        }


        /// <summary>
        /// 读PLC数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public T ReadPlcData<T>(string name)
        {
            return PlcHander.Read<T>(new PlcContext() { VarName = name });
        }

        /// <summary>
        /// 调试模式操作完毕
        /// </summary>
        /// <returns></returns>
        public virtual bool IsServiceOperationDone()
        {
            return ServiceOperationDone;
        }

        /// <summary>
        /// 版本号
        /// </summary>
        /// <returns></returns>
        public virtual string GetControllerSW_Version()
        {
            return ControllerSwVersion;
        }

        /// <summary>
        /// 相机触发方式（软、硬）事件
        /// </summary>
        /// <param name="triggerEvent"></param>
        public virtual void SetCameraTriggerEvent(int triggerEvent)
        {
        }

        /// <summary>
        /// PTP_ContSetAlignmentSideTriggerEvent
        /// </summary>
        /// <param name="triggerEvent"></param>
        public virtual void SetAlignmentSideTriggerEvent(int triggerEvent)
        {
        }

        /// <summary>
        /// PTP_ContSetPalTriggerEvent
        /// </summary>
        /// <param name="triggerEvent"></param>
        public virtual void SetPalTriggerEvent(int triggerEvent)
        {
        }

        /// <summary>
        /// 设置TiltTarget
        /// </summary>
        /// <param name="tiltTarget"></param>
        /// <param name="delayedSet"></param>
        public virtual void SetTiltTarget(int tiltTarget, bool delayedSet)
        {
        }

        /// <summary>
        /// 设置ScannerTargetPosition
        /// </summary>
        /// <param name="scannerStarts"></param>
        /// <param name="numStarts"></param>
        /// <param name="tiltCounts"></param>
        /// <param name="writeTiltTarget"></param>
        /// <param name="scannerMasterProfile"></param>
        /// <param name="scannerActualProfile"></param>
        /// <param name="profilePointsNum"></param>
        /// <param name="delayedSet"></param>
        public virtual void SetScannerTargetPositions(ref double scannerStarts, int numStarts, out int tiltCounts,
            bool writeTiltTarget, ref int scannerMasterProfile, ref double scannerActualProfile, int profilePointsNum,
            bool delayedSet)
        {
            tiltCounts = 0;
            scannerStarts = 0;
            scannerMasterProfile = 0;
            scannerActualProfile = 0;
        }

        private void CarrierParam_SegmentShiftCorrectionSetChanged(ControllerConfig config)
        {
            config.Reset();
            config.Ower = EventBroadcastNodeDefine.PluginWindowIdentity;
            config.AddSubscriber(EventBroadcastNodeDefine.EngineIdentity);
            config.AddSubscriber(EventBroadcastNodeDefine.WindowIdentity);
            ControllerConfigEventManager?.Publish(config);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual bool IsProductionConfiguration()
        {
            return ProductionConfiguration;
        }

        /// <summary>
        /// 硅片信息
        /// </summary>
        /// <param name="busbars"></param>
        /// <param name="marks"></param>
        public virtual void WaferInfo(ref int busbars, ref bool marks)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TotalLayers"></param>
        public virtual void SetMultiLayerOption(int totalLayers)
        {
        }

        /// <summary>
        /// 设置激光功率
        /// 48V高电平置true
        /// </summary>
        /// <param name="power"></param>
        public void UpdateCommands(ref bool commandFlag, ref bool waitingFlag)
        {
            lock (SendCommandObject)
            {
                //if (ControllerConnectedFlag)
                {
                    try
                    {
                        //ResetCommands(Commands);
                        commandFlag = true;
                        //WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_COMMAND);
                    }
                    catch (Exception ex)
                    {
                        TriggerException(new ErrorInfo { Message = ex.Message, Scope = Scope }, ex);
                    }
                }
            }
        }

        public void UpdateLastErrorMessage(string errorMessage, string detailedErrorMessage)
        {
            LastErrorMessage = errorMessage;
            ErrorMessageReceived = true;
            ErrorMessageReported = false;

            Log.DebugFormat(detailedErrorMessage);
        }

        public bool GetLastErrorMessage(string msg)
        {
            if (ErrorMessageReceived && !ErrorMessageReported)
            {
                msg = LastErrorMessage;
                ErrorMessageReported = true;
                return true;
            }
            else
                return false; // error is still being updated
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="updatedObject"></param>
        /// <param name="writeFlag"></param>
        public virtual void UpdateStatus(ControllerOutputIndex updatedObject, bool writeFlag)
        {
        }

        /// <summary>
        /// 定时读下位机变量值
        /// </summary>
        public abstract void ReadControlLoop();

        public abstract void ReadControllerStatus();

        public abstract void ReadAlarmListLoop();
    }
}