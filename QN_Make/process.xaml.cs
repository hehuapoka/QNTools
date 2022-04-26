using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;

namespace QN_Make
{
    /// <summary>
    /// process.xaml 的交互逻辑
    /// </summary>
    public partial class process : Window
    {
        //public int usd_value = 0;
        public process()
        {
            InitializeComponent();
            progressbar.IsIndeterminate = false;
            //start_cmd();
        }
        private void closetask(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void RunCmds(IProgress<int> pbar,List<string> s_cmds)
        {
            int ct = 0;
            foreach (string s_cmd in s_cmds)
            {
                ProcessStartInfo processInfo = new ProcessStartInfo("cmd.exe", "/S /C" + s_cmd)
                {
                    CreateNoWindow = true,
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden,

                };

                Process process_task = new Process { StartInfo = processInfo };
                process_task.Start();
                process_task.WaitForExit();
                ct++;
                pbar.Report(ct);
            
            }
        }

        private async void StartRunTask(object sender, EventArgs e)
        {
            MainWindow win_main = (MainWindow)this.Owner;
            progressbar.Maximum = win_main.use_data.Count;

            var progress = new Progress<int>(percent => 
            {
                progressbar.Value = percent;
                label.Content = $"{percent}/{progressbar.Maximum}";
            }
            );
            await Task.Run(() => RunCmds(progress, win_main.use_data));
            this.Close();
        }
    }
}
