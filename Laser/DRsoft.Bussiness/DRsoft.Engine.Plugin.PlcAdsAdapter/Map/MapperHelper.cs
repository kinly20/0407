using System;
using System.Linq;
using System.Reflection;

namespace DRsoft.Engine.Plugin.PlcAdsAdapter.Map
{
    internal class MapperHelper
    {
        private static MethodInfo? _mapMethod;

        private static MethodInfo? MapMethod(MapperProvider provider)
        {
            if (_mapMethod == null)
            {
                _mapMethod = provider.Mapper.GetType().GetMethods()
                    .FirstOrDefault(r => r.ToString()
                        .Equals("TDestination Map[TSource,TDestination](TSource)",
                            StringComparison.CurrentCultureIgnoreCase));
            }

            return _mapMethod;
        }

        /// <summary>
        ///  数据之间的转换
        /// </summary>
        /// <returns></returns>
        public static object? MapperDataConvert(MapperProvider provider, string fromType, string toType, object value)
        {
            try
            {
                var method = MapMethod(provider);

                var orgType = Type.GetType(fromType, true, true);
                var targetType = Type.GetType(toType, true, true);
                if (orgType == null || targetType == null)
                    return null;

                method = method?.MakeGenericMethod(new Type[] { orgType, targetType });
                return method?.Invoke(provider.Mapper, new object[] { value });
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public static object? MapperDataConvert(MapperProvider provider, Type fromType, Type toType, object value)
        {
            try
            {
                var method = MapMethod(provider);
                if (fromType == null || toType == null)
                    return null;

                method = method?.MakeGenericMethod(new Type[] { fromType, toType });
                return method?.Invoke(provider.Mapper, new object[] { value });
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}