namespace backend.Models;

public class ParcelLocker
{
    public Guid Id { get; set; }
    
    public required string Address { get; set; }
    
    public required ParcelLockerState LockerState {get; set;}
}