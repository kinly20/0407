using System;
using System.Threading;
using DRsoft.Engine.Core.Control;
using DRsoft.Engine.Core.Control.AbstractController;
using DRsoft.Engine.Model.Enum;
using DRsoft.Runtime.Core.Platform.Events;
using DRsoft.Runtime.Core.Platform.Exceptions;
using DRsoft.Runtime.Core.Platform.Logging;
using DRsoft.Runtime.Core.Platform.Plc;

namespace DRsoft.Engine.Plugin.Control.Beckhoff.Implementation
{
    /// <summary>
    /// 倍福Controller实现
    /// </summary>
    public partial class BeckhoffController : AbstractController
    {

        public BeckhoffController() : base()
        {
            this.Log = LogProvider.GetLogger(typeof(BeckhoffController).Name);
        }

        ~BeckhoffController()
        {
            base.Dispose();
        }

        /// <summary>
        /// 初始化Controller
        /// </summary>
        /// <param name="plcHandler"></param>
        /// <param name="aggregator"></param>
        /// <param name="config"></param>
        public override void InitializeController(IPlcHandler plcHandler, IEventAggregator aggregator,
            ControllerConfig config)
        {
            // 调用基类初始化方法
            base.InitializeController(plcHandler, aggregator, config);

            ActionReportFeedbackTimer.Elapsed += new System.Timers.ElapsedEventHandler(ActionReportFeedbackCallback);
            ActionReportFeedbackTimer.Interval = 20;
            ActionReportFeedbackTimer.AutoReset = false;
            ActionReportFeedbackTimer.Enabled = false;

            ControllerSwVersion = "Beckhoff-Controller";

            ReadControllerStatus();
        }

        public override void ReadControllerStatus()
        {
            ReadControlLoop();
        }

        public override bool InitializeControllerExt(bool sim)
        {
            //return true;
            bool initSuccessFlag = true;

            int count = 0;
            while (!IsPlcConnected())
            {
                Thread.Sleep(50);
                count++;
                if (count > 3)
                {
                    break;
                }
            }

            ControllerConnectedFlag = IsPlcConnected();
            initSuccessFlag &= ControllerConnectedFlag;
            if (!initSuccessFlag)
            {
                //上传错误信息的事件
                Log.DebugFormat("ConnectAdsPort failed!");
            }

            //if (!sim)
            {
                if (initSuccessFlag)
                {
                    //获取PLC配置参数
                    bool init = true;
                    GetParameterFromConfig(Config, init);

                    CmdParam = Config.ControllerParam;
                    ParaAxisGantry11 = Config.ParaAxisGantry11;
                    ParaAxisGantry12 = Config.ParaAxisGantry12;
                    ParaAxisGantry21 = Config.ParaAxisGantry21;
                    ParaAxisGantry22 = Config.ParaAxisGantry22;
                    ParaAxisAlign11 = Config.ParaAxisAlign11;
                    ParaAxisAlign12 = Config.ParaAxisAlign12;
                    ParaAxisAlign21 = Config.ParaAxisAlign21;
                    ParaAxisAlign22 = Config.ParaAxisAlign22;
                    ParaAxisCamShutter1 = Config.ParaAxisCamShutter1;
                    ParaAxisCamShutter2 = Config.ParaAxisCamShutter2;
                    ParaAxisZ1 = Config.ParaAxisZ1;
                    ParaAxisZ2 = Config.ParaAxisZ2;
                    ParaAxisUwLift = Config.ParaAxisUwLift;
                    ParaAxisUw = Config.ParaAxisUw;
                    ParaAxisRwLift = Config.ParaAxisRwLift;
                    ParaAxisRw = Config.ParaAxisRw;
                    ParaAxisClean = Config.ParaAxisClean;
                    ParaAxisPowerMeter = Config.ParaAxisPowerMeter;
                    ParaAxisUwSteer = Config.ParaAxisUwSteer;
                    ParaAxisPeeling1 = Config.ParaAxisPeeling1;
                    ParaAxisStationABelt = Config.ParaAxisStationABelt;
                    ParaAxisPeeling2 = Config.ParaAxisPeeling2;
                    ParaAxisStationBBelt = Config.ParaAxisStationBBelt;
                    ParaAxisRwSteer = Config.ParaAxisRwSteer;


                    //初始化时，需要向下位机发送的数据
                    for (int i = 1; i <= 54; i++)
                    {
                        WriteToControllerByIndex((ControllerInputIndex)i);
                    }
                }
                else
                {
                    //上传错误信息的事件
                    Log.DebugFormat("WriteToControllerByIndex failed!");
                }
            }

            return initSuccessFlag;
        }


        /// <summary>
        /// 定时读取下位机控制变量，外部定时器调用
        /// </summary>
        public override void ReadControlLoop()
        {
            if (!PlcHander.IsConnected) return;
            Thread.Sleep(1);
            try
            {
                for (int i = 1; i <= 32; i++)
                {
                    //if (i == 1 || i == 26 || i == 29 || i == 30 || i == 31)
                    if (i == 1 || i == 26)
                        continue;
                    Thread.Sleep(2);
                    ReadFromControllerByIndex((ControllerOutputIndex)i);
                }
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = "ReadControlLoop 发生异常" }, ex);
            }
        }

        public override void ReadAlarmListLoop()
        {
            if (!PlcHander.IsConnected) return;
            try
            {
                ReadFromControllerByIndex(ControllerOutputIndex.OUT_INDEX_ALARMLIST_FEEDBACK);
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = "ReadAlarmListControlLoop 发生异常" }, ex);
            }
        }

        public override void UpdateStatus(ControllerOutputIndex updatedObject, bool writeFlag)
        {
            lock (UpdateStatusObject)
            {
                int rc;
                bool resetWait = false, updateStatus = false, trigger;
                string logMsg;

                //UpdateSimulations();

                switch (updatedObject)
                {
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void CommandErrAck()
        {
            if (SysStatus.AutoAlarm)
            {
                CmdCommand.Alarm_Ack = true;
                SendCommandToController(ref CmdCommand.Alarm_Ack);
                string logMsg;
                logMsg = "CommandErrAck()";
                Log.DebugFormat(logMsg);
            }
        }


        /// <summary>
        /// 不是运行状态，就可以执行初始化
        /// </summary>
        public override void CommandInit()
        {
            if (!SysStatus.Running)
            {
                CmdCommand.Home = true;
                SendCommandToController(ref CmdCommand.Home);
                string logMsg;
                logMsg = "CommandInit()";
                Log.DebugFormat(logMsg);
            }
        }

        /// <summary>
        /// 只有初始化完成，才能开启自动化
        /// </summary>
        public override void CommandProduction()
        {
            if (SysStatus.Homed)
            {
                CmdCommand.Start = true;
                SendCommandToController(ref CmdCommand.Start);
                string logMsg;
                logMsg = "CommandProduction()";
                Log.DebugFormat(logMsg);
            }
        }

        public override void SendManualContoller(int index)
        {
            switch (index)
            {
                case 1:
                    CmdCommand.ManualAlignStationA = true;
                    SendCommandToController(ref CmdCommand.ManualAlignStationA);
                    break;
                case 2:
                    CmdCommand.ManualAlignStationB = true;
                    SendCommandToController(ref CmdCommand.ManualAlignStationB);
                    break;
                case 3:
                    CmdCommand.ManualUpStreamToStationA = true;
                    SendCommandToController(ref CmdCommand.ManualUpStreamToStationA);
                    break;
                case 4:
                    CmdCommand.ManualStationAToStationB = true;
                    SendCommandToController(ref CmdCommand.ManualStationAToStationB);
                    break;
                case 5:
                    CmdCommand.ManualStationBToDownStream = true;
                    SendCommandToController(ref CmdCommand.ManualStationBToDownStream);
                    break;
                case 6:
                    break;
                case 7:
                    CmdCommand.ManualLiftUpStationA = true;
                    SendCommandToController(ref CmdCommand.ManualLiftUpStationA);
                    break;
                case 8:
                    CmdCommand.ManualLiftDownStationA = true;
                    SendCommandToController(ref CmdCommand.ManualLiftDownStationA);
                    break;
                case 9:
                    break;
                case 10:
                    CmdCommand.ManualLiftUpStationB = true;
                    SendCommandToController(ref CmdCommand.ManualLiftUpStationB);
                    break;
                case 11:
                    CmdCommand.ManualLiftDownStationB = true;
                    SendCommandToController(ref CmdCommand.ManualLiftDownStationB);
                    break;
                default:
                    break;
            }
        }

        public override void SendCommandToController(ref bool commandFlag)
        {
            lock (SendCommandObject)
            {
                ResetCommand();
                commandFlag = true;
                WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD);
            }
        }


        public void ResetCommand()
        {
            CmdCommand.Start = false;
            CmdCommand.Stop = false;
            CmdCommand.Home = false;
            CmdCommand.Pause = false;
            CmdCommand.Mute = false;
            CmdCommand.Alarm_Ack = false;
            CmdCommand.RepairMode = false;
            CmdCommand.ShiledSafeDoor = false;
            CmdCommand.AllAxis_Disable = false;
            CmdCommand.CaliMode = false;
            CmdCommand.RefusePiece_Upstream = false;
            CmdCommand.Simu_Downstream = false;
            CmdCommand.ShiledCam = false;
            CmdCommand.ShiledMark = false;
            CmdCommand.PowerMeterStart = false;
            CmdCommand.CleanSegment = false;

            CmdCommand.ClearStatistical = false;
            CmdCommand.ManualAlignStationA = false;
            CmdCommand.ManualAlignStationB = false;
            CmdCommand.ManualLiftUpStationA = false;
            CmdCommand.ManualLiftUpStationB = false;
            CmdCommand.ManualLiftDownStationA = false;
            CmdCommand.ManualLiftDownStationB = false;
            CmdCommand.ManualUpStreamToStationA = false;
            CmdCommand.ManualStationAToStationB = false;
            CmdCommand.ManualStationBToDownStream = false;
        }


    }
}