using System.Net;
using System.Net.Http.Json;
using ContosoBookstore.Common.Dtos;
using Microsoft.AspNetCore.Mvc.Testing;

namespace ContosoBookstore.Api.Tests;

public class BooksControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public BooksControllerTests(WebApplicationFactory<Program> factory) => _factory = factory;

    [Fact]
    public async Task GetAll_Returns200_AndAtLeastOneBookFromSeed()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/books");
        response.EnsureSuccessStatusCode();
        var books = await response.Content.ReadFromJsonAsync<List<BookDto>>();
        Assert.NotNull(books);
        Assert.NotEmpty(books!);
    }

    [Fact]
    public async Task GetById_NonExistent_Returns404()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/books/99999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Health_Returns200()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/health");
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Config_Returns200_WithExpectedKeys()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/config");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        Assert.Contains("newBookListing", json);
        Assert.Contains("appName", json);
    }
}
