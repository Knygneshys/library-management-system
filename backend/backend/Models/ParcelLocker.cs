namespace backend.Models;

public class ParcelLocker
{
    public Guid Id { get; set; }

    public string Address { get; set; }

    public ParcelLockerState LockerState { get; set; }

    public ICollection<Locker> Lockers { get; } = [];
}