namespace backend.Models;

public class Author
{
    public Guid Id { get; set; }
    
    public required string FullName { get; set; } 
    
    public required string Nationality { get; set; }
    
    public required string Biography { get; set; }
}