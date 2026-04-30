using backend.Dtos.Locker;

namespace backend.Services.Interfaces;

public interface ILockerServices
{
    Task<LockerDto> CreateAsync(LockerCreateDto dto);

    Task<List<LockerDto>> GetAllAsync();

    Task<List<LockerDto>> GetLockersByParcelLockerAsync(Guid parcelLocker);

    Task<LockerDto> UpdateAsync(Guid id, LockerUpdateDto dto);

    void Delete(Guid id);
}