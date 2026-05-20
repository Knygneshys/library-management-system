using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    public class IssueCompartment
    {
        public Guid Id { get; set; }
        public DateTime? InsertionDate { get; set; }
        public string? PinCodeLibrarian { get; set; }
        public string? PinCodeReader { get; set; }
        public Guid LockerId { get; set; }
        public Locker Locker { get; set; } = null!;
        public Reservation? IssueReservation { get; set; }
        public Reservation? ReturnReservation { get; set; }
        public static IssueCompartment GetByLocker(Locker locker)
        {
            return locker.IssueCompartment ?? throw new Exception("Compartment not found");
        }
    }
}
