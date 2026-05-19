using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Seeding.Seeders;

public class PublisherSeeder : ISeeder
{
    public async Task SeedAsync(LibraryDbContext context, IServiceProvider services)
    {
        if (await context.Publishers.AnyAsync()) return;

        var pubs = new List<Publisher>
        {
            new Publisher { Id = Guid.NewGuid(), Name = "Horizon Books", Address = "1 Main St", Website = "https://horizon.example", Phone = "+37061111111" },
            new Publisher { Id = Guid.NewGuid(), Name = "Beacon Publishing", Address = "200 Market Rd", Website = "https://beacon.example", Phone = "+37062222222" }
        };

        context.Publishers.AddRange(pubs);
        await context.SaveChangesAsync();
    }
}