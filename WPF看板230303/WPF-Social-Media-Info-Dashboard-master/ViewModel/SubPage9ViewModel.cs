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
    class SubPage9ViewModel : CNotifyPropertyChange
    {
        data data1 = new data();
        public SubPage9ViewModel()
        {
            loaddata();
        }

        public void loaddata()
        {
            List<bool> datas = StaticPlc.Getstatus();
            Dictionary<string, string> dt = StaticPlc.GetConnectIPInfos();
            ObservableCollection<ButtonModel> subs = new ObservableCollection<ButtonModel>();
            for (int i = 0; i < dt.Count; i++)
            {
                ButtonModel sub = new ButtonModel(dt.Values.ToList()[i].ToString(), dt.Values.ToList()[i].ToString());
                subs.Add(sub);
            }
            _showsubModels = new ObservableCollection<ButtonModel>();
            ShowsubModels = subs;
            if (subs.Count > 0)
                _selectsubModel = subs[0];

            ObservableCollection<string> products = new ObservableCollection<string>();

            ObservableCollection<RecipeModel> recipes = new ObservableCollection<RecipeModel>();
            DataTable dtrecipe= data1.SearchRecipe();
            for (int i = 0; i < dtrecipe.Rows.Count; i++)
            {
                RecipeModel sub = new RecipeModel(dtrecipe.Rows[i][0].ToString(),dtrecipe.Rows[i][4].ToString(), dtrecipe.Rows[i][2].ToString(), dtrecipe.Rows[i][3].ToString());
                recipes.Add(sub);
                string product = dtrecipe.Rows[i][2].ToString();
                if (!products.Contains(product))
                    products.Add(product);
            }
            _showrecipeModels = recipes;
            _productnames = products;


        }

        private RelayCommand _changeWidthCommand;
        public RelayCommand ChangeWidthCommand
        {
            get
            {
                if (_changeWidthCommand == null)
                    _changeWidthCommand = new RelayCommand(
                       new Action<object>(
                           o => ChangeWidth()));
                return _changeWidthCommand;
            }
        }

        public void ChangeWidth()
        {
            //StaticPlc.ChanggeWidth(SelectsubModel.Name, 1);
          
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
            if (!string.IsNullOrEmpty(_selectrecipeModel.Id))
                data1.DeleteRecipe(_selectrecipeModel.Id);
            loaddata();
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
            NewRecipeWindow subView = new NewRecipeWindow();

            var dialog = subView.ShowDialog();

            loaddata();
        }


        private ButtonModel _selectsubModel;

        public ButtonModel SelectsubModel
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

        private ObservableCollection<ButtonModel> _showsubModels;

        public ObservableCollection<ButtonModel> ShowsubModels
        {
            get { return _showsubModels; }
            set
            {
                _showsubModels = value;
                this.NotifyPropertyChange("ShowsubModels");
            }
        }

        private RecipeModel _selectrecipeModel;

        public RecipeModel SelectrecipeModel
        {
            get
            {
                return _selectrecipeModel;
            }
            set
            {
                _selectrecipeModel = value;
                this.NotifyPropertyChange("SelectrecipeModel");
            }
        }

        private ObservableCollection<RecipeModel> _showrecipeModels;

        public ObservableCollection<RecipeModel> ShowrecipeModels
        {
            get { return _showrecipeModels; }
            set
            {
                _showrecipeModels = value;
                this.NotifyPropertyChange("ShowrecipeModels");
            }
        }

        private ObservableCollection<string> _productnames;

        public ObservableCollection<string> Productnames
        {
            get { return _productnames; }
            set
            {
                _productnames = value;
                this.NotifyPropertyChange("Productnames");
            }
        }

        private string _selectproduct;

        public string SelectProduct
        {
            get { return _selectproduct; }
            set
            {
                _selectproduct = value;
                this.NotifyPropertyChange("SelectProduct");
            }
        }
    }

    public class ButtonModel : CNotifyPropertyChange
    {
        public ButtonModel(string name, string ip)
        {
            _name = name;
            _ip = ip;
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
    }

    public class RecipeModel : CNotifyPropertyChange
    {
        public RecipeModel(string id, string machine,string product,string recipe)
        {
            _id = id;
            _machine = machine;
            _product = product;
            _recipe = recipe;
        }

        private string _id;
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                this.NotifyPropertyChange("Id");
            }
        }

        private string _machine;
        public string Machine
        {
            get { return _machine; }
            set
            {
                _machine = value;
                this.NotifyPropertyChange("Machine");
            }
        }

        private string _product;
        public string Product
        {
            get { return _product; }
            set
            {
                _product = value;
                this.NotifyPropertyChange("Product");
            }
        }

        private string _recipe;
        public string Recipe
        {
            get { return _recipe; }
            set
            {
                _recipe = value;
                this.NotifyPropertyChange("Recipe");
            }
        }
    }
}
