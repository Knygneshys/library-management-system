using backend.Dtos.Copy;

namespace backend.Dtos.Book;

public class BookDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Summary { get; set; }

    public string Isbn { get; set; }

    public string Language { get; set; }

    public DateTime PublishedAt { get; set; }

    public string Author { get; set; }

    public string PrintingHouse { get; set; }

    public List<string> Genres { get; set; }

    public string Publisher { get; set; }

    public List<CopyDto> Copies { get; set; } = new();

    public int FreeCopyCount { get; set; }
    public ReservationDto? ActiveReservation { get; set; }
}