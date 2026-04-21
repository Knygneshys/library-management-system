using backend.Data;
using backend.Dtos.ParcelLocker;
using backend.Exceptions;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementations;

public class ParcelLockerServices(LibraryDbContext dbContext) : IParcelLockerServices
{
    public async Task<Guid> CreateAsync(ParcelLockerCreateDto dto)
    {
        var parcelLockerInDb = await dbContext.ParcelLockers.FirstOrDefaultAsync(p => p.Address.Equals(dto.Address));
        if (parcelLockerInDb is not null)
        {
            throw new ParcelLockerByAddressAlreadyExistsException(dto.Address);
        }

        var parcelLocker = new ParcelLocker()
        {
            Id = Guid.NewGuid(),
            Address = dto.Address,
            LockerState = ParcelLockerState.Active,
        };

        await dbContext.ParcelLockers.AddAsync(parcelLocker);
        await dbContext.SaveChangesAsync();
        
        return parcelLocker.Id;
    }

    public async Task<List<ParcelLocker>> GetAllAsync()
    {
        return await dbContext.ParcelLockers.ToListAsync();
    }

    public async Task UpdateAsync(ParcelLockerUpdateDto dto)
    {
        var parcelLocker = await dbContext.ParcelLockers.FindAsync(dto.Id);
        if (parcelLocker is null)
        {
            throw new KeyNotFoundException("Parcel locker not found.");
        }

        parcelLocker.Address = dto.Address;
        parcelLocker.LockerState = dto.LockerState;

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var parcelLocker = await dbContext.ParcelLockers.FindAsync(id);
        if (parcelLocker is null)
        {
            throw new KeyNotFoundException("Parcel locker not found.");
        }

        dbContext.ParcelLockers.Remove(parcelLocker);
        await dbContext.SaveChangesAsync();
    }
}