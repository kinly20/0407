using Caliburn.Micro;
using DRsoft.Runtime.Core.Platform.Ioc;
using Engine.Configurations;
using Engine.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Windows.Threading;
using DRsoft.Engine.Core;
using DRsoft.Runtime.Core.Platform.Crypt;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Microsoft.Extensions.Hosting;
using Engine.Behaviors;
using Engine.StartUpWindow;
using Engine.Views.MainPageComponent;

namespace Engine
{
    public class Bootstrapper : BootstrapperBase
    {
        public IConfiguration? Configuration { get; private set; }
        public ServiceProviderFactory? ServiceProviderFactory { get; private set; }
        public static AppRunConfig Config = new AppRunConfig();
        private readonly SimpleContainer _container = new SimpleContainer();
        private readonly SystemCommonFuction _systemCommonFuction = new SystemCommonFuction();
        private bool _created;

        public Bootstrapper()
        {
            _systemCommonFuction.ThreadExitis("DRMark", true);
            if (!_systemCommonFuction.CheckSoftware(new Dictionary<string, bool>()
                {
                    { "DRMark", false },
                    { "MySQL", false }
                }))
            {
                MessageBox.Show($"请确认DRMark软件及MySQL软件安装正常!\r\n请将上述软件安装至系统C盘默认路径!", "警报！");
                Environment.Exit(0);
            }
            Initialize();
            StartDebugLogger();
            string strProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            if (System.Diagnostics.Process.GetProcessesByName(strProcessName)[0].ProcessName == "Engine")
                AsyncOnStartUp();
        }

        // [Conditional("DEBUG")] You can use this conditional starting with C# 9.0
        public static void StartDebugLogger()
        {
            LogManager.GetLog = type => new DebugLog(type);
        }

        protected override void Configure()
        {
            _container.Instance(_container);
            _container
                .Singleton<IWindowManager, WindowManager>();
            //.Singleton<IEventAggregator, EventAggregator>();

            foreach (var assembly in SelectAssemblies())
            {
                assembly.GetTypes()
                    .Where(type => type.IsClass)
                    .Where(type => type.Name.EndsWith("ViewModel"))
                    .ToList()
                    .ForEach(viewModelType => _container.RegisterPerRequest(
                        viewModelType, viewModelType.ToString(), viewModelType));
            }
        }

        public async void AsyncOnStartUp()
        {
            //只运行开启一个
            var dummy = new Mutex(false, "Engine.exe", out var createNew);
            if (createNew)
            {
                //读取配置
                Config = GetAppRunConfig();
                ConfigRuntimeEnvironment(Config);
                //构建配置
                ServiceProviderFactory = ServiceProviderBootStrap.BuildFactory(Config);
                //构建宿主
                var appBuilder = CreateHostBuilder().Build();
                //获取配置节点信息
                //Configuration.GetSection("PLC").Get<PLCConfig>();
                //运行Host
                await appBuilder.RunAsync();
            }
            else
            {
                _created = true;
                MessageBox.Show("Engine.exe程序已经运行！");
                Application.Current.Shutdown();
            }
        }

        private IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(ServiceProviderFactory)
                .ConfigureHostConfiguration(config =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsetting.json", optional: false, reloadOnChange: true);
                    Configuration = config.Build();
                })
                .ConfigureServices(services =>
                {
                    ServiceProviderBootStrap.BuildHostBuilder(services);
                    services.AddTransient(typeof(Bootstrapper));
                    services.AddHostedService<EngineBootStrap>();
                });
        }

        private static AppRunConfig GetAppRunConfig()
        {
            string json;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "appsetting.json");
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (var sr = new StreamReader(fs, Encoding.UTF8))
                {
                    json = sr.ReadToEnd();
                }
            }

            return JsonConvert.DeserializeObject<AppRunConfig>(json) ?? new AppRunConfig();
        }

        private void ConfigRuntimeEnvironment(AppRunConfig config)
        {
            if (config.Plc != null)
            {
                if (config.Plc.Manufacturer != null) RuntimeEnvironment.Controller = config.Plc.Manufacturer;
                if (config.Plc.IdentificationCode != null) RuntimeEnvironment.PluginPlc = config.Plc.IdentificationCode;
            }

            if (config.Camera != null)
                if (config.Camera.IdentificationCode != null)
                    RuntimeEnvironment.VisualDispose = config.Camera.IdentificationCode;
            if (config.Calibrate != null)
                if (config.Calibrate.IdentificationCode != null)
                    RuntimeEnvironment.VisionCalibration = config.Calibrate.IdentificationCode;
            for (int i = 0; i < RuntimeEnvironment.VisionProduction.Length; i++)
            {
                if (config.Vision[i] != null)
                    if (config.Vision[i]?.IdentificationCode != null)
                        RuntimeEnvironment.VisionProduction[i] = config.Vision[i]?.IdentificationCode;
            }

            if (config.PowerMeter != null)
            {
                if (config.PowerMeter.IdentificationCode != null)
                    RuntimeEnvironment.PowerMeter = config.PowerMeter.IdentificationCode;
            }
        }

        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {
                if (_created) return;
                if (!LicenseManager.Instance.IsActivated)
                {
                    var registerKey = new RegisterKey();
                    registerKey.ShowDialog();
                    Environment.Exit(0);
                    return;
                }
                var thread2 = new Thread(() =>
                {
                    Thread.Sleep(10000);
                    MainImgView.MarkingBackgroundWin = new MarkingBackgroundWindow();
                    MainImgView.MarkingBackgroundWin.Show();
                    MainImgView.MarkingBackgroundWin.Activate();
                    MainImgView.MarkingBackgroundWin.Hide();
                    Dispatcher.Run();
                });
                thread2.SetApartmentState(ApartmentState.STA);
                thread2.IsBackground = true;
                thread2.Start();

                var thread = new Thread(() =>
                {
                    MainImgView.LoadingPageWindow = new LoadingPageView();
                    MainImgView.LoadingPageWindow.Show();
                    MainImgView.LoadingPageWindow.Activate();
                    Dispatcher.Run();
                });
                thread.SetApartmentState(ApartmentState.STA);
                thread.IsBackground = true;
                thread.Start();

                var dummy = IoC.Get<SimpleContainer>();
                await DisplayRootViewForAsync(typeof(MainWindowViewModel));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(0);
            }
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
