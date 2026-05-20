using backend.Enums;

namespace backend.Dtos.Task;

public class TaskDto
{
    public Guid Id { get; set; }

    public LibrarianTaskType Type { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid ReservationId { get; set; }

    public Guid? LockerId { get; set; }

    public string? LockerLocationCode { get; set; }

    public string? PinCodeLibrarian { get; set; }

    public string? PinCodeReader { get; set; }
}