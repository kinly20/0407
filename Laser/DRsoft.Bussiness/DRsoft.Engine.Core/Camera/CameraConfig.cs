using DRsoft.Runtime.Core.Platform.Config;
using System;

namespace DRsoft.Engine.Core.Camera
{
    /// <summary>
    /// 相机配置
    /// </summary>
    public class CameraConfig : ConfigEventBase, IConfigExt<CameraConfig>
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }

        /// <summary>
        /// 对象是否发生变更
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual bool Changed(CameraConfig obj)
        {
            return !string.Equals(obj.IpAddress, this.IpAddress, StringComparison.OrdinalIgnoreCase) ||
                   obj.Port != this.Port;
        }

        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public CameraConfig Clone()
        {
            return new CameraConfig
            {
                Ower = this.Ower,
                Subscriber = this.Subscriber,
                IpAddress = this.IpAddress,
                Port = this.Port
            };
        }
    }
}