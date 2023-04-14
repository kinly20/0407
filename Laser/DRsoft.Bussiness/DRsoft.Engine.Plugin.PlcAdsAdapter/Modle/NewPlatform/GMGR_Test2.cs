using System.Runtime.InteropServices;

namespace DRsoft.Engine.Plugin.PlcAdsAdapter.Modle.NewPlatform
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class GmgrTest2
    {
        [MarshalAs(UnmanagedType.I1)]
        public bool test2;
    }
}
