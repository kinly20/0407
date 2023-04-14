using DRsoft.Engine.Core.PowerMeter;
using DRsoft.Engine.Plugin.PowerMeter.Implementation.Common;

namespace DRsoft.Engine.Plugin.PowerMeter.Implementation
{
    public class PowerMeterControlPlugin : AbstractPowerMeterControl
    {
        public CRS232Comm powerMeter;
        public PowerMeterControlPlugin(PowerMeterControlConfig config) : base(config)
        {
            Config = config;
            //Initialize(config.CmLightEnable, config.OptLightEnable, config.JlLightEnable);
            if (powerMeter == null)
            {
                powerMeter = new CRS232Comm();
                powerMeter.OnPowerData += new CRS232Comm.EventHandler(Refresh);
            }
        }

        public void Refresh(double index,bool flag)
        {
            receive = index;
            isOpen = flag;
        }

        public double receive = 0.0;

        public bool isOpen = false;
        public override string Scope => "PowerMeter";

        public override double Receive => receive;

        public override bool IsOpen => isOpen;

        // public override string Receive => receive;

        public override void Initial()
        {
            if (IsOpen)
                return;
            powerMeter.PortName = Config.PortName;
            powerMeter.BaudRate = Config.BaudRate;
            powerMeter.DataBits = Config.DataBits;
            powerMeter.Parity = Config.Parity;
            powerMeter.StopBits = Config.StopBits;
            powerMeter.ReadTimeout = Config.ReadTimeout;
            powerMeter.WriteTimeout = Config.WriteTimeout;
            powerMeter.InitialCom();
        }

        public override void StopCommunication()
        {
            powerMeter.ClosePort();
        }

        public override void Read()
        {
            powerMeter.ReadPort();
        }

        public override void Write(string msg)
        {
            powerMeter.WiterPort(msg);
        }
    }
}