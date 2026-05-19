namespace backend.Models;

public class Loan
{
    public Guid Id { get; set; }

    public DateTime LoanDate { get; set; }

    public DateTime ReturnDate { get; set; }

    public Guid CopyId { get; set; }
    public Copy Copy { get; set; }

    public Guid ReservationId { get; set; }
    public Reservation Reservation { get; set; }
}