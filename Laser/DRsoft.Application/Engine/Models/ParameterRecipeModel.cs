using System.ComponentModel.DataAnnotations;

namespace Engine.Models
{
    public class ParameterRecipeModel : ModelBase
    {
        private string _id;
        private string _recipeName;

        [Display(Name = "序号")]
        public string ID
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        [Display(Name = "配方名称")]
        public string RecipeName
        {
            get => _recipeName;
            set => SetProperty(ref _recipeName, value);
        }
    }
}
