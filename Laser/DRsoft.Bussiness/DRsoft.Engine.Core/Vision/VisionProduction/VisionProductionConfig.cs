using DRsoft.Runtime.Core.Platform.Config;
using System;

namespace DRsoft.Engine.Core.Vision.VisionProduction
{
    public class VisionProductionConfig : ConfigEventBase, IConfigExt<VisionProductionConfig>
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }

        /// <summary>
        /// 是否发生改变
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Changed(VisionProductionConfig obj)
        {
            return obj.IpAddress != IpAddress ||
                   obj.Port != Port;
        }

        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public VisionProductionConfig Clone()
        {
            return new VisionProductionConfig()
            {
                Ower = Ower,
                Subscriber = Subscriber,
                IpAddress = IpAddress,
                Port = Port
            };
        }
    }
}