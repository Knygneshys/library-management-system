using backend.Data;
using backend.Models;
using backend.Enums;
using Microsoft.EntityFrameworkCore;

namespace backend.Data.Seeding.Seeders;

public class ReservationSeeder : ISeeder
{
    public async Task SeedAsync(LibraryDbContext context, IServiceProvider services)
    {
        if (await context.Reservations.AnyAsync()) return;

        var books = await context.Books.Take(3).ToListAsync();
        if (!books.Any()) return;

        var now = DateTime.UtcNow;

        var reservations = new List<Reservation>
        {
            new Reservation
            {
                Id = Guid.NewGuid(),
                BookId = books[0].Id,
                DueDate = now.AddDays(5), 
                IsExtended = false,
                WantsToReturn = false,
                State = ReservationState.NotLate
            },
            new Reservation
            {
                Id = Guid.NewGuid(),
                BookId = books.Count > 1 ? books[1].Id : books[0].Id,
                DueDate = now.AddDays(10),
                IsExtended = false,
                WantsToReturn = false,
                State = ReservationState.InQueue
            },
            new Reservation
            {
                Id = Guid.NewGuid(),
                BookId = books.Count > 2 ? books[2].Id : books[0].Id,
                DueDate = now.AddDays(15),
                IsExtended = false,
                WantsToReturn = false,
                State = ReservationState.InQueue
            }
        };

        context.Reservations.AddRange(reservations);
        await context.SaveChangesAsync();
    }
}