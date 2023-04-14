using System.Runtime.InteropServices;

namespace Engine.Transfer
{
    internal class HardwareUtility
    {
        /// <summary>
        /// ATAPI驱动器相关
        /// </summary>
        public class AtapiDevice
        {
            #region DllImport

            [DllImport("kernel32.dll", SetLastError = true)]
            static extern int CloseHandle(IntPtr hObject);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall,
                SetLastError = true)]
            static extern IntPtr CreateFile(
                string lpFileName,
                uint dwDesiredAccess,
                uint dwShareMode,
                IntPtr lpSecurityAttributes,
                uint dwCreationDisposition,
                uint dwFlagsAndAttributes,
                IntPtr hTemplateFile);

            [DllImport("kernel32.dll")]
            static extern int DeviceIoControl(
                IntPtr hDevice,
                uint dwIoControlCode,
                ref STORAGE_PROPERTY_QUERY lpInBuffer,
                uint nInBufferSize,
                ref STORAGE_DEVICE_DESCRIPTOR lpOutBuffer,
                uint nOutBufferSize,
                ref uint lpBytesReturned,
                [Out] IntPtr lpOverlapped);

            const uint FILE_DEVICE_MASS_STORAGE = 0x0000002d;
            const uint IOCTL_STORAGE_BASE = FILE_DEVICE_MASS_STORAGE;
            const uint GENERIC_READ = 0x80000000;
            const uint GENERIC_WRITE = 0x40000000;
            const uint FILE_SHARE_READ = 0x00000001;
            const uint FILE_SHARE_WRITE = 0x00000002;
            const uint CREATE_NEW = 1;
            const uint OPEN_EXISTING = 3;
            const uint METHOD_BUFFERED = 0;
            const uint FILE_ANY_ACCESS = 0;

            #endregion

            #region GetHddInfo

            /// <summary>
            /// 获得硬盘信息
            /// </summary>
            /// <param name="driveIndex">硬盘序号</param>
            /// <returns>硬盘信息</returns>
            /// <remarks>           
            public static string GetHddInfo(byte driveIndex)
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32Windows:
                        throw new NotSupportedException("Win32Windows is not supported.");
                    case PlatformID.Win32NT:
                        return GetHddInfoNT(driveIndex);
                    case PlatformID.Win32S:
                        throw new NotSupportedException("Win32s is not supported.");
                    case PlatformID.WinCE:
                        throw new NotSupportedException("WinCE is not supported.");
                    default:
                        throw new NotSupportedException("Unknown Platform.");
                }
            }

            #region GetHddInfoNT

            [StructLayout(LayoutKind.Sequential)]
            internal struct STORAGE_PROPERTY_QUERY
            {
                public uint PropertyId;
                public uint QueryType;

                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
                public byte[] AdditionalParameters;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            internal struct STORAGE_DEVICE_DESCRIPTOR
            {
                public uint Version;
                public uint size;
                public byte DeviceType;
                public byte DeviceTypeModifier;
                public byte RemovableMedia;
                public byte CommandQueueing;
                public uint VendorIdOffset;
                public uint ProductIdOffset;
                public uint ProductRevisionOffset;
                public uint SerialNumberOffset;
                public uint BusType;
                public uint RawPropertiesLength;

                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x1000)]
                public byte[] RawDeviceProperties;
            }

            private static uint CTL_CODE(uint DeviceType, uint Function, uint Method, uint Access)
            {
                return DeviceType << 16 | Access << 14 |
                       Function << 2 | Method;
            }

            private static uint IOCTL_STORAGE_QUERY_PROPERTY =
                CTL_CODE(IOCTL_STORAGE_BASE, 0x500, METHOD_BUFFERED, FILE_ANY_ACCESS); // From winioctl.h

            private static string GetHddInfoNT(byte driveIndex)
            {
                STORAGE_PROPERTY_QUERY query = new STORAGE_PROPERTY_QUERY();
                query.PropertyId = 0;
                query.QueryType = 0;
                STORAGE_DEVICE_DESCRIPTOR device = new STORAGE_DEVICE_DESCRIPTOR();
                uint bytesReturned = 0;
                // We start in NT/Win2000
                IntPtr hDevice = CreateFile(
                    string.Format(@"//./PhysicalDrive{0}", driveIndex),
                    GENERIC_READ | GENERIC_WRITE,
                    FILE_SHARE_READ | FILE_SHARE_WRITE,
                    IntPtr.Zero,
                    OPEN_EXISTING,
                    0,
                    IntPtr.Zero);
                if (hDevice == IntPtr.Zero)
                {
                    int lastError = Marshal.GetLastWin32Error();
                    throw new Exception("CreateFile faild.");
                }

                if (0 == DeviceIoControl(
                        hDevice,
                        IOCTL_STORAGE_QUERY_PROPERTY,
                        ref query,
                        (uint)Marshal.SizeOf(query),
                        ref device,
                        (uint)Marshal.SizeOf(device),
                        ref bytesReturned,
                        IntPtr.Zero))
                {
                    CloseHandle(hDevice);
                    throw new Exception(string.Format("Drive {0} may not exists.", driveIndex + 1));
                }

                string SerialNumber = null;
                if (device.SerialNumberOffset > 0)
                {
                    int n = 0;
                    while (device.RawDeviceProperties[device.SerialNumberOffset - 35 + n++] != 0) ;
                    byte[] buf = new byte[n];
                    Array.Copy(device.RawDeviceProperties, device.SerialNumberOffset - 36, buf, 0, n);
                    SerialNumber = Encoding.ASCII.GetString(buf).Trim();
                }

                return SerialNumber;
            }

            #endregion

            private static void ChangeByteOrder(byte[] charArray)
            {
                byte temp;
                for (int i = 0; i < charArray.Length; i += 2)
                {
                    temp = charArray[i];
                    charArray[i] = charArray[i + 1];
                    charArray[i + 1] = temp;
                }
            }

            #endregion
        }
    }
}