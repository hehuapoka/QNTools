using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace QNWebServer.Admin;


[Route("Admin/Shots")]
[ApiController]
public class ShotController:Controller
{
    private readonly ShotConetext _db;
    public ShotController(ShotConetext db)
    {
        _db = db;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<UsersLabel>>> GetAllShots()
    {
        return await _db.Projects.Include(p => p.Shots).
    }

}