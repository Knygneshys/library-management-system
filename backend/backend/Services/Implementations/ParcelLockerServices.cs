using backend.Data;
using backend.Dtos.ParcelLocker;
using backend.Exceptions;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementations;

public class ParcelLockerServices(LibraryDbContext dbContext) : IParcelLockerServices
{
    
    private const string EntityName = "ParcelLocker";
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

    public async Task<List<ParcelLocker>> UpdateAsync(string oldAddress, ParcelLockerUpdateDto dto)
    {
        var parcelLocker = await dbContext.ParcelLockers.FirstOrDefaultAsync(p => p.Address.Equals(oldAddress));
        if (parcelLocker is null)
        {
            throw new KeyNotFoundException("Parcel locker not found.");
        }
        
        var parcelLockerAddresChanged = !parcelLocker.Address.ToLower().Equals(dto.Address.ToLower());

        if (parcelLockerAddresChanged)
        {
            var newAddressAlreadyExists = await dbContext.ParcelLockers.AnyAsync(p => p.Address.Equals(dto.Address));
            if (newAddressAlreadyExists)
            {
                throw new ParcelLockerByAddressAlreadyExistsException(dto.Address);
            }
        }
        
        parcelLocker.Address = dto.Address;
        parcelLocker.LockerState = dto.LockerState;
        
        await dbContext.SaveChangesAsync();

        return await GetAllAsync();
    }

    public async Task<List<ParcelLocker>> DeleteAsync(Guid id)
    {
        var parcelLocker = await dbContext.ParcelLockers.FirstOrDefaultAsync(p => p.Id.Equals(id));
        if(parcelLocker is null)
        {
            throw new EntityNotFoundException(EntityName);
        }
        
        dbContext.ParcelLockers.Remove(parcelLocker);
        await dbContext.SaveChangesAsync();
        return await GetAllAsync();
    }
}