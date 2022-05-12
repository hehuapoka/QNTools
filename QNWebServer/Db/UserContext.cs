using Microsoft.EntityFrameworkCore;
namespace QNWebServer.Admin;
public class UserContext:DbContext
{
    public UserContext(DbContextOptions<UserContext> options):base(options)
    {
    }
    public DbSet<Users> Users {get;set;}
    public DbSet<UsersTemp> UsersTemp {get;set;}
}