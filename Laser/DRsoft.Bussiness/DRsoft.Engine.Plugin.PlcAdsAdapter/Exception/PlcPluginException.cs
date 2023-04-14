using DRsoft.Runtime.Core.Platform.Exceptions;


namespace DRsoft.Engine.Plugin.PlcAdsAdapter.Exception
{
    /// <summary>
    /// 
    /// </summary>
    public class PlcPluginException : PlatformException
    {
        public PlcPluginException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}