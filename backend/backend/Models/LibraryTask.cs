namespace backend.Models
{
    public class LibraryTask
    {
        public Guid Id { get; set; }
        public bool IsIssueTask { get; set; }
        public bool IsDone { get; set; }
        public Guid ReservationId { get; set; }
        public Reservation Reservation { get; set; } = null!;
        
        public void UpdateTask()
        {
            IsDone = true;
        }

        public static LibraryTask Create(Reservation reservation, bool IsIssue)
        {
            return new LibraryTask
            {
                Id = Guid.NewGuid(),
                IsIssueTask = IsIssue,
                IsDone = false,
                ReservationId = reservation.Id,
                Reservation = reservation
            };

        }
    }
}
