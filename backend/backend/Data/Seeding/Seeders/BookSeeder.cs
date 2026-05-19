using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Seeding.Seeders;

public class BookSeeder : ISeeder
{
    public async Task SeedAsync(LibraryDbContext context, IServiceProvider services)
    {
        if (await context.Books.AnyAsync()) return;

        var authors = await context.Authors.ToListAsync();
        var publishers = await context.Publishers.ToListAsync();
        var printingHouses = await context.PrintingHouses.ToListAsync();
        var genres = await context.Genres.ToListAsync();

        if (authors.Count == 0 || publishers.Count == 0 || printingHouses.Count == 0 || genres.Count == 0)
            return;

        var books = new List<Book>
        {
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "The Silent Spring",
                Summary = "Environmental classic.",
                Isbn = "9780000000001",
                Language = "English",
                PublishedAt = new DateTime(1962,9,27),
                AuthorId = authors[0].Id,
                PrintingHouseId = printingHouses[0].Id,
                PublisherId = publishers[0].Id,
                Genres = new List<Genre> { genres[0] }
            },
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "Mystery of the Old Manor",
                Summary = "A classic mystery novel.",
                Isbn = "9780000000002",
                Language = "English",
                PublishedAt = new DateTime(1995,5,10),
                AuthorId = authors[1].Id,
                PrintingHouseId = printingHouses[1].Id,
                PublisherId = publishers[1].Id,
                Genres = new List<Genre> { genres[1], genres[2] }
            },
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "Journey to Mars",
                Summary = "Science fiction adventure.",
                Isbn = "9780000000003",
                Language = "English",
                PublishedAt = new DateTime(2010,7,21),
                AuthorId = authors[2].Id,
                PrintingHouseId = printingHouses[2].Id,
                PublisherId = publishers[0].Id,
                Genres = new List<Genre> { genres[3] }
            },
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "Tales of Fantasy",
                Summary = "A collection of fantasy short stories.",
                Isbn = "9780000000004",
                Language = "English",
                PublishedAt = new DateTime(2001,3,15),
                AuthorId = authors[3].Id,
                PrintingHouseId = printingHouses[0].Id,
                PublisherId = publishers[1].Id,
                Genres = new List<Genre> { genres[4] }
            },
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "Love in the Time of Code",
                Summary = "Modern romance.",
                Isbn = "9780000000005",
                Language = "English",
                PublishedAt = new DateTime(2020,2,14),
                AuthorId = authors[4].Id,
                PrintingHouseId = printingHouses[1].Id,
                PublisherId = publishers[0].Id,
                Genres = new List<Genre> { genres[5] }
            },
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "A Short History of Everything",
                Summary = "Popular science overview.",
                Isbn = "9780000000006",
                Language = "English",
                PublishedAt = new DateTime(1999,11,1),
                AuthorId = authors[0].Id,
                PrintingHouseId = printingHouses[2].Id,
                PublisherId = publishers[1].Id,
                Genres = new List<Genre> { genres[6] }
            },
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "Historical Voices",
                Summary = "Collected historical essays.",
                Isbn = "9780000000007",
                Language = "English",
                PublishedAt = new DateTime(1988,6,30),
                AuthorId = authors[1].Id,
                PrintingHouseId = printingHouses[2].Id,
                PublisherId = publishers[0].Id,
                Genres = new List<Genre> { genres[7] }
            }
        };

        context.Books.AddRange(books);
        await context.SaveChangesAsync();
    }
}