using Caliburn.Micro;
using DRsoft.Engine.Core;
using DRsoft.Engine.Core.Control.AbstractController;
using DRsoft.Engine.Core.Engine;
using DRsoft.Engine.Core.Interface;
using DRsoft.Engine.Model.Const;
using DRsoft.Runtime.Core.Platform.Events;
using DRsoft.Runtime.Core.Platform.Ioc;
using Engine.Models;
using Engine.Transfer;
using Engine.ViewModels.MainPageComponent;
using DRsoft.Modeling.AppServices;
using DRsoft.Modeling.Metadata.Models.Config;
using DRsoft.Runtime.Core.Platform.Crypt;
using DRsoft.Runtime.Core.Platform.Logging;
using Microsoft.Extensions.DependencyInjection;
using EngineConfig = DRsoft.Engine.Core.Engine.EngineConfig;
using DRsoft.Runtime.Core.DBService.Interface;
using System.Collections.ObjectModel;
using Engine.Views.MainPageComponent;
using Engine.Behaviors;
using JetBrains.Annotations;

namespace Engine.ViewModels
{
    public class MainWindowViewModel : Conductor<object>
    {
        public readonly IWindowManager WindowManager;
        public MainWindowViewModel Mwvm;
        public ViewModelBase? MainPageContent;
        public ViewModelBase? ParameterPageContent;
        public ViewModelBase? DebugPageContent;
        public ViewModelBase? QueryPageContent;
        public ViewModelBase? AlarmPageContent;
        public ViewModelBase? LoginPageContent;
        public ViewModelBase? AboutPageContent;
        public bool MarkingMateInitFinish = false;
        //业务引擎申明
        public AbstractEngine? Engine;
        public AbstractController? Controller;
        public IDataBase? Idatabase;
        public IEventAggregator? Aggregator;
        public ILog? Log;
        public int index = 0;

        public RecipeAppService AppService = new RecipeAppService();
        public RecipeConfig RecipeConfig = new RecipeConfig();
        public EngineConfig Config = new EngineConfig();

        public EngineAppService EngineAppService = new EngineAppService();
        public DRsoft.Modeling.Metadata.Models.Config.EngineConfig EngineConfig = new DRsoft.Modeling.Metadata.Models.Config.EngineConfig();

        public ControllerAppService ControllAppService = new ControllerAppService();
        public ControlConfig ControlConfig = new ControlConfig();

        public VisionCalibrationAppService VisionAppService = new VisionCalibrationAppService();
        public VisionCalibrationConfig VisionCalibrationConfig = new VisionCalibrationConfig();


        public MainWindowViewModel(IWindowManager windowManager)
        {
            Mwvm = this;
            this.WindowManager = windowManager;
            Log = LogProvider.GetLogger(this.GetType());
            this.Aggregator = ServiceProviderManager.Instance.ServiceProvider.GetService<IEventAggregator>();
            var iController =
       ServiceProviderManager.Instance.ServiceProvider.GetService<IController>(RuntimeEnvironment.Controller);
            if (iController != null) Controller = (AbstractController)iController;
            Idatabase = ServiceProviderManager.Instance.ServiceProvider.GetService<IDataBase>();
            EngineInit();
            MainMenuViewContent = new MainMenuViewModel(this.WindowManager, Mwvm);
            MainPageContent = new MainPageViewModel(windowManager, Mwvm);
            ParameterPageContent = new ParameterPageViewModel(windowManager, Mwvm);
            DebugPageContent = new DebugPageViewModel(windowManager, Mwvm);
            QueryPageContent = new QueryPageViewModel(windowManager, Mwvm);
            AlarmPageContent = new AlarmPageViewModel(windowManager, Mwvm);
            LoginPageContent = new LoginPageViewModel(windowManager, Mwvm);
            AboutPageContent = new AboutPageViewModel(windowManager, Mwvm);

            //MainContent = MainPageContent;
            MainMainPageContent = MainPageContent;
            MainParameterPageContent = ParameterPageContent;
            MainDebugPageContent = DebugPageContent;
            MainQueryPageContent = QueryPageContent;
            MainAlarmPageContent = AlarmPageContent;
            MainLoginPageContent = LoginPageContent;
            MainAboutPageContent = AboutPageContent;

            MainMainPageContent.Initialize();
            MainMenuViewContent.Initialize();
            MainParameterPageContent.Initialize();
            MainDebugPageContent.Initialize();
            MainQueryPageContent.Initialize();
            MainAlarmPageContent.Initialize();
            MainLoginPageContent.Initialize();
            MainAboutPageContent.Initialize();

            MainVisbily = new ObservableCollection<Visibility> { Visibility.Visible };
            for (var i = 1; i < 8; i++)
            {
                MainVisbily.Add(Visibility.Collapsed);
            }

            InteractiveData = new InteractiveDataModel();
            LoadAllConfig();
            EngineConfigChanged();
        }

        #region 方法集合

        public void EngineInit()
        {
            LoadEngineConfig();
            CreateEngine();
            //临时开启引擎测试
            InitEngine();
            RunEngine();
        }

        private void LoadAllConfig()
        {
            try
            {
                if (Method.LisRecipeName.Count > 0) InteractiveData!.RecipeItemsControl = Method.LisRecipeName!;
                if (Method.SelectRecipe != null)
                {
                    InteractiveData!.RecipeSelectValue = Method.SelectRecipe;
                    bool isGetAllConfig = false;
                    if (Method.DicEngineConfig.ContainsKey(Method.SelectRecipe))
                    {
                        EngineConfig = Method.DicEngineConfig[Method.SelectRecipe];
                    }
                    else isGetAllConfig = true;
                    if (Method.DicControlConfig.ContainsKey(Method.SelectRecipe))
                    {
                        ControlConfig = Method.DicControlConfig[Method.SelectRecipe];
                    }
                    else isGetAllConfig = true;
                    if (Method.DicVisionCalibrationConfig.ContainsKey(Method.SelectRecipe))
                    {
                        VisionCalibrationConfig = Method.DicVisionCalibrationConfig[Method.SelectRecipe];
                    }
                    else isGetAllConfig = true;

                    if (isGetAllConfig)
                    {
                        return;
                    }
                    Method.LisRecipes = Method.RecipeConfigUse.LisRecipeNote;
                    InteractiveData!.RecipeDataGrid = Method.LisRecipes;
                }
                else
                {
                    EngineConfig = EngineAppService.Read() ??
                                   new DRsoft.Modeling.Metadata.Models.Config.EngineConfig();
                    ControlConfig = ControllAppService.Read() ?? new ControlConfig();
                    VisionCalibrationConfig = VisionAppService.Read() ?? new VisionCalibrationConfig();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"参数加载失败{e}");
                Log?.ErrorFormat($"参数加载失败{e}");
            }
        }

        #endregion

        #region 数据绑定

        private InteractiveDataModel? _interactiveData;

        public InteractiveDataModel? InteractiveData
        {
            get => _interactiveData;
            set
            {
                _interactiveData = value;
                NotifyOfPropertyChange(() => InteractiveData);
            }
        }

        private ViewModelBase? _mainMenuViewContent;

        public ViewModelBase? MainMenuViewContent
        {
            get => _mainMenuViewContent;
            set { _mainMenuViewContent = value; NotifyOfPropertyChange(() => MainMenuViewContent); }
        }

        private ViewModelBase? _mainContent;

        public ViewModelBase? MainContent
        {
            get => _mainContent;
            set { _mainContent = value; NotifyOfPropertyChange(() => MainContent); }
        }

        private ViewModelBase? _mainMainPageContent;

        public ViewModelBase? MainMainPageContent
        {
            get => _mainMainPageContent;
            set { _mainMainPageContent = value; NotifyOfPropertyChange(() => MainMainPageContent); }
        }

        private ViewModelBase? _mainParameterPageContent;

        public ViewModelBase? MainParameterPageContent
        {
            get => _mainParameterPageContent;
            set { _mainParameterPageContent = value; NotifyOfPropertyChange(() => MainParameterPageContent); }
        }

        private ViewModelBase? _mainDebugPageContent;

        public ViewModelBase? MainDebugPageContent
        {
            get => _mainDebugPageContent;
            set { _mainDebugPageContent = value; NotifyOfPropertyChange(() => MainDebugPageContent); }
        }

        private ViewModelBase? _mainQueryPageContent;

        public ViewModelBase? MainQueryPageContent
        {
            get => _mainQueryPageContent;
            set { _mainQueryPageContent = value; NotifyOfPropertyChange(() => MainQueryPageContent); }
        }

        private ViewModelBase? _mainAlarmPageContent;

        public ViewModelBase? MainAlarmPageContent
        {
            get => _mainAlarmPageContent;
            set { _mainAlarmPageContent = value; NotifyOfPropertyChange(() => MainAlarmPageContent); }
        }

        private ViewModelBase? _mainLoginPageContent;

        public ViewModelBase? MainLoginPageContent
        {
            get => _mainLoginPageContent;
            set { _mainLoginPageContent = value; NotifyOfPropertyChange(() => MainLoginPageContent); }
        }

        private ViewModelBase? _mainAboutPageContent;

        public ViewModelBase? MainAboutPageContent
        {
            get => _mainAboutPageContent;
            set { _mainAboutPageContent = value; NotifyOfPropertyChange(() => MainAboutPageContent); }
        }

        private string? userName = "用户名: Observer";
        public string? UserName
        {
            get { return userName;}
            set
            { 
                userName = value; 
                NotifyOfPropertyChange(() => UserName);
            }

        }

        public void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainVisbily[((ListBox)sender).SelectedIndex] = Visibility.Visible;
            index = ((ListBox)sender).SelectedIndex;
            for (var i = 0; i < MainVisbily.Count; i++)
            {
                if (i != ((ListBox)sender).SelectedIndex) MainVisbily[i] = Visibility.Collapsed;
            }

            switch (((ListBox)sender).SelectedIndex)
            {
                case 0:
                    MainMainPageContent?.Refresh();
                    break;
                case 1:
                    MainParameterPageContent?.Refresh();
                    break;
                case 2:
                    MainDebugPageContent?.Refresh();
                    break;
                case 3:
                    MainQueryPageContent?.Refresh();
                    break;
                case 4:
                    MainAlarmPageContent?.Refresh();
                    break;
                case 5:
                    MainLoginPageContent?.Refresh();
                    break;
                case 6:
                    MainAboutPageContent?.Refresh();
                    break;
            }
        }

        private ObservableCollection<Visibility> _mainVisibilty = null!;

        public ObservableCollection<Visibility> MainVisbily
        {
            get => _mainVisibilty;
            set
            {
                _mainVisibilty = value; NotifyOfPropertyChange(() => MainVisbily);
            }
        }
        #endregion

        #region 配置相关

        /// <summary>
        /// 加载引擎配置
        /// </summary>
        /// <returns></returns>
        private void LoadEngineConfig()
        {
            int result = new TransferConfig().UpdateEngConfig(out EngineConfig curConfig);
            if (result == 0) Config = curConfig;
        }

        /// <summary>
        /// 处理引擎配置变更
        /// </summary>
        [UsedImplicitly]
        private void EngineConfigChanged()
        {
            //发布配置变更事件
            var curConfig = Config.Reset();
            curConfig.SetOwer(EventBroadcastNodeDefine.WindowIdentity);
            curConfig.AddSubscriber(EventBroadcastNodeDefine.EngineIdentity);
            Aggregator?.GetEvent<PubSubEvent<EngineConfig>>().Publish((EngineConfig)curConfig);
            Config = (EngineConfig)curConfig;
        }

        #endregion

        #region 业务引擎相关

        /// <summary>
        /// 初始化化运行时插件
        /// </summary>
        private void CreateEngine()
        {
            var iEngine =
                ServiceProviderManager.Instance.ServiceProvider.GetService<IEngine>(RuntimeEnvironment.Engine);
            if (iEngine == null || ((AbstractEngine)iEngine) == null) return;

            Engine = (AbstractEngine)iEngine;
            Engine.BuildEngine(Config);
        }

        private void InitEngine()
        {
            // 初始化引擎
            Engine?.InitializeEngine(Config);
        }

        /// <summary>
        /// 运行引擎
        /// </summary>
        private void RunEngine(bool needRestToken = false)
        {
            try
            {
                // 重置Token
                if (needRestToken) Engine?.ResetToken();
                // 启动引擎
                Engine?.StartEngineLoop();
            }
            catch (Exception ex)
            {
                Log?.ErrorException($"引擎【{RuntimeEnvironment.Engine}】运行异常:", ex);
            }
        }

        /// <summary>
        /// 停止引擎
        /// </summary>
        public void StopEngine()
        {
            try
            {
                // 停止生产
                Engine?.StopProduction();
                //停止引擎
                Engine?.StopEventLoop();
                // 垃圾回收
                GC.Collect();
            }
            catch (Exception ex)
            {
                Log?.ErrorException($"引擎【{RuntimeEnvironment.Engine}】关闭异常:", ex);
            }
        }

        public void OnClosed(System.ComponentModel.CancelEventArgs e)
        {
            StopEngine();
            (((MainImgViewModel)(MainMainPageContent as MainPageViewModel)?.MainImgViewContent!)?.GetView() as
                MainImgView)?.Dispatcher_ShutdownStarted(null, null!);
            var syscommonfunction = new SystemCommonFuction();
            syscommonfunction.ThreadExitis("DRMark", true);
            Environment.Exit(0);
        }
        #endregion
    }
}
