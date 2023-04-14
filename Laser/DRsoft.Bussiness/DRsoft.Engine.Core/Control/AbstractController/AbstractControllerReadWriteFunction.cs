using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DRsoft.Engine.Core.Interface;
using DRsoft.Engine.Model.Const;
using DRsoft.Engine.Model.Controller;
using DRsoft.Engine.Model.Enum;
using DRsoft.Engine.Model.Error;
using DRsoft.Runtime.Core.Platform.Exceptions;
using DRsoft.Runtime.Core.Platform.Logging;
using DRsoft.Runtime.Core.Platform.Plc;
using DRsoft.Runtime.Core.Platform.Plugin;
using DRsoftProperty = DRsoft.Runtime.Core.Platform;

namespace DRsoft.Engine.Core.Control.AbstractController
{
    public abstract partial class AbstractController : IController, IPlugin, DRsoftProperty.Bind.INotifyPropertyChanged
    {
        ConcurrentDictionary<ControllerOutputIndex, PlcContext> _readDic =
            new ConcurrentDictionary<ControllerOutputIndex, PlcContext>();

        ConcurrentDictionary<ControllerInputIndex, PlcContext> _writeDic =
            new ConcurrentDictionary<ControllerInputIndex, PlcContext>();

        #region Controller--Input参数,向下位机写数据

        public StParam CmdParam = new StParam();
        public StCmd CmdCommand = new StCmd();
        public StAxis_CMD CmdAxisGantry11 = new StAxis_CMD();
        public StAxis_CMD CmdAxisGantry12 = new StAxis_CMD();
        public StAxis_CMD CmdAxisGantry21 = new StAxis_CMD();
        public StAxis_CMD CmdAxisGantry22 = new StAxis_CMD();
        public StAxis_CMD CmdAxisAlign11 = new StAxis_CMD();
        public StAxis_CMD CmdAxisAlign12 = new StAxis_CMD();
        public StAxis_CMD CmdAxisAlign21 = new StAxis_CMD();
        public StAxis_CMD CmdAxisAlign22 = new StAxis_CMD();
        public StAxis_CMD CmdAxisCamShutter1 = new StAxis_CMD();
        public StAxis_CMD CmdAxisCamShutter2 = new StAxis_CMD();
        public StAxis_CMD CmdAxisZ1 = new StAxis_CMD();
        public StAxis_CMD CmdAxisZ2 = new StAxis_CMD();
        public StAxis_CMD CmdAxisUwLift = new StAxis_CMD();
        public StAxis_CMD CmdAxisUw = new StAxis_CMD();
        public StAxis_CMD CmdAxisRwLift = new StAxis_CMD();
        public StAxis_CMD CmdAxisRw = new StAxis_CMD();
        public StAxis_CMD CmdAxisClean = new StAxis_CMD();
        public StAxis_CMD CmdAxisPowerMeter = new StAxis_CMD();
        public StAxis_CMD CmdAxisUwSteer = new StAxis_CMD();
        public StAxis_CMD CmdAxisPeeling1 = new StAxis_CMD();
        public StAxis_CMD CmdAxisStationABelt = new StAxis_CMD();
        public StAxis_CMD CmdAxisPeeling2 = new StAxis_CMD();
        public StAxis_CMD CmdAxisStationBBelt = new StAxis_CMD();
        public StAxis_CMD CmdAxisRwSteer = new StAxis_CMD();

        public StAxis_Par ParaAxisGantry11 = new StAxis_Par();
        public StAxis_Par ParaAxisGantry12 = new StAxis_Par();
        public StAxis_Par ParaAxisGantry21 = new StAxis_Par();
        public StAxis_Par ParaAxisGantry22 = new StAxis_Par();
        public StAxis_Par ParaAxisAlign11 = new StAxis_Par();
        public StAxis_Par ParaAxisAlign12 = new StAxis_Par();
        public StAxis_Par ParaAxisAlign21 = new StAxis_Par();
        public StAxis_Par ParaAxisAlign22 = new StAxis_Par();
        public StAxis_Par ParaAxisCamShutter1 = new StAxis_Par();
        public StAxis_Par ParaAxisCamShutter2 = new StAxis_Par();
        public StAxis_Par ParaAxisZ1 = new StAxis_Par();
        public StAxis_Par ParaAxisZ2 = new StAxis_Par();
        public StAxis_Par ParaAxisUwLift = new StAxis_Par();
        public StAxis_Par ParaAxisUw = new StAxis_Par();
        public StAxis_Par ParaAxisRwLift = new StAxis_Par();
        public StAxis_Par ParaAxisRw = new StAxis_Par();
        public StAxis_Par ParaAxisClean = new StAxis_Par();
        public StAxis_Par ParaAxisPowerMeter = new StAxis_Par();
        public StAxis_Par ParaAxisUwSteer = new StAxis_Par();
        public StAxis_Par ParaAxisPeeling1 = new StAxis_Par();
        public StAxis_Par ParaAxisStationABelt = new StAxis_Par();
        public StAxis_Par ParaAxisPeeling2 = new StAxis_Par();
        public StAxis_Par ParaAxisStationBBelt = new StAxis_Par();
        public StAxis_Par ParaAxisRwSteer = new StAxis_Par();

        public StCamera CmdCamera = new StCamera();
        //public StManualCtrlCyl CmdManualCtrlCyl = new StManualCtrlCyl();

        public StOutput IoOutput_CMD = new StOutput();
        public HmiToPlc HMIDataToPLC = new HmiToPlc();  

        #endregion

        #region Controller--Output参数，从下位机读数据
        public StInput IoInput = new StInput();
        public StOutput IoOutput = new StOutput();
        public StCmd CmdOutput = new StCmd();
        public StAxis_Status StatusAxisGantry11 = new StAxis_Status();   //0
        public StAxis_Status StatusAxisGantry12 = new StAxis_Status();   //1
        public StAxis_Status StatusAxisGantry21 = new StAxis_Status();   //2
        public StAxis_Status StatusAxisGantry22 = new StAxis_Status();   //3
        public StAxis_Status StatusAxisAlign11 = new StAxis_Status();   //4
        public StAxis_Status StatusAxisAlign12 = new StAxis_Status();   //5
        public StAxis_Status StatusAxisAlign21 = new StAxis_Status();   //6
        public StAxis_Status StatusAxisAlign22 = new StAxis_Status();   //7
        public StAxis_Status StatusAxisCamShutter1 = new StAxis_Status();//8
        public StAxis_Status StatusAxisCamShutter2 = new StAxis_Status();//9
        public StAxis_Status StatusAxisZ1 = new StAxis_Status();        //10
        public StAxis_Status StatusAxisZ2 = new StAxis_Status();        //11
        public StAxis_Status StatusAxisUwLift = new StAxis_Status();    //12
        public StAxis_Status StatusAxisUw = new StAxis_Status();        //13
        public StAxis_Status StatusAxisRwLift = new StAxis_Status();    //14
        public StAxis_Status StatusAxisRw = new StAxis_Status();        //15
        public StAxis_Status StatusAxisClean = new StAxis_Status();     //16
        public StAxis_Status StatusAxisPowerMeter = new StAxis_Status();//17
        public StAxis_Status StatusAxisUwSteer = new StAxis_Status();   //18
        public StAxis_Status StatusAxisPeeling1 = new StAxis_Status();  //19
        public StAxis_Status StatusAxisStationABelt = new StAxis_Status();//20
        public StAxis_Status StatusAxisPeeling2 = new StAxis_Status();    //21
        public StAxis_Status StatusAxisStationBBelt = new StAxis_Status();//22
        public StAxis_Status StatusAxisRwSteer = new StAxis_Status();     //23

        public AlarmClass Alarms = new AlarmClass();
        public StCamera FbCamera = new StCamera();
        //public StManualCtrlCyl FbManualCtrlCyl = new StManualCtrlCyl();

        public StStatus SysStatus = new StStatus();

        public PlcToHmi PLCDataToHMI = new PlcToHmi();

        public HmiToPlc HMIDataFromPLC = new HmiToPlc();

        #endregion

        protected static object ReadLockObject = new object();
        protected static object WriteLockObject = new object();

        public List<StAxis_CMD> ListCmds = new List<StAxis_CMD>();
        /// <summary>
        /// 给readDic和writeDic，添加成员
        /// </summary>
        public void AddDic()
        {
            #region 添加writeDic

            PlcContext varInput2 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Param, VarValue = CmdParam };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_PARAMS, varInput2);
            PlcContext varInput3 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command, VarValue = CmdCommand };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD, varInput3);

            PlcContext varInput4 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisGantry11, VarValue = CmdAxisGantry11 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry11AxisControl, varInput4);
            PlcContext varInput5 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisGantry12, VarValue = CmdAxisGantry12 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry12AxisControl, varInput5);
            PlcContext varInput6 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisGantry21, VarValue = CmdAxisGantry21 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry21AxisControl, varInput6);
            PlcContext varInput7 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisGantry22, VarValue = CmdAxisGantry22 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry22AxisControl, varInput7);
            PlcContext varInput8 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisAlign11, VarValue = CmdAxisAlign11 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_Align11AxisControl, varInput8);
            PlcContext varInput9 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisAlign12, VarValue = CmdAxisAlign12 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_Align12AxisControl, varInput9);
            PlcContext varInput10 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisAlign21, VarValue = CmdAxisAlign21 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_Align21AxisControl, varInput10);
            PlcContext varInput11 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisAlign22, VarValue = CmdAxisAlign22 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_Align22AxisControl, varInput11);
            PlcContext varInput12 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisCamShutter1, VarValue = CmdAxisCamShutter1 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_CamShutter1AxisControl, varInput12);
            PlcContext varInput13 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisCamShutter2, VarValue = CmdAxisCamShutter2 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_CamShutter2AxisControl, varInput13);
            PlcContext varInput14 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisZ1, VarValue = CmdAxisZ1 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_Z1AxisControl, varInput14);
            PlcContext varInput15 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisZ2, VarValue = CmdAxisZ2 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_Z2AxisControl, varInput15);
            PlcContext varInput16 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisUwLift, VarValue = CmdAxisUwLift };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_UwLiftAxisControl, varInput16);
            PlcContext varInput17 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisUw, VarValue = CmdAxisUw };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_UwAxisControl, varInput17);
            PlcContext varInput18 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisRwLift, VarValue = CmdAxisRwLift };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_RwLiftAxisControl, varInput18);
            PlcContext varInput19 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisRw, VarValue = CmdAxisRw };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_RwAxisControl, varInput19);
            PlcContext varInput20 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisClean, VarValue = CmdAxisClean };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_CleanAxisControl, varInput20);
            PlcContext varInput21 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisPowerMeter, VarValue = CmdAxisPowerMeter };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_PowerMeterAxisControl, varInput21);
            PlcContext varInput22 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisUwSteer, VarValue = CmdAxisUwSteer };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_UwSteerAxisControl, varInput22);
            PlcContext varInput23 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisPeeling1, VarValue = CmdAxisPeeling1 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_Peeling1AxisControl, varInput23);
            PlcContext varInput24 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisStationABelt, VarValue = CmdAxisStationABelt };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_StationABeltAxisControl, varInput24);
            PlcContext varInput25 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisPeeling2, VarValue = CmdAxisPeeling2 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_Peeling2AxisControl, varInput25);
            PlcContext varInput26 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisStationBBelt, VarValue = CmdAxisStationBBelt };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_StationBBeltAxisControl, varInput26);
            PlcContext varInput27 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_AxisRwSteer, VarValue = CmdAxisRwSteer };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CMD_AXIS_RwSteerAxisControl, varInput27);
            
            PlcContext varInput28 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisGantry11, VarValue = ParaAxisGantry11 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry11AxisControl, varInput28);
            PlcContext varInput29 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisGantry12, VarValue = ParaAxisGantry12 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry12AxisControl, varInput29);
            PlcContext varInput30 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisGantry21, VarValue = ParaAxisGantry21 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry21AxisControl, varInput30);
            PlcContext varInput31 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisGantry22, VarValue = ParaAxisGantry22 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry22AxisControl, varInput31);
            PlcContext varInput32 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisAlign11, VarValue = ParaAxisAlign11 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_Align11AxisControl, varInput32);
            PlcContext varInput33 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisAlign12, VarValue = ParaAxisAlign12 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_Align12AxisControl, varInput33);
            PlcContext varInput34 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisAlign21, VarValue = ParaAxisAlign21 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_Align21AxisControl, varInput34);
            PlcContext varInput35 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisAlign22, VarValue = ParaAxisAlign22 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_Align22AxisControl, varInput35);
            PlcContext varInput36 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisCamShutter1, VarValue = ParaAxisCamShutter1 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_CamShutter1AxisControl, varInput36);
            PlcContext varInput37 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisCamShutter2, VarValue = ParaAxisCamShutter2 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_CamShutter2AxisControl, varInput37);
            PlcContext varInput38 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisZ1, VarValue = ParaAxisZ1 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_Z1AxisControl, varInput38);
            PlcContext varInput39 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisZ2, VarValue = ParaAxisZ2 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_Z2AxisControl, varInput39);
            PlcContext varInput40 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisUwLift, VarValue = ParaAxisUwLift };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_UwLiftAxisControl, varInput40);
            PlcContext varInput41 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisUw, VarValue = ParaAxisUw };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_UwAxisControl, varInput41);
            PlcContext varInput42 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisRwLift, VarValue = ParaAxisRwLift };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_RwLiftAxisControl, varInput42);
            PlcContext varInput43 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisRw, VarValue = ParaAxisRw };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_RwAxisControl, varInput43);
            PlcContext varInput44 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisClean, VarValue = ParaAxisClean };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_CleanAxisControl, varInput44);
            PlcContext varInput45 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisPowerMeter, VarValue = ParaAxisPowerMeter };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_PowerMeterAxisControl, varInput45);
            PlcContext varInput46 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisUwSteer, VarValue = ParaAxisUwSteer };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_UwSteerAxisControl, varInput46);
            PlcContext varInput47 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisPeeling1, VarValue = ParaAxisPeeling1 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_Peeling1AxisControl, varInput47);
            PlcContext varInput48 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisStationABelt, VarValue = ParaAxisStationABelt };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_StationABeltAxisControl, varInput48);
            PlcContext varInput49 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisPeeling2, VarValue = ParaAxisPeeling2 };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_Peeling2AxisControl, varInput49);
            PlcContext varInput50 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisStationBBelt, VarValue = ParaAxisStationBBelt };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_StationBBeltAxisControl, varInput50);
            PlcContext varInput51 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisRwSteer, VarValue = ParaAxisRwSteer };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_Para_AXIS_RwSteerAxisControl, varInput51);

            PlcContext varInput52 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_Camera, VarValue = CmdCamera };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_CAMERA, varInput52);
            //PlcContext varInput53 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_ManualCtrlCyl, VarValue = CmdManualCtrlCyl };
            //_writeDic.TryAdd(ControllerInputIndex.IN_INDEX_MANUALCTRLCYL, varInput53);
            PlcContext varInput54 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_Command_Output, VarValue = IoOutput_CMD };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_OutputIO, varInput54);
            PlcContext varInput55 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Input_HMIDATA_TOPLC, VarValue = HMIDataToPLC };
            _writeDic.TryAdd(ControllerInputIndex.IN_INDEX_HMIData_TOPLC, varInput55);
            #endregion

            #region 添加readDic
            PlcContext varOutput1 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_Output, VarValue = IoOutput };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_IO_OUTPUT, varOutput1);

            PlcContext varOutput2 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisGantry11, VarValue = StatusAxisGantry11 };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Gantry11AxisControl, varOutput2);
            PlcContext varOutput3 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisGantry12, VarValue = StatusAxisGantry12 };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Gantry12AxisControl, varOutput3);
            PlcContext varOutput4 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisGantry21, VarValue = StatusAxisGantry21 };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Gantry21AxisControl, varOutput4);
            PlcContext varOutput5 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisGantry22, VarValue = StatusAxisGantry22 };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Gantry22AxisControl, varOutput5);
            PlcContext varOutput6 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisAlign11, VarValue = StatusAxisAlign11 };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Align11AxisControl, varOutput6);
            PlcContext varOutput7 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisAlign12, VarValue = StatusAxisAlign12 };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Align12AxisControl, varOutput7);
            PlcContext varOutput8 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisAlign21, VarValue = StatusAxisAlign21 };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Align21AxisControl, varOutput8);
            PlcContext varOutput9 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisAlign22, VarValue = StatusAxisAlign22 };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Align22AxisControl, varOutput9);
            PlcContext varOutput10 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisCamShutter1, VarValue = StatusAxisCamShutter1 };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_CamShutter1AxisControl, varOutput10);
            PlcContext varOutput11 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisCamShutter2, VarValue = StatusAxisCamShutter2 };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_CamShutter2AxisControl, varOutput11);
            PlcContext varOutput12 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisZ1, VarValue = StatusAxisZ1 };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Z1AxisControl, varOutput12);
            PlcContext varOutput13 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisZ2, VarValue = StatusAxisZ2 };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Z2AxisControl, varOutput13);
            PlcContext varOutput14 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisUwLift, VarValue = StatusAxisUwLift };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_UwLiftAxisControl, varOutput14);
            PlcContext varOutput15 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisUw, VarValue = StatusAxisUw };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_UwAxisControl, varOutput15);
            PlcContext varOutput16 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisRwLift, VarValue = StatusAxisRwLift };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_RwLiftAxisControl, varOutput16);
            PlcContext varOutput17 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisRw, VarValue = StatusAxisRw };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_RwAxisControl, varOutput17);
            PlcContext varOutput18 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisClean, VarValue = StatusAxisClean };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_CleanAxisControl, varOutput18);
            PlcContext varOutput19 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisPowerMeter, VarValue = StatusAxisPowerMeter };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_PowerMeterAxisControl, varOutput19);
            PlcContext varOutput20 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisUwSteer, VarValue = StatusAxisUwSteer };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_UwSteerAxisControl, varOutput20);
            PlcContext varOutput21 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisPeeling1, VarValue = StatusAxisPeeling1 };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Peeling1AxisControl, varOutput21);
            PlcContext varOutput22 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisStationABelt, VarValue = StatusAxisStationABelt };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_StationABeltAxisControl, varOutput22);
            PlcContext varOutput23 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisPeeling2, VarValue = StatusAxisPeeling2 };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Peeling2AxisControl, varOutput23);
            PlcContext varOutput24 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisStationBBelt, VarValue = StatusAxisStationBBelt };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_StationBBeltAxisControl, varOutput24);
            PlcContext varOutput25 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisRwSteer, VarValue = StatusAxisRwSteer };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_RwSteerAxisControl, varOutput25);

            PlcContext varOutput26 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_Alarm_List, VarValue = Alarms };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_ALARMLIST_FEEDBACK, varOutput26);
            PlcContext varOutput27 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_Camera, VarValue = FbCamera };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_CAMERA_FEEDBACK, varOutput27);
            //PlcContext varOutput28 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_ManualCtrlCyl, VarValue = FbManualCtrlCyl };
            //_readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_MANUALCTRLCYL, varOutput28);
            PlcContext varOutput29 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_System_Status, VarValue = SysStatus };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_STATUS, varOutput29);
            PlcContext varOutput30 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_IO_Input, VarValue = IoInput };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_IO_INPUT, varOutput30);
            PlcContext varOutput31 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_PLCDATA_TOHMI, VarValue = PLCDataToHMI };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_PLCDATA_TOHMI, varOutput31);
            PlcContext varOutput32 = new PlcContext { VarName = DRSoftInteractiveDefine.GMGR_Output_FeedBack_HMIDATA_FROMHMI, VarValue = HMIDataFromPLC };
            _readDic.TryAdd(ControllerOutputIndex.OUT_INDEX_HMIDATA_FROMPLC, varOutput32);
            #endregion

            #region 加载命令集合
            ListCmds.Add(CmdAxisGantry11);
            ListCmds.Add(CmdAxisGantry12);
            ListCmds.Add(CmdAxisGantry21);
            ListCmds.Add(CmdAxisGantry22);
            ListCmds.Add(CmdAxisAlign11);
            ListCmds.Add(CmdAxisAlign12);
            ListCmds.Add(CmdAxisAlign21);
            ListCmds.Add(CmdAxisAlign22);
            ListCmds.Add(CmdAxisCamShutter1);
            ListCmds.Add(CmdAxisCamShutter2);
            ListCmds.Add(CmdAxisZ1);
            ListCmds.Add(CmdAxisZ2);
            ListCmds.Add(CmdAxisUwLift);
            ListCmds.Add(CmdAxisUw);
            ListCmds.Add(CmdAxisRwLift);
            ListCmds.Add(CmdAxisRw);
            ListCmds.Add(CmdAxisClean);
            ListCmds.Add(CmdAxisPowerMeter);
            ListCmds.Add(CmdAxisUwSteer);
            ListCmds.Add(CmdAxisPeeling1);
            ListCmds.Add(CmdAxisStationABelt);
            ListCmds.Add(CmdAxisPeeling2); 
            ListCmds.Add(CmdAxisStationBBelt);
            ListCmds.Add(CmdAxisRwSteer);
            #endregion
        }

        /// <summary>
        /// 通过ControllerOutputIndex从Controller读参数
        /// </summary>
        /// <param name="index"></param>
        public void ReadFromControllerByIndex(ControllerOutputIndex index)
        {
            if (!IsPlcConnected()) return;
            lock (ReadLockObject)
            {
                try
                {
                    switch (index)
                    {
                        case ControllerOutputIndex.OUT_INDEX_IO_OUTPUT:
                            var data1 = PlcHander.Read<StOutput>(_readDic[index]);
                            if (data1 != null)
                            {
                                IoOutput = data1;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Gantry11AxisControl:
                            var data2 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data2 != null)
                            {
                                StatusAxisGantry11 = data2;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Gantry12AxisControl:
                            var data3 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data3 != null)
                            {
                                StatusAxisGantry12 = data3;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Gantry21AxisControl:
                            var data4 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data4 != null)
                            {
                                StatusAxisGantry21 = data4;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Gantry22AxisControl:
                            var data5 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data5 != null)
                            {
                                StatusAxisGantry22 = data5;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Align11AxisControl:
                            var data6 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data6 != null)
                            {
                                StatusAxisAlign11 = data6;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Align12AxisControl:
                            var data7 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data7 != null)
                            {
                                StatusAxisAlign12 = data7;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Align21AxisControl:
                            var data8 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data8 != null)
                            {
                                StatusAxisAlign21 = data8;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Align22AxisControl:
                            var data9 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data9 != null)
                            {
                                StatusAxisAlign22 = data9;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_CamShutter1AxisControl:
                            var data10 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data10 != null)
                            {
                                StatusAxisCamShutter1 = data10;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_CamShutter2AxisControl:
                            var data11 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data11 != null)
                            {
                                StatusAxisCamShutter2 = data11;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Z1AxisControl:
                            var data12 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data12 != null)
                            {
                                StatusAxisZ1 = data12;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Z2AxisControl:
                            var data13 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data13 != null)
                            {
                                StatusAxisZ2 = data13;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_UwLiftAxisControl:
                            var data14 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data14 != null)
                            {
                                StatusAxisUwLift = data14;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_UwAxisControl:
                            var data15 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data15 != null)
                            {
                                StatusAxisUw = data15;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_RwLiftAxisControl:
                            var data16 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data16 != null)
                            {
                                StatusAxisRwLift = data16;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_RwAxisControl:
                            var data17 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data17 != null)
                            {
                                StatusAxisRw = data17;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_CleanAxisControl:
                            var data18 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data18 != null)
                            {
                                StatusAxisClean = data18;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_PowerMeterAxisControl:
                            var data19 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data19 != null)
                            {
                                StatusAxisPowerMeter = data19;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_UwSteerAxisControl:
                            var data20 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data20 != null)
                            {
                                StatusAxisUwSteer = data20;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Peeling1AxisControl:
                            var data21 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data21 != null)
                            {
                                StatusAxisPeeling1 = data21;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_StationABeltAxisControl:
                            var data22 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data22 != null)
                            {
                                StatusAxisStationABelt = data22;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_Peeling2AxisControl:
                            var data23 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data23 != null)
                            {
                                StatusAxisPeeling2 = data23;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_StationBBeltAxisControl:
                            var data24 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data24 != null)
                            {
                                StatusAxisStationBBelt = data24;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS_AXIS_RwSteerAxisControl:
                            var data25 = PlcHander.Read<StAxis_Status>(_readDic[index]);
                            if (data25 != null)
                            {
                                StatusAxisRwSteer = data25;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_ALARMLIST_FEEDBACK:
                            var data26 = PlcHander.Read<AlarmClass>(_readDic[index]);
                            if (data26 != null)
                            {
                                Alarms = data26;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_CAMERA_FEEDBACK:
                            var data27 = PlcHander.Read<StCamera>(_readDic[index]);
                            if (data27 != null)
                            {
                                FbCamera = data27;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        //case ControllerOutputIndex.OUT_INDEX_MANUALCTRLCYL:
                        //    var data28 = PlcHander.Read<StManualCtrlCyl>(_readDic[index]);
                        //    if (data28 != null)
                        //    {
                        //        FbManualCtrlCyl = data28;
                        //    }
                        //    else
                        //    {
                        //        Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                        //    }
                        //    break;
                        case ControllerOutputIndex.OUT_INDEX_STATUS:
                            var data29 = PlcHander.Read<StStatus>(_readDic[index]);
                            if (data29 != null)
                            {
                                SysStatus = data29;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_IO_INPUT:
                            var data30 = PlcHander.Read<StInput>(_readDic[index]);
                            if (data30 != null)
                            {
                                IoInput = data30;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_PLCDATA_TOHMI:
                            var data31 = PlcHander.Read<PlcToHmi>(_readDic[index]);
                            if (data31 != null)
                            {
                                PLCDataToHMI = data31;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        case ControllerOutputIndex.OUT_INDEX_HMIDATA_FROMPLC:
                            var data32 = PlcHander.Read<HmiToPlc>(_readDic[index]);
                            if (data32 != null)
                            {
                                HMIDataFromPLC = data32;
                            }
                            else
                            {
                                Log.DebugFormat(("Read data failed, index:{0}"), (int)index);
                            }
                            break;
                        default:
                            break;
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
        }

        /// <summary>
        /// 通过ControllerInputIndex往Controller写参数
        /// </summary>
        /// <param name="index"></param>
        public void WriteToControllerByIndex(ControllerInputIndex index)
        {
            if (!IsPlcConnected()) return;
            lock (WriteLockObject)
            {
                try
                {
                    switch (index)
                    {
                        //case ControllerInputIndex.IN_INDEX_IO: //1
                        //    UpdateDic(ControllerInputIndex.IN_INDEX_IO, IoInput);
                        //    PlcHander.Write(_writeDic[index]);
                        //    break;
                        case ControllerInputIndex.IN_INDEX_PARAMS: //2
                            UpdateDic(ControllerInputIndex.IN_INDEX_PARAMS, CmdParam);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD: //3
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD, CmdCommand);
                            PlcHander.Write(_writeDic[index]);
                            break;

                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry11AxisControl: //4
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry11AxisControl, CmdAxisGantry11);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry12AxisControl: //5
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry12AxisControl, CmdAxisGantry12);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry21AxisControl: //6
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry21AxisControl, CmdAxisGantry21);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry22AxisControl: //7
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry22AxisControl, CmdAxisGantry22);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_Align11AxisControl: //8
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_Align11AxisControl, CmdAxisAlign11);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_Align12AxisControl: //9
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_Align12AxisControl, CmdAxisAlign12);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_Align21AxisControl: //10
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_Align21AxisControl, CmdAxisAlign21);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_Align22AxisControl: //11
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_Align22AxisControl, CmdAxisAlign22);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_CamShutter1AxisControl: //12
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_CamShutter1AxisControl, CmdAxisCamShutter1);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_CamShutter2AxisControl: //13
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_CamShutter2AxisControl, CmdAxisCamShutter2);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_Z1AxisControl: //14
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_Z1AxisControl, CmdAxisZ1);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_Z2AxisControl: //15
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_Z2AxisControl, CmdAxisZ2);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_UwLiftAxisControl: //16
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_UwLiftAxisControl, CmdAxisUwLift);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_UwAxisControl: //17
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_UwAxisControl, CmdAxisUw);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_RwLiftAxisControl: //18
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_RwLiftAxisControl, CmdAxisRwLift);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_RwAxisControl: //19
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_RwAxisControl, CmdAxisRw);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_CleanAxisControl: //20
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_CleanAxisControl, CmdAxisClean);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_PowerMeterAxisControl: //21
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_PowerMeterAxisControl, CmdAxisPowerMeter);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_UwSteerAxisControl: //22
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_UwSteerAxisControl, CmdAxisUwSteer);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_Peeling1AxisControl: //23
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_Peeling1AxisControl, CmdAxisPeeling1);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_StationABeltAxisControl: //24
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_StationABeltAxisControl, CmdAxisStationABelt);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_Peeling2AxisControl: //25
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_Peeling2AxisControl, CmdAxisPeeling2);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_StationBBeltAxisControl: //26
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_StationBBeltAxisControl, CmdAxisStationBBelt);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_CMD_AXIS_RwSteerAxisControl: //27
                            UpdateDic(ControllerInputIndex.IN_INDEX_CMD_AXIS_RwSteerAxisControl, CmdAxisRwSteer);
                            PlcHander.Write(_writeDic[index]);
                            break;

                        case ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry11AxisControl: //28
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry11AxisControl, ParaAxisGantry11);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry12AxisControl: //29
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry12AxisControl, ParaAxisGantry12);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry21AxisControl: //30
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry21AxisControl, ParaAxisGantry21);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry22AxisControl: //31
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry22AxisControl, ParaAxisGantry22);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_Align11AxisControl: //32
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_Align11AxisControl, ParaAxisAlign11);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_Align12AxisControl: //33
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_Align12AxisControl, ParaAxisAlign12);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_Align21AxisControl: //34
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_Align21AxisControl, ParaAxisAlign21);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_Align22AxisControl: //35
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_Align22AxisControl, ParaAxisAlign22);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_CamShutter1AxisControl: //36
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_CamShutter1AxisControl, ParaAxisCamShutter1);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_CamShutter2AxisControl: //37
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_CamShutter2AxisControl, ParaAxisCamShutter2);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_Z1AxisControl: //38
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_Z1AxisControl, ParaAxisZ1);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_Z2AxisControl: //39
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_Z2AxisControl, ParaAxisZ2);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_UwLiftAxisControl: //40
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_UwLiftAxisControl, ParaAxisUwLift);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_UwAxisControl: //41
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_UwAxisControl, ParaAxisUw);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_RwLiftAxisControl: //42
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_RwLiftAxisControl, ParaAxisRwLift);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_RwAxisControl: //43
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_RwAxisControl, ParaAxisRw);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_CleanAxisControl: //44
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_CleanAxisControl, ParaAxisClean);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_PowerMeterAxisControl: //45
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_PowerMeterAxisControl, ParaAxisPowerMeter);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_UwSteerAxisControl: //46
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_UwSteerAxisControl, ParaAxisUwSteer);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_Peeling1AxisControl: //47
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_Peeling1AxisControl, ParaAxisPeeling1);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_StationABeltAxisControl: //48
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_StationABeltAxisControl, ParaAxisStationABelt);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_Peeling2AxisControl: //49
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_Peeling2AxisControl, ParaAxisPeeling2);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_StationBBeltAxisControl: //50
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_StationBBeltAxisControl, ParaAxisStationBBelt);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        case ControllerInputIndex.IN_INDEX_Para_AXIS_RwSteerAxisControl: //51
                            UpdateDic(ControllerInputIndex.IN_INDEX_Para_AXIS_RwSteerAxisControl, ParaAxisRwSteer);
                            PlcHander.Write(_writeDic[index]);
                            break;


                        case ControllerInputIndex.IN_INDEX_CAMERA: //52
                            UpdateDic(ControllerInputIndex.IN_INDEX_CAMERA, CmdCamera);
                            PlcHander.Write(_writeDic[index]);
                            break;
                        //case ControllerInputIndex.IN_INDEX_MANUALCTRLCYL: //53  
                        //    UpdateDic(ControllerInputIndex.IN_INDEX_MANUALCTRLCYL, CmdManualCtrlCyl);
                        //    PlcHander.Write(_writeDic[index]); //
                        //    break;
                        case ControllerInputIndex.IN_INDEX_OutputIO: //53  
                            UpdateDic(ControllerInputIndex.IN_INDEX_OutputIO, IoOutput_CMD);
                            PlcHander.Write(_writeDic[index]); //
                            break;
                        case ControllerInputIndex.IN_INDEX_HMIData_TOPLC: //54  
                            UpdateDic(ControllerInputIndex.IN_INDEX_HMIData_TOPLC, HMIDataToPLC);
                            PlcHander.Write(_writeDic[index]); //
                            break;
                        default:
                            break;
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
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void UpdateDic(ControllerInputIndex key, object value)
        {
            if (_writeDic.ContainsKey(key))
            {
                _writeDic[key].VarValue = value;
            }
        }

        public void CommandServiceOperation()
        {
            throw new NotImplementedException();
        }

        public void CommandCalibration(bool on)
        {
            throw new NotImplementedException();
        }

        public void InsertSimulatedWafer()
        {
            throw new NotImplementedException();
        }
    }
}