namespace DRsoft.Modeling.Metadata.Caches
{
    /// <summary>
    /// 缓存类型
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// 只取运行时
        /// </summary>
        Runtime,
        /// <summary>
        /// 只取设计时
        /// </summary>
        Design,

        /// <summary>
        /// 调试预览取设计模式，运行取运行模式
        /// </summary>
        RuntimeOrDesign
    }
}
