namespace DRsoft.Engine.Model.Enum
{
    public enum ControllerOutputIndex
    {
        OUT_INDEX_IO_OUTPUT = 1, //1
        OUT_INDEX_STATUS_AXIS_Gantry11AxisControl, //2
        OUT_INDEX_STATUS_AXIS_Gantry12AxisControl, //3
        OUT_INDEX_STATUS_AXIS_Gantry21AxisControl, //4
        OUT_INDEX_STATUS_AXIS_Gantry22AxisControl, //5
        OUT_INDEX_STATUS_AXIS_Align11AxisControl, //6
        OUT_INDEX_STATUS_AXIS_Align12AxisControl, //7
        OUT_INDEX_STATUS_AXIS_Align21AxisControl, //8
        OUT_INDEX_STATUS_AXIS_Align22AxisControl, //9
        OUT_INDEX_STATUS_AXIS_CamShutter1AxisControl, //10
        OUT_INDEX_STATUS_AXIS_CamShutter2AxisControl, //11
        OUT_INDEX_STATUS_AXIS_Z1AxisControl, //12
        OUT_INDEX_STATUS_AXIS_Z2AxisControl, //13
        OUT_INDEX_STATUS_AXIS_UwLiftAxisControl, //14
        OUT_INDEX_STATUS_AXIS_UwAxisControl, //15
        OUT_INDEX_STATUS_AXIS_RwLiftAxisControl, //16
        OUT_INDEX_STATUS_AXIS_RwAxisControl, //17
        OUT_INDEX_STATUS_AXIS_CleanAxisControl, //18
        OUT_INDEX_STATUS_AXIS_PowerMeterAxisControl, //19
        OUT_INDEX_STATUS_AXIS_UwSteerAxisControl, //20
        OUT_INDEX_STATUS_AXIS_Peeling1AxisControl, //21
        OUT_INDEX_STATUS_AXIS_StationABeltAxisControl, //22
        OUT_INDEX_STATUS_AXIS_Peeling2AxisControl, //23
        OUT_INDEX_STATUS_AXIS_StationBBeltAxisControl, //24
        OUT_INDEX_STATUS_AXIS_RwSteerAxisControl, //25
        OUT_INDEX_ALARMLIST_FEEDBACK, //26
        OUT_INDEX_CAMERA_FEEDBACK, //27
        OUT_INDEX_MANUALCTRLCYL, //28
        OUT_INDEX_STATUS, //29
        OUT_INDEX_IO_INPUT, //30
        OUT_INDEX_PLCDATA_TOHMI, //31
        OUT_INDEX_HMIDATA_FROMPLC
    }
}