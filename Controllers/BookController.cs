 using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Net;

namespace Exam11.Controllers;
[ApiController]
[Route("Exam11/book/")]
public class BookController(IBookService BookService ) : ControllerBase
{
    [HttpGet]
    public async Task<List<Book>> GetBookAsync()
    {
        return await BookService.GetBookAsync();
    }

    [HttpPost]
    public async Task<Response<string>> AddAsync(Book Book)
    {
        return await BookService.AddBookAsync(Book);
    }

    [HttpGet("{BookId}")]
    public async Task<Response<Book>> GetBookByIdAsync(int BookId)
    {
        return await BookService.GetBookByIdAsync(BookId);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(Book Book)
    {
        return await BookService.UpdateAsync(Book);
    }
    [HttpDelete("{BookId}")]
    public async Task<Response<string>> DeleteAsync(int BookId)
    {
        return await BookService.DeleteAsync(BookId);
    }
}