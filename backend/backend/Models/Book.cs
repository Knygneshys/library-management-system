namespace backend.Models;

public class Book
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }
    
    public string Summary { get; set; }
    
    public string Isbn { get; set; }
    
    public string Language { get; set; }
    
    public DateTime PublishedAt { get; set; }
    
    public Guid AuthorId { get; set; }
    
    public Author Author { get; set; }
}