namespace backend.Models;

public class Loan
{
    public Guid Id { get; set; }
    
    public DateTime LoanDate { get; set; }
    
    public DateTime? ReturnDate { get; set; }
    
    public Guid CopyId { get; set; }
    public Copy? Copy { get; set; }

    public Guid ReservationId { get; set; }
    public Reservation? Reservation { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }


    public static Loan Create(Reservation reservation, Copy copy)
    {
        return new Loan
        {
            Id = Guid.NewGuid(),
            LoanDate = DateTime.UtcNow,
            CopyId = copy.Id,
            ReservationId = reservation.Id,
            UserId = reservation.UserId,
        };
    }

    public void ReturnBook()
    {
        ReturnDate = DateTime.Now;
    }
}