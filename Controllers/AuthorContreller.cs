 using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Net;

namespace Exam11.Controllers;
[ApiController]
[Route("Exam11/author/")]
public class AuthorController(IAuthorService AuthorService ) : ControllerBase
{
    [HttpGet]
    public async Task<List<Author>> GetAuthorAsync()
    {
        return await AuthorService.GetAuthorAsync();
    }

    [HttpPost]
    public async Task<Response<string>> AddAsync(Author Author)
    {
        return await AuthorService.AddAuthorAsync(Author);
    }

    [HttpGet("{AuthorId}")]
    public async Task<Response<Author>> GetAuthorByIdAsync(int AuthorId)
    {
        return await AuthorService.GetAuthorByIdAsync(AuthorId);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(Author Author)
    {
        return await AuthorService.UpdateAsync(Author);
    }
    [HttpDelete("{AuthorId}")]
    public async Task<Response<string>> DeleteAsync(int AuthorId)
    {
        return await AuthorService.DeleteAsync(AuthorId);
    }
}