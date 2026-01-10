public interface IBookLoonService
{
    Task<Response<string>> AddBookLoonAsync(BookLoon bookLoon);
    Task<List<BookLoon>> GetBookLoonAsync();
    Task<Response<BookLoon>> GetBookLoonByIdAsync(int BookLoonId);
    Task<Response<string>> UpdateAsync(BookLoon bookLoon);
    Task<Response<string>> DeleteAsync(int BookLoonId);
}