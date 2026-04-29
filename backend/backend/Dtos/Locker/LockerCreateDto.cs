using backend.Models;

namespace backend.Dtos.Locker;

public class LockerCreateDto
{
    public required string LocationCode { get; set; }
    public required double Height { get; set; } 
    public required double Width { get; set; }
    public required double Length { get; set; }
    public required Guid ParcelLockerId { get; set; }
}