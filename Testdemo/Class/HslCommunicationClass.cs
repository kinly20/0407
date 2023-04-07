using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HslCommunication;
using HslCommunication.WebSocket;

namespace ICD.Class
{
    public class HslCommunicationClass
    {
        HslCommunication.Profinet.Inovance.InovanceAMTcp inovanceAMTcp;
        string ip;
        int port;
        public bool isconnect = false;
        private int slaveId = 255;
        public HslCommunicationClass(string ip, int port = 502)
        {
            this.ip = ip;
            this.port = port;
        }

        public void Connect()
        {

            inovanceAMTcp = new HslCommunication.Profinet.Inovance.InovanceAMTcp(ip, port);
            inovanceAMTcp.ConnectClose();
            inovanceAMTcp.IpAddress = ip;
            inovanceAMTcp.Port = port;
            //inovanceAMTcp.IsStringReverse = true;
            //inovanceAMTcp.SetLoginAccount("", "");

            inovanceAMTcp.ConnectTimeOut = 500;
            inovanceAMTcp.ReceiveTimeOut = 500;
            inovanceAMTcp.DataFormat = HslCommunication.Core.DataFormat.CDAB;
            OperateResult connect = inovanceAMTcp.ConnectServer();
            isconnect = connect.IsSuccess;
        }

        public void DisConnect()
        {
            if (inovanceAMTcp != null)
            {
                isconnect = false;
                inovanceAMTcp.ConnectClose();
                inovanceAMTcp.Dispose();
            }
        }

        private string getAddr(int id, string addr)
        {
            return string.Format("s={0};{1}", id, addr);
        }

        private short Read(int StartAddress, short DataCount, ref short[] value, int dataIndex = -1)
        {
            //short[] temp = new short[DataCount];

            OperateResult<short[]> result = inovanceAMTcp.ReadInt16(getAddr(slaveId, StartAddress.ToString()), (ushort)DataCount);
            if (result.IsSuccess)
            {
                Array.Copy(result.Content, 0, value, StartAddress, DataCount);
                return 0;
            }
            else
            {
                //     MessageBox.Show(result.ToMessageShowString()+1);
            }
            return -1;
        }
        private short Write(int StartAddress, short DataCount, short[] Data, int dataIndex = -1)
        {

            short[] temp = new short[DataCount];
            if (StartAddress + DataCount < Data.Length)
                Array.Copy(Data, StartAddress, temp, 0, DataCount);
            else
                Array.Copy(Data, 0, temp, 0, DataCount);
            OperateResult result = inovanceAMTcp.Write(getAddr(slaveId, StartAddress.ToString()), temp);
            if (result.IsSuccess)
                return 0;
            else
            {
                //   MessageBox.Show(result.ToMessageShowString());
            }
            return -1;
        }

        //批量读
        private short readMulitDatas(int startAddr, int count, ref short[] data)
        {
            //读次数
            var times = count / 100;
            //最后一次读数量
            var endCount = count % 100;
            for (int i = 0; i < times; i++)
            {
                if (this.Read(startAddr + i * 100, 100, ref data) != 0)
                {
                    return -1;
                }
            }
            if (endCount > 0)
            {
                if (this.Read(startAddr + times * 100, (short)endCount, ref data) != 0)
                {
                    return -1;
                }
            }
            return 0;
        }


        public void WriteValue<T>(string startAddress, T value, out string msg)
        {
            msg = string.Empty;
            if (!isconnect)
                return;

            OperateResult result = null;

            if (typeof(T) == typeof(bool))
            {
                result = inovanceAMTcp.Write(startAddress, Convert.ToBoolean(value));
            }
            else if (typeof(T) == typeof(sbyte))
            {
                result = inovanceAMTcp.Write(startAddress, Convert.ToSByte(value));
            }
            else if (typeof(T) == typeof(byte))
            {
                result = inovanceAMTcp.Write(startAddress, Convert.ToByte(value));
            }
            else if (typeof(T) == typeof(short))
            {
                result = inovanceAMTcp.Write(startAddress, Convert.ToInt16(value));
            }
            else if (typeof(T) == typeof(ushort))
            {
                result = inovanceAMTcp.Write(startAddress, Convert.ToUInt16(value));
            }
            else if (typeof(T) == typeof(int))
            {
                result = inovanceAMTcp.Write(startAddress, Convert.ToInt32(value));
            }
            else if (typeof(T) == typeof(uint))
            {
                result = inovanceAMTcp.Write(startAddress, Convert.ToUInt32(value));
            }
            else if (typeof(T) == typeof(long))
            {
                result = inovanceAMTcp.Write(startAddress, Convert.ToInt64(value));
            }
            else if (typeof(T) == typeof(ulong))
            {
                result = inovanceAMTcp.Write(startAddress, Convert.ToUInt64(value));
            }
            else if (typeof(T) == typeof(float))
            {
                result = inovanceAMTcp.Write(startAddress, Convert.ToSingle(value));
            }
            else if (typeof(T) == typeof(double))
            {
                result = inovanceAMTcp.Write(startAddress, Convert.ToDouble(value));
            }
            else if (typeof(T) == typeof(string))
            {
                result = inovanceAMTcp.Write(startAddress, value.ToString());
            }
            else
            {
                msg = $"类型不支持";
            }
            if (!result.IsSuccess)
                msg = $"写入失败";
            else
                Log.writelog("地址" + startAddress + " 写入" + value.ToString() + " 时间" + System.DateTime.Now.ToString());

        }

        public void ReadValue<T>(string startAddress, out T value, out string msg) where T : struct
        {
            try
            {
                value = default(T);
                msg = string.Empty;
                if (!isconnect)
                    return;


                ushort length = 0;
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
                    length = 8;
                }

                //OperateResult<byte[]> back = inovanceAMTcp.Read(startAddress, length);
                //byte[] backbyte = back.Content;

                if (typeof(T) == typeof(bool))
                {
                    OperateResult<bool[]> backint = inovanceAMTcp.ReadBool(startAddress, length);
                    if (backint.Content != null)
                    {
                        bool[] backbyte = backint.Content;
                        value = (T)(object)Convert.ToBoolean(backbyte[0]);
                    }
                }
                //value = (T)(object)Convert.ToBoolean(backbyte[0]);
                else if (typeof(T) == typeof(sbyte))
                {
                    OperateResult<byte[]> back = inovanceAMTcp.Read(startAddress, length);
                    if (back.Content != null)
                    {
                        byte[] backbyte = back.Content;
                        value = (T)(object)(sbyte)(backbyte[0]);
                    }
                }
                else if (typeof(T) == typeof(byte))
                {
                    OperateResult<byte[]> back = inovanceAMTcp.Read(startAddress, length);
                    if (back.Content != null)
                    {
                        byte[] backbyte = back.Content;
                        value = (T)(object)backbyte[0];
                    }
                    else
                    {
                        isconnect = false;
                    }
                }
                //value = (T)(object)backbyte[0];
                else if (typeof(T) == typeof(short))
                {
                    OperateResult<short[]> backint = inovanceAMTcp.ReadInt16(startAddress, length);
                    if (backint.Content != null)
                    {
                        short[] backints = backint.Content;
                        value = (T)(object)backints[0];
                    }
                    else
                    {
                        isconnect = false;
                    }
                }
                //value = (T)(object)BitConverter.ToInt16(backbyte, 0);
                else if (typeof(T) == typeof(ushort))
                {
                    OperateResult<ushort[]> backint = inovanceAMTcp.ReadUInt16(startAddress, length);
                    if (backint.Content != null)
                    {
                        ushort[] backints = backint.Content;
                        value = (T)(object)backints[0];
                    }
                }
                //value = (T)(object)BitConverter.ToUInt16(backbyte, 0);
                else if (typeof(T) == typeof(int))
                {
                    OperateResult<int[]> backint = inovanceAMTcp.ReadInt32(startAddress, length);
                    if (backint.Content != null)
                    {
                        int[] backints = backint.Content;
                        value = (T)(object)backints[0];
                    }
                    else
                    {
                        isconnect = false;
                    }
                }
                else if (typeof(T) == typeof(uint))
                {
                    OperateResult<uint[]> backint = inovanceAMTcp.ReadUInt32(startAddress, length);
                    if (backint.Content != null)
                    {
                        uint[] backints = backint.Content;
                        value = (T)(object)backints[0];
                    }
                }
                //value = (T)(object)BitConverter.ToUInt32(backbyte, 0);
                else if (typeof(T) == typeof(long))
                {
                    OperateResult<long[]> backint = inovanceAMTcp.ReadInt64(startAddress, length);
                    if (backint.Content != null)
                    {
                        long[] backints = backint.Content;
                        value = (T)(object)backints[0];
                    }
                }
                //value = (T)(object)BitConverter.ToInt64(backbyte, 0);
                else if (typeof(T) == typeof(ulong))
                {
                    OperateResult<ulong[]> backint = inovanceAMTcp.ReadUInt64(startAddress, length);
                    if (backint.Content != null)
                    {
                        ulong[] backints = backint.Content;
                        value = (T)(object)backints[0];
                    }
                }
                //value = (T)(object)BitConverter.ToUInt64(backbyte, 0);
                else if (typeof(T) == typeof(float))
                {
                    OperateResult<float[]> backint = inovanceAMTcp.ReadFloat(startAddress, length);
                    if (backint.Content != null)
                    {
                        float[] backints = backint.Content;
                        value = (T)(object)backints[0];
                    }
                }
                //value = (T)(object)BitConverter.ToSingle(backbyte, 0);
                else if (typeof(T) == typeof(double))
                {
                    OperateResult<double[]> backint = inovanceAMTcp.ReadDouble(startAddress, length);
                    if (backint.Content != null)
                    {
                        double[] backints = backint.Content;
                        value = (T)(object)backints[0];
                    }
                    else
                    {
                        isconnect = false;
                    }
                }
                else if (typeof(T) == typeof(string))
                {
                    OperateResult<string> backstring = inovanceAMTcp.ReadString(startAddress, length);
                    if (backstring.Content != null)
                    {
                        string backstrings = backstring.Content;
                        value = (T)(object)backstrings[0];
                    }
                }
                //value = (T)(object)BitConverter.ToDouble(backbyte, 0);
            }
            catch
            {
                value = default(T);
                msg = string.Empty;
            }
        }


        public void ReadValueString(string startAddress, out string value, out string msg)
        {
            value = string.Empty;
            msg = string.Empty;
            if (!isconnect)
                return;


            ushort length = 0;

            length = 8;

            OperateResult<byte[]> back = inovanceAMTcp.Read(startAddress, length);
            byte[] backbyte = back.Content;

            OperateResult<string> backstring = inovanceAMTcp.ReadString(startAddress, length);
            string backstrings = backstring.Content;

            OperateResult<int[]> backint = inovanceAMTcp.ReadInt32(startAddress, length);
            int[] backints = backint.Content;

            OperateResult<double[]> backdouble = inovanceAMTcp.ReadDouble(startAddress, length);
            double[] backdoubles = backdouble.Content;

            value = BitConverter.ToString(backbyte, 0);
        }

    }

    public class addrint : EventArgs
    {
        public string addr { get; set; }
        public int value { get; set; }
    }

    public class addrdouble : EventArgs
    {
        public string addr { get; set; }
        public double value { get; set; }
    }
}
