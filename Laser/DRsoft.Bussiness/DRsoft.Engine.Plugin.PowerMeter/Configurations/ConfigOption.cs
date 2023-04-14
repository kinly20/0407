using System.Threading;
using DRsoft.Runtime.Core.Platform.Ioc;
using RJCP.IO.Ports;

namespace DRsoft.Engine.Plugin.PowerMeter.Configuration
{
    /// <summary>
    /// 控制器配置选项
    /// </summary>
    public class ConfigOption : IkeyConfig
    {
        public string Key { get; set; } = "DRSoft.Plugin.PowerMeter";

        public string PortName { get; set; }
        public int BaudRate { get; set; }

        public int DataBits { get; set; }

        public StopBits StopBits { get; set; }

        public Parity Parity { get; set; }

        public int WriteTimeout { get; set; }

        public int ReadTimeout { get; set; }

        public SynchronizationContext Context { get; set; }
    }
}
