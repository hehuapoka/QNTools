using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace QN_Make
{
    public class c_data
    {
        public string name;
        public string path;
        public string incolor = "Utility - Raw";
        public string outcolor = "ACES - ACEScg";
        public string format = "tx";
    }
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private int formatall = 0;
        public List<string> use_data;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void button_Click(object sender, MouseButtonEventArgs e)
        {
            listBox.Items.Clear();

            //生成ListWorkItems
            string[] images = c_get_images();
            foreach (string image in images)
            {
                if (Regex.Match(image, @"^.+\.(tif|jpg|png|exr|tiff|hdr)$", RegexOptions.IgnoreCase) == Match.Empty)
                    continue;
                var image_info = new FileInfo(image);
                ListBoxItem c_item = new ListBoxItem();
                c_item.Content = image_info.Name;
                c_item.DataContext = new c_data();
                //init data
                if (Regex.Match(image, @"^.+[_\.]\w{0,20}color[_\.].+$", RegexOptions.IgnoreCase) != Match.Empty)
                {
                    c_item.Foreground = Brushes.Blue;
                    init_data(c_item, image_info.Name, image_info.FullName, "Utility - sRGB - Texture");
                }
                else if (Regex.Match(image, @"^.+[_\.]emission[_\.].+$", RegexOptions.IgnoreCase) != Match.Empty)
                {
                    c_item.Foreground = Brushes.Blue;
                    init_data(c_item, image_info.Name, image_info.FullName, "Utility - sRGB - Texture");
                }
                else
                {
                    c_item.Foreground = Brushes.SlateGray;
                    init_data(c_item, image_info.Name, image_info.FullName, "Utility - Raw");
                }

                listBox.Items.Add(c_item);
            }
        }
        private void changedText(object sender, TextChangedEventArgs e)
        {
            listBox.Items.Clear();

            //生成ListWorkItems
            string[] images = e_get_images();
            foreach (string image in images)
            {
                if (Regex.Match(image, @"^.+\.(tif|jpg|png|exr|tiff|hdr|tga)$", RegexOptions.IgnoreCase) == Match.Empty)
                    continue;
                var image_info = new FileInfo(image);
                ListBoxItem c_item = new ListBoxItem();
                c_item.Content = image_info.Name;
                c_item.DataContext = new c_data();
                //init data
                if (Regex.Match(image, @"^.+[_\.]\w{0,20}color[_\.].+$", RegexOptions.IgnoreCase) != Match.Empty)
                {
                    c_item.Foreground = Brushes.Blue;
                    init_data(c_item, image_info.Name, image_info.FullName, "Utility - sRGB - Texture");
                }
                else if (Regex.Match(image, @"^.+[_\.]emission[_\.].+$", RegexOptions.IgnoreCase) != Match.Empty)
                {
                    c_item.Foreground = Brushes.Blue;
                    init_data(c_item, image_info.Name, image_info.FullName, "Utility - sRGB - Texture");
                }
                else
                {
                    c_item.Foreground = Brushes.SlateGray;
                    init_data(c_item, image_info.Name, image_info.FullName, "Utility - Raw");
                }

                listBox.Items.Add(c_item);
            }

        }
        //private void button_Click(object sender, RoutedEventArgs e)
        //{
        //    listBox.Items.Clear();

        //    //生成ListWorkItems
        //    string[] images=c_get_images();
        //    foreach(string image in images)
        //    {
        //        if (Regex.Match(image, @"^.+\.(tif|jpg|png|exr|tiff|hdr)$", RegexOptions.IgnoreCase) == Match.Empty)
        //            continue;
        //        var image_info = new FileInfo(image);
        //        ListBoxItem c_item = new ListBoxItem();
        //        c_item.Content = image_info.Name;
        //        c_item.DataContext = new c_data();
        //        //init data
        //        if (Regex.Match(image,@"^.+[_\.]\w{0,20}color[_\.].+$", RegexOptions.IgnoreCase) != Match.Empty)
        //        {
        //            c_item.Foreground = Brushes.Blue;
        //            init_data(c_item, image_info.Name,image_info.FullName, "Utility - sRGB - Texture");
        //        }
        //        else if(Regex.Match(image, @"^.+[_\.]emission[_\.].+$", RegexOptions.IgnoreCase) != Match.Empty)
        //        {
        //            c_item.Foreground = Brushes.Blue;
        //            init_data(c_item, image_info.Name, image_info.FullName, "Utility - sRGB - Texture");
        //        }
        //        else
        //        {
        //            c_item.Foreground = Brushes.SlateGray;
        //            init_data(c_item, image_info.Name, image_info.FullName, "Utility - Raw");
        //        }

        //        listBox.Items.Add(c_item);
        //    }

        //}

        public string[] c_get_images()
        {
            var win = new System.Windows.Forms.FolderBrowserDialog();
            win.Description = "选择图片文件夹";
            //win.RootFolder = Environment.SpecialFolder.Personal;
            win.ShowDialog();
            if (win.SelectedPath == string.Empty)
            {
                string[] d = { };
                return d;
            }
            try
            {
                string[] images = Directory.GetFiles(win.SelectedPath);
                //button.Content = win.SelectedPath;
                textBox.Text = win.SelectedPath;
                //Console.WriteLine($"{win.SelectedPath} has images: {images}");
                return images;
            }
            catch
            {
                string[] d = { };
                return d;
            }
            
            
        }
        public string[] e_get_images()
        {
            if(!Directory.Exists(textBox.Text))
            {
                string[] d = { };
                return d;

            }
            try
            {
                string[] images = Directory.GetFiles(textBox.Text);
                return images;
            }
            catch
            {
                string[] d = { };
                return d;
            }


        }
        public void init_data(FrameworkElement e,string name,string path,string incolor= "Utility - Raw",string outcolor = "ACES - ACEScg",string format="exr")
        {
            c_data cData = (c_data)e.DataContext;
            cData.name = name;
            cData.path = path;
            cData.incolor = incolor;
            cData.outcolor = outcolor;
            cData.format = format;

        }

        private void sel_changed_lb(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBoxItem selItem = (ListBoxItem)listBox.SelectedItem;
                c_data selItemData = (c_data)selItem.DataContext;
                //set colorspace
                if (selItemData.incolor == "Utility - sRGB - Texture")
                {
                    colorspace.SelectedIndex = 0;
                }
                else if (selItemData.incolor == "Utility - Raw")
                {
                    colorspace.SelectedIndex = 1;
                }
                else
                {
                    colorspace.SelectedIndex = 2;
                }
                //set format
                if (selItemData.format == "tx")
                {
                    //Console.WriteLine("cc");
                    fileformat.SelectedIndex = 0;
                }
                else
                {
                    fileformat.SelectedIndex = 1;
                }

                info.Content = "信息：" + selItemData.path + $"    |    colorspace:{selItemData.incolor}    |    format:{selItemData.format}";
            }
            catch
            {
                return;
            }
            
        }

        private void colorspaceChange(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem selItem = (ListBoxItem)listBox.SelectedItem;
            if (selItem == null) return;
            c_data selItemData = (c_data)selItem.DataContext;

            if (colorspace.SelectedIndex == 0)
            {
                selItemData.incolor = "Utility - sRGB - Texture";
                selItem.Foreground = Brushes.Blue;
            }
            else if (colorspace.SelectedIndex == 1)
            {
                selItem.Foreground = Brushes.SlateGray;
                selItemData.incolor = "Utility - Raw";
            }
            else
            {
                selItem.Foreground = Brushes.SpringGreen;
                selItemData.incolor = "Utility - Linear - sRGB";
            }
        }

        private void formatChange(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem selItem = (ListBoxItem)listBox.SelectedItem;
            if (selItem == null) return;
            c_data selItemData = (c_data)selItem.DataContext;

            if (((ComboBox)sender).SelectedIndex == 0)
            {
                selItemData.format = "tx";
            }
            else
            {
                selItemData.format = "exr";
            }
        }
        public List<string> get_commands(string in_dir)
        {
            List<string> a = new List<string>();
            foreach (ListBoxItem item_get in listBox.Items)
            {
                c_data c_d = (c_data)item_get.DataContext;
                string out_dir = in_dir + System.IO.Path.GetFileNameWithoutExtension(c_d.path) +"."+ c_d.format;
                string c_cmd = $".\\openimageio\\maketx {c_d.path} -u -v --compression DWAA:100 --colorconfig .\\aces_1.2\\config.ocio --colorconvert \"{c_d.incolor}\" \"{c_d.outcolor}\" --oiio -o {out_dir}";
                a.Add(c_cmd);
            }
            return a;
        }
        private void runTaskClick(object sender, RoutedEventArgs e)
        {
            if(listBox.Items.Count < 1)
            {
                return;
            }
            DirectoryInfo d_info = new DirectoryInfo(textBox.Text+"\\Image_aces\\");
            if (!d_info.Exists)
            {
                Directory.CreateDirectory(d_info.FullName);
            }

            use_data=get_commands(d_info.FullName);
            process p_win = new process();

            p_win.Owner = this;
            p_win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            p_win.ShowDialog();
            

        }

        private void swithTxEXR(object sender, RoutedEventArgs e)
        {
            if(formatall == 0)
            {
                foreach (ListBoxItem item_get in listBox.Items)
                {
                    c_data c_d = (c_data)item_get.DataContext;
                    c_d.format = "exr";
                }
                formatall = 1;
            }
            else
            {
                foreach (ListBoxItem item_get in listBox.Items)
                {
                    c_data c_d = (c_data)item_get.DataContext;
                    c_d.format = "tx";
                }
                formatall = 0;
            }
        }

        
    }
    
}
