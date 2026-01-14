using Dapper;
using Npgsql;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;
public class BookService(ApplicationDBContext applicationDbContext,ILogger<BookService> logger): IBookService
{
    private readonly ApplicationDBContext _dbContext = applicationDbContext;
    private readonly ILogger<BookService> _logger = logger;

    //Add
    public async Task<Response<string>> AddBookAsync(Book book)
    {
        using var conn = _dbContext.Connection();
        var query = "insert into book(title,publishedyear,genre) values(@title,@publishedyear,@genre)";
        var res = await conn.ExecuteAsync(query, new {title = book.Title,publishedyear=book.PublishedYear,genre=book.Genre});
        if(res==0)
        {
            _logger.LogWarning("Something went wrong while adding book");
            return new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong!");
        }
        else
        {
            _logger.LogInformation("Nothing went wrong");
        return new Response<string>(HttpStatusCode.OK, "Book added successfully!");
        }       
    }
    

    //delete
    public async Task<Response<string>> DeleteAsync(int BookId)
    {
        _logger.LogInformation("Starting the proccess ");
        try{
        using var context = _dbContext.Connection();
        var query = "delete from book where id = @id";
        var res = await context.ExecuteAsync(query,new{id=BookId});
            if(res==0)
            {
                _logger.LogWarning("Something went wrong while delete book");
                return  new Response<string>(HttpStatusCode.InternalServerError, "Book data not deleted!");
            }
            else
            {
                _logger.LogInformation("Nothing went wrong by deleting this book");
             return new Response<string>(HttpStatusCode.OK, "Book data successfully deleted!");
            }
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public Task<List<Book>> GetBookAsync()
    {
        throw new NotImplementedException();
    }

    //GetbyId
    public async Task<Response<Book>> GetBookByIdAsync(int bookId)
    {
        _logger.LogInformation("Searching Book by id is processing...");
        var conn = _dbContext.Connection();
        var query = "select * from Book where id = @id";
        var res = await conn.QueryFirstOrDefaultAsync(query,new{id = bookId});
        return new Response<Book>(HttpStatusCode.OK, "The data: ", res);
    }
    //Get
    public async Task<List<Book>> GetBooksAsync()
    {
        using var conn = _dbContext.Connection();
        var query = "select * from Book";
        var res = await conn.QueryAsync<Book>(query);
        return res.ToList();
    }

    //Update
    public async Task<Response<string>> UpdateAsync(Book book)
    {
        try
        {
            using var context = _dbContext.Connection();
            var query = "update book set title = @title,publishedyear = @publishedyear,genre=@genre where id = @id";
            var result = await context.ExecuteAsync(query, new{title = book.Title, publishedyear=book.PublishedYear,genre=book.Genre,id = book.Id});
            if(result==0)
            {
                _logger.LogWarning("Something went wrong while update book");
                 return new Response<string>(HttpStatusCode.InternalServerError, "Book data not updated!");
            }  
            else
            {
                _logger.LogInformation("Nothing went wrong while updating ");
                return new Response<string>(HttpStatusCode.OK, "Book data successfully updated!");
            }   
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

}