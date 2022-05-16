using Microsoft.EntityFrameworkCore;
using QNWebServer.Data;
namespace QNWebServer.Admin;
public class ProjectContext:DbContext
{
    public ProjectContext(DbContextOptions<ProjectContext> options):base(options)
    {
    }

    public DbSet<Project> Projects {get;set;}
    public DbSet<Sc> Scs {get;set;}
    public DbSet<Shot> Shots {get;set;}
    public DbSet<Asset> Assets {get;set;}

    // public DbSet<JobTaskAsset> JobTaskAssets {get;set;}
    // public DbSet<JobTaskShot> JobTaskShots {get;set;}

    public DbSet<ProjectTask> ProjectTasks {get;set;}

    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
        
    //     modelBuilder.Entity<JobTask>().HasKey(t => new {t.AssetId,t.ShotId});
    //     modelBuilder.Entity<JobTask>().HasOne(t => t.Asset).WithMany(a => a.JobTasks).HasForeignKey(t => t.AssetId);
    //     modelBuilder.Entity<JobTask>().HasOne(t => t.Shot).WithMany(s => s.JobTasks).HasForeignKey(t => t.ShotId);

    // }

}