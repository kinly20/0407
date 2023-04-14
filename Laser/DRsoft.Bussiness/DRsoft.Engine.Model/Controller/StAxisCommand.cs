using System.Runtime.InteropServices;

namespace DRsoft.Engine.Model.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class StAxisCommand
    {
        /// <summary>
        /// 去使能
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Disable;
        /// <summary>
        /// 回零
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool Home;
        /// <summary>
        /// 走相对
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool MoveRel;
        /// <summary>
        /// 走绝对1
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool MoveAbs1;
        /// <summary>
        /// 走绝对2
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool MoveAbs2;
        /// <summary>
        /// 点动+
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool JogFw;
        /// <summary>
        /// 点动-
        /// </summary>
        [MarshalAs(UnmanagedType.I1)] [BR()] public bool JogBw;
    }
}
