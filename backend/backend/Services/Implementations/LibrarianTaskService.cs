using backend.Data;
using backend.Dtos.Task;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementations;

public class LibrarianTaskService(LibraryDbContext context) : ILibrarianTaskService
{
    public async Task<List<TaskDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.LibrarianTasks
            .AsNoTracking()
            .Include(t => t.Reservation)
            .ThenInclude(r => r.IssueCompartment)
            .ThenInclude(ic => ic.Locker)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new TaskDto
            {
                Id = t.Id,
                Type = t.Type,
                CreatedAt = t.CreatedAt,
                ReservationId = t.ReservationId,

                LockerId = t.Reservation.IssueCompartment != null
                    ? t.Reservation.IssueCompartment.LockerId
                    : null,

                LockerLocationCode = t.Reservation.IssueCompartment != null
                    ? t.Reservation.IssueCompartment.Locker.LocationCode
                    : null,

                Pin = t.Reservation.IssueCompartment != null
                    ? t.Reservation.IssueCompartment.Pin
                    : null
            })
            .ToListAsync(cancellationToken);
    }
}