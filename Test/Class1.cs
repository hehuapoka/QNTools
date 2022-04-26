using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Encodings;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.IO;

namespace Test
{
    public class UsdAssetInfo
    {
        public string name;
        public string path;
        public List<string> images;
    }
    class Class1
    {
        static void Main(string[] args)
        {
            //var bb = new List<Person> ();

            //Person a = new Person();
            //a.Name = "alina";
            //a.age = 21;
            //a.ak = new float[]{ 13.5f,12.5f};
            //bb.Add(a);

            //Person b = new Person();
            //b.Name = "hehua";
            //b.age = 26;
            //b.ak = new float[] { 13.5f, 12.5f };
            //bb.Add(b);
            //string outvalue = JsonConvert.SerializeObject(bb);
            //Console.WriteLine(outvalue);
            writejsondata();
            Console.ReadKey();
        }
        static public void writejsondata()
        {
            List<string> asset_info;
            string project_root = Environment.GetEnvironmentVariable("PROJECT_ROOT");
            if (project_root == String.Empty) return;
            string asset_root = Path.Combine(project_root.Replace("/",@"\"), @"AssetsRoot\USDAssets\Assets");
            if (!Directory.Exists(asset_root)) return;
            foreach (var asset_dir in Directory.EnumerateDirectories(asset_root))
            {
                foreach(var asset_file in Directory.EnumerateFiles(asset_dir))
                {
                    if(Regex.Match(asset_file, @".+_Assets\.usd$") != Match.Empty)
                    {
                        var info = new UsdAssetInfo();
                        info.name = Path.GetFileName(asset_file);
                        info.path = asset_file;

                        var image_dir = Path.Combine(asset_dir, "Images");
                        if(Directory.Exists(image_dir))
                        foreach(var image_file in Directory.EnumerateFiles(image_dir))
                        {
                                if (Regex.Match(image_file, @".+\.png$") != Match.Empty)
                                {
                                    info.images.Add(image_file);
                                }
                        }
                    }
                }
                //Console.WriteLine(asset_dir);
            }
        }
    }
}
