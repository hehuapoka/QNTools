using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MayaAnimaExportTools
{
    class Commands
    {
        //public delegate void CommandsDelegate();
        //public event CommandsDelegate GetOutline;
        //async public Task run()
        //{
        //    await Task.Delay(0);
        //    Process p = Process.Start(@"mayapy.exe", "listOutline.py");
        //    p.WaitForExit();
        //    p.Close();
        //    GetOutline();
        //}
        async public Task run()
        {
            await Task.Delay(0);
            Process p = Process.Start(@"mayapy.exe", "listOutline.py");
            p.WaitForExit();
            p.Close();
            //GetOutline();
        }
    }
}
