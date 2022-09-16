using log4net;
using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using log4net.Config;
using System.Configuration;
using System.Threading;

namespace WinServiceDemo
{
    public partial class ServiceDemo : ServiceBase
    {
        private readonly ILog _logWriter = LogManager.GetLogger(typeof(ServiceDemo));
        private readonly ManualResetEvent _shutdownEvent = new ManualResetEvent(false);

        private readonly string _environment;
        private readonly int _shutdownInterval = 60000;
        private readonly int _runInterval = 1000;

        public ServiceDemo()
        {
            InitializeComponent();
            XmlConfigurator.Configure();

            if (ConfigurationManager.AppSettings["Environment"] != null)
            {
                _environment = ConfigurationManager.AppSettings["Environment"];
            }
        }

        internal void StartService(string[] args)
        {
            OnStart(args);
        }

        internal void StopService()
        {
            OnStop();
        }

        protected override void OnStart(string[] args)
        {
            //OnStart(args);
            _logWriter.InfoFormat("GFile Logging Service Starting...");

            ParallelRun();

            _logWriter.InfoFormat("GFile Logging Service Started.");

        }

        private void ParallelRun()
        {
            //throw new NotImplementedException();
            Parallel.Invoke(
                              () =>
                                 {
                                     Task taskA = Task.Factory.StartNew(() => new ServiceDemo().taskWriteLogs());
                                     taskA.Wait(_runInterval);
                                 }
                             ); //end of parallel invoke
        }

        protected void taskWriteLogs()
        {
            do
            {
                _logWriter.InfoFormat("Run time environment {0}", _environment);
            }
            while (!_shutdownEvent.WaitOne(_shutdownInterval));
        }

        protected override void OnStop()
        {
            //OnStop();
            _logWriter.InfoFormat("GFile Logging Service Stopping...");
            _logWriter.InfoFormat("GFile Logging Service Stopped.");
        }
    }
}
