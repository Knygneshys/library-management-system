using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Seeding.Seeders;

public class PrintingHouseSeeder : ISeeder
{
    public async Task SeedAsync(LibraryDbContext context, IServiceProvider services)
    {
        if (await context.PrintingHouses.AnyAsync()) return;

        var houses = new List<PrintingHouse>
        {
            new PrintingHouse { Id = Guid.NewGuid(), Name = "North Star Press", Address = "12 Aurora Ave", Website = "https://northstar.example", Phone = "+37060000001" },
            new PrintingHouse { Id = Guid.NewGuid(), Name = "Riverbend Print", Address = "88 Riverside Dr", Website = "https://riverbend.example", Phone = "+37060000002" },
            new PrintingHouse { Id = Guid.NewGuid(), Name = "Oak & Ink", Address = "5 Oak Street", Website = "https://oakandink.example", Phone = "+37060000003" }
        };

        context.PrintingHouses.AddRange(houses);
        await context.SaveChangesAsync();
    }
}