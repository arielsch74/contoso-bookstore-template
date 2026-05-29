using ContosoBookstore.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCors(opt => opt.AddDefaultPolicy(p =>
    p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

// DB: si hay connection string usa SQL Server; si no, InMemory para dev "out of the box".
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connStr))
{
    builder.Services.AddDbContext<BookstoreDbContext>(o => o.UseInMemoryDatabase("BookstoreDb"));
}
else
{
    builder.Services.AddDbContext<BookstoreDbContext>(o => o.UseSqlServer(connStr));
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();

// Seed inicial (sólo si DB vacía).
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BookstoreDbContext>();
    if (db.Database.IsRelational())
    {
        db.Database.EnsureCreated();
    }
    SeedData.Initialize(db);
}

app.Run();

// Para que WebApplicationFactory<Program> funcione en los tests.
public partial class Program { }
