using System.Windows.Forms.Integration;
using AxMMMarkx641Lib;
using AxMMIOx641Lib;
using AxMMEditx641Lib;
using DRsoft.Engine.Model.Vision;
using DRsoft.Runtime.Core.Platform.Events;
using DRsoft.Runtime.Core.Platform.Ioc;
using DRsoft.Runtime.Core.Platform.Logging;
using Engine.Behaviors;
using Engine.Models;
using System.Windows.Threading;

namespace Engine.StartUpWindow
{
    /// <summary>
    /// Interaction logic for MarkingBackground.xaml
    /// </summary>
    public partial class MarkingBackgroundWindow
    {
        public static int MarkLength = 12;
        public ILog? Log;
        public IEventAggregator? Aggregator;
        public int[] MarkingStatusFeedback = new int[6];
        public DispatcherTimer DrMarkInitialTimer = new DispatcherTimer();
        public bool[] MarkingInitialFlag = new bool[MarkLength];
        public List<AxMMMarkx641> DrMark = new List<AxMMMarkx641>()
        {
            new AxMMMarkx641Lib.AxMMMarkx641(),
            new AxMMMarkx641Lib.AxMMMarkx641(),
            new AxMMMarkx641Lib.AxMMMarkx641(),
            new AxMMMarkx641Lib.AxMMMarkx641(),
            new AxMMMarkx641Lib.AxMMMarkx641(),
            new AxMMMarkx641Lib.AxMMMarkx641(),
            new AxMMMarkx641Lib.AxMMMarkx641(),
            new AxMMMarkx641Lib.AxMMMarkx641(),
            new AxMMMarkx641Lib.AxMMMarkx641(),
            new AxMMMarkx641Lib.AxMMMarkx641(),
            new AxMMMarkx641Lib.AxMMMarkx641(),
            new AxMMMarkx641Lib.AxMMMarkx641()
        };
        public List<AxMMIOx641> DrIo = new List<AxMMIOx641Lib.AxMMIOx641>()
        {
            new AxMMIOx641Lib.AxMMIOx641(),
            new AxMMIOx641Lib.AxMMIOx641(),
            new AxMMIOx641Lib.AxMMIOx641(),
            new AxMMIOx641Lib.AxMMIOx641(),
            new AxMMIOx641Lib.AxMMIOx641(),
            new AxMMIOx641Lib.AxMMIOx641(),
            new AxMMIOx641Lib.AxMMIOx641(),
            new AxMMIOx641Lib.AxMMIOx641(),
            new AxMMIOx641Lib.AxMMIOx641(),
            new AxMMIOx641Lib.AxMMIOx641(),
            new AxMMIOx641Lib.AxMMIOx641(),
            new AxMMIOx641Lib.AxMMIOx641()
        };
        public List<AxMMEditx641> DrEdit = new List<AxMMEditx641>()
        {
            new AxMMEditx641Lib.AxMMEditx641(),
            new AxMMEditx641Lib.AxMMEditx641(),
            new AxMMEditx641Lib.AxMMEditx641(),
            new AxMMEditx641Lib.AxMMEditx641(),
            new AxMMEditx641Lib.AxMMEditx641(),
            new AxMMEditx641Lib.AxMMEditx641(),
            new AxMMEditx641Lib.AxMMEditx641(),
            new AxMMEditx641Lib.AxMMEditx641(),
            new AxMMEditx641Lib.AxMMEditx641(),
            new AxMMEditx641Lib.AxMMEditx641(),
            new AxMMEditx641Lib.AxMMEditx641(),
            new AxMMEditx641Lib.AxMMEditx641()
        };
        public readonly List<WindowsFormsHost> HostMark = new List<WindowsFormsHost>()
        {
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
        };
        public readonly List<WindowsFormsHost> HostEdit = new List<WindowsFormsHost>()
        {
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
        };
        public readonly List<WindowsFormsHost> HostIo = new List<WindowsFormsHost>()
        {
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
            new WindowsFormsHost(),
        };

        public readonly List<Grid> Grid = new List<Grid>();

        public MarkingMateMethod MarkingMethod;

        public MarkingBackgroundWindow()
        {
            InitializeComponent();
            DataContext = this;
            this.Aggregator = ServiceProviderManager.Instance.ServiceProvider.GetService<IEventAggregator>();

            Log = LogProvider.GetLogger(this.GetType());
            MarkingMethod = new MarkingMateMethod(Log);
            Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
            DrMarkInitialTimer.Interval = TimeSpan.FromMilliseconds(500);
            DrMarkInitialTimer.Tick += new EventHandler(QueryMarkInitialTimer);
            for (int i = 0; i < MarkingInitialFlag.Length; i++) MarkingInitialFlag[i] = false;
            Grid.Clear();
            Grid.Add(WinFormGrid1);
            Grid.Add(WinFormGrid2);
            Grid.Add(WinFormGrid3);
            Grid.Add(WinFormGrid4);
            Grid.Add(WinFormGrid5);
            Grid.Add(WinFormGrid6);
            Grid.Add(WinFormGrid7);
            Grid.Add(WinFormGrid8);
            Grid.Add(WinFormGrid9);
            Grid.Add(WinFormGrid10);
            Grid.Add(WinFormGrid11);
            Grid.Add(WinFormGrid12);
            try
            {
                for (var i = 0; i < Grid.Count; i++)
                {
                    MarkingMethod.MarkingInit(i,Grid[i], HostMark[i],
                        HostEdit[i], HostIo[i], DrMark[i],
                        DrEdit[i], DrIo[i], $"/cfg_config_MM{i + 1}", "", $"{i + 1}#打标卡");
                }
                Environment.CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                DrMarkInitialTimer.Start();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void MarkingMateInit()
        {
            Aggregator?.GetEvent<PubSubEvent<WindowMessagePush>>().Publish(new WindowMessagePush() { MarkingMateFinish = true });
        }

        public void ManualMarking(LaserPadPosition laserPadPosition, int i, double x, double y, double z)
        {
            MarkingMethod.ManualMarking(DrMark[i - 1], DrEdit[i - 1], DrIo[i - 1], laserPadPosition, i - 1, x, y, z);
        }

        public int[] Marking(int longMenNum, int processLineNum, LaserPadPosition laserPadPosition, double x, double y,
            double z)
        {
            if (longMenNum < 1 || longMenNum > 2 || processLineNum < 1 || laserPadPosition.SolderTapesGroupList == null)
            {
                return MarkingStatusFeedback;
            }

            if (processLineNum == 10)
            {
                for (var i = 0 + 6 * (longMenNum - 1); i < laserPadPosition.SolderTapesGroupList.Length + 6 * (longMenNum - 1); i++)
                {
                    DrIo[i].SetOutput(15, 1);
                    DrMark[i].LaserOff();
                    DrMark[i].JumpToStartPos();
                    string[] layerNames = new string[6]; //汇流带名称
                    for (var k = 0; k < 6; k++)
                    {
                        layerNames[k] = "H" + k;
                    }

                    var unused = layerNames.Length; //汇流焊带数
                    SolderTape[]? solderTapes = null;
                    if (longMenNum == 1) solderTapes = laserPadPosition.SolderTapesGroupList[i].FlowTogetherTapes;
                    if (longMenNum == 2) solderTapes = laserPadPosition.SolderTapesGroupList[i - 6].SolderTapes;
                    if (solderTapes != null)
                    {
                        MarkingMethod.SetLayerLh(DrEdit[i], layerNames, 1, solderTapes, x, y, z);
                        var result = DrMark[i].StartAutomation();
                        if (i >= 6)
                        {
                            MarkingStatusFeedback[i - 6] = result;
                        }
                        else
                            MarkingStatusFeedback[i] = result;
                        if (result > 0) Log?.ErrorFormat((i + 1).ToString() + "#激光器" + "打标功能异常StartAutomation()，请联系开发人员：" + result);
                    }
                }
            }
            else if (processLineNum == 7)
            {
                for (int i = 0 + 6; i < laserPadPosition.SolderTapesGroupList.Length + 6; i++)
                {
                    DrIo[i].SetOutput(15, 1);
                    DrMark[i].LaserOff();
                    DrMark[i].JumpToStartPos();
                    string[] layerNames = new string[6]; //汇流带名称
                    for (int k = 0; k < 6; k++)
                    {
                        layerNames[k] = "H" + k;
                    }
                    int unused = layerNames.Length; //汇流焊带数
                    SolderTape[]? solderTapes = laserPadPosition.SolderTapesGroupList[i - 6].SolderTapes;
                    if (solderTapes != null)
                    {
                        MarkingMethod.SetLayerLh(DrEdit[i], layerNames, 1, solderTapes, x, y, z);
                        var result = DrMark[i].StartAutomation();
                        if (i >= 6)
                        {
                            MarkingStatusFeedback[i - 6] = result;
                        }
                        else
                            MarkingStatusFeedback[i] = result;
                        if (result > 0) Log?.ErrorFormat((i + 1).ToString() + "#激光器" + "打标功能异常StartAutomation()，请联系开发人员：" + result);
                    }
                }
            }
            else if(processLineNum == 6)
            {
                if (laserPadPosition.SolderTapesGroupList.Length == 0)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        for (int k = 0; k < 24; k++)
                        {
                            string name = "L" + k + 1;
                            DrEdit[i].SetLayerOutput(name, 0);//禁用图层L1输出 
                        }
                    }
                }
                for (int i = 0 + 6 * (longMenNum - 1); i < laserPadPosition.SolderTapesGroupList.Length + 6 * (longMenNum - 1); i++)
                {
                    DrIo[i].SetOutput(15, 1);
                    DrMark[i].LaserOff();
                    DrMark[i].JumpToStartPos();
                    string[] layerNames = new string[24]; //汇流带名称
                    for (int k = 0; k < 24; k++)
                    {
                        layerNames[k] = "L" + (k + 1);
                    }

                    int solderNum = layerNames.Length; //汇流焊带数
                    SolderTape[]? solderTapes = null;
                    if (longMenNum == 1) solderTapes = laserPadPosition.SolderTapesGroupList[i].SolderTapes;
                    if (longMenNum == 2) solderTapes = laserPadPosition.SolderTapesGroupList[i - 6].SolderTapes;
                    if (solderTapes != null)
                    {
                        MarkingMethod.SetLayer(DrEdit[i], layerNames, solderNum, solderTapes, x, y, z);
                        var result = DrMark[i].StartAutomation();
                        if (i >= 6)
                        {
                            MarkingStatusFeedback[i - 6] = result;
                        }
                        else
                            MarkingStatusFeedback[i] = result;

                        if (result > 0) Log?.ErrorFormat((i + 1).ToString() + "#激光器" + "打标功能异常StartAutomation()，请联系开发人员：" + result);
                    }
                }
            }
            else
            {
                if (laserPadPosition.SolderTapesGroupList.Length == 0)
                {
                    for (int i = 0; i < 6; i++)
                    {
                        for (int k = 0; k < 24; k++)
                        {
                            string name = "L" + k + 1;
                            DrEdit[i].SetLayerOutput(name, 0);//禁用图层L1输出 
                        }
                    }
                }
                for (int i = 0 + 6 * (longMenNum - 1); i < laserPadPosition.SolderTapesGroupList.Length + 6 * (longMenNum - 1); i++)
                {
                    DrIo[i].SetOutput(15, 1);
                    DrMark[i].LaserOff();
                    DrMark[i].JumpToStartPos();
                    string[] layerNames = new string[24]; //汇流带名称
                    for (int k = 0; k < 24; k++)
                    {
                        layerNames[k] = "L" + (k + 1);
                    }

                    int solderNum = layerNames.Length; //汇流焊带数
                    SolderTape[]? solderTapes = null;
                    if (longMenNum == 1) solderTapes = laserPadPosition.SolderTapesGroupList[i].SolderTapes;
                    if (longMenNum == 2) solderTapes = laserPadPosition.SolderTapesGroupList[i - 6].SolderTapes;
                    if (solderTapes != null)
                    {
                        MarkingMethod.SetLayer(DrEdit[i], layerNames, solderNum, solderTapes, x, y, z);
                        var result = DrMark[i].StartAutomation();
                        if (i >= 6)
                        {
                            MarkingStatusFeedback[i - 6] = result;
                        }
                        else
                            MarkingStatusFeedback[i] = result;

                        if (result > 0) Log?.ErrorFormat((i + 1).ToString() + "#激光器" + "打标功能异常StartAutomation()，请联系开发人员：" + result);
                    }
                }

                //for (int i = 6 + 6 * (longMenNum - 1); i < laserPadPosition.SolderTapesGroupList.Length + 6 * (longMenNum - 1); i--)
                //{
                //    DrIo[i].SetOutput(15, 1);
                //    DrMark[i].LaserOff();
                //    DrMark[i].JumpToStartPos();
                //    string[] layerNames = new string[24]; //汇流带名称
                //    for (int k = 0; k < 24; k++)
                //    {
                //        layerNames[k] = "L" + (k + 1);
                //    }

                //    int solderNum = layerNames.Length; //汇流焊带数
                //    SolderTape[]? solderTapes = null;
                //    if (longMenNum == 1) solderTapes = laserPadPosition.SolderTapesGroupList[i].SolderTapes;
                //    if (longMenNum == 2) solderTapes = laserPadPosition.SolderTapesGroupList[i - 6].SolderTapes;
                //    if (solderTapes != null)
                //    {
                //        MarkingMethod.SetLayer(DrEdit[i], layerNames, solderNum, solderTapes, x, y, z);
                //        var result = DrMark[i].StartAutomation();
                //        if (i >= 6)
                //        {
                //            MarkingStatusFeedback[i - 6] = result;
                //        }
                //        else
                //            MarkingStatusFeedback[i] = result;

                //        if (result > 0) Log?.ErrorFormat((i + 1).ToString() + "#激光器" + "打标功能异常StartAutomation()，请联系开发人员：" + result);
                //    }
                //}
            }
            return MarkingStatusFeedback;
        }

        public void MarkEndRestGraph(int k)
        {
            //禁用所有图层输出
            for (int i = 1; i < 30; i++)
            {
                string name = "L" + i;
                DrEdit[k - 1].SetLayerOutput(name, 0);
            }
        }

        public void ResetMark()
        {
            for (int i = 0; i < DrMark.Count; i++)
            {
                DrMark[i].StopAutomation();
                DrMark[i].LaserOff();
                DrMark[i].JumpToStartPos();
                DrIo[i].SetOutput(15, 0);
            }
        }

        public void Dispatcher_ShutdownStarted(object? sender, EventArgs eventArgs)
        {
            for (int i = 0; i < MarkLength; i++)
            {
                MarkingMethod.MarkingFinish(DrMark[i], DrEdit[i], DrIo[i], $"{i + 1}#打标卡");
            }
        }

        private void QueryMarkInitialTimer(object? sender, EventArgs e)
        {
            for (int i = 0; i < MarkLength; i++)
            {
                MarkingInitialFlag[i] = MarkingMethod.QueryMarkingInitial(DrMark[i]);
            }
            foreach (var item in MarkingInitialFlag)
            {
                if (item == false) return;
            }
            DrMarkInitialTimer.Stop();
            MarkingMateInit();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Dispatcher_ShutdownStarted(sender, e);
        }
    }
}
