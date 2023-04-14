using Caliburn.Micro;
using Engine.Models;
using Engine.ViewModels.CommonComponent;
using Engine.ViewModels.ParameterPageComponent;

namespace Engine.ViewModels
{
    public class ParameterPageViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainWindowViewModel Mwvm;
        public ParameterPageViewModel Ppvm;
        public ParameterPageViewModel(IWindowManager iwindow, MainWindowViewModel mwm)
        {
            Ppvm = this;
            this.WindowManager = iwindow;
            this.Mwvm = mwm;

            ParameterRecipeViewContent = new ParameterRecipeViewModel(WindowManager, Mwvm, Ppvm);
            ParameterDetailViewContent = new ParameterDetailViewModel(WindowManager, Mwvm, Ppvm);
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

        private ViewModelBase? _parameterRecipeViewContent;

        public ViewModelBase? ParameterRecipeViewContent
        {
            get => _parameterRecipeViewContent;
            set { _parameterRecipeViewContent = value; NotifyOfPropertyChange(() => ParameterRecipeViewContent); }
        }

        private ViewModelBase? _parameterDetailViewContent;

        public ViewModelBase? ParameterDetailViewContent
        {
            get => _parameterDetailViewContent;
            set { _parameterDetailViewContent = value; NotifyOfPropertyChange(() => ParameterDetailViewContent); }
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
