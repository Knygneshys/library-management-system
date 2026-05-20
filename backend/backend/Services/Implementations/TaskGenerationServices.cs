using backend.Data;
using backend.Enums;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementations;

public class TaskGenerationServices(LibraryDbContext dbContext) : ITaskGenerationServices
{
    public async Task GenerateTasksAsync(CancellationToken cancellationToken)
    {
        var reservations = await dbContext.Reservations
            .Include(r => r.LibrarianTasks)
            .Include(r => r.IssueCompartment)
            .Where(r =>
                !r.LibrarianTasks.Any() &&
                r.IssueCompartment == null &&
                (
                    r.State == ReservationState.InQueue ||
                    r.State == ReservationState.InProgress ||
                    r.WantsToReturn
                ))
            .ToListAsync(cancellationToken);

        var freeLockers = await dbContext.Lockers
            .Where(l => l.LockerState == LockerState.Empty)
            .ToListAsync(cancellationToken);

        foreach (var reservation in reservations)
        {
            if (freeLockers.Count == 0)
                break;

            var randomIndex = Random.Shared.Next(freeLockers.Count);
            var freeLocker = freeLockers[randomIndex];
            freeLockers.RemoveAt(randomIndex);

            if (reservation.State == ReservationState.InQueue ||
                reservation.State == ReservationState.InProgress)
            {
                CreateIssueTask(reservation, freeLocker);
            }
            else if (reservation.WantsToReturn)
            {
                CreateReturnTask(reservation, freeLocker);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private void CreateIssueTask(Reservation reservation, Locker freeLocker)
    {
        dbContext.IssueCompartments.Add(new IssueCompartment
        {
            Id = Guid.NewGuid(),
            Type = IssueCompartmentType.Issue,
            PinCodeReader = GeneratePin(),
            PinCodeLibrarian = GeneratePin(),
            LockerId = freeLocker.Id,
            ReservationId = reservation.Id,
        });

        freeLocker.LockerState = LockerState.Occupied;

        dbContext.TaskService.Add(new LibrarianTask
        {
            Id = Guid.NewGuid(),
            Type = LibrarianTaskType.Issue,
            CreatedAt = DateTime.UtcNow,
            ReservationId = reservation.Id,
        });
    }

    private void CreateReturnTask(Reservation reservation, Locker freeLocker)
    {
        dbContext.IssueCompartments.Add(new IssueCompartment
        {
            Id = Guid.NewGuid(),
            Type = IssueCompartmentType.Return,
            PinCodeReader = GeneratePin(),
            PinCodeLibrarian = GeneratePin(),
            LockerId = freeLocker.Id,
            ReservationId = reservation.Id,
        });

        freeLocker.LockerState = LockerState.Occupied;

        dbContext.TaskService.Add(new LibrarianTask
        {
            Id = Guid.NewGuid(),
            Type = LibrarianTaskType.Return,
            CreatedAt = DateTime.UtcNow,
            ReservationId = reservation.Id,
        });
    }

    private static string GeneratePin() =>
        Random.Shared.Next(1000, 10000).ToString();
}