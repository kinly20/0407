using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SFServer
{
    public abstract class PLCNet
    {
        public short[] MW0 = new short[16000];
        public abstract short Link(string LocalIP, string RemoteIP, short LocalPort = 0, int RemotePort = 502);
        public abstract void Close();

        public abstract short Read(int StartAddress, short DataCount, ref short[] value, int dataIndex = -1);

        public abstract short Write(int StartAddress, short DataCount, short[] Data, int dataIndex = -1);

        public abstract void SetBit(string AddrName, bool value);

        public abstract bool GetBit(string AddrName);

        public abstract void SetShort(string AddrName, short value);

        public abstract short GetShort(string AddrName, string type);

        public abstract int GetInt(string AddrName);

        public abstract void SetInt(string AddrName, int Value);

        public abstract float GetIntWithUnit(string AddrName, float Unit);

        public abstract void UpdatePara();

        public abstract void UpdatePoint();

        public abstract short GetIndex(string addr);

        public abstract string GetString(string AddrName, int count);

        #region 异步方法
        public abstract Task SetBitAsync(string AddrName, bool value);
        public abstract Task SetShortAsync(string AddrName, short value);
        public abstract Task SetIntAsync(string AddrName, int value);
        #endregion
    }
}
