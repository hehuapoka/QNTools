using System.ComponentModel.DataAnnotations;
namespace QNWebServer.Data;
public class NewSC
{
    [Required]
    public string Name {get;set;}

    public string? Image {get;set;}
    public string? Description {get;set;}

    public string? CreateUser {get;set;}
    public DateTime CreateTime {get;set;}
    public int ProjectId {get;set;}

    public bool IsShow {get;set;}
    public void closeDialog()
    {
        IsShow=false;
    }
}