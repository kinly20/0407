using DRsoft.Engine.Model.Const;
using DRsoft.Engine.Model.Controller;
using DRsoft.Engine.Model.Error;
using DRsoft.Runtime.Core.Platform.Cache;

namespace DRsoft.Engine.Plugin.PlcAdsAdapter.Map
{
    /// <summary>
    /// 类型转换配置项
    /// </summary>
    public class MapConfigProvider : BaseCacheProvider<MapConfig>
    {
        private StOutput _gfbOutput = new StOutput();
        private StInput _gfbInput = new StInput();

        private StAxis_Status _gfbAxisGantry11 = new StAxis_Status();
        private StAxis_Status _gfbAxisGantry12 = new StAxis_Status();
        private StAxis_Status _gfbAxisGantry21 = new StAxis_Status();
        private StAxis_Status _gfbAxisGantry22 = new StAxis_Status();
        private StAxis_Status _gfbAxisAlign11 = new StAxis_Status();
        private StAxis_Status _gfbAxisAlign12 = new StAxis_Status();
        private StAxis_Status _gfbAxisAlign21 = new StAxis_Status();
        private StAxis_Status _gfbAxisAlign22 = new StAxis_Status();
        private StAxis_Status _gfbAxisCamShutter1 = new StAxis_Status();
        private StAxis_Status _gfbAxisCamShutter2 = new StAxis_Status();
        private StAxis_Status _gfbAxisZ1 = new StAxis_Status();
        private StAxis_Status _gfbAxisZ2 = new StAxis_Status();
        private StAxis_Status _gfbAxisUwLift = new StAxis_Status();
        private StAxis_Status _gfbAxisUw = new StAxis_Status();
        private StAxis_Status _gfbAxisRwLift = new StAxis_Status();
        private StAxis_Status _gfbAxisRw = new StAxis_Status();
        private StAxis_Status _gfbAxisClean = new StAxis_Status();
        private StAxis_Status _gfbAxisPowerMeter = new StAxis_Status();
        private StAxis_Status _gfbAxisUwSteer = new StAxis_Status();
        private StAxis_Status _gfbAxisPeeling1 = new StAxis_Status();
        private StAxis_Status _gfbAxisStationABelt = new StAxis_Status();
        private StAxis_Status _gfbAxisPeeling2 = new StAxis_Status();
        private StAxis_Status _gfbAxisStationBBelt = new StAxis_Status();
        private StAxis_Status _gfbAxisRwSteer = new StAxis_Status();
        
        private AlarmClass _gAlarmlist = new AlarmClass();
        private StCamera _gfbCamera = new StCamera();
        //private StManualCtrlCyl _gfbManualCtrlCyl = new StManualCtrlCyl();

        private StStatus _gSysStatus = new StStatus();
        private PlcToHmi _gPLCToHMI = new PlcToHmi();
        private HmiToPlc _gHMIFromPLC = new HmiToPlc();

        public MapConfigProvider()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Init()
        {
            #region 下发参数

            //base.AddOrUpdate(new MapConfig
            //{
            //    Key = DRSoftInteractiveDefine.GMGR_IO_Input,
            //    ShouldMap = false
            //});
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Param,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command,
                ShouldMap = false
            });

            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisGantry11,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisGantry12,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisGantry21,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisGantry22,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisAlign11,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisAlign12,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisAlign21,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisAlign22,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisCamShutter1,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisCamShutter2,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisZ1,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisZ2,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisUwLift,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisUw,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisRwLift,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisRw,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisClean,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisPowerMeter,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisUwSteer,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisPeeling1,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisStationABelt,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisPeeling2,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisStationBBelt,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_AxisRwSteer,
                ShouldMap = false
            });

            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisGantry11,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisGantry12,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisGantry21,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisGantry22,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisAlign11,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisAlign12,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisAlign21,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisAlign22,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisCamShutter1,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisCamShutter2,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisZ1,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisZ2,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisUwLift,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisUw,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisRwLift,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisRw,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisClean,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisPowerMeter,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisUwSteer,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisPeeling1,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisStationABelt,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisPeeling2,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisStationBBelt,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Parameter_AxisRwSteer,
                ShouldMap = false
            });

            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_Camera,
                ShouldMap = false
            });
            //base.AddOrUpdate(new MapConfig
            //{
            //    Key = DRSoftInteractiveDefine.GMGR_Input_Command_ManualCtrlCyl,
            //    ShouldMap = false
            //});
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_Command_Output,
                ShouldMap = false
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Input_HMIDATA_TOPLC,
                ShouldMap = false
            });
            #endregion

            #region 上传参数

            //base.AddOrUpdate(new MapConfig
            //{
            //    Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_Input,
            //    XwType = _gfbInput.GetType(),
            //    XwFullType = _gfbInput.GetType().ToString(),
            //    ShouldMap = false
            //});
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_Output,
                XwType = _gfbOutput.GetType(),
                XwFullType = _gfbOutput.GetType().ToString(),
                ShouldMap = false,
                EnableNotify = true  //开启变量数值变换监控
            });
            //base.AddOrUpdate(new MapConfig
            //{
            //    Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_Params,
            //    XwType = _gfbParam.GetType(),
            //    XwFullType = _gfbParam.GetType().ToString(),
            //    ShouldMap = false
            //});
            //base.AddOrUpdate(new MapConfig
            //{
            //    Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_Command,
            //    XwType = _gfbCommand.GetType(),
            //    XwFullType = _gfbCommand.GetType().ToString(),
            //    ShouldMap = false
            //});



            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisGantry11,
                XwType = _gfbAxisGantry11.GetType(),
                XwFullType = _gfbAxisGantry11.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisGantry12,
                XwType = _gfbAxisGantry12.GetType(),
                XwFullType = _gfbAxisGantry12.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisGantry21,
                XwType = _gfbAxisGantry21.GetType(),
                XwFullType = _gfbAxisGantry21.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisGantry22,
                XwType = _gfbAxisGantry22.GetType(),
                XwFullType = _gfbAxisGantry22.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisAlign11,
                XwType = _gfbAxisAlign11.GetType(),
                XwFullType = _gfbAxisAlign11.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisAlign12,
                XwType = _gfbAxisAlign12.GetType(),
                XwFullType = _gfbAxisAlign12.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisAlign21,
                XwType = _gfbAxisAlign21.GetType(),
                XwFullType = _gfbAxisAlign21.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisAlign22,
                XwType = _gfbAxisAlign22.GetType(),
                XwFullType = _gfbAxisAlign22.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisCamShutter1,
                XwType = _gfbAxisCamShutter1.GetType(),
                XwFullType = _gfbAxisCamShutter1.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisCamShutter2,
                XwType = _gfbAxisCamShutter2.GetType(),
                XwFullType = _gfbAxisCamShutter2.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisZ1,
                XwType = _gfbAxisZ1.GetType(),
                XwFullType = _gfbAxisZ1.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisZ2,
                XwType = _gfbAxisZ2.GetType(),
                XwFullType = _gfbAxisZ2.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisUwLift,
                XwType = _gfbAxisUwLift.GetType(),
                XwFullType = _gfbAxisUwLift.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisUw,
                XwType = _gfbAxisUw.GetType(),
                XwFullType = _gfbAxisUw.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisRwLift,
                XwType = _gfbAxisRwLift.GetType(),
                XwFullType = _gfbAxisRwLift.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisRw,
                XwType = _gfbAxisRw.GetType(),
                XwFullType = _gfbAxisRw.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisClean,
                XwType = _gfbAxisClean.GetType(),
                XwFullType = _gfbAxisClean.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisPowerMeter,
                XwType = _gfbAxisPowerMeter.GetType(),
                XwFullType = _gfbAxisPowerMeter.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisUwSteer,
                XwType = _gfbAxisUwSteer.GetType(),
                XwFullType = _gfbAxisUwSteer.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisPeeling1,
                XwType = _gfbAxisPeeling1.GetType(),
                XwFullType = _gfbAxisPeeling1.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisStationABelt,
                XwType = _gfbAxisStationABelt.GetType(),
                XwFullType = _gfbAxisStationABelt.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisPeeling2,
                XwType = _gfbAxisPeeling2.GetType(),
                XwFullType = _gfbAxisPeeling2.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisStationBBelt,
                XwType = _gfbAxisStationBBelt.GetType(),
                XwFullType = _gfbAxisStationBBelt.GetType().ToString(),
                ShouldMap = false,
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_AxisRwSteer,
                XwType = _gfbAxisRwSteer.GetType(),
                XwFullType = _gfbAxisRwSteer.GetType().ToString(),
                ShouldMap = false,
            });
            
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_Alarm_List,
                XwType = _gAlarmlist.GetType(),
                XwFullType = _gAlarmlist.GetType().ToString(),
                ShouldMap = false,
                EnableNotify = true //开启变量数值变换监控
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_Camera,
                XwType = _gfbCamera.GetType(),
                XwFullType = _gfbCamera.GetType().ToString(),
                ShouldMap = false
            });
            //base.AddOrUpdate(new MapConfig
            //{
            //    Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_ManualCtrlCyl,
            //    XwType = _gfbManualCtrlCyl.GetType(),
            //    XwFullType = _gfbManualCtrlCyl.GetType().ToString(),
            //    ShouldMap = false               
            //});
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_System_Status,
                XwType = _gSysStatus.GetType(),
                XwFullType = _gSysStatus.GetType().ToString(),
                ShouldMap = false,
                EnableNotify = true //开启变量数值变换监控
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_IO_Input,
                XwType = _gfbInput.GetType(),
                XwFullType = _gfbInput.GetType().ToString(),
                ShouldMap = false,
                EnableNotify = true //开启变量数值变换监控
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_PLCDATA_TOHMI,
                XwType = _gPLCToHMI.GetType(),
                XwFullType = _gPLCToHMI.GetType().ToString(),
                ShouldMap = false,
                EnableNotify = true //开启变量数值变换监控
            });
            base.AddOrUpdate(new MapConfig
            {
                Key = DRSoftInteractiveDefine.GMGR_Output_FeedBack_HMIDATA_FROMHMI,
                XwType = _gHMIFromPLC.GetType(),
                XwFullType = _gHMIFromPLC.GetType().ToString(),
                ShouldMap = false,
                //EnableNotify = true //开启变量数值变换监控
            });
            #endregion
        }
    }
}