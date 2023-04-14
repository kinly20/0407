namespace DRsoft.Modeling.Metadata.Managers
{
    /// <summary>
    /// 元数据提交工厂
    /// </summary>
    internal static class MetadataSubmitFactory
    {
        private static readonly object s_lock = new object();
        private static volatile MetadataSubmit s_metadataSubmit;

        /// <summary>
        /// 创建版本管理实例
        /// </summary>
        /// <returns>版本管理实例</returns>
        public static MetadataSubmit Create()
        {
            if (s_metadataSubmit == null)
            {
                lock (s_lock)
                {
                    if (s_metadataSubmit == null)
                        s_metadataSubmit = CreateMetadataSubmit();
                }
            }
            return s_metadataSubmit;
        }

        private static MetadataSubmit CreateMetadataSubmit()
        {
            // 暂时采用离线管理模式，后期采用git管理
            var manager = new OfflineMetadataSubmit();
            return manager;
        }
    }
}
