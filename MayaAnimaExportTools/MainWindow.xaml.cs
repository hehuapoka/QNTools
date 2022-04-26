using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MayaAnimaExportTools;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
//using Microsoft.JScript;
namespace MayaAnimaExportTools
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Commands cmd_c;
        public MainWindow()
        {
            InitializeComponent();
            string old_path = Environment.GetEnvironmentVariable("path");
            Environment.SetEnvironmentVariable("path", old_path + ";" + @"C:\Program Files\Autodesk\Maya2020\bin");

            //
            cmd_c = new Commands();
            //cmd_c.GetOutline += new Commands.CommandsDelegate(getOutlineCallback);
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            button.IsEnabled = false;
            await cmd_c.run();
            await setListData();
            button.IsEnabled = true;
        }
        async public Task setListData()
        {
            await Task.Delay(0);
            try
            {
                if(File.Exists(Directory.GetCurrentDirectory() + "\\qn.json"))
                {
                    StreamReader txt =File.OpenText(Directory.GetCurrentDirectory() + "\\qn.json");
                    //Console.WriteLine(txt.ReadToEnd());
                    JArray data = JsonConvert.DeserializeObject<JArray>(txt.ReadToEnd());
                    //Console.WriteLine(data[0].ToString());
                    List<string> old_tree=new List<string>();
                    //foreach(JValue data_single in data)
                    //{
                    //    string[] data_split = data_single.ToString().Split('/');
                    //    foreach(string data_split_single in data_split)
                    //    {
                    //        if(old_tree.BinarySearch(data_split_single) < 0 )
                    //        {
                    //            treeView.has
                    //        }
                    //    }

                    //}
                    var coll = new TreeViewItem();
                    coll.
                    
                }
            }
            catch
            {
                return;
            }
        }

        //public void getOutlineCallback()
        //{
        //    MessageBox.Show("hello world");
        //    button.IsEnabled = true;
        //}
    }
}
