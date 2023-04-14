using System;
using System.ComponentModel;
using System.Reflection;
using DRsoft.Runtime.Core.Platform.Language;

namespace DRsoft.Modeling.Metadata.Tools
{
    /// <summary>
    /// 对枚举类型的扩展。
    ///  </summary>
    public static class EnumExtensions
    {

        /// <summary>
        /// 得到枚举上面多语言标记
        /// </summary>
        /// <typeparam name="T">类型。</typeparam>
        /// <param name="value">枚举值。</param>
        /// <returns>枚举中文字符。</returns>

        public static string GetMultiLanguageDescription<T>(int value)
        {
            Type type = typeof(T);

            BindingFlags flag = BindingFlags.Public | BindingFlags.Static;

            foreach (FieldInfo field in type.GetFields(flag))
            {
                //过滤非枚举类型
                if (field.FieldType != typeof(int))
                {
                    continue;
                }

                int val = Convert.ToInt32(field.GetValue(null));
                string text = null;

                if (value == val)
                {

                }
                //检索应用于类型的成员的MultiLanguageAttribute自定义特性
                var attrMulti = field.GetCustomAttribute<MultiLanguageAttribute>();
                //如果有MultiLanguageAttribute标签，得到多语言资源KEY
                if (attrMulti != null)
                {
                    text = attrMulti.ResourceKey;
                }
                //获取到DescriptionAttribute特性里面描述信息
                else
                {
                    //检索应用于类型的成员的DescriptionAttribute自定义特性
                    var attrDesc = field.GetCustomAttribute<DescriptionAttribute>();
                    if (attrDesc != null)
                    {
                        text = attrDesc.Description;
                    }
                }
                //返回多语言资源中的中文文本信息
                if (string.IsNullOrEmpty(text) == false)
                {
                    return LanguageResourceManager.GetString(text, "zh-CHS");
                }
            }
            return null;
        }

        /// <summary>
        /// 获取枚举上的<see cref="DescriptionAttribute"/>的说明属性的值。
        /// </summary>
        /// <typeparam name="T">枚举类型。</typeparam>
        /// <param name="enumerationValue">枚举值。</param>
        /// <returns>枚举说明</returns>
        public static string GetDescription<T>(this T enumerationValue) where T : struct
        {
            string enumValue = enumerationValue.ToString();

            FieldInfo memberInfo = typeof(T).GetField(enumValue);
            if (memberInfo != null)
            {
                var attrs = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Length > 0)
                {
                    enumValue = ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return enumValue;
        }
    }
}
