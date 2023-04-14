using DRsoft.Modeling.Metadata.Caches;
using DRsoft.Runtime.Core.Platform.Services;
using System;
using System.Windows;
using DRsoft.Modeling.Metadata.Models.Config;

namespace DRsoft.Modeling.Provider
{
    /// <summary>
    /// 
    /// </summary>
    public class ModelingProvider : IModelingProvider
    {
        /// <summary>
        /// 初始化元数据
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void InitMetadata()
        {
            // 初始化各类元数据
            try
            {
                CacheProviderFactory<ControlConfig>.Create(CacheType.RuntimeOrDesign);
                CacheProviderFactory<CamConfig>.Create(CacheType.RuntimeOrDesign);
                CacheProviderFactory<CamRecipeConfig>.Create(CacheType.RuntimeOrDesign);
                CacheProviderFactory<EngineConfig>.Create(CacheType.RuntimeOrDesign);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
