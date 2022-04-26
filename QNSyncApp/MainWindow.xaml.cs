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
using System.ServiceProcess;


namespace QNSyncApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Asset asset_page;
        Shot shot_page;

        public MainWindow()
        {
            InitializeComponent();

            



            asset_page = new Asset();
            shot_page = new Shot();
            mainframe.Content = asset_page;




            this.DataContext = new ConfigData();

            asset_page.TexAsset.ItemsSource = ((ConfigData)this.DataContext).TexAsset;
            asset_page.RigAsset.ItemsSource = ((ConfigData)this.DataContext).RigAsset;
            asset_page.HairAsset.ItemsSource = ((ConfigData)this.DataContext).HairAsset;

            shot_page.shotview.ItemsSource =  ((ConfigData)this.DataContext).ShotList;
        }

        private void SyncTask(object sender, RoutedEventArgs e)
        {
            //foreach( var i in ((ConfigData)this.DataContext).TexAsset)
            //{
            //    Console.WriteLine($"Name:{i.Name}  Sync:{i.Sync}");
            //}
           ServiceController[] services =  ServiceController.GetServices();
           foreach(ServiceController i in services)
            {
                Console.WriteLine(i.ServiceName);
            }
        }

        private void switchPage(object sender, RoutedEventArgs e)
        {
            if(this.mainframe.Content.GetType() == asset_page.GetType())
            {
                mainframe.Content = shot_page;
            }
            else
            {
                mainframe.Content = asset_page;
            }
        }
    }
}
