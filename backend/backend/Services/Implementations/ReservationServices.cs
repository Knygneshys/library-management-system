using backend.Data;
using backend.Exceptions;
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
}