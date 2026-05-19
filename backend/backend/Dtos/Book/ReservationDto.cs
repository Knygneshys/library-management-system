using backend.Enums;

namespace backend.Dtos.Book;

public class ReservationDto
{
    public Guid Id { get; set; }
    public ReservationState State { get; set; }
}