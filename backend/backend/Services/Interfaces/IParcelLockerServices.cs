using backend.Dtos.ParcelLocker;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.Interfaces;

public interface IParcelLockerServices
{
    Task<List<ParcelLocker>> CreateAsync(ParcelLockerCreateDto dto);

    Task<List<ParcelLocker>> GetAllAsync();

    Task<List<ParcelLocker>> UpdateAsync(Guid id, ParcelLockerUpdateDto dto);

    Task<List<ParcelLocker>> DeleteAsync(Guid id);
}