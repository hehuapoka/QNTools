using System.ComponentModel.DataAnnotations;
namespace QNWebServer.Data;

public enum FileType {Image=0,Maya,Alembic,houdini,Nuke,Docs}
public class FileAsset
{
    [Key]
    public int FileAssetId {get;set;}
    [Required]
    public string Name {get;set;}
    [Required]
    public string FilePath {get;set;}
    [Required]
    public FileType FileType;

    public string? Tags {get;set;}
}