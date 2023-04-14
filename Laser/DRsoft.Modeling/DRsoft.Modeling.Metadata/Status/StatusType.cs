using System.ComponentModel;

namespace DRsoft.Modeling.Metadata.Status
{
    /// <summary>
    /// 状态类型
    /// </summary>
    public enum StatusType
    {
        /// <summary>
        /// 已提交
        /// </summary>
        [Description("已发布")]
        Normal,
        /// <summary>
        /// 新增
        /// </summary>
        [Description("已新增")]
        AppendNew,
        /// <summary>
        /// 修改
        /// </summary>
        [Description("已修改")]
        Modified,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("已删除")]
        Deleted,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("已恢复")]
        Restore
    }
}
