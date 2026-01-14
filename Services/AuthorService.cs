using Dapper;
using Npgsql;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;
public class AuthorService(ApplicationDBContext applicationDbContext,ILogger<AuthorService> logger): IAuthorService
{
    private readonly ApplicationDBContext _dbContext = applicationDbContext;
    private readonly ILogger<AuthorService> _logger = logger;

    public async Task<Response<string>> AddAuthorAsync(Author author)
    {
        using var conn = _dbContext.Connection();
        var query = "insert into author(fullname,birthdate,country) values(@fullname,@birthdate,@country)";
        var res = await conn.ExecuteAsync(query, new {fullname = author.Fullname,birthdate=author.Birthdate,country=author.Country});
     
        if(res==0)
        {
           _logger.LogWarning(""); 
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong!");
        }
        else
        {
            _logger.LogInformation("");
         return new Response<string>(HttpStatusCode.OK, "Author added successfully!");
        }
    }

    public async Task<Response<string>> DeleteAsync(int AuthorId)
    {
        try{
        using var context = _dbContext.Connection();
        var query = "delete from author where id = @id";
        var res = await context.ExecuteAsync(query,new{id=AuthorId});
        if(res==0)
            {
                _logger.LogInformation("");
            return new Response<string>(HttpStatusCode.InternalServerError, "Author data not deleted!");                                                                                                                                                            
            }
        else
            {
                _logger.LogInformation("");
            return new Response<string>(HttpStatusCode.OK, "Author data successfully deleted!");
            }
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
        if(result==null)
            {
                _logger.LogInformation("");
              return new Response<Author>(HttpStatusCode.InternalServerError, "Author not found!");
            }      
            else
            {
                _logger.LogInformation("");
                return new Response<Author>(HttpStatusCode.OK, "Author found!", result);
            }
        
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
             if(result==0)
            {
                _logger.LogInformation("");
                return new Response<string>(HttpStatusCode.InternalServerError, "Author data not updated!");
            }
            else
            {
                _logger.LogInformation("");
                return new Response<string>(HttpStatusCode.OK, "Author data successfully updated!");
            }
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

}