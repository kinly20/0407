using System.Runtime.InteropServices;

namespace DRsoft.Engine.Model.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class StManualCtrlCyl
    {
        /// <summary>
        /// 上料风刀1
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool InAirKnife1;

        /// <summary>
        /// 上料风刀2
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool InAirKnife2;

        /// <summary>
        /// 上料抬升气缸
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool InLift;

        /// <summary>
        /// 下料抬升气缸
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool OutLift;

        /// <summary>
        /// 网板压紧气缸1
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool ScreenPress1;

        /// <summary>
        /// 网板压紧气缸2
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool ScreenPress2;

        /// <summary>
        /// 刮刀气缸
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Scraper;

        /// <summary>
        /// 刮刀比例阀
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool ScraperProper;

        /// <summary>
        /// 负压切换电磁阀1
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool AirSwitchValve1;

        /// <summary>
        /// 负压切换电磁阀2
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool AirSwitchValve2;

        /// <summary>
        /// 负压切换电磁阀3
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool AirSwitchValve3;

        /// <summary>
        /// 负压切换电磁阀4
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool AirSwitchValve4;

        /// <summary>
        /// 负压吸附电磁阀1
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Inhale1;

        /// <summary>
        /// 负压吸附电磁阀2
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Inhale2;

        /// <summary>
        /// 负压吸附电磁阀3
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Inhale3;

        /// <summary>
        /// 负压吸附电磁阀4
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Inhale4;

        /// <summary>
        /// 反吹电磁阀1
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Blow1;

        /// <summary>
        /// 反吹电磁阀2
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Blow2;

        /// <summary>
        /// 反吹电磁阀3
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Blow3;

        /// <summary>
        /// 反吹电磁阀4
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Blow4;

        /// <summary>
        /// 负压切换电磁阀5
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool AirSwitchValve5;

        /// <summary>
        /// 负压切换电磁阀6
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool AirSwitchValve6;

        /// <summary>
        /// 负压吸附电磁阀5
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Inhale5;

        /// <summary>
        /// 负压吸附电磁阀6
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Inhale6;

        /// <summary>
        /// 反吹电磁阀5
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Blow5;

        /// <summary>
        /// 反吹电磁阀6
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Blow6;

        /// <summary>
        /// 预留
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve7;

        /// <summary>
        /// 预留
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve8;

        /// <summary>
        /// 预留
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve9;

        /// <summary>
        /// 预留
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve10;
    }
}