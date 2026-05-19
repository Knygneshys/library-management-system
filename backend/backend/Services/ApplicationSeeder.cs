using backend.Data;
using backend.Data.Seeding;

namespace backend.Services;

public class ApplicationSeeder
{
    private readonly IEnumerable<ISeeder> _seeders;
    public ApplicationSeeder(IEnumerable<ISeeder> seeders) => _seeders = seeders;

    public async Task SeedAllAsync(LibraryDbContext context, IServiceProvider services)
    {
        foreach (var seeder in _seeders)
        {
            await seeder.SeedAsync(context, services);
        }
    }
}