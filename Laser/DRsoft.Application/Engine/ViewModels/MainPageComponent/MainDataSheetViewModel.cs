using Caliburn.Micro;
using DRsoft.Engine.Model.Controller;
using Engine.Models;
using System.Windows.Threading;
using Engine.Transfer;
using System.Security.RightsManagement;

namespace Engine.ViewModels.MainPageComponent
{
    public class MainDataSheetViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainPageViewModel Mpvm;
        public MainWindowViewModel Mwvm;
        public DispatcherTimer DataRefresh = new DispatcherTimer();
        public MainDataSheetViewModel(IWindowManager windowManager, MainWindowViewModel mvm, MainPageViewModel mpm)
        {
            this.WindowManager = windowManager;
            this.Mpvm = mpm;
            this.Mwvm = mvm;
            DataRefresh.Interval = TimeSpan.FromMilliseconds(500);
            DataRefresh.Tick += DataRefreshFunction!;
            DataRefresh.Start();
            IoOutput = this.Mwvm.Controller!.IoOutput;
            IoInput = this.Mwvm.Controller!.IoInput;
            Gantry1ActPos = this.Mwvm.Controller!.StatusAxisGantry11.sT_AxisStatus.ActPos;
            Gantry2ActPos = this.Mwvm.Controller!.StatusAxisGantry21.sT_AxisStatus.ActPos;
            SysStatus = this.Mwvm.Controller!.SysStatus;
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
            if (this.Mwvm.Controller == null) return;
            IoOutput = this.Mwvm.Controller!.IoOutput;
            IoInput = this.Mwvm.Controller!.IoInput;
            Gantry1ActPos = this.Mwvm.Controller!.StatusAxisGantry11.sT_AxisStatus.ActPos;
            Gantry2ActPos = this.Mwvm.Controller!.StatusAxisGantry21.sT_AxisStatus.ActPos;
            SysStatus = this.Mwvm.Controller!.SysStatus;
            if (SysStatus.Running) SystemStatusText = "运行";
            else if (SysStatus.Stop) SystemStatusText = "停止";
            else if (SysStatus.Homing) SystemStatusText = "回零中";
            else if (SysStatus.Homed) SystemStatusText = "回零完成";
            else SystemStatusText = "待机";
        }

        #endregion

        #region 数据写入

        public void InputConfirm()
        {
            Mwvm.Engine!.Config.GroupId = GroupId!;
            Mwvm.Engine!.Config.SilicaId = SilicaId!;
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
            get
            {
                _iooutput = this.Mwvm.Controller!.IoOutput;
                return _iooutput;
            }
            set
            {
                _iooutput = value;
                NotifyOfPropertyChange(() => IoOutput);
                this.Mwvm.Controller!.IoOutput = _iooutput;
            }
        }

        private StInput _ioinput = null!;

        public StInput IoInput
        {
            get
            {
                _ioinput = this.Mwvm.Controller!.IoInput; 
                return _ioinput;
            }
            set
            {
                _ioinput = value;
                NotifyOfPropertyChange(() => IoInput);
                this.Mwvm.Controller!.IoInput = _ioinput;
            }
        }

        private double _gantry1ActPos;

        public double Gantry1ActPos
        {
            get
            {
                _gantry1ActPos = this.Mwvm.Controller!.StatusAxisGantry11.sT_AxisStatus.ActPos;
                 return _gantry1ActPos;
            } 
            set
            {
                _gantry1ActPos = value;
                NotifyOfPropertyChange(() => Gantry1ActPos);
                this.Mwvm.Controller!.StatusAxisGantry11.sT_AxisStatus.ActPos = (float)_gantry1ActPos;
            }
        }

        private double _gantry2ActPos;
        public double Gantry2ActPos
        {
            get
            {
                _gantry2ActPos = this.Mwvm.Controller!.StatusAxisGantry21.sT_AxisStatus.ActPos;
                return _gantry2ActPos;
            }
            set
            {
                _gantry2ActPos = value;
                NotifyOfPropertyChange(() => Gantry2ActPos);
                this.Mwvm.Controller!.StatusAxisGantry21.sT_AxisStatus.ActPos = (float)_gantry2ActPos;
            }
        }

        private StStatus _sysStatus = null!;

        public StStatus SysStatus
        {
            get
            {
                _sysStatus = this.Mwvm.Controller!.SysStatus;
                return _sysStatus;
            }
            set
            {
                _sysStatus = value;
                NotifyOfPropertyChange(() => SysStatus);
                this.Mwvm.Controller!.SysStatus = _sysStatus;
            }
        }

        private string _systemStatusText = null!;

        public string SystemStatusText
        {
            get => _systemStatusText;
            set
            {
                _systemStatusText = value;
                NotifyOfPropertyChange(() => SystemStatusText);
            }
        }

        public void ClearStatistical()
        {
            this.Mwvm.Controller!.CmdCommand.ClearStatistical = true;
            this.Mwvm.Controller!.WriteToControllerByIndex(DRsoft.Engine.Model.Enum.ControllerInputIndex.IN_INDEX_CMD);
            this.Mwvm.Controller!.CmdCommand.ClearStatistical = false;
        }

        private string? groupId = "";
        public string? GroupId
        {
            get { return groupId; }
            set
            {
                groupId = value;
                NotifyOfPropertyChange(() => GroupId);
            }
        }

        private string? silicaId= Guid.NewGuid().ToString();
        public string? SilicaId
        {
            get { return silicaId; }
            set 
            { 
                silicaId = value;
                NotifyOfPropertyChange(() => SilicaId);
            }
        }

        #endregion
    }
}
