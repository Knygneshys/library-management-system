namespace backend.Models;

public class Locker
{
    public Guid Id { get; set; }
    public required string LocationCode { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
    public LockerState LockerState { get; set; }

    public Guid ParcelLockerId { get; set; }
    public ParcelLocker ParcelLocker { get; set; } = null!;
    public Guid IssueCompartmentId { get; set; }
    public IssueCompartment? IssueCompartment { get; set; }

    public bool IsDoorClosed { get; set; } = true;

    public void ResetLockerState()
    {
        IsDoorClosed = true;
        LockerState = LockerState.Empty;
    }

    public void SetOccupied()
    {
        IsDoorClosed = true;
        LockerState = LockerState.Occupied;
    }
}
