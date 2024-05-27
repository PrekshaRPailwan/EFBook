using EFBook.Models;
using EFBook.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace EFBook.Services.Services
{
    public class BookService : IBookService
    {
        private readonly EbookManagementContext _context;

        public BookService(EbookManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await _context.Books.Where(a => a.IsPresent == true).ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> AddBookAsync(BookDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Description = bookDto.Description,
                Isbn = bookDto.Isbn,
                PublicationDate = bookDto.PublicationDate,
                Price = bookDto.Price,
                Language = bookDto.Language,
                Publisher = bookDto.Publisher,
                PageCount = bookDto.PageCount,
                AverageRating = bookDto.AverageRating,
                GenreId = bookDto.GenreId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            var authors = await _context.Authors
                .Where(a => bookDto.AuthorIds.Contains(a.AuthorId))
                .ToListAsync();

            book.Authors = authors;
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<bool> UpdateBookAsync(int id, BookDto bookDto)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                return false;
            }

            // Update book properties
            book.Title = bookDto.Title;
            book.Description = bookDto.Description;
            book.Isbn = bookDto.Isbn;
            book.PublicationDate = bookDto.PublicationDate;
            book.Price = bookDto.Price;
            book.Language = bookDto.Language;
            book.Publisher = bookDto.Publisher;
            book.PageCount = bookDto.PageCount;
            book.AverageRating = bookDto.AverageRating;
            book.GenreId = bookDto.GenreId;
            book.UpdatedAt = DateTime.UtcNow;

            // Update authors
            var authors = await _context.Authors
                .Where(a => bookDto.AuthorIds.Contains(a.AuthorId))
                .ToListAsync();

            book.Authors.Clear();
            foreach (var author in authors)
            {
                book.Authors.Add(author);
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }

            book.IsPresent = false;
            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
