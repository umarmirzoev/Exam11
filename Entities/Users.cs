using System.ComponentModel.DataAnnotations;

public class Users
{
    public int Id {get;set;}
     [Required(ErrorMessage = "Fullname обязателен")]
    public string? Fullname {get;set;}
    [Required(ErrorMessage = "Email обязателен")]

    public string? Email {get;set;}
    public DateTime RegisteredAt {get;set;}

}