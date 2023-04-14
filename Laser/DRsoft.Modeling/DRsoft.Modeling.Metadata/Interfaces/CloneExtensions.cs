using System;
using System.Collections.Generic;

namespace DRsoft.Modeling.Metadata.Interfaces
{
    /// <summary>
    /// 深拷贝扩展方法实现类
    /// </summary>
    public static class CloneExtensions
    {

        /// <summary>
        /// 有些集合是List(Of 元数据类)，深拷贝时候很麻烦，于是写了这个扩展方法。直接在集合上调用Clone方法即可
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static List<T> Clone<T>(this List<T> src) where T : class, ICloneable<T>
        {
            if (src == null)
            {
                throw new ArgumentNullException(nameof(src));
            }

            List<T> result = new List<T>(src.Count);

            foreach (var item in src)
            {
                if (object.Equals(item, default(T)))
                {
                    continue;
                }
                result.Add(item.Clone<T>());
            }
            return result;
        }

        /// <summary>
        /// 只用于Clone值类型集合，包含string，如果是复杂类型，调用Clone方法
        /// </summary>
        /// <param name="src"></param>
        /// <returns>Clone对象</returns>
        public static List<T> CloneValueType<T>(this List<T> src)
        {
            if (src == null)
            {
                throw new ArgumentNullException(nameof(src));
            }

            List<T> result = new List<T>(src.Count);
            foreach (var item in src)
            {
                if (object.Equals(item, default(T)))
                {
                    continue;
                }
                result.Add(item);
            }
            return result;
        }


        /// <summary>
        /// 有些集合是Dictionary(Of T, Of T1)，深拷贝时候很麻烦，于是写了这个扩展方法。直接在集合上调用Clone方法即可
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static Dictionary<T, T1> Clone<T, T1>(this Dictionary<T, T1> src)
        {
            if (object.Equals(src, default(Dictionary<T, T1>)))
            {
                throw new ArgumentNullException(nameof(src));
            }

            Dictionary<T, T1> dest = new Dictionary<T, T1>(src.Count);

            foreach (var kvp in src)
            {
                dest.Add(kvp.Key, kvp.Value);
            }

            return dest;
        }

        /// <summary>
        /// 有些类的深拷贝实现是返回object，这样返回特定类型要转换，所以写了这个扩展方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <returns></returns>
        public static T Clone<T>(this T src) where T : ICloneable<T>
        {
            if (src == null)
            {
                throw new ArgumentNullException(nameof(src));
            }

            return (T)src.Clone();
        }
    }
}
