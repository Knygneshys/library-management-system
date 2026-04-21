using backend.Dtos.ParcelLocker;
using backend.Models;

namespace backend.Services.Interfaces;

public interface IParcelLockerServices
{
    Task<Guid> CreateAsync(ParcelLockerCreateDto dto);

    Task<List<ParcelLocker>> GetAllAsync();
}