using Caliburn.Micro;
using Engine.Models;
using Engine.ViewModels.CommonComponent;

namespace Engine.ViewModels
{
    public class AboutPageViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainWindowViewModel Mwvm;
        public AboutPageViewModel(IWindowManager iwindow, MainWindowViewModel mwm)
        {
            this.WindowManager = iwindow;
            this.Mwvm = mwm;
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

        private ViewModelBase? _dataLogLargeViewContent;
        public ViewModelBase? DataLogLargeViewContent
        {
            get => _dataLogLargeViewContent;
            set { _dataLogLargeViewContent = value; NotifyOfPropertyChange(() => DataLogLargeViewContent); }
        }
        #endregion
    }
}
