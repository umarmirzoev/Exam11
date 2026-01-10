using Dapper;
using Npgsql;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;
public class BookService(ApplicationDBContext applicationDbContext): IBookService
{
    private readonly ApplicationDBContext _dbContext = applicationDbContext;

    //Add
    public async Task<Response<string>> AddBookAsync(Book book)
    {
        using var conn = _dbContext.Connection();
        var query = "insert into book(title,publishedyear,genre) values(@title,@publishedyear,@genre)";
        var res = await conn.ExecuteAsync(query, new {title = book.Title,publishedyear=book.PublishedYear,genre=book.Genre});
        return res==0
        ? new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong!")
        : new Response<string>(HttpStatusCode.OK, "Book added successfully!");
    }

    //delete
    public async Task<Response<string>> DeleteAsync(int BookId)
    {
        try{
        using var context = _dbContext.Connection();
        var query = "delete from book where id = @id";
        var res = await context.ExecuteAsync(query,new{id=BookId});
        return res==0
            ?new Response<string>(HttpStatusCode.InternalServerError, "Book data not deleted!")
            :new Response<string>(HttpStatusCode.OK, "Book data successfully deleted!");
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
    public async Task<Response<Book>> GetBookByIdAsync(int BookId)
    {
        try{
        using var conn = _dbContext.Connection();
        var query = "select * from book where id = @id";
        var result = await conn.QueryFirstOrDefaultAsync<Book>(query, new{id=BookId});
        return result==null
                ?new Response<Book>(HttpStatusCode.InternalServerError, "Book not found!")
                :new Response<Book>(HttpStatusCode.OK, "Book found!", result);
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<Book>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
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
            return result==0
                ?new Response<string>(HttpStatusCode.InternalServerError, "Book data not updated!")
                :new Response<string>(HttpStatusCode.OK, "Book data successfully updated!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

}