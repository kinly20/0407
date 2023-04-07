using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HslCommunication;
using HslCommunication.ModBus;
using MyControl.tools;
using MyControl.Communication;

namespace Testdemo.Class
{
    public class ModbusTcpHsl : PLCNet
    {
        private ModbusTcpNet _modbusTcpHsl = new ModbusTcpNet() { DataFormat = HslCommunication.Core.DataFormat.CDAB, ConnectTimeOut = 1000, ReceiveTimeOut = 1000 };

        //private bool _connected;
        //public bool Connected { get=>_connected; set=>_connected=value; }  
        private int slaveId = 255;

        /// <summary>
        /// 获取Hsl版本带从站Modbus地址
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addr"></param>
        /// <returns></returns>
        private string getAddr(int id, string addr)
        {
            return string.Format("s={0};{1}", id, addr);
        }
        /// <summary>
        /// 获取地址索引
        /// </summary>
        /// <param name="addr"></param>
        /// <returns></returns>
        private string getAddr(string addr)
        {
            if (addr.ToLower().Contains("mw"))
            {
                addr = addr.ToLower().Replace("mw", "");
            }

            return addr;
        }
        public int SlaveId
        {
            get { return slaveId; }
            set { slaveId = value; }
        }

        public override void Close()
        {
            _modbusTcpHsl.ConnectClose();
        }

        public override short Link(string LocalIP, string RemoteIP, short LocalPort = 0, int RemotePort = 502)
        {
            _modbusTcpHsl.ConnectClose();
            _modbusTcpHsl.IpAddress = RemoteIP;
            _modbusTcpHsl.Port = RemotePort;
            try
            {
                OperateResult connect = _modbusTcpHsl.ConnectServer();
                if (connect.IsSuccess)
                {
                    //_connected = true;
                    return 0;

                }
                else
                {
                    //_connected = false;
                    return -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //_connected = false;
                return -2;
            }
        }

        public override short Read(int StartAddress, short DataCount, ref short[] value, int dataIndex = -1)
        {
            //short[] temp = new short[DataCount];

            OperateResult<short[]> result = _modbusTcpHsl.ReadInt16(getAddr(slaveId, StartAddress.ToString()), (ushort)DataCount);
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
        public override short Write(int StartAddress, short DataCount, short[] Data, int dataIndex = -1)
        {

            short[] temp = new short[DataCount];
            if (StartAddress + DataCount < Data.Length)
                Array.Copy(Data, StartAddress, temp, 0, DataCount);
            else
                Array.Copy(Data, 0, temp, 0, DataCount);
            OperateResult result = _modbusTcpHsl.Write(getAddr(slaveId, StartAddress.ToString()), temp);
            if (result.IsSuccess)
                return 0;
            else
            {
                //   MessageBox.Show(result.ToMessageShowString());
            }
            return -1;
        }
        public override bool GetBit(string AddrName)
        {
            var temp = GetBoolIndex(AddrName);
            if (temp.Length > 1)
                return dataChange.GetBitValue(MW0[temp[0]], temp[1]);
            else
                return false;
        }

        public override short GetIndex(string addr)
        {
            string[] temp = addr.ToLower().Replace("mw", "").Split('.', '*');
            return short.Parse(temp[0]);
        }
        public short[] GetBoolIndex(string addr)
        {
            string[] temp = addr.ToLower().Replace("mw", "").Split('.', '*');
            short boolIndex = 0;
            if (temp.Length > 1)
            {
                short.TryParse(temp[1], out boolIndex);
            }
            return new short[] { short.Parse(temp[0]), boolIndex };
        }
        public override int GetInt(string AddrName)
        {
            int index = short.Parse(getAddr(AddrName));
            return dataChange.DInt16toI32(PLCAddress.MW0[index], PLCAddress.MW0[index + 1]);
        }



        public override float GetIntWithUnit(string AddrName, float Unit)
        {
            int index = short.Parse(getAddr(AddrName));
            return dataChange.DInt16toI32(MW0[index], MW0[index + 1]) * Unit;
        }

        private readonly static object _locker = new object();
        public override void SetBit(string AddrName, bool value)
        {
            short[] temp = GetBoolIndex(AddrName);
            MW0[temp[0]] = dataChange.SetBitValue(MW0[temp[0]], (ushort)temp[1], value);
            SetShort(temp[0].ToString(), MW0[temp[0]]);

        }

        public override void SetInt(string AddrName, int Value)
        {
            AddrName = AddrName.ToLower().Replace("mw", "");
            int index = int.Parse(AddrName);
            ///写入PLCAddr内存,供位运算使用
            dataChange.DintToInt(Value, ref MW0[index + 1], ref MW0[index]);

            _modbusTcpHsl.Write(getAddr(slaveId, AddrName), Value);
        }


        public override void SetShort(string AddrName, short value)
        {
            AddrName = AddrName.ToLower().Replace("mw", "");
            ///写入PLCAddr内存,供位运算使用
            MW0[int.Parse(AddrName)] = value;
            _modbusTcpHsl.Write(getAddr(slaveId, AddrName), value);

        }

        public override void UpdatePara()
        {
            throw new NotImplementedException();
        }

        public override void UpdatePoint()
        {
            throw new NotImplementedException();
        }

        public override short GetShort(string AddrName, string type)
        {
            return MW0[int.Parse(getAddr(AddrName))];
        }
        #region 异步方法写
        public override async Task SetBitAsync(string AddrName, bool value)
        {

            short[] temp = GetBoolIndex(AddrName);
            MW0[temp[0]] = dataChange.SetBitValue(MW0[temp[0]], (ushort)temp[1], value);
            await SetShortAsync(temp[0].ToString(), MW0[temp[0]]);
        }
        public override async Task SetShortAsync(string AddrName, short value)
        {
            AddrName = AddrName.ToLower().Replace("mw", "");
            ///写入PLCAddr内存,供位运算使用
            MW0[int.Parse(AddrName)] = value;
            await Task.Run(() => _modbusTcpHsl.Write(getAddr(slaveId, AddrName), value));

        }
        public override async Task SetIntAsync(string AddrName, int Value)
        {
            AddrName = AddrName.ToLower().Replace("mw", "");

            int index = int.Parse(AddrName);
            ///写入PLCAddr内存,供位运算使用
            dataChange.DintToInt(Value, ref MW0[index + 1], ref MW0[index]);
            await Task.Run(() => _modbusTcpHsl.Write(getAddr(slaveId, AddrName), Value));
        }

        public override string GetString(string AddrName, int count)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
