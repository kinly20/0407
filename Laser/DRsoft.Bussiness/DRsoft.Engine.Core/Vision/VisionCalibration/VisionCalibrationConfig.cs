using DRsoft.Runtime.Core.Platform.Config;
using System;

namespace DRsoft.Engine.Core.Vision.VisionCalibration
{
    public class VisionCalibrationConfig : ConfigEventBase, IConfigExt<VisionCalibrationConfig>
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }

        /// <summary>
        /// 是否发生改变
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Changed(VisionCalibrationConfig obj)
        {
            return obj.IpAddress != IpAddress ||
                   obj.Port != Port ;
        }

        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public VisionCalibrationConfig Clone()
        {
            return new VisionCalibrationConfig()
            {
                Ower = Ower,
                Subscriber = Subscriber,
                IpAddress = IpAddress,
                Port = Port,
            };
        }
    }
}