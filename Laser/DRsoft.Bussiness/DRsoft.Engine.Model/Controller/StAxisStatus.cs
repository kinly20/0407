using System.Runtime.InteropServices;

namespace DRsoft.Engine.Model.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class StAxisStatus
    {
        /// <summary>
        /// 使能
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)][BR()] public bool Enable { set; get; }
        /// <summary>
        /// 运动中
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)][BR()] public bool Moving { set; get; }
        /// <summary>
        /// 回零中
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)][BR()] public bool Homing { set; get; }
        /// <summary>
        /// 回零完成
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)][BR()] public bool Homed { set; get; }
        /// <summary>
        /// 轴错误
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)][BR()] public bool Error { set; get; }
        /// <summary>
        /// 轴错误ID
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)][BR()] public uint ErrorID { set; get; }
        /// <summary>
        /// 设定位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)][BR()] public float SetPos { set; get; }
        /// <summary>
        /// 实际位置
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)][BR()] public float ActPos { set; get; }
        /// <summary>
        /// 实际速度
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)][BR()] public float ActVelo { set; get; }
        /// <summary>
        /// 龙门轴链接状态
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)][BR()] public bool Coupled { set; get; }
        /// <summary>
        /// 安全距离（龙门）
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)][BR()] public float SafetyDistance;

    }
}
