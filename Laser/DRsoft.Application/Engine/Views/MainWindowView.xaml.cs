namespace Engine.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            InitializeComponent();
            DataContext = this;
            this.MainWindowName.MouseLeftButtonDown += (o, e) => { DragMove(); };
        }
    }
}
