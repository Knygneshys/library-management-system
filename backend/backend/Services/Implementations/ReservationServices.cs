using backend.Data;
using backend.Enums;
using backend.Exceptions;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementations;

public class ReservationServices(LibraryDbContext dbContext) : IReservationServices
{
    public async Task<bool> SetReservationAsReturningAsync(Guid reservationId)
    {
        var reservation = await dbContext.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId);
        if (reservation is null)
        {
            throw new EntityNotFoundException("Reservation");
        }

        reservation.WantsToReturn = true;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ReserveCopyAsync(Guid bookId)
    {
        var bookExists = await dbContext.Books
            .AnyAsync(b => b.Id == bookId);

        if (!bookExists)
        {
            throw new EntityNotFoundException("Book");
        }

        var freeCopy = await dbContext.Copies
            .FirstOrDefaultAsync(c => c.BookId == bookId && !c.IsTaken);

        if (freeCopy is null)
        {
            return false;
        }

        freeCopy.IsTaken = true;

        var reservation = new Reservation
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(14),
            IsExtended = false,
            WantsToReturn = false,
            State = ReservationState.InProgress,
            BookId = bookId
        };

        dbContext.Reservations.Add(reservation);

        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> GoToQueueAsync(Guid bookId)
    {
        var bookExist = await dbContext.Books.AnyAsync(b => b.Id.Equals(bookId));

        if (!bookExist)
        {
            throw new EntityNotFoundException("Book");
        }

        var reservation = new Reservation()
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            IsExtended = false,
            DueDate = DateTime.UtcNow.AddDays(14),
            WantsToReturn = false,
            State = ReservationState.InQueue,
            BookId = bookId
        };

        dbContext.Reservations.Add(reservation);

        await dbContext.SaveChangesAsync();

        return true;
    }
}