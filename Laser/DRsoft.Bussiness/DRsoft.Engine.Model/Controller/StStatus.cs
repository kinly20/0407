using System.Runtime.InteropServices;

namespace DRsoft.Engine.Model.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class StStatus
    {
        /// <summary>
        /// 运行中
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Running { get; set; }

        /// <summary>
        /// 停止
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Stop { get; set; }
        /// <summary>
        /// 暂停
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)][BR()] public bool Pause { get; set; }
        /// <summary>
        /// 报警
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)][BR()] public bool AutoAlarm { get; set; }
        /// <summary>
        /// 1秒脉冲
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)][BR()] public bool OneSecend { get; set; }
        /// <summary>
        /// 回零中
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)][BR()] public bool Homing { get; set; }
        /// <summary>
        /// 回零完成
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)][BR()] public bool Homed { get; set; } 
        /// <summary>
        /// 进料计数
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)][BR()] public uint CountInPiece { get; set; }
        /// <summary>
        /// 出料计数
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)][BR()] public uint CountOutPiece { get; set; }
        /// <summary>
        /// 放卷卷径
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)][BR()] public float UW_ActRadius { get; set; }
        /// <summary>
        /// 收卷卷径
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)][BR()] public float RW_ActRadius { get; set; }
        /// <summary>
        /// 打标时间
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)][BR()] public uint CT_Mark { get; set; }
        /// <summary>
        /// 拍照时间
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)][BR()] public uint CT_Cam { get; set; }
        /// <summary>
        /// 加工时间
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)][BR()] public uint CT_Machining { get; set; }
        /// <summary>
        /// 安全门状态
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool SafeDoor { get; set; }
        /// <summary>
        /// 安全光栅
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool SafeGrating { get; set; }
        /// <summary>
        /// 产能片数
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)][BR()] public uint Pps { get; set; }

        /// <summary>
        /// 手动指令完成
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)][BR()] public bool ManualOperationComplete { get; set; }
        /// <summary>
        /// 龙门11反馈力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)] [BR()] public float Gantry11ActTorque { get; set; }
        /// <summary>
        /// 龙门12反馈力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)] [BR()] public float Gantry12ActTorque { get; set; }
        /// <summary>
        /// 龙门21反馈力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)] [BR()] public float Gantry21ActTorque { get; set; }
        /// <summary>
        /// 龙门22反馈力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)] [BR()] public float Gantry22ActTorque { get; set; }
        /// <summary>
        ///龙门11反馈最大力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)] [BR()] public float Gantry11MaxTorque { get; set; }
        /// <summary>
        /// 龙门12反馈最大力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)] [BR()] public float Gantry12MaxTorque { get; set; }
        /// <summary>
        /// 龙门21反馈最大力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)] [BR()] public float Gantry21MaxTorque { get; set; }
        /// <summary>
        /// 龙门22反馈最大力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)] [BR()] public float Gantry22MaxTorque { get; set; }
        /// <summary>
        /// 放卷反馈力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)] [BR()] public float UwActTorque { get; set; }
        /// <summary>
        /// 收卷反馈力矩
        /// </summary>
        [field: MarshalAs(UnmanagedType.R4)] [BR()] public float RwActTorque { get; set; }
        /// <summary>
        /// 龙门1图片位置转换 
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] [BR()] public uint Pictrue_Gantry1Pos { get; set; }
        /// <summary>
        /// 龙门2图片位置转换 
        /// </summary>
        [field: MarshalAs(UnmanagedType.U4)] [BR()] public uint Pictrue_Gantry2Pos { get; set; }

        /// <summary>
        /// 上游到台面A
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Pictrue_UpstreamToStaA { get; set; }
        /// <summary>
        /// 台面A到台面B
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Pictrue_StaAToStaB { get; set; }
        /// <summary>
        /// 台面B到下游
        /// </summary>
        [field: MarshalAs(UnmanagedType.I1)] [BR()] public bool Pictrue_StaBToDownstream { get; set; }
    }
}
