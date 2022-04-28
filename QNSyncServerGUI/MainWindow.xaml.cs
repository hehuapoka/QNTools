using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace QNSyncServerGUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ProjectAll project_all;
        System.Windows.Forms.NotifyIcon notifyIcon;
        System.Windows.Forms.MenuItem[] menuItems;

        DispatcherTimer timer;
        bool syncing = false;

        public int progressValue { set; get; } = 10;
        public MainWindow()
        {
            InitializeComponent();

            project_all = new ProjectAll();

            this.cureen_project.ItemsSource = project_all.project_list;
            changeProject(0);
            InitNotify();
            
        }

        private void InitNotify()
        {
            System.Windows.Forms.MenuItem open_item = new System.Windows.Forms.MenuItem("打开主页面");
            open_item.Click += new EventHandler((o, e) => { this.WindowState=WindowState.Normal;this.Show(); });

            System.Windows.Forms.MenuItem sync_item = new System.Windows.Forms.MenuItem("同步");
            sync_item.Click += new EventHandler(SyncBackground);

            System.Windows.Forms.MenuItem close_item = new System.Windows.Forms.MenuItem("退出");
            close_item.Click += new EventHandler((o, e) => { this.Close(); });

            menuItems = new System.Windows.Forms.MenuItem[] { open_item, sync_item, close_item };


            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(menuItems);
            notifyIcon.BalloonTipText = "千鸟文件同步启动";
            notifyIcon.Text = "千鸟文件同步";
            notifyIcon.Icon = new System.Drawing.Icon("sync.ico");
            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += OnNotifyIconDoubleClick;
            notifyIcon.ShowBalloonTip(1000);

            InitTimer();


        }
        private void InitTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += timeEventUpdate;
            resetTimer();
            //timer.Start();
        }
        private TimeSpan TimeSpanMax(TimeSpan a,TimeSpan b)
        {
            if (a > b) return a;
            return b;
        }
        private void resetTimer()
        {
            //Math.Max(TimeSpan.FromMinutes(10), TimeSpan.FromDays(m_day_s.Value) + TimeSpan.FromHours(m_hours_s.Value) + TimeSpan.FromMinutes(m_mintus_s.Value));
            timer.Interval = TimeSpanMax(TimeSpan.FromMinutes(10), TimeSpan.FromDays(m_day_s.Value) + TimeSpan.FromHours(m_hours_s.Value) + TimeSpan.FromMinutes(m_mintus_s.Value));
        }

        private async void  timeEventUpdate(object sender,EventArgs e)
        {
            if ((bool)autoUpdata.IsChecked && this.WindowState == WindowState.Minimized && syncing == false)
            {
                notifyIcon.BalloonTipText = "后台同步中...";
                notifyIcon.ShowBalloonTip(1000);
                await runTask();
            }
        }
        private async void SyncBackground(object sender, EventArgs e)
        {
            await runTask();
        }
        private async Task runTask()
        {
            syncing = true;
            var progress = new Progress<int>(percent =>
            {
                progressBar.Value = percent;
            }
            );
            await Task.Run(() => runSyncGetCount());

            int a = Sync.sync_tasks.Count;
            progressBar.Maximum = a;
            progressBar.Value = 0;
            if(a>0)
                await Task.Run(() => runSync(progress,a));
            Sync.sync_tasks.Clear();

            syncing = false;
        }

        private void runSyncGetCount()
        {
            Sync.GetAllTask(project_all);
        }
        private void runSync(IProgress<int> pbar,int CC)
        {
            int i = 1;
            foreach(var a in Sync.sync_tasks)
            {
                Sync.ProcessTask(a);
                i++;
                pbar.Report(i);
            }
        }

        //程序自启动





        private void OnNotifyIconDoubleClick(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Normal;
            this.Show();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Hide();

                notifyIcon.BalloonTipText = "千鸟文件同步最小化";
                notifyIcon.ShowBalloonTip(1000);
                timer.Start();
                resetTimer();

            }
            else
            {
                timer.Stop();
            }
        }

        private void cureen_project_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int a = project_all.project_list.IndexOf((ConfigData)cureen_project.SelectedValue);
            if(a>=0 && a< project_all.project_list.Count)
                changeProject(a);
        }
        private void changeProject(int index)
        {
            this.DataContext = project_all.project_list[index];
            TexAsset.ItemsSource = ((ConfigData)this.DataContext).TexAsset;
            RigAsset.ItemsSource = ((ConfigData)this.DataContext).RigAsset;
            HairAsset.ItemsSource = ((ConfigData)this.DataContext).HairAsset;

            shotview.ItemsSource = ((ConfigData)this.DataContext).ShotList;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            mainButton.IsEnabled = false;
            if(!syncing)
            {
                await runTask();
            }
            else
            {
                MessageBox.Show("任务在后台运行中");
            }
            //Console.WriteLine(project_all.project_list[0].Sync);
            mainButton.IsEnabled = true;
        }

        private void anims_Click(object sender, RoutedEventArgs e)
        {
            ShotTreeItem item = (ShotTreeItem)shotview.SelectedItem;
            if("ShotTreeItem".Equals(item.GetType().Name))
            {
                foreach(ShotTreeItem i in item.Children)
                {
                    i.SyncAnims = item.SyncAnims;
                }
            }
        }

        private void cfx_Checked(object sender, RoutedEventArgs e)
        {
            ShotTreeItem item = (ShotTreeItem)shotview.SelectedItem;
            if ("ShotTreeItem".Equals(item.GetType().Name))
            {
                foreach (ShotTreeItem i in item.Children)
                {
                    i.SyncCFX = item.SyncCFX;
                }
            }
        }

        private void vfx_Checked(object sender, RoutedEventArgs e)
        {
            ShotTreeItem item = (ShotTreeItem)shotview.SelectedItem;
            if ("ShotTreeItem".Equals(item.GetType().Name))
            {
                foreach (ShotTreeItem i in item.Children)
                {
                    i.SyncVFX = item.SyncVFX;
                }
            }
        }

        private void close_Window(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult a = MessageBox.Show("你需要最小化窗口吗？", "关闭提示", MessageBoxButton.YesNo);
            if(a == MessageBoxResult.Yes)
            {
                WindowState = WindowState.Minimized;
                e.Cancel = true;   
            }
            else
            {
                project_all.WriteConfig();
            }
                
        }
    }
}
