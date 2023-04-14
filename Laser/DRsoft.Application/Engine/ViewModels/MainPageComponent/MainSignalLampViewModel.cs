using Caliburn.Micro;
using Engine.Models;
using Engine.Transfer;
using System.Windows.Threading;
using DRsoft.Engine.Model.Controller;

namespace Engine.ViewModels.MainPageComponent
{
    public class MainSignalLampViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainPageViewModel Mpvm;
        public MainWindowViewModel Mwvm;
        public DispatcherTimer DataRefresh = new DispatcherTimer();

        public MainSignalLampViewModel(IWindowManager windowManager, MainWindowViewModel mvm, MainPageViewModel mpm)
        {
            this.WindowManager = windowManager;
            this.Mpvm = mpm;
            this.Mwvm = mvm;
            DataRefresh.Interval = TimeSpan.FromMilliseconds(500);
            DataRefresh.Tick += DataRefreshFunction!;
            DataRefresh.Start();
            IoOutput = this.Mwvm.Controller!.IoOutput; 
        }

        private RelayCommand _closeWindowCommand = null!;
        public RelayCommand CloseWindowCommand
        {
            get
            {
                if (_closeWindowCommand == null) _closeWindowCommand = new RelayCommand((_) => CloseWindow());
                return _closeWindowCommand;
            }
            set => _closeWindowCommand = value;
        }

        private void CloseWindow()
        {
            DataRefresh.Stop();
        }

        #region 数据刷新

        public void DataRefreshFunction(object obj, EventArgs e)
        {
            if (this.Mwvm.Controller == null || this.Mwvm.Engine == null) return;
            IoOutput = this.Mwvm.Controller!.IoOutput;
            SafeDoorStatus = this.Mwvm.Controller!.IoInput.SafeDoor1 &&
                             this.Mwvm.Controller!.IoInput.SafeDoor2 &&
                             this.Mwvm.Controller!.IoInput.SafeDoor3 &&
                             this.Mwvm.Controller!.IoInput.SafeDoor4 &&
                             this.Mwvm.Controller!.IoInput.SafeDoor5 &&
                             this.Mwvm.Controller!.IoInput.SafeDoor6 &&
                             this.Mwvm.Controller!.IoInput.SafeDoor7 &&
                             this.Mwvm.Controller!.IoInput.SafeDoor8;
            ProcessStatus = this.Mwvm.Engine!.IsRunning;
            PlcStatus = this.Mwvm.Controller!.IsPlcConnected();
            VisionAStatus = this.Mwvm.Engine!.IsVisionProductionAConnected();
            VisionBStatus = this.Mwvm.Engine!.IsVisionProductionBConnected();
            PowerMeterStatus = this.Mwvm.Engine!.IsPowerMeterConnected();
        }

        #endregion

        #region 方法绑定

        public void PlcConnect()
        {
            if (this.Mwvm.Engine == null || this.Mwvm.Controller!.PlcHander == null) return;
            this.Mwvm.Engine.StopProduction();
            this.Mwvm.Controller.EmergencyStop();
            //this.Mwvm.Controller.ReConnect();
            this.Mwvm.Controller.InitializeControllerExt(false);
        }

        public void VisionAConnect()
        {
            if (this.Mwvm.Engine == null) return;
            if(this.Mwvm.Engine.VisionProduction != null) this.Mwvm.Engine.VisionProduction[0].Initialize();
        }

        public void VisionBConnect()
        {
            if (this.Mwvm.Engine == null) return;
            if (this.Mwvm.Engine.VisionProduction != null) this.Mwvm.Engine.VisionProduction[1].Initialize();
        }

        public void PowerMeterConnect()
        {
            if (this.Mwvm.Engine == null) return;
            if (this.Mwvm.Engine.PowerMeter != null) this.Mwvm.Engine.PowerMeter.Initial();
        }

        #endregion

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

        private StOutput _iooutput = null!;
        public StOutput IoOutput
        {
            get => _iooutput;
            set
            {
                _iooutput = value;
                NotifyOfPropertyChange(() => IoOutput);
            }
        }

        private bool _safeDoorStatus;
        public bool SafeDoorStatus
        {
            get => _safeDoorStatus;
            set
            {
                _safeDoorStatus = value;
                NotifyOfPropertyChange(() => SafeDoorStatus);
            }
        }

        private bool _processStatus;
        public bool ProcessStatus
        {
            get => _processStatus;
            set
            {
                _processStatus = value;
                NotifyOfPropertyChange(() => ProcessStatus);
            }
        }

        private bool _plcStatus;
        public bool PlcStatus
        {
            get => _plcStatus;
            set
            {
                _plcStatus = value;
                NotifyOfPropertyChange(() => PlcStatus);
            }
        }

        private bool _visionAStatus;
        public bool VisionAStatus
        {
            get => _visionAStatus;
            set
            {
                _visionAStatus = value;
                NotifyOfPropertyChange(() => VisionAStatus);
            }
        }

        private bool _visionBStatus;
        public bool VisionBStatus
        {
            get => _visionBStatus;
            set
            {
                _visionBStatus = value;
                NotifyOfPropertyChange(() => VisionBStatus);
            }
        }

        private bool _powerMeterStatus;
        public bool PowerMeterStatus
        {
            get => _powerMeterStatus;
            set
            {
                _powerMeterStatus = value;
                NotifyOfPropertyChange(() => PowerMeterStatus);
            }
        }

        #endregion
    }
}
