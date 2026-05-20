using backend.Enums;

namespace backend.Models;

public class Reservation
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public bool IsExtended { get; set; }
    
    public DateTime DueDate { get; set; }
    public bool WantsToReturn { get; set; }
    public Guid BookId { get; set; }
    public Book Book { get; set; } = null!;
    public Guid? CopyId { get; set; }
    public Copy? Copy { get; set; }
    public ReservationState State { get; set; }
    public ICollection<LibraryTask> LibraryTasks { get; set; } = new List<LibraryTask>();
    public Guid? IssueCompartmentId { get; set; }
    public IssueCompartment? IssueCompartment { get; set; }
    public Guid? ReturnCompartmentId { get; set; }
    public IssueCompartment? ReturnCompartment { get; set; }
    public Loan? Loan { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public void IssueBook()
    {
        State = ReservationState.NotLate;
    }

    public void InsertBook()
    {
        State = ReservationState.Completed;
    }
}