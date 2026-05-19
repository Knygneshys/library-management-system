namespace backend.Models
{
    public class Loan
    {
        public Guid Id { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Guid CopyId { get; set; }
        public Copy Copy { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
