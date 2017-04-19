using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using PPOK_Refill_System_Service;

namespace PPOK_Refill_System_WCF_Host
{
    public partial class PPOK_System_Service : ServiceBase
    {
        // hold the service host class
        private ServiceHost _host;

        public ServiceHost Host {
            get
            {
                return _host ?? new ServiceHost(typeof(PPOK_Refill_System_Service.PPOK_Refill_System_Service));
            }
            private set
            {
                _host = value;
            }
        }

        public PPOK_System_Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Host.Open();
        }

        protected override void OnStop()
        {
            if (Host != null)
            {
                Host.Close();
            }
        }
    }
}
