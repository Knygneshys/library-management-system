using backend.Dtos.ParcelLocker;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.Interfaces;

public interface IParcelLockerServices
{
    Task<List<ParcelLocker>> CreateAsync(ParcelLockerCreateDto dto);

    Task<List<ParcelLocker>> GetAllAsync();

    Task<List<ParcelLocker>> UpdateAsync(string oldAddress, ParcelLockerUpdateDto dto);

    Task<List<ParcelLocker>> DeleteAsync(Guid id);
}