using System.ComponentModel.DataAnnotations;

namespace ContosoBookstore.Api.Models;

public class Author
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    public DateOnly? Birthdate { get; set; }
    public string? Bio { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
