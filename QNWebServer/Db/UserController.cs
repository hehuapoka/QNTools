using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace QNWebServer.Admin;


[Route("admin")]
[ApiController]
public class UserController:Controller
{
    private readonly UserConetext _db;
    public UserController(UserConetext db)
    {
        _db = db;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<UsersLabel>>> GetAllUsers()
    {
        return await _db.Users.Select(u => new UsersLabel{ Name=u.Name,Type=u.Type}).ToListAsync();
    }
    //注册用户
    [HttpPost("register")]
    public async Task<ActionResult<bool>> UserRegister(Users user)
    {
       var a= await _db.Users.Where(u => u.Name == user.Name).FirstOrDefaultAsync();
       if(a == null)
       {
           await _db.AddAsync(new Users{Name=user.Name,DisplayName=user.DisplayName,Type=user.Type,Password=user.Password});
           await _db.SaveChangesAsync();
           return true;
       }
        await Task.Delay(0);
        return false;
    }
    //登录
    [HttpPost("login")]
    public async Task<ActionResult<UserLoginState?>> UserLogin(LoginUser user)
    {
       var a= await _db.Users.Where(u => u.Name == user.Name && u.Password == user.Password).FirstOrDefaultAsync();
       if(a == null)
       {
           return new UserLoginState();
       }
        
        
        return new UserLoginState{Name=a.Name,DisplayName=a.DisplayName,Type=a.Type};
    }
}