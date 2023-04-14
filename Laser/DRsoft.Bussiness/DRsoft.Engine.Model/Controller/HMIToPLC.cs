using System.Runtime.InteropServices;

namespace DRsoft.Engine.Model.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class HmiToPlc
    {
        /// <summary>
        /// 龙门1拍照完成结果
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool Gantry1_CameraShootDone;
        /// <summary>
        /// 龙门2拍照完成结果
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool Gantry2_CameraShootDone;
        /// <summary>
        /// 龙门1打标结果完成
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool Gantry1_MarkDone;
        /// <summary>
        /// 龙门2打标结果完成
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool Gantry2_MarkDone;
        /// <summary>
        /// 龙门1可以打标
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry1_ReadyToMark;
        /// <summary>
        /// 龙门2可以打标
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry2_ReadyToMark;
        /// <summary>
        /// 单次测功率完成
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool PowerMeterDone;
        /// <summary>
        /// 测功率完成
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool PowerMeterFinish;
        /// <summary>
        /// 功率测试_振镜号1-12 空闲0
        /// </summary>
        [MarshalAs(UnmanagedType.U2)][BR()] public short iPowerTestUnitNum;
        /// <summary>
        /// 台面A打齐偏移下发标志位
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool StationA_AlignFlag;
        /// <summary>
        /// 台面A打齐偏移值
        /// </summary>
        [MarshalAs(UnmanagedType.R4)][BR()] public float StationA_AlignOffset;
        /// <summary>
        /// 台面B打齐偏移下发标志位
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool StationB_AlignFlag;
        /// <summary>
        /// 台面B打齐偏移值
        /// </summary>
        [MarshalAs(UnmanagedType.R4)][BR()] public float StationB_AlignOffset;
        /// <summary>
        /// 膜需要清洗标志位 
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool DirtyNeedClean;
        /// <summary>
        /// 清空功率启动信号
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool ClearPowerMeterStart;
        /// <summary>
        /// 停止进料 
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool NoFeedIn;
        /// <summary>
        /// 更换硅胶膜 
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool ChangeDirtyField;
    }
}
