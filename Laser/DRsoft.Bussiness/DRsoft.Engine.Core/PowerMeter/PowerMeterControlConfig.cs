using DRsoft.Runtime.Core.Platform.Config;
using System;
//using System.IO.Ports;
using RJCP.IO.Ports;


namespace DRsoft.Engine.Core.PowerMeter
{
    public class PowerMeterControlConfig : ConfigEventBase, IConfigExt<PowerMeterControlConfig>
    {
        public string PortName { get; set; }
        public int BaudRate { get; set; }

        public int DataBits { get; set; }

        public StopBits StopBits { get; set; }

        public Parity Parity { get; set; }

        public int WriteTimeout { get; set; }

        public int ReadTimeout { get; set; }


        /// <summary>
        /// 是否发生改变
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Changed(PowerMeterControlConfig obj)
        {
            return obj.PortName != obj.PortName ||
                   obj.BaudRate != obj.BaudRate ||
                   obj.DataBits != obj.DataBits ||
                   obj.StopBits != obj.StopBits ||
                   obj.Parity != obj.Parity ||
                   obj.WriteTimeout != obj.WriteTimeout ||
                   obj.ReadTimeout != ReadTimeout;
        }

        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public PowerMeterControlConfig Clone()
        {
            return new PowerMeterControlConfig()
            {
                Ower = Ower,
                Subscriber = Subscriber,
                PortName = PortName,
                BaudRate = BaudRate,
                DataBits = DataBits,
                StopBits = StopBits,
                Parity = Parity,
                WriteTimeout = WriteTimeout,
                ReadTimeout = ReadTimeout,
            };
        }
    }
}