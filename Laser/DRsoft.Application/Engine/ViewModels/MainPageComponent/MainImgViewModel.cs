using AxMMMarkx641Lib;
using Caliburn.Micro;
using DRsoft.Runtime.Core.Platform.Events;
using Engine.Models;
using Engine.Views.MainPageComponent;
using DRsoft.Engine.Model.Controller;
using DRsoft.Engine.Model.PC;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Engine.Transfer;
using Action = System.Action;
using DRsoft.Engine.Model.Enum;
using Engine.ViewModels.DebugPageComponent;
using DRsoft.Engine.Model.Vision;

//using System.ComponentModel.Composition;

namespace Engine.ViewModels.MainPageComponent
{

    public class MainImgViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainPageViewModel Mpvm;
        public MainWindowViewModel Mwvm;
        public MainImgView? MainImgView;
        public DispatcherTimer DataRefresh = new DispatcherTimer();
        public PubSubEvent<MarkingSendPara> MarkingSendParaEventManager;
        public PubSubEvent<MarkingRecvPara> MarkingRecvParaEventManager;
        public PubSubEvent<MarkingRecvStatusFeedback> MarkingRecvStatusFeedbackEventManager;
        public PubSubEvent<WindowMessagePush> MarkingMateEventManager;
        public PubSubEvent<Login> RoleChangeEventManager;
        public MarkingRecvPara MarkingRecvPara = new MarkingRecvPara();
        public MarkingRecvStatusFeedback MarkingRecvStatusFeedback = new MarkingRecvStatusFeedback();
        private VibraOfs _vibraOfs = new VibraOfs();
        public List<string> DrMarkPath = new List<string>();
        public static int CurrentProcessLineNum;
        public static int MarkLength = 12;
        public int[] MarkingStatusFeedback = new int[6];
        public BitmapImage TempBitmapImage = new BitmapImage();
        public delegate void HwMarkEnd1();
        public delegate void HwMarkEnd2();
        public delegate void HwMarkEnd3();
        public delegate void HwMarkEnd4();
        public delegate void HwMarkEnd5();
        public delegate void HwMarkEnd6();
        public delegate void HwMarkEnd7();
        public delegate void HwMarkEnd8();
        public delegate void HwMarkEnd9();
        public delegate void HwMarkEnd10();
        public delegate void HwMarkEnd11();
        public delegate void HwMarkEnd12();
        public event HwMarkEnd1 OnHwMarkEnd1;
        public event HwMarkEnd2 OnHwMarkEnd2;
        public event HwMarkEnd3 OnHwMarkEnd3;
        public event HwMarkEnd4 OnHwMarkEnd4;
        public event HwMarkEnd5 OnHwMarkEnd5;
        public event HwMarkEnd6 OnHwMarkEnd6;
        public event HwMarkEnd7 OnHwMarkEnd7;
        public event HwMarkEnd8 OnHwMarkEnd8;
        public event HwMarkEnd9 OnHwMarkEnd9;
        public event HwMarkEnd10 OnHwMarkEnd10;
        public event HwMarkEnd11 OnHwMarkEnd11;
        public event HwMarkEnd12 OnHwMarkEnd12;

        public DebugDetailViewModel? debugModel;

        public MainImgViewModel(IWindowManager windowManager, MainWindowViewModel mvm, MainPageViewModel mpm)
        {
            this.WindowManager = windowManager;
            this.Mpvm = mpm;
            this.Mwvm = mvm;
            DataRefresh.Interval = TimeSpan.FromMilliseconds(500);
            DataRefresh.Tick += DataRefreshFunction!;
            DataRefresh.Start();
            MarkingSendParaEventManager = this.Mwvm.Aggregator!.GetEvent<PubSubEvent<MarkingSendPara>>();
            MarkingRecvParaEventManager = this.Mwvm.Aggregator!.GetEvent<PubSubEvent<MarkingRecvPara>>();
            MarkingRecvStatusFeedbackEventManager = this.Mwvm.Aggregator!.GetEvent<PubSubEvent<MarkingRecvStatusFeedback>>();
            MarkingMateEventManager = this.Mwvm.Aggregator!.GetEvent<PubSubEvent<WindowMessagePush>>();
            MarkingMateEventManager.Subscribe(_ =>
            {
                this.Mwvm.MarkingMateInitFinish = true;
                MarkingmateInit();
                MarkingMateInitial = true;
                debugModel!.MarkingMateInitial = true;
            });
            RoleChangeEventManager = Mwvm.Aggregator.GetEvent<PubSubEvent<Login>>();
            RoleChangeEventManager.Subscribe(p =>
            {
                Role = p;
            });

            IoOutput = this.Mwvm.Controller!.IoOutput;
            SysCmd = this.Mwvm.Controller!.CmdOutput;
            GantryMarkingStatus = new ObservableCollection<bool>();
            for (var i = 0; i < 72; i++)
            {
                GantryMarkingStatus.Add(false);
            }

            //this.OnHwMarkEnd1 += Hw_MarkEnd1;
            //this.OnHwMarkEnd2 += Hw_MarkEnd2;
            //this.OnHwMarkEnd3 += Hw_MarkEnd3;
            //this.OnHwMarkEnd4 += Hw_MarkEnd4;
            //this.OnHwMarkEnd5 += Hw_MarkEnd5;
            //this.OnHwMarkEnd6 += Hw_MarkEnd6;
            //this.OnHwMarkEnd7 += Hw_MarkEnd7;
            //this.OnHwMarkEnd8 += Hw_MarkEnd8;
            //this.OnHwMarkEnd9 += Hw_MarkEnd9;
            //this.OnHwMarkEnd10 += Hw_MarkEnd10;
            //this.OnHwMarkEnd11 += Hw_MarkEnd11;
            //this.OnHwMarkEnd12 += Hw_MarkEnd12;
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
            //MarkEnd1Change = this.Mwvm.Controller!.IoInput.Mark1_Status;
            //MarkEnd2Change = this.Mwvm.Controller!.IoInput.Mark2_Status;
            //MarkEnd3Change = this.Mwvm.Controller!.IoInput.Mark3_Status;
            //MarkEnd4Change = this.Mwvm.Controller!.IoInput.Mark4_Status;
            //MarkEnd5Change = this.Mwvm.Controller!.IoInput.Mark5_Status;
            //MarkEnd6Change = this.Mwvm.Controller!.IoInput.Mark6_Status;
            //MarkEnd7Change = this.Mwvm.Controller!.IoInput.Mark7_Status;
            //MarkEnd8Change = this.Mwvm.Controller!.IoInput.Mark8_Status;
            //MarkEnd9Change = this.Mwvm.Controller!.IoInput.Mark9_Status;
            //MarkEnd10Change = this.Mwvm.Controller!.IoInput.Mark10_Status;
            //MarkEnd11Change = this.Mwvm.Controller!.IoInput.Mark11_Status;
            //MarkEnd12Change = this.Mwvm.Controller!.IoInput.Mark12_Status;

            //SysCmd = this.Mwvm.Controller!.CmdOutput;
            Active = !Mwvm.Controller.SysStatus.Running && !Mwvm.Controller.SysStatus.Pause;
        }

        #endregion

        protected void MarkingmateInit()
        {
            MainImgView = (MainImgView?)this.GetView();
            DrMarkPath.Clear();

            if (MainImgView == null) return;
            MainImgView.GetInstance().DrMark[0].MarkEnd += Mark1_MarkEnd;
            MainImgView.GetInstance().DrMark[1].MarkEnd += Mark2_MarkEnd;
            MainImgView.GetInstance().DrMark[2].MarkEnd += Mark3_MarkEnd;
            MainImgView.GetInstance().DrMark[3].MarkEnd += Mark4_MarkEnd;
            MainImgView.GetInstance().DrMark[4].MarkEnd += Mark5_MarkEnd;
            MainImgView.GetInstance().DrMark[5].MarkEnd += Mark6_MarkEnd;
            MainImgView.GetInstance().DrMark[6].MarkEnd += Mark7_MarkEnd;
            MainImgView.GetInstance().DrMark[7].MarkEnd += Mark8_MarkEnd;
            MainImgView.GetInstance().DrMark[8].MarkEnd += Mark9_MarkEnd;
            MainImgView.GetInstance().DrMark[9].MarkEnd += Mark10_MarkEnd;
            MainImgView.GetInstance().DrMark[10].MarkEnd += Mark11_MarkEnd;
            MainImgView.GetInstance().DrMark[11].MarkEnd += Mark12_MarkEnd;

            string readPath = this.Mwvm.Config.PcParamConfig.MarkingPath!;
            try
            {
                if (!Directory.Exists(readPath)) Directory.CreateDirectory(readPath);
            }
            catch (Exception)
            {
                readPath = Directory.GetCurrentDirectory() + "\\MarkingGraph";
                if (!Directory.Exists(readPath)) Directory.CreateDirectory(readPath);
            }

            for (int i = 0; i < MainImgView.GetInstance().DrMark.Count; i++)
            {
                DrMarkPath.Add($"{readPath}\\{i + 1}.ezm");
                MainImgView.GetInstance().DrMark[i].LoadFile($"{readPath}\\{i + 1}.ezm");
                MainImgView.GetInstance().DrMark[i].ExportJPG($"{AppDomain.CurrentDomain.BaseDirectory}\\{i + 1}.jpg", 0);
                MainImgView.GetInstance().DrMark[i].Redraw();
                MainImgView.GetInstance().DrMark[i].JumpToStartPos();
            }

            MainImgView.LoadingMarkingJpg();
            //MainImgView.DrMark[0].SetPower()

            MarkingSendParaEventManager.Subscribe(markingpara =>
            {
                if (markingpara.ClearFlag) ClearMarkingStatus();
                CurrentProcessLineNum = markingpara.ProcessLineNum;
                if (markingpara.Marking && !(markingpara.LongMenNum < 1 || markingpara.LongMenNum > 2 ||
                                             markingpara.ProcessLineNum < 1 ||
                                             markingpara.LaserPadPosition.SolderTapesGroupList == null))
                {
                    _vibraOfs = GetVirbarOfsValue(
                        (markingpara.LongMenNum - 1) * markingpara.ProcessLineNum,
                        this.Mwvm.Config.PcParamConfig,
                        new VibraOfs() { OfsX = markingpara.XPos, OfsY = markingpara.YPos, OfsA = markingpara.APos });
                    MarkingStatusFeedback = MainImgView.Marking(markingpara.LongMenNum, markingpara.ProcessLineNum,
                        markingpara.LaserPadPosition, _vibraOfs.OfsX,
                        _vibraOfs.OfsY, _vibraOfs.OfsA);
                    if (markingpara.LongMenNum == 1)
                    {
                        MarkingRecvStatusFeedback.MarkingStatusAFeedback = MarkingStatusFeedback;
                    }
                    if (markingpara.LongMenNum == 2)
                    {
                        MarkingRecvStatusFeedback.MarkingStatusBFeedback = MarkingStatusFeedback;
                    }
                    MarkingRecvStatusFeedbackEventManager.Publish(MarkingRecvStatusFeedback);
                }
                else
                {
                    MainImgView.ResetMark();
                }
            });

        }

        protected override void OnViewLoaded(object view)
        {
            try
            {
                if (MainImgView.LoadingPageWindow != null)
                {
                    MainImgView.LoadingPageWindow.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        MainImgView.LoadingPageWindow.LoadProgress(2);
                        MainImgView.LoadingPageWindow.Close();
                    }));
                }
                DebugPageViewModel debugPage = (DebugPageViewModel)Mwvm!.DebugPageContent!;
                debugModel = (DebugDetailViewModel)debugPage!.DebugDetailViewContent!;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public VibraOfs GetVirbarOfsValue(int index, StPCParam stPcParam, VibraOfs defaultVibraOfs)
        {
            VibraOfs vibraOfs = new VibraOfs() { OfsX = defaultVibraOfs.OfsX, OfsY = defaultVibraOfs.OfsY, OfsA = defaultVibraOfs.OfsA };
            switch (index)
            {
                case 1:
                    vibraOfs.OfsX = stPcParam.VibraOfs1X;
                    vibraOfs.OfsY = stPcParam.VibraOfs1Y;
                    vibraOfs.OfsA = stPcParam.VibraOfs1A;
                    return vibraOfs;
                case 2:
                    vibraOfs.OfsX = stPcParam.VibraOfs2X;
                    vibraOfs.OfsY = stPcParam.VibraOfs2Y;
                    vibraOfs.OfsA = stPcParam.VibraOfs2A;
                    return vibraOfs;
                case 3:
                    vibraOfs.OfsX = stPcParam.VibraOfs2X;
                    vibraOfs.OfsY = stPcParam.VibraOfs2Y;
                    vibraOfs.OfsA = stPcParam.VibraOfs2A;
                    return vibraOfs;
                case 4:
                    vibraOfs.OfsX = stPcParam.VibraOfs2X;
                    vibraOfs.OfsY = stPcParam.VibraOfs2Y;
                    vibraOfs.OfsA = stPcParam.VibraOfs2A;
                    return vibraOfs;
                case 5:
                    vibraOfs.OfsX = stPcParam.VibraOfs2X;
                    vibraOfs.OfsY = stPcParam.VibraOfs2Y;
                    vibraOfs.OfsA = stPcParam.VibraOfs2A;
                    return vibraOfs;
                case 6:
                    vibraOfs.OfsX = stPcParam.VibraOfs2X;
                    vibraOfs.OfsY = stPcParam.VibraOfs2Y;
                    vibraOfs.OfsA = stPcParam.VibraOfs2A;
                    return vibraOfs;
                case 7:
                    vibraOfs.OfsX = stPcParam.VibraOfs2X;
                    vibraOfs.OfsY = stPcParam.VibraOfs2Y;
                    vibraOfs.OfsA = stPcParam.VibraOfs2A;
                    return vibraOfs;
                case 8:
                    vibraOfs.OfsX = stPcParam.VibraOfs2X;
                    vibraOfs.OfsY = stPcParam.VibraOfs2Y;
                    vibraOfs.OfsA = stPcParam.VibraOfs2A;
                    return vibraOfs;
                case 9:
                    vibraOfs.OfsX = stPcParam.VibraOfs2X;
                    vibraOfs.OfsY = stPcParam.VibraOfs2Y;
                    vibraOfs.OfsA = stPcParam.VibraOfs2A;
                    return vibraOfs;
                case 10:
                    vibraOfs.OfsX = stPcParam.VibraOfs2X;
                    vibraOfs.OfsY = stPcParam.VibraOfs2Y;
                    vibraOfs.OfsA = stPcParam.VibraOfs2A;
                    return vibraOfs;
                case 11:
                    vibraOfs.OfsX = stPcParam.VibraOfs2X;
                    vibraOfs.OfsY = stPcParam.VibraOfs2Y;
                    vibraOfs.OfsA = stPcParam.VibraOfs2A;
                    return vibraOfs;
                case 12:
                    vibraOfs.OfsX = stPcParam.VibraOfs2X;
                    vibraOfs.OfsY = stPcParam.VibraOfs2Y;
                    vibraOfs.OfsA = stPcParam.VibraOfs2A;
                    return vibraOfs;
                default:
                    return vibraOfs;
            }
        }

        private void SetMarkingStatus(int longmen, int processline, int markpos)
        {
            if (longmen < 1 ||
                longmen > 2 ||
                processline < 1 ||
                processline > 6 ||
                markpos < 1 ||
                markpos > 6)
                return;
            GantryMarkingStatus[(longmen - 1) * 36 + (markpos - 1) * 6 + processline - 1] = true;
            for (int i = 1; i <= 6; i++)
            {
                if (processline != i)
                {
                    GantryMarkingStatus[(longmen - 1) * 36 + (markpos - 1) * 6 + i - 1] = false;
                }
            }
            switch(longmen)
            {
                case 1:
                    debugModel!.StationA = processline;
                    debugModel!.GantryMarkingStatus[markpos - 1] = true;
                    break;
                case 2:
                    debugModel!.StationB = processline;
                    debugModel!.GantryMarkingStatus[6+ markpos - 1] = true;
                    break;
            }
        }

        public void ClearMarkingStatus()
        {
            for (var i = 0; i < GantryMarkingStatus.Count; i++)
            {
                GantryMarkingStatus[i] = false;
            }
            for(int j=0;j<12;j++)
            {
                debugModel!.GantryMarkingStatus[j] = false;
            }
        }

        public void Test(int i, int j, int k)
        {
            //SetMarkingStatus(i, j, k);
            SetMarkingStatus(1, CurrentProcessLineNum, 1);
            SetMarkingStatus(1, CurrentProcessLineNum, 2);
            SetMarkingStatus(1, CurrentProcessLineNum, 3);
            SetMarkingStatus(1, CurrentProcessLineNum, 4);
            SetMarkingStatus(1, CurrentProcessLineNum, 5);
            SetMarkingStatus(1, CurrentProcessLineNum, 6);
            SetMarkingStatus(2, CurrentProcessLineNum, 1);
            SetMarkingStatus(2, CurrentProcessLineNum, 2);
            SetMarkingStatus(2, CurrentProcessLineNum, 3);
            SetMarkingStatus(2, CurrentProcessLineNum, 4);
            SetMarkingStatus(2, CurrentProcessLineNum, 5);
            SetMarkingStatus(2, CurrentProcessLineNum, 6);
        }

        private void Mark1_MarkEnd(object sender, _DMMMarkx641Events_MarkEndEvent e)
        {
            MarkingRecvPara.RecvFlagA[0] = true;
            MarkingRecvParaEventManager.Publish(MarkingRecvPara);           
            MainImgView?.MarkEndRestGraph(1);
        }
        private void Mark2_MarkEnd(object sender, _DMMMarkx641Events_MarkEndEvent e)
        {
            MarkingRecvPara.RecvFlagA[1] = true;
            MarkingRecvParaEventManager.Publish(MarkingRecvPara);
            MainImgView?.MarkEndRestGraph(2);
        }
        private void Mark3_MarkEnd(object sender, _DMMMarkx641Events_MarkEndEvent e)
        {
            MarkingRecvPara.RecvFlagA[2] = true;
            MarkingRecvParaEventManager.Publish(MarkingRecvPara);
            MainImgView?.MarkEndRestGraph(3);
        }
        private void Mark4_MarkEnd(object sender, _DMMMarkx641Events_MarkEndEvent e)
        {
            MarkingRecvPara.RecvFlagA[3] = true;
            MarkingRecvParaEventManager.Publish(MarkingRecvPara);
            MainImgView?.MarkEndRestGraph(4);
        }
        private void Mark5_MarkEnd(object sender, _DMMMarkx641Events_MarkEndEvent e)
        {
            MarkingRecvPara.RecvFlagA[4] = true;
            MarkingRecvParaEventManager.Publish(MarkingRecvPara);
            MainImgView?.MarkEndRestGraph(5);
        }
        private void Mark6_MarkEnd(object sender, _DMMMarkx641Events_MarkEndEvent e)
        {
            MarkingRecvPara.RecvFlagA[5] = true;
            MarkingRecvParaEventManager.Publish(MarkingRecvPara);
            MainImgView?.MarkEndRestGraph(6);
        }
        private void Mark7_MarkEnd(object sender, _DMMMarkx641Events_MarkEndEvent e)
        {
            MarkingRecvPara.RecvFlagB[0] = true;
            MarkingRecvParaEventManager.Publish(MarkingRecvPara);
            MainImgView?.MarkEndRestGraph(7);
        }
        private void Mark8_MarkEnd(object sender, _DMMMarkx641Events_MarkEndEvent e)
        {
            MarkingRecvPara.RecvFlagB[1] = true;
            MarkingRecvParaEventManager.Publish(MarkingRecvPara);
            MainImgView?.MarkEndRestGraph(8);
        }
        private void Mark9_MarkEnd(object sender, _DMMMarkx641Events_MarkEndEvent e)
        {
            MarkingRecvPara.RecvFlagB[2] = true;
            MarkingRecvParaEventManager.Publish(MarkingRecvPara);
            MainImgView?.MarkEndRestGraph(9);
        }
        private void Mark10_MarkEnd(object sender, _DMMMarkx641Events_MarkEndEvent e)
        {
            MarkingRecvPara.RecvFlagB[3] = true;
            MarkingRecvParaEventManager.Publish(MarkingRecvPara);
            MainImgView?.MarkEndRestGraph(10);
        }
        private void Mark11_MarkEnd(object sender, _DMMMarkx641Events_MarkEndEvent e)
        {
            MarkingRecvPara.RecvFlagB[4] = true;
            MarkingRecvParaEventManager.Publish(MarkingRecvPara);
            MainImgView?.MarkEndRestGraph(11);
        }
        private void Mark12_MarkEnd(object sender, _DMMMarkx641Events_MarkEndEvent e)
        {
            MarkingRecvPara.RecvFlagB[5] = true;
            MarkingRecvParaEventManager.Publish(MarkingRecvPara);
            MainImgView?.MarkEndRestGraph(12);
        }
        private void Hw_MarkEnd1()
        {
            SetMarkingStatus(1, 1, 1);
        }

        private void Hw_MarkEnd2()
        {
            SetMarkingStatus(1, 1, 2);
        }

        private void Hw_MarkEnd3()
        {
            SetMarkingStatus(1, 1, 3);
        }

        private void Hw_MarkEnd4()
        {
            SetMarkingStatus(1, 1, 4);
        }

        private void Hw_MarkEnd5()
        {
            SetMarkingStatus(1, 1, 5);
        }

        private void Hw_MarkEnd6()
        {
            SetMarkingStatus(1, 1, 6);
        }

        private void Hw_MarkEnd7()
        {
            SetMarkingStatus(2, 2, 1);
        }

        private void Hw_MarkEnd8()
        {
            SetMarkingStatus(2, 2, 2);
        }

        private void Hw_MarkEnd9()
        {
            SetMarkingStatus(2, 2, 3);
        }

        private void Hw_MarkEnd10()
        {
            SetMarkingStatus(2, 2, 4);
        }

        private void Hw_MarkEnd11()
        {
            SetMarkingStatus(2, 2, 5);
        }

        private void Hw_MarkEnd12()
        {
            SetMarkingStatus(2, 2, 6);
        }

        #region 事件监听

        private bool _markEnd1Change;
        public bool MarkEnd1Change
        {
            get => _markEnd1Change;
            set
            {
                if (_markEnd1Change != value )
                {
                    OnHwMarkEnd1();
                }
                _markEnd1Change = value;
            }
        }

        private bool _markEnd2Change;
        public bool MarkEnd2Change
        {
            get => _markEnd2Change;
            set
            {
                if (_markEnd2Change != value && _markEnd2Change == false)
                {
                    OnHwMarkEnd2();
                }
                _markEnd2Change = value;
            }
        }

        private bool _markEnd3Change;
        public bool MarkEnd3Change
        {
            get => _markEnd3Change;
            set
            {
                if (_markEnd3Change != value && _markEnd3Change == false)
                {
                    OnHwMarkEnd3();
                }
                _markEnd3Change = value;
            }
        }

        private bool _markEnd4Change;
        public bool MarkEnd4Change
        {
            get => _markEnd4Change;
            set
            {
                if (_markEnd4Change != value && _markEnd4Change == false)
                {
                    OnHwMarkEnd4();
                }
                _markEnd4Change = value;
            }
        }

        private bool _markEnd5Change;
        public bool MarkEnd5Change
        {
            get => _markEnd5Change;
            set
            {
                if (_markEnd5Change != value && _markEnd5Change == false)
                {
                    OnHwMarkEnd5();
                }
                _markEnd5Change = value;
            }
        }

        private bool _markEnd6Change;
        public bool MarkEnd6Change
        {
            get => _markEnd6Change;
            set
            {
                if (_markEnd6Change != value && _markEnd6Change == false)
                {
                    OnHwMarkEnd6();
                }
                _markEnd6Change = value;
            }
        }

        private bool _markEnd7Change;
        public bool MarkEnd7Change
        {
            get => _markEnd7Change;
            set
            {
                if (_markEnd7Change != value && _markEnd7Change == false)
                {
                    OnHwMarkEnd7();
                }
                _markEnd7Change = value;
            }
        }

        private bool _markEnd8Change;
        public bool MarkEnd8Change
        {
            get => _markEnd8Change;
            set
            {
                if (_markEnd8Change != value && _markEnd8Change == false)
                {
                    OnHwMarkEnd8();
                }
                _markEnd8Change = value;
            }
        }

        private bool _markEnd9Change;
        public bool MarkEnd9Change
        {
            get => _markEnd9Change;
            set
            {
                if (_markEnd9Change != value && _markEnd9Change == false)
                {
                    OnHwMarkEnd9();
                }
                _markEnd9Change = value;
            }
        }

        private bool _markEnd10Change;
        public bool MarkEnd10Change
        {
            get => _markEnd10Change;
            set
            {
                if (_markEnd10Change != value && _markEnd10Change == false)
                {
                    OnHwMarkEnd10();
                }
                _markEnd10Change = value;
            }
        }

        private bool _markEnd11Change;
        public bool MarkEnd11Change
        {
            get => _markEnd11Change;
            set
            {
                if (_markEnd11Change != value && _markEnd11Change == false)
                {
                    OnHwMarkEnd11();
                }
                _markEnd11Change = value;
            }
        }

        private bool _markEnd12Change;
        public bool MarkEnd12Change
        {
            get => _markEnd12Change;
            set
            {
                if (_markEnd12Change != value && _markEnd12Change == false)
                {
                    OnHwMarkEnd12();
                }
                _markEnd12Change = value;
            }
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

        private bool? active = false;
        public bool? Active
        {
            get { return active; }
            set
            {
                active = value;
                NotifyOfPropertyChange(() => Active);
            }
        }

        private Login? role = new Login() { UserName = "Observer", Password = "123", DebugLimit = false, ParamSetLimit = false, PhotoLimit = false,MarkingLimit=false };
        public Login? Role
        {
            get { return role; }
            set
            {
                role = value;
                NotifyOfPropertyChange(() => Role);
            }
        }

        private bool _choosed;
        public bool Choosed
        {
            get => _choosed;
            set
            {
                _choosed = value;
                NotifyOfPropertyChange(() => Choosed);
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

        private StCmd _sysCmd = null!;

        public StCmd SysCmd
        {
            get => _sysCmd;
            set
            {
                _sysCmd = value;
                NotifyOfPropertyChange(() => SysCmd);
            }
        }

        private bool? ischeck = false;
        public bool? Ischeck
        {
            get => ischeck;
            set
            {
                ischeck = value;
                NotifyOfPropertyChange(() => Ischeck);
            }
        }

        private ObservableCollection<bool> _gantryMarkingStatus = null!;
        public ObservableCollection<bool> GantryMarkingStatus
        {
            get => _gantryMarkingStatus;
            set
            {
                _gantryMarkingStatus = value;
                NotifyOfPropertyChange(() => GantryMarkingStatus);
            }
        }

        private ObservableCollection<ImageSource> _markingMateImage = null!;
        public ObservableCollection<ImageSource> MarkingMateImage
        {
            get => _markingMateImage;
            set
            {
                _markingMateImage = value;
                NotifyOfPropertyChange(() => MarkingMateImage);
            }
        }

        #endregion

        #region 方法

        public void AddMarkGraph(int i)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "DRMark EZM File (.ezm)|*.ezm"
            };
            var result = openFileDialog.ShowDialog();
#pragma warning disable CS8629
            if ((bool)result)
#pragma warning restore CS8629
            {
                DrMarkPath[i - 1] = openFileDialog.FileName;
                MainImgView!.GetInstance().DrMark[i - 1].LoadFile(openFileDialog.FileName);
                MainImgView!.GetInstance().DrMark[i - 1].ExportJPG($"{AppDomain.CurrentDomain.BaseDirectory}\\{i}.jpg", 0);
                MainImgView!.LoadingMarkingJpg();
            }
        }

        public void EditMarkGraph(int i)
        {
            MainImgView!.GetInstance().DrMark[i - 1].RunMarkingMate(DrMarkPath[i - 1], 0);
        }

        public void Marking(int i)
        {
            _vibraOfs = GetVirbarOfsValue(i,
                this.Mwvm.Config.PcParamConfig,
                new VibraOfs() { OfsX = 0, OfsY = 0, OfsA = 0 });
            if(this.Mwvm.Engine!._laserPadPosition1==null || this.Mwvm.Engine!._laserPadPosition2==null)
            {
                string msg = "{\r\n  \"groupId\": 1,\r\n  \"workStationId\": 0,\r\n  \"hash\": null,\r\n  \"command\": null,\r\n  \"status\": null,\r\n  \"status_code\": 0,\r\n  \"message\": null,\r\n  \"solder_tapes_group_list\": [{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  },\r\n{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  },\r\n{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  },\r\n{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  },\r\n{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  },\r\n{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  }],\r\n  \"response_type\": \"take_photo\",\r\n  \"time_cost\": 0.0,\r\n  \"snap_cost\": 0.0,\r\n  \"find_pad_cost\": 0.0,\r\n  \"find_si_dirty_spots_cost\": 0.0\r\n}";
                this.Mwvm.Engine!._laserPadPosition1 = LaserPadPosition.FromJson(msg);
                this.Mwvm.Engine!._laserPadPosition2 = LaserPadPosition.FromJson(msg);
            }
            if (i <= 6)
            {
                if (MainImgView != null) MainImgView.ManualMarking(this.Mwvm.Engine!._laserPadPosition1, i, _vibraOfs.OfsX, _vibraOfs.OfsY, _vibraOfs.OfsA);
            }
            else
            {
                if (MainImgView != null) MainImgView.ManualMarking(this.Mwvm.Engine!._laserPadPosition2, i, _vibraOfs.OfsX, _vibraOfs.OfsY, _vibraOfs.OfsA);
            }
        }

        public void LaserOutputCheck(int num)
        {
            if (Choosed)
            {
                MainImgView!.GetInstance().DrMark[num - 1].LaserOn();
            }
            else
            {
                MainImgView!.GetInstance().DrMark[num - 1].LaserOff();
            }
        }

        public void ManualOutputCheck(int num)
        {
            switch (num)
            {
                case 1:
                    if (IoOutput.LedLight)
                    {
                        if (this.Mwvm.Controller != null)
                        {
                            this.Mwvm.Controller.IoOutput_CMD.LedLight = true;
                            this.Mwvm.Controller.WriteToControllerByIndex(DRsoft.Engine.Model.Enum.ControllerInputIndex
                                .IN_INDEX_OutputIO);
                        }
                    }
                    else
                    {
                        if (this.Mwvm.Controller != null)
                        {
                            this.Mwvm.Controller.IoOutput_CMD.LedLight = false;
                            this.Mwvm.Controller.WriteToControllerByIndex(DRsoft.Engine.Model.Enum.ControllerInputIndex
                                .IN_INDEX_OutputIO);
                        }
                    }
                    break;
                case 2:
                    if (this.Mwvm.Controller != null)
                    {
                        this.Mwvm.Controller.CmdCommand.RepairMode = !this.Mwvm.Controller.CmdCommand.RepairMode;
                        this.Mwvm.Controller.WriteToControllerByIndex(DRsoft.Engine.Model.Enum.ControllerInputIndex
                            .IN_INDEX_CMD);
                    }
                    break;
                case 3:
                    if (this.Mwvm.Controller != null)
                    {
                        this.Mwvm.Controller.CmdCommand.Simu_Downstream = !this.Mwvm.Controller.CmdCommand.Simu_Downstream;
                        this.Mwvm.Controller.WriteToControllerByIndex(DRsoft.Engine.Model.Enum.ControllerInputIndex
                            .IN_INDEX_CMD);
                    }
                    break;
                case 4:
                    if (IoOutput.Gantry1Light)
                    {
                        if (this.Mwvm.Controller != null)
                        {
                            this.Mwvm.Controller.IoOutput_CMD.Gantry1Light = true;
                            this.Mwvm.Controller.WriteToControllerByIndex(DRsoft.Engine.Model.Enum.ControllerInputIndex
                                .IN_INDEX_OutputIO);
                        }
                    }
                    else
                    {
                        if (this.Mwvm.Controller != null)
                        {
                            this.Mwvm.Controller.IoOutput_CMD.Gantry1Light = false;
                            this.Mwvm.Controller.WriteToControllerByIndex(DRsoft.Engine.Model.Enum.ControllerInputIndex
                                .IN_INDEX_OutputIO);
                        }
                    }
                    break;
                case 5:
                    if (IoOutput.Gantry2Light)
                    {
                        if (this.Mwvm.Controller != null)
                        {
                            this.Mwvm.Controller.IoOutput_CMD.Gantry2Light = true;
                            this.Mwvm.Controller.WriteToControllerByIndex(DRsoft.Engine.Model.Enum.ControllerInputIndex
                                .IN_INDEX_OutputIO);
                        }
                    }
                    else
                    {
                        if (this.Mwvm.Controller != null)
                        {
                            this.Mwvm.Controller.IoOutput_CMD.Gantry2Light = false;
                            this.Mwvm.Controller.WriteToControllerByIndex(DRsoft.Engine.Model.Enum.ControllerInputIndex
                                .IN_INDEX_OutputIO);
                        }
                    }
                    break;
                case 6:
                    Mwvm.Controller?.ReadFromControllerByIndex(ControllerOutputIndex.OUT_INDEX_HMIDATA_FROMPLC);
                    HmiToPlc commands = CloneHelper.DeepClone<HmiToPlc>(Mwvm.Controller?.HMIDataFromPLC);
                    commands.DirtyNeedClean = true;
                    Mwvm!.Controller!.HMIDataToPLC = commands;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);
                    break;
                case 7:
                    Mwvm.Controller?.ReadFromControllerByIndex(ControllerOutputIndex.OUT_INDEX_HMIDATA_FROMPLC);
                    HmiToPlc command = CloneHelper.DeepClone<HmiToPlc>(Mwvm.Controller?.HMIDataFromPLC);
                    command.ChangeDirtyField = true;
                    Mwvm!.Controller!.HMIDataToPLC = command;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);
                    break;
            }
        }
        #endregion

        protected override void OnViewAttached(object view, object context)
        {
            base.OnViewAttached(view, context);
            if (view != null)
            {

            }
        }

    }
}
