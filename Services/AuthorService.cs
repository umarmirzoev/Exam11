using Dapper;
using Npgsql;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;
public class AuthorService(ApplicationDBContext applicationDbContext): IAuthorService
{
    private readonly ApplicationDBContext _dbContext = applicationDbContext;

    public async Task<Response<string>> AddAuthorAsync(Author author)
    {
        using var conn = _dbContext.Connection();
        var query = "insert into author(fullname,birthdate,country) values(@fullname,@birthdate,@country)";
        var res = await conn.ExecuteAsync(query, new {fullname = author.Fullname,birthdate=author.Birthdate,country=author.Country});
        return res==0
        ? new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong!")
        : new Response<string>(HttpStatusCode.OK, "Author added successfully!");
    }

    public async Task<Response<string>> DeleteAsync(int AuthorId)
    {
        try{
        using var context = _dbContext.Connection();
        var query = "delete from author where id = @id";
        var res = await context.ExecuteAsync(query,new{id=AuthorId});
        return res==0
            ?new Response<string>(HttpStatusCode.InternalServerError, "Author data not deleted!")
            :new Response<string>(HttpStatusCode.OK, "Author data successfully deleted!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    //GetbyId
    public async Task<Response<Author>> GetAuthorByIdAsync(int AuthorId)
    {
        try{
        using var conn = _dbContext.Connection();
        var query = "select * from author where id = @id";
        var result = await conn.QueryFirstOrDefaultAsync<Author>(query, new{id=AuthorId});
        return result==null
                ?new Response<Author>(HttpStatusCode.InternalServerError, "Author not found!")
                :new Response<Author>(HttpStatusCode.OK, "Author found!", result);
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<Author>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
    //Get
    public async Task<List<Author>> GetAuthorAsync()
    {
        using var conn = _dbContext.Connection();
        var query = "select * from Author";
        var res = await conn.QueryAsync<Author>(query);
        return res.ToList();
    }

    //Update
    public async Task<Response<string>> UpdateAsync(Author author)
    {
        try
        {
            using var context = _dbContext.Connection();
            var query = "update author set fullname = @fullname,birthdate = @birthdate,country=@country where id = @id";
            var result = await context.ExecuteAsync(query, new{fullname = author.Fullname, birthdate=author.Birthdate,coutry=author.Country,id = author.Id});
            return result==0
                ?new Response<string>(HttpStatusCode.InternalServerError, "Author data not updated!")
                :new Response<string>(HttpStatusCode.OK, "Author data successfully updated!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

}