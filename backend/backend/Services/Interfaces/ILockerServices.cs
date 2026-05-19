using backend.Dtos.Locker;

namespace backend.Services.Interfaces;

public interface ILockerServices
{
    Task<LockerDto> CreateAsync(Guid parcelLockerId, LockerCreateDto dto);

    Task<List<LockerDto>> GetAllAsync();

    Task<List<LockerDto>> GetLockersByParcelLockerAsync(Guid parcelLocker);

    Task<LockerDto> UpdateAsync(Guid id, LockerUpdateDto dto);

    void Delete(Guid id);




    Task<LockerDto?> GetLockerByCodeAsync(string pinCode);
    Task OpenLockerAsync(Guid id);

    Task<bool> IsLockerClosedAsync(Guid id);
    Task HandleLockerClosedAsync(Guid id);
    Task ResetLockerAsync(Guid lockerId, string pinCode);
}