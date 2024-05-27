using EFBook.Models;
using EFBook.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFBook.Services.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly EbookManagementContext _context;

        public AuthorService(EbookManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _context.Authors.Where(a => a.IsActive == true).ToListAsync();

        }

        public async Task<Author> GetAuthorByIdAsync(int authorId)
        {
            return await _context.Authors.FindAsync(authorId);
        }

        public async Task<Author> AddAuthorAsync(AuthorDto authorDto)
        {
            var author = new Author
            {
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
                Biography = authorDto.Biography,
                Birthdate = authorDto.Birthdate.HasValue ? DateOnly.FromDateTime(authorDto.Birthdate.Value) : null,
                Country = authorDto.Country,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return author;
        }

        public async Task<bool> UpdateAuthorAsync(int authorId, AuthorDto authorDto)
        {
            var author = await _context.Authors.FindAsync(authorId);
            if (author == null)
            {
                throw new KeyNotFoundException("Author not found");
            }

            author.FirstName = authorDto.FirstName;
            author.LastName = authorDto.LastName;
            author.Biography = authorDto.Biography;
            author.Birthdate = authorDto.Birthdate.HasValue ? DateOnly.FromDateTime(authorDto.Birthdate.Value) : null;
            author.Country = authorDto.Country;
            author.UpdatedAt = DateTime.UtcNow;

            _context.Entry(author).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAuthorAsync(int authorId)
        {
            var author = await _context.Authors.FindAsync(authorId);
            if (author == null || author.IsActive == false)
            {
                throw new KeyNotFoundException("Author not found");
            }

            author.IsActive = false;
            _context.Entry(author).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
