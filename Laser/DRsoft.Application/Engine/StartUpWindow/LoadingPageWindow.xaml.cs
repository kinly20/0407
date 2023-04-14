namespace Engine.StartUpWindow
{
    /// <summary>
    /// Interaction logic for LoadingPage.xaml
    /// </summary>
    public partial class LoadingPageView
    {
        public LoadingPageView()
        {
            InitializeComponent();
        }

        public void LoadProgress(int markingProgress)
        {
            switch (markingProgress)
            {
                case 1:
                    LoadingLabel.Text += $"\r\n引擎加载完成...\r\n打标卡后台初始化中...\r\n正在绘制窗口请稍等...";
                    LoadingProgress.Value = (100.0 / 12.0) * 0;
                    break;
                case 2:
                    LoadingLabel.Text += $"\r\n软件准备启动,请稍等...";
                    LoadingProgress.Value = (100.0 / 12.0) * markingProgress;
                    break;
            }
        }
    }
}
