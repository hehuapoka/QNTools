using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace QNSyncServerGUI
{
    public static class Tools
    {
        public static void AutoStartSync(bool ac, string value)
        {
            bool exist_registry = false;
            string name = "QNSyncServerGUI";
            RegistryKey a = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            foreach (var k in a.GetValueNames())
            {
                if (k == name)
                {
                    exist_registry = true;
                    break;
                }
            }
            //创建注册表
            if (!exist_registry)
            {
                a.SetValue(name, "", RegistryValueKind.String);
            }
            if (ac)
                a.SetValue(name, $"\"{System.IO.Path.Combine(value, "QNSyncServerGUI.exe")}\" --cmd=autorun", RegistryValueKind.String);
            else
                a.SetValue(name, "", RegistryValueKind.String);

            //Console.WriteLine(a.GetValue(name).ToString());


        }
    }
}
