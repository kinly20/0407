using System;

namespace DRsoft.Engine.Model.Const
{
    /// <summary>
    /// 自定义属性映射
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public class MapModeTypeAttribute : Attribute
    {
        public Type Target;

        public MapModeTypeAttribute(Type target)
        {
            Target = target;
        }
    }
}