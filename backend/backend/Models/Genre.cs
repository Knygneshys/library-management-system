namespace backend.Models;

public class Genre
{
    public Guid Id { get; set; }
    
    public string Title { get; set; }

    public virtual ICollection<Book> Books { get; set; }
}