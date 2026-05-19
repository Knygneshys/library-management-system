namespace backend.Models
{
    public class Copy
    {
        public Guid Id { get; set; }
        public bool IsTaken { get; set; }
        public string Code { get; set; } = string.Empty;
        public Guid BookId { get; set; }
        public Book Book { get; set; } = null!;
        public Loan? Loan { get; set; }
        public Reservation? Reservation { get; set; }

    }
}
