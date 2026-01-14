using System.ComponentModel.DataAnnotations;

public class BookDTO
{
    [Required(ErrorMessage ="Title обязателен")]
    public string? Title {get;set;}
    public int PublishedYear {get;set;}
    [Required(ErrorMessage ="Genre обязателен")]
    public string? Genre {get;set;}
};
public class BookDTOo
{
    public int Id {get;set;}
     [Required(ErrorMessage ="Title обязателен")]
    public string? Title {get;set;}
    public int PublishedYear {get;set;}
    [Required(ErrorMessage ="Genre обязателен")]
    public string? Genre {get;set;}
}