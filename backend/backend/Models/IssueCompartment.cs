namespace backend.Models;

public class IssueCompartment
{
    public Guid Id { get; set; }
    public IssueCompartmentType Type { get; set; }

    public string PinCodeReader { get; set; } = string.Empty;
    public string PinCodeLibrarian { get; set; } = string.Empty;

    public Guid LockerId { get; set; }
    public Locker Locker { get; set; }

    public Guid ReservationId { get; set; }
    public Reservation Reservation { get; set; }

    public static IssueCompartment GetByLocker(Locker locker)
    {
        return locker.IssueCompartments.FirstOrDefault() ?? new IssueCompartment();
    }
}