using backend.Data;
using backend.Dtos.Locker;
using backend.Exceptions;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementations;

public class LockerServices(LibraryDbContext dbContext) : ILockerServices
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








    // 5: getLockerByCode()
    public async Task<LockerDto?> GetLockerByCodeAsync(string pinCode)
    {
        var locker = await dbContext.Lockers
            .Include(l => l.IssueCompartment)
            .FirstOrDefaultAsync(l => l.IssueCompartment != null &&
                (l.IssueCompartment.PinCodeReader == pinCode ||
                 l.IssueCompartment.PinCodeLibrarian == pinCode));

        if (locker == null)
        {
            return null; 
        }

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
    //7: openLocker() viduje
    public async Task OpenLockerAsync(Guid id)
    {
        var locker = await dbContext.Lockers.FirstOrDefaultAsync(l => l.Id == id)
            ?? throw new EntityNotFoundException(EntityName);

        locker.IsDoorClosed = false;

        await dbContext.SaveChangesAsync();
    }

    // 11: isLockerClosed()
    public async Task<bool> IsLockerClosedAsync(Guid id)
    {
        var locker = await dbContext.Lockers.FirstOrDefaultAsync(l => l.Id == id)
            ?? throw new EntityNotFoundException(EntityName);
        return locker.IsDoorClosed;
    }

    // 10: handleLockerClosed()
    public async Task HandleLockerClosedAsync(Guid id)
    {
        var locker = await dbContext.Lockers.FirstOrDefaultAsync(l => l.Id == id)
            ?? throw new EntityNotFoundException(EntityName);

        // Žmogus uždarė dureles
        locker.IsDoorClosed = true;

        await dbContext.SaveChangesAsync();
    }






    // PASIIMTI KNYGA
    public async Task ResetLockerAsync(Guid lockerId, string pinCode)
    {
        var locker = await dbContext.Lockers
            .Include(l => l.IssueCompartment)
            .FirstOrDefaultAsync(l => l.Id == lockerId)
            ?? throw new EntityNotFoundException(EntityName);

        // 4: reset()
        ResetLockerState(locker);

        // 6: getByLocker() ir 7: issueCompartment

        var issueCompartment = GetByLocker(locker);

        if (issueCompartment.PinCodeReader == pinCode)
        {
            var reservation = await dbContext.Reservations
                .FirstOrDefaultAsync(r => r.IssueCompartmentId == issueCompartment.Id);

            if (reservation != null)
            {
                // 10: update() 
                UpdateReservation(reservation);

                // 12: create() ir 13: loan
                await CreateLoanAsync(reservation);
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
                    // 18: updateStatus() - pazymeti egzemplioriu kaip laisva
                    UpdateCopyStatus(copy);
                }

                // 20: update() (Task)
                var task = reservation.LibraryTasks.FirstOrDefault(t => t.IsIssueTask == false && t.IsDone == false);
                if (task != null)
                {
                    UpdateTask(task);
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

    //pagalbiniai reikia iskelt cj

    private void ResetLockerState(Locker locker)
    {
        locker.IsDoorClosed = true;
        locker.LockerState = LockerState.Empty;
        dbContext.Lockers.Update(locker);
    }
    private IssueCompartment GetByLocker(Locker locker)
    {
        return locker.IssueCompartment ?? throw new Exception("");
    }
    private void UpdateReservation(Reservation reservation)
    {
        reservation.State = ReservationState.Completed;
        dbContext.Reservations.Update(reservation);
    }

    private async Task CreateLoanAsync(Reservation reservation)
    {
        var loan = new Loan
        {
            Id = Guid.NewGuid(),
            LoanDate = DateTime.UtcNow,
            ReturnDate = DateTime.UtcNow.AddDays(14),
            CopyId = reservation.CopyId ?? Guid.Empty,
            UserId = reservation.UserId
        };
        await dbContext.Loans.AddAsync(loan);
    }

    private void UpdateCopyStatus(Copy copy)
    {
        copy.IsTaken = false;
        dbContext.Copies.Update(copy);
    }

    private void UpdateTask(LibraryTask task)
    {
        task.IsDone = true;
        dbContext.LibraryTasks.Update(task);
    }


}