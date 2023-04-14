using Caliburn.Micro;
using Engine.Models;

namespace Engine.ViewModels.CommonComponent
{
    public class DataLogLargeViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainWindowViewModel Mwvm;
        public DataLogLargeViewModel(IWindowManager iwindow, MainWindowViewModel mwm)
        {
            this.WindowManager = iwindow;
            this.Mwvm = mwm;
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
