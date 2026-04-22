using backend.Data;
using backend.Dtos.PrintingHouse;
using backend.Exceptions;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

namespace backend.Services.Implementations;

public class PrintingHouseServices(LibraryDbContext dbContext) : IPrintingHouseServices
{
    private const string EntityName = "PrintingHouse";

    public async Task<List<PrintingHouse>> Create(PrintingHouseCreateDto dto)
    {
        var printingHouseInDb = await dbContext.PrintingHouses.FirstOrDefaultAsync(a => a.Name.ToLower().Equals(dto.Name.ToLower()));
        if (printingHouseInDb is not null)
        {
            throw new EntityByNameAlreadyExistsException(dto.Name);
        }

        var printingHouse = new PrintingHouse()
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Address = dto.Address,
            Website = dto.Website,
            Phone = dto.Phone
        };

        await dbContext.PrintingHouses.AddAsync(printingHouse);
        await dbContext.SaveChangesAsync();

        return await GetAll();
    }

    public async Task<List<PrintingHouse>> GetAll()
    {
        return await dbContext.PrintingHouses.ToListAsync();
    }

    public async Task<List<PrintingHouse>> Update(Guid id, PrintingHouseUpdateDto dto)
    {
        var printingHouse = await dbContext.PrintingHouses.FirstOrDefaultAsync(a => a.Id.Equals(id));
        if (printingHouse is null)
        {
            throw new EntityNotFoundException(EntityName);
        }

        var printingHouseNameChanged = !printingHouse.Name.ToLower().Equals(dto.Name.ToLower());

        if (printingHouseNameChanged)
        {
            var newNameAlreadyExists = await dbContext.PrintingHouses.AnyAsync(a => a.Name.ToLower().Equals(dto.Name.ToLower()));
            if (newNameAlreadyExists)
            {
                throw new EntityByNameAlreadyExistsException(dto.Name);
            }
        }

        printingHouse.Name = dto.Name;
        printingHouse.Address = dto.Address;
        printingHouse.Website = dto.Website;
        printingHouse.Phone = dto.Phone;

        await dbContext.SaveChangesAsync();

        return await GetAll();
    }

    public async Task<List<PrintingHouse>> Delete(Guid id)
    {
        var printingHouse = await dbContext.PrintingHouses.FirstOrDefaultAsync(a => a.Id.Equals(id));
        if (printingHouse is null)
        {
            throw new EntityNotFoundException(EntityName);
        }

        var printingHouseHasBooks = await dbContext.Books.AnyAsync(b => b.PrintingHouseId.Equals(id));
        if (printingHouseHasBooks)
        {
            throw new PrintingHouseHasBookException(printingHouse.Name);
        }

        dbContext.PrintingHouses.Remove(printingHouse);
        await dbContext.SaveChangesAsync();

        return await GetAll();
    }
}
