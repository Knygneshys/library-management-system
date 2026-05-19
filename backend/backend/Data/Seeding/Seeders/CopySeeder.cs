using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Seeding.Seeders;

public class CopySeeder : ISeeder
{
    public async Task SeedAsync(LibraryDbContext context, IServiceProvider services)
    {
        if (await context.Copies.AnyAsync()) return;

        var books = await context.Books.ToListAsync();
        var copies = new List<Copy>();

        foreach (var book in books)
        {
            for (var i = 0; i < 5; i++)
            {
                copies.Add(new Copy
                {
                    Id = Guid.NewGuid(),
                    BookId = book.Id,
                    Code = $"{book.Isbn}-{i+1}",
                    IsTaken = false
                });
            }
        }

        if (copies.Any())
        {
            context.Copies.AddRange(copies);
            await context.SaveChangesAsync();
        }
    }
}