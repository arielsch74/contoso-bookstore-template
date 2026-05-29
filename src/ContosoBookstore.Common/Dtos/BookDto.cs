namespace ContosoBookstore.Common.Dtos;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int PublishedYear { get; set; }
    public string? CoverImageUrl { get; set; }
    public decimal? Rating { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; } = string.Empty;
}

public class CreateBookDto
{
    public string Title { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int PublishedYear { get; set; }
    public string? CoverImageUrl { get; set; }
    public decimal? Rating { get; set; }
    public int AuthorId { get; set; }
}
