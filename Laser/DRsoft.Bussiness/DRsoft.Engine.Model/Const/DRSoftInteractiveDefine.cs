// ReSharper disable All

using System.Collections.Generic;

namespace DRsoft.Engine.Model.Const
{
    /// <summary>
    /// 
    /// </summary>
    public static class DRSoftInteractiveDefine
    {
        #region 输入参数（下位机），给下位机下发参数

        public static readonly string GMGR_Input_Param                              = "stParam";
        public static readonly string GMGR_Input_Command                            = "stCommand";

        public static readonly string GMGR_Input_Command_AxisGantry11               = "stAxis_Gantry11.Command";
        public static readonly string GMGR_Input_Command_AxisGantry12               = "stAxis_Gantry12.Command";
        public static readonly string GMGR_Input_Command_AxisGantry21               = "stAxis_Gantry21.Command";
        public static readonly string GMGR_Input_Command_AxisGantry22               = "stAxis_Gantry22.Command";
        public static readonly string GMGR_Input_Command_AxisAlign11                = "stAxis_Align11.Command";
        public static readonly string GMGR_Input_Command_AxisAlign12                = "stAxis_Align12.Command";
        public static readonly string GMGR_Input_Command_AxisAlign21                = "stAxis_Align21.Command";
        public static readonly string GMGR_Input_Command_AxisAlign22                = "stAxis_Align22.Command";
        public static readonly string GMGR_Input_Command_AxisCamShutter1            = "stAxis_CamShutter1.Command";
        public static readonly string GMGR_Input_Command_AxisCamShutter2            = "stAxis_CamShutter2.Command";
        public static readonly string GMGR_Input_Command_AxisZ1                     = "stAxis_Z1.Command";
        public static readonly string GMGR_Input_Command_AxisZ2                     = "stAxis_Z2.Command";
        public static readonly string GMGR_Input_Command_AxisUwLift                 = "stAxis_UwLift.Command";
        public static readonly string GMGR_Input_Command_AxisUw                     = "stAxis_Uw.Command";
        public static readonly string GMGR_Input_Command_AxisRwLift                 = "stAxis_RwLift.Command";
        public static readonly string GMGR_Input_Command_AxisRw                     = "stAxis_Rw.Command";
        public static readonly string GMGR_Input_Command_AxisClean                  = "stAxis_Clean.Command";
        public static readonly string GMGR_Input_Command_AxisPowerMeter             = "stAxis_PowerMeter.Command";
        public static readonly string GMGR_Input_Command_AxisUwSteer                = "stAxis_UwSteer.Command";
        public static readonly string GMGR_Input_Command_AxisPeeling1               = "stAxis_Peeling1.Command";
        public static readonly string GMGR_Input_Command_AxisStationABelt           = "stAxis_StationA_Belt.Command";
        public static readonly string GMGR_Input_Command_AxisPeeling2               = "stAxis_Peeling2.Command";
        public static readonly string GMGR_Input_Command_AxisStationBBelt           = "stAxis_StationB_Belt.Command";
        public static readonly string GMGR_Input_Command_AxisRwSteer                = "stAxis_RwSteer.Command";

        public static readonly string GMGR_Input_Parameter_AxisGantry11             = "stAxis_Gantry11.Param";
        public static readonly string GMGR_Input_Parameter_AxisGantry12             = "stAxis_Gantry12.Param";
        public static readonly string GMGR_Input_Parameter_AxisGantry21             = "stAxis_Gantry21.Param";
        public static readonly string GMGR_Input_Parameter_AxisGantry22             = "stAxis_Gantry22.Param";
        public static readonly string GMGR_Input_Parameter_AxisAlign11              = "stAxis_Align11.Param";
        public static readonly string GMGR_Input_Parameter_AxisAlign12              = "stAxis_Align12.Param";
        public static readonly string GMGR_Input_Parameter_AxisAlign21              = "stAxis_Align21.Param";
        public static readonly string GMGR_Input_Parameter_AxisAlign22              = "stAxis_Align22.Param";
        public static readonly string GMGR_Input_Parameter_AxisCamShutter1          = "stAxis_CamShutter1.Param";
        public static readonly string GMGR_Input_Parameter_AxisCamShutter2          = "stAxis_CamShutter2.Param";
        public static readonly string GMGR_Input_Parameter_AxisZ1                   = "stAxis_Z1.Param";
        public static readonly string GMGR_Input_Parameter_AxisZ2                   = "stAxis_Z2.Param";
        public static readonly string GMGR_Input_Parameter_AxisUwLift               = "stAxis_UwLift.Param";
        public static readonly string GMGR_Input_Parameter_AxisUw                   = "stAxis_Uw.Param";
        public static readonly string GMGR_Input_Parameter_AxisRwLift               = "stAxis_RwLift.Param";
        public static readonly string GMGR_Input_Parameter_AxisRw                   = "stAxis_Rw.Param";
        public static readonly string GMGR_Input_Parameter_AxisClean                = "stAxis_Clean.Param";
        public static readonly string GMGR_Input_Parameter_AxisPowerMeter           = "stAxis_PowerMeter.Param";
        public static readonly string GMGR_Input_Parameter_AxisUwSteer              = "stAxis_UwSteer.Param";
        public static readonly string GMGR_Input_Parameter_AxisPeeling1             = "stAxis_Peeling1.Param";
        public static readonly string GMGR_Input_Parameter_AxisStationABelt         = "stAxis_StationA_Belt.Param";
        public static readonly string GMGR_Input_Parameter_AxisPeeling2             = "stAxis_Peeling2.Param";
        public static readonly string GMGR_Input_Parameter_AxisStationBBelt         = "stAxis_StationB_Belt.Param";
        public static readonly string GMGR_Input_Parameter_AxisRwSteer              = "stAxis_RwSteer.Param";

        public static readonly string GMGR_Input_Command_Camera                     = "stCamera";
        //public static readonly string GMGR_Input_Command_ManualCtrlCyl              = "sT_ManualCtrlCyl";
        public static readonly string GMGR_Input_Command_Output                     = "stOutput";
        public static readonly string GMGR_Input_HMIDATA_TOPLC                      = "stHMIToPLC";

        #endregion

        #region 输出信号（下位机），读取下位机数据
        public static readonly string GMGR_Output_FeedBack_Output                   = "stOutput";

        public static readonly string GMGR_Output_FeedBack_AxisGantry11             = "stAxis_Gantry11.Status";
        public static readonly string GMGR_Output_FeedBack_AxisGantry12             = "stAxis_Gantry12.Status";
        public static readonly string GMGR_Output_FeedBack_AxisGantry21             = "stAxis_Gantry21.Status";
        public static readonly string GMGR_Output_FeedBack_AxisGantry22             = "stAxis_Gantry22.Status";
        public static readonly string GMGR_Output_FeedBack_AxisAlign11              = "stAxis_Align11.Status";
        public static readonly string GMGR_Output_FeedBack_AxisAlign12              = "stAxis_Align12.Status";
        public static readonly string GMGR_Output_FeedBack_AxisAlign21              = "stAxis_Align21.Status";
        public static readonly string GMGR_Output_FeedBack_AxisAlign22              = "stAxis_Align22.Status";
        public static readonly string GMGR_Output_FeedBack_AxisCamShutter1          = "stAxis_CamShutter1.Status";
        public static readonly string GMGR_Output_FeedBack_AxisCamShutter2          = "stAxis_CamShutter2.Status";
        public static readonly string GMGR_Output_FeedBack_AxisZ1                   = "stAxis_Z1.Status";
        public static readonly string GMGR_Output_FeedBack_AxisZ2                   = "stAxis_Z2.Status";
        public static readonly string GMGR_Output_FeedBack_AxisUwLift               = "stAxis_UwLift.Status";
        public static readonly string GMGR_Output_FeedBack_AxisUw                   = "stAxis_Uw.Status";
        public static readonly string GMGR_Output_FeedBack_AxisRwLift               = "stAxis_RwLift.Status";
        public static readonly string GMGR_Output_FeedBack_AxisRw                   = "stAxis_Rw.Status";
        public static readonly string GMGR_Output_FeedBack_AxisClean                = "stAxis_Clean.Status";
        public static readonly string GMGR_Output_FeedBack_AxisPowerMeter           = "stAxis_PowerMeter.Status";
        public static readonly string GMGR_Output_FeedBack_AxisUwSteer              = "stAxis_UwSteer.Status";
        public static readonly string GMGR_Output_FeedBack_AxisPeeling1             = "stAxis_Peeling1.Status";
        public static readonly string GMGR_Output_FeedBack_AxisStationABelt         = "stAxis_StationA_Belt.Status";
        public static readonly string GMGR_Output_FeedBack_AxisPeeling2             = "stAxis_Peeling2.Status";
        public static readonly string GMGR_Output_FeedBack_AxisStationBBelt         = "stAxis_StationB_Belt.Status";
        public static readonly string GMGR_Output_FeedBack_AxisRwSteer              = "stAxis_RwSteer.Status";

        public static readonly string GMGR_Output_FeedBack_Alarm_List               = "Alarm";
        public static readonly string GMGR_Output_FeedBack_Camera                   = "stCamera";
        //public static readonly string GMGR_Output_FeedBack_ManualCtrlCyl            = "sT_ManualCtrlCyl";
        public static readonly string GMGR_Output_FeedBack_System_Status            = "stStatus";
        public static readonly string GMGR_Output_FeedBack_IO_Input                 = "stInput";
        public static readonly string GMGR_Output_FeedBack_PLCDATA_TOHMI            = "stPLCToHMI";
        public static readonly string GMGR_Output_FeedBack_HMIDATA_FROMHMI          = "stHMIToPLC";
        #endregion
    }
}