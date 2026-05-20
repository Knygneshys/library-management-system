namespace backend.Models;

public class Copy
{
    public Guid Id { get; set; }

    public string Code { get; set; }

    public bool IsTaken { get; set; }

    public Guid BookId { get; set; }

    public Book Book { get; set; }

    public Loan Loan { get; set; }

    public void UpdateStatus()
    {
        IsTaken = false;
    }
}