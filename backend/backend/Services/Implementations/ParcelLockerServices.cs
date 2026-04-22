using backend.Data;
using backend.Dtos.ParcelLocker;
using backend.Exceptions;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementations;

public class ParcelLockerServices(LibraryDbContext dbContext) : IParcelLockerServices
{
    public async Task<List<ParcelLocker>> CreateAsync(ParcelLockerCreateDto dto)
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

        return await GetAllAsync();
    }

    public async Task<List<ParcelLocker>> GetAllAsync()
    {
        return await dbContext.ParcelLockers.ToListAsync();
    }

    public async Task UpdateAsync(string oldAddress, ParcelLockerUpdateDto dto)
    {
        var parcelLocker = await dbContext.ParcelLockers.FirstOrDefaultAsync(p => p.Address.Equals(oldAddress));
        if (parcelLocker is null)
        {
            throw new KeyNotFoundException("Parcel locker not found.");
        }
        
        if (!oldAddress.Equals(dto.Address))
        {
            var exists = await dbContext.ParcelLockers.AnyAsync(p => p.Address.Equals(dto.Address));
            if (exists)
            {
                throw new Exception("A parcel locker with the new address already exists.");
            }
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