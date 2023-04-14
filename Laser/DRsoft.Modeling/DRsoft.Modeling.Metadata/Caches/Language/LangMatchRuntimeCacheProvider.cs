using DRsoft.Modeling.Metadata.Models.Language;
using System;

namespace DRsoft.Modeling.Metadata.Caches.Language
{

    internal class LangMatchRuntimeCacheProvider : BaseCacheProvider<LangMatch>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        { }

        /// <summary>
        /// 运行时不需要实现此方法
        /// </summary>
        /// <param name="metadataId"></param>
        public override void SyncRuntimeToDesign(Guid metadataId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 运行时不需要实现此方法
        /// </summary>
        /// <param name="metadataId"></param>
        public override void SyncDesignToRuntime(Guid metadataId)
        {
            throw new NotImplementedException();
        }
    }
}