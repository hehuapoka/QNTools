using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace QNSyncServer
{
    public partial class QNSyncServer : ServiceBase
    {
        private System.Timers.Timer timer1;

        private int eventId = 1;
        public QNSyncServer()
        {
            InitializeComponent();
            eventLog1 = new System.Diagnostics.EventLog();
            if(!System.Diagnostics.EventLog.SourceExists("QNSyncSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource("QNSyncSource", "QNSyncLog");
            }
            eventLog1.Source = "QNSyncSource";
            eventLog1.Log = "QNSyncLog";

            timer1 = new Timer();
            timer1.Interval = 6000;
            timer1.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer1.Start();
        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Server is starting");
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Server is Stop");
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);
        }
    }
}
