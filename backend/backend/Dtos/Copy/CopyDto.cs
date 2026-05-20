namespace backend.Dtos.Copy;

public class CopyDto
{
    public Guid Id { get; set; }

    public string Code { get; set; }

    public bool IsTaken { get; set; }
}