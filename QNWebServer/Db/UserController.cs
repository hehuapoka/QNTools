using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace QNWebServer.Admin;


[Route("admin")]
[ApiController]
public class UserController:Controller
{
    private readonly UserContext _db;
    public UserController(UserContext db)
    {
        _db = db;
    }

    [HttpGet("users/all")]
    public async Task<ActionResult<List<UsersLabel>>> GetAllUsers()
    {
        return await _db.Users.Select(u => new UsersLabel{ Name=u.Name,Type=u.Type,DisplayName=u.DisplayName}).ToListAsync();
    }

    [HttpGet("users/alltemp")]
    public async Task<ActionResult<List<UsersTemp>>> GetAllTempUsers()
    {
        return await _db.UsersTemp.ToListAsync();
    }

    [HttpPost("register")]
    public async Task<ActionResult<bool>> UserRegister(UsersTemp user)
    {
       var a= await _db.UsersTemp.Where(u => u.Name == user.Name).FirstOrDefaultAsync();
       var b= await _db.Users.Where(u => u.Name == user.Name).FirstOrDefaultAsync();
       if(a == null || b == null)
       {
           if(user.Type==UsersType.SuperAdmin) return false;
           await _db.UsersTemp.AddAsync(new UsersTemp{Name=user.Name,DisplayName=user.DisplayName,Type=user.Type,Password=user.Password});
           await _db.SaveChangesAsync();
           return true;
       }
        return false;
    }

    //注册用户
    [HttpPost("register/add")]
    public async Task<ActionResult<bool>> UserRegisterAdd(UsersTemp user)
    {
       var a= await _db.Users.Where(u => u.Name == user.Name).FirstOrDefaultAsync();
       if(a == null)
       {
           if(user.Type==UsersType.SuperAdmin) return false;
           await _db.Users.AddAsync(new Users{Name=user.Name,DisplayName=user.DisplayName,Type=user.Type,Password=user.Password});
           _db.UsersTemp.Remove(user);
           await _db.SaveChangesAsync();
           return true;
       }
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
        
        
        return new UserLoginState{Name=a.Name,DisplayName=a.DisplayName,Type=a.Type,Image = a.Image};
    }

    [HttpPost("users/removetemp")]
    public async Task<ActionResult<bool>> UserRemoveTemp(UsersTemp user)
    {
       _db.UsersTemp.Remove(user);
       await _db.SaveChangesAsync();
       return true;
    }

    [HttpPost("users/remove")]
    public async Task<ActionResult<bool>> UserRemove(Users user)
    {
       _db.Users.Remove(user);
       await _db.SaveChangesAsync();
       return true;
    }
}