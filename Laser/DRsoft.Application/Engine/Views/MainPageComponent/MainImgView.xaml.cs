using System.Windows.Media.Imaging;
using DRsoft.Engine.Model.Vision;
using DRsoft.Runtime.Core.Platform.Logging;
using Engine.StartUpWindow;
using Application = System.Windows.Application;
using Image = System.Windows.Controls.Image;

namespace Engine.Views.MainPageComponent
{
    /// <summary>
    /// Interaction logic for MainImgView.xaml
    /// </summary>
    public partial class MainImgView
    {
        public static LoadingPageView? LoadingPageWindow = null;
        public static MarkingBackgroundWindow? MarkingBackgroundWin = null;
        public static int MarkLength = 12;
        public int[] MarkingStatusFeedback = new int[6];
        public ILog? Log;

        public MainImgView()
        {
            InitializeComponent();
            DataContext = this;
            Log = LogProvider.GetLogger(this.GetType());
            Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
            try
            {
                if (LoadingPageWindow != null)
                {
                    LoadingPageWindow.Dispatcher.BeginInvoke((Action)(() =>
                    {
                        LoadingPageWindow.LoadProgress(1);
                    }));
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public MarkingBackgroundWindow GetInstance()
        {
            return MarkingBackgroundWin!;
        }

        public void MarkingPowerTestOn(int currentTestUnitNum, double powerTestRate, double testX, double testY)
        {
            if (MarkingBackgroundWin != null)
            {
                MarkingBackgroundWin.Dispatcher.BeginInvoke((Action)(() =>
                {
                    MarkingBackgroundWin.DrMark[currentTestUnitNum].SetPower("default", powerTestRate);
                    MarkingBackgroundWin.DrMark[currentTestUnitNum].JumpTo(testX, testY);
                    MarkingBackgroundWin.DrMark[currentTestUnitNum].LaserOn();
                    MarkingBackgroundWin.DrIo[currentTestUnitNum].SetOutput(15, 0);
                }));
            }
        }

        public void MarkingPowerTestOff(int currentTestUnitNum)
        {
            if (MarkingBackgroundWin != null)
            {
                MarkingBackgroundWin.Dispatcher.BeginInvoke((Action)(() =>
                {
                    MarkingBackgroundWin.DrMark[currentTestUnitNum].LaserOff();
                    MarkingBackgroundWin.DrIo[currentTestUnitNum].SetOutput(15, 1);
                }));
            }
        }

        public void ManualMarking(LaserPadPosition laserPadPosition, int i, double x, double y, double z)
        {
            if (MarkingBackgroundWin != null)
            {
                MarkingBackgroundWin.Dispatcher.BeginInvoke((Action)(() =>
                {
                    MarkingBackgroundWin.ManualMarking(laserPadPosition, i, x, y, z);
                }));
            }
        }

        public int[] Marking(int longMenNum, int processLineNum, LaserPadPosition laserPadPosition, double x, double y, double z)
        {
            if (MarkingBackgroundWin != null)
            {
                MarkingBackgroundWin.Dispatcher.Invoke((Action)(() =>
                {
                    MarkingStatusFeedback = MarkingBackgroundWin.Marking(longMenNum, processLineNum, laserPadPosition, x, y, z);
                }));
            }
            return MarkingStatusFeedback;
        }

        public void MarkEndRestGraph(int k)
        {
            if (MarkingBackgroundWin != null)
            {
                MarkingBackgroundWin.Dispatcher.BeginInvoke((Action)(() =>
                {
                    MarkingBackgroundWin.MarkEndRestGraph(k);
                }));
            }
        }

        public void ResetMark()
        {
            if (MarkingBackgroundWin != null)
            {
                MarkingBackgroundWin.Dispatcher.BeginInvoke((Action)(() =>
                {
                    MarkingBackgroundWin.ResetMark();
                }));
            }
        }

        public void Dispatcher_ShutdownStarted(object? sender, EventArgs eventArgs)
        {
            if (MarkingBackgroundWin != null)
            {
                MarkingBackgroundWin.Dispatcher.Invoke((Action)(() =>
                {
                    MarkingBackgroundWin.Dispatcher_ShutdownStarted(sender, eventArgs);
                }));
            }
            try
            {
                if (MarkingBackgroundWin != null)
                {
                    MarkingBackgroundWin.Dispatcher.Invoke((Action)(() =>
                    {
                        MarkingBackgroundWin.Close();
                    }));
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        public void LoadingJpg(string path,Image image)
        {
            BitmapImage bitmap;
            try
            {
                using (MemoryStream ms = new MemoryStream(File.ReadAllBytes(path)))
                {
                    bitmap = new BitmapImage
                    {
                        DecodePixelHeight = 100
                    };
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();
                    bitmap.Freeze();
                }
                image.Source = bitmap;
            }
            catch (Exception e)
            {
            }
        }

        public void LoadingMarkingJpg()
        {
            string strImagePath = null;
            Application.Current.Dispatcher.Invoke((Action)delegate ()
            {
                strImagePath = $"{AppDomain.CurrentDomain.BaseDirectory}1.jpg";
                LoadingJpg(strImagePath, this.WinFormGrid1);
                strImagePath = $"{AppDomain.CurrentDomain.BaseDirectory}2.jpg";
                LoadingJpg(strImagePath, this.WinFormGrid2);
                strImagePath = $"{AppDomain.CurrentDomain.BaseDirectory}3.jpg";
                LoadingJpg(strImagePath, this.WinFormGrid3);
                strImagePath = $"{AppDomain.CurrentDomain.BaseDirectory}4.jpg";
                LoadingJpg(strImagePath, this.WinFormGrid4);
                strImagePath = $"{AppDomain.CurrentDomain.BaseDirectory}5.jpg";
                LoadingJpg(strImagePath, this.WinFormGrid5);
                strImagePath = $"{AppDomain.CurrentDomain.BaseDirectory}6.jpg";
                LoadingJpg(strImagePath, this.WinFormGrid6);
                strImagePath = $"{AppDomain.CurrentDomain.BaseDirectory}7.jpg";
                LoadingJpg(strImagePath, this.WinFormGrid7);
                strImagePath = $"{AppDomain.CurrentDomain.BaseDirectory}8.jpg";
                LoadingJpg(strImagePath, this.WinFormGrid8);
                strImagePath = $"{AppDomain.CurrentDomain.BaseDirectory}9.jpg";
                LoadingJpg(strImagePath, this.WinFormGrid9);
                strImagePath = $"{AppDomain.CurrentDomain.BaseDirectory}10.jpg";
                LoadingJpg(strImagePath, this.WinFormGrid10);
                strImagePath = $"{AppDomain.CurrentDomain.BaseDirectory}11.jpg";
                LoadingJpg(strImagePath, this.WinFormGrid11);
                strImagePath = $"{AppDomain.CurrentDomain.BaseDirectory}12.jpg";
                LoadingJpg(strImagePath, this.WinFormGrid12);
            });
        }
    }
}
