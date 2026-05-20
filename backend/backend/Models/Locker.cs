using backend.Models;

public class Locker
{
    public Guid Id { get; set; }
    public string LocationCode { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
    public double Length { get; set; }
    public LockerState LockerState { get; set; }

    public Guid ParcelLockerId { get; set; }
    public ParcelLocker ParcelLocker { get; set; } = null!;

    public ICollection<IssueCompartment> IssueCompartments { get; set; } = new List<IssueCompartment>();

    // TODO: Implement Issue compartment 
    // public Guid IssueCompartmentId { get; set; }
    // public IssueCompartment IssueCompartment { get; set; }
}
