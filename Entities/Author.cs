using System.ComponentModel.DataAnnotations;

public class Author
{
    public int Id {get;set;}
   [Required(ErrorMessage = "Fullname обязателен")]
    public string? Fullname {get;set;}
    public DateTime Birthdate {get;set;}
    [Required(ErrorMessage = "Country обязателен")]

    public string? Country {get;set;}
}