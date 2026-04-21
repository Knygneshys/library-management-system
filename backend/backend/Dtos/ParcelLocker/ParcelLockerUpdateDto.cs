using backend.Models;

namespace backend.Dtos.ParcelLocker;

public class ParcelLockerUpdateDto
{
    public required Guid Id { get; set; }
    
    public required string Address { get; set; }
    
    public required ParcelLockerState LockerState { get; set; }
}
