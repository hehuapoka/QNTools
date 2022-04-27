using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace QNSyncServer
{
    public static class Sync
    {
        static Dictionary<string, string> sync_tasks = new Dictionary<string, string>();
        static bool InList(string a, string[] alist)
        {
            foreach (var i in alist)
            {
                if (i.Equals(a))
                {
                    return true;
                }
            }
            return false;
        }
        static void SyncFile(string path, string[] filter_type, string server_dir, string local_dir)
        {

            string base_path = path.Replace(server_dir, local_dir);
            FileInfo info_server = new FileInfo(path);
            FileInfo info_local = new FileInfo(base_path);


            if (!InList(info_server.Extension, filter_type)) return;


            //情况1 本地没有文件
            if (info_local.Exists)
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
        static void SyncDir(string path, string[] filter_list, string server_dir, string local_dir)
        {
            if (!Directory.Exists(path)) return;

            var tex_files = Directory.EnumerateFiles(path);
            foreach (string tex_file in tex_files)
            {
                SyncFile(tex_file, filter_list, server_dir, local_dir);
            }
            var tex_dirs = Directory.EnumerateDirectories(path);
            foreach (string tex_dir in tex_dirs)
            {
                if (tex_dir.Replace(path, "").Equals("\\his")) continue;
                SyncDir(tex_dir, filter_list, server_dir, local_dir);
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




        //开始任务
        public static void ProcessTask(KeyValuePair<string, string> tas)
        {

            if (!Directory.Exists(Path.GetDirectoryName(tas.Value))) Directory.CreateDirectory(Path.GetDirectoryName(tas.Value));
            File.Copy(tas.Key, tas.Value, true);

        }
        public static void Run()
        {
            if (!File.Exists("app_config.json")) return;

            ProjectAll projects = new ProjectAll();

            string[] filter_list1 = { ".abc", ".usd" };
            foreach (var project in projects.project_list)
            {
                ////如果总开关是假就跳过任务
                if (!project.Sync) break;

                foreach (var a in project.TexAsset)
                {
                    if (!a.Sync) break;
                    SyncDir(ProjectAll.GetServerPath(project, a.Path), a.Format, ProjectAll.GetServerPath(project, a.Path), ProjectAll.GetLocalPath(project, a.Path));
                }
                foreach (var a in project.RigAsset)
                {
                    if (!a.Sync) break;
                    SyncDir(ProjectAll.GetServerPath(project, a.Path), a.Format, ProjectAll.GetServerPath(project, a.Path), ProjectAll.GetLocalPath(project, a.Path));
                }
                foreach (var a in project.HairAsset)
                {
                    if (!a.Sync) break;
                    SyncDir(ProjectAll.GetServerPath(project, a.Path), a.Format, ProjectAll.GetServerPath(project, a.Path), ProjectAll.GetLocalPath(project, a.Path));
                }
                foreach (var sc in project.ShotList)
                {
                    foreach (var shot in sc.Children)
                    {
                        if (shot.SyncAnims)
                            SyncCacheDir(filter_list1, ProjectAll.GetServerCachePath(project, sc.Name, shot.Name, 0), ProjectAll.GetLocalCachePath(project, sc.Name, shot.Name, 0));
                        if (shot.SyncCFX)
                            SyncCacheDir(filter_list1, ProjectAll.GetServerCachePath(project, sc.Name, shot.Name, 1), ProjectAll.GetLocalCachePath(project, sc.Name, shot.Name, 1));
                        if (shot.SyncVFX)
                            SyncCacheDir(filter_list1, ProjectAll.GetServerCachePath(project, sc.Name, shot.Name, 2), ProjectAll.GetLocalCachePath(project, sc.Name, shot.Name, 2));
                    }
                }
            }
            foreach (var i in sync_tasks)
            {
                ProcessTask(i);
                //File.AppendAllText("temp.txt", $"server:{i.Key} local:{i.Value}");
            }
            
        }
    }
}
