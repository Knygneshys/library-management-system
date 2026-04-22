namespace backend.Dtos.Author;

public class AuthorUpdateDto
{
    public required string FullName { get; set; }

    public required string Nationality { get; set; }

    public required string Biography { get; set; }
}