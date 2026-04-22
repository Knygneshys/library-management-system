namespace backend.Models
{
    public class PrintingHouse
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }

        public required string Website { get; set; }
        public required string Phone { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
