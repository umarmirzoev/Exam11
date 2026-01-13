using System.ComponentModel.DataAnnotations;

public class BookLoon
{
    public int Id {get;set;}
    public int BookId {get;set;}
    public int UsersId {get;set;}
    public DateTime Loandate {get;set;}
    public DateTime Returndate {get;set;}
}
