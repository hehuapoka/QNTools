using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace sync
{
    class Program
    {
        static Dictionary<string,string> sync_tasks=new Dictionary<string, string>();
        static bool InList(string a,string[] alist)
        {
            foreach(var i in alist)
            {
                if(i.Equals(a))
                {
                    return true;
                }
            }
            return false;
        }
        static void SyncFile(string path,string[] filter_type,string server_dir,string local_dir)
        {

            string base_path = path.Replace(server_dir, local_dir,true,null);
            FileInfo info_server = new FileInfo(path);
            FileInfo info_local = new FileInfo(base_path);


            if (!InList(info_server.Extension, filter_type)) return;

            
            //情况1 本地没有文件
            if(info_local.Exists)
            {
                if (info_local.LastWriteTime >= info_server.LastWriteTime && info_local.Length == info_server.Length)
                {
                    //Console.WriteLine($"文件{info_local.FullName}已存在");
                    return;
                }       
            }
            //if (!Directory.Exists(info_local.DirectoryName)) Directory.CreateDirectory(info_local.DirectoryName);
            //File.Copy(info_server.FullName,info_local.FullName,true);

            //Console.WriteLine(info_local.FullName);
            //Console.WriteLine(info_server.FullName);
            sync_tasks[info_server.FullName] = info_local.FullName;
        }
        //同步Maya文件
        static void SyncDir(string path,string[] filter_list,string server_dir,string local_dir)
        {
            if (!Directory.Exists(path)) return;

            var tex_files = Directory.EnumerateFiles(path);
            foreach (string tex_file in tex_files)
            {
                SyncFile(tex_file, filter_list,server_dir, local_dir);
            }
            var tex_dirs = Directory.EnumerateDirectories(path);
            foreach (string tex_dir in tex_dirs)
            {
                if (tex_dir.Replace(path, "", true, null).Equals("\\his")) continue;
                SyncDir(tex_dir,filter_list,server_dir,local_dir);
            }
        }

        //同步Cache
        static void SyncCacheDir(string[] filter_list, string server_dir, string local_dir)
        {
            if (!Directory.Exists(server_dir)) return;

            var tex_files = Directory.EnumerateFiles(server_dir);
            foreach (string tex_file in tex_files)
            {
                SyncFile(tex_file, filter_list, server_dir, local_dir);
            }
        }


        static void SyncTexAssets(ProjectConfig project_info)
        {
            string[] filter_list0 = { ".ma", ".mb" };
            SyncDir(project_info.GetServerTexAssetMaya(), filter_list0, project_info.GetServerTexAssetMaya(), project_info.GetLocalTexAssetMaya());

            //maya abc
            string[] filter_list1 = { ".abc", "mtlx" };
            SyncDir(project_info.GetServerTexAssetMatX(), filter_list1, project_info.GetServerTexAssetMatX(), project_info.GetLocalTexAssetMatX());
            //maya usd
            string[] filter_list2 = { ".usd", ".usda" };
            SyncDir(project_info.GetServerTexAssetUSD(), filter_list2, project_info.GetServerTexAssetUSD(), project_info.GetLocalTexAssetUSD());
            //maya tex
            string[] filter_list3 = { ".tx" };
            SyncDir(project_info.GetServerTexAssetTex(), filter_list3, project_info.GetServerTexAssetTex(), project_info.GetLocalTexAssetTex());
        }



        //rig


        static void SyncRigAssets(ProjectConfig project_info)
        {
            string[] filter_list1 = { ".ma",".mb" };
            SyncDir(project_info.GetServerRigAssetMaya(), filter_list1, project_info.GetServerRigAssetMaya(), project_info.GetLocalRigAssetMaya());
            string[] filter_list2 = { ".tx",".png" };
            SyncDir(project_info.GetServerRigAssetTex(), filter_list2, project_info.GetServerRigAssetTex(), project_info.GetLocalRigAssetTex());
        }

        static void SyncHairAssets(ProjectConfig project_info)
        {
            string[] filter_list1 = { ".ma", ".mb" ,".xgen"};
            SyncDir(project_info.GetServerHairAssetMaya(), filter_list1, project_info.GetServerHairAssetMaya(), project_info.GetLocalHairAssetMaya());
            string[] filter_list2 = { ".tx"};
            SyncDir(project_info.GetServerHairAssetTex(), filter_list2, project_info.GetServerHairAssetTex(), project_info.GetLocalHairAssetTex());
            string[] filter_list3 = { ".mtlx" };
            SyncDir(project_info.GetServerHairAssetMatX(), filter_list3, project_info.GetServerHairAssetMatX(), project_info.GetLocalHairAssetMatX());
        }

        static void SyncShots(ProjectConfig project_info,string sc,string shot,CACHE_TYPE cache_type)
        {
            string[] filter_list1 = { ".abc"};
            SyncCacheDir(filter_list1, project_info.GetServerCacheAssetDir(sc, shot, cache_type), project_info.GetLocalCacheAssetDir(sc, shot, cache_type));
        }

        //开始任务
        static void ProcessTask(KeyValuePair<string,string> tas)
        {

            if (!Directory.Exists(Path.GetDirectoryName(tas.Value))) Directory.CreateDirectory(Path.GetDirectoryName(tas.Value));
            File.Copy(tas.Key, tas.Value, true);
            
        }
        static void Main(string[] args)
        {
            //ProjectConfig project_info = new ProjectConfig();
            //SyncTexAssets(project_info);
            //SyncRigAssets(project_info);
            //SyncHairAssets(project_info);
            //SyncShots(project_info, "SC001", "Shot0010",CACHE_TYPE.ANIMS);

            //Console.WriteLine(sync_tasks.Count);
            //foreach(var i in sync_tasks)
            //{
            //    ProcessTask(i);
            //}
            //Console.WriteLine("同步完成!");
            ProjectAll projects = new ProjectAll();
            foreach(var project in projects.project_list)
            {
                foreach(var a in project.TexAsset)
                {
                    Console.WriteLine(a.Name,a.Sync);
                }
                foreach (var a in project.RigAsset)
                {
                    Console.WriteLine(a.Name, a.Sync);
                }
                foreach (var a in project.HairAsset)
                {
                    Console.WriteLine(a.Name, a.Sync);
                }
                //Console.WriteLine(project.Name);
            }
            Console.ReadKey();
        }
    }
}
