using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Seeding.Seeders;

public class UserSeeder : ISeeder
{
    public async Task SeedAsync(LibraryDbContext context, IServiceProvider services)
    {
        if (await context.Users.AnyAsync()) return;

        var users = new List<User>
        {
            new User { Id = Guid.NewGuid(), FullName = "Jaunikis Jonas", Email = "gytis.kaulakis@gmail.com", Deactivated = false}
        };

        context.Users.AddRange(users);
        await context.SaveChangesAsync();
    }
}