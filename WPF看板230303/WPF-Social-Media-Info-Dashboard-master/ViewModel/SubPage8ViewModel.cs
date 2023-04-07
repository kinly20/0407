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
    class SubPage8ViewModel : CNotifyPropertyChange
    {
        data data1 = new data();
        public SubPage8ViewModel()
        {
            loaddata();
        }

        public void loaddata()
        {

            DataTable dt = data1.SearchAllMachine();
            ObservableCollection<newmachinesubModel> subs = new ObservableCollection<newmachinesubModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                newmachinesubModel sub = new newmachinesubModel(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][4].ToString());
                subs.Add(sub);
            }
            _showsubModels = new ObservableCollection<newmachinesubModel>();
            ShowsubModels = subs;
            if (subs.Count > 0)
                _selectsubModel = subs[0];
        }


        private RelayCommand _deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new RelayCommand(
                       new Action<object>(
                           o => delete()));
                return _deleteCommand;
            }
        }

        public void delete()
        {
            if (!string.IsNullOrEmpty(_selectsubModel.Name))
                data1.DeleteMachine(_selectsubModel.Name);
            loaddata();
            //_showsubModels.Remove(_selectsubModel);
            //_selectsubModel = _showsubModels[0];
        }

        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                    _addCommand = new RelayCommand(
                       new Action<object>(
                           o => add()));
                return _addCommand;
            }
        }

        public void add()
        {
            //newmachinesubModel user = new newmachinesubModel("", "", "", "");
            NewMachineWindow SUBView = new NewMachineWindow();

            var dialog = SUBView.ShowDialog();

            if (dialog == true)
            {

                loaddata();

            }
        }


        private newmachinesubModel _selectsubModel;

        public newmachinesubModel SelectsubModel
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

        private ObservableCollection<newmachinesubModel> _showsubModels;

        public ObservableCollection<newmachinesubModel> ShowsubModels
        {
            get { return _showsubModels; }
            set
            {
                _showsubModels = value;
                this.NotifyPropertyChange("ShowsubModels");
            }
        }
    }

    public class newmachinesubModel : CNotifyPropertyChange
    {
        public newmachinesubModel(string id, string ip, string name, string type)
        {
            _id = id;
            _ip = ip;
            _name = name;
            _type = type;
        }

        private string _id;
        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                this.NotifyPropertyChange("ID");
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

        private string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                this.NotifyPropertyChange("Type");
            }
        }
    }
}
