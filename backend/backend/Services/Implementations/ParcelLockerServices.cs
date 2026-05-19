using backend.Data;
using backend.Dtos.ParcelLocker;
using backend.Exceptions;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementations;

public class ParcelLockerServices(LibraryDbContext dbContext) : IParcelLockerServices
{

    private const string EntityName = "ParcelLocker";
    public async Task<ParcelLockerDto> CreateAsync(ParcelLockerCreateDto dto)
    {
        var parcelLockerInDb = await dbContext.ParcelLockers.FirstOrDefaultAsync(p => p.Address.ToLower().Equals(dto.Address.ToLower()));
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

        return new ParcelLockerDto
        {
            Id = parcelLocker.Id,
            Address = parcelLocker.Address,
            LockerState = parcelLocker.LockerState,
        };
    }

    public async Task<List<ParcelLockerDto>> GetAllAsync()
    {
        return await dbContext.ParcelLockers
        .Select(pl => new ParcelLockerDto
        {
            Id = pl.Id,
            Address = pl.Address,
            LockerState = pl.LockerState,
        })
        .ToListAsync();
    }

    public async Task<ParcelLockerDto> UpdateAsync(Guid id, ParcelLockerUpdateDto dto)
    {
        var parcelLocker = await dbContext.ParcelLockers.FirstOrDefaultAsync(p => p.Id.Equals(id))
            ?? throw new KeyNotFoundException("Parcel locker not found.");

        if (!parcelLocker.Address.ToLower().Equals(dto.Address.ToLower()) &&
            await dbContext.ParcelLockers.AnyAsync(p => p.Address.ToLower().Equals(dto.Address.ToLower())))
        {
            throw new ParcelLockerByAddressAlreadyExistsException(dto.Address);
        }

        parcelLocker.Address = dto.Address;
        parcelLocker.LockerState = dto.LockerState;

        await dbContext.SaveChangesAsync();

        return new ParcelLockerDto
        {
            Id = parcelLocker.Id,
            Address = parcelLocker.Address,
            LockerState = parcelLocker.LockerState,
        };
    }

    public async void Delete(Guid id)
    {
        var parcelLocker = await dbContext.ParcelLockers.FirstOrDefaultAsync(p => p.Id.Equals(id))
            ?? throw new EntityNotFoundException(EntityName);

        dbContext.ParcelLockers.Remove(parcelLocker);
        await dbContext.SaveChangesAsync();
    }
}