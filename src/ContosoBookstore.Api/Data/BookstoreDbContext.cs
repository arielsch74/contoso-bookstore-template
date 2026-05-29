using ContosoBookstore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ContosoBookstore.Api.Data;

public class BookstoreDbContext : DbContext
{
    public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : base(options) { }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(b =>
        {
            b.HasIndex(x => x.Isbn).IsUnique();
            b.Property(x => x.Price).HasPrecision(18, 2);
            b.Property(x => x.Rating).HasPrecision(3, 2);
            b.HasOne(x => x.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
