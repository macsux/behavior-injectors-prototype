using System;
using System.ServiceProcess;

namespace TestService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Console.WriteLine("Started");
        }

        protected override void OnStop()
        {
            Console.WriteLine("Stopped");
        }
    }
}