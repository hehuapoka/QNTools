using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace QNSyncServer
{
    public class ItemSingle
    {
        public string Name
        {
            get;
            set;
        }
        public bool Sync
        {
            get;
            set;
        }
        public string Path
        {
            get;
            set;
        }
    }
    public class ShotTreeItem
    {
        public string Name
        {
            get; 
            set; 
        }
        public string Icon
        {
            get;
            set;
        }

        //
        public bool SyncAnims
        {
            get;
            set;
        }
        public bool SyncCFX
        {
            get;
            set;
        }
        public bool SyncVFX
        {
            get;
            set;
        }
        public List<ShotTreeItem> Children
        {
            get;
            set;
        }
        public ShotTreeItem()
        {
            Children = new List<ShotTreeItem>();
        }
    }
    public class ConfigData
    {
        public string Name { get; }
        public string ProjectPath { get; set; }
        public bool Sync { get; set; }
        public double Day { get; set; }
        public double Hours { get; set; }
        public double Mintus { get; set; }
        public List<ItemSingle> TexAsset { get; set; }

        public List<ItemSingle> RigAsset { get; set; }

        public List<ItemSingle> HairAsset { get; set; }

        public List<ShotTreeItem> ShotList { get; set; }
        public ConfigData(string name)
        {
            Name = name;
            Sync = false;
            Day = 1.0;
            Hours = 0.0;
            Mintus = 0.0;
        }
    }
    public class ProjectAll
    {
        public List<ConfigData> project_list;
        public ProjectAll()
        {
            if(File.Exists("app_config.json"))
                ReadConfig();
        }

        public void ReadConfig()
        {
            project_list = JsonConvert.DeserializeObject<List<ConfigData>>(File.ReadAllText("app_config.json"));
        }

    }
}
