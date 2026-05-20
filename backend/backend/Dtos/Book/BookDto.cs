namespace backend.Dtos.Book;

public class BookDto
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public required string Summary { get; set; }

    public required string Isbn { get; set; }

    public required string Language { get; set; }

    public DateTime PublishedAt { get; set; }

    public required string Author { get; set; }

    public required string PrintingHouse { get; set; }

    public List<string> Genres { get; set; }

    public required string Publisher { get; set; }

    public ReservationDto? ActiveReservation { get; set; }
}