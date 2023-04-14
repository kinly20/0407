using DRsoft.Engine.Core.Camera;

namespace DRsoft.Engine.Plugin.CameraHikvision.Implementation
{
    /// <summary>
    /// 海康视觉算法插件
    /// </summary>
    public class VisualDisposeHikvision : AbstractCameraVisual
    {
        public override string Scope => "VisualDisposeHikvision";

        public VisualDisposeHikvision(CameraConfig config)
            : base(config)
        {
        }
    }
}