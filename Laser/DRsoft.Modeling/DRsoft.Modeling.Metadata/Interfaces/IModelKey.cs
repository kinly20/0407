using System;

namespace DRsoft.Modeling.Metadata.Interfaces
{
    /// <summary>
    /// 元数据主键接口
    /// </summary>
    public interface IModelKey : IMetadataStatus
    {
        /// <summary>
        /// 主键
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// 元数据版本(考虑后期兼容不同的版本)
        /// </summary>
        string MetadataVersion { get; set; }
    }
}
