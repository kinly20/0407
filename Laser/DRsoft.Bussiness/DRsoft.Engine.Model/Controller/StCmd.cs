using System.Runtime.InteropServices;

namespace DRsoft.Engine.Model.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class StCmd
    {
        /// <summary>
        /// 启动
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Start;

        /// <summary>
        /// 停止
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Stop;

        /// <summary>
        /// 回零
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Home;

        /// <summary>
        /// 暂停
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Pause;

        /// <summary>
        /// 消音
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Mute;

        /// <summary>
        /// 报警清除
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Alarm_Ack;

        /// <summary>
        /// 检修模式
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool RepairMode { get; set; }

        /// <summary>
        /// 屏蔽安全门
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool ShiledSafeDoor;

        /// <summary>
        /// 所有轴去使能
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool AllAxis_Disable;

        /// <summary>
        /// 标定模式
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool CaliMode;

        /// <summary>
        /// 禁止上游进片
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool RefusePiece_Upstream;

        /// <summary>
        /// 模拟下游要片
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Simu_Downstream { get; set; }

        /// <summary>
        /// 屏蔽拍照
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool ShiledCam;

        /// <summary>
        /// 屏蔽打标
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool ShiledMark;

        /// <summary>
        /// 功率计测量开始
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool PowerMeterStart;

        /// <summary>
        /// 清洗膜
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool CleanSegment;

        /// <summary>
        /// 统计清零
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool ClearStatistical;

        /// <summary>
        /// 龙门A打齐
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool ManualAlignStationA;

        /// <summary>
        /// 龙门B打齐
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool ManualAlignStationB;

        /// <summary>
        /// 台面A顶起
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool ManualLiftUpStationA;

        /// <summary>
        /// 台面B顶起
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool ManualLiftUpStationB;

        /// <summary>
        /// 台面A下降
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool ManualLiftDownStationA;

        /// <summary>
        /// 台面B下降
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool ManualLiftDownStationB;

        /// <summary>
        /// 物料进料到A台面
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool ManualUpStreamToStationA;

        /// <summary>
        /// 物料A台面到B台面
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool ManualStationAToStationB;

        /// <summary>
        /// 物料B台面到出料
        /// </summary>
        [MarshalAs(UnmanagedType.I1)][BR()] public bool ManualStationBToDownStream;



    }
}