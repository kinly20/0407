using DRsoft.Modeling.Metadata.Models.Config;

namespace Engine.Models
{
    public class InteractiveDataModel
    {
        public string TestValue { get; set; } = "";

        public string TestValue2 { get; set; } = "";

        public List<string>? RecipeItemsControl { get; set; } = null;

        public List<RecipeNote>? RecipeDataGrid { get; set; } = null;

        public Object? RecipeSelectValue { get; set; } = null;

        public string RecipeText { get; set; } = "";

        public int RecipeDataGridSelectedIndex { get; set; }

        public object? RecipeDataGridSelectedItem { get; set; }

    }
}
