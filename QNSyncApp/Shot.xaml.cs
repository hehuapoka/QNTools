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

namespace QNSyncApp
{
    /// <summary>
    /// Shot.xaml 的交互逻辑
    /// </summary>
    public partial class Shot : Page
    {
        public Shot()
        {
            InitializeComponent();
        }

        //private void shotview_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        //{
        //    var sel_item=(ShotTreeItem)shotview.SelectedItem;
        //    anims.IsChecked = sel_item.SyncAnims;
        //    cfx.IsChecked = sel_item.SyncCFX;
        //    vfx.IsChecked = sel_item.SyncVFX;
        //}
    }
}
