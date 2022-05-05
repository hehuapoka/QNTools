using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
//using Microsoft.EntityFrameworkCore.Sqlite;
namespace QNWebServer;
public enum UsersType {SuperAdmin=0,Admin,Product,Art,Anim,Layout,CFX,VFX,Comp}
public class Users
{

    [Key,Required(ErrorMessage ="请输入你的登录名称"),MaxLength(50,ErrorMessage ="你的账户太长了"),MinLength(5,ErrorMessage ="你的账户名称太短了!")]
    [RegularExpression(@"^\w+\d*$",ErrorMessage ="格式中不能包含特殊字符")]
    public string Name {get;set;}

    [Required(ErrorMessage ="请输入你的显示名称")]
    [MaxLength(50,ErrorMessage ="你的账户太长了"),MinLength(1,ErrorMessage ="你的账户名称太短了!"),RegularExpression(@"^\w+\d*$",ErrorMessage ="格式中不能包含特殊字符")]
    public string DisplayName {get;set;}

    [Required]
    public UsersType Type {get;set;}

    [Required(ErrorMessage ="请输入你的密码"),DataType(DataType.Password),MaxLength(50,ErrorMessage ="你的账户太长了"),MinLength(5,ErrorMessage ="你的账户名称太短了!"),RegularExpression(@"^\w+\d+$",ErrorMessage ="必须同时包含之母和数字")]
    public string Password {get;set;}



}
