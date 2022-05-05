using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
//using Microsoft.EntityFrameworkCore.Sqlite;
namespace QNWebServer;

public class LoginUser
{

    [Required(ErrorMessage ="你需要输入账户名")]
    public string? Name {get;set;}

    [Required(ErrorMessage ="你需要输入密码")]
    public string Password {get;set;}

}
