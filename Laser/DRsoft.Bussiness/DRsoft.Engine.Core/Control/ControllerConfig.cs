using DRsoft.Engine.Model.Controller;
using DRsoft.Runtime.Core.Platform.Config;


namespace DRsoft.Engine.Core.Control
{
    /// <summary>
    /// 控制器配置
    /// </summary>
    public class ControllerConfig : ConfigEventBase, IConfigExt<ControllerConfig>// CompareBase,
    {
        public StParam ControllerParam { get; set; } = new StParam();
        public StAxis_Par ParaAxisGantry11 { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisGantry12 { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisGantry21 { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisGantry22 { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisAlign11 { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisAlign12 { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisAlign21 { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisAlign22 { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisCamShutter1 { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisCamShutter2 { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisZ1 { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisZ2 { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisUwLift { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisUw { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisRwLift { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisRw { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisClean { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisPowerMeter { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisUwSteer { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisPeeling1 { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisStationABelt { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisPeeling2 { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisStationBBelt { get; set; } = new StAxis_Par();
        public StAxis_Par ParaAxisRwSteer { get; set; } = new StAxis_Par();
        /// <summary>
        /// 比较控制器配置是否变更
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool Changed(ControllerConfig obj)
        {
            if (obj == null) return false;
            return ControllerParam.Changed(obj.ControllerParam) ||
                   ParaAxisGantry11.Changed(obj.ParaAxisGantry11) ||
                   ParaAxisGantry12.Changed(obj.ParaAxisGantry12) ||
                   ParaAxisGantry21.Changed(obj.ParaAxisGantry21) ||
                   ParaAxisGantry22.Changed(obj.ParaAxisGantry22) ||
                   ParaAxisAlign11.Changed(obj.ParaAxisAlign11) ||
                   ParaAxisAlign12.Changed(obj.ParaAxisAlign12) ||
                   ParaAxisAlign21.Changed(obj.ParaAxisAlign21) ||
                   ParaAxisAlign22.Changed(obj.ParaAxisAlign22) ||
                   ParaAxisCamShutter1.Changed(obj.ParaAxisCamShutter1) ||
                   ParaAxisCamShutter2.Changed(obj.ParaAxisCamShutter2) ||
                   ParaAxisZ1.Changed(obj.ParaAxisZ1) ||
                   ParaAxisZ2.Changed(obj.ParaAxisZ2) ||
                   ParaAxisUwLift.Changed(obj.ParaAxisUwLift) ||
                   ParaAxisUw.Changed(obj.ParaAxisUw) ||
                   ParaAxisRwLift.Changed(obj.ParaAxisRwLift) ||
                   ParaAxisRw.Changed(obj.ParaAxisRw) ||
                   ParaAxisClean.Changed(obj.ParaAxisClean) ||
                   ParaAxisPowerMeter.Changed(obj.ParaAxisPowerMeter) ||
                   ParaAxisUwSteer.Changed(obj.ParaAxisUwSteer) ||
                   ParaAxisPeeling1.Changed(obj.ParaAxisPeeling1) ||
                   ParaAxisStationABelt.Changed(obj.ParaAxisStationABelt) ||
                   ParaAxisPeeling2.Changed(obj.ParaAxisPeeling2) ||
                   ParaAxisStationBBelt.Changed(obj.ParaAxisStationBBelt) ||
                   ParaAxisRwSteer.Changed(obj.ParaAxisRwSteer);
        }

        /// <summary>
        /// 配置深拷贝
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ControllerConfig Clone()
        {
            return new ControllerConfig
            {
                ControllerParam = this.ControllerParam.Clone(),
                ParaAxisGantry11 = this.ParaAxisGantry11.Clone(),
                ParaAxisGantry12 = this.ParaAxisGantry12.Clone(),
                ParaAxisGantry21 = this.ParaAxisGantry21.Clone(),
                ParaAxisGantry22 = this.ParaAxisGantry22.Clone(),
                ParaAxisAlign11 = this.ParaAxisAlign11.Clone(),
                ParaAxisAlign12 = this.ParaAxisAlign12.Clone(),
                ParaAxisAlign21 = this.ParaAxisAlign21.Clone(),
                ParaAxisAlign22 = this.ParaAxisAlign22.Clone(),
                ParaAxisCamShutter1 = this.ParaAxisCamShutter1.Clone(),
                ParaAxisCamShutter2 = this.ParaAxisCamShutter2.Clone(),
                ParaAxisZ1 = this.ParaAxisZ1.Clone(),
                ParaAxisZ2 = this.ParaAxisZ2.Clone(),
                ParaAxisUwLift = this.ParaAxisUwLift.Clone(),
                ParaAxisUw = this.ParaAxisUw.Clone(),
                ParaAxisRwLift = this.ParaAxisRwLift.Clone(),
                ParaAxisRw = this.ParaAxisRw.Clone(),
                ParaAxisClean = this.ParaAxisClean.Clone(),
                ParaAxisPowerMeter = this.ParaAxisPowerMeter.Clone(),
                ParaAxisUwSteer = this.ParaAxisUwSteer.Clone(),
                ParaAxisPeeling1 = this.ParaAxisPeeling1.Clone(),
                ParaAxisStationABelt = this.ParaAxisStationABelt.Clone(),
                ParaAxisPeeling2 = this.ParaAxisPeeling2.Clone(),
                ParaAxisStationBBelt = this.ParaAxisStationBBelt.Clone(),
                ParaAxisRwSteer = this.ParaAxisRwSteer.Clone()
            };
        }
    }
}
