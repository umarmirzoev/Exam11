 using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Net;

namespace Exam11.Controllers;
[ApiController]
[Route("Exam11/users/")]
public class UsersController(IUsersService UsersService ) : ControllerBase
{
    [HttpGet]
    public async Task<List<Users>> GetUsersAsync()
    {
        return await UsersService.GetUsersAsync();
    }

    [HttpPost]
    public async Task<Response<string>> AddAsync(Users Users)
    {
        return await UsersService.AddUsersAsync(Users);
    }

    [HttpGet("{UsersId}")]
    public async Task<Response<Users>> GetUsersByIdAsync(int UsersId)
    {
        return await UsersService.GetUsersByIdAsync(UsersId);
    }

    [HttpPut]
    public async Task<Response<string>> UpdateAsync(Users Users)
    {
        return await UsersService.UpdateAsync(Users);
    }
    [HttpDelete("{UsersId}")]
    public async Task<Response<string>> DeleteAsync(int UsersId)
    {
        return await UsersService.DeleteAsync(UsersId);
    }
}