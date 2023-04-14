using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using DRsoft.Runtime.Core.Platform.Events;
using System.Collections.ObjectModel;
using Engine.Transfer;
using System.Windows.Threading;
using System.Windows.Media;
using DRsoft.Engine.Model.Controller;
using Engine.Models;
using System.Threading;

namespace Engine.ViewModels.MainPageComponent
{
    public class MainLayoutViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public MainPageViewModel Mpvm;
        public MainWindowViewModel Mwvm;
        public DispatcherTimer MainTimer;
        public Task Test;
        int times = 0;
        TestEnum testFlow = TestEnum.StandBy;
        int GrantyA = 0;
        int GrantyB = 0;
        bool WorkB = false;
        bool initial = false;
        private CancellationTokenSource mainStop = new CancellationTokenSource();
        public MainLayoutViewModel(IWindowManager windowManager, MainWindowViewModel mvm, MainPageViewModel mpm)
        {
            this.WindowManager = windowManager;
            this.Mpvm = mpm;
            this.Mwvm = mvm;
            MainTimer = new DispatcherTimer();
            MainTimer.Interval = TimeSpan.FromMilliseconds(800);
            MainTimer.Tick += new EventHandler(Refresh);
            MainTimer.Start();
            Test = new Task(RefreshTest);
            //Test.Start();

            Input = new StInput();

            ItemsString.Add("从起始到A台面");
            ItemsString.Add("从起始到B台面");
            ItemsString.Add("从A台面到B台面");
            ItemsString.Add("从A台面出料");
            ItemsString.Add("从B台面出料");
        }

        public void Refresh(object? obj, EventArgs e)
        {

            Input = Mwvm.Controller?.IoInput;
            if (FinishA)
            {
                CompleteA = false;
                IsEnableA = Visibility.Hidden;
                MesaBackgroundA = Brushes.LightBlue;
            }
            if (FinishB)
            {
                CompleteB = false;
                IsEnableB = Visibility.Hidden;
                MesaBackgroundB = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAFF3EE"));
            }

            if (GrantyABusy)
                StateA = "Busy";
            else
                StateA = "Idle";

            if (GrantyBBusy)
                StateB = "Busy";
            else
                StateB = "Idle";

            IdA = (int)Mwvm!.Controller!.SysStatus.Pictrue_Gantry2Pos;
            IdB = (int)Mwvm!.Controller!.SysStatus.Pictrue_Gantry1Pos;
        }

        public void RefreshTest()
        {



            //while (true)
            //{
            //    if (mainStop.Token.IsCancellationRequested)
            //        break;
            //    switch (testFlow)
            //    {
            //        case TestEnum.StandBy:
            //            testFlow = TestEnum.ChargeIn;
            //            break;
            //        case TestEnum.ChargeIn:
            //            IsEnableA = Visibility.Visible;
            //            IsMoveToA1 = true;
            //            Thread.Sleep(4000);
            //            GrantyA = 10;
            //            GrantyB = 14;
            //            if (!IsBusyA)
            //            {
            //                IsMoveToB2 = true;
            //                IsEnableB = Visibility.Visible;
            //                testFlow = TestEnum.Moving;
            //            }
            //            break;
            //        case TestEnum.Moving:
            //            IdA = GrantyA;
            //            IdB = GrantyB;
            //            Thread.Sleep(300);
            //            if (!GrantyABusy && !GrantyBBusy)
            //            {
            //                testFlow = TestEnum.Marking;
            //            }
            //            break;
            //        case TestEnum.Marking:
            //            ShootA = true;
            //            ShootB = true;
            //            Thread.Sleep(2000);
            //            if (!WorkB)
            //                testFlow = TestEnum.LaserOff;
            //            else
            //            {
            //                ShootA = false;
            //                ShootB = false;
            //                CompleteA = true;
            //                MesaBackgroundA = Brushes.LightPink;
            //                testFlow = TestEnum.MoveWorkB;
            //            }
            //            break;
            //        case TestEnum.LaserOff:
            //            ShootA = false;
            //            ShootB = false;
            //            Thread.Sleep(500);
            //            GrantyA = 11;
            //            GrantyB = 13;
            //            testFlow = TestEnum.Moving;
            //            WorkB = true;
            //            break;
            //        case TestEnum.MoveWorkB:
            //            GrantyA = 3;
            //            GrantyB = 5;
            //            IdA = GrantyA;
            //            IdB = GrantyB;
            //            Thread.Sleep(500);
            //            if (!GrantyABusy && !GrantyBBusy)
            //            {
            //                testFlow = TestEnum.MarkingB;
            //            }
            //            break;
            //        case TestEnum.MarkingB:
            //            ShootA = true;
            //            ShootB = true;
            //            Thread.Sleep(1000);
            //            testFlow = TestEnum.LaseOffB;
            //            break;
            //        case TestEnum.LaseOffB:
            //            ShootA = false;
            //            ShootB = false;
            //            testFlow = TestEnum.ChargeOut;
            //            break;
            //        case TestEnum.ChargeOut:
            //            CompleteB = true;
            //            MesaBackgroundB = Brushes.LightPink;
            //            if (finishB)
            //                testFlow = TestEnum.End;
            //            break;
            //        case TestEnum.End:
            //            testFlow = TestEnum.StandBy;
            //            mainStop.Cancel();
            //            initial = true;

            //            break;
            //    }
            //}

        }


        #region 属性绑定

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



        #endregion

        private int idA = 0;
        public int IdA
        {
            get { return idA; }
            set
            {
                idA = value;
                NotifyOfPropertyChange(() => IdA);
            }
        }

        private int idB = 0;
        public int IdB
        {
            get { return idB; }
            set
            {
                idB = value;
                NotifyOfPropertyChange(() => IdB);
            }
        }

        private bool mark = false;
        public bool Mark
        {
            get { return mark; }
            set
            {
                mark = value;
                NotifyOfPropertyChange(() => Mark);
            }
        }

        private bool isOkA = false;
        public bool IsOkA
        {
            get { return isOkA; }
            set
            {
                isOkA = value;
                NotifyOfPropertyChange(() => IsOkA);
            }
        }

        private bool isOkB = false;
        public bool IsOkB
        {
            get { return isOkB; }
            set
            {
                isOkB = value;
                NotifyOfPropertyChange(() => IsOkB);
            }
        }

        private bool isActiveA = false;
        public bool IsActiveA
        {
            get { return isActiveA; }
            set
            {
                isActiveA = value;
                NotifyOfPropertyChange(() => IsActiveA);
            }
        }

        private bool isActiveB = false;
        public bool IsActiveB
        {
            get { return isActiveB; }
            set
            {
                isActiveB = value;
                NotifyOfPropertyChange(() => IsActiveB);
            }
        }

        private bool start = false;
        public bool Start
        {
            get { return start; }
            set
            {
                start = value;
                NotifyOfPropertyChange(() => Start);
            }
        }

        private bool choosed;
        public bool Choosed
        {
            get { return choosed; }
            set
            {
                choosed = value;
                NotifyOfPropertyChange(() => Choosed);
            }
        }

        private bool isErrorA = false;
        public bool IsErrorA
        {
            get { return isErrorA; }
            set
            {
                isErrorA = value;
                NotifyOfPropertyChange(() => isErrorA);
            }
        }
        private bool isErrorB = false;
        public bool IsErrorB
        {
            get { return isErrorB; }
            set
            {
                isErrorB = value;
                NotifyOfPropertyChange(() => IsErrorB);
            }
        }

        private Visibility isEnableA = Visibility.Hidden;
        public Visibility IsEnableA
        {
            get { return isEnableA; }
            set
            {
                isEnableA = value;
                NotifyOfPropertyChange(() => IsEnableA);
            }
        }

        private Visibility isEnableB = Visibility.Hidden;
        public Visibility IsEnableB
        {
            get { return isEnableB; }
            set
            {
                isEnableB = value;
                NotifyOfPropertyChange(() => IsEnableB);
            }
        }

        private bool isMoveToA1 = false;
        public bool IsMoveToA1
        {
            get { return isMoveToA1; }
            set
            {
                isMoveToA1 = value;
                NotifyOfPropertyChange(() => IsMoveToA1);
            }
        }

        private bool isMoveToA2 = false;
        public bool IsMoveToA2
        {
            get { return isMoveToA2; }
            set
            {
                isMoveToA2 = value;
                NotifyOfPropertyChange(() => IsMoveToA2);
            }
        }

        private bool isMoveToB1 = false;
        public bool IsMoveToB1
        {
            get { return isMoveToB1; }
            set
            {
                isMoveToB1 = value;
                NotifyOfPropertyChange(() => IsMoveToB1);
            }
        }

        private bool isMoveToB2 = false;
        public bool IsMoveToB2
        {
            get { return isMoveToB2; }
            set
            {
                isMoveToB2 = value;
                NotifyOfPropertyChange(() => IsMoveToB2);
            }
        }

        private bool isAToB1 = false;
        public bool IsAToB1
        {
            get { return isAToB1; }
            set
            {
                isAToB1 = value;
                NotifyOfPropertyChange(() => IsAToB1);
            }
        }

        private bool isAToB2 = false;
        public bool IsAToB2
        {
            get { return isAToB2; }
            set
            {
                isAToB2 = value;
                NotifyOfPropertyChange(() => IsAToB2);
            }
        }

        private bool isBToE1 = false;
        public bool IsBToE1
        {
            get { return isBToE1; }
            set
            {
                isBToE1 = value;
                NotifyOfPropertyChange(() => IsBToE1);
            }
        }

        private bool isBToE2 = false;
        public bool IsBToE2
        {
            get { return isBToE2; }
            set
            {
                isBToE2 = value;
                NotifyOfPropertyChange(() => IsBToE2);
            }
        }

        private bool isBusyA = false;
        public bool IsBusyA
        {
            get { return isBusyA; }
            set
            {
                isBusyA = value;
                NotifyOfPropertyChange(() => IsBusyA);
            }
        }

        private bool grantyABusy = false;
        public bool GrantyABusy
        {
            get { return grantyABusy; }
            set
            {
                grantyABusy = value;
                NotifyOfPropertyChange(() => GrantyABusy);
            }
        }

        private bool grantyBBusy = false;
        public bool GrantyBBusy
        {
            get { return grantyBBusy; }
            set
            {
                grantyBBusy = value;
                NotifyOfPropertyChange(() => GrantyBBusy);
            }
        }

        private bool isBusyB = false;
        public bool IsBusyB
        {
            get { return isBusyB; }
            set
            {
                isBusyB = value;
                NotifyOfPropertyChange(() => IsBusyB);
            }
        }


        private bool completeA = false;
        public bool CompleteA
        {
            get { return completeA; }
            set
            {
                completeA = value;
                NotifyOfPropertyChange(() => CompleteA);
            }
        }
        private bool completeB = false;
        public bool CompleteB
        {
            get { return completeB; }
            set
            {
                completeB = value;
                NotifyOfPropertyChange(() => CompleteB);
            }
        }


        private bool finishA = false;
        public bool FinishA
        {
            get { return finishA; }
            set
            {
                finishA = value;
                NotifyOfPropertyChange(() => FinishA);
            }
        }

        private bool finishB = false;
        public bool FinishB
        {
            get { return finishB; }
            set
            {
                finishB = value;
                NotifyOfPropertyChange(() => FinishB);
            }
        }

        private string errorMsgA = "龙门出现异常";
        public string ErrorMsgA
        {
            get { return errorMsgA; }
            set
            {
                errorMsgA = value;
                NotifyOfPropertyChange(() => ErrorMsgA);
            }
        }
        private string errorMsgB = "龙门出现异常";
        public string ErrorMsgB
        {
            get { return errorMsgB; }
            set
            {
                errorMsgB = value;
                NotifyOfPropertyChange(() => ErrorMsgB);
            }
        }

        private bool chooseA;
        public bool ChooseA
        {
            get { return chooseA; }
            set
            {
                chooseA = value;
                NotifyOfPropertyChange(() => ChooseA);
            }
        }

        private bool chooseB;
        public bool ChooseB
        {
            get { return chooseB; }
            set
            {
                chooseB = value;
                NotifyOfPropertyChange(() => ChooseB);
            }
        }

        private bool shootA = false;
        public bool ShootA
        {
            get { return shootA; }
            set
            {
                shootA = value;
                NotifyOfPropertyChange(() => ShootA);
            }
        }
        private bool shootB = false;
        public bool ShootB
        {
            get { return shootB; }
            set
            {
                shootB = value;
                NotifyOfPropertyChange(() => ShootB);
            }
        }

        private ObservableCollection<string> itemsString = new ObservableCollection<string>();
        public ObservableCollection<string> ItemsString
        {
            get { return itemsString; }
            set
            {
                itemsString = value;
                NotifyOfPropertyChange(() => ItemsString);
            }
        }

        private int? longmenAIndex = 0;
        public int? LongmenAIndex
        {
            get { return longmenAIndex; }
            set
            {
                longmenAIndex = value;
                NotifyOfPropertyChange(() => LongmenAIndex);
            }
        }

        private int? longmenBIndex = 0;
        public int? LongmenBIndex
        {
            get { return longmenBIndex; }
            set
            {
                longmenBIndex = value;
                NotifyOfPropertyChange(() => LongmenBIndex);
            }
        }


        private string stateA= "Idle";
        public string StateA
        {
            get { return stateA; }
            set
            {
                stateA = value;
                NotifyOfPropertyChange(() => stateA);
            }
        }
        private string stateB = "Idle";
        public string StateB
        {
            get { return stateB; }
            set
            {
                stateB = value;
                NotifyOfPropertyChange(() => StateB);
            }
        }

        private Brush mesaBackgroundA = Brushes.LightBlue;
        public Brush MesaBackgroundA
        {
            get { return mesaBackgroundA; }
            set
            {
                mesaBackgroundA = value;
                NotifyOfPropertyChange(() => MesaBackgroundA);
            }
        }

        private Brush mesaBackgroundB = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFAFF3EE"));
        public Brush MesaBackgroundB
        {
            get { return mesaBackgroundB; }
            set
            {
                mesaBackgroundB = value;
                NotifyOfPropertyChange(() => MesaBackgroundB);
            }
        }

        public void JogFor(int index)
        {
            switch(index)
            {
                case 1:
                    IdA++;
                    break;
                case 2:
                    IdB++;
                    break;
            }
        }
        public void JogRev(int index)
        {
            switch (index)
            {
                case 1:
                    if(IdA>1) IdA--;
                    break;
                case 2:
                    if (IdB > 1) IdB--;
                    break;
            }
        }

        public void LaserOutputCheck(int index)
        {
            switch (index)
            {
                case 1:
                    if (ChooseA)
                        ShootA = true;
                    else
                        ShootA = false;
                    break;
                case 2:
                    if (ChooseB)
                        ShootB = true;
                    else
                        ShootB = false;
                    break;
            }
        }
        public void IsError(int index)
        {
            switch (index)
            {
                case 1:
                    if (ChooseA)
                    {
                        IsErrorA = true;
                        IsOkA = true;
                    }
                    else
                    {
                        IsErrorA = false;
                        IsOkA = false;
                    }
                    break;
                case 2:
                    if (ChooseB)
                    {
                        IsErrorB = true;
                        IsOkB = true;
                    }
                    else
                    {
                        IsErrorB = false;
                        IsOkB = false;
                    }
                    break;
            }
        }
        public void Confirm(int index)
        { 
            switch (index)
            {
                case 1:
                    switch(LongmenAIndex)
                    {
                        case 0:
                            IsEnableA = Visibility.Visible;
                            IsAToB1 = false;
                            IsBToE1= false;
                            CompleteA = false;
                            IsMoveToB1= false;
                            IsMoveToA1 = true;
                            break;
                        case 1:
                            IsEnableA = Visibility.Visible;
                            IsAToB1 = false;
                            IsBToE1 = false;
                            CompleteA = false;
                            IsMoveToB1 = true;
                            IsMoveToA1 = false;
                            break;
                        case 2:
                            IsEnableA = Visibility.Visible;
                            IsAToB1 = true;
                            IsBToE1 = false;
                            CompleteA = false;
                            IsMoveToB1 = false;
                            IsMoveToA1 = false;
                            break;
                        case 3:
                            IsEnableA = Visibility.Visible;
                            IsAToB1 = false;
                            IsBToE1 = false;
                            CompleteA = true;
                            IsMoveToB1 = false;
                            IsMoveToA1 = false;
                            MesaBackgroundA = Brushes.LightPink;
                            break;
                        case 4:
                            IsEnableA = Visibility.Visible;
                            IsAToB1 = false;
                            IsBToE1 = false;
                            CompleteA = true;
                            IsMoveToB1 = false;
                            IsMoveToA1 = false;
                            MesaBackgroundA = Brushes.LightPink;
                            break;
                    }
                    break;
                case 2:
                    switch (LongmenBIndex)
                    {
                        case 0:
                            IsEnableB = Visibility.Visible;
                            IsAToB2 = false;
                            IsBToE2 = false;
                            CompleteB = false;
                            IsMoveToB2 = false;
                            IsMoveToA2 = true;
                            break;
                        case 1:
                            IsEnableB = Visibility.Visible;
                            IsAToB2 = false;
                            IsBToE2 = false;
                            CompleteB = false;
                            IsMoveToB2 = true;
                            IsMoveToA2 = false;
                            break;
                        case 2:
                            IsEnableB = Visibility.Visible;
                            IsAToB2 = true;
                            IsBToE2 = false;
                            CompleteB = false;
                            IsMoveToB2 = false;
                            IsMoveToA2 = false;
                            break;
                        case 3:
                            IsEnableB = Visibility.Visible;
                            IsAToB2 = false;
                            IsBToE2 = false;
                            CompleteB = true;
                            IsMoveToB2 = false;
                            IsMoveToA2 = false;
                            MesaBackgroundB = Brushes.LightPink;
                            break;
                        case 4:
                            IsEnableB = Visibility.Visible;
                            IsAToB2 = false;
                            IsBToE2 = false;
                            CompleteB = true;
                            IsMoveToB2 = false;
                            IsMoveToA2 = false;
                            MesaBackgroundB = Brushes.LightPink;
                            break;
                    }
                    break; 
            }
        }

        public void Clear()
        {
            IsAToB1 = false;
            IsBToE1 = false;
            CompleteA = false;
            IsMoveToB1 = false;
            IsMoveToA1 = false;
            IsAToB2 = false;
            IsBToE2 = false;
            CompleteB = false;
            IsMoveToB2 = false;
            IsMoveToA2 = false;

            //CompleteA = true;
            //CompleteB = true;
            //IsEnableA = Visibility.Hidden;
            //IsEnableB = Visibility.Hidden;
            //GrantyA = 0;
            //GrantyB = 0;
            WorkB = false;
        }

        public void Auto()
        {
            mainStop = new CancellationTokenSource();
            Test = new Task(RefreshTest);
            Test.Start();
            // mainStop=new CancellationTokenSource(); 
        }

    }
}
