using backend.Dtos.ParcelLocker;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services.Interfaces;

public interface IParcelLockerServices
{
    Task<ParcelLockerDto> CreateAsync(ParcelLockerCreateDto dto);

    Task<List<ParcelLockerDto>> GetAllAsync();

    Task<ParcelLockerDto> UpdateAsync(Guid id, ParcelLockerUpdateDto dto);

    void Delete(Guid id);
}