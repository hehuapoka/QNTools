using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using QNWebServer.Data;

namespace QNWebServer.Admin;


[Route("Admin/project")]
[ApiController]
public class ShotController:Controller
{
    private readonly ProjectContext _db;
    public ShotController(ProjectContext db)
    {
        _db = db;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<List<string>>>> GetAllShots()
    {
        //await Task.Delay(0);
        return await _db.Projects.Select(a => new List<string>{a.Name,a.Image?? "/Images/project.png" }).ToListAsync();
    }

    //创建新项目
    [HttpPost("new")]
    public async Task<ActionResult<bool>> CreateNewProject(NewProject newproject)
    {
        //return new List<Project>{new Project{Name="GBWZ",Description="this is a project"}};
        var n=await _db.Projects.Where(p => p.Name == newproject.Name).FirstOrDefaultAsync();
        if(n==null)
        {
            var a = new Project{
                            Name=newproject.Name,
                            Description=newproject.Description,
                            Image=newproject.Image,
                            CreateTime = DateTime.Now
                            };
            await _db.Projects.AddAsync(a);
            await _db.SaveChangesAsync();
            return true;
        }

        return false;
        
    }

    [HttpGet("{projectName}")]
    public async Task<ActionResult<Project?>> GetProjectInfo(string projectName)
    {
        return await _db.Projects.Where(a => a.Name==projectName).SingleOrDefaultAsync();
    }

    [HttpGet("remove/{projectName}")]
    public async Task<ActionResult<bool>> GetProjectRemove(string projectName)
    {
        var a=await _db.Projects.Where(a => a.Name==projectName).SingleOrDefaultAsync();
        if(a == null) return false;
        else
        {
            _db.Projects.Remove(a);
            await _db.SaveChangesAsync();
            return true;
        }

    }

    [HttpGet("assets/{projectName}")]
    public async Task<ActionResult<Project?>> GetAllAssets(string projectName)
    {
        return await _db.Projects.Where(a => a.Name == projectName).Include(p => p.Assets).FirstOrDefaultAsync();

    }

    [HttpPost("assets/new")]
    public async Task<ActionResult<bool>> CreateNewAsset(NewAsset newAsset)
    {
        var project = await _db.Projects.Where(p => p.Name == newAsset.Project).FirstAsync();
        if(project == null)
            return false;
        
        var project1=await _db.Assets.Where(a => a.ProjectId == project.ProjectId && a.Name == newAsset.Name).SingleOrDefaultAsync();
        if(project1 != null)
            return false;
        var project2=await _db.Projects.Where(p => p.Name == newAsset.Project).Include(p => p.Assets).FirstAsync();
        project2.Assets.Add(
                new Asset
                {
                    Name = newAsset.Name,
                    Description = newAsset.Description,
                    Type =newAsset.Type,
                    Image = newAsset.Image,
                    CreateUser = newAsset.CreateUser,
                    CreateTime = DateTime.Now
                }
        );

        await _db.SaveChangesAsync();
        //await Task.Delay(0);
        return true;
    }

    [HttpGet("assets/{ProjectName}/{AssetName}")]
    public async Task<ActionResult<Project?>> GetAssetInfo(string ProjectName,string AssetName)
    {
        return await _db.Projects.Where(p => p.Name == ProjectName).Include(a => a.Assets.Where(b => b.Name == AssetName)).ThenInclude(c => c.JobTaskAssets).FirstOrDefaultAsync();
    }

    [HttpPost("assets/tasks/new")]
    public async Task<ActionResult<bool>> CreateNewTask(NewTask newtask)
    {
        var p = await _db.Projects.Where(p => p.Name == newtask.ProjectName).Include(p => p.Assets.Where(a => a.Name == newtask.AssetName)).FirstOrDefaultAsync();
        if(p == null) return false;
        if(p.Assets == null) return false;

        //Console.WriteLine(JsonSerializer.Serialize(p));
        var task_a = new JobTaskAsset{
                        Name = newtask.Name,
                        Description = newtask.Description,
                        CreateUser = newtask.CreateUser,
                        CreateTime = DateTime.Now,
                        Worker = newtask.Worker,
                        Reviewer = newtask.Reviewer,
                        StartDate = newtask.StartDate,
                        EndDate = newtask.EndDate,
                        Status = newtask.Status,
                        Type = newtask.Type,
                        };
        p.Assets[0].JobTaskAssets.Add(task_a);

        await _db.SaveChangesAsync();
        return true;
    }

    


    [HttpGet("assets/tasks/{taskId:int}")]
    public async Task<ActionResult<JobTaskAsset?>> GetAssetTask(int taskId)
    {
        return await _db.JobTaskAssets.Where(t => t.JobTaskAssetId == taskId).SingleOrDefaultAsync();
    }

    [HttpGet("assets/tasks/creater/{username}")]
    public async Task<ActionResult<List<JobTaskAsset>?>> GetMyCreateTask(string username)
    {
        return await _db.JobTaskAssets.Where(t => t.CreateUser == username).ToListAsync();
    }

    [HttpGet("assets/tasks/worker/{username}")]
    public async Task<ActionResult<List<JobTaskAsset>?>> GetMyWorkerTask(string username)
    {
        return await _db.JobTaskAssets.Where(t => t.Worker == username).ToListAsync();
    }

    [HttpGet("assets/tasks/reviewer/{username}")]
    public async Task<ActionResult<List<JobTaskAsset>?>> GetMyReviewerTask(string username)
    {
        return await _db.JobTaskAssets.Where(t => t.Reviewer == username).ToListAsync();
    }

}