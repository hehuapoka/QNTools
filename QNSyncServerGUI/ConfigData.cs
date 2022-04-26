using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace QNSyncServerGUI
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

            //TexAsset = new List<ItemSingle>();
            //TexAsset.Add(new ItemSingle { Name = "Maya资产", Sync = false });
            //TexAsset.Add(new ItemSingle { Name = "MatX资产", Sync = false });
            //TexAsset.Add(new ItemSingle { Name = "USD资产", Sync = false });
            //TexAsset.Add(new ItemSingle { Name = "贴图资产", Sync = false });

            //RigAsset = new List<ItemSingle>();
            //RigAsset.Add(new ItemSingle { Name = "Maya资产", Sync = false });
            //RigAsset.Add(new ItemSingle { Name = "贴图资产", Sync = false });

            //HairAsset = new List<ItemSingle>();
            //HairAsset.Add(new ItemSingle { Name = "Maya资产", Sync = false });
            //HairAsset.Add(new ItemSingle { Name = "MatX资产", Sync = false });
            //HairAsset.Add(new ItemSingle { Name = "贴图资产", Sync = false });

            //ShotList = new List<ShotTreeItem>();

            //ShotTreeItem sc01 = new ShotTreeItem { Name="SC01",Icon="./Images/sc.png"};
            //ShotTreeItem sc02 = new ShotTreeItem { Name="SC02",SyncAnims=true, Icon = "./Images/sc.png" };


            //ShotTreeItem sc01_shot01 = new ShotTreeItem() { Name="Shot01", Icon = "./Images/shot.png" };
            //ShotTreeItem sc01_shot02 = new ShotTreeItem() { Name="Shot02", Icon = "./Images/shot.png" };
            //sc01.Children.Add(sc01_shot01);
            //sc01.Children.Add(sc01_shot02);

            //ShotTreeItem sc02_shot01 = new ShotTreeItem() { Name="Shot01", Icon = "./Images/shot.png" };
            //ShotTreeItem sc02_shot02 = new ShotTreeItem() { Name="Shot02", Icon = "./Images/shot.png" };
            //sc02.Children.Add(sc02_shot01);
            //sc02.Children.Add(sc02_shot02);

            //ShotList.Add(sc01);
            //ShotList.Add(sc02);

        }
    }
    public class ProjectAll
    {
        public List<ConfigData> project_list;
        public ProjectAll()
        {
            InitData();
        }

        private void InitData()
        {
            project_list = new List<ConfigData>();

            if (File.Exists("app_config.json"))
            {
                ReadConfig();
            }
            else
            {
                ReadConfigFromWeb();
            }


        }
        public void ReadConfigFromWeb()
        {
            if (!File.Exists("config.json"))
                return;
            dynamic a = JsonConvert.DeserializeObject(File.ReadAllText("config.json"));
            foreach(dynamic pr in a)
            {
                ConfigData project = new ConfigData(pr.Name);
                project.ProjectPath = pr.Value.PROJECT_PATH.ToString();

                List<ItemSingle> TexAsset = new List<ItemSingle>();
                List<ItemSingle> RigAsset = new List<ItemSingle>();
                List<ItemSingle> HairAsset = new List<ItemSingle>();
                List<ShotTreeItem> ShotList = new List<ShotTreeItem>();

                foreach (dynamic asset in pr.Value.ASSETS)
                {
                    ItemSingle ite = new ItemSingle() { Name = asset.Name, Sync = false,Path= asset.Value.ToString() };
                   
                    string prefix = ite.Name.Split('_')[0];
                    
                    if (prefix == "TEX")
                        TexAsset.Add(ite);
                    else if (prefix == "RIG")
                        RigAsset.Add(ite);
                    else if (prefix == "HAIR")
                        HairAsset.Add(ite);
                    //Console.WriteLine(ite.Name);
                }

                foreach (dynamic sc in pr.Value.SHOT_TABEL)
                {
                    ShotTreeItem sc_item = new ShotTreeItem { Name = sc.Name, Icon = "./Images/sc.png" };
                    foreach(dynamic shot in sc.Value.Properties())
                    {
                        ShotTreeItem shot_item = new ShotTreeItem { Name = shot.Name, Icon = "./Images/shot.png" };
                        //Console.WriteLine(shot.Name);
                        sc_item.Children.Add(shot_item);
                    }

                    ShotList.Add(sc_item);
                    
                }


                project.TexAsset = TexAsset;
                project.RigAsset = RigAsset;
                project.HairAsset = HairAsset;
                project.ShotList = ShotList;

                project_list.Add(project);
            }
        }
        public void WriteConfig()
        {
            var a=JsonConvert.SerializeObject(project_list);

            File.WriteAllText("app_config.json", a);
        }
        public void ReadConfig()
        {
            project_list = JsonConvert.DeserializeObject<List<ConfigData>>(File.ReadAllText("app_config.json"));
        }

    }
}
