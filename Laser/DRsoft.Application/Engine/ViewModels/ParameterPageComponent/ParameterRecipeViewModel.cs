using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Caliburn.Micro;
using DRsoft.Modeling.Metadata.Models.Config;
using DRsoft.Runtime.Core.Platform.Logging;
using Engine.Models;
using Engine.Transfer;

namespace Engine.ViewModels.ParameterPageComponent
{
    public class ParameterRecipeViewModel : ViewModelBase
    {
        public readonly IWindowManager WindowManager;
        public ParameterPageViewModel Ppvm;
        public MainWindowViewModel Mwvm;

        public ParameterRecipeViewModel(IWindowManager iwindow, MainWindowViewModel mwm, ParameterPageViewModel ppm)
        {
            this.WindowManager = iwindow;
            this.Ppvm = ppm;
            this.Mwvm = mwm;
            RecipeNotes = new ObservableCollection<List<RecipeNote>>();
        }

        #region 属性绑定

        private ObservableCollection<List<RecipeNote>> _recipeNotes = null!;

        public ObservableCollection<List<RecipeNote>> RecipeNotes
        {
            get
            {
                return _recipeNotes;
            }
            set
            {
                _recipeNotes = value;
                NotifyOfPropertyChange(() => RecipeNotes);
            }
        }

        private object _recipeDataGridSelectedItem = null!;

        public object RecipeDataGridSelectedItem
        {
            get
            {
                return _recipeDataGridSelectedItem;
            }
            set
            {
                _recipeDataGridSelectedItem = value;
                NotifyOfPropertyChange(() => RecipeDataGridSelectedItem);
            }
        }

        private int _recipeDataGridSelectedIndex;

        public int RecipeDataGridSelectedIndex
        {
            get
            {
                return _recipeDataGridSelectedIndex;
            }
            set
            {
                _recipeDataGridSelectedIndex = value;
                NotifyOfPropertyChange(() => RecipeDataGridSelectedIndex);
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

        #endregion

        #region 事件处理
        public void DgAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            var result = e.PropertyName;
            var p = (e.PropertyDescriptor as PropertyDescriptor)?.ComponentType.GetProperties().FirstOrDefault(x => x.Name == e.PropertyName);

            if (p != null)
            {
                var found = p.GetCustomAttribute<DisplayAttribute>();
                if (found != null) result = found.Name;
            }

            e.Column.Header = result;
        }

        public void AddRecipe()
        {
            var newguid = new[]
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
            };
            var recipeNote = new RecipeNote
            {
                Order = Method.LisRecipes.Count + 1,
                Id = newguid,
                Name = ""
            };
            for (var i = 0; i < recipeNote.Path!.Length; i++)
            {
                recipeNote.Path[i] = "_metadata\\RecipeConfig\\" + newguid[i].ToString() + ".metadata.config";
            }
            Method.LisRecipes.Add(recipeNote);
            InteractiveData!.RecipeDataGrid = Method.LisRecipes;
            RecipeNotes.Add(InteractiveData!.RecipeDataGrid);
        }

        public void DelRecipe()
        {
            var index = RecipeDataGridSelectedIndex;
            if (Method.SelectRecipe == (RecipeDataGridSelectedItem as RecipeNote)!.Name)
            {
                MessageBox.Show("不允许删除当前使用配方,请再次确认", "警告");
                this.Mwvm.Log?.ErrorFormat("不允许删除当前使用配方,请再次确认");
                return;
            }

            try
            {
                // ReSharper disable once UnusedVariable
                var path0 = AppDomain.CurrentDomain.BaseDirectory + Method.LisRecipes[index].Path![0];
                // ReSharper disable once UnusedVariable
                var path1 = AppDomain.CurrentDomain.BaseDirectory + Method.LisRecipes[index].Path![1];
                // ReSharper disable once UnusedVariable
                var path2 = AppDomain.CurrentDomain.BaseDirectory + Method.LisRecipes[index].Path![2];
                if (Method.DicEngineConfig.ContainsKey(Method.LisRecipes[index].Name))
                    Method.DicEngineConfig.Remove(Method.LisRecipes[index].Name);
                if (Method.DicControlConfig.ContainsKey(Method.LisRecipes[index].Name))
                    Method.DicControlConfig.Remove(Method.LisRecipes[index].Name);
                if (Method.DicVisionCalibrationConfig.ContainsKey(Method.LisRecipes[index].Name))
                    Method.DicVisionCalibrationConfig.Remove(Method.LisRecipes[index].Name);
                if (Method.GuidRecipeConfig.ContainsKey(Method.LisRecipes[index].Name))
                    Method.GuidRecipeConfig.Remove(Method.LisRecipes[index].Name);
                Method.LisRecipeName.Remove(Method.LisRecipes[index].Name);
                Method.LisRecipes.RemoveAt(index);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                this.Mwvm.Log?.ErrorFormat(e.ToString());
                this.Mwvm.EngineAppService.CancleCheckOut();
                this.Mwvm.ControllAppService.CancleCheckOut();
                this.Mwvm.VisionAppService.CancleCheckOut();
            }
        }

        public void SaveRecipe()
        {

        }
        #endregion
    }
}
