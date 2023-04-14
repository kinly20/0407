using DRsoft.Modeling.Metadata.Managers.Language;
using DRsoft.Modeling.Metadata.Status;
using System;
using System.Collections.Generic;
using DRsoft.Modeling.Metadata.Managers.Config;

namespace DRsoft.Modeling.Metadata.Managers
{
    /// <summary>
    /// 元数据管理类工厂
    /// </summary>
    internal static class MetadataManagerFactory
    {
        /// <summary>
        /// 创建一个元数据管理类的实例
        /// </summary>
        /// <param name="typeName">元数据类型</param>
        /// <returns>元数据管理类</returns>
        public static IMetadataManager Create(string typeName)
        {
            // 
            switch (typeName)
            {
                case "EngineConfig":
                    return EngineMetadataManager.GetInstance();
                case "CameraConfig":
                    return CameraManager.GetInstance();
                case "CameraRecipeConfig":
                    return CameraRecipeManager.GetInstance();
                case "ControlConfig":
                    return ControllerManager.GetInstance();
                case "Langs":
                    return LangsManager.GetInstance();                
                default:
                    throw new ArgumentException("元数据的ControlType不合法：" + typeName, nameof(typeName));
            }
        }

        public static Guid Submit(List<MetadataStatusItemExt> items, string appCode, string message, string email = "")
        {
            var mgr = MetadataSubmitFactory.Create();
            return mgr.Start(items, appCode, message, email);
        }        
    }
}
