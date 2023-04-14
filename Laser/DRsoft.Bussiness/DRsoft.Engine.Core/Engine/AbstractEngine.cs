using DRsoft.Engine.Core.Camera;
using DRsoft.Engine.Core.Control.AbstractController;
using DRsoft.Engine.Core.Interface;
using DRsoft.Engine.Core.Internal;
using DRsoft.Engine.Core.Vision.VisionCalibration;
using DRsoft.Engine.Core.Vision.VisionProduction;
using DRsoft.Engine.Model.Error;
using DRsoft.Runtime.Core.Platform.Camera;
using DRsoft.Runtime.Core.Platform.Events;
using DRsoft.Runtime.Core.Platform.Exceptions;
using DRsoft.Runtime.Core.Platform.Ioc;
using DRsoft.Engine.Core.PowerMeter;
using DRsoft.Runtime.Core.Platform.Logging;
using DRsoft.Runtime.Core.Platform.Plc;
using DRsoft.Runtime.Core.Platform.Vision;
using DRsoft.Runtime.Core.DBService.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using DRsoft.Engine.Model.Controller;
using DRsoft.Engine.Model.Enum;
using System.Timers;
using System.Windows;
using DRsoft.Runtime.Core.Platform.Power;

namespace DRsoft.Engine.Core.Engine
{
    /// <summary>
    /// 控制引擎基类
    /// </summary>
    public abstract partial class AbstractEngine : IEngine
    {
        private static CancellationTokenSource? _cancelToken;
        private static CancellationTokenSource? _cancelThreadToken;
        public EngineConfig Config;
        protected AbstractController Controller;
        public IDataBase DataBase;
        protected ILog Log;
        protected TaskFactory EngineTaskFactory;
        protected TaskFactory EngineThreadTaskFactory;
        public Task CurrentEngineTask;
        public Task CurrentTimerTask;
        public Task CurrentControllerTask;
        public IEventAggregator Aggregator;
        protected bool FirstUpdate;
        public AbstractPowerMeterControl PowerMeter;

        protected bool ProductionEnabled;

        protected DefaultCameraBuilder CameraBuilder;
        protected AbstractCameraVisual CameraVisual;
        public AbstractVisionProduction[] VisionProduction;

        /// <summary>
        /// 用于保存下位机上报的异常信息
        /// </summary>
        public bool PcError;

        public bool MaintenanceNeeded;

        public ErrorPlugin ErrorPlugin = new ErrorPlugin();
        public PluginStatus PluginStatus = new PluginStatus();
        protected static bool EngineFirstStart = true;
        protected AbstractVisionCalibration VisionCalibration;

        public SystemStep CurrentSystemStep = new SystemStep();

        public const short StationNullProcess = 0;
        public const short StationAProcess = 1;
        public const short StationBProcess = 2;

        public const short FirstLineProcess = 1;
        public const short LastLineProcess = 6;


        public const short LaserNumber = 6;
        //串焊变量定义


        //打标事件管理器
        protected PubSubEvent<MarkingSendPara> MarkingSendParaEventManager;
        protected PubSubEvent<MarkingRecvPara> MarkingRecvParaEventManager;
        protected PubSubEvent<MarkingRecvStatusFeedback> MarkingRecvStatusFeedbackEventManager;

        protected MarkingRecvPara MarkingRecvPara = new MarkingRecvPara();
        protected MarkingRecvStatusFeedback MarkingRecvStatusFeedback = new MarkingRecvStatusFeedback();
        //视觉生产通讯相关变量

        //消息事件管理器
        protected PubSubEvent<SendMessageToView> SendMessageToViewEventManager;

        public bool Simulate = true;

        public System.Timers.Timer Longmen1CameraShootTimer = new System.Timers.Timer();
        public System.Timers.Timer Longmen2CameraShootTimer = new System.Timers.Timer();

        protected static CancellationTokenSource CancelToken
        {
            get
            {
                if (_cancelToken == null)
                {
                    _cancelToken = new CancellationTokenSource();
                }

                return _cancelToken;
            }
            set => _cancelToken = value;
        }

        protected static CancellationTokenSource CancelThreadToken
        {
            get
            {
                if (_cancelThreadToken == null)
                {
                    _cancelThreadToken = new CancellationTokenSource();
                }

                return _cancelThreadToken;
            }
            set => _cancelThreadToken = value;
        }

        public virtual string Scope => "Engine";

        public event EventHandler<PlatformException> OnErrored;

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public AbstractEngine()
        {
            CurrentSystemStep = SystemStep.None;
            IsRunning = false;
            FirstUpdate = true;
            Log = LogProvider.GetLogger(this.GetType());
            EngineTaskFactory = new TaskFactory(CancelToken.Token);
            EngineThreadTaskFactory = new TaskFactory(CancelThreadToken.Token);
            VisionProduction = new AbstractVisionProduction[RuntimeEnvironment.VisionProduction.Length];
            OnErrored += Plugin_OnErrored;
            AggregateExceptionCatched += new EventHandler<AggregateExceptionArg>(AggregateExceptionProcessed);

            Longmen1CameraShootTimer.Elapsed += new System.Timers.ElapsedEventHandler(Longmen1CameraShootTimeout);
            Longmen1CameraShootTimer.Interval = 10 * 1000;          //30s
            Longmen1CameraShootTimer.AutoReset = false;
            Longmen1CameraShootTimer.Enabled = false;

            Longmen2CameraShootTimer.Elapsed += new System.Timers.ElapsedEventHandler(Longmen2CameraShootTimeout);
            Longmen2CameraShootTimer.Interval = 10 * 1000;          //30s
            Longmen2CameraShootTimer.AutoReset = false;
            Longmen2CameraShootTimer.Enabled = false;
        }

        public void Longmen1CameraShootTimeout(object? sender, ElapsedEventArgs e)
        {
            try
            {
                //TODO 报警提示拍照失败
                Longmen1CameraShootTimer.Enabled = false;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Longmen2CameraShootTimeout(object? sender, ElapsedEventArgs e)
        {
            try
            {
                //TODO 报警提示拍照失败
                Longmen2CameraShootTimer.Enabled = false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="eventAggregator"></param>
        /// <param name="config"></param>
        public AbstractEngine(AbstractController controller, IEventAggregator eventAggregator, EngineConfig config)
            : this()
        {
            Config = config;
            Controller = controller;
            Aggregator = eventAggregator;
            OnErrored += Plugin_OnErrored;
            AggregateExceptionCatched += new EventHandler<AggregateExceptionArg>(AggregateExceptionProcessed);
            //SiemensController.Initialize();
            IsRunning = false;
            BuildEngine(config);
        }

        #region 构建引擎

        /// <summary>
        /// 创建引擎
        /// </summary>
        /// <param name="config"></param>
        public virtual void BuildEngine(EngineConfig config)
        {
            Config = config;
            //1、构建事件聚合器
            BuildPluginAggregator();
            //2、构建相机通讯插件
            BuildPluginVisual();
            //3、构建视觉生产通讯插件
            BuildPluginVisionProduction();
            //4、构建视觉标定通讯插件
            BuildPluginVisionCalibration();
            //5、构建控制器及PLC插件
            BuildPluginController();
            //6、构建数据库插件
            BuildPluginDataBase();
            //7创建功率计插件
            BuildPluginPowerMeterControl();
        }

        /// <summary>
        /// 构建Aggregator插件
        /// </summary>
        protected virtual bool BuildPluginAggregator()
        {
            try
            {
                var iAggregator = ServiceProviderManager.Instance.ServiceProvider.GetService<IEventAggregator>();
                if (iAggregator == null) return false;

                // 注册配置事件管理器
                Aggregator = iAggregator;
                PubSubEventManager = Aggregator.GetEvent<PubSubEvent<EngineEventArgs>>();
                SendMessageToViewEventManager = Aggregator.GetEvent<PubSubEvent<SendMessageToView>>();
                MarkingSendParaEventManager = Aggregator.GetEvent<PubSubEvent<MarkingSendPara>>();
                MarkingRecvParaEventManager = Aggregator.GetEvent<PubSubEvent<MarkingRecvPara>>();
                MarkingRecvParaEventManager.Subscribe(recvpara =>
                {
                    MarkingRecvPara = recvpara;
                });
                MarkingRecvStatusFeedbackEventManager = Aggregator.GetEvent<PubSubEvent<MarkingRecvStatusFeedback>>();
                MarkingRecvStatusFeedbackEventManager.Subscribe(recvstatusfeedback =>
                {
                    MarkingRecvStatusFeedback = recvstatusfeedback; 
                });
                if (EngineFirstStart)
                {
                    RegisterConfigChangedEventManager();
                    EngineFirstStart = false;
                }

                return true;
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = ex.Message, Scope = Scope }, ex);
#if DEBUG
                throw;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// 构建相机通讯插件
        /// </summary>
        protected virtual void BuildPluginVisual()
        {
            try
            {
                var iVisualHandler =
                    ServiceProviderManager.Instance.ServiceProvider.GetService<IVisualHandler>(RuntimeEnvironment
                        .VisualDispose);
                if (iVisualHandler == null || ((AbstractCameraVisual)iVisualHandler) == null)
                    return;
                CameraVisual = (AbstractCameraVisual)iVisualHandler;
                CameraVisual.SetEventAggregator(Aggregator);
                CameraVisual.OnErrored -= Plugin_OnErrored;
                CameraVisual.OnErrored += Plugin_OnErrored;
                CameraBuilder = new DefaultCameraBuilder(Aggregator, CameraVisual)
                {
                    CameraConfig = Config.CameraConfig,
                    //RecipeConfig = Config.CameraRecipeConfig
                };
                CameraBuilder.OnErrored -= Plugin_OnErrored;
                CameraBuilder.OnErrored += Plugin_OnErrored;
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = ex.Message, Scope = Scope }, ex);
#if DEBUG
                throw;
#endif
            }
        }

        /// <summary>
        /// 构建视觉生产通讯插件
        /// </summary>
        protected virtual void BuildPluginVisionProduction()
        {
            try
            {
                for (int i = 0; i < RuntimeEnvironment.VisionProduction.Length; i++)
                {
                    var iVisionProduction =
                        ServiceProviderManager.Instance.ServiceProvider.GetService<IVisionHandler>(RuntimeEnvironment
                            .VisionProduction[i]);
                    if (iVisionProduction == null || ((AbstractVisionProduction)iVisionProduction) == null) continue;
                    VisionProduction[i] = (AbstractVisionProduction)iVisionProduction;
                    VisionProduction[i].SetEventAggregator(Aggregator);
                    VisionProduction[i].OnErrored -= Plugin_OnErrored;
                    VisionProduction[i].OnErrored += Plugin_OnErrored;
                }
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = ex.Message, Scope = Scope }, ex);
#if DEBUG
                throw;
#endif
            }
        }

        /// <summary>
        /// 构建VisionCalibration插件
        /// </summary>
        protected virtual void BuildPluginVisionCalibration()
        {
            try
            {
                var iVisionHandler =
                    ServiceProviderManager.Instance.ServiceProvider.GetService<IVisionHandler>(RuntimeEnvironment
                        .VisionCalibration);
                if (iVisionHandler == null || ((AbstractVisionCalibration)iVisionHandler) == null) return;

                VisionCalibration = (AbstractVisionCalibration)iVisionHandler;
                VisionCalibration.SetEventAggregator(Aggregator);

                VisionCalibration.OnErrored -= Plugin_OnErrored;
                VisionCalibration.OnErrored += Plugin_OnErrored;
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = ex.Message, Scope = Scope }, ex);
#if DEBUG
                throw;
#endif
            }
        }

        /// <summary>
        /// 构建控制器插件
        /// </summary>
        protected virtual void BuildPluginController()
        {
            try
            {
                //获取引擎运行的业务插件
                var iController =
                    ServiceProviderManager.Instance.ServiceProvider.GetService<IController>(RuntimeEnvironment
                        .Controller);
                var iPlcHandler =
                    ServiceProviderManager.Instance.ServiceProvider.GetService<IPlcHandler>(
                        RuntimeEnvironment.PluginPlc);
                if (iController == null || ((AbstractController)iController) == null || iPlcHandler == null) return;

                Controller = (AbstractController)iController;
                Controller.InitializeController(iPlcHandler, Aggregator, Config.ControllerConfig);
                if (Controller.ControllerSwVersion == "Beckhoff-Controller")
                {
                    Controller.InitializeControllerExt(Simulate);
                }

                Controller.OnErrored -= Plugin_OnErrored;
                Controller.OnErrored += Plugin_OnErrored;
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = ex.Message, Scope = Scope }, ex);
#if DEBUG
                throw;
#endif
            }
        }

        /// <summary>
        /// 构建数据库插件
        /// </summary>
        protected virtual void BuildPluginDataBase()
        {
            try
            {
                //获取引擎运行的业务插件
                var iDatabase = ServiceProviderManager.Instance.ServiceProvider.GetService<IDataBase>();
                if (iDatabase == null) return;
                DataBase = iDatabase;
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = ex.Message, Scope = Scope }, ex);
#if DEBUG
                throw;
#endif
            }
        }
        //创建功率计
        protected virtual void BuildPluginPowerMeterControl()
        {
            try
            {
                //获取引擎运行的业务插件
                var iPowerMeter = ServiceProviderManager.Instance.ServiceProvider.GetService<IPowerMeter>();
                if (iPowerMeter == null) return;
                PowerMeter = (AbstractPowerMeterControl)iPowerMeter;
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = ex.Message, Scope = Scope }, ex);
#if DEBUG
                throw;
#endif
            }
        }

        #endregion

        #region 初始化引擎

        public virtual void InitializeEngine(EngineConfig config)
        {
            try
            {
                //重置Token
                ResetToken();
                //AOI相机初始化
                CameraBuilder.UpdateConfig(config.CameraConfig);
                CameraBuilder.Initialize();
                ////控制器初始化：
                //Controller.CommandInit();

                //清掉PC端的异常
                PcError = false;
                MaintenanceNeeded = false;
                Controller.PcErrorMode(PcError);
                Controller.MaintenanceNeeded(MaintenanceNeeded);
                //初始化功率计通讯
                InitialPower();
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = ex.Message, Scope = Scope }, ex);
#if DEBUG
                //throw;
#endif
            }
        }

        public virtual void InitialPower()
        {
            PowerMeter.Initial();
        }
        public virtual void SendInitToController()
        {
            ResetMark();
            //控制器初始化：
            Controller.CommandInit();
        }

        public virtual void SendErrAckToController()
        {
            //控制器报警确认：
            Controller.CommandErrAck();
        }

        public virtual void StartProduction()
        {
            try
            {
                //开启生产：
                ProductionEnabled = true;
                ClearSystemFlag();

                GetAlignOffset(StationAProcess);
                GetAlignOffset(StationBProcess);

                CurrentSystemStep = SystemStep.SendAlignOffset;
                Controller?.CommandProduction();
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = "StopProduction 发生异常!", Scope = this.Scope }, ex);
#if DEBUG
                throw;
#endif
            }
        }

        /// <summary>
        /// 自动化开始前，清理交互标志
        /// </summary>
        public void ClearSystemFlag()
        {
            //清掉与相机的交互变量
            TakePhotoFinished1 = false;
            PadPositionFinished1 = false;
            SilicagelStatusFinished1 = false;
            TakePhotoFinished2 = false;
            PadPositionFinished2 = false;
            SilicagelStatusFinished2 = false;

            Longmen1CameraShootTimer.Stop();
            Longmen2CameraShootTimer.Stop();

            Controller.ReadFromControllerByIndex(ControllerOutputIndex.OUT_INDEX_PLCDATA_TOHMI);

            //清掉与PLC的交互信息
            NoAlignResult1 = false;
            NoAlignResult2 = false;
            Controller.HMIDataToPLC.StationA_AlignFlag = false;
            Controller.HMIDataToPLC.StationB_AlignFlag = false;
            Controller.HMIDataToPLC.Gantry1_CameraShootDone = false;
            Controller.HMIDataToPLC.Gantry2_CameraShootDone = false;
            Controller.HMIDataToPLC.Gantry1_MarkDone = false;
            Controller.HMIDataToPLC.Gantry2_MarkDone = false;
            Controller.HMIDataToPLC.NoFeedIn = false;
            Controller.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);
        }

        public virtual void StopProduction()
        {
            try
            {
                //停止生产：
                ProductionEnabled = false;
                CurrentSystemStep = SystemStep.None;
                Controller?.CommandStop();

            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = "StopProduction 发生异常!", Scope = this.Scope }, ex);
#if DEBUG
                throw;
#endif
            }
        }

        #endregion

        #region 引擎处理业务插件错误及状态

        /// <summary>
        /// 处理业务插件异常，并通知PLC
        /// 根据e.ErrorInfo.Level的级别，通知PLC是否停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Plugin_OnErrored(object? sender, PlatformException e)
        {
            if (e.ErrorInfo.Level == ErrorLevel.Interrupt)
            {
                PcError = true;
                Log.Error(e.ErrorInfo.Message);
            }
            else if (e.ErrorInfo.Level == ErrorLevel.Remind)
            {
                MaintenanceNeeded = true;
                Log.Warn(e.ErrorInfo.Message);
            }
            else if (e.ErrorInfo.Plugin_Status.Alarm)
            {
                ShowPluginError(e.ErrorInfo.Plugin_Status);
            }
        }

        /// <summary>
        /// 界面显示各个组件的状态
        /// </summary>
        /// <param name="pluginStatus"></param>
        public void ShowPluginError(PluginStatus pluginStatus)
        {
            PluginStatus = pluginStatus;
            try
            {
                if (pluginStatus.Alarm)
                {
                    if (pluginStatus.ErrorList[(int)PluginIndex.CalibrationPlugin])
                    {
                        this.ErrorPlugin.CalibrationPlugin = true;
                    }

                    if (pluginStatus.ErrorList[(int)PluginIndex.CameraHikvisionPlugin])
                    {
                        this.ErrorPlugin.CameraHikvisionPlugin = true;
                    }

                    if (pluginStatus.ErrorList[(int)PluginIndex.ControllerBeckhoffPlugin])
                    {
                        this.ErrorPlugin.ControllerBeckhoffPlugin = true;
                    }

                    if (pluginStatus.ErrorList[(int)PluginIndex.PlcAdsAdapterPlugin])
                    {
                        this.ErrorPlugin.PlcAdsAdapterPlugin = true;
                    }

                    if (pluginStatus.ErrorList[(int)PluginIndex.PlcPviAdapterPlugin]) //需要在平台层PluginIndex为Siemens
                    {
                        this.ErrorPlugin.SiemensAdapter = true;
                    }
                }
                else
                {
                    this.ErrorPlugin.CalibrationPlugin = false;
                    this.ErrorPlugin.CameraHikvisionPlugin = false;
                    this.ErrorPlugin.ControllerBeckhoffPlugin = false;
                    this.ErrorPlugin.PlcAdsAdapterPlugin = false;
                    this.ErrorPlugin.SiemensAdapter = false;
                }
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        /// <summary>
        /// 清除组件的异常
        /// </summary>
        public void ClearPluginError()
        {
            try
            {
                PluginStatus.Alarm = false;
                for (int i = 0; i < 10; i++)
                {
                    PluginStatus.ErrorList[i] = false;
                    PluginStatus.Alarm |= false;
                }

                this.ErrorPlugin.CalibrationPlugin = false;
                this.ErrorPlugin.CameraHikvisionPlugin = false;
                this.ErrorPlugin.ControllerBeckhoffPlugin = false;
                this.ErrorPlugin.PlcAdsAdapterPlugin = false;
                this.ErrorPlugin.SiemensAdapter = false;
            }
            catch (Exception ex)
            {
                //throw;
            }
        }

        public bool IsCalibrConnected()
        {
            if (VisionCalibration == null) return false;
            return VisionCalibration.IsConnected;
        }

        public bool IsVisionProductionAConnected()
        {
            if (VisionProduction[0] == null) return false;
            return VisionProduction[0].IsConnected;
        }

        public bool IsVisionProductionBConnected()
        {
            if (VisionProduction[1] == null) return false;
            return VisionProduction[1].IsConnected;
        }

        public bool IsCameraConnected()
        {
            if (CameraBuilder == null) return false;
            return CameraBuilder.IsConnected();
        }

        public bool IsPowerMeterConnected()
        {
            if (PowerMeter == null) return false;
            return PowerMeter.IsOpen;
        }

        #endregion

        /// <summary>
        /// 开启Production模式的入口
        /// </summary>
        /// <param name="nextTubeCreation"></param>
        /// <param name="rollId"></param>
        public virtual void EnableProduction(bool nextTubeCreation, string rollId)
        {
            ProductionEnabled = true;

            string logMsg = "";

            Log.Info(logMsg);
        }

        public virtual void ErrorAclnowledge()
        {
        }

        public virtual void PauseEngine()
        {
            Controller?.CommandPause();
        }

        public virtual void ResumeEngine()
        {
            Controller?.CommandResume();
        }

        public virtual void RunEngine()
        {
            try
            {
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = "引擎运行失败", Scope = Scope }, ex);
#if DEBUG
                throw;
#endif
            }
        }



        public virtual void StopEventLoop()
        {
            try
            {
                if (!CancelToken.IsCancellationRequested || !CancelThreadToken.IsCancellationRequested)
                {
                    CancelToken.Cancel();
                    CancelThreadToken.Cancel();
                    // 等待任务执行完毕
                    AwaitTaskCompleted();
                }

                GC.Collect();
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = "引擎关闭失败!", Scope = this.Scope }, ex);
#if DEBUG
                throw;
#endif
            }
        }

        /// <summary>
        /// 等待任务完成
        /// </summary>
        public virtual void AwaitTaskCompleted()
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, 5);
            if (CurrentEngineTask != null)
                CurrentEngineTask.Wait(ts);
            if (CurrentTimerTask != null)
                CurrentTimerTask.Wait(ts);
        }

        public virtual void Dispose()
        {
            try
            {
                StopProduction();
                StopEventLoop();
                Controller?.Dispose();
                GC.Collect();
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = "引擎回收失败!", Scope = this.Scope }, ex);
#if DEBUG
                throw;
#endif
            }
        }
    }
}