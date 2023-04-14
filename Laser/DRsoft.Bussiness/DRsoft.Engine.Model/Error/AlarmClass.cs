using System.Runtime.InteropServices;

namespace DRsoft.Engine.Model.Error
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class AlarmClass
    {
        [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 201)] [BR()]
        public bool[] AlarmList = new bool[201];
    }
}