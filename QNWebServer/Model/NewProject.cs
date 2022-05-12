using System.ComponentModel.DataAnnotations;
namespace QNWebServer.Data;
public class NewProject
{
    [Required(ErrorMessage ="项目名称是必须的")]
    public string Name {get;set;}

    public string? Image {get;set;}

    public string? Description {get;set;}

    public string? CreateUser {get;set;}

    public bool IsShow {get;set;}

    public void CloseDialog()
    {
        IsShow=false;
    }
    public void CreateDialog()
    {
        IsShow=true;
    }
}