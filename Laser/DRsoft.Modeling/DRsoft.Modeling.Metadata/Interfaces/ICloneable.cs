using System;

namespace DRsoft.Modeling.Metadata.Interfaces
{
    /// <summary>
    /// 深拷贝接口
    /// </summary>
    /// <typeparam name="T">元数据类型</typeparam>
    public interface ICloneable<in T> : ICloneable
    {
        /// <summary>
        /// 深拷贝成员到目标对象上。
        /// </summary>
        /// <param name="dest">目标对象</param>
        void CopyTo(T dest);
    }
}
