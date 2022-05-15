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

    //获取工程信息
    [HttpGet("{projectName}")]
    public async Task<ActionResult<Project?>> GetProjectInfo(string projectName)
    {
        return await _db.Projects.Where(a => a.Name==projectName).SingleOrDefaultAsync();
    }

    //移除工程
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




    //--------------------------------------------------------------------------------------




    //获取工程及资产
    [HttpGet("assets/{projectName}")]
    public async Task<ActionResult<Project?>> GetAllAssets(string projectName)
    {
        return await _db.Projects.Where(a => a.Name == projectName).Include(p => p.Assets).FirstOrDefaultAsync();

    }

    //创建新资产
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


    //获取资产
    [HttpGet("assets/{ProjectName}/{AssetName}")]
    public async Task<ActionResult<Project?>> GetAssetInfo(string ProjectName,string AssetName)
    {
        return await _db.Projects.Where(p => p.Name == ProjectName).Include(a => a.Assets.Where(b => b.Name == AssetName)).FirstOrDefaultAsync(); //.ThenInclude(c => c.JobTaskAssets)
    }


    //--------------------------------------------------------------------------------------




    [HttpGet("assets/tasks/all/{AssetId:int}")]
    public async Task<ActionResult<List<ProjectTask>?>> GetAssetTasks(int AssetId)
    {
        return await _db.ProjectTasks.Where(t => t.AssetIndex == AssetId).ToListAsync();
    }

    [HttpPost("assets/tasks/new")]
    public async Task<ActionResult<bool>> CreateNewTask(NewTask newtask)
    {

        //Console.WriteLine(JsonSerializer.Serialize(p));
        var task_a = new ProjectTask{
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
                        AssetIndex = newtask.AssetId,
                        ShotIndex = newtask.ShotId
                        };
        await _db.ProjectTasks.AddAsync(task_a);

        await _db.SaveChangesAsync();
        return true;
    }

    


    [HttpGet("assets/tasks/{taskId:int}")]
    public async Task<ActionResult<ProjectTask?>> GetAssetTask(int taskId)
    {
        return await _db.ProjectTasks.Where(t => t.ProjectTaskId == taskId).SingleOrDefaultAsync();
    }

    [HttpGet("assets/tasks/creater/{username}")]
    public async Task<ActionResult<List<ProjectTask>?>> GetMyCreateTask(string username)
    {
        return await _db.ProjectTasks.Where(t => t.CreateUser == username).ToListAsync();
    }

    [HttpGet("assets/tasks/worker/{username}")]
    public async Task<ActionResult<List<ProjectTask>?>> GetMyWorkerTask(string username)
    {
        return await _db.ProjectTasks.Where(t => t.Worker == username).ToListAsync();
    }

    [HttpGet("assets/tasks/reviewer/{username}")]
    public async Task<ActionResult<List<ProjectTask>?>> GetMyReviewerTask(string username)
    {
        return await _db.ProjectTasks.Where(t => t.Reviewer == username).ToListAsync();
    }

}