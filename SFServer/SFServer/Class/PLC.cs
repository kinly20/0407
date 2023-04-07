using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;


namespace SFServer
{
    public class PLC
    {
        public PLCNet plcNet = new ModbusTcpHsl();

        Task task;
        public short Link;
        public string RemoteIP;
        private List<int> StartAddr = new List<int>();
        private List<int> AddrLenght = new List<int>();
        private List<string> Mode = new List<string>();

        public short Initial(string Path = @"D:\AutoMation\PLCaddr.csv", string Protocol = "ModbusTcpHsl")
        {
            switch (Protocol)
            {
                case "ModbusTcp":
                    //return ModbusTcp(Path);
                    return ModbusTcpHsl(Path);
                case "ModbusTcpHsl":
                    return ModbusTcpHsl(Path);
                default:
                    return -1;
            }
        }
        private short ModbusTcpHsl(string Path)
        {
            if (File.Exists(Path))
            {
                ReadAddrMessage(Path);
            }
            else
            {
                NoReadAddrMessage();
            }
            Link = plcNet.Link("", RemoteIP, 0);

            task = new Task(ModbusTcpHsl_Communicate);
            task.Start();
            return 0;
        }

        private void ModbusTcpHsl_Communicate()
        {
            while (true)
            {
                if (Link != 0)
                {
                    plcNet.Close();
                    Link = plcNet.Link("", RemoteIP, 0, 502);
                }
                if (Link == 0)
                {
                    //循环读读状态地址200-599
                    //Link = readMulitDatas(200, 400, ref MW0);
                    for (int i = 0; i < StartAddr.Count; i++)
                    {
                        switch (Mode[i])
                        {
                            case "写":
                                Link = writeMulitDatas(StartAddr[i], (short)AddrLenght[i], plcNet.MW0);
                                break;
                            case "读":
                                Link = readMulitDatas(StartAddr[i], (short)AddrLenght[i], ref plcNet.MW0);
                                break;
                            case "0":
                                Link = writeMulitDatas(StartAddr[i], (short)AddrLenght[i], plcNet.MW0);
                                break;
                            case "1":
                                Link = readMulitDatas(StartAddr[i], (short)AddrLenght[i], ref plcNet.MW0);
                                break;
                            default:
                                break;
                        }
                    }
                }
                Thread.Sleep(200);
            }
        }

        private void ReadAddrMessage(string Path)
        {
            using (StreamReader sr = new StreamReader(Path, Encoding.Default))
            {
                if ((sr.Peek() != -1))
                    sr.ReadLine();
                while (sr.Peek() != -1)
                {
                    string[] temp = sr.ReadLine().Split(',');
                    if (temp.Length >= 3)
                    {
                        Mode.Add(temp[0]);
                        StartAddr.Add(int.Parse(temp[1]));
                        AddrLenght.Add(int.Parse(temp[2]));
                        //if (temp[0] == "线体参数")
                        //{
                        //    lineParaStartAddr = int.Parse(temp[1]);
                        //    lineParaCount = int.Parse(temp[2]);
                        //}
                    }
                }
            }
        }

        private void NoReadAddrMessage()
        {
            Mode.Add("1");
            StartAddr.Add(504);
            AddrLenght.Add(4);

            Mode.Add("0");
            StartAddr.Add(504);
            AddrLenght.Add(4);


        }

        /// <summary>
        /// 批量读数据
        /// </summary>
        /// <param name="startAddr">起始地址</param>
        /// <param name="count">数量</param>
        /// <param name="data">接收数据地址</param>
        /// <returns>0为成功，-1为失败</returns>
        private short readMulitDatas(int startAddr, int count, ref short[] data)
        {
            //读次数
            var times = count / 100;
            //最后一次读数量
            var endCount = count % 100;
            for (int i = 0; i < times; i++)
            {
                if (plcNet?.Read(startAddr + i * 100, 100, ref data) != 0)
                {
                    return -1;
                }
            }
            if (endCount > 0)
            {
                if (plcNet?.Read(startAddr + times * 100, (short)endCount, ref data) != 0)
                {
                    return -1;
                }
            }
            return 0;
        }

        /// <summary>
        /// 批量写入数据,主要是参数点位等批量数据写入
        /// </summary>
        /// <param name="startAddr">起始位</param>
        /// <param name="count">数量</param>
        /// <param name="data">写入数据</param>
        /// <returns>0为成功，-1为失败</returns>
        private short writeMulitDatas(int startAddr, int count, short[] data)
        {
            //写次数
            var times = count / 100;
            //最后一次写入数量
            var endCount = count % 100;

            for (int i = 0; i < times; i++)
            {
                if (plcNet.Write(startAddr + i * 100, 100, data) != 0)
                {
                    return -1;
                }
            }
            if (endCount > 0)
            {
                return plcNet.Write(startAddr + times * 100, (short)endCount, data);
            }
            return 0;
        }

        public void WriteBit(string Addr, bool value)
        {
            if (plcNet != null && Link == 0 && Addr != null)
                plcNet.SetBit(Addr, value);
        }
        public void WriteShort(string Addr, short value)
        {
            if (plcNet != null && Link == 0 && Addr != null)
                plcNet.SetShort(Addr, value);
            else if (plcNet == null)
                Log.writelog("链接不通plcNet == null");
            else if (Addr == null)
                Log.writelog("链接不通Addr == null");
            else if (Link != 0)
                Log.writelog("链接不通Link != 0");
        }
        public void WriteInt(string Addr, int value)
        {
            if (plcNet != null && Link == 0 && Addr != null)
                plcNet.SetInt(Addr, value);
        }
        public void WritePulse(string AddrName, int PulseTime = 400)
        {
            WriteBit(AddrName, true);
            Thread.Sleep(PulseTime);
            WriteBit(AddrName, false);
        }
        public bool ReadBit(string Addr)
        {
            if (plcNet != null && Addr != null && Link == 0)
                return plcNet.GetBit(Addr);
            else
                return false;
        }

        public short ReadShort(string Addr, string type = "D")
        {
            if (plcNet != null && Addr != null && Link == 0)
                return plcNet.GetShort(Addr, type);
            else if (plcNet == null)
                Log.writelog("链接不通plcNet == null");
            else if (Addr == null)
                Log.writelog("链接不通Addr == null");
            else if (Link != 0) {
                Log.writelog("链接不通Link != 0");
                Link = plcNet.Link("", RemoteIP, 0);
            }
            return 0;

        }
        public int ReadInt(string Addr)
        {
            if (plcNet != null && Addr != null && Link == 0)
                return plcNet.GetInt(Addr);
            return 0;
        }


        public float ReadIntWithUnit(string Addr, float Unit)
        {
            if (plcNet != null && Addr != null && Link == 0)
                return plcNet.GetIntWithUnit(Addr, Unit);
            else
                return 0;
        }
        ///Add by Ming,2021/2/26
        public float ReadShortWithUnit(string Addr, float Unit, string type = "D")
        {
            if (plcNet != null && Addr != null && Link == 0)
                return plcNet.GetShort(Addr, type) * Unit;
            else
                return 0;
        }
        public short GetIndex(string str)
        {
            if (plcNet != null)
                return plcNet.GetIndex(str);
            else
                return 0;
        }


    }
}
