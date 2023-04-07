using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace Testdemo.Class
{
    //public class InovancePlcdemo
    //{
        public class InovanceTcp
        {
            public bool IsConnected;

            public StoreByteCategory StoreByteCategory { get; set; }

            public bool IsStirngReverse = true;
            string ip;
            int port;

            TcpClient msender;
            Socket msock;

            public event Action<byte[], byte[], double> RecordDataEvent;

            public InovanceTcp(string ip, int port, bool isStirngReverse, StoreByteCategory storeByteCategory = StoreByteCategory.CDAB)
            {
                this.ip = ip;
                this.port = port;
                this.StoreByteCategory = storeByteCategory;
                this.IsStirngReverse = isStirngReverse;
                IsConnected = false;
            }

            public void DisConnect()
            {
                msender?.Close();
                msock?.Close();
                msock?.Dispose();
                IsConnected = false;
            }

            public void Connect()
            {
                if (!IsConnected)
                {
                    msender = new TcpClient(ip, port);
                    msock = msender.Client;
                    msock.ReceiveTimeout = 3000;
                    IsConnected = true;
                }
            }

            public int WriteValue<T>(int startAddress, T value, out string msg) where T : struct
            {
                byte[] datas = new byte[0];
                if (typeof(T) == typeof(bool))
                {
                    datas = BitConverter.GetBytes(Convert.ToBoolean(value));
                }
                else if (typeof(T) == typeof(sbyte))
                {
                    datas = BitConverter.GetBytes(Convert.ToSByte(value));
                }
                else if (typeof(T) == typeof(byte))
                {
                    datas = BitConverter.GetBytes(Convert.ToByte(value));
                }
                else if (typeof(T) == typeof(short))
                {
                    datas = BitConverter.GetBytes(Convert.ToInt16(value));
                }
                else if (typeof(T) == typeof(ushort))
                {
                    datas = BitConverter.GetBytes(Convert.ToUInt16(value));
                }
                else if (typeof(T) == typeof(int))
                {
                    datas = BitConverter.GetBytes(Convert.ToInt32(value));
                }
                else if (typeof(T) == typeof(uint))
                {
                    datas = BitConverter.GetBytes(Convert.ToUInt32(value));
                }
                else if (typeof(T) == typeof(long))
                {
                    datas = BitConverter.GetBytes(Convert.ToInt64(value));
                }
                else if (typeof(T) == typeof(ulong))
                {
                    datas = BitConverter.GetBytes(Convert.ToUInt64(value));
                }
                else if (typeof(T) == typeof(float))
                {
                    datas = BitConverter.GetBytes(Convert.ToSingle(value));
                }
                else if (typeof(T) == typeof(double))
                {
                    datas = BitConverter.GetBytes(Convert.ToDouble(value));
                }
                else
                {
                    msg = $"类型不支持";
                    return -1;
                }
                byte[] rcvBuffer = new byte[0];
                return SendByte(startAddress, false, 0, ref rcvBuffer, out msg, datas);
            }

            public int WriteValue(int startAddress, byte[] buffer, out string msg)
            {
                byte[] rcvBuffer = new byte[0];
                return SendByte(startAddress, false, 100, ref rcvBuffer, out msg, buffer);
            }

            public int WriteString(int startAddress, int length, string barcode, out string msg)
            {
                if (barcode == null)
                {
                    barcode = string.Empty;
                }
                barcode = barcode.PadRight(length, '\0');
                return WriteValue(startAddress, Encoding.ASCII.GetBytes(barcode), out msg);
            }

            public int WriteLongString(int startAddress, string longstring, out string msg)
            {
                msg = string.Empty;
                if (longstring == null)
                    longstring = string.Empty;
                int cyclecount = 200;
                int maxlength = longstring.Length;
                int pagesize = (maxlength + cyclecount - 1) / cyclecount;
                int errorcode = -1;
                for (int i = 0; i < pagesize; i++)
                {
                    int writelength = cyclecount;
                    if (i == pagesize - 1)
                        writelength = maxlength - i * cyclecount;
                    string segment = longstring.Substring(i * cyclecount, writelength);
                    errorcode = WriteString(startAddress + (i * cyclecount / 2), writelength, segment, out msg);
                    if (errorcode != 0)
                        return errorcode;
                }
                return errorcode;
            }

            public int ReadValue<T>(int startAddress, out T value, out string msg) where T : struct
            {
                value = default(T);
                int length = 0;
                if (typeof(T) == typeof(bool) || typeof(T) == typeof(sbyte) || typeof(T) == typeof(byte))
                {
                    length = 1;
                }
                else if (typeof(T) == typeof(short) || typeof(T) == typeof(ushort))
                {
                    length = 2;
                }
                else if (typeof(T) == typeof(int) || typeof(T) == typeof(uint) || typeof(T) == typeof(float))
                {
                    length = 4;
                }
                else if (typeof(T) == typeof(long) || typeof(T) == typeof(ulong) || typeof(T) == typeof(double))
                {
                    length = 8;
                }
                else
                {
                    msg = $"类型不支持";
                }

                byte[] rcvBuffer = new byte[0];
                int errorCode = SendByte(startAddress, true, length, ref rcvBuffer, out msg);
                if (errorCode != 0)
                    return errorCode;
                if (typeof(T) == typeof(bool))
                    value = (T)(object)Convert.ToBoolean(rcvBuffer[0]);
                else if (typeof(T) == typeof(sbyte))
                    value = (T)(object)(sbyte)(rcvBuffer[0]);
                else if (typeof(T) == typeof(byte))
                    value = (T)(object)rcvBuffer[0];
                else if (typeof(T) == typeof(short))
                    value = (T)(object)BitConverter.ToInt16(rcvBuffer, 0);
                else if (typeof(T) == typeof(ushort))
                    value = (T)(object)BitConverter.ToUInt16(rcvBuffer, 0);
                else if (typeof(T) == typeof(int))
                    value = (T)(object)BitConverter.ToInt32(rcvBuffer, 0);
                else if (typeof(T) == typeof(uint))
                    value = (T)(object)BitConverter.ToUInt32(rcvBuffer, 0);
                else if (typeof(T) == typeof(long))
                    value = (T)(object)BitConverter.ToInt64(rcvBuffer, 0);
                else if (typeof(T) == typeof(ulong))
                    value = (T)(object)BitConverter.ToUInt64(rcvBuffer, 0);
                else if (typeof(T) == typeof(float))
                    value = (T)(object)BitConverter.ToSingle(rcvBuffer, 0);
                else if (typeof(T) == typeof(Double))
                    value = (T)(object)BitConverter.ToDouble(rcvBuffer, 0);
                return 0;
            }

            public int ReadValue(int startAddress, int length, out byte[] values, out string msg)
            {
                values = new byte[length];
                return SendByte(startAddress, true, length, ref values, out msg);
            }

            public int ReadLongString(int startAddress, int length, out string longstring, out string msg)
            {
                msg = string.Empty;
                longstring = string.Empty;
                int cyclecount = 200;

                int pagesize = (length + cyclecount - 1) / cyclecount;
                int errorcode = -1;
                for (int i = 0; i < pagesize; i++)
                {
                    int readlength = cyclecount;
                    if (i == pagesize - 1)
                        readlength = length - i * cyclecount;
                    byte[] values;
                    errorcode = ReadValue(startAddress + (i * cyclecount / 2), readlength, out values, out msg);
                    if (errorcode != 0)
                        return errorcode;
                    longstring += Encoding.ASCII.GetString(values);
                }
                return errorcode;
            }

            private int SendByte(int startAddress, bool isRead, int length, ref byte[] rev, out string msg)
            {
                byte[] datas = new byte[0];
                return SendByte(startAddress, isRead, length, ref rev, out msg, datas);
            }
            private int SendByte(int startAddress, bool isRead, int length, ref byte[] rev, out string msg, byte[] datas)
            {
                msg = string.Empty;
                if (!IsConnected)
                {
                    msg = "链接失败";
                    return 1000;
                }
                if (startAddress < 0 || startAddress > 65535)
                {
                    msg = "地址错误";
                    return 1001;
                }
                if (isRead && (length < 1 || length > 250))
                {
                    msg = "参数非法";
                    return 1002;
                }
                if (!isRead && (datas == null || length < 1 || length > 240))
                {
                    msg = "参数非法";
                    return 1003;
                }
                double lockValue = 0;
                while (Interlocked.Exchange(ref lockValue, 1) != 0)
                {
                }
                byte[] addrArray = BitConverter.GetBytes((ushort)startAddress);
                byte[] SendByte = new byte[12];
                byte registerCount = (byte)((length - 1) / 2);
                if (isRead)
                {
                    SendByte = new byte[12] { 0x02, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x03, addrArray[1], addrArray[0], 0x40, 0x40 };
                }
                else
                {
                    SendByte = GenerateWriteCommand(addrArray, datas);
                }
                System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

                try
                {
                    msock.Send(SendByte, SocketFlags.None);
                }
                catch (SocketException ex)
                {
                    IsConnected = false;
                    msg = "网络异常";
                    Interlocked.Exchange(ref lockValue, 0);
                    return 1004;
                }
                catch (Exception ex)
                {
                    IsConnected = false;
                    msg = "网络异常";
                    Interlocked.Exchange(ref lockValue, 0);
                    return 1005;
                }

                byte[] buffer = new byte[2048];
                int rcvCount = 0;
                try
                {
                    rcvCount = msock.Receive(buffer);
                    RecordDataEvent?.Invoke(SendByte, new ArraySegment<byte>(buffer, 0, rcvCount).ToArray(), stopwatch.ElapsedMilliseconds);
                }
                catch (SocketException ex)
                {
                    IsConnected = false;
                    msg = "网络异常";
                    Interlocked.Exchange(ref lockValue, 0);
                    return 1006;
                }
                catch (Exception ex)
                {
                    IsConnected = false;
                    msg = "网络异常";
                    Interlocked.Exchange(ref lockValue, 0);
                    return 1007;
                }

                if (rcvCount < 9 || buffer[0] != SendByte[0])
                {
                    msg = "网络异常";
                    Interlocked.Exchange(ref lockValue, 0);
                    return 1008;
                }
                if (isRead)
                {
                    int receivelength = buffer[8];
                    if (receivelength != registerCount * 2)
                    {
                        msg = "网络异常";
                        Interlocked.Exchange(ref lockValue, 0);
                        return 1009;
                    }

                    rev = new byte[receivelength];
                    for (int i = 0; i < receivelength; i++)
                    {
                        if (i % 2 == 0)
                            rev[i] = buffer[9 + i + 1];
                        else
                            rev[i] = buffer[9 + i - 1];
                    }
                }
                else
                {
                    if (buffer[7] == 0x10 && rcvCount > 11 && buffer[11] != SendByte[11])
                    {
                        msg = "网络异常";
                        Interlocked.Exchange(ref lockValue, 0);
                        return 1010;
                    }

                }

                Interlocked.Exchange(ref lockValue, 0);
                return 0;
            }

            private byte[] GenerateWriteCommand(byte[] addrArray, byte[] datas)
            {
                byte[] SendByte = new byte[12];
                byte registerCount = (byte)((datas.Length - 1) / 2);
                byte writeByteCount = (byte)(registerCount * 2);
                if (writeByteCount == 1)
                {
                    SendByte = new byte[12] { 0x02, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x03, addrArray[1], addrArray[0], 0x40, 0x40 };
                    if (datas.Length == 1)
                        SendByte[11] = datas[0];
                    else
                    {
                        SendByte[10] = datas[1];
                        SendByte[11] = datas[0];
                    }
                    return SendByte;
                }


                SendByte = new byte[13 + writeByteCount];
                SendByte[0] = 0x02;
                SendByte[1] = 0x01;
                SendByte[5] = (byte)(7 + writeByteCount);
                SendByte[6] = 0x01;
                SendByte[7] = 0x10;
                SendByte[8] = addrArray[1];
                SendByte[9] = addrArray[0];
                SendByte[11] = registerCount;
                SendByte[12] = writeByteCount;

                for (int i = 0; i < writeByteCount; i++)
                {
                    if (i % 2 == 0)
                    {
                        if (i + 1 == datas.Length)
                        {
                            SendByte[13 + i] = 0;
                        }
                        else
                        {
                            SendByte[13 + i] = datas[i + 1];
                        }
                    }
                    else
                        SendByte[13 + i] = datas[i - 1];
                }
                return SendByte;
            }
        }
    //}

    public enum StoreByteCategory
    {
        ABCD = 0,
        BACD = 1,
        CDAB = 2,
        DCBA = 3
    }
}
