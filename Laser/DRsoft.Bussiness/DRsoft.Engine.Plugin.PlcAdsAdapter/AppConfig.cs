using DRsoft.Engine.Plugin.PlcAdsAdapter.Configurations;

namespace DRsoft.Engine.Plugin.PlcAdsAdapter
{
    /// <summary>
    /// 配置文件
    /// </summary>
    public class AppConfig
    {
        // public static IConfigurationRoot Configuration { get; set; }


        public static AdsConfigOption Options { get; internal set; }
    }
}