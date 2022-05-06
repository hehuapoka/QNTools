using Microsoft.EntityFrameworkCore;
namespace QNWebServer.Admin;
public class ShotConetext:DbContext
{
    public ShotConetext(DbContextOptions options):base(options)
    {
    }

    public DbSet<Sc> Projects {get;set;}
    public DbSet<Sc> Scs {get;set;}
    public DbSet<Shot> Shots {get;set;}
}