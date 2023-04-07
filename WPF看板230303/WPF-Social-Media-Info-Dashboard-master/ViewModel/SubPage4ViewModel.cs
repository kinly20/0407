using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.ComponentModel;
using System.Data;
using Dashboard;
using System.Collections.ObjectModel;

namespace Dashboard.ViewModel
{
    class SubPage4ViewModel : CNotifyPropertyChange
    {
        DispatcherTimer timer;
        public SubPage4ViewModel()
        {
            loaddata();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer1_Tick;
            timer.Start();
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            loaddata();
        }

        public void loaddata()
        {
            List<bool> datas = StaticPlc.Getstatus();
            Dictionary<string, string> dt = StaticPlc.GetIPInfos();
            ObservableCollection<MachineModel> subs = new ObservableCollection<MachineModel>();
            for (int i = 0; i < dt.Count; i++)
            {
                //if (i % 2 == 0)
                //{
                MachineModel sub = new MachineModel(dt.Values.ToList()[i].ToString(), dt.Values.ToList()[i].ToString(), datas[i] ? "Visible" : "Hidden");
                subs.Add(sub);
                //}
                //else
                //{
                //    MachineModel sub = new MachineModel(dt.Values.ToList()[i].ToString(), dt.Values.ToList()[i].ToString(), "Visible");
                //    subs.Add(sub);
                //}
            }
            _showsubModels = new ObservableCollection<MachineModel>();
            ShowsubModels = subs;
            if (subs.Count > 0)
                _selectsubModel = subs[0];
        }

        private RelayCommand _chartCommand;
        public RelayCommand ChartCommand
        {
            get
            {
                if (_chartCommand == null)
                    _chartCommand = new RelayCommand(
                       new Action<object>(
                           o => chart()));
                return _chartCommand;
            }
        }

        public void chart()
        {

            ChartWindows subView = new ChartWindows(_selectsubModel.Name);

            var dialog = subView.ShowDialog();

            if (dialog == true)
            {

                loaddata();

            }
        }


        private MachineModel _selectsubModel;

        public MachineModel SelectsubModel
        {
            get
            {
                return _selectsubModel;
            }
            set
            {
                _selectsubModel = value;
                this.NotifyPropertyChange("SelectsubModel");
            }
        }

        private ObservableCollection<MachineModel> _showsubModels;

        public ObservableCollection<MachineModel> ShowsubModels
        {
            get { return _showsubModels; }
            set
            {
                _showsubModels = value;
                this.NotifyPropertyChange("ShowsubModels");
            }
        }
    }

    public class MachineModel : CNotifyPropertyChange
    {
        public MachineModel(string name, string ip, string runstate)
        {
            _name = name;
            _ip = ip;
            _runstate = runstate == "Visible" ? "Hidden" : "Visible";
            _crunstate = runstate == "Hidden" ? "Hidden" : "Visible";
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                this.NotifyPropertyChange("Name");
            }
        }

        private string _ip;
        public string Ip
        {
            get { return _ip; }
            set
            {
                _ip = value;
                this.NotifyPropertyChange("Ip");
            }
        }

        private string _runstate;
        public string Runstate
        {
            get { return _runstate; }
            set
            {
                _runstate = value;
                this.NotifyPropertyChange("Runstate");
            }
        }

        private string _crunstate;
        public string CRunstate
        {
            get { return _crunstate; }
            set
            {
                _crunstate = value;
                this.NotifyPropertyChange("CRunstate");
            }
        }

    }
}
