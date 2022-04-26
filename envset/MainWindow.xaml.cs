using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace envset
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void button_Click(object sender, RoutedEventArgs e)
        {
            //await Task.Run(()=> sprocess());
            button.IsEnabled = false;
            await sprocess();
            button.IsEnabled = true;
        }
        public async Task sprocess()
        {
            await Task.Delay(0);
            try
            {
                Environment.SetEnvironmentVariable("PROJECT_ROOT", textBox.Text.Replace(@"\","/"), EnvironmentVariableTarget.User);
                Environment.SetEnvironmentVariable("OCIO", textBox1.Text.Replace(@"\", "/"), EnvironmentVariableTarget.User);
            }
            catch
            {
                System.Windows.MessageBox.Show("设置失败");
                return;

            }

            System.Windows.MessageBox.Show("设置成功");
        }

        private void doubleclick_projectroot(object sender, MouseButtonEventArgs e)
        {
            var a = new FolderBrowserDialog();
            a.Description = "选择文件夹";
            //win.RootFolder = Environment.SpecialFolder.Personal;
            a.ShowDialog();
            if (a.SelectedPath == string.Empty) return;
            textBox.Text = a.SelectedPath.Replace("\\", "/");
        }

        private void doubleclick_ocio(object sender, MouseButtonEventArgs e)
        {
            var a = new FolderBrowserDialog();
            a.Description = "选择文件夹";
            //win.RootFolder = Environment.SpecialFolder.Personal;
            a.ShowDialog();
            if (a.SelectedPath == string.Empty) return;
            textBox1.Text = a.SelectedPath.Replace("\\", "/");
        }
    }
}
