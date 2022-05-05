namespace QNWebServer.Admin;
public static class InitServer
{
    public static void InitUserDb(UserConetext db)
    {
        db.Users.Add(new Users{Name="Admin",Password="Qn9981",Type=UsersType.Admin,DisplayName="超级管理员"});
        db.Users.Add(new Users{Name="hehua",Password="wc121212",Type=UsersType.Admin,DisplayName="feng"});
        db.SaveChanges();
    }
}