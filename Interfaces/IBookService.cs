public interface IBookService
{
    Task<Response<string>> AddBookAsync(Book book);
    Task<List<Book>> GetBookAsync();
    Task<Response<Book>> GetBookByIdAsync(int BookId);
    Task<Response<string>> UpdateAsync(Book book);
    Task<Response<string>> DeleteAsync(int BookId);
}