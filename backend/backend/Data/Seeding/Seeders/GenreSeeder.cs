using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Seeding.Seeders;

public class GenreSeeder : ISeeder
{
    public async Task SeedAsync(LibraryDbContext context, IServiceProvider services)
    {
        if (await context.Genres.AnyAsync()) return;

        var genres = new List<Genre>
        {
            new Genre { Id = Guid.NewGuid(), Title = "Environment" },
            new Genre { Id = Guid.NewGuid(), Title = "Mystery" },
            new Genre { Id = Guid.NewGuid(), Title = "Crime" },
            new Genre { Id = Guid.NewGuid(), Title = "Science Fiction" },
            new Genre { Id = Guid.NewGuid(), Title = "Fantasy" },
            new Genre { Id = Guid.NewGuid(), Title = "Romance" },
            new Genre { Id = Guid.NewGuid(), Title = "Science" },
            new Genre { Id = Guid.NewGuid(), Title = "History" }
        };

        context.Genres.AddRange(genres);
        await context.SaveChangesAsync();
    }
}