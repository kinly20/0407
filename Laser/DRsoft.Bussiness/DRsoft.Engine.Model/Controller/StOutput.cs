using System.Runtime.InteropServices;

namespace DRsoft.Engine.Model.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class StOutput
    {
        /// <summary>
        /// 暂停按钮指示灯
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)][BR()] public bool PauseLight { get; set; }

        /// <summary>
        /// 报警消音指示灯
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool MuteLight { get; set; }

        /// <summary>
        /// 三色灯-黄灯
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool yellowLight { get; set; }

        /// <summary>
        /// 三色灯-绿灯
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool greenLight { get; set; }

        /// <summary>
        /// 三色灯-红灯
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool redLight { get; set; }

        /// <summary>
        /// 三色灯-蜂鸣器
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Buzzer { get; set; }

        /// <summary>
        /// 照明灯
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool LedLight { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve1_8 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve1_9 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve1_10 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve1_11 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve1_12 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve1_13 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve1_14 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve1_15 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve1_16 { get; set; }

        /// <summary>
        /// 打标卡1启动(控制龙门1的1-6的打标卡)
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Mark1Start { get; set; }

        /// <summary>
        /// 打标卡7启动(控制龙门2的7-12的打标卡)
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Mark2Start { get; set; }

        /// <summary>
        /// 龙门1光源
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry1Light { get; set; }

        /// <summary>
        /// 龙门2光源
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry2Light { get; set; }

        /// <summary>
        /// 入口流水线电机正转
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool InBeltMotorFw { get; set; }

        /// <summary>
        /// 入口流水线电机反转
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool InBeltMotorBw { get; set; }

        /// <summary>
        /// 中间流水线电机正转
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool MidBeltMotorFw { get; set; }

        /// <summary>
        /// 中间流水线电机反转
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool MidBeltMotorBw { get; set; }

        /// <summary>
        /// 出口流水线电机正转
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool OutBeltMotorFw { get; set; }

        /// <summary>
        /// 出口流水线电机反转
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool OutBeltMotorBw { get; set; }
        /// <summary>
        /// 允许上游进料
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool AllowToUp { get; set; }

        /// <summary>
        /// 故障输出至上游
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool AlmToUp { get; set; }
        /// <summary>
        /// 本机准备好至下游
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool ReadyToDown { get; set; }

        /// <summary>
        /// 故障输出至下游
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool AlmToDown { get; set; }

        /// <summary>
        /// 龙门1激光使能
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry1LaserEnable { get; set; }

        /// <summary>
        /// 龙门2激光使能
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Gantry2LaserEnable { get; set; }

        /// <summary>
        /// 入口压膜气缸1电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool InPressCyl1 { get; set; }

        /// <summary>
        /// 入口压膜气缸2电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool InPressCyl2 { get; set; }

        /// <summary>
        /// 中间压膜气缸电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool MidPressCyl { get; set; }

        /// <summary>
        /// 出口压膜气缸电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool OutPressCyl { get; set; }

        /// <summary>
        /// A侧托盘横向打齐气缸电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_AlignCyl1 { get; set; }

        /// <summary>
        /// A侧托盘入口打齐气缸电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_AlignCyl3 { get; set; }

        /// <summary>
        /// A侧托盘出口打齐气缸电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_AlignCyl4 { get; set; }

        /// <summary>
        /// B侧托盘横向打齐气缸电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_AlignCyl1 { get; set; }

        /// <summary>
        /// B侧托盘入口打齐气缸电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_AlignCyl3 { get; set; }

        /// <summary>
        /// B侧托盘出口打齐气缸电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_AlignCyl4 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve3_11 { get; set; }
        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve3_12 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve3_13 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve3_14 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve3_15 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve3_16 { get; set; }

        /// <summary>
        /// 升降平台A边沿真空发生器1电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_CycleVac1 { get; set; }

        /// <summary>
        /// 升降平台A中间真空发生器2电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_MidVac { get; set; }

        /// <summary>
        /// 升降平台B边沿真空发生器3电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_CycleVac1 { get; set; }

        /// <summary>
        /// 升降平台B中间真空发生器4电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_MidVac { get; set; }

        /// <summary>
        /// A侧真空泵启动
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_VacPumpRun { get; set; }

        /// <summary>
        /// B侧真空泵启动
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_VacPumpRun { get; set; }

        /// <summary>
        /// 升降平台A边沿真空发生器2电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationA_CycleVac2 { get; set; }

        /// <summary>
        /// 升降平台B边沿真空发生器2电磁阀
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool StationB_CycleVac2 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve4_9 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve4_10 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve4_11 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve4_12 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve4_13 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve4_14 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve4_15 { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Reserve4_16 { get; set; }
    }
}