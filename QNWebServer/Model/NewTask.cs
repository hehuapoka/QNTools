using System.ComponentModel.DataAnnotations;
namespace QNWebServer.Data;
public class NewTask
{
    [Required]
    public string Name {get;set;}
    public string ProjectName {get;set;} = "";
    public string AssetName {get;set;} = "";
    public string? Description {get;set;}
    public string? CreateUser {get;set;}
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

    public bool IsShow {get;set;}

    public void closeShow()
    {
        IsShow = false;
    }
    public void SetWorker(string a)
    {
        Worker =a;
    }

    public void SetReviewer(string a)
    {
        Reviewer =a;
    }
}