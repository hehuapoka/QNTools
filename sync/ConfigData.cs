using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace sync
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
        public string[] Format
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
        public string ProjectMapDrive { get; set; }
        public string ShotCache { get; set; }


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
        private static string[] cache_type_name = { "Anims", "CFX", "VFX" };

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
        public static string GetServerPath(ConfigData proj,string path)
        {
            return Path.Combine(proj.ProjectPath.Replace("/","\\"),path.Replace("/","\\"));
        }
        public static string GetLocalPath(ConfigData proj, string path)
        {
            return Path.Combine(proj.ProjectMapDrive,proj.Name.Replace("/", "\\"), path.Replace("/", "\\"));
        }

        public static string GetServerCachePath(ConfigData proj, string sc,string shot,int type)
        {
            return Path.Combine(proj.ProjectPath.Replace("/", "\\"), proj.ShotCache.Replace("/","\\"),sc,shot,cache_type_name[type]);
        }
        public static string GetLocalCachePath(ConfigData proj, string sc, string shot, int type)
        {
            return Path.Combine(proj.ProjectMapDrive.Replace("/", "\\"), proj.Name.Replace("/", "\\"), proj.ShotCache.Replace("/", "\\"), sc, shot, cache_type_name[type]);
        }

    }
}
