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

        var locker = await context.Lockers.FirstOrDefaultAsync();

        var user = await context.Users.FirstOrDefaultAsync();
        if (!books.Any() || locker == null || user == null) return;

        var now = DateTime.UtcNow;

        var copy = await context.Copies.FirstOrDefaultAsync(c => c.BookId == books[0].Id);
        if (copy == null) return;

        var compartment = new IssueCompartment
        {
            Id = Guid.NewGuid(),
            LockerId = locker.Id,
            InsertionDate = DateTime.UtcNow,
            PinCodeReader = "1234",
            PinCodeLibrarian = "9999"
        };
        context.IssueCompartments.Add(compartment);

        var reservations = new List<Reservation>
        {
            new Reservation
            {
                Id = Guid.NewGuid(),
                CreatedAt = now.AddDays(-3),
                BookId = books[0].Id,
                CopyId = copy.Id,
                IssueCompartmentId = compartment.Id, // knyga atsiemimui
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
                UserId = user.Id,
                DueDate = now.AddDays(15),
                IsExtended = false,
                WantsToReturn = false,
                State = ReservationState.InQueue
            }
        };

        context.Reservations.AddRange(reservations);

        copy.IsTaken = true;
        context.Copies.Update(copy);

        locker.LockerState = LockerState.Occupied;
        context.Lockers.Update(locker);

        await context.SaveChangesAsync();
    }
}