using backend.Enums;

namespace backend.Models;

public class Reservation
{
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public bool IsExtended { get; set; }
    
    public DateTime DueDate { get; set; }
    
    public bool WantsToReturn { get; set; }
    
    public ReservationState State { get; set; }

    public Guid BookId { get; set; }
    public Book Book { get; set; }

    public Loan Loan { get; set; }
}