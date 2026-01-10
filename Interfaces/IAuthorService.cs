public interface IAuthorService
{
    Task<Response<string>> AddAuthorAsync(Author author);
    Task<List<Author>> GetAuthorAsync();
    Task<Response<Author>> GetAuthorByIdAsync(int AuthorId);
    Task<Response<string>> UpdateAsync(Author author);
    Task<Response<string>> DeleteAsync(int AuthorId);
}