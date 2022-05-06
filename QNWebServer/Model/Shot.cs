using System.ComponentModel.DataAnnotations;
namespace QNWebServer;

public class Project
{
    public int ProjectId {get;set;}
    [Required]
    public string Name {get;set;}

    public string Description {get;set;}

    public string CreateUser {get;set;}

    public List<Sc> Scs {get;set;}
}
public class Sc
{
    public int ScId {get;set;}
    [Required]
    public string Name {get;set;}

    public string Description {get;set;}

    public string CreateUser {get;set;}

    public List<Shot> Shots {get;set;}
    public int ProjectId {get;set;}
}

public class Shot
{
    public int ShotId {get;set;}

    [Required]
    public string Name {get;set;}

    public string Image {get;set;}

    public string Description {get;set;}
    public string CreateUser {get;set;}


    public int StratFrame {get;set;}
    public int EndFrame {get;set;}




    public int ScId {get;set;}

    public Sc Sc {get;set;}

}