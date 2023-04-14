using Caliburn.Micro;
using Engine.Models;
using System.Windows.Forms;
using Engine.Views.MainPageComponent;
using MessageBox = System.Windows.MessageBox;
using DRsoft.Runtime.Core.Platform.Logging;
using DRsoft.Runtime.Core.Platform.Events;

namespace Engine.ViewModels.MainPageComponent
{
    public class MainControlPanelViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainPageViewModel Mpvm;
        public MainWindowViewModel Mwvm;
        public PubSubEvent<WindowMessagePush> MarkingMateEventManager;
        private VibraOfs _vibraOfs = new VibraOfs();
        public PubSubEvent<Login> RoleChangeEventManager;
        public MainControlPanelViewModel(IWindowManager windowManager, MainWindowViewModel mvm, MainPageViewModel mpm)
        {
            this.WindowManager = windowManager;
            this.Mpvm = mpm;
            this.Mwvm = mvm;
            MarkingMateEventManager = this.Mwvm.Aggregator!.GetEvent<PubSubEvent<WindowMessagePush>>();
            MarkingMateEventManager.Subscribe(_ =>
            {
                this.Mwvm.MarkingMateInitFinish = true;
                MarkingMateInitial = true;
            });
            RoleChangeEventManager = Mwvm.Aggregator.GetEvent<PubSubEvent<Login>>();
            RoleChangeEventManager.Subscribe(p =>
            {
                Role = p;
            });
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

        private bool _markingMateInitial;
        public bool MarkingMateInitial
        {
            get => _markingMateInitial;
            set
            {
                _markingMateInitial = value;
                NotifyOfPropertyChange(() => MarkingMateInitial);
            }
        }

        private bool _clearAlarm;
        public bool ClearAlarm
        {
            get => _clearAlarm;
            set
            {
                _clearAlarm = value;
                NotifyOfPropertyChange(() => ClearAlarm);
            }
        }

        #endregion

        #region 命令绑定
        public void AutoStart()
        {
            //Mpvm.AutoStart();
            if (MessageBox.Show("您确认执行开启自动化操作?", "安全提示", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }

            //开启自动化：
            Mwvm.Engine?.StartProduction();
        }

        public void AutoStop()
        {
            //停止自动化：
            Mwvm.Engine?.StopProduction();
        }

        public void BtnInitClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("您确认执行初始化操作?", "安全提示", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }

            if (MessageBox.Show("是否确认整体复位?", "提示", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
            //控制器初始化：
            Mwvm.Engine?.SendInitToController();
            //BtnStartEngine.IsEnabled = false;
            MessageBox.Show("初始化执行成功");
            this.Mwvm.Log?.Info("主页面-初始化执行成功!");
        }

        /// <summary>
        /// 
        /// </summary>
        public void AlarmConfirm()
        {
            Mwvm.Engine?.SendErrAckToController();
            ClearAlarm = true; //清除当前报警显示
        }

        public void AllMarkFileLoad()
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
            var mainImgViewmodel = (MainImgViewModel)this.Mpvm.MainImgViewContent!;
            mainImgViewmodel.DrMarkPath.Clear();
            var outDir = folderBrowserDialog.SelectedPath;
            var mainImgView = (MainImgView)this.Mpvm.MainImgViewContent!.GetView();
            for (var i = 0; i < mainImgView.GetInstance().DrMark.Count; i++)
            {
                mainImgViewmodel.DrMarkPath.Add($"{outDir}\\{i + 1}.ezm");
                mainImgView.GetInstance().DrMark[i].LoadFile($"{outDir}\\{i + 1}.ezm");
                mainImgView.GetInstance().DrMark[i].ExportJPG($"{AppDomain.CurrentDomain.BaseDirectory}\\{i + 1}.jpg", 0);
                mainImgView.GetInstance().DrMark[i].Redraw();
                mainImgView.GetInstance().DrMark[i].JumpToStartPos();
            }

            mainImgView.LoadingMarkingJpg();
        }

        public void ManualAllMarking()
        {
            var mainImgView = (MainImgView)this.Mpvm.MainImgViewContent!.GetView();
            var mainImgViewmodel = (MainImgViewModel)this.Mpvm.MainImgViewContent;
            if (mainImgView.GetInstance() == null)
            {
                MessageBox.Show("打标卡驱动加载异常,请排查后尝试重新开启软件!");
                this.Mwvm.Log?.Info("打标卡驱动加载异常,请排查后尝试重新开启软件!");
            }
            if (MessageBox.Show("请确认是否手动激光出光打标?", "警告-手动打标", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
            for (var i = 0; i < mainImgView.GetInstance().DrMark.Count; i++)
            {
                _vibraOfs = mainImgViewmodel.GetVirbarOfsValue(i, this.Mwvm.Config.PcParamConfig, new VibraOfs() { OfsX = 0, OfsY = 0, OfsA = 0 });
                mainImgView.ManualMarking(
                    i <= 6 ? this.Mwvm.Engine!._laserPadPosition1 : this.Mwvm.Engine!._laserPadPosition2, i + 1,
                    _vibraOfs.OfsX, _vibraOfs.OfsY, _vibraOfs.OfsA);
            }
        }

        #endregion
    }
}
