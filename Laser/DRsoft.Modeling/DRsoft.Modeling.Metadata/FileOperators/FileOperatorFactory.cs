using DRsoft.Modeling.Metadata.Interfaces;

namespace DRsoft.Modeling.Metadata.FileOperators
{
    /// <summary>
    /// 文件操作类工厂
    /// </summary>
    internal static class FileOperatorFactory
    {
        /// <summary>
        /// 创建不带设计时的文件操作类实例
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns>文件操作类实例</returns>
        public static IFileOperator<T> Create<T>() where T : IModelKey
        {
            //var workMode = ModelingSection.GetCurrent().Manager.WorkMode;
            //if (workMode == EnumWorkMode.Product)
                return new ProductFileOperator<T>();
            //if (typeof(T) == typeof(Langs))
            //    return (IFileOperator<T>)(new CustomizeLangsOperator());
            //return new CustomizeFileOperator<T>();
        }
    }
}
