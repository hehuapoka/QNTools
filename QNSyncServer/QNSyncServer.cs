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
using System.Threading;
using System.IO;

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

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            timer1 = new System.Timers.Timer();
            timer1.Interval = 1000*60;
            timer1.Elapsed += new ElapsedEventHandler(this.OnTimer);
            timer1.Start();
        }


        protected override void OnStart(string[] args)
        {
            
            eventLog1.WriteEntry($"开始服务...{File.Exists("app_config.json")}");
            Sync.Run();

        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("同步服务停止");
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            eventLog1.WriteEntry($"同步次数{eventId++}");
            Sync.Run();
        }
    }
}
