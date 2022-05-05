namespace QNWebServer.Admin;
public static class InitServer
{
    public static void InitUserDb(UserConetext db)
    {
        db.Users.Add(new Users{Name="Admin",Password="Qn9981",Type=UsersType.SuperAdmin,DisplayName="超级管理员"});
        db.SaveChanges();
    }
}