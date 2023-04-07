using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using MyControl.Parameters;
using MyControl.tools;
using HslCommunication.Profinet;
using System.Threading;
using HslCommunication.Profinet.Melsec;
using HslCommunication;
using System.Windows.Threading;

namespace Dashboard
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            loaddata();
            InitializeComponent();
            string pagefilename = "Dashboard.SubPage7";
            Type T = Type.GetType(pagefilename);
            if (T != null)
                pagechange.Content = new Frame()
                {
                    Content = Activator.CreateInstance(T)
                };
            
            Loaded += new RoutedEventHandler(WindowMain_Loaded);

            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            //tbmax.Text = this.WindowState == WindowState.Maximized ? "  回" : "  口";
        }

     
        //Trag trig = new Trag();
        private DispatcherTimer timer;
      

        public void loaddata()
        {
            StaticPlc.Load();
            //trig.RisingEdge += Trig_RisingEdge;
        }

        public void WindowMain_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer1_Tick;
            timer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tb_allalarm.Text = StaticPlc.getAllAlarm();
            //trig.Value = dataChange.GetBitValue(1, 0);//模拟地址150为扫码信号
        }
       

        //private void Trig_RisingEdge(object sender, EventArgs e)
        //{
        //    try
        //    {
                

        //    }
        //    catch (Exception ex)
        //    {
        //        //Log.writelog("扫码" + ex.ToString());
        //    }
        //}



        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void MinBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MaxBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            //tbmax.Text = this.WindowState == WindowState.Maximized ? "  回" : "  口";
        }



        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button bt = sender as System.Windows.Controls.Button;
            string btname = bt.Name;
            ChanggeMenu(btname);
        }

        private void ChanggeMenu(string selectmenu)
        {
            string pagefilename = "Dashboard.SubPage";

            subChanggeMenu(btmenu1, false);
            subChanggeMenu(btmenu2, false);
            subChanggeMenu(btmenu3, false);
            subChanggeMenu(btmenu4, false);
            subChanggeMenu(btmenu5, false);
            subChanggeMenu(btmenu6, false);
            subChanggeMenu(btmenu7, false);
            switch (selectmenu)
            {
                case "btmenu1":
                    subChanggeMenu(btmenu1, true);
                    pagefilename += "7";
                    break;
                case "btmenu2":
                    subChanggeMenu(btmenu2, true);
                    pagefilename += "4_1";
                    break;
                case "btmenu3":
                    subChanggeMenu(btmenu3, true);
                    pagefilename += "3";
                    break;
                case "btmenu4":
                    subChanggeMenu(btmenu4, true);
                    pagefilename += "8";
                    break;
                case "btmenu5":
                    subChanggeMenu(btmenu5, true);
                    pagefilename += "2";
                    break;
                case "btmenu6":
                    subChanggeMenu(btmenu6, true);
                    pagefilename += "9";
                    break;
                case "btmenu7":
                    subChanggeMenu(btmenu7, true);
                    pagefilename += "5";
                    break;
                case "btmenudown":
                    //subChanggeMenu(btmenu6, true);
                    pagefilename += "6";
                    break;
            }

            //if (pagefilename != "Dashboard.SubPage2")
            //{
                Type T = Type.GetType(pagefilename);
                if (T != null)
                {

                    pagechange.Content = new Frame()
                    {
                        Content = Activator.CreateInstance(T)
                    };
                }
            //}
            //else {
            //    SubPage2 a = new SubPage2();
            //    this.pagechange.Content = a;
            //    a.ParentWindow = this;
            //}

           
        }

        private void subChanggeMenu(System.Windows.Controls.Button bt, bool show)
        {
            if (show)
            {
                var style = this.TryFindResource("menuButtonSelect") as Style;
                bt.Style = style;
            }
            else
            {
                var style = this.TryFindResource("menuButton") as Style;
                bt.Style = style;
            }
        }

        private void tbmenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            string btname = tb.Name;
            upMenu(btname);
        }

        private void upMenu(string selectmenu)
        {
            subupMenu(tb_1, false);
            subupMenu(tb_2, false);
            subupMenu(tb_3, false);
            subupMenu(tb_4, false);
            subupMenu(tb_5, false);
            switch (selectmenu)
            {
                case "tb_1":
                    subupMenu(tb_1, true);
                    break;
                case "tb_2":
                    subupMenu(tb_2, true);
                    break;
                case "tb_3":
                    subupMenu(tb_3, true);
                    break;
                case "tb_4":
                    subupMenu(tb_4, true);
                    break;
                case "tb_5":
                    subupMenu(tb_5, true);
                    break;
            }
        }

        private void subupMenu(TextBlock bt, bool show)
        {
            if (show)
            {
                var style = this.TryFindResource("menuTitleSelect") as Style;
                bt.Style = style;
            }
            else
            {
                var style = this.TryFindResource("menuTitle") as Style;
                bt.Style = style;
            }
        }
    }
}