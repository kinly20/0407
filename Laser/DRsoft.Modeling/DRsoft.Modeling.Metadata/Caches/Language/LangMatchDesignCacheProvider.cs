using DRsoft.Modeling.Metadata.Models.Language;
using System;

namespace DRsoft.Modeling.Metadata.Caches.Language
{
    internal class LangMatchDesignCacheProvider : BaseCacheProvider<LangMatch>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        { }

        public override void SyncRuntimeToDesign(Guid metadataId)
        {
            throw new NotImplementedException();
        }

        public override void SyncDesignToRuntime(Guid metadataId)
        {
            throw new NotImplementedException();
        }
    }
}