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
using Microsoft.Win32;

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

        static void AutoStartSync(bool ac,string value)
        {
            bool exist_registry=false;
            string name = "QNSyncServerGUI";
            RegistryKey a = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run",true);
            foreach (var k in a.GetValueNames())
            {
                if(k == name)
                {
                    exist_registry = true;
                    break;
                } 
            }
            //创建注册表
            if(!exist_registry)
            {
                a.SetValue(name, "",RegistryValueKind.String);
            }
            if (ac)
                a.SetValue(name, value, RegistryValueKind.String);
            else
                a.DeleteValue(name);

            a.Close();

            Console.WriteLine("环境变量设置完成!");


        }
        static void Main(string[] args)
        {

            AutoStartSync(true,$"\"{System.IO.Path.Combine(Directory.GetCurrentDirectory(), "QNSyncServerGUI.exe")}\" --cmd=autorun");
            Console.ReadKey();

        }
       
    }
}
