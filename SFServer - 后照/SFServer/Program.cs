using System;
using System.ServiceProcess;

namespace SFServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			    { 
				    new TestService() 
			    };
            ServiceBase.Run(ServicesToRun);   
        }
    }
}
