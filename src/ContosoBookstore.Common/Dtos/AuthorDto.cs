namespace ContosoBookstore.Common.Dtos;

public class AuthorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly? Birthdate { get; set; }
    public string? Bio { get; set; }
    public int BookCount { get; set; }
}

public class CreateAuthorDto
{
    public string Name { get; set; } = string.Empty;
    public DateOnly? Birthdate { get; set; }
    public string? Bio { get; set; }
}
