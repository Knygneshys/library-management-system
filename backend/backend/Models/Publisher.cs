namespace backend.Models;

public class Publisher
{
    public Guid Id { get; set; } 
    
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string Website { get; set; }
    
    public string Phone { get; set; }

    public virtual ICollection<Book> Books { get; set; }
}