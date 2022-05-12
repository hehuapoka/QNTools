using System.ComponentModel.DataAnnotations;
namespace QNWebServer.Data;
public enum AssetType {CHR=0,PROP,ELEM,SET}
public enum TaskStatus {Define=0,Start,Modify,Finished}
public enum TaskType {Arts=0,Painter,Storyboard,Asset,Layout,Anim,CFX,VFX,Development,Light,Comp,Cuter,Colorer}
public class Project
{
    [Key]
    public int ProjectId {get;set;}
    [Required]
    public string Name {get;set;}

    public string? Image {get;set;}

    public string? Description {get;set;}

    public string? CreateUser {get;set;}

    public DateTime CreateTime {get;set;}

    public List<Sc> Scs {get;set;}
    public List<Asset> Assets {get;set;}
}
public class Asset
{
    [Key]
    public int AssetId {get;set;}

    [Required]
    public string Name {get;set;}
    public AssetType Type {get;set;}
    public string? Image {get;set;}

    public string? Description {get;set;}

    public string? CreateUser {get;set;}

    public DateTime CreateTime {get;set;}

    public List<JobTaskAsset> JobTaskAssets {get;set;} = new();

    public int ProjectId {get;set;}
    public Project Project {get;set;}

}
public class Sc
{
    [Key]
    public int ScId {get;set;}
    [Required]
    public string Name {get;set;}

    public string? Description {get;set;}

    public string? CreateUser {get;set;}
    public DateTime CreateTime {get;set;}

    public List<Shot> Shots {get;set;}
    public int ProjectId {get;set;}
    public Project Project {get;set;}
}

public class Shot
{
    [Key]
    public int ShotId {get;set;}

    [Required]
    public string Name {get;set;}

    public string? Image {get;set;}

    public string? Description {get;set;}
    public string? CreateUser {get;set;}
    public DateTime CreateTime {get;set;}


    public int StratFrame {get;set;}
    public int EndFrame {get;set;}

    public List<JobTaskShot> JobTaskShots {get;set;} = new();


    public int ScId {get;set;}

    public Sc Sc {get;set;}

}

public class JobTaskAsset
{

    [Key]
    public int JobTaskAssetId {get;set;}
    [Required]
    public string Name {get;set;}
    public string? Description {get;set;}
    public string? CreateUser {get;set;}
    public DateTime CreateTime {get;set;}
    [Required]
    public string Worker {get;set;}
    [Required]
    public string Reviewer {get;set;}

    [Required]
    public DateTime StartDate {get;set;}
    [Required]
    public DateTime EndDate {get;set;}

    public TaskStatus Status {get;set;}
    public TaskType Type {get;set;}


    public int AssetId {get;set;}
    
    public Asset Asset {get;set;}
}

public class JobTaskShot
{

    [Key]
    public int JobTaskShotId {get;set;}
    [Required]
    public string Name {get;set;}
    public string? Description {get;set;}
    public string? CreateUser {get;set;}
    public DateTime CreateTime {get;set;}
    [Required]
    public string Worker {get;set;}
    [Required]
    public string Reviewer {get;set;}

    [Required]
    public DateTime StartDate {get;set;}
    [Required]
    public DateTime EndDate {get;set;}

    public TaskStatus Status {get;set;}
    public TaskType Type {get;set;}


    public int ShotId {get;set;}
    public Shot Shot {get;set;}
}