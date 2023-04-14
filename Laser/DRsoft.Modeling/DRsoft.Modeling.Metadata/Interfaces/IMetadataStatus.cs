using DRsoft.Modeling.Metadata.Status;

namespace DRsoft.Modeling.Metadata.Interfaces
{
    /// <summary>
    /// 元数据接口
    /// </summary>
    public interface IMetadataStatus
    {
        /// <summary>
        /// 元数据状态
        /// </summary>
        MetaDataStatus MetadataStatus { get; set; }
    }
}
