using Caliburn.Micro;
using DRsoft.Engine.Model.Controller;
using Engine.Models;
using DRsoft.Engine.Model.Enum;
using Engine.Transfer;
using System.Windows.Threading;
using DRsoft.Engine.Core.PowerMeter;
using Engine.ViewModels.MainPageComponent;
using Engine.Views.MainPageComponent;
using DRsoft.Engine.Model.Vision;
using System.Collections.ObjectModel;
using DRsoft.Runtime.Core.Platform.Logging;
using DRsoft.Engine.Model.DataBaseTable;
using DRsoft.Runtime.Core.Platform.Events;
using System.Windows.Documents;

namespace Engine.ViewModels.DebugPageComponent
{
    public class DebugDetailViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public DebugPageViewModel Dpvm;
        public MainWindowViewModel Mwvm;
        AbstractPowerMeterControl DebugPowerMeter;
        public static int CurrentTestUnitNum = 0;
        private List<TestPoint> testPoints = new List<TestPoint>();
        public LoginPageViewModel Login;
        public PubSubEvent<Login> RoleChangeEventManager;
        public Login CurrentRole = new Login();

        public DispatcherTimer powerTest = new DispatcherTimer();
        private DispatcherTimer stateRefresh = new DispatcherTimer();

        public MainImgView? _mainImgView;
        public MainImgViewModel? _mainImgViewMode;
        int count = 0;
        string str1 = "";
        string str2 = "";
        string msg1 = "abc";
        string msg2 = "abc";
        bool runStart = false;
        bool run = false;
        bool timerStart = true;

        bool markViewLoad = false;

        PowerTestFlow TestFlow = PowerTestFlow.Standby;
        public List<StAxis_Par> ListPars = new List<StAxis_Par>();

        public DebugDetailViewModel(IWindowManager iwindow, MainWindowViewModel mwm, DebugPageViewModel dpm)
        {
            this.WindowManager = iwindow;
            this.Dpvm = dpm;
            this.Mwvm = mwm;

            Input = new StInput();

            Output = new StOutput();

            AxisGantry1 = new StAxisStatus();
            AxisGantry2 = new StAxisStatus();
            AxisCamShutter1 = new StAxisStatus();
            AxisCamShutter2 = new StAxisStatus();
            AxisPowerMeter = new StAxisStatus();
            AxisAlign11 = new StAxisStatus();
            AxisAlign12 = new StAxisStatus();
            AxisAlign21 = new StAxisStatus();
            AxisAlign22 = new StAxisStatus();
            AxisZ1 = new StAxisStatus();
            AxisZ2 = new StAxisStatus();
            AxisPeeling1 = new StAxisStatus();
            AxisPeeling2 = new StAxisStatus();
            AxisStationABelt = new StAxisStatus();
            AxisStationBBelt = new StAxisStatus();
            Axis_UwLift = new StAxisStatus();
            AxisUw = new StAxisStatus();
            AxisRwLift = new StAxisStatus();
            AxisRw = new StAxisStatus();
            AxisClean = new StAxisStatus();
            AxisUwSteer = new StAxisStatus();
            AxisRwSteer = new StAxisStatus();

            ListStatus.Add(Mwvm!.Controller!.StatusAxisGantry11);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisGantry12);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisGantry21);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisGantry22);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisAlign11);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisAlign12);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisAlign21);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisAlign22);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisCamShutter1);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisCamShutter2);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisZ1);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisZ2);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisUwLift);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisUw);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisRwLift);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisRw);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisClean);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisPowerMeter);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisUwSteer);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisPeeling1);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisStationABelt);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisPeeling2);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisStationBBelt);
            ListStatus.Add(Mwvm!.Controller!.StatusAxisRwSteer);

            powerTest.Interval = TimeSpan.FromMilliseconds(500);
            powerTest.Tick += new EventHandler(Refresh);
            powerTest.Start();

            stateRefresh.Interval = TimeSpan.FromMilliseconds(500);
            stateRefresh.Tick += new EventHandler(StateRefresh);

            Nums = new double[12, 3];
            for (int i = 1; i < 9; i++)
            {
                ItemsIndex.Add(i);
            }

            GantryMarkingStatus = new ObservableCollection<bool>();
            for (int j = 0; j < 12; j++)
            {
                GantryMarkingStatus.Add(false);
            }
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            //获取功率计
            DebugPowerMeter = Mwvm!.Engine!.PowerMeter;
            //获取打标卡的数据
            PowerDataInitial();
            //视觉相关操作
            Camera1IsConnect = Mwvm.Engine.VisionProduction[0].IsConnected;
            Camera2IsConnect = Mwvm.Engine.VisionProduction[1].IsConnected;
            ReceiveMsg1 = Mwvm.Engine.VisionProduction[0].ReceiveMsg;
            ReceiveMsg2 = Mwvm.Engine.VisionProduction[1].ReceiveMsg;

        }

        public void StateRefresh(object? obj, EventArgs e)
        {
            if (Mwvm!.Engine!.VisionProduction[0].cameraDataList.Count > 0 && !run)
                msg1 = string.Join("\n", Mwvm!.Engine.VisionProduction[0].cameraDataList.ToArray());
            if (Mwvm!.Engine!.VisionProduction[0].cameraDataList.Count > 0 && !run)
                msg2 = string.Join("\n", Mwvm!.Engine.VisionProduction[1].cameraDataList.ToArray());

            if (str1.CompareTo(msg1) != 0 && SendMsg1 != "" && Mwvm!.Engine.VisionProduction[0].cameraDataList.Count >= 3)
            {
                ReceiveMsg1 = "";
                str1 = msg1;
                ReceiveMsg1 += msg1;
            }
            if (str2.CompareTo(msg2) != 0 && SendMsg2 != "" && Mwvm!.Engine.VisionProduction[1].cameraDataList.Count >= 3)
            {
                ReceiveMsg2 = "";
                str2 = msg2;
                ReceiveMsg2 += msg2;
            }
            Camera1IsConnect = Mwvm.Engine.VisionProduction[0].IsConnected;
            Camera2IsConnect = Mwvm.Engine.VisionProduction[1].IsConnected;

            Input = Mwvm.Controller?.IoInput;
            Output = Mwvm.Controller?.IoOutput;
            MarkInitial = Mwvm.MarkingMateInitFinish;

            AxisGantry1 = Mwvm.Controller?.StatusAxisGantry11.sT_AxisStatus;
            AxisGantry2 = Mwvm.Controller?.StatusAxisGantry21.sT_AxisStatus;
            AxisCamShutter1 = Mwvm.Controller?.StatusAxisCamShutter1.sT_AxisStatus;
            AxisCamShutter2 = Mwvm.Controller?.StatusAxisCamShutter2.sT_AxisStatus;
            AxisPowerMeter = Mwvm.Controller?.StatusAxisPowerMeter.sT_AxisStatus;
            AxisAlign11 = Mwvm.Controller?.StatusAxisAlign11.sT_AxisStatus;
            AxisAlign12 = Mwvm.Controller?.StatusAxisAlign12.sT_AxisStatus;
            AxisAlign21 = Mwvm.Controller?.StatusAxisAlign21.sT_AxisStatus;
            AxisAlign22 = Mwvm.Controller?.StatusAxisAlign22.sT_AxisStatus;
            AxisZ1 = Mwvm.Controller?.StatusAxisZ1.sT_AxisStatus;
            AxisZ2 = Mwvm.Controller?.StatusAxisZ2.sT_AxisStatus;
            Axis_UwLift = Mwvm.Controller?.StatusAxisUwLift.sT_AxisStatus;
            AxisPeeling1 = Mwvm.Controller?.StatusAxisPeeling1.sT_AxisStatus;
            AxisPeeling2 = Mwvm.Controller?.StatusAxisPeeling2.sT_AxisStatus;
            AxisStationABelt = Mwvm.Controller?.StatusAxisStationABelt.sT_AxisStatus;
            AxisStationBBelt = Mwvm.Controller?.StatusAxisStationBBelt.sT_AxisStatus;
            AxisUw = Mwvm.Controller?.StatusAxisUw.sT_AxisStatus;
            AxisRwLift = Mwvm.Controller?.StatusAxisRwLift.sT_AxisStatus;
            AxisRw = Mwvm.Controller?.StatusAxisRw.sT_AxisStatus;
            AxisClean = Mwvm.Controller?.StatusAxisClean.sT_AxisStatus;
            AxisUwSteer = Mwvm.Controller?.StatusAxisUwSteer.sT_AxisStatus;
            AxisRwSteer = Mwvm.Controller?.StatusAxisRwSteer.sT_AxisStatus;

            MachineStatus = Mwvm.Controller?.SysStatus;
            //当前角色权限
            Role = Const.CurrentRole;
        }
        public void Refresh(object? obj, EventArgs e)
        {
            if (!markViewLoad)
            {
                //获取打标卡控件
                MainPageViewModel mainpage = (MainPageViewModel)Mwvm!.MainPageContent!;
                _mainImgViewMode = (MainImgViewModel)mainpage!.MainImgViewContent!;
                _mainImgView = _mainImgViewMode.MainImgView;
                markViewLoad = true;
                //获取Log中的对象
                Login = (LoginPageViewModel)Mwvm!.LoginPageContent!;
                RoleChangeEventManager = Mwvm!.Aggregator!.GetEvent<PubSubEvent<Login>>();
            }
            bool alarm = Mwvm.Controller!.SysStatus.AutoAlarm;
            run = Mwvm.Controller.SysStatus.Running;
            switch (TestFlow)
            {
                case PowerTestFlow.Standby:
                    if (alarm || run)
                        break;
                    else
                    {
                        TestFlow = PowerTestFlow.StartTest;
                    }
                    break;
                case PowerTestFlow.StartTest:
                    bool start = Mwvm.Controller.PLCDataToHMI.PowerMeterStart;
                    if (start)
                    {
                        TestFlow = PowerTestFlow.SetPara;
                        Mwvm.Controller?.ReadFromControllerByIndex(ControllerOutputIndex.OUT_INDEX_HMIDATA_FROMPLC);
                        HmiToPlc commands = CloneHelper.DeepClone<HmiToPlc>(Mwvm.Controller?.HMIDataFromPLC);
                        commands.ClearPowerMeterStart = true;
                        Mwvm!.Controller!.HMIDataToPLC = commands;
                        Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);
                    }
                    break;
                case PowerTestFlow.SetPara: //下发打标参数
                    CurrentTestUnitNum = Mwvm.Controller.PLCDataToHMI.iPowerTestUnitNum;
                    _mainImgView!.MarkingPowerTestOn(CurrentTestUnitNum, testPoints[0].PowerTestRate,
                        testPoints[0].TestX, testPoints[0].TestY);
                    TestFlow = PowerTestFlow.SentCmd;
                    break;
                case PowerTestFlow.SentCmd:
                    string SendDataPower = "*CVU";
                    DebugPowerMeter.Write(SendDataPower);
                    count = 0;
                    TestFlow = PowerTestFlow.WaitTime;
                    break;
                case PowerTestFlow.WaitTime:
                    count++;
                    if (count > 25)
                    {
                        int cloumn = Mwvm.Controller.PLCDataToHMI.iPowerTestUnitNum;
                        int row = Mwvm.Controller.PLCDataToHMI.iPowerTestLaserNum;
                        double data = DebugPowerMeter.Receive;
                        //Nums[cloumn, row] = data;
                        DataExchange(cloumn, row, data);
                        TestFlow = PowerTestFlow.LaserOff;
                    }
                    break;
                case PowerTestFlow.LaserOff:
                    _mainImgView!.MarkingPowerTestOff(CurrentTestUnitNum);
                    TestFlow = PowerTestFlow.Finish;
                    break;
                case PowerTestFlow.Finish:
                    Mwvm.Controller?.ReadFromControllerByIndex(ControllerOutputIndex.OUT_INDEX_HMIDATA_FROMPLC);
                    HmiToPlc command = CloneHelper.DeepClone<HmiToPlc>(Mwvm.Controller?.HMIDataFromPLC);
                    command.PowerMeterDone = true;
                    Mwvm.Controller!.HMIDataToPLC = command;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);
                    TestFlow = PowerTestFlow.Standby;
                    break;

            }
            if (run)
            {
                if (!runStart)
                {
                    runStart = true;
                    TestFlow = PowerTestFlow.Standby;
                    SendMsg1 = "";
                    SendMsg2 = "";
                    UserLogin[] user = Login.UserManagers.Where(t => t.UserName == "Observer").ToArray();
                    Const.CurrentRole = user[0];
                    CurrentRole.UserName = Const.CurrentRole.UserName;
                    CurrentRole.Password = Const.CurrentRole.Password;
                    CurrentRole.DebugLimit = Const.CurrentRole.DebugLimit;
                    CurrentRole.MarkingLimit = Const.CurrentRole.MarkingLimit;
                    CurrentRole.PhotoLimit = Const.CurrentRole.PhotoLimit;
                    CurrentRole.ParamSetLimit = Const.CurrentRole.ParamSetLimit;
                    RoleChangeEventManager.Publish(CurrentRole);
                    Mwvm.UserName = "用户名 :" + CurrentRole.UserName;
                }
            }
            else
                runStart = false;

            if (Mwvm.index != 2 && timerStart)
            {
                timerStart = false;
                stateRefresh.Stop();
            }
            else if (Mwvm.index == 2 && !timerStart)
            {
                timerStart = true;
                stateRefresh.Start();
            }
        }
        private void DataExchange(int cloumn, int row, double value)
        {
            int num = 3 * cloumn + row - 4;
            if (num < 0)
                return;
            PowerData[num] = value;
        }

        public void PowerDataInitial()
        {
            TestPoint test1 = new TestPoint();
            test1.PowerTestRate = Mwvm.Config.PcParamConfig.Laser1Power;
            test1.TestX = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos1X;
            test1.TestY = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos1Y;
            test1.Ratio = Mwvm.Config.PcParamConfig.PowerMeterPercent;
            testPoints.Add(test1);
            PowerSetData[0] = test1.PowerTestRate;

            TestPoint test2 = new TestPoint();
            test2.PowerTestRate = Mwvm.Config.PcParamConfig.Laser2Power;
            test2.TestX = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos2X;
            test2.TestY = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos2Y;
            test2.Ratio = Mwvm.Config.PcParamConfig.PowerMeterPercent;
            testPoints.Add(test2);
            PowerSetData[1] = test2.PowerTestRate;

            TestPoint test3 = new TestPoint();
            test3.PowerTestRate = Mwvm.Config.PcParamConfig.Laser3Power;
            test3.TestX = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos3X;
            test3.TestY = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos3Y;
            test3.Ratio = Mwvm.Config.PcParamConfig.PowerMeterPercent;
            testPoints.Add(test3);
            PowerSetData[2] = test3.PowerTestRate;

            TestPoint test4 = new TestPoint();
            test4.PowerTestRate = Mwvm.Config.PcParamConfig.Laser4Power;
            test4.TestX = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos4X;
            test4.TestY = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos4Y;
            test4.Ratio = Mwvm.Config.PcParamConfig.PowerMeterPercent;
            testPoints.Add(test4);
            PowerSetData[3] = test2.PowerTestRate;

            TestPoint test5 = new TestPoint();
            test5.PowerTestRate = Mwvm.Config.PcParamConfig.Laser5Power;
            test5.TestX = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos5X;
            test5.TestY = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos5Y;
            test5.Ratio = Mwvm.Config.PcParamConfig.PowerMeterPercent;
            testPoints.Add(test5);
            PowerSetData[4] = test5.PowerTestRate;

            TestPoint test6 = new TestPoint();
            test6.PowerTestRate = Mwvm.Config.PcParamConfig.Laser6Power;
            test6.TestX = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos6X;
            test6.TestY = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos6Y;
            test6.Ratio = Mwvm.Config.PcParamConfig.PowerMeterPercent;
            testPoints.Add(test6);
            PowerSetData[5] = test6.PowerTestRate;

            TestPoint test7 = new TestPoint();
            test7.PowerTestRate = Mwvm.Config.PcParamConfig.Laser7Power;
            test7.TestX = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos7X;
            test7.TestY = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos7Y;
            test7.Ratio = Mwvm.Config.PcParamConfig.PowerMeterPercent;
            testPoints.Add(test7);
            PowerSetData[6] = test7.PowerTestRate;

            TestPoint test8 = new TestPoint();
            test8.PowerTestRate = Mwvm.Config.PcParamConfig.Laser8Power;
            test8.TestX = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos8X;
            test8.TestY = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos8Y;
            test8.Ratio = Mwvm.Config.PcParamConfig.PowerMeterPercent;
            testPoints.Add(test8);
            PowerSetData[7] = test8.PowerTestRate;

            TestPoint test9 = new TestPoint();
            test9.PowerTestRate = Mwvm.Config.PcParamConfig.Laser9Power;
            test9.TestX = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos9X;
            test9.TestY = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos9Y;
            test9.Ratio = Mwvm.Config.PcParamConfig.PowerMeterPercent;
            testPoints.Add(test9);
            PowerSetData[8] = test9.PowerTestRate;

            TestPoint test10 = new TestPoint();
            test10.PowerTestRate = Mwvm.Config.PcParamConfig.Laser10Power;
            test10.TestX = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos10X;
            test10.TestY = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos10Y;
            test10.Ratio = Mwvm.Config.PcParamConfig.PowerMeterPercent;
            testPoints.Add(test10);
            PowerSetData[9] = test10.PowerTestRate;

            TestPoint test11 = new TestPoint();
            test11.PowerTestRate = Mwvm.Config.PcParamConfig.Laser11Power;
            test11.TestX = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos11X;
            test11.TestY = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos11Y;
            test11.Ratio = Mwvm.Config.PcParamConfig.PowerMeterPercent;
            testPoints.Add(test11);
            PowerSetData[10] = test11.PowerTestRate;

            TestPoint test12 = new TestPoint();
            test12.PowerTestRate = Mwvm.Config.PcParamConfig.Laser12Power;
            test12.TestX = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos12X;
            test12.TestY = Mwvm.Config.PcParamConfig.PowerMeterMeasurePos12Y;
            test12.Ratio = Mwvm.Config.PcParamConfig.PowerMeterPercent;
            testPoints.Add(test12);
            PowerSetData[11] = test12.PowerTestRate;
        }

        public void AxisParaListInitial()
        {
            ListPars.Clear();
            ListPars.Add(Mwvm!.Controller!.ParaAxisGantry11);
            ListPars.Add(Mwvm!.Controller!.ParaAxisGantry12);
            ListPars.Add(Mwvm!.Controller!.ParaAxisGantry21);
            ListPars.Add(Mwvm!.Controller!.ParaAxisGantry22);
            ListPars.Add(Mwvm!.Controller!.ParaAxisAlign11);
            ListPars.Add(Mwvm!.Controller!.ParaAxisAlign12);
            ListPars.Add(Mwvm!.Controller!.ParaAxisAlign21);
            ListPars.Add(Mwvm!.Controller!.ParaAxisAlign22);
            ListPars.Add(Mwvm!.Controller!.ParaAxisCamShutter1);
            ListPars.Add(Mwvm!.Controller!.ParaAxisCamShutter2);
            ListPars.Add(Mwvm!.Controller!.ParaAxisZ1);
            ListPars.Add(Mwvm!.Controller!.ParaAxisZ2);
            ListPars.Add(Mwvm!.Controller!.ParaAxisUwLift);
            ListPars.Add(Mwvm!.Controller!.ParaAxisUw);
            ListPars.Add(Mwvm!.Controller!.ParaAxisRwLift);
            ListPars.Add(Mwvm!.Controller!.ParaAxisRw);
            ListPars.Add(Mwvm!.Controller!.ParaAxisClean);
            ListPars.Add(Mwvm!.Controller!.ParaAxisPowerMeter);
            ListPars.Add(Mwvm!.Controller!.ParaAxisUwSteer);
            ListPars.Add(Mwvm!.Controller!.ParaAxisPeeling1);
            ListPars.Add(Mwvm!.Controller!.ParaAxisStationABelt);
            ListPars.Add(Mwvm!.Controller!.ParaAxisPeeling2);
            ListPars.Add(Mwvm!.Controller!.ParaAxisStationBBelt);
            ListPars.Add(Mwvm!.Controller!.ParaAxisRwSteer);
        }

        private void AxisStatusRefresh()
        {
            ListStatus[0] = Mwvm!.Controller!.StatusAxisGantry11;
            ListStatus[1] = Mwvm!.Controller!.StatusAxisGantry12;
            ListStatus[2] = Mwvm!.Controller!.StatusAxisGantry21;
            ListStatus[3] = Mwvm!.Controller!.StatusAxisGantry22;
            ListStatus[4] = Mwvm!.Controller!.StatusAxisAlign11;
            ListStatus[5] = Mwvm!.Controller!.StatusAxisAlign12;
            ListStatus[6] = Mwvm!.Controller!.StatusAxisAlign21;
            ListStatus[7] = Mwvm!.Controller!.StatusAxisAlign22;
            ListStatus[8] = Mwvm!.Controller!.StatusAxisCamShutter1;
            ListStatus[9] = Mwvm!.Controller!.StatusAxisCamShutter2;
            ListStatus[10] = Mwvm!.Controller!.StatusAxisZ1;
            ListStatus[11] = Mwvm!.Controller!.StatusAxisZ2;
            ListStatus[12] = Mwvm!.Controller!.StatusAxisUwLift;
            ListStatus[13] = Mwvm!.Controller!.StatusAxisUw;
            ListStatus[14] = Mwvm!.Controller!.StatusAxisRwLift;
            ListStatus[15] = Mwvm!.Controller!.StatusAxisRw;
            ListStatus[16] = Mwvm!.Controller!.StatusAxisClean;
            ListStatus[17] = Mwvm!.Controller!.StatusAxisPowerMeter;
            ListStatus[18] = Mwvm!.Controller!.StatusAxisUwSteer;
            ListStatus[19] = Mwvm!.Controller!.StatusAxisPeeling1;
            ListStatus[20] = Mwvm!.Controller!.StatusAxisStationABelt;
            ListStatus[21] = Mwvm!.Controller!.StatusAxisPeeling2;
            ListStatus[22] = Mwvm!.Controller!.StatusAxisStationBBelt;
            ListStatus[23] = Mwvm!.Controller!.StatusAxisRwSteer;
        }

        #region 属性绑定

        private MainImgViewModel mainImgViewmodel;
        public MainImgViewModel MainImgViewmodel
        {
            get
            {
                return mainImgViewmodel;
            }
            set
            {
                mainImgViewmodel = value;
                NotifyOfPropertyChange(() => MainImgViewmodel);
            }
        }

        private int? stationA = 0;
        public int? StationA
        {
            get { return stationA; }
            set
            {
                stationA = value;
                NotifyOfPropertyChange(() => StationA);
            }
        }

        private int? stationB = 0;
        public int? StationB
        {
            get { return stationB; }
            set
            {
                stationB = value;
                NotifyOfPropertyChange(() => StationB);
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

        private ObservableCollection<bool> _gantryMarkingStatus = new ObservableCollection<bool>();
        public ObservableCollection<bool> GantryMarkingStatus
        {
            get => _gantryMarkingStatus;
            set
            {
                _gantryMarkingStatus = value;
                NotifyOfPropertyChange(() => GantryMarkingStatus);
            }
        }

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

        private UserLogin? role = new UserLogin();
        public UserLogin? Role
        {
            get { return role; }
            set
            {
                role = value;
                NotifyOfPropertyChange(() => Role);
            }
        }


        private bool? markInitial = false;
        public bool? MarkInitial
        {
            get { return markInitial; }
            set
            {
                markInitial = value;
                NotifyOfPropertyChange(() => MarkInitial);
            }
        }

        private bool? manualOperationCompleteFlag = false;
        public bool? ManualOperationCompleteFlag
        {
            get { return manualOperationCompleteFlag; }
            set
            {
                manualOperationCompleteFlag = value;
                NotifyOfPropertyChange(() => ManualOperationCompleteFlag);
            }
        }

        private StInput? input;
        public StInput? Input
        {
            get => this.input;
            set
            {
                input = value;
                NotifyOfPropertyChange(() => Input);
            }
        }

        private StOutput? output;
        public StOutput? Output
        {
            get => this.output;
            set
            {
                output = value;
                NotifyOfPropertyChange(() => Output);
            }
        }

        private StAxisStatus? axisGantry1;

        public StAxisStatus? AxisGantry1
        {
            get { return axisGantry1; }
            set
            {
                axisGantry1 = value;
                NotifyOfPropertyChange(() => AxisGantry1);
            }
        }

        private StStatus? machineStatus;
        public StStatus? MachineStatus
        {
            get { return machineStatus; }
            set
            {
                machineStatus = value;
                NotifyOfPropertyChange(() => MachineStatus);
            }
        }

        private StAxisStatus? axisGantry2;
        public StAxisStatus? AxisGantry2
        {
            get { return axisGantry2; }
            set
            {
                axisGantry2 = value;
                NotifyOfPropertyChange(() => AxisGantry2);
            }
        }

        private StAxisStatus? axisCamShutter1;
        public StAxisStatus? AxisCamShutter1
        {
            get { return axisCamShutter1; }
            set
            {
                axisCamShutter1 = value;
                NotifyOfPropertyChange(() => AxisCamShutter1);
            }
        }
        private StAxisStatus? axisCamShutter2;
        public StAxisStatus? AxisCamShutter2
        {
            get { return axisCamShutter2; }
            set
            {
                axisCamShutter2 = value;
                NotifyOfPropertyChange(() => AxisCamShutter2);
            }
        }
        private StAxisStatus? axisPowerMeter;
        public StAxisStatus? AxisPowerMeter
        {
            get { return axisPowerMeter; }
            set
            {
                axisPowerMeter = value;
                NotifyOfPropertyChange(() => AxisPowerMeter);
            }
        }
        private StAxisStatus? axisAlign11;
        public StAxisStatus? AxisAlign11
        {
            get { return axisAlign11; }
            set
            {
                axisAlign11 = value;
                NotifyOfPropertyChange(() => AxisAlign11);
            }
        }
        private StAxisStatus? axisAlign12;
        public StAxisStatus? AxisAlign12
        {
            get { return axisAlign12; }
            set
            {
                axisAlign12 = value;
                NotifyOfPropertyChange(() => AxisAlign12);
            }
        }
        private StAxisStatus? axisAlign21;
        public StAxisStatus? AxisAlign21
        {
            get { return axisAlign21; }
            set
            {
                axisAlign21 = value;
                NotifyOfPropertyChange(() => AxisAlign21);
            }
        }
        private StAxisStatus? axisAlign22;
        public StAxisStatus? AxisAlign22
        {
            get { return axisAlign22; }
            set
            {
                axisAlign22 = value;
                NotifyOfPropertyChange(() => AxisAlign22);
            }
        }
        private StAxisStatus? axisZ1;
        public StAxisStatus? AxisZ1
        {
            get { return axisZ1; }
            set
            {
                axisZ1 = value;
                NotifyOfPropertyChange(() => AxisZ1);
            }
        }
        private StAxisStatus? axisZ2;
        public StAxisStatus? AxisZ2
        {
            get { return axisZ2; }
            set
            {
                axisZ2 = value;
                NotifyOfPropertyChange(() => AxisZ2);
            }
        }
        private StAxisStatus? axisPeeling1;
        public StAxisStatus? AxisPeeling1
        {
            get { return axisPeeling1; }
            set
            {
                axisPeeling1 = value;
                NotifyOfPropertyChange(() => AxisPeeling1);
            }
        }
        private StAxisStatus? axisStationABelt;
        public StAxisStatus? AxisStationABelt
        {
            get { return axisStationABelt; }
            set
            {
                axisStationABelt = value;
                NotifyOfPropertyChange(() => AxisStationABelt);
            }
        }
        private StAxisStatus? axisPeeling2;
        public StAxisStatus? AxisPeeling2
        {
            get { return axisPeeling2; }
            set
            {
                axisPeeling2 = value;
                NotifyOfPropertyChange(() => AxisPeeling2);
            }
        }
        private StAxisStatus? axisStationBBelt;
        public StAxisStatus? AxisStationBBelt
        {
            get { return axisStationBBelt; }
            set
            {
                axisStationBBelt = value;
                NotifyOfPropertyChange(() => AxisStationBBelt);
            }
        }
        private StAxisStatus? axis_UwLift;
        public StAxisStatus? Axis_UwLift
        {
            get { return axis_UwLift; }
            set
            {
                axis_UwLift = value;
                NotifyOfPropertyChange(() => Axis_UwLift);
            }
        }
        private StAxisStatus? axisUw;
        public StAxisStatus? AxisUw
        {
            get { return axisUw; }
            set
            {
                axisUw = value;
                NotifyOfPropertyChange(() => AxisUw);
            }
        }
        private StAxisStatus? axisRwLift;
        public StAxisStatus? AxisRwLift
        {
            get { return axisRwLift; }
            set
            {
                axisRwLift = value;
                NotifyOfPropertyChange(() => AxisRwLift);
            }
        }
        private StAxisStatus? axisRw;
        public StAxisStatus? AxisRw
        {
            get { return axisRw; }
            set
            {
                axisRw = value;
                NotifyOfPropertyChange(() => AxisRw);
            }
        }
        private StAxisStatus? axisClean;
        public StAxisStatus? AxisClean
        {
            get { return axisClean; }
            set
            {
                axisClean = value;
                NotifyOfPropertyChange(() => AxisClean);
            }
        }
        private StAxisStatus? axisUwSteer;
        public StAxisStatus? AxisUwSteer
        {
            get { return axisUwSteer; }
            set
            {
                axisUwSteer = value;
                NotifyOfPropertyChange(() => AxisUwSteer);
            }
        }
        private StAxisStatus? axisRwSteer;
        public StAxisStatus? AxisRwSteer
        {
            get { return axisRwSteer; }
            set
            {
                axisRwSteer = value;
                NotifyOfPropertyChange(() => AxisRwSteer);
            }
        }


        private bool choosed = false;
        public bool Choosed
        {
            get => choosed;
            set
            {
                choosed = value;
                NotifyOfPropertyChange(() => Choosed);
            }
        }

        //入口电机正反转
        private bool inputAxisFor = false;
        public bool InputAxisFor
        {
            get => inputAxisFor;
            set
            {
                inputAxisFor = value;
                NotifyOfPropertyChange(() => InputAxisFor);
            }
        }
        private bool inputAxisRev = false;
        public bool InputAxisRev
        {
            get => inputAxisRev;
            set
            {
                inputAxisRev = value;
                NotifyOfPropertyChange(() => InputAxisRev);
            }
        }

        //中间电机正反转
        private bool midAxisFor = false;
        public bool MidAxisFor
        {
            get => midAxisFor;
            set
            {
                midAxisFor = value;
                NotifyOfPropertyChange(() => MidAxisFor);
            }
        }
        private bool midAxisRev = false;
        public bool MidAxisRev
        {
            get => midAxisRev;
            set
            {
                midAxisRev = value;
                NotifyOfPropertyChange(() => MidAxisRev);
            }
        }

        //出口电机正反转
        private bool outputAxisFor = false;
        public bool OutputAxisFor
        {
            get => outputAxisFor;
            set
            {
                outputAxisFor = value;
                NotifyOfPropertyChange(() => OutputAxisFor);
            }
        }
        private bool outputAxisRev = false;
        public bool OutputAxisRev
        {
            get => outputAxisRev;
            set
            {
                outputAxisRev = value;
                NotifyOfPropertyChange(() => OutputAxisRev);
            }
        }

        private ObservableCollection<float> displacement = new ObservableCollection<float>()
        {
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0
        };
        public ObservableCollection<float> Displacement
        {
            get { return displacement; }
            set
            {
                displacement = value;
                NotifyOfPropertyChange(() => Displacement);
            }
        }

        private double[,] nums = new double[12, 3];

        public double[,] Nums
        {
            get { return nums; }
            set
            {
                nums = value;
                NotifyOfPropertyChange(() => Nums);
            }
        }

        private ObservableCollection<double> powerdatas = new ObservableCollection<double>()
        {
            0,0,0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,0,0,0,0
        };
        public ObservableCollection<double> PowerData
        {
            get { return powerdatas; }
            set
            {
                powerdatas = value;
                NotifyOfPropertyChange(() => PowerData);
            }
        }

        private ObservableCollection<StAxis_Status> listStatus = new ObservableCollection<StAxis_Status>();
        public ObservableCollection<StAxis_Status> ListStatus
        {
            get { return listStatus; }
            set
            {
                listStatus = value;
                NotifyOfPropertyChange(() => ListStatus);
            }
        }


        //界面设定的功率参数
        private double[] powerSetData = new double[12];

        public double[] PowerSetData
        {
            get { return powerSetData; }
            set
            {
                powerSetData = value;
                NotifyOfPropertyChange(() => PowerSetData);
            }
        }

        #region 视觉相关
        private bool camera1IsConnect;
        public bool Camera1IsConnect
        {
            get { return camera1IsConnect; }
            set
            {
                camera1IsConnect = value;
                NotifyOfPropertyChange(() => Camera1IsConnect);
            }
        }

        private bool camera2IsConnect;
        public bool Camera2IsConnect
        {
            get { return camera2IsConnect; }
            set
            {
                camera2IsConnect = value;
                NotifyOfPropertyChange(() => Camera2IsConnect);
            }
        }

        private string? sendMsg1 = "";
        public string? SendMsg1
        {
            get { return sendMsg1; }
            set
            {
                sendMsg1 = value;
                NotifyOfPropertyChange(() => SendMsg1);
            }
        }

        private string? sendMsg2 = "";
        public string? SendMsg2
        {
            get { return sendMsg2; }
            set
            {
                sendMsg2 = value;
                NotifyOfPropertyChange(() => SendMsg2);
            }
        }

        private string? receiveMsg1;
        public string? ReceiveMsg1
        {
            get { return receiveMsg1; }
            set
            {
                receiveMsg1 = value;
                NotifyOfPropertyChange(() => ReceiveMsg1);
            }
        }
        private string? receiveMsg2;
        public string? ReceiveMsg2
        {
            get { return receiveMsg2; }
            set
            {
                receiveMsg2 = value;
                NotifyOfPropertyChange(() => ReceiveMsg2);
            }
        }
        #endregion

        #endregion

        #region 命令实现

        //IO是输出状态灯测试
        public void OutputCheck(string name)
        {
            Mwvm.Controller?.ReadFromControllerByIndex(ControllerOutputIndex.OUT_INDEX_IO_OUTPUT);
            StOutput currentStatus = CloneHelper.DeepClone<StOutput>(Mwvm.Controller?.IoOutput);
            if (Choosed)
            {
                SetProperty(currentStatus, name, true);
                Mwvm!.Controller!.IoOutput_CMD = currentStatus;
                Mwvm!.Controller!.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_OutputIO);
            }
            else
            {
                SetProperty(currentStatus, name, false);
                Mwvm!.Controller!.IoOutput_CMD = currentStatus;
                Mwvm!.Controller!.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_OutputIO);
            }
        }

        public void AxisJog(string commandName, string reverseName, string status, string reverseStatus)
        {
            Mwvm.Controller!.ReadFromControllerByIndex(ControllerOutputIndex.OUT_INDEX_IO_OUTPUT);
            StOutput currentStatus = CloneHelper.DeepClone<StOutput>(Mwvm.Controller?.IoOutput);
            if ((bool)GetProperty(this, status))
            {
                SetProperty(this, reverseStatus, false);
                SetProperty(currentStatus, commandName, true);
                SetProperty(currentStatus, reverseName, false);
                Mwvm!.Controller!.IoOutput_CMD = currentStatus;
                Mwvm!.Controller!.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_OutputIO);
            }
            else
            {
                SetProperty(currentStatus, commandName, false);
                Mwvm!.Controller!.IoOutput_CMD = currentStatus;
                Mwvm!.Controller!.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_OutputIO);
            }
        }

        //龙门轴回零
        public void Granty1Home()
        {
            bool status1 = Mwvm!.Controller!.StatusAxisGantry11.sT_AxisStatus.Coupled;
            bool status2 = Mwvm!.Controller!.StatusAxisGantry21.sT_AxisStatus.Coupled;
            if (!status1 || !status2)
            {
                MessageBox.Show("龙门轴的耦合状态丢失，不能运动!");
                this.Mwvm.Log?.ErrorFormat("调试页面-龙门轴的耦合状态丢失，不能运动!");
                return;
            }
            var result = MessageBox.Show("确定要进行龙门轴回零吗？", "信息提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
            if (result == MessageBoxResult.No)
                return;
            StAxisCommand command = new StAxisCommand();
            command.Home = true;
            Mwvm.Controller.CmdAxisGantry11.sT_AxisCommand = command;
            Mwvm.Controller.CmdAxisGantry12.sT_AxisCommand = command;
            Mwvm.Controller.CmdAxisGantry21.sT_AxisCommand = command;
            Mwvm.Controller.CmdAxisGantry21.sT_AxisCommand = command;
            Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry11AxisControl);
            Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry12AxisControl);
            Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry21AxisControl);
            Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry22AxisControl);
        }

        //其他轴回零
        public void AxisHome(int index)
        {
            var result = MessageBox.Show("确定要进行回零动作吗?", "信息提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
            if (result == MessageBoxResult.No)
                return;
            StAxisCommand command = new StAxisCommand();
            command.Home = true;
            Mwvm!.Controller!.ListCmds[index].sT_AxisCommand = command;
            Mwvm!.Controller?.WriteToControllerByIndex((ControllerInputIndex)(index + 3));
        }
        //龙门轴去使能
        public void GrantyEnable(int index)
        {
            var result = MessageBox.Show("确定要进行使能切换吗?", "信息提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
            if (result == MessageBoxResult.No)
                return;
            switch (index)
            {
                case 1:
                    bool status = Mwvm.Controller!.StatusAxisGantry11.sT_AxisStatus.Enable;
                    if (status)
                    {
                        StAxisCommand command = new StAxisCommand();
                        command.Disable = true;
                        Mwvm.Controller.CmdAxisGantry11.sT_AxisCommand = command;
                        Mwvm.Controller.CmdAxisGantry12.sT_AxisCommand = command;
                        Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry11AxisControl);
                        Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry12AxisControl);
                    }
                    else
                    {
                        StAxisCommand command = new StAxisCommand();
                        command.Disable = false;
                        Mwvm.Controller.CmdAxisGantry11.sT_AxisCommand = command;
                        Mwvm.Controller.CmdAxisGantry12.sT_AxisCommand = command;
                        Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry11AxisControl);
                        Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry12AxisControl);
                    }
                    break;
                case 2:
                    bool statuss = Mwvm.Controller!.StatusAxisGantry21.sT_AxisStatus.Enable;
                    if (statuss)
                    {
                        StAxisCommand command = new StAxisCommand();
                        command.Disable = true;
                        Mwvm.Controller.CmdAxisGantry21.sT_AxisCommand = command;
                        Mwvm.Controller.CmdAxisGantry22.sT_AxisCommand = command;
                        Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry21AxisControl);
                        Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry22AxisControl);
                    }
                    else
                    {
                        StAxisCommand command = new StAxisCommand();
                        command.Disable = false;
                        Mwvm.Controller.CmdAxisGantry21.sT_AxisCommand = command;
                        Mwvm.Controller.CmdAxisGantry22.sT_AxisCommand = command;
                        Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry21AxisControl);
                        Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry22AxisControl);
                    }
                    break;
                default: break;
            }
        }
        //其他轴去使能
        public void SwitchEnable(int index)
        {
            var result = MessageBox.Show("确定要进行使能切换吗?", "信息提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
            if (result == MessageBoxResult.No)
                return;
            AxisStatusRefresh();
            bool status = ListStatus[index].sT_AxisStatus.Enable;
            if (status)
            {
                StAxisCommand command = new StAxisCommand();
                command.Disable = true;
                Mwvm!.Controller!.ListCmds[index].sT_AxisCommand = command;
                Mwvm!.Controller?.WriteToControllerByIndex((ControllerInputIndex)(index + 3));
            }
            else
            {
                StAxisCommand command = new StAxisCommand();
                command.Disable = false;
                Mwvm!.Controller!.ListCmds[index].sT_AxisCommand = command;
                Mwvm.Controller?.WriteToControllerByIndex((ControllerInputIndex)(index + 3));
            }
        }
        //龙门相对运动
        public void GrantyRelativeMove(int index)
        {
            bool status1 = Mwvm!.Controller!.StatusAxisGantry11.sT_AxisStatus.Coupled;
            bool status2 = Mwvm.Controller.StatusAxisGantry21.sT_AxisStatus.Coupled;
            if (!status1 || !status2)
            {
                MessageBox.Show("龙门轴的耦合状态丢失，不能运动!");
                this.Mwvm.Log?.ErrorFormat("调试页面-龙门轴的耦合状态丢失，不能运动!");
                return;
            }
            float value = Displacement[index];
            float safeDistance = Mwvm.Controller.StatusAxisGantry11.sT_AxisStatus.SafetyDistance;
            float granty1CurrentMpos = Mwvm.Controller.StatusAxisGantry11.sT_AxisStatus.ActPos;
            float granty2CurrentMpos = Mwvm.Controller.StatusAxisGantry21.sT_AxisStatus.ActPos;
            switch (index)
            {
                case 1:
                     if (granty2CurrentMpos + safeDistance  > granty1CurrentMpos + value)
                       // if (granty1CurrentMpos + safeDistance + value > granty2CurrentMpos)
                    {
                        MessageBox.Show("龙门1运行距离超过安全距离，有碰撞风险，禁止运动!");
                        this.Mwvm.Log?.ErrorFormat("调试页面-龙门1运行距离超过安全距离，有碰撞风险，禁止运动!");
                        return;
                    }
                    var result = MessageBox.Show($"确定要龙门1相对移动{value}的距离吗？", "信息提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
                    if (result == MessageBoxResult.No)
                        return;
                    Mwvm.Controller.ParaAxisGantry11.sT_AxisParameter.RelDistance = value;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry11AxisControl);
                    StAxisCommand command = new StAxisCommand();
                    command.MoveRel = true;
                    Mwvm.Controller!.CmdAxisGantry11.sT_AxisCommand = command;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry11AxisControl);
                    break;
                case 2:
                    if (granty2CurrentMpos + value + safeDistance> granty1CurrentMpos)
                    //   if (granty1CurrentMpos + safeDistance > granty2CurrentMpos + value)
                    {
                        MessageBox.Show("龙门2运行距离超过安全距离，有碰撞风险，禁止运动!");
                        this.Mwvm.Log?.ErrorFormat("调试页面-龙门2运行距离超过安全距离，有碰撞风险，禁止运动!");
                        return;
                    }
                    var results = MessageBox.Show($"确定要龙门2相对移动{value}的距离吗？", "信息提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
                    if (results == MessageBoxResult.No)
                        return;
                    Mwvm!.Controller!.ParaAxisGantry21.sT_AxisParameter.RelDistance = value;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry21AxisControl);
                    StAxisCommand commands = new StAxisCommand();
                    commands.MoveRel = true;
                    Mwvm!.Controller!.CmdAxisGantry21.sT_AxisCommand = commands;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry21AxisControl);
                    break;
                default: break;
            }
        }

        private int? longmen1Index = 0;
        public int? Longmen1Index
        {
            get { return longmen1Index; }
            set
            {
                longmen1Index = value;
                NotifyOfPropertyChange(() => Longmen1Index);
            }
        }

        private int? longmen2Index = 0;
        public int? Longmen2Index
        {
            get { return longmen2Index; }
            set
            {
                longmen2Index = value;
                NotifyOfPropertyChange(() => Longmen2Index);
            }
        }

        private ObservableCollection<int> itemsIndex = new ObservableCollection<int>();
        public ObservableCollection<int> ItemsIndex
        {
            get { return itemsIndex; }
            set
            {
                itemsIndex = value;
                NotifyOfPropertyChange(() => ItemsIndex);
            }
        }

        //龙门按目标运动
        public void GrantyMoveByTarget(int index)
        {
            int? target = 0;
            float value = 0;
            if (index == 1)
            {
                target = Longmen1Index + 1;
                switch (target)
                {
                    case 1:
                        value = Mwvm!.Controller!.CmdParam.Gantry1StationAMark1Pos;
                        break;
                    case 2:
                        value = Mwvm!.Controller!.CmdParam.Gantry1StationAMark2Pos;
                        break;
                    case 3:
                        value = Mwvm!.Controller!.CmdParam.Gantry1StationAMark3Pos;
                        break;
                    case 4:
                        value = Mwvm!.Controller!.CmdParam.Gantry1StationAMark4Pos;
                        break;
                    case 5:
                        value = Mwvm!.Controller!.CmdParam.Gantry1StationAMark5Pos;
                        break;
                    case 6:
                        value = Mwvm!.Controller!.CmdParam.Gantry1StationAMark6Pos;
                        break;

                    default:
                        break;
                }

            }
            else if (index == 2)
            {
                target = Longmen2Index + 1;
                switch (target)
                {
                    case 1:
                        value = Mwvm!.Controller!.CmdParam.Gantry2StationAMark1Pos;
                        break;
                    case 2:
                        value = Mwvm!.Controller!.CmdParam.Gantry2StationAMark2Pos;
                        break;
                    case 3:
                        value = Mwvm!.Controller!.CmdParam.Gantry2StationAMark3Pos;
                        break;
                    case 4:
                        value = Mwvm!.Controller!.CmdParam.Gantry2StationAMark4Pos;
                        break;
                    case 5:
                        value = Mwvm!.Controller!.CmdParam.Gantry2StationAMark5Pos;
                        break;
                    case 6:
                        value = Mwvm!.Controller!.CmdParam.Gantry2StationAMark6Pos;
                        break;

                    default:
                        break;
                }
            }
            bool status1 = Mwvm!.Controller!.StatusAxisGantry11.sT_AxisStatus.Coupled;
            bool status2 = Mwvm!.Controller!.StatusAxisGantry21.sT_AxisStatus.Coupled;
            if (!status1 || !status2)
            {
                MessageBox.Show("龙门轴的耦合状态丢失，不能运动!");
                this.Mwvm.Log?.ErrorFormat("调试页面-龙门轴的耦合状态丢失，不能运动!");
                return;
            }
            //float value = Displacement[index];
            float safeDistance = Mwvm.Controller.StatusAxisGantry11.sT_AxisStatus.SafetyDistance;
            float granty1CurrentMpos = Mwvm.Controller.StatusAxisGantry11.sT_AxisStatus.ActPos;
            float granty2CurrentMpos = Mwvm.Controller.StatusAxisGantry21.sT_AxisStatus.ActPos;
            switch (index)
            {
                case 1:
                    if (safeDistance + value > granty2CurrentMpos)
                    {
                        MessageBox.Show("龙门1运行距离超过安全距离，有碰撞风险，禁止运动!");
                        this.Mwvm.Log?.ErrorFormat("调试页面-龙门1运行距离超过安全距离，有碰撞风险，禁止运动!");
                        return;
                    }
                    var result = MessageBox.Show($"确定要龙门1运动到{value}的距离吗？", "信息提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
                    if (result == MessageBoxResult.No)
                        return;
                    Mwvm!.Controller!.ParaAxisGantry11.sT_AxisParameter.AbsPosition1 = value;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry11AxisControl);
                    StAxisCommand command = new StAxisCommand();
                    command.MoveAbs1 = true;
                    Mwvm!.Controller!.CmdAxisGantry11.sT_AxisCommand = command;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry11AxisControl);
                    break;
                case 2:
                    if (granty1CurrentMpos + safeDistance > value)
                    {
                        MessageBox.Show("龙门2运行距离超过安全距离，有碰撞风险，禁止运动!");
                        this.Mwvm.Log?.ErrorFormat("调试页面-龙门2运行距离超过安全距离，有碰撞风险，禁止运动!");
                        return;
                    }
                    var results = MessageBox.Show($"确定要龙门2运动到{value}的距离吗？", "信息提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
                    if (results == MessageBoxResult.No)
                        return;
                    Mwvm.Controller.ParaAxisGantry21.sT_AxisParameter.AbsPosition1 = value;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry21AxisControl);
                    StAxisCommand commands = new StAxisCommand();
                    commands.MoveAbs1 = true;
                    Mwvm!.Controller!.CmdAxisGantry21.sT_AxisCommand = commands;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry21AxisControl);
                    break;
                default: break;
            }
        }



        public void AddMarkGraph(int i)
        {
            _mainImgViewMode!.AddMarkGraph(i);
        }

        public void EditMarkGraph(int i)
        {
            _mainImgViewMode!.EditMarkGraph(i);
        }

        public void Marking(int i)
        {
            _mainImgViewMode!.Marking(i);
        }
        public void LaserOutputCheck(int i)
        {
            _mainImgViewMode!.LaserOutputCheck(i);
        }

        public void ManualLoadInProcessByIndex(int index)
        {
            Mwvm.Controller?.SendManualContoller(index);        
        }


        //龙门绝对运动
        public void GrantyMoveAbs(int index)
        {
            bool status1 = Mwvm!.Controller!.StatusAxisGantry11.sT_AxisStatus.Coupled;
            bool status2 = Mwvm!.Controller!.StatusAxisGantry21.sT_AxisStatus.Coupled;
            if (!status1 || !status2)
            {
                MessageBox.Show("龙门轴的耦合状态丢失，不能运动!");
                this.Mwvm.Log?.ErrorFormat("调试页面-龙门轴的耦合状态丢失，不能运动!");
                return;
            }
            float value = Displacement[index];
            float safeDistance = Mwvm.Controller.StatusAxisGantry11.sT_AxisStatus.SafetyDistance;
            float granty1CurrentMpos = Mwvm.Controller.StatusAxisGantry11.sT_AxisStatus.ActPos;
            float granty2CurrentMpos = Mwvm.Controller.StatusAxisGantry21.sT_AxisStatus.ActPos;
            switch (index)
            {
                case 1:
                    if ( value < granty2CurrentMpos+ safeDistance)
                    {
                        MessageBox.Show("龙门1运行距离超过安全距离，有碰撞风险，禁止运动!");
                        this.Mwvm.Log?.ErrorFormat("调试页面-龙门1运行距离超过安全距离，有碰撞风险，禁止运动!");
                        return;
                    }
                    var result = MessageBox.Show($"确定要龙门1运动到{value}的距离吗？", "信息提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
                    if (result == MessageBoxResult.No)
                        return;
                    Mwvm!.Controller!.ParaAxisGantry11.sT_AxisParameter.AbsPosition1 = value;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry11AxisControl);
                    StAxisCommand command = new StAxisCommand();
                    command.MoveAbs1 = true;
                    Mwvm!.Controller!.CmdAxisGantry11.sT_AxisCommand = command;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry11AxisControl);
                    break;
                case 2:
                    if (granty1CurrentMpos  < value+ safeDistance)
                    {
                        MessageBox.Show("龙门2运行距离超过安全距离，有碰撞风险，禁止运动!");
                        this.Mwvm.Log?.ErrorFormat("调试页面-龙门2运行距离超过安全距离，有碰撞风险，禁止运动!");
                        return;
                    }
                    var results = MessageBox.Show($"确定要龙门2运动到{value}的距离吗？", "信息提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
                    if (results == MessageBoxResult.No)
                        return;
                    Mwvm.Controller.ParaAxisGantry21.sT_AxisParameter.AbsPosition1 = value;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_Para_AXIS_Gantry21AxisControl);
                    StAxisCommand commands = new StAxisCommand();
                    commands.MoveAbs1 = true;
                    Mwvm!.Controller!.CmdAxisGantry21.sT_AxisCommand = commands;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_Gantry21AxisControl);
                    break;
                default: break;
            }
        }
        //其他轴的相对运动
        public void RelativeMove(int index)
        {
            float value = Displacement[index];
            var result = MessageBox.Show($"确定要相对移动{value}的距离吗？", "信息提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
            if (result == MessageBoxResult.No)
                return;
            AxisParaListInitial();
            ListPars[index].sT_AxisParameter.RelDistance = value;
            Mwvm.Controller?.WriteToControllerByIndex((ControllerInputIndex)(index + 27));
            StAxisCommand command = new StAxisCommand();
            command.MoveRel = true;
            Mwvm!.Controller!.ListCmds[index].sT_AxisCommand = command;
            Mwvm.Controller?.WriteToControllerByIndex((ControllerInputIndex)(index + 3));
        }
        public void MoveAbs(int index)
        {
            float value = Displacement[index];
            var result = MessageBox.Show($"确定要移动到{value}的位置吗?", "信息提示", MessageBoxButton.YesNo, MessageBoxImage.Asterisk);
            if (result == MessageBoxResult.No)
                return;
            AxisParaListInitial();
            ListPars[index].sT_AxisParameter.AbsPosition1 = value;
            Mwvm.Controller?.WriteToControllerByIndex((ControllerInputIndex)(index + 27));
            StAxisCommand command = new StAxisCommand();
            command.MoveAbs1 = true;
            Mwvm!.Controller!.ListCmds[index].sT_AxisCommand = command;
            Mwvm.Controller?.WriteToControllerByIndex((ControllerInputIndex)(index + 3));
        }
        //收放卷/清洗轴点动
        public void JogFwStart(int index)
        {
            switch (index)
            {
                case 1:
                    Mwvm!.Controller!.CmdAxisUw.sT_AxisCommand.JogBw = false;
                    Mwvm!.Controller!.CmdAxisUw.sT_AxisCommand.JogFw = true;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_UwAxisControl);
                    break;
                case 2:
                    Mwvm!.Controller!.CmdAxisRw.sT_AxisCommand.JogBw = false;
                    Mwvm!.Controller!.CmdAxisRw.sT_AxisCommand.JogFw = true;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_RwAxisControl);
                    break;
                case 3:
                    Mwvm!.Controller!.CmdAxisClean.sT_AxisCommand.JogBw = false;
                    Mwvm!.Controller!.CmdAxisClean.sT_AxisCommand.JogFw = true;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_CleanAxisControl);
                    break;
                default: break;
            }
        }
        public void JogFwStop(int index)
        {
            switch (index)
            {
                case 1:
                    Mwvm!.Controller!.CmdAxisUw.sT_AxisCommand.JogBw = false;
                    Mwvm!.Controller!.CmdAxisUw.sT_AxisCommand.JogFw = false;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_UwAxisControl);
                    break;
                case 2:
                    Mwvm!.Controller!.CmdAxisRw.sT_AxisCommand.JogBw = false;
                    Mwvm!.Controller!.CmdAxisRw.sT_AxisCommand.JogFw = false;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_RwAxisControl);
                    break;
                case 3:
                    Mwvm!.Controller!.CmdAxisClean.sT_AxisCommand.JogBw = false;
                    Mwvm!.Controller!.CmdAxisClean.sT_AxisCommand.JogFw = false;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_CleanAxisControl);
                    break;
                default: break;
            }
        }
        public void JogBwStart(int index)
        {
            switch (index)
            {
                case 1:
                    Mwvm!.Controller!.CmdAxisUw.sT_AxisCommand.JogBw = true;
                    Mwvm!.Controller!.CmdAxisUw.sT_AxisCommand.JogFw = false;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_UwAxisControl);
                    break;
                case 2:
                    Mwvm!.Controller!.CmdAxisRw.sT_AxisCommand.JogBw = true;
                    Mwvm!.Controller!.CmdAxisRw.sT_AxisCommand.JogFw = false;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_RwAxisControl);
                    break;
                case 3:
                    Mwvm!.Controller!.CmdAxisClean.sT_AxisCommand.JogBw = true;
                    Mwvm!.Controller!.CmdAxisClean.sT_AxisCommand.JogFw = false;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_CleanAxisControl);
                    break;
                default: break;
            }
        }
        public void JogBwStop(int index)
        {
            switch (index)
            {
                case 1:
                    Mwvm!.Controller!.CmdAxisUw.sT_AxisCommand.JogBw = false;
                    Mwvm!.Controller!.CmdAxisUw.sT_AxisCommand.JogFw = false;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_UwAxisControl);
                    break;
                case 2:
                    Mwvm!.Controller!.CmdAxisRw.sT_AxisCommand.JogBw = false;
                    Mwvm!.Controller!.CmdAxisRw.sT_AxisCommand.JogFw = false;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_RwAxisControl);
                    break;
                case 3:
                    Mwvm!.Controller!.CmdAxisClean.sT_AxisCommand.JogBw = false;
                    Mwvm!.Controller!.CmdAxisClean.sT_AxisCommand.JogFw = false;
                    Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD_AXIS_CleanAxisControl);
                    break;
                default: break;
            }
        }
        //功率计发送指令
        public void DisplacementCommand(short index)
        {
            bool ganty1Homed = Mwvm!.Controller!.StatusAxisGantry11.sT_AxisStatus.Homed;
            bool ganty2Homed = Mwvm!.Controller!.StatusAxisGantry21.sT_AxisStatus.Homed;
            bool status1 = Mwvm.Controller.StatusAxisGantry11.sT_AxisStatus.Coupled;
            bool status2 = Mwvm.Controller.StatusAxisGantry21.sT_AxisStatus.Coupled;
            bool run = Mwvm.Controller.SysStatus.Running;
            bool alarm = Mwvm.Controller.SysStatus.AutoAlarm;
            if (alarm)
            {
                MessageBox.Show("设备处于报错状态，不能测试!");
                this.Mwvm.Log?.ErrorFormat("调试页面-设备处于报错状态，不能测试!");
                return;
            }
            else if (!ganty1Homed || !ganty2Homed)
            {
                MessageBox.Show("测试功率前龙门必须先回零!");
                this.Mwvm.Log?.ErrorFormat("调试页面-测试功率前龙门必须先回零!");
                return;
            }
            else if (!status1 || !status2)
            {
                MessageBox.Show("测试功率前龙门必须先处于耦合状态!");
                this.Mwvm.Log?.ErrorFormat("调试页面-测试功率前龙门必须先处于耦合状态!");
                return;
            }
            else if (run)
            {
                MessageBox.Show("自动运行期间不允许进行功率测试!");
                this.Mwvm.Log?.ErrorFormat("调试页面-自动运行期间不允许进行功率测试!");
                return;
            }
            else
            {
                Mwvm.Controller?.ReadFromControllerByIndex(ControllerOutputIndex.OUT_INDEX_HMIDATA_FROMPLC);
                HmiToPlc command = CloneHelper.DeepClone<HmiToPlc>(Mwvm.Controller?.HMIDataFromPLC);
                command.iPowerTestUnitNum = index;
                CurrentTestUnitNum = index;
                Mwvm!.Controller!.HMIDataToPLC = command;
                Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_HMIData_TOPLC);
                TestFlow = PowerTestFlow.StartTest;
            }
            //SysStatus
        }

        //功率计停止测试
        public void StopTest()
        {
            //关闭所有的激光器
            Mwvm!.Controller!.HMIDataToPLC.iPowerTestUnitNum = 0;
            Mwvm.Controller.CmdCommand.Stop = true;
            Mwvm.Controller?.WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_CMD);
            TestFlow = PowerTestFlow.Standby;
        }

        public void CameraReConnect(int i)
        {
            if (Mwvm!.Engine!.VisionProduction[i].IsConnected)
                return;
            else
                Mwvm.Engine.VisionProduction[i].Reconnect();
        }

        public void CameraShoot(int i)
        {

        }
        public void CameraSendMsg(int i)
        {
            if (i == 0)
            {
                if (!Mwvm!.Engine.VisionProduction[0].IsConnected)
                {
                    MessageBox.Show("请先连接相机!", "信息提示", MessageBoxButton.YesNo);
                    return;
                }
                if (SendMsg1 == "")
                {
                    MessageBox.Show("发送的数据不能为空", "信息提示", MessageBoxButton.YesNo);
                    return;
                }
                SendCommand sendCameraCommand = new SendCommand();
                //sendCameraCommand.GroupId = longMenNum.ToString();
                sendCameraCommand.GroupId = Mwvm!.Engine.Config.GroupId;
                sendCameraCommand.WorkStationId = 0;
                sendCameraCommand.Hash = Guid.NewGuid().ToString();
                sendCameraCommand.Command = SendMsg1;
                Mwvm.Engine.VisionProduction[0].CommandAction(VisionProduction_Command.COMMAND_VISIONREQUEST, sendCameraCommand);
            }
            else if (i == 1)
            {
                if (!Mwvm!.Engine.VisionProduction[1].IsConnected)
                {
                    MessageBox.Show("请先连接相机!", "信息提示", MessageBoxButton.YesNo);
                    return;
                }
                if (SendMsg2 == "")
                {
                    MessageBox.Show("发送的数据不能为空", "信息提示", MessageBoxButton.YesNo);
                    return;
                }
                SendCommand sendCameraCommand = new SendCommand();
                sendCameraCommand.GroupId = Mwvm!.Engine.Config.GroupId;
                sendCameraCommand.WorkStationId = 1;
                sendCameraCommand.Hash = Guid.NewGuid().ToString();
                sendCameraCommand.Command = SendMsg2;
                Mwvm.Engine.VisionProduction[1].CommandAction(VisionProduction_Command.COMMAND_VISIONREQUEST, sendCameraCommand);
            }
        }

        public void CleatReceiveString(int index)
        {
            switch (index)
            {
                case 0:
                    {
                        if (ReceiveMsg1 == "")
                            return;
                        ReceiveMsg1 = string.Empty;
                        SendMsg1 = string.Empty;
                        str1 = "";
                        msg1 = "";
                        Mwvm!.Engine!.VisionProduction[0].cameraDataList.Clear();
                    }
                    break;
                case 1:
                    {
                        if (ReceiveMsg2 == "")
                            return;
                        SendMsg2 = string.Empty;
                        ReceiveMsg2 = string.Empty;
                        str2 = "";
                        msg2 = "";
                        Mwvm!.Engine!.VisionProduction[1].cameraDataList.Clear();
                    }
                    break;
            }
        }
        #endregion

        public bool SetField(object obj, string name, object value)
        {
            bool flag = false;
            var fields = obj.GetType().GetFields();
            try
            {
                foreach (var field in fields)
                {
                    if (field.Name == name)
                    {
                        field.SetValue(obj, value);
                        flag = true;
                        break;
                    }
                }
                return flag;
            }
            catch
            {
                return false;
            }
        }

        public bool SetProperty(object obj, string name, object value)
        {
            bool flag = false;
            var properties = obj.GetType().GetProperties();
            try
            {
                foreach (var property in properties)
                {
                    if (property.Name == name)
                    {
                        property.SetValue(obj, value);
                        flag = true;
                        break;
                    }
                }
                return flag;
            }
            catch
            {
                return false;
            }
        }

        public object GetField(object obj, string name)
        {
            object result = null;
            var fields = obj.GetType().GetFields();
            try
            {
                foreach (var field in fields)
                {
                    if (field.Name == name)
                    {
                        result = field.GetValue(obj!);
                        break;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        public object GetProperty(object obj, string name)
        {
            object result = null;
            var properties = obj.GetType().GetProperties();
            try
            {
                foreach (var property in properties)
                {
                    if (property.Name == name)
                    {
                        result = property.GetValue(obj);
                        break;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

    }
}