using backend.Data;
using backend.Dtos.Task;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementations;

public class TaskServices(LibraryDbContext context) : ITaskService
{
    public async Task<List<TaskDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.LibrarianTasks
            .AsNoTracking()
            .Include(t => t.Reservation)
                .ThenInclude(r => r.IssueCompartment)
                .ThenInclude(ic => ic.Locker)
            .Where(t => t.IsDone == false &&
                t.Reservation.Loan != null &&
                t.Reservation.Loan.ReturnDate == null)
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new TaskDto
            {
                Id = t.Id,
                Type = t.Type,
                CreatedAt = t.CreatedAt,
                ReservationId = t.ReservationId,
                BookId = t.Reservation.BookId,

                LockerId = t.Reservation.IssueCompartment != null
                    ? t.Reservation.IssueCompartment.LockerId
                    : null,

                LockerLocationCode = t.Reservation.IssueCompartment != null
                    ? t.Reservation.IssueCompartment.Locker.LocationCode
                    : null,
                PinCodeLibrarian = t.Reservation.IssueCompartment != null
                    ? t.Reservation.IssueCompartment.PinCodeLibrarian
                    : null,
                PinCodeReader = t.Reservation.IssueCompartment != null
                    ? t.Reservation.IssueCompartment.PinCodeReader
                    : null
            })
            .ToListAsync(cancellationToken);
        
    }
}