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
        public MainWindow()
        {
            InitializeComponent();

            project_all = new ProjectAll();
            this.cureen_project.ItemsSource = project_all.project_list;

            changeProject(0);
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
    }
}
