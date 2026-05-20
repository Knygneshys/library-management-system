using backend.Data;
using backend.Dtos.Locker;
using backend.Enums;
using backend.Exceptions;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementations;

public class LockerServices(LibraryDbContext dbContext, IMailService mailService) : ILockerServices
{

    private const string EntityName = "Locker";

    public async Task<LockerDto> CreateAsync(Guid parcelLockerId, LockerCreateDto dto)
    {
        var lockerInDb = await dbContext.Lockers.FirstOrDefaultAsync(p => p.LocationCode.ToLower().Equals(dto.LocationCode.ToLower()));
        if (lockerInDb is not null)
        {
            throw new LockerByLocationCodeAlreadyExists(dto.LocationCode);
        }

        var parcelLockerInDb = await dbContext.ParcelLockers.FirstOrDefaultAsync(p => p.Id.Equals(parcelLockerId));

        if (parcelLockerInDb is null)
        {
            throw new EntityNotFoundException("Parcel locker");
        }

        var locker = new Locker()
        {
            Id = Guid.NewGuid(),
            Height = dto.Height,
            Length = dto.Length,
            Width = dto.Width,
            LocationCode = dto.LocationCode,
            ParcelLockerId = parcelLockerId,
            ParcelLocker = parcelLockerInDb,
            LockerState = LockerState.Empty,
        };

        await dbContext.Lockers.AddAsync(locker);
        await dbContext.SaveChangesAsync();

        return new LockerDto
        {
            Id = locker.Id,
            LocationCode = locker.LocationCode,
            Height = locker.Height,
            Length = locker.Length,
            Width = locker.Width,
            LockerState = locker.LockerState,
            ParcelLockerId = locker.ParcelLockerId,
        };
    }

    public async Task<List<LockerDto>> GetAllAsync()
    {
        return await dbContext.Lockers
        .Select(l => new LockerDto
        {
            Id = l.Id,
            LocationCode = l.LocationCode,
            Height = l.Height,
            Width = l.Width,
            Length = l.Length,
            LockerState = l.LockerState,
            ParcelLockerId = l.ParcelLockerId,
        })
        .ToListAsync();
    }

    public async Task<List<LockerDto>> GetLockersByParcelLockerAsync(Guid parcelLockerId)
    {
        return await dbContext.Lockers
        .Where(l => l.ParcelLockerId.Equals(parcelLockerId))
        .Select(l => new LockerDto
        {
            Id = l.Id,
            LocationCode = l.LocationCode,
            Height = l.Height,
            Width = l.Width,
            Length = l.Length,
            LockerState = l.LockerState,
            ParcelLockerId = l.ParcelLockerId,
        })
        .ToListAsync();
    }

    public async Task<LockerDto> UpdateAsync(Guid id, LockerUpdateDto dto)
    {
        var Locker = await dbContext.Lockers.FirstOrDefaultAsync(p => p.Id.Equals(id))
            ?? throw new KeyNotFoundException("Locker not found.");

        if (!Locker.LocationCode.ToLower().Equals(dto.LocationCode.ToLower()) &&
            await dbContext.Lockers.AnyAsync(p => p.LocationCode.ToLower().Equals(dto.LocationCode.ToLower())))
        {
            throw new LockerByLocationCodeAlreadyExists(dto.LocationCode);
        }

        Locker.Height = dto.Height;
        Locker.Width = dto.Width;
        Locker.Length = dto.Length;
        Locker.LocationCode = dto.LocationCode;
        Locker.ParcelLockerId = dto.ParcelLockerId;
        Locker.LockerState = dto.LockerState;

        await dbContext.SaveChangesAsync();

        return new LockerDto
        {
            Id = Locker.Id,
            LocationCode = Locker.LocationCode,
            Height = Locker.Height,
            Length = Locker.Length,
            Width = Locker.Width,
            LockerState = Locker.LockerState,
            ParcelLockerId = Locker.ParcelLockerId,
        };
    }

    public async void Delete(Guid id)
    {
        var Locker = await dbContext.Lockers.FirstOrDefaultAsync(p => p.Id.Equals(id))
            ?? throw new EntityNotFoundException(EntityName);

        dbContext.Lockers.Remove(Locker);

        await dbContext.SaveChangesAsync();
    }

    public async Task<LockerDto?> GetLockerByCodeAsync(string pinCode)
    {
        var locker = await dbContext.Lockers
            .Include(l => l.IssueCompartment)
            .FirstOrDefaultAsync(l => l.IssueCompartment != null &&
                (l.IssueCompartment.PinCodeReader == pinCode ||
                 l.IssueCompartment.PinCodeLibrarian == pinCode))
            ?? throw new EntityNotFoundException(EntityName);

        return new LockerDto
        {
            Id = locker.Id,
            LocationCode = locker.LocationCode,
            Height = locker.Height,
            Length = locker.Length,
            Width = locker.Width,
            LockerState = locker.LockerState,
            ParcelLockerId = locker.ParcelLockerId
        };
    }
    public async Task OpenLockerAsync(Guid id)
    {
        var locker = await dbContext.Lockers.FirstOrDefaultAsync(l => l.Id == id)
            ?? throw new EntityNotFoundException(EntityName);

        locker.IsDoorClosed = false;

        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsLockerClosedAsync(Guid id)
    {
        var locker = await dbContext.Lockers.FirstOrDefaultAsync(l => l.Id == id)
            ?? throw new EntityNotFoundException(EntityName);
        return locker.IsDoorClosed;
    }

    public async Task HandleLockerClosedAsync(Guid id)
    {
        var locker = await dbContext.Lockers.FirstOrDefaultAsync(l => l.Id == id)
            ?? throw new EntityNotFoundException(EntityName);

        locker.IsDoorClosed = true;

        await dbContext.SaveChangesAsync();
    }



    public async Task ResetLockerAsync(Guid lockerId, string pinCode)
    {
        var locker = await dbContext.Lockers
            .Include(l => l.IssueCompartment)
            .FirstOrDefaultAsync(l => l.Id == lockerId)
            ?? throw new EntityNotFoundException(EntityName);

        locker.ResetLockerState();
        dbContext.Lockers.Update(locker);

        var issueCompartment = IssueCompartment.GetByLocker(locker);

        if (issueCompartment.PinCodeReader == pinCode)
        {
            var reservation = await dbContext.Reservations
                .FirstOrDefaultAsync(r => r.IssueCompartmentId == issueCompartment.Id);

            if (reservation != null)
            {
                reservation.IssueBook();
                dbContext.Reservations.Update(reservation);
                // create loan
                var loan = Loan.Create(reservation);
                await dbContext.Loans.AddAsync(loan);
            }
        }
        else if (issueCompartment.PinCodeLibrarian == pinCode)
        {
            var reservation = await dbContext.Reservations
                .Include(r => r.LibraryTasks)
                .FirstOrDefaultAsync(r => r.IssueCompartmentId == issueCompartment.Id);

            if (reservation != null && reservation.CopyId.HasValue)
            {
                var copy = await dbContext.Copies.FindAsync(reservation.CopyId.Value);
                if (copy != null)
                {
                    // pazymeti egzemplioriu kaip laisva
                    copy.UpdateStatus();
                    dbContext.Copies.Update(copy);
                }

                // pazymeti is_done true
                var task = reservation.LibraryTasks.FirstOrDefault(t => t.IsIssueTask == false && t.IsDone == false);
                if (task != null)
                {
                    task.UpdateTask();
                    dbContext.LibraryTasks.Update(task);
                }
            }
        }
        else
        {
            throw new Exception("Neteisingas PIN kodas. Operacija negalima.");
        }

        dbContext.IssueCompartments.Remove(issueCompartment);
        await dbContext.SaveChangesAsync();
    }

    public async Task InsertBookAsync(Guid lockerId, string pinCode)
    {
        var locker = await dbContext.Lockers
            .Include(l => l.IssueCompartment)
            .Include(l => l.ParcelLocker)
            .FirstOrDefaultAsync(l => l.Id == lockerId)
            ?? throw new EntityNotFoundException(EntityName);

        // 4.
        locker.SetOccupied();
        dbContext.Lockers.Update(locker);

        // 6.
        var issueCompartment = IssueCompartment.GetByLocker(locker);

        // 8.
        var reservation = await dbContext.Reservations
            .Include(l => l.User)
            .Include(l => l.Book)
            .FirstOrDefaultAsync(r => r.IssueCompartmentId == issueCompartment.Id)
            ?? throw new EntityNotFoundException($"Reservation with issueCompartnment: {issueCompartment.Id}");

        // Alt
        if (issueCompartment.PinCodeReader == pinCode)
        {
            // 10.
            reservation.InsertBook();
            dbContext.Reservations.Update(reservation);
            
            // 12.
            var loan = await dbContext.Loans
                .FirstOrDefaultAsync(l => l.ReservationId == reservation.Id)
                ?? throw new EntityNotFoundException($"Loan with reservation: {reservation.Id}");

            // 14.
            loan.ReturnBook();
            dbContext.Loans.Update(loan);

            // 16. TODO: Create task Entity
            var task = LibraryTask.Create(reservation, IsIssue: false); 
            await dbContext.LibraryTasks.AddAsync(task);

        }
        else if (issueCompartment.PinCodeLibrarian == pinCode)
        {
            // 18.
            var task = reservation.LibraryTasks.FirstOrDefault(t => t.IsIssueTask == true && t.IsDone == false)
                ?? throw new EntityNotFoundException($"Issue Task with reservation: {reservation.Id}");

            // 20.
            task.UpdateTask();
            dbContext.LibraryTasks.Update(task);

            // 22. Siųsti paštą
            var user = reservation.User ?? throw new Exception($"User not found from reservation: {reservation.Id}");

            await mailService.SendLockerNotificationAsync(
                recipientEmail: user.Email,
                bookTitle: reservation.Book.Title,
                lockerAddress: locker.ParcelLocker.Address,
                openCode: pinCode,
                daysToPickUp: 3
            );
    
        }
        else
        {
            throw new Exception("Neteisingas PIN kodas. Operacija negalima.");
        }

        dbContext.IssueCompartments.Remove(issueCompartment);
        await dbContext.SaveChangesAsync();
    }

}