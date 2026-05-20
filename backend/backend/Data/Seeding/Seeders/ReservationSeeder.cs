using backend.Data;
using backend.Enums;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;

namespace backend.Data.Seeding.Seeders;

public class ReservationSeeder : ISeeder
{
    public async Task SeedAsync(LibraryDbContext context, IServiceProvider services)
    {
        if (await context.Reservations.AnyAsync()) return;

        var books = await context.Books.Take(3).ToListAsync();

        var lockers = await context.Lockers.Take(2).ToListAsync();

        var user = await context.Users.FirstOrDefaultAsync();
        if (!books.Any() || !lockers.Any() == null || user == null) return;

        var now = DateTime.UtcNow;

        var copy = await context.Copies.FirstOrDefaultAsync(c => c.BookId == books[0].Id);
        if (copy == null) return;

        var compartments = new List<IssueCompartment>
        {
            new IssueCompartment {
                Id = Guid.NewGuid(),
                LockerId = lockers[0].Id,
                InsertionDate = DateTime.UtcNow,
                PinCodeReader = "1234",
                PinCodeLibrarian = "9999"
            },
            new IssueCompartment {
                Id = Guid.NewGuid(),
                LockerId = lockers[1].Id,
                PinCodeReader = "0001",
                PinCodeLibrarian = "0002"
            }
        };
        context.IssueCompartments.AddRange(compartments);

        var reservations = new List<Reservation>
        {
            new Reservation
            {
                Id = Guid.NewGuid(),
                CreatedAt = now.AddDays(-3),
                BookId = books[0].Id,
                IssueCompartmentId = compartments[0].Id, // knyga atsiemimui
                UserId = user.Id,
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
                UserId = user.Id,
                IsExtended = false,
                WantsToReturn = false,
                State = ReservationState.InQueue
            },
            new Reservation
            {
                Id = Guid.NewGuid(),
                CreatedAt = now.AddDays(-1),
                BookId = books[0].Id,
                UserId = user.Id,
                IsExtended = false,
                WantsToReturn = false,
                State = ReservationState.InQueue
            }
        };

        context.Reservations.AddRange(reservations);

        copy.IsTaken = true;
        context.Copies.Update(copy);

        lockers[0].LockerState = LockerState.Occupied;
        context.Lockers.Update(lockers[0]);

        await context.SaveChangesAsync();
    }
}