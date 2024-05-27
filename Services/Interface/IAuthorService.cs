using EFBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFBook.Services.Interface
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAuthorsAsync();
        Task<Author> GetAuthorByIdAsync(int authorId);
        Task<Author> AddAuthorAsync(AuthorDto authorDto);
        Task<bool> UpdateAuthorAsync(int authorId, AuthorDto authorDto);
        Task<bool> DeleteAuthorAsync(int authorId);
    }
}
