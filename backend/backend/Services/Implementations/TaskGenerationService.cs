using backend.Data;
using backend.Enums;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementations;

public class TaskGenerationService(LibraryDbContext dbContext) : ITaskGenerationServices
{
    public async Task GenerateTasksAsync(CancellationToken cancellationToken)
    {
        var reservations = await dbContext.Reservations
            .Include(r => r.LibrarianTask)
            .Where(r => r.LibrarianTask == null)
            .ToListAsync(cancellationToken);

        foreach (var reservation in reservations)
        {
            var freeLocker = await dbContext.Lockers
                .FirstOrDefaultAsync(l => l.LockerState == LockerState.Empty, cancellationToken);

            if (freeLocker is null)
                break;

            var pin = GeneratePin();

            if (reservation.State == ReservationState.InQueue)
            {
                dbContext.IssueCompartments.Add(new IssueCompartment
                {
                    Id = Guid.NewGuid(),
                    Type = IssueCompartmentType.Issue,
                    Pin = pin,
                    LockerId = freeLocker.Id,
                    ReservationId = reservation.Id,
                });

                freeLocker.LockerState = LockerState.Occupied;

                dbContext.LibrarianTasks.Add(new LibrarianTask
                {
                    Id = Guid.NewGuid(),
                    Type = LibrarianTaskType.Issue,
                    CreatedAt = DateTime.UtcNow,
                    ReservationId = reservation.Id,
                });
            }
            else if (reservation.WantsToReturn)
            {
                dbContext.IssueCompartments.Add(new IssueCompartment
                {
                    Id = Guid.NewGuid(),
                    Type = IssueCompartmentType.Return,
                    Pin = pin,
                    LockerId = freeLocker.Id,
                    ReservationId = reservation.Id,
                });

                freeLocker.LockerState = LockerState.Occupied;

                dbContext.LibrarianTasks.Add(new LibrarianTask
                {
                    Id = Guid.NewGuid(),
                    Type = LibrarianTaskType.Return,
                    CreatedAt = DateTime.UtcNow,
                    ReservationId = reservation.Id,
                });
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static string GeneratePin() =>
        Random.Shared.Next(1000, 9999).ToString();
}