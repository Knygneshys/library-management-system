using System.Threading.Tasks;

namespace backend.Services.Interfaces
{
    public interface IReservationServices
    {
        Task<bool> SetReservationAsReturningAsync(Guid reservationId);

        Task<int> GetFreeCopyCountAsync(Guid bookId);

        Task<bool> ReserveCopyAsync(Guid bookId);

        Task<bool> GoToQueueAsync(Guid bookId);
    }
}