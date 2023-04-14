using Caliburn.Micro;
using Engine.Models;
using Engine.ViewModels.CommonComponent;
using Engine.ViewModels.DebugPageComponent;

namespace Engine.ViewModels
{
    public class DebugPageViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainWindowViewModel Mwvm;
        public DebugPageViewModel Dpvm;
        public DebugPageViewModel(IWindowManager iwindow, MainWindowViewModel mwm)
        {
            this.WindowManager = iwindow;
            this.Mwvm = mwm;
            Dpvm = this;
            DebugDetailViewContent = new DebugDetailViewModel(WindowManager, Mwvm, Dpvm);
            DataLogLargeViewContent = new DataLogLargeViewModel(WindowManager, Mwvm);
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

        private ViewModelBase? _debugDetailViewContent;

        public ViewModelBase? DebugDetailViewContent
        {
            get => _debugDetailViewContent;
            set { _debugDetailViewContent = value; NotifyOfPropertyChange(() => DebugDetailViewContent); }
        }

        private ViewModelBase? _dataLogLargeViewContent;
        public ViewModelBase? DataLogLargeViewContent
        {
            get => _dataLogLargeViewContent;
            set { _dataLogLargeViewContent = value; NotifyOfPropertyChange(() => DataLogLargeViewContent); }
        }

        #endregion
    }
}
