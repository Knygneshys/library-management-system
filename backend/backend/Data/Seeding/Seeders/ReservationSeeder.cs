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
                CreatedAt = now.AddDays(-3),
                BookId = books[0].Id,
                DueDate = now.AddDays(5), 
                IsExtended = false,
                WantsToReturn = false,
                State = ReservationState.NotLate
            },
            new Reservation
            {
                Id = Guid.NewGuid(),
                CreatedAt = now.AddDays(-2),
                BookId = books[0].Id,
                DueDate = now.AddDays(10),
                IsExtended = false,
                WantsToReturn = false,
                State = ReservationState.InQueue
            },
            new Reservation
            {
                Id = Guid.NewGuid(),
                CreatedAt = now.AddDays(-1),
                BookId = books[0].Id,
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