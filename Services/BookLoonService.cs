using Dapper;
using Npgsql;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;
public class BookLoonService(ApplicationDBContext applicationDbContext): IBookLoonService
{
    private readonly ApplicationDBContext _dbContext = applicationDbContext;

    //Add
    public async Task<Response<string>> AddBookLoonAsync(BookLoon bookLoon)
    {
        using var conn = _dbContext.Connection();
        var query = "insert into bookLoon(id,bookId,usersId) values(@id,@bookId,@usersId)";
        var res = await conn.ExecuteAsync(query, new {id = bookLoon.Id,bookId=bookLoon.BookId,usersId=bookLoon.UsersId});
        return res==0
        ? new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong!")
        : new Response<string>(HttpStatusCode.OK, "BookLoon added successfully!");
    }
    //Delete
    public async Task<Response<string>> DeleteAsync(int BookLoonId)
    {
        try{
        using var context = _dbContext.Connection();
        var query = "delete from bookLoon where id = @id";
        var res = await context.ExecuteAsync(query,new{id=BookLoonId});
        return res==0
            ?new Response<string>(HttpStatusCode.InternalServerError, "BookLoon data not deleted!")
            :new Response<string>(HttpStatusCode.OK, "BookLoon data successfully deleted!");
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
        try{
        using var conn = _dbContext.Connection();
        var query = "select * from bookLoon where id = @id";
        var result = await conn.QueryFirstOrDefaultAsync<BookLoon>(query, new{id=BookLoonId});
        return result==null
                ?new Response<BookLoon>(HttpStatusCode.InternalServerError, "BookLoon not found!")
                :new Response<BookLoon>(HttpStatusCode.OK, "BookLoon found!", result);
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<BookLoon>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
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
            return result==0
                ?new Response<string>(HttpStatusCode.InternalServerError, "BookLoon data not updated!")
                :new Response<string>(HttpStatusCode.OK, "BookLoon data successfully updated!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

}