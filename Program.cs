using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WinServiceDemo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            if (!Environment.UserInteractive)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] { new ServiceDemo() };
                ServiceBase.Run(ServicesToRun);
            }
            else
            {
                ServiceDemo service = new ServiceDemo();
                service.StartService(null);

                Console.ReadLine();
                service.StopService();
            }

            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new Service1()
            //};
            //ServiceBase.Run(ServicesToRun);
        }
    }
}
