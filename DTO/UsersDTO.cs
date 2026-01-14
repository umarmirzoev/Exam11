using System.ComponentModel.DataAnnotations;

public class UsersDTO
{
    [Required(ErrorMessage ="FullName обязателен")]
    public string? Fullname {get;set;}
    [Required(ErrorMessage ="Email обязателен")]
    public string? Email {get;set;}

};
public class UsersDTOo
{
    public int Id {get;set;}
      [Required(ErrorMessage ="FullName обязателен")]
    public string? Fullname {get;set;}
    [Required(ErrorMessage ="Email обязателен")]
    public string? Email {get;set;}
}