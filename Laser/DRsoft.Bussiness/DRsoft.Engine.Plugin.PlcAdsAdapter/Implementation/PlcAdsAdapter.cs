using TwinCAT.Ads;
using DRsoft.Runtime.Core.Platform.Plc;
using DRsoft.Runtime.Core.Platform.Exceptions;
using System.Collections.Generic;
using DRsoft.Engine.Plugin.PlcAdsAdapter.Configurations;
using DRsoft.Engine.Plugin.PlcAdsAdapter.Map;

namespace DRsoft.Engine.Plugin.PlcAdsAdapter.Implementation
{
    /// <summary>
    /// 倍福适配器
    /// </summary>
    public class PlcAdsAdapter : AbstractProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public PlcAdsAdapter(AdsConfigOption config, MapperProvider provider)
            : base(config, provider)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Initialize()
        {
            // 连接状态
            if (!IsConnected) Connect();
            // 创建变量句柄
            if (IsConnected) IniteVariableHandle();
        }

        public override Dictionary<string, object> ReadAll()
        {
            return null;
        }

        /// <summary>
        /// 读指定变量的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public override T Read<T>(PlcContext context)
        {
            string readIndex = "";
            try
            {
                //1、从配置映射缓存中获取当前映射
                var mapConfig = base.MapConfig(context);

                //2、从下位机读取数据
                readIndex = "上位机数据类型:" + context.VarName;
                var value = TcAdsClient.ReadAny(mapConfig.Handle, mapConfig.XwType);
                if (!mapConfig.ShouldMap)
                    return (T)value;

                //3、将下位机数据类型转成上位机类型
                return (T)MapperHelper.MapperDataConvert(MapperProvider, mapConfig.XwType, mapConfig.SwType, value);
            }
            catch (System.Exception ex)
            {
                TriggerException(new ErrorInfo
                {
                    Type = ErrorType.ReadError,
                    Message = "PlcSiemens读取数据失败！" + readIndex,
                    Scope = Scope
                }, ex);

                return default;
            }
        }

        /// <summary>
        /// 向PLC写值
        /// </summary>
        /// <param name="context"></param>
        public override bool Write(PlcContext context)
        {
            string writeIndex = "";
            try
            {
                //1、从配置映射缓存中获取当前映射
                var mapConfig = base.MapConfig(context);

                //2、将上位机数据类型转换为下位机               
                var afterConvertValue = context.VarValue;
                if (mapConfig.ShouldMap)
                    MapperHelper.MapperDataConvert(MapperProvider, mapConfig.SwType, mapConfig.XwType,
                        context.VarValue);

                if (afterConvertValue == null) return false;

                //3、下发数据
                writeIndex = "上位数据类型:" + context.VarName;
                TcAdsClient.WriteAny(mapConfig.Handle, afterConvertValue);
                return true;
            }
            catch (System.Exception ex)
            {
                TriggerException(new ErrorInfo
                {
                    Type = ErrorType.WriteError,
                    Message = "PlcSiemens下发数据失败！" + writeIndex,
                    Scope = Scope
                }, ex);
                return false;
            }
        }

        /// <summary>
        /// 监听变量变化
        /// </summary>
        /// <param name="variableName"></param>
        public override void AddNotification(string variableName)
        {
            try
            {
                // 获取当前配置
                var mapconfig = MapperProvider.Provider.FindByKey(variableName);
                if (mapconfig == null) return;

                // 创建变量监听
                var customerData = new object();
                if (mapconfig.NotifyHandle > 0)
                    TcAdsClient.DeleteDeviceNotification(mapconfig.NotifyHandle);

                var notifyHandle = TcAdsClient.AddDeviceNotificationEx($"{Config.Prefix}{variableName}",
                    AdsTransMode.OnChange, CycleTime, DelayTime, customerData, mapconfig.XwType);
                mapconfig.NotifyHandle = notifyHandle;

                MapperProvider.Provider.AddOrUpdate(mapconfig);
            }
            catch (System.Exception ex)
            {
                TriggerException(new ErrorInfo
                {
                    Type = ErrorType.CreateNotifyError,
                    Message = "PLC创建节点监听异常！",
                    Scope = Scope
                }, ex);
            }
        }
    }
}