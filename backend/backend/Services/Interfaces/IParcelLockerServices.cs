using backend.Dtos.ParcelLocker;
using backend.Models;

namespace backend.Services.Interfaces;

public interface IParcelLockerServices
{
    Task<Guid> CreateAsync(ParcelLockerCreateDto dto);

    Task<List<ParcelLocker>> GetAllAsync();

    Task UpdateAsync(string oldAddress, ParcelLockerUpdateDto dto);

    Task DeleteAsync(Guid id);
}