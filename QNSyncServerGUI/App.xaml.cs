using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace QNSyncServerGUI
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {


        private System.Threading.Mutex mutex;

        public App()
        {
            this.Startup += new StartupEventHandler(App_Startup);
            
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            bool ret;
            mutex = new System.Threading.Mutex(true, "QNSyncServerGUI", out ret);

            if (!ret)
            {
                MessageBox.Show("程序已打开", "", MessageBoxButton.OK, MessageBoxImage.Stop);
                Environment.Exit(0);
            }
        }



    }
}
