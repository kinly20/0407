using Caliburn.Micro;
using DRsoft.Runtime.Core.Platform.Logging;
using Engine.Models;
using Engine.ViewModels.CommonComponent;
using Engine.ViewModels.MainPageComponent;

namespace Engine.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainWindowViewModel Mwvm;
        public MainPageViewModel Mpm;
        public MainPageViewModel(IWindowManager iwindow, MainWindowViewModel mwm)
        {
            Mpm = this;
            this.WindowManager = iwindow;
            this.Mwvm = mwm;
            MainImgViewContent = new MainImgViewModel(WindowManager, Mwvm, Mpm);
            MainImgViewContent.Initialize();

            MainFlowContent = new MainLayoutViewModel(WindowManager, Mwvm, Mpm);
            MainFlowContent.Initialize();

            MainSignalLampViewContent = new MainSignalLampViewModel(WindowManager, Mwvm, Mpm);
            MainSignalLampViewContent.Initialize();

            MainDataSheetViewContent = new MainDataSheetViewModel(WindowManager, Mwvm, Mpm);
            MainDataSheetViewContent.Initialize();

            MainControlPanelViewContent = new MainControlPanelViewModel(WindowManager, Mwvm, Mpm);
            MainControlPanelViewContent.Initialize();

            DataLogSmallViewContent = new DataLogSmallViewModel(WindowManager, Mwvm, Mpm);
            DataLogSmallViewContent.Initialize();
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

        private ViewModelBase? _mainImgViewContent;

        public ViewModelBase? MainImgViewContent
        {
            get => _mainImgViewContent;
            set { _mainImgViewContent = value; NotifyOfPropertyChange(() => MainImgViewContent); }
        }

        private ViewModelBase? _mainFlowContent;
        public ViewModelBase? MainFlowContent
        {
            get => _mainFlowContent;
            set { _mainFlowContent = value; NotifyOfPropertyChange(() => MainFlowContent); }
        }

        private ViewModelBase? _mainSignalLampViewContent;

        public ViewModelBase? MainSignalLampViewContent
        {
            get => _mainSignalLampViewContent;
            set { _mainSignalLampViewContent = value; NotifyOfPropertyChange(() => MainSignalLampViewContent); }
        }

        private ViewModelBase? _mainDataSheetViewContent;

        public ViewModelBase? MainDataSheetViewContent
        {
            get => _mainDataSheetViewContent;
            set { _mainDataSheetViewContent = value; NotifyOfPropertyChange(() => MainDataSheetViewContent); }
        }

        private ViewModelBase? _mainControlPanelViewContent;

        public ViewModelBase? MainControlPanelViewContent
        {
            get => _mainControlPanelViewContent;
            set { _mainControlPanelViewContent = value; NotifyOfPropertyChange(() => MainControlPanelViewContent); }
        }

        private ViewModelBase? _dataLogSmallViewContent;

        public ViewModelBase? DataLogSmallViewContent
        {
            get => _dataLogSmallViewContent;
            set { _dataLogSmallViewContent = value; NotifyOfPropertyChange(() => DataLogSmallViewContent); }
        }

        #endregion

        #region 命令操作

        public void AutoStart()
        {
            MessageBox.Show("自动启动");
            this.Mwvm.Log?.Info("主页面-自动启动");
        }
        #endregion
    }
}
