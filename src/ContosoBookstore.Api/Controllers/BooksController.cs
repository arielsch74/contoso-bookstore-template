using ContosoBookstore.Api.Data;
using ContosoBookstore.Api.Models;
using ContosoBookstore.Common.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContosoBookstore.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BookstoreDbContext _db;

    public BooksController(BookstoreDbContext db) => _db = db;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAll()
    {
        var books = await _db.Books
            .Include(b => b.Author)
            .OrderBy(b => b.Title)
            .Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Isbn = b.Isbn,
                Price = b.Price,
                PublishedYear = b.PublishedYear,
                CoverImageUrl = b.CoverImageUrl,
                Rating = b.Rating,
                AuthorId = b.AuthorId,
                AuthorName = b.Author!.Name
            })
            .ToListAsync();
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BookDto>> GetById(int id)
    {
        var b = await _db.Books.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == id);
        if (b is null) return NotFound();
        return Ok(new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            Isbn = b.Isbn,
            Price = b.Price,
            PublishedYear = b.PublishedYear,
            CoverImageUrl = b.CoverImageUrl,
            Rating = b.Rating,
            AuthorId = b.AuthorId,
            AuthorName = b.Author!.Name
        });
    }

    [HttpPost]
    public async Task<ActionResult<BookDto>> Create([FromBody] CreateBookDto dto)
    {
        var author = await _db.Authors.FindAsync(dto.AuthorId);
        if (author is null) return BadRequest($"Author {dto.AuthorId} not found");

        var book = new Book
        {
            Title = dto.Title,
            Isbn = dto.Isbn,
            Price = dto.Price,
            PublishedYear = dto.PublishedYear,
            CoverImageUrl = dto.CoverImageUrl,
            Rating = dto.Rating,
            AuthorId = dto.AuthorId
        };
        _db.Books.Add(book);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = book.Id }, new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Isbn = book.Isbn,
            Price = book.Price,
            PublishedYear = book.PublishedYear,
            CoverImageUrl = book.CoverImageUrl,
            Rating = book.Rating,
            AuthorId = book.AuthorId,
            AuthorName = author.Name
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateBookDto dto)
    {
        var book = await _db.Books.FindAsync(id);
        if (book is null) return NotFound();
        book.Title = dto.Title;
        book.Isbn = dto.Isbn;
        book.Price = dto.Price;
        book.PublishedYear = dto.PublishedYear;
        book.CoverImageUrl = dto.CoverImageUrl;
        book.Rating = dto.Rating;
        book.AuthorId = dto.AuthorId;
        await _db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _db.Books.FindAsync(id);
        if (book is null) return NotFound();
        _db.Books.Remove(book);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
