using System.Runtime.InteropServices;

namespace DRsoft.Engine.Model.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class PlcToHmi
    {
        /// <summary>
        /// 龙门1拍照请求
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool Gantry1_CameraRequest;
        /// <summary>
        /// 龙门2拍照请求
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool Gantry2_CameraRequest;
        /// <summary>
        /// 龙门1打标请求
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool Gantry1_MarkRequest;
        /// <summary>
        /// 龙门2打标请求
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool Gantry2_MarkRequest;
        /// <summary>
        /// 台面A加工完成
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool StationA_ProcessDone;
        /// <summary>
        /// 台面B加工完成
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool StationB_ProcessDone;
        /// <summary>
        /// 龙门加工台面号
        /// </summary>
        [MarshalAs(UnmanagedType.U2)][BR()] public short GantryProcessStationgNum;
        /// <summary>
        /// 龙门1拍照序号
        /// </summary>
        [MarshalAs(UnmanagedType.U2)][BR()] public short Gantry1CameraLineNum;
        /// <summary>
        /// 龙门1加工序号
        /// </summary>
        [MarshalAs(UnmanagedType.U2)][BR()] public short Gantry1ProcessLineNum;
        /// <summary>
        /// 龙门2拍照序号
        /// </summary>
        [MarshalAs(UnmanagedType.U2)][BR()] public short Gantry2CameraLineNum;
        /// <summary>
        /// 龙门2加工序号
        /// </summary>
        [MarshalAs(UnmanagedType.U2)][BR()] public short Gantry2ProcessLineNum;
        /// <summary>
        /// 龙门1加工完成
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry1ProcessDone;
        /// <summary>
        /// 龙门2加工完成
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry2ProcessDone;
        /// <summary>
        /// 功率测试_振镜号1-12 空闲0
        /// </summary>
        [MarshalAs(UnmanagedType.U2)][BR()] public short iPowerTestUnitNum;
        /// <summary>
        /// 功率测试_单激光号1-3
        /// </summary>
        [MarshalAs(UnmanagedType.U2)][BR()] public short iPowerTestLaserNum;
        /// <summary>
        /// 台面A打齐拍照位置
        /// </summary>
        [MarshalAs(UnmanagedType.R4)][BR()] public float StationA_AlignCamPos;
        /// <summary>
        /// 台面B打齐拍照位置
        /// </summary>
        [MarshalAs(UnmanagedType.R4)][BR()] public float StationB_AlignCamPos;
        /// <summary>
        /// 龙门到位
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool PowerMeterStart;
        /// <summary>
        /// 设备有硅片
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool SystemHaveWafer;
        /// <summary>
        /// 串焊产品编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst =81)] [BR()] public string? GroupId;

    }
}
