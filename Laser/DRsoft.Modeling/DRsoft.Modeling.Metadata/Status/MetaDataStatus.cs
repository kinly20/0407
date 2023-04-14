namespace DRsoft.Modeling.Metadata.Status
{
    /// <summary>
    /// 元数据类型（现阶段暂不考虑扩展及自定义）
    /// </summary>
    public enum MetaDataStatus
    {
        /// <summary>
        /// 产品元数据
        /// </summary>
        Product = 0,
        /// <summary>
        /// 扩展元数据
        /// </summary>
        Extend = 1,
        /// <summary>
        /// 自定义
        /// </summary>
        Customize = 2
    }
}
