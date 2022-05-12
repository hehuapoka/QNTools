using System.ComponentModel.DataAnnotations;
namespace QNWebServer.Data;
public class NewAsset
{
    
    public string? Project {get;set;}
    [Required(ErrorMessage ="资产名称是必须的")]
    public string Name {get;set;}
    public AssetType Type {get;set;}
    public string? Image {get;set;}

    public string? Description {get;set;}

    public string? CreateUser {get;set;}

    public DateTime CreateTime {get;set;}

    public bool IsShow {get;set;}
    public void closeDialog()
    {
        IsShow=false;
    }
}