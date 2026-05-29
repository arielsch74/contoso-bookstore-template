using System.ComponentModel.DataAnnotations;

namespace ContosoBookstore.Api.Models;

public class Book
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(20)]
    public string Isbn { get; set; } = string.Empty;

    public decimal Price { get; set; }
    public int PublishedYear { get; set; }
    public string? CoverImageUrl { get; set; }
    public decimal? Rating { get; set; }

    public int AuthorId { get; set; }
    public Author? Author { get; set; }
}
