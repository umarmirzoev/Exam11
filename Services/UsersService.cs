using Dapper;
using Npgsql;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;
public class UsersService(ApplicationDBContext applicationDbContext): IUsersService
{
    private readonly ApplicationDBContext _dbContext = applicationDbContext;
    //Add
    public async Task<Response<string>> AddUsersAsync(Users users)
    {
        using var conn = _dbContext.Connection();
        var query = "insert into users(fullname,email) values(@fullname,@email)";
        var res = await conn.ExecuteAsync(query, new {fullname = users.Fullname,email=users.Email});
        return res==0
        ? new Response<string>(HttpStatusCode.InternalServerError, "Something went wrong!")
        : new Response<string>(HttpStatusCode.OK, "Users added successfully!");
    }

    //Delete
    public async Task<Response<string>> DeleteAsync(int UsersId)
    {
        try{
        using var context = _dbContext.Connection();
        var query = "delete from users where id = @id";
        var res = await context.ExecuteAsync(query,new{id=UsersId});
        return res==0
            ?new Response<string>(HttpStatusCode.InternalServerError, "Users data not deleted!")
            :new Response<string>(HttpStatusCode.OK, "Users data successfully deleted!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    //GetbyId
    public async Task<Response<Users>> GetUsersByIdAsync(int UsersId)
    {
        try{
        using var conn = _dbContext.Connection();
        var query = "select * from users where id = @id";
        var result = await conn.QueryFirstOrDefaultAsync<Users>(query, new{id=UsersId});
        return result==null
                ?new Response<Users>(HttpStatusCode.InternalServerError, "Users not found!")
                :new Response<Users>(HttpStatusCode.OK, "Users found!", result);
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<Users>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
    //Get
    public async Task<List<Users>> GetUsersAsync()
    {
        using var conn = _dbContext.Connection();
        var query = "select * from Users";
        var res = await conn.QueryAsync<Users>(query);
        return res.ToList();
    }

    //Update
    public async Task<Response<string>> UpdateAsync(Users users)
    {
        try
        {
            using var context = _dbContext.Connection();
            var query = "update users set fullname = @fullname,email = @email where id = @id";
            var result = await context.ExecuteAsync(query, new{fullname = users.Fullname, email=users.Email, id = users.Id});
            return result==0
                ?new Response<string>(HttpStatusCode.InternalServerError, "Users data not updated!")
                :new Response<string>(HttpStatusCode.OK, "Users data successfully updated!");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

}