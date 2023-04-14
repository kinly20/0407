using System.Text.RegularExpressions;

namespace Engine.Views.DebugPageComponent
{
    /// <summary>
    /// Interaction logic for DebugDetailView.xaml
    /// </summary>
    public partial class DebugDetailView : UserControl
    {
        public DebugDetailView()
        {
            InitializeComponent();
        }
        private void TextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string inputText = ((TextBox)sender).Text;
            int dotlength = Regex.Matches(inputText, @"[.]").Count;
            int spacelength = Regex.Matches(inputText, " ").Count;
            if ((e.Text == "." && dotlength >= 1) || (e.Text == " " && spacelength >= 1))
            {
                e.Handled = true;
                return;
            }
            var regex = new Regex(@"[^0-9,.-]+").IsMatch(e.Text);
            e.Handled = regex;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
