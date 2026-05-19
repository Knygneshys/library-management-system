using System.Threading.Tasks;

namespace backend.Services.Interfaces
{
    public interface IReservationServices
    {
        Task<bool> SetReservationAsReturningAsync(Guid reservationId);
    }
}