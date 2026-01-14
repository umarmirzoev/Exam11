using System.ComponentModel.DataAnnotations;

public class Author
{
    public int Id {get;set;}
    public string? Fullname {get;set;}
    public DateTime Birthdate {get;set;}
    public string? Country {get;set;}
}