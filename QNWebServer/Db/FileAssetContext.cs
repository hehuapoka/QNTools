using Microsoft.EntityFrameworkCore;
using QNWebServer.Data;
namespace QNWebServer.Admin;
public class FileAssetContext:DbContext
{
    public FileAssetContext(DbContextOptions<FileAssetContext> options):base(options)
    {
    }

    public DbSet<FileAsset> Assets {get;set;}
}