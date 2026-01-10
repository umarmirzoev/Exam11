public interface IUsersService
{
    Task<Response<string>> AddUsersAsync(Users users);
    Task<List<Users>> GetUsersAsync();
    Task<Response<Users>> GetUsersByIdAsync(int users);
    Task<Response<string>> UpdateAsync(Users users);
    Task<Response<string>> DeleteAsync(int UserId);
}