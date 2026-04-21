using backend.Models;

namespace backend.Dtos.ParcelLocker;

public class ParcelLockerUpdateDto
{
    public required string Address { get; set; }
    
    public required ParcelLockerState LockerState { get; set; }
}
