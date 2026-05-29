using ContosoBookstore.Api.Data;
using ContosoBookstore.Api.Models;
using ContosoBookstore.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoBookstore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly BookstoreDbContext _db;

    public AuthorsController(BookstoreDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll()
    {
        var authors = await _db.Authors
            .OrderBy(a => a.Name)
            .Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Birthdate = a.Birthdate,
                Bio = a.Bio,
                BookCount = a.Books.Count
            })
            .ToListAsync();
        return Ok(authors);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AuthorDto>> GetById(int id)
    {
        var a = await _db.Authors.Include(x => x.Books).FirstOrDefaultAsync(x => x.Id == id);
        if (a is null) return NotFound();
        return Ok(new AuthorDto { Id = a.Id, Name = a.Name, Birthdate = a.Birthdate, Bio = a.Bio, BookCount = a.Books.Count });
    }

    [HttpPost]
    public async Task<ActionResult<AuthorDto>> Create([FromBody] CreateAuthorDto dto)
    {
        var author = new Author { Name = dto.Name, Birthdate = dto.Birthdate, Bio = dto.Bio };
        _db.Authors.Add(author);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = author.Id }, new AuthorDto
        {
            Id = author.Id, Name = author.Name, Birthdate = author.Birthdate, Bio = author.Bio, BookCount = 0
        });
    }
}
