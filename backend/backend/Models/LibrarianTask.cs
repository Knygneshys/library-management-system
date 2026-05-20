namespace backend.Models;

public class LibrarianTask
{
    public Guid Id { get; set; }
    public LibrarianTaskType Type { get; set; } // Issue or Return
    public DateTime CreatedAt { get; set; }

    public Guid ReservationId { get; set; }
    public Reservation Reservation { get; set; } = null!;
}