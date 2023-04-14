namespace DRsoft.Engine.Model.Error
{
    /// <summary>
    /// 组件的异常
    /// </summary>
    public class ErrorPlugin
    {
        public bool CalibrationPlugin;
        public bool CameraHikvisionPlugin;
        public bool ControllerBeckhoffPlugin;
        public bool PlcAdsAdapterPlugin;
        public bool SiemensAdapter;

        public ErrorPlugin()
        {
            CalibrationPlugin = false;
            CameraHikvisionPlugin = false;
            ControllerBeckhoffPlugin = false;
            PlcAdsAdapterPlugin = false;
            SiemensAdapter = false;
        }
    }
}