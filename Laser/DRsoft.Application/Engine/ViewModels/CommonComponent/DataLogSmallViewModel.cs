using Caliburn.Micro;
using Engine.Models;

namespace Engine.ViewModels.CommonComponent
{
    public class DataLogSmallViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainWindowViewModel Mwvm;
        public MainPageViewModel Mpvm;

        public DataLogSmallViewModel(IWindowManager windowManager, MainWindowViewModel mvm, MainPageViewModel mpm)
        {
            this.WindowManager = windowManager;
            this.Mwvm = mvm;
            this.Mpvm = mpm;
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

        #endregion
    }
}
