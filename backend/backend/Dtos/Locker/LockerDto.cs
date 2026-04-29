using backend.Models;

namespace backend.Dtos.Locker;

public class LockerDto
{
    public required Guid Id { get; set; }
    public required string LocationCode { get; set; }
    public required double Height { get; set; }
    public required double Width { get; set; }
    public required double Length { get; set; }
    public required LockerState LockerState { get; set; }
    public required Guid ParcelLockerId { get; set; }
}