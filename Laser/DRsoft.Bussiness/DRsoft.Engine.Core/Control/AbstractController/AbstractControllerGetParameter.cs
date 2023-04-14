using DRsoft.Engine.Core.Interface;
using DRsoft.Engine.Model.Enum;
using DRsoft.Runtime.Core.Platform.Plugin;
using DRsoftProperty = DRsoft.Runtime.Core.Platform;

namespace DRsoft.Engine.Core.Control.AbstractController
{
    public abstract partial class AbstractController : IController, IPlugin, DRsoftProperty.Bind.INotifyPropertyChanged
    {
        /// <summary>
        /// 更新Controller的配置
        /// </summary>
        /// <param name="config"></param>
        public virtual void UpdateControllerConfig(ControllerConfig config)
        {
            var curConfig = config.Clone();
            bool init = false;
            GetParameterFromConfig(curConfig, init);
            Config = curConfig;
            CmdParam = Config.ControllerParam;
            ParaAxisGantry11 = Config.ParaAxisGantry11;
            ParaAxisGantry12 = Config.ParaAxisGantry12;
            ParaAxisGantry21 = Config.ParaAxisGantry21;
            ParaAxisGantry22 = Config.ParaAxisGantry22;
            ParaAxisAlign11 = Config.ParaAxisAlign11;
            ParaAxisAlign12 = Config.ParaAxisAlign12;
            ParaAxisAlign21 = Config.ParaAxisAlign21;
            ParaAxisAlign22 = Config.ParaAxisAlign22;
            ParaAxisCamShutter1 = Config.ParaAxisCamShutter1;
            ParaAxisCamShutter2 = Config.ParaAxisCamShutter2;
            ParaAxisZ1 = Config.ParaAxisZ1;
            ParaAxisZ2 = Config.ParaAxisZ2;
            ParaAxisUwLift = Config.ParaAxisUwLift;
            ParaAxisUw = Config.ParaAxisUw;
            ParaAxisRwLift = Config.ParaAxisRwLift;
            ParaAxisRw = Config.ParaAxisRw;
            ParaAxisClean = Config.ParaAxisClean;
            ParaAxisPowerMeter = Config.ParaAxisPowerMeter;
            ParaAxisUwSteer = Config.ParaAxisUwSteer;
            ParaAxisPeeling1 = Config.ParaAxisPeeling1;
            ParaAxisStationABelt = Config.ParaAxisStationABelt;
            ParaAxisPeeling2 = Config.ParaAxisPeeling2;
            ParaAxisStationBBelt = Config.ParaAxisStationBBelt;
            ParaAxisRwSteer = Config.ParaAxisRwSteer;
            for (int i = 1; i <= 54; i++)
            {
                WriteToControllerByIndex((ControllerInputIndex)i);
            }
        }

        /// <summary>
        /// 从Config获取PLC各个配置参数
        /// initFlag为true，表示初始化，参数直接写入，不管当前参数与原来参数是否一致
        /// </summary>
        /// <param name="config"></param>
        public virtual void GetParameterFromConfig(ControllerConfig config, bool initFlag)
        {
            var curConfig = config.Clone();

            Config = curConfig;
        }
    }
}