using Microsoft.EntityFrameworkCore;
namespace QNWebServer.Admin;
public class UserConetext:DbContext
{
    public UserConetext(DbContextOptions options):base(options)
    {
    }
    public DbSet<Users> Users {get;set;}
}