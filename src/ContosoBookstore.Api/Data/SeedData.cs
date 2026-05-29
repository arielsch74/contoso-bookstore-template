using ContosoBookstore.Api.Models;

namespace ContosoBookstore.Api.Data;

public static class SeedData
{
    public static void Initialize(BookstoreDbContext db)
    {
        if (db.Authors.Any()) return;

        var orwell = new Author { Name = "George Orwell", Birthdate = new DateOnly(1903, 6, 25), Bio = "British novelist and essayist." };
        var huxley = new Author { Name = "Aldous Huxley", Birthdate = new DateOnly(1894, 7, 26), Bio = "English writer and philosopher." };
        var bradbury = new Author { Name = "Ray Bradbury", Birthdate = new DateOnly(1920, 8, 22), Bio = "American author and screenwriter." };
        var asimov = new Author { Name = "Isaac Asimov", Birthdate = new DateOnly(1920, 1, 2), Bio = "Russian-American writer and biochemist." };

        db.Authors.AddRange(orwell, huxley, bradbury, asimov);
        db.SaveChanges();

        db.Books.AddRange(
            new Book { Title = "1984", Isbn = "978-0451524935", Price = 12.99m, PublishedYear = 1949, AuthorId = orwell.Id, Rating = 4.7m, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/978-0451524935-M.jpg" },
            new Book { Title = "Animal Farm", Isbn = "978-0451526342", Price = 9.99m, PublishedYear = 1945, AuthorId = orwell.Id, Rating = 4.5m, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/978-0451526342-M.jpg" },
            new Book { Title = "Brave New World", Isbn = "978-0060850524", Price = 14.50m, PublishedYear = 1932, AuthorId = huxley.Id, Rating = 4.4m, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/978-0060850524-M.jpg" },
            new Book { Title = "Fahrenheit 451", Isbn = "978-1451673319", Price = 11.99m, PublishedYear = 1953, AuthorId = bradbury.Id, Rating = 4.6m, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/978-1451673319-M.jpg" },
            new Book { Title = "Foundation", Isbn = "978-0553293357", Price = 13.50m, PublishedYear = 1951, AuthorId = asimov.Id, Rating = 4.5m, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/978-0553293357-M.jpg" },
            new Book { Title = "I, Robot", Isbn = "978-0553382563", Price = 10.99m, PublishedYear = 1950, AuthorId = asimov.Id, Rating = 4.4m, CoverImageUrl = "https://covers.openlibrary.org/b/isbn/978-0553382563-M.jpg" }
        );
        db.SaveChanges();
    }
}
