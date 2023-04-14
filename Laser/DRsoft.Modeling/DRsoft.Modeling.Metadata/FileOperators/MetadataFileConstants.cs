namespace DRsoft.Modeling.Metadata.FileOperators
{
    /// <summary>
    /// 元数据文件常量
    /// </summary>
    internal static class MetadataFileConstants
    {
        /// <summary>
        /// 元数据根目录
        /// </summary>
        public static string MetadataDirectory =>
            //string dir = ModelingSection.GetCurrent().MetadataDirectory;
            //return dir;
            "";

        /// <summary>
        /// 产品目录
        /// </summary>
        public static string ProductDirectory => "_metadata";
        /// <summary>
        /// 运行时文件后缀名
        /// </summary>
        public static string RuntimeFileExtension => @".metadata.config";
        /// <summary>
        /// 设计时文件后缀名
        /// </summary>
        public static string DesignFileExtension => @".metadata.design.config";
        /// <summary>
        /// 实体日志保存路径
        /// </summary>
        public static string EntityLogDirectory => "EntityLog";
    }
}
