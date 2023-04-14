using System.ComponentModel;

namespace DRsoft.Modeling.Metadata.Status
{
    /// <summary>
    /// 元数据类型
    /// </summary>
    public enum MetadataType
    {
        /// <summary>
        /// 引擎配置
        /// </summary>
        [Description("引擎配置")]
        Engine,
        /// <summary>
        /// 多语言
        /// </summary>
        [Description("多语言")]
        Langs
    }
}
