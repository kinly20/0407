using Caliburn.Micro;
using DRsoft.Modeling.Metadata.Models.Config;
using DRsoft.Runtime.Core.Platform.Events;
using DRsoft.Runtime.Core.Platform.Logging;
using Engine.Models;
using Engine.Transfer;
using Engine.ViewModels.DebugPageComponent;
using JetBrains.Annotations;
using StAxisParameter = DRsoft.Modeling.Metadata.Models.Config.StAxisParameter;

namespace Engine.ViewModels.ParameterPageComponent
{
    public class ParameterDetailViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public ParameterPageViewModel Ppvm;
        public MainWindowViewModel Mwvm;
        public PubSubEvent<WindowMessagePush> MarkingMateEventManager = null!;
        public PubSubEvent<Login> RoleChangeEventManager;

        private readonly object _lockobj = new object();

        public ParameterDetailViewModel(IWindowManager iwindow, MainWindowViewModel mwm, ParameterPageViewModel ppm)
        {

            this.WindowManager = iwindow;
            this.Ppvm = ppm;
            this.Mwvm = mwm;
            RoleChangeEventManager = Mwvm.Aggregator.GetEvent<PubSubEvent<Login>>();
            RoleChangeEventManager.Subscribe(p =>
            {
                Role = p;
            });
            ControllerParam = new ST_Param();
            PcParam = new StPcParam();
            ParaAxisGantry11 = new StAxisParameter();
            ParaAxisGantry12 = new StAxisParameter();
            ParaAxisGantry21 = new StAxisParameter();
            ParaAxisGantry22 = new StAxisParameter();
            ParaAxisAlign11 = new StAxisParameter();
            ParaAxisAlign12 = new StAxisParameter();
            ParaAxisAlign21 = new StAxisParameter();
            ParaAxisAlign22 = new StAxisParameter();
            ParaAxisCamShutter1 = new StAxisParameter();
            ParaAxisCamShutter2 = new StAxisParameter();
            ParaAxisZ1 = new StAxisParameter();
            ParaAxisZ2 = new StAxisParameter();
            ParaAxisUwLift = new StAxisParameter();
            ParaAxisUw = new StAxisParameter();
            ParaAxisRwLift = new StAxisParameter();
            ParaAxisRw = new StAxisParameter();
            ParaAxisClean = new StAxisParameter();
            ParaAxisPowerMeter = new StAxisParameter();
            ParaAxisUwSteer = new StAxisParameter();
            ParaAxisPeeling1 = new StAxisParameter();
            ParaAxisStationABelt = new StAxisParameter();
            ParaAxisPeeling2 = new StAxisParameter();
            ParaAxisStationBBelt = new StAxisParameter();
            ParaAxisRwSteer = new StAxisParameter();
            ListRecipeNote = Method.LisRecipes;
        }


        #region 属性绑定
        private InteractiveDataModel? _interactiveData;

        public InteractiveDataModel? InteractiveData
        {
            get
            {
                _interactiveData = this.Mwvm.InteractiveData;
                return _interactiveData;
            }
            set
            {
                _interactiveData = value;
                NotifyOfPropertyChange(() => InteractiveData);
                this.Mwvm.InteractiveData = _interactiveData;
            }
        }

        private Login? role = new Login() { UserName = "Observer", Password = "123", DebugLimit = false, ParamSetLimit = false, PhotoLimit = false, MarkingLimit = false };
        public Login? Role
        {
            get { return role; }
            set
            {
                role = value;
                NotifyOfPropertyChange(() => Role);
            }
        }

        private ST_Param? _controllerParam;

        public ST_Param? ControllerParam
        {
            get
            {
                _controllerParam = this.Mwvm.ControlConfig.ControllerParam;
                return _controllerParam;
            }
            set
            {
                _controllerParam = value;
                NotifyOfPropertyChange(() => ControllerParam);
                this.Mwvm.ControlConfig.ControllerParam = _controllerParam!;
            }
        }

        private StPcParam? _pcParam;

        public StPcParam? PcParam
        {
            get
            {
                _pcParam = this.Mwvm.EngineConfig.PcParam;
                return _pcParam;
            }
            set
            {
                _pcParam = value;
                NotifyOfPropertyChange(() => PcParam);
                this.Mwvm.EngineConfig.PcParam = _pcParam!;
            }
        }

        private StAxisParameter? _paraAxisGantry11;
        public StAxisParameter? ParaAxisGantry11
        {
            get
            {
                _paraAxisGantry11 = this.Mwvm.ControlConfig.ParaAxisGantry11;
                return _paraAxisGantry11;
            }
            set
            {
                _paraAxisGantry11 = value;
                NotifyOfPropertyChange(() => ParaAxisGantry11);
                this.Mwvm.ControlConfig.ParaAxisGantry11 = _paraAxisGantry11!;
            }
        }
        private StAxisParameter? _paraAxisGantry12;
        public StAxisParameter? ParaAxisGantry12
        {
            get
            {
                _paraAxisGantry12 = this.Mwvm.ControlConfig.ParaAxisGantry12;
                return _paraAxisGantry12;
            }
            set
            {
                _paraAxisGantry12 = value;
                NotifyOfPropertyChange(() => ParaAxisGantry12);
                this.Mwvm.ControlConfig.ParaAxisGantry12 = _paraAxisGantry12!;
            }
        }
        private StAxisParameter? _paraAxisGantry21;
        public StAxisParameter? ParaAxisGantry21
        {
            get
            {
                _paraAxisGantry21 = this.Mwvm.ControlConfig.ParaAxisGantry21;
                return _paraAxisGantry21;
            }
            set
            {
                _paraAxisGantry21 = value;
                NotifyOfPropertyChange(() => ParaAxisGantry21);
                this.Mwvm.ControlConfig.ParaAxisGantry21 = _paraAxisGantry21!;
            }
        }
        private StAxisParameter? _paraAxisGantry22;
        public StAxisParameter? ParaAxisGantry22
        {
            get
            {
                _paraAxisGantry22 = this.Mwvm.ControlConfig.ParaAxisGantry22;
                return _paraAxisGantry22;
            }
            set
            {
                _paraAxisGantry22 = value;
                NotifyOfPropertyChange(() => ParaAxisGantry22);
                this.Mwvm.ControlConfig.ParaAxisGantry22 = _paraAxisGantry22!;
            }
        }
        private StAxisParameter? _paraAxisAlign11;
        public StAxisParameter? ParaAxisAlign11
        {
            get
            {
                _paraAxisAlign11 = this.Mwvm.ControlConfig.ParaAxisAlign11;
                return _paraAxisAlign11;
            }
            set
            {
                _paraAxisAlign11 = value;
                NotifyOfPropertyChange(() => ParaAxisAlign11);
                this.Mwvm.ControlConfig.ParaAxisAlign11 = _paraAxisAlign11!;
            }
        }
        private StAxisParameter? _paraAxisAlign12;
        public StAxisParameter? ParaAxisAlign12
        {
            get
            {
                _paraAxisAlign12 = this.Mwvm.ControlConfig.ParaAxisAlign12;
                return _paraAxisAlign12;
            }
            set
            {
                _paraAxisAlign12 = value;
                NotifyOfPropertyChange(() => ParaAxisAlign12);
                this.Mwvm.ControlConfig.ParaAxisAlign12 = _paraAxisAlign12!;
            }
        }
        private StAxisParameter? _paraAxisAlign21;
        public StAxisParameter? ParaAxisAlign21
        {
            get
            {
                _paraAxisAlign21 = this.Mwvm.ControlConfig.ParaAxisAlign21;
                return _paraAxisAlign21;
            }
            set
            {
                _paraAxisAlign21 = value;
                NotifyOfPropertyChange(() => ParaAxisAlign21);
                this.Mwvm.ControlConfig.ParaAxisAlign21 = _paraAxisAlign21!;
            }
        }
        private StAxisParameter? _paraAxisAlign22;
        public StAxisParameter? ParaAxisAlign22
        {
            get
            {
                _paraAxisAlign22 = this.Mwvm.ControlConfig.ParaAxisAlign22;
                return _paraAxisAlign22;
            }
            set
            {
                _paraAxisAlign22 = value;
                NotifyOfPropertyChange(() => ParaAxisAlign22);
                this.Mwvm.ControlConfig.ParaAxisAlign22 = _paraAxisAlign22!;
            }
        }
        private StAxisParameter? _paraAxisCamShutter1;
        public StAxisParameter? ParaAxisCamShutter1
        {
            get
            {
                _paraAxisCamShutter1 = this.Mwvm.ControlConfig.ParaAxisCamShutter1;
                return _paraAxisCamShutter1;
            }
            set
            {
                _paraAxisCamShutter1 = value;
                NotifyOfPropertyChange(() => ParaAxisCamShutter1);
                this.Mwvm.ControlConfig.ParaAxisCamShutter1 = _paraAxisCamShutter1!;
            }
        }
        private StAxisParameter? _paraAxisCamShutter2;
        public StAxisParameter? ParaAxisCamShutter2
        {
            get
            {
                _paraAxisCamShutter2 = this.Mwvm.ControlConfig.ParaAxisCamShutter2;
                return _paraAxisCamShutter2;
            }
            set
            {
                _paraAxisCamShutter2 = value;
                NotifyOfPropertyChange(() => ParaAxisCamShutter2);
                this.Mwvm.ControlConfig.ParaAxisCamShutter2 = _paraAxisCamShutter2!;
            }
        }
        private StAxisParameter? _paraAxisZ1;
        public StAxisParameter? ParaAxisZ1
        {
            get
            {
                _paraAxisZ1 = this.Mwvm.ControlConfig.ParaAxisZ1;
                return _paraAxisZ1;
            }
            set
            {
                _paraAxisZ1 = value;
                NotifyOfPropertyChange(() => ParaAxisZ1);
                this.Mwvm.ControlConfig.ParaAxisZ1 = _paraAxisZ1!;
            }
        }
        private StAxisParameter? _paraAxisZ2;
        public StAxisParameter? ParaAxisZ2
        {
            get
            {
                _paraAxisZ2 = this.Mwvm.ControlConfig.ParaAxisZ2;
                return _paraAxisZ2;
            }
            set
            {
                _paraAxisZ2 = value;
                NotifyOfPropertyChange(() => ParaAxisZ2);
                this.Mwvm.ControlConfig.ParaAxisZ2 = _paraAxisZ2!;
            }
        }
        private StAxisParameter? _paraAxisUwLift;
        public StAxisParameter? ParaAxisUwLift
        {
            get
            {
                _paraAxisUwLift = this.Mwvm.ControlConfig.ParaAxisUwLift;
                return _paraAxisUwLift;
            }
            set
            {
                _paraAxisUwLift = value;
                NotifyOfPropertyChange(() => ParaAxisUwLift);
                this.Mwvm.ControlConfig.ParaAxisUwLift = _paraAxisUwLift!;
            }
        }
        private StAxisParameter? _paraAxisUw;
        public StAxisParameter? ParaAxisUw
        {
            get
            {
                _paraAxisUw = this.Mwvm.ControlConfig.ParaAxisUw;
                return _paraAxisUw;
            }
            set
            {
                _paraAxisUw = value;
                NotifyOfPropertyChange(() => ParaAxisUw);
                this.Mwvm.ControlConfig.ParaAxisUw = _paraAxisUw!;
            }
        }
        private StAxisParameter? _paraAxisRwLift;
        public StAxisParameter? ParaAxisRwLift
        {
            get
            {
                _paraAxisRwLift = this.Mwvm.ControlConfig.ParaAxisRwLift;
                return _paraAxisRwLift;
            }
            set
            {
                _paraAxisRwLift = value;
                NotifyOfPropertyChange(() => ParaAxisRwLift);
                this.Mwvm.ControlConfig.ParaAxisRwLift = _paraAxisRwLift!;
            }
        }
        private StAxisParameter? _paraAxisRw;
        public StAxisParameter? ParaAxisRw
        {
            get
            {
                _paraAxisRw = this.Mwvm.ControlConfig.ParaAxisRw;
                return _paraAxisRw;
            }
            set
            {
                _paraAxisRw = value;
                NotifyOfPropertyChange(() => ParaAxisRw);
                this.Mwvm.ControlConfig.ParaAxisRw = _paraAxisRw!;
            }
        }
        private StAxisParameter? _paraAxisClean;
        public StAxisParameter? ParaAxisClean
        {
            get
            {
                _paraAxisClean = this.Mwvm.ControlConfig.ParaAxisClean;
                return _paraAxisClean;
            }
            set
            {
                _paraAxisClean = value;
                NotifyOfPropertyChange(() => ParaAxisClean);
                this.Mwvm.ControlConfig.ParaAxisClean = _paraAxisClean!;
            }
        }
        private StAxisParameter? _paraAxisPowerMeter;
        public StAxisParameter? ParaAxisPowerMeter
        {
            get
            {
                _paraAxisPowerMeter = this.Mwvm.ControlConfig.ParaAxisPowerMeter;
                return _paraAxisPowerMeter;
            }
            set
            {
                _paraAxisPowerMeter = value;
                NotifyOfPropertyChange(() => ParaAxisPowerMeter);
                this.Mwvm.ControlConfig.ParaAxisPowerMeter = _paraAxisPowerMeter!;
            }
        }
        private StAxisParameter? _paraAxisUwSteer;
        public StAxisParameter? ParaAxisUwSteer
        {
            get
            {
                _paraAxisUwSteer = this.Mwvm.ControlConfig.ParaAxisUwSteer;
                return _paraAxisUwSteer;
            }
            set
            {
                _paraAxisUwSteer = value;
                NotifyOfPropertyChange(() => ParaAxisUwSteer);
                this.Mwvm.ControlConfig.ParaAxisUwSteer = _paraAxisUwSteer!;
            }
        }
        private StAxisParameter? _paraAxisPeeling1;
        public StAxisParameter? ParaAxisPeeling1
        {
            get
            {
                _paraAxisPeeling1 = this.Mwvm.ControlConfig.ParaAxisPeeling1;
                return _paraAxisPeeling1;
            }
            set
            {
                _paraAxisPeeling1 = value;
                NotifyOfPropertyChange(() => ParaAxisPeeling1);
                this.Mwvm.ControlConfig.ParaAxisPeeling1 = _paraAxisPeeling1!;
            }
        }
        private StAxisParameter? _paraAxisStationABelt;
        public StAxisParameter? ParaAxisStationABelt
        {
            get
            {
                _paraAxisStationABelt = this.Mwvm.ControlConfig.ParaAxisStationABelt;
                return _paraAxisStationABelt;
            }
            set
            {
                _paraAxisStationABelt = value;
                NotifyOfPropertyChange(() => ParaAxisStationABelt);
                this.Mwvm.ControlConfig.ParaAxisStationABelt = _paraAxisStationABelt!;
            }
        }
        private StAxisParameter? _paraAxisPeeling2;
        public StAxisParameter? ParaAxisPeeling2
        {
            get
            {
                _paraAxisPeeling2 = this.Mwvm.ControlConfig.ParaAxisPeeling2;
                return _paraAxisPeeling2;
            }
            set
            {
                _paraAxisPeeling2 = value;
                NotifyOfPropertyChange(() => ParaAxisPeeling2);
                this.Mwvm.ControlConfig.ParaAxisPeeling2 = _paraAxisPeeling2!;
            }
        }
        private StAxisParameter? _araAxisStationBBelt;
        public StAxisParameter? ParaAxisStationBBelt
        {
            get
            {
                _araAxisStationBBelt = this.Mwvm.ControlConfig.ParaAxisStationBBelt;
                return _araAxisStationBBelt;
            }
            set
            {
                _araAxisStationBBelt = value;
                NotifyOfPropertyChange(() => ParaAxisStationBBelt);
                this.Mwvm.ControlConfig.ParaAxisStationBBelt = _araAxisStationBBelt!;
            }
        }
        private StAxisParameter? _araAxisRwSteer;
        public StAxisParameter? ParaAxisRwSteer
        {
            get
            {
                _araAxisRwSteer = this.Mwvm.ControlConfig.ParaAxisRwSteer;
                return _araAxisRwSteer;
            }
            set
            {
                _araAxisRwSteer = value;
                NotifyOfPropertyChange(() => ParaAxisRwSteer);
                this.Mwvm.ControlConfig.ParaAxisRwSteer = _araAxisRwSteer!;
            }
        }

        private List<RecipeNote> _listRecipeNote = null!;

        public List<RecipeNote> ListRecipeNote
        {
            get => this._listRecipeNote;
            set
            {
                _listRecipeNote = value;
                NotifyOfPropertyChange(() => ListRecipeNote);
            }
        }
        #endregion

        #region 方法集

        [UsedImplicitly]
        private void LoadRecipeParameter()
        {
            try
            {

            }
            catch (Exception e)
            {
                MessageBox.Show($"加载配方参数{e}");
                this.Mwvm.Log?.ErrorFormat($"加载配方参数{e}");
            }
        }

        public void ParaSave()
        {
            Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory!;
            SaveConfig();
            Method.BackUpPara();
        }

        private void SaveConfig()
        {
            lock (_lockobj)
            {
                try
                {
                    var recipe = InteractiveData!.RecipeText;
                    if (Method.GuidRecipeConfig.ContainsKey($"{recipe}"))
                    {
                        this.Mwvm.EngineAppService.metadataGuid = Method.GuidRecipeConfig[$"{recipe}0"];
                        this.Mwvm.ControllAppService.metadataGuid = Method.GuidRecipeConfig[$"{recipe}1"];
                        this.Mwvm.VisionAppService.MetadataGuid = Method.GuidRecipeConfig[$"{recipe}2"];
                    }
                    this.Mwvm.EngineAppService.CheckOut();
                    this.Mwvm.ControllAppService.CheckOut();
                    this.Mwvm.VisionAppService.CheckOut();
                    if (Method.DicEngineConfig.ContainsKey($"{recipe}0")) Method.DicEngineConfig[$"{recipe}0"] = this.Mwvm.EngineConfig;
                    if (Method.DicControlConfig.ContainsKey($"{recipe}1")) Method.DicControlConfig[$"{recipe}1"] = this.Mwvm.ControlConfig;
                    if (Method.DicVisionCalibrationConfig.ContainsKey($"{recipe}2")) Method.DicVisionCalibrationConfig[$"{recipe}2"] = this.Mwvm.VisionCalibrationConfig;

                    //保存对应配方参数
                    this.Mwvm.EngineAppService.Save(this.Mwvm.EngineConfig);
                    this.Mwvm.ControllAppService.Save(this.Mwvm.ControlConfig);
                    this.Mwvm.VisionAppService.Save(this.Mwvm.VisionCalibrationConfig);

                    //if (Method.SelectRecipe != null & recipe != null && recipe.Equals(Method.SelectRecipe))
                    //{
                    //赋值页面
                    this.Mwvm.Config.IsVerbose = this.Mwvm.EngineConfig.IsVerbose;
                    this.Mwvm.Config.RefreshRate = this.Mwvm.EngineConfig.RefreshRate;
                    this.Mwvm.Config.PcParamConfig.ProductionType = this.Mwvm.EngineConfig.PcParam.ProductionType;
                    this.Mwvm.Config.PcParamConfig.MarkingPath = this.Mwvm.EngineConfig.PcParam.MarkingPath;
                    this.Mwvm.Config.PcParamConfig.MarkingNamePrefix = this.Mwvm.EngineConfig.PcParam.MarkingNamePrefix;
                    this.Mwvm.Config.PcParamConfig.LogOutTime = this.Mwvm.EngineConfig.PcParam.LogOutTime;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos1X = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos1X;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos1Y = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos1Y;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos2X = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos2X;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos2Y = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos2Y;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos3X = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos3X;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos3Y = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos3Y;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos4X = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos4X;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos4Y = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos4Y;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos5X = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos5X;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos5Y = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos5Y;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos6X = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos6X;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos6Y = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos6Y;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos7X = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos7X;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos7Y = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos7Y;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos8X = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos8X;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos8Y = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos8Y;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos9X = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos9X;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos9Y = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos9Y;  
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos10X = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos10X;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos10Y = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos10Y;  
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos11X = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos11X;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos11Y = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos11Y;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos12X = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos12X;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasurePos12Y = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasurePos12Y;
                    this.Mwvm.Config.PcParamConfig.PowerMeterInterval = this.Mwvm.EngineConfig.PcParam.PowerMeterInterval;
                    this.Mwvm.Config.PcParamConfig.Laser1Power = this.Mwvm.EngineConfig.PcParam.Laser1Power;
                    this.Mwvm.Config.PcParamConfig.Laser1Freq = this.Mwvm.EngineConfig.PcParam.Laser1Freq;
                    this.Mwvm.Config.PcParamConfig.Laser2Power = this.Mwvm.EngineConfig.PcParam.Laser2Power;
                    this.Mwvm.Config.PcParamConfig.Laser2Freq = this.Mwvm.EngineConfig.PcParam.Laser2Freq;
                    this.Mwvm.Config.PcParamConfig.Laser3Power = this.Mwvm.EngineConfig.PcParam.Laser3Power;
                    this.Mwvm.Config.PcParamConfig.Laser3Freq = this.Mwvm.EngineConfig.PcParam.Laser3Freq;
                    this.Mwvm.Config.PcParamConfig.Laser4Power = this.Mwvm.EngineConfig.PcParam.Laser4Power;
                    this.Mwvm.Config.PcParamConfig.Laser4Freq = this.Mwvm.EngineConfig.PcParam.Laser4Freq;
                    this.Mwvm.Config.PcParamConfig.Laser5Power = this.Mwvm.EngineConfig.PcParam.Laser5Power;
                    this.Mwvm.Config.PcParamConfig.Laser5Freq = this.Mwvm.EngineConfig.PcParam.Laser5Freq;
                    this.Mwvm.Config.PcParamConfig.Laser6Power = this.Mwvm.EngineConfig.PcParam.Laser6Power;
                    this.Mwvm.Config.PcParamConfig.Laser6Freq = this.Mwvm.EngineConfig.PcParam.Laser6Freq;
                    this.Mwvm.Config.PcParamConfig.Laser7Power = this.Mwvm.EngineConfig.PcParam.Laser7Power;
                    this.Mwvm.Config.PcParamConfig.Laser7Freq = this.Mwvm.EngineConfig.PcParam.Laser7Freq;
                    this.Mwvm.Config.PcParamConfig.Laser8Power = this.Mwvm.EngineConfig.PcParam.Laser8Power;
                    this.Mwvm.Config.PcParamConfig.Laser8Freq = this.Mwvm.EngineConfig.PcParam.Laser8Freq;
                    this.Mwvm.Config.PcParamConfig.Laser9Power = this.Mwvm.EngineConfig.PcParam.Laser9Power;
                    this.Mwvm.Config.PcParamConfig.Laser9Freq = this.Mwvm.EngineConfig.PcParam.Laser9Freq;
                    this.Mwvm.Config.PcParamConfig.Laser10Power = this.Mwvm.EngineConfig.PcParam.Laser10Power;
                    this.Mwvm.Config.PcParamConfig.Laser10Freq = this.Mwvm.EngineConfig.PcParam.Laser10Freq;
                    this.Mwvm.Config.PcParamConfig.Laser11Power = this.Mwvm.EngineConfig.PcParam.Laser11Power;
                    this.Mwvm.Config.PcParamConfig.Laser11Freq = this.Mwvm.EngineConfig.PcParam.Laser11Freq;
                    this.Mwvm.Config.PcParamConfig.Laser12Power = this.Mwvm.EngineConfig.PcParam.Laser12Power;
                    this.Mwvm.Config.PcParamConfig.Laser12Freq = this.Mwvm.EngineConfig.PcParam.Laser12Freq;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasureHl = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasureHl;
                    this.Mwvm.Config.PcParamConfig.PowerMeterMeasureLl = this.Mwvm.EngineConfig.PcParam.PowerMeterMeasureLl;
                    this.Mwvm.Config.PcParamConfig.PowerMeterRatio = this.Mwvm.EngineConfig.PcParam.PowerMeterRatio;
                    this.Mwvm.Config.PcParamConfig.PowerMeterPercent = this.Mwvm.EngineConfig.PcParam.PowerMeterPercent;
                    this.Mwvm.Config.PcParamConfig.IsSilicaWashed = this.Mwvm.EngineConfig.PcParam.IsSilicaWashed;
                    this.Mwvm.Config.PcParamConfig.IsDirtyPosMarked = this.Mwvm.EngineConfig.PcParam.IsDirtyPosMarked;
                    this.Mwvm.Config.PcParamConfig.VibraOfs1X = this.Mwvm.EngineConfig.PcParam.VibraOfs1X;
                    this.Mwvm.Config.PcParamConfig.VibraOfs1Y = this.Mwvm.EngineConfig.PcParam.VibraOfs1Y;
                    this.Mwvm.Config.PcParamConfig.VibraOfs1A = this.Mwvm.EngineConfig.PcParam.VibraOfs1A;
                    this.Mwvm.Config.PcParamConfig.VibraOfs2X = this.Mwvm.EngineConfig.PcParam.VibraOfs2X;
                    this.Mwvm.Config.PcParamConfig.VibraOfs2Y = this.Mwvm.EngineConfig.PcParam.VibraOfs2Y;
                    this.Mwvm.Config.PcParamConfig.VibraOfs2A = this.Mwvm.EngineConfig.PcParam.VibraOfs2A;
                    this.Mwvm.Config.PcParamConfig.VibraOfs3X = this.Mwvm.EngineConfig.PcParam.VibraOfs3X;
                    this.Mwvm.Config.PcParamConfig.VibraOfs3Y = this.Mwvm.EngineConfig.PcParam.VibraOfs3Y;
                    this.Mwvm.Config.PcParamConfig.VibraOfs3A = this.Mwvm.EngineConfig.PcParam.VibraOfs3A;
                    this.Mwvm.Config.PcParamConfig.VibraOfs4X = this.Mwvm.EngineConfig.PcParam.VibraOfs4X;
                    this.Mwvm.Config.PcParamConfig.VibraOfs4Y = this.Mwvm.EngineConfig.PcParam.VibraOfs4Y;
                    this.Mwvm.Config.PcParamConfig.VibraOfs4A = this.Mwvm.EngineConfig.PcParam.VibraOfs4A;
                    this.Mwvm.Config.PcParamConfig.VibraOfs5X = this.Mwvm.EngineConfig.PcParam.VibraOfs5X;
                    this.Mwvm.Config.PcParamConfig.VibraOfs5Y = this.Mwvm.EngineConfig.PcParam.VibraOfs5Y;
                    this.Mwvm.Config.PcParamConfig.VibraOfs5A = this.Mwvm.EngineConfig.PcParam.VibraOfs5A;
                    this.Mwvm.Config.PcParamConfig.VibraOfs6X = this.Mwvm.EngineConfig.PcParam.VibraOfs6X;
                    this.Mwvm.Config.PcParamConfig.VibraOfs6Y = this.Mwvm.EngineConfig.PcParam.VibraOfs6Y;
                    this.Mwvm.Config.PcParamConfig.VibraOfs6A = this.Mwvm.EngineConfig.PcParam.VibraOfs6A;
                    this.Mwvm.Config.PcParamConfig.VibraOfs7X = this.Mwvm.EngineConfig.PcParam.VibraOfs7X;
                    this.Mwvm.Config.PcParamConfig.VibraOfs7Y = this.Mwvm.EngineConfig.PcParam.VibraOfs7Y;
                    this.Mwvm.Config.PcParamConfig.VibraOfs7A = this.Mwvm.EngineConfig.PcParam.VibraOfs7A;
                    this.Mwvm.Config.PcParamConfig.VibraOfs8X = this.Mwvm.EngineConfig.PcParam.VibraOfs8X;
                    this.Mwvm.Config.PcParamConfig.VibraOfs8Y = this.Mwvm.EngineConfig.PcParam.VibraOfs8Y;
                    this.Mwvm.Config.PcParamConfig.VibraOfs8A = this.Mwvm.EngineConfig.PcParam.VibraOfs8A;
                    this.Mwvm.Config.PcParamConfig.VibraOfs9X = this.Mwvm.EngineConfig.PcParam.VibraOfs9X;
                    this.Mwvm.Config.PcParamConfig.VibraOfs9Y = this.Mwvm.EngineConfig.PcParam.VibraOfs9Y;
                    this.Mwvm.Config.PcParamConfig.VibraOfs9A = this.Mwvm.EngineConfig.PcParam.VibraOfs9A;
                    this.Mwvm.Config.PcParamConfig.VibraOfs10X = this.Mwvm.EngineConfig.PcParam.VibraOfs10X;
                    this.Mwvm.Config.PcParamConfig.VibraOfs10Y = this.Mwvm.EngineConfig.PcParam.VibraOfs10Y;
                    this.Mwvm.Config.PcParamConfig.VibraOfs10A = this.Mwvm.EngineConfig.PcParam.VibraOfs10A;
                    this.Mwvm.Config.PcParamConfig.VibraOfs11X = this.Mwvm.EngineConfig.PcParam.VibraOfs11X;
                    this.Mwvm.Config.PcParamConfig.VibraOfs11Y = this.Mwvm.EngineConfig.PcParam.VibraOfs11Y;
                    this.Mwvm.Config.PcParamConfig.VibraOfs11A = this.Mwvm.EngineConfig.PcParam.VibraOfs11A;
                    this.Mwvm.Config.PcParamConfig.VibraOfs12X = this.Mwvm.EngineConfig.PcParam.VibraOfs12X;
                    this.Mwvm.Config.PcParamConfig.VibraOfs12Y = this.Mwvm.EngineConfig.PcParam.VibraOfs12Y;
                    this.Mwvm.Config.PcParamConfig.VibraOfs12A = this.Mwvm.EngineConfig.PcParam.VibraOfs12A;
                    this.Mwvm.Config.PcParamConfig.CameraShootFailThresX = this.Mwvm.EngineConfig.PcParam.CameraShootFailThresX;
                    this.Mwvm.Config.PcParamConfig.CameraShootFailThresY = this.Mwvm.EngineConfig.PcParam.CameraShootFailThresY;
                    this.Mwvm.Config.PcParamConfig.CameraShootFailThresA = this.Mwvm.EngineConfig.PcParam.CameraShootFailThresA;

                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler11BasePos = this.Mwvm.ControlConfig.ControllerParam.Z11_RulerPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler12BasePos = this.Mwvm.ControlConfig.ControllerParam.Z12_RulerPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1WaitPos = this.Mwvm.ControlConfig.ControllerParam.Gantry1WaitPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationAGrabPos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationAGrabPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationAGrabPos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationAGrabPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationAMark1Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationAMark1Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationAMark2Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationAMark2Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationAMark3Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationAMark3Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationAMark4Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationAMark4Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationAMark5Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationAMark5Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationAMark6Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationAMark6Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationAMark7Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationAMark7Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationAMark8Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationAMark8Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationAMark1Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationAMark1Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationAMark2Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationAMark2Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationAMark3Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationAMark3Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationAMark4Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationAMark4Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationAMark5Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationAMark5Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationAMark6Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationAMark6Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationAMark7Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationAMark7Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationAMark8Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationAMark8Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Peeling1StartPos = this.Mwvm.ControlConfig.ControllerParam.Peeling1StartPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Peeling1EndPos = this.Mwvm.ControlConfig.ControllerParam.Peeling1EndPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Z1DownPos = this.Mwvm.ControlConfig.ControllerParam.Z1_DownPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Z1UpPos = this.Mwvm.ControlConfig.ControllerParam.Z1_UpPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler21BasePos = this.Mwvm.ControlConfig.ControllerParam.Z21_RulerPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler22BasePos = this.Mwvm.ControlConfig.ControllerParam.Z22_RulerPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2WaitPos = this.Mwvm.ControlConfig.ControllerParam.Gantry2WaitPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationBGrabPos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationBGrabPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationBGrabPos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationBGrabPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationBMark1Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationBMark1Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationBMark2Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationBMark2Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationBMark3Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationBMark3Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationBMark4Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationBMark4Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationBMark5Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationBMark5Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationBMark6Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationBMark6Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationBMark6Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationBMark6Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationBMark7Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationBMark7Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationBMark7Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationBMark7Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1StationBMark8Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry1StationBMark8Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationBMark1Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationBMark1Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationBMark2Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationBMark2Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationBMark3Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationBMark3Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationBMark4Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationBMark4Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationBMark5Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationBMark5Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationBMark6Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationBMark6Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationBMark7Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationBMark7Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2StationBMark8Pos = this.Mwvm.ControlConfig.ControllerParam.Gantry2StationBMark8Pos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Peeling2StartPos = this.Mwvm.ControlConfig.ControllerParam.Peeling2StartPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Peeling2EndPos = this.Mwvm.ControlConfig.ControllerParam.Peeling2EndPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Z2DownPos = this.Mwvm.ControlConfig.ControllerParam.Z2_DownPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Z2UpPos = this.Mwvm.ControlConfig.ControllerParam.Z2_UpPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.CamShutter1Pos0 = this.Mwvm.ControlConfig.ControllerParam.CamShutter1Pos0;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.CamShutter1Pos1 = this.Mwvm.ControlConfig.ControllerParam.CamShutter1Pos1;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.CamShutter1Pos2 = this.Mwvm.ControlConfig.ControllerParam.CamShutter1Pos2;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.CamShutter1Pos3 = this.Mwvm.ControlConfig.ControllerParam.CamShutter1Pos3;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.CamShutter2Pos0 = this.Mwvm.ControlConfig.ControllerParam.CamShutter2Pos0;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.CamShutter2Pos1 = this.Mwvm.ControlConfig.ControllerParam.CamShutter2Pos1;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.CamShutter2Pos2 = this.Mwvm.ControlConfig.ControllerParam.CamShutter2Pos2;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.CamShutter2Pos3 = this.Mwvm.ControlConfig.ControllerParam.CamShutter2Pos3;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.UwLiftUpPos = this.Mwvm.ControlConfig.ControllerParam.UwLiftUpPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.RwLiftUpPos = this.Mwvm.ControlConfig.ControllerParam.RwLiftUpPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.ProcessTimes = this.Mwvm.ControlConfig.ControllerParam.ProcessTimes;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.GrabTimeOutSet = this.Mwvm.ControlConfig.ControllerParam.GrabTimeOutSet;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.StationAVacOkDelay = this.Mwvm.ControlConfig.ControllerParam.StationA_VacOkDelay;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.StationBVacOkDelay = this.Mwvm.ControlConfig.ControllerParam.StationB_VacOkDelay;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.StationABlowDelay = this.Mwvm.ControlConfig.ControllerParam.StationA_BlowDelay;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.StationBBlowDelay = this.Mwvm.ControlConfig.ControllerParam.StationB_BlowDelay;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.AutoLeaserMeasureNum = this.Mwvm.ControlConfig.ControllerParam.AutoLeaserMeasureNum;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry1PowerMeterPos = this.Mwvm.ControlConfig.ControllerParam.Gantry1PowerMeterPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry2PowerMeterPos = this.Mwvm.ControlConfig.ControllerParam.Gantry2PowerMeterPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.LeftOffset = this.Mwvm.ControlConfig.ControllerParam.LeftOffset;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.MidOffset = this.Mwvm.ControlConfig.ControllerParam.MidOffset;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.RightOffset = this.Mwvm.ControlConfig.ControllerParam.RightOffset;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.PowerMeterMeasurePos1 = this.Mwvm.ControlConfig.ControllerParam.PowerMeterMeasurePos1;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.PowerMeterMeasurePos2 = this.Mwvm.ControlConfig.ControllerParam.PowerMeterMeasurePos2;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.PowerMeterMeasurePos3 = this.Mwvm.ControlConfig.ControllerParam.PowerMeterMeasurePos3;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.PowerMeterMeasurePos4 = this.Mwvm.ControlConfig.ControllerParam.PowerMeterMeasurePos4;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.PowerMeterMeasurePos5 = this.Mwvm.ControlConfig.ControllerParam.PowerMeterMeasurePos5;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.PowerMeterMeasurePos6 = this.Mwvm.ControlConfig.ControllerParam.PowerMeterMeasurePos6;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.UwTorqueSet = this.Mwvm.ControlConfig.ControllerParam.UwTorqueSet;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.RwTorqueSet = this.Mwvm.ControlConfig.ControllerParam.RwTorqueSet;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.TapeLength = this.Mwvm.ControlConfig.ControllerParam.TapeLength;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.StationPosADelay = this.Mwvm.ControlConfig.ControllerParam.StationPosADelay;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.StationPosBDelay = this.Mwvm.ControlConfig.ControllerParam.StationPosBDelay;

                    this.Mwvm.Config.ControllerConfig.ControllerParam.UwTorqueModeVeloLimt = this.Mwvm.ControlConfig.ControllerParam.UwTorqueModeVeloLimt;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.RwTorqueModeVeloLimt = this.Mwvm.ControlConfig.ControllerParam.RwTorqueModeVeloLimt;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.UwRadius_AnalogMax = this.Mwvm.ControlConfig.ControllerParam.UwRadius_AnalogMax;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.UwRadius_AnalogMin = this.Mwvm.ControlConfig.ControllerParam.UwRadius_AnalogMin;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.UwRadius_MeasurementMax = this.Mwvm.ControlConfig.ControllerParam.UwRadius_MeasurementMax;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.UwRadius_MeasurementMin = this.Mwvm.ControlConfig.ControllerParam.UwRadius_MeasurementMin;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.RwRadius_AnalogMax = this.Mwvm.ControlConfig.ControllerParam.RwRadius_AnalogMax;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.RwRadius_AnalogMin = this.Mwvm.ControlConfig.ControllerParam.RwRadius_AnalogMin;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.RwRadius_MeasurementMax = this.Mwvm.ControlConfig.ControllerParam.RwRadius_MeasurementMax;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.RwRadius_MeasurementMin = this.Mwvm.ControlConfig.ControllerParam.RwRadius_MeasurementMin;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.UwSteer_AnalogMax = this.Mwvm.ControlConfig.ControllerParam.UwSteer_AnalogMax;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.UwSteer_AnalogMin = this.Mwvm.ControlConfig.ControllerParam.UwSteer_AnalogMin;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.UwSteer_MeasurementMax = this.Mwvm.ControlConfig.ControllerParam.UwSteer_MeasurementMax;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.UwSteer_MeasurementMin = this.Mwvm.ControlConfig.ControllerParam.UwSteer_MeasurementMin;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.RwSteer_AnalogMax = this.Mwvm.ControlConfig.ControllerParam.RwSteer_AnalogMax;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.RwSteer_AnalogMin = this.Mwvm.ControlConfig.ControllerParam.RwSteer_AnalogMin;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.RwSteer_MeasurementMax = this.Mwvm.ControlConfig.ControllerParam.RwSteer_MeasurementMax;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.RwSteer_MeasurementMin = this.Mwvm.ControlConfig.ControllerParam.RwSteer_MeasurementMin;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler11_AnalogMax = this.Mwvm.ControlConfig.ControllerParam.Ruler11_AnalogMax;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler11_AnalogMin = this.Mwvm.ControlConfig.ControllerParam.Ruler11_AnalogMin;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler11_MeasurementMax = this.Mwvm.ControlConfig.ControllerParam.Ruler11_MeasurementMax;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler11_MeasurementMin = this.Mwvm.ControlConfig.ControllerParam.Ruler11_MeasurementMin;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler12_AnalogMax = this.Mwvm.ControlConfig.ControllerParam.Ruler12_AnalogMax;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler12_AnalogMin = this.Mwvm.ControlConfig.ControllerParam.Ruler12_AnalogMin;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler12_MeasurementMax = this.Mwvm.ControlConfig.ControllerParam.Ruler12_MeasurementMax;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler12_MeasurementMin = this.Mwvm.ControlConfig.ControllerParam.Ruler12_MeasurementMin;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler21_AnalogMax = this.Mwvm.ControlConfig.ControllerParam.Ruler21_AnalogMax;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler21_AnalogMin = this.Mwvm.ControlConfig.ControllerParam.Ruler21_AnalogMin;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler21_MeasurementMax = this.Mwvm.ControlConfig.ControllerParam.Ruler21_MeasurementMax;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler21_MeasurementMin = this.Mwvm.ControlConfig.ControllerParam.Ruler21_MeasurementMin;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler22_AnalogMax = this.Mwvm.ControlConfig.ControllerParam.Ruler22_AnalogMax;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler22_AnalogMin = this.Mwvm.ControlConfig.ControllerParam.Ruler22_AnalogMin;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler22_MeasurementMax = this.Mwvm.ControlConfig.ControllerParam.Ruler22_MeasurementMax;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Ruler22_MeasurementMin = this.Mwvm.ControlConfig.ControllerParam.Ruler22_MeasurementMin;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Align11WaitPos = this.Mwvm.ControlConfig.ControllerParam.Align11WaitPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Align12WaitPos = this.Mwvm.ControlConfig.ControllerParam.Align12WaitPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Align21WaitPos = this.Mwvm.ControlConfig.ControllerParam.Align21WaitPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Align22WaitPos = this.Mwvm.ControlConfig.ControllerParam.Align22WaitPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Z1_PeelingPos = this.Mwvm.ControlConfig.ControllerParam.Z1_PeelingPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Z2_PeelingPos = this.Mwvm.ControlConfig.ControllerParam.Z2_PeelingPos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry11BasePos = this.Mwvm.ControlConfig.ControllerParam.Gantry11BasePos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry12BasePos = this.Mwvm.ControlConfig.ControllerParam.Gantry12BasePos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry21BasePos = this.Mwvm.ControlConfig.ControllerParam.Gantry21BasePos;
                    this.Mwvm.Config.ControllerConfig.ControllerParam.Gantry22BasePos = this.Mwvm.ControlConfig.ControllerParam.Gantry22BasePos;



                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisGantry11.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisGantry11.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisGantry11.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisGantry11.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisGantry11.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisGantry11.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisGantry11.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisGantry11.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry11.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisGantry11.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisGantry12.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisGantry12.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisGantry12.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisGantry12.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisGantry12.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisGantry12.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisGantry12.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisGantry12.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry12.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisGantry12.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisGantry21.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisGantry21.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisGantry21.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisGantry21.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisGantry21.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisGantry21.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisGantry21.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisGantry21.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry21.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisGantry21.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisGantry22.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisGantry22.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisGantry22.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisGantry22.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisGantry22.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisGantry22.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisGantry22.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisGantry22.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisGantry22.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisGantry22.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisAlign11.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisAlign11.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisAlign11.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisAlign11.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisAlign11.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisAlign11.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisAlign11.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisAlign11.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign11.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisAlign11.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisAlign12.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisAlign12.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisAlign12.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisAlign12.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisAlign12.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisAlign12.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisAlign12.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisAlign12.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign12.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisAlign12.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisAlign21.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisAlign21.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisAlign21.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisAlign21.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisAlign21.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisAlign21.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisAlign21.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisAlign21.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign21.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisAlign21.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisAlign22.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisAlign22.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisAlign22.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisAlign22.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisAlign22.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisAlign22.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisAlign22.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisAlign22.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisAlign22.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisAlign22.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisCamShutter1.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisCamShutter1.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisCamShutter1.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisCamShutter1.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisCamShutter1.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisCamShutter1.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisCamShutter1.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisCamShutter1.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter1.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisCamShutter1.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisCamShutter2.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisCamShutter2.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisCamShutter2.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisCamShutter2.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisCamShutter2.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisCamShutter2.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisCamShutter2.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisCamShutter2.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisCamShutter2.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisCamShutter2.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisZ1.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisZ1.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisZ1.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisZ1.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisZ1.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisZ1.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisZ1.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisZ1.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ1.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisZ1.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisZ2.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisZ2.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisZ2.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisZ2.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisZ2.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisZ2.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisZ2.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisZ2.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisZ2.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisZ2.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisUwLift.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisUwLift.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisUwLift.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisUwLift.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisUwLift.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisUwLift.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisUwLift.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisUwLift.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwLift.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisUwLift.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUw.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisUw.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUw.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisUw.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUw.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisUw.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUw.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisUw.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUw.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisUw.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUw.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisUw.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUw.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisUw.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUw.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisUw.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUw.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisUw.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisRwLift.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisRwLift.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisRwLift.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisRwLift.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisRwLift.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisRwLift.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisRwLift.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisRwLift.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwLift.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisRwLift.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRw.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisRw.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRw.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisRw.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRw.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisRw.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRw.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisRw.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRw.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisRw.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRw.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisRw.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRw.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisRw.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRw.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisRw.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRw.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisRw.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisClean.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisClean.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisClean.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisClean.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisClean.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisClean.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisClean.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisClean.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisClean.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisClean.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisClean.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisClean.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisClean.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisClean.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisClean.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisClean.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisClean.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisClean.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisPowerMeter.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisPowerMeter.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisPowerMeter.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisPowerMeter.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisPowerMeter.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisPowerMeter.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisPowerMeter.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisPowerMeter.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPowerMeter.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisPowerMeter.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisUwSteer.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisUwSteer.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisUwSteer.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisUwSteer.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisUwSteer.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisUwSteer.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisUwSteer.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisUwSteer.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisUwSteer.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisUwSteer.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisPeeling1.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisPeeling1.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisPeeling1.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisPeeling1.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisPeeling1.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisPeeling1.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisPeeling1.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisPeeling1.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling1.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisPeeling1.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisStationABelt.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisStationABelt.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisStationABelt.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisStationABelt.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisStationABelt.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisStationABelt.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisStationABelt.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisStationABelt.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationABelt.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisStationABelt.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisPeeling2.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisPeeling2.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisPeeling2.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisPeeling2.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisPeeling2.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisPeeling2.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisPeeling2.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisPeeling2.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisPeeling2.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisPeeling2.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisStationBBelt.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisStationBBelt.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisStationBBelt.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisStationBBelt.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisStationBBelt.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisStationBBelt.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisStationBBelt.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisStationBBelt.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisStationBBelt.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisStationBBelt.Dec;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.HomeOffset = this.Mwvm.ControlConfig.ParaAxisRwSteer.HomeOffset;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.RelDistance = this.Mwvm.ControlConfig.ParaAxisRwSteer.RelDistance;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.AbsPosition1 = this.Mwvm.ControlConfig.ParaAxisRwSteer.AbsPosition1;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.AbsPosition2 = this.Mwvm.ControlConfig.ParaAxisRwSteer.AbsPosition2;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.HomeVelo = this.Mwvm.ControlConfig.ParaAxisRwSteer.HomeVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.ManualVelo = this.Mwvm.ControlConfig.ParaAxisRwSteer.ManualVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.WorkVelo = this.Mwvm.ControlConfig.ParaAxisRwSteer.WorkVelo;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.Acc = this.Mwvm.ControlConfig.ParaAxisRwSteer.Acc;
                    this.Mwvm.Config.ControllerConfig.ParaAxisRwSteer.sT_AxisParameter.Dec = this.Mwvm.ControlConfig.ParaAxisRwSteer.Dec;

                    this.Mwvm.Config.CalibrationConfig.IpAddress = this.Mwvm.VisionCalibrationConfig.IpAddress;
                    this.Mwvm.Config.CalibrationConfig.Port = this.Mwvm.VisionCalibrationConfig.Port;

#pragma warning disable CS8600
                    var ddvm = (((DebugPageViewModel)this.Mwvm.DebugPageContent)!).DebugDetailViewContent as DebugDetailViewModel;
#pragma warning restore CS8600
                    ddvm!.PowerDataInitial();
                    //事件发布
                    EngineConfigManager.Instance.Changed(this.Mwvm.Config);
                }
                catch (Exception e)
                {
                    MessageBox.Show($"参数下发异常{e}");
                    this.Mwvm.Log?.ErrorFormat($"参数下发异常{e}");
                    this.Mwvm.EngineAppService.CancleCheckOut();
                    this.Mwvm.ControllAppService.CancleCheckOut();
                    this.Mwvm.VisionAppService.CancleCheckOut();
                }
            }
        }

        #endregion
    }
}
