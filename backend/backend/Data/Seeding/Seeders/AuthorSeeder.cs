using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Seeding.Seeders;

public class AuthorSeeder : ISeeder
{
    public async Task SeedAsync(LibraryDbContext context, IServiceProvider services)
    {
        if (await context.Authors.AnyAsync()) return;

        var authors = new List<Author>
        {
            new Author { Id = Guid.NewGuid(), FullName = "Rachel Carson", Nationality = "American", Biography = "Marine biologist and conservationist." },
            new Author { Id = Guid.NewGuid(), FullName = "Arthur Conan Doyle", Nationality = "British", Biography = "Author of detective stories." },
            new Author { Id = Guid.NewGuid(), FullName = "Kim Stanley Robinson", Nationality = "American", Biography = "Science fiction author." },
            new Author { Id = Guid.NewGuid(), FullName = "Ursula K. Le Guin", Nationality = "American", Biography = "Fantasy and science fiction author." },
            new Author { Id = Guid.NewGuid(), FullName = "Jane Austen", Nationality = "British", Biography = "Romantic novelist." }
        };

        context.Authors.AddRange(authors);
        await context.SaveChangesAsync();
    }
}