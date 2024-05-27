using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EFBook.Models;
using EFBook.Services.Interface;

namespace EFBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: api/Authors
        [HttpGet("GetAllAuthors")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authors = await _authorService.GetAuthorsAsync();
            return Ok(authors);
        }

        // GET: api/Authors/5
        [HttpGet("GetAuthorById")]
        public async Task<ActionResult<Author>> GetAuthor(int authorId)
        {
            var author = await _authorService.GetAuthorByIdAsync(authorId);
            if (author == null)
            {
                return NotFound("Author does not exists ");
            }

            return Ok(author);
        }

        // POST: api/Authors
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(AuthorDto authorDto)
        {
            var author = await _authorService.AddAuthorAsync(authorDto);
            return CreatedAtAction(nameof(GetAuthor), new { id = author.AuthorId }, author);
        }

        // PUT: api/Authors/5
        [HttpPut("UpdateAuthor")]
        public async Task<IActionResult> PutAuthor(int authorId, AuthorDto authorDto)
        {
            try
            {
                await _authorService.UpdateAuthorAsync(authorId, authorDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Author does not exists ");
            }

            return Ok(authorDto);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                await _authorService.DeleteAuthorAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Author does not exists ");
            }

            return Ok("Author Deleted sucessfully");
        }
    }
}
