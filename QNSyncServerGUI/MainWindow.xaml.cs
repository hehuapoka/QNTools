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
        public MainWindow()
        {
            InitializeComponent();

            project_all = new ProjectAll();

            this.cureen_project.ItemsSource = project_all.project_list;

            changeProject(0);
            icon();
        }

        private void icon()
        {
            System.Windows.Forms.MenuItem open_item = new System.Windows.Forms.MenuItem("打开主页面");
            open_item.Click += new EventHandler((o, e) => { this.WindowState=WindowState.Normal;this.Show(); });

            System.Windows.Forms.MenuItem sync_item = new System.Windows.Forms.MenuItem("同步");

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


        }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            project_all.WriteConfig();
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
                
        }
    }
}
