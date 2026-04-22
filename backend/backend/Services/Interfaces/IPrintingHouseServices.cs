using backend.Dtos.Author;
using backend.Dtos.PrintingHouse;
using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IPrintingHouseServices
    {
        Task<List<PrintingHouse>> Create(PrintingHouseCreateDto dto);

        Task<List<PrintingHouse>> GetAll();

        Task<List<PrintingHouse>> Update(Guid id, PrintingHouseUpdateDto dto);

        Task<List<PrintingHouse>> Delete(Guid id);
    }
}
