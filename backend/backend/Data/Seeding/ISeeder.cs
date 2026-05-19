namespace backend.Data.Seeding;

public interface ISeeder
{
    Task SeedAsync(LibraryDbContext context, IServiceProvider services);
}