using backend.Data;
using backend.Dtos.Book;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementations;

public class BookServices(LibraryDbContext dbContext) : IBookServices
{
    public async Task<List<BookDto>> GetBooksAsync()
    {
        var books = await dbContext.Books
            .Include(b => b.Genres)
            .Include(b => b.Publisher)
            .Include(b => b.PrintingHouse)
            .Include(b => b.Author)
            .ToListAsync();

        return books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            Summary = b.Summary,
            Isbn = b.Isbn,
            Language = b.Language,
            PublishedAt = b.PublishedAt,
            Author = b.Author.FullName,
            PrintingHouse = b.PrintingHouse.Name,
            Genres = b.Genres.Select(g => g.Title).ToList(),
            Publisher = b.Publisher.Name
        }).ToList();
    }
}