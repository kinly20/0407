using System.Runtime.InteropServices;

namespace DRsoft.Engine.Model.Controller
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class StCamera
    {
        /// <summary>
        /// (*InCellDataWrite1 [0] 相机拍照完成标志，1代表完成，-1代表未完成 [1] 处理结果，0：无片；1：正常片；2：破片；3：缺角；4：其他异常 [2] A侧X轴电机位置 [3] A侧Y1轴电机位置 [4] A侧Y2轴电机位置
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public double[] InCellDataWrite1 = new double[6];

        /// <summary>
        /// (*InCellDataWrite2 [0] 相机拍照完成标志，2代表完成，-2代表未完成 [1] 处理结果，0：无片；1：正常片；2：破片；3：缺角；4：其他异常 [2] B侧X轴电机位置 [3] B侧Y1轴电机位置 [4] B侧Y2轴电机位置
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public double[] InCellDataWrite2 = new double[6];
    }
}