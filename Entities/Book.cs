using System.ComponentModel.DataAnnotations;

public class Book
{
    public int Id {get;set;}
    [Required(ErrorMessage = "Title обязателен")]
    public string? Title {get;set;}
    public int PublishedYear {get;set;}
    [Required(ErrorMessage = "Genre обязателен")]
    public string? Genre {get;set;}
    public int AuthorId {get;set;}
}