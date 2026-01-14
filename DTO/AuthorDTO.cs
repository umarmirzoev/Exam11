using System.ComponentModel.DataAnnotations;

public class AuthorDTO
{
    [Required(ErrorMessage = "FullName обязателен")]
    public string? Fullname {get;set;}
    public DateTime Birthdate {get;set;}
    [Required(ErrorMessage = "Country обязателен")]
    public string? Country {get;set;}
};

public class AuthorDTOO
{
    public int Id {get;set;}
     [Required(ErrorMessage = "FullName обязателен")]
    public string? Fullname {get;set;}
    public DateTime Birthdate {get;set;}
    [Required(ErrorMessage = "Country обязателен")]
    public string? Country {get;set;}
}