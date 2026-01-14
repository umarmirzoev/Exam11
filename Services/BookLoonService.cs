using Dapper;
using Npgsql;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;
public class BookLoonService(ApplicationDBContext applicationDbContext,ILogger<BookLoonService> logger): IBookLoonService
{
    private readonly ApplicationDBContext _dbContext = applicationDbContext;
    private readonly ILogger<BookLoonService> _logger = logger;

    //Add
    public async Task<Response<string>> AddBookLoonAsync(BookLoon bookLoon)
    {
        
        _logger.LogInformation("Ading new BookLoon:");
        using var conn = _dbContext.Connection();
        var query = "insert into bookLoon(id,bookId,usersId) values(@id,@bookId,@usersId)";
        var res = await conn.ExecuteAsync(query, new {id = bookLoon.Id,bookId=bookLoon.BookId,usersId=bookLoon.UsersId});
        if(res==0)
        {
            _logger.LogWarning("Something went wrong while adding book");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong!");
        }
        else
        {
         _logger.LogInformation("Nothing went wrong");   
        return  new Response<string>(HttpStatusCode.OK, "BookLoon added successfully!");
        }
    }
    //Delete
    public async Task<Response<string>> DeleteAsync(int BookLoonId)
    {
        try{
        using var context = _dbContext.Connection();
        var query = "delete from bookLoon where id = @id";
        var res = await context.ExecuteAsync(query,new{id=BookLoonId});
         if(res==0)
            {
                _logger.LogWarning("Something went wrong while deleting book");
                return new Response<string>(HttpStatusCode.InternalServerError, "BookLoon data not deleted!");
            }  
        else
            {
              _logger.LogInformation("Nothing went wrong");  
            return new Response<string>(HttpStatusCode.OK, "BookLoon data successfully deleted!");
            }
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    //GetbyId
    public async Task<Response<BookLoon>> GetBookLoonByIdAsync(int BookLoonId)
    {
         _logger.LogInformation("Searching Book by id is processing...");
        var conn = _dbContext.Connection();
        var query = "select * from Book where id = @id";
        var res = await conn.QueryFirstOrDefaultAsync(query,new{id = BookLoonId});
        return new Response<BookLoon>(HttpStatusCode.OK, "The data: ", res);
    }
    //Get
    public async Task<List<BookLoon>> GetBookLoonAsync()
    {
        using var conn = _dbContext.Connection();
        var query = "select * from BookLoon";
        var res = await conn.QueryAsync<BookLoon>(query);
        return res.ToList();
    }

    //Update
    public async Task<Response<string>> UpdateAsync(BookLoon bookLoon)
    {
        try
        {
            using var context = _dbContext.Connection();
            var query = "update bookLoon set bookId=@bookId,usersId=@usersId where id = @id";
            var result = await context.ExecuteAsync(query, new{bookId = bookLoon.BookId, usersId=bookLoon.UsersId, id = bookLoon.Id});
            if(result==0)
            {
                _logger.LogWarning("Something went wrong while update book");
                 return new Response<string>(HttpStatusCode.InternalServerError, "BookLoon data not updated!");
            }  
            else
            {
                _logger.LogInformation("Nothing went wrong");
                return new Response<string>(HttpStatusCode.OK, "BookLoon data successfully updated!");
            }  
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

}