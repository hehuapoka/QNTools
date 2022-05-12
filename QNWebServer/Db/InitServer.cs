using QNWebServer.Data;
namespace QNWebServer.Admin;

public static class InitServer
{
    public static void InitUserDb(UserContext db)
    {
        db.Users.Add(new Users{Name="Admin",Password="Qn9981",Type=UsersType.SuperAdmin,DisplayName="超级管理员"});
        db.SaveChanges();
    }

    public static void InitProjectDb(ProjectContext db)
    {
        var a= new QNWebServer.Data.Project{Name="Default",Description="这是一个基础项目",CreateTime=DateTime.Now};
        db.Projects.Add(a);
        db.SaveChanges();
    }

     public static void InitFileAssetDb(FileAssetContext db)
    {
        var guid = Guid.NewGuid();
        var a= new FileAsset{
                            Name="project_face",
                            FilePath=$"/Images/project.png",
                            FileType=FileType.Image
                            };
        db.Assets.Add(a);
        db.SaveChanges();
    }
}