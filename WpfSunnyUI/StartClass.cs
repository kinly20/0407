using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Win32;
using WpfSunnyUI.ViewModels;

namespace WpfSunnyUI
{
    public class StartClass : BootstrapperBase
    {
        private readonly SimpleContainer _container = new SimpleContainer();
        public StartClass()
        {
            Initialize();
            
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


        protected override async void OnStartup(object sender, StartupEventArgs e)
        {
            try
            {

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
