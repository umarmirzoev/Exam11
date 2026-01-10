 using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Net;

namespace Exam11.Controllers;
[ApiController]
[Route("Exam11/bookloon/")]
public class BookLoonController(IBookLoonService BookLoonService ) : ControllerBase
{
    [HttpGet]
    public async Task<List<BookLoon>> GetBookLoonAsync()
    {
        return await BookLoonService.GetBookLoonAsync();
    }

    [HttpPost]
    public async Task<Response<string>> AddAsync(BookLoon BookLoon)
    {
        return await BookLoonService.AddBookLoonAsync(BookLoon);
    }

    [HttpGet("{BookLoonId}")]
    public async Task<Response<BookLoon>> GetBookLoonByIdAsync(int BookLoonId)
    {
        return await BookLoonService.GetBookLoonByIdAsync(BookLoonId);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(BookLoon BookLoon)
    {
        return await BookLoonService.UpdateAsync(BookLoon);
    }
    [HttpDelete("{BookLoonId}")]
    public async Task<Response<string>> DeleteAsync(int BookLoonId)
    {
        return await BookLoonService.DeleteAsync(BookLoonId);
    }
}