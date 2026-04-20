using backend.Data;
using backend.Dtos.Author;
using backend.Exceptions;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementations;

public class AuthorServices(LibraryDbContext dbContext) : IAuthorServices
{
    public async Task<Guid> CreateAsync(AuthorCreateDto dto)
    {
        var authorInDb = await dbContext.Authors.FirstOrDefaultAsync(a => a.FullName.Equals(dto.FullName));
        if (authorInDb is not null)
        {
            throw new EntityByNameAlreadyExistsException(dto.FullName);
        }

        var author = new Author()
        {
            Id = Guid.NewGuid(),
            FullName = dto.FullName,
            Nationality = dto.Nationality,
            Biography = dto.Biography
        };

        await dbContext.Authors.AddAsync(author);
        await dbContext.SaveChangesAsync();

        return author.Id;
    }

    public async Task<List<Author>> GetAllAsync()
    {
        return await dbContext.Authors.ToListAsync();
    }
}