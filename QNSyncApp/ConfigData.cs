using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QNSyncApp
{
    class ItemSingle
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
    }
    class ShotTreeItem
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
    class ConfigData
    {
        public List<ItemSingle> TexAsset { get; set; }

        public List<ItemSingle> RigAsset { get; set; }

        public List<ItemSingle> HairAsset { get; set; }

        public List<ShotTreeItem> ShotList { get; set; }
        public ConfigData()
        {
            TexAsset = new List<ItemSingle>();
            TexAsset.Add(new ItemSingle { Name = "Maya资产", Sync = false });
            TexAsset.Add(new ItemSingle { Name = "MatX资产", Sync = false });
            TexAsset.Add(new ItemSingle { Name = "USD资产", Sync = false });
            TexAsset.Add(new ItemSingle { Name = "贴图资产", Sync = false });

            RigAsset = new List<ItemSingle>();
            RigAsset.Add(new ItemSingle { Name = "Maya资产", Sync = false });
            RigAsset.Add(new ItemSingle { Name = "贴图资产", Sync = false });

            HairAsset = new List<ItemSingle>();
            HairAsset.Add(new ItemSingle { Name = "Maya资产", Sync = false });
            HairAsset.Add(new ItemSingle { Name = "MatX资产", Sync = false });
            HairAsset.Add(new ItemSingle { Name = "贴图资产", Sync = false });

            ShotList = new List<ShotTreeItem>();

            ShotTreeItem sc01 = new ShotTreeItem { Name="SC01"};
            ShotTreeItem sc02 = new ShotTreeItem { Name="SC02",SyncAnims=true};


            ShotTreeItem sc01_shot01 = new ShotTreeItem() { Name="Shot01"};
            ShotTreeItem sc01_shot02 = new ShotTreeItem() { Name="Shot02"};
            sc01.Children.Add(sc01_shot01);
            sc01.Children.Add(sc01_shot02);

            ShotTreeItem sc02_shot01 = new ShotTreeItem() { Name="Shot01"};
            ShotTreeItem sc02_shot02 = new ShotTreeItem() { Name="Shot02"};
            sc02.Children.Add(sc02_shot01);
            sc02.Children.Add(sc02_shot02);

            ShotList.Add(sc01);
            ShotList.Add(sc02);

        }
    }
}
