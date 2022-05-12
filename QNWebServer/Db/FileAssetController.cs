using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QNWebServer.Data;
using System.IO;

namespace QNWebServer.Admin;


[Route("Admin/FileAssets")]
[ApiController]
public class FileAssetController:Controller
{
    private readonly FileAssetContext _db;
    public FileAssetController(FileAssetContext db)
    {
        _db = db;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<FileAsset>>> GetAllAssets()
    {
        //await Task.Delay(0);
        return await _db.Assets.ToListAsync();
    }
    [HttpPost("deletes")]
    public async Task<ActionResult<bool>> RemoveAssets(List<FileAsset> fas)
    {
        foreach(var fa in fas)
        {
            var a=await _db.Assets.Where(a=>a.FileAssetId==fa.FileAssetId).SingleOrDefaultAsync();
            if(a != null)
            {
                _db.Assets.Remove(a);
            }
        }
        await _db.SaveChangesAsync();
        foreach(var fa in fas)
        {
            try
            {
                var real_path = Path.Combine(Environment.CurrentDirectory,"wwwroot",fa.FilePath.Substring(1));
                if(System.IO.File.Exists(real_path))
                    System.IO.File.Delete(real_path);
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        return true;
        
    }

}