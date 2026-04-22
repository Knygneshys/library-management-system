using backend.Data;
using backend.Dtos.Author;
using backend.Exceptions;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Services.Implementations;

public class AuthorServices(LibraryDbContext dbContext) : IAuthorServices
{
    private const string EntityName = "Author";
    
    public async Task<List<Author>> CreateAsync(AuthorCreateDto dto)
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

        return await GetAllAsync();
    }

    public async Task<List<Author>> GetAllAsync()
    {
        return await dbContext.Authors.ToListAsync();
    }

    public async Task<List<Author>> UpdateAsync(Guid id, AuthorUpdateDto dto)
    {
        var author = await dbContext.Authors.FirstOrDefaultAsync(a => a.Id.Equals(id));
        if (author is null)
        {
            throw new EntityNotFoundException(EntityName);
        }
        
        var authorsNameChanged = !author.FullName.ToLower().Equals(dto.FullName.ToLower());
        
        if(authorsNameChanged)
        {
            var newNameAlreadyExists = await dbContext.Authors.AnyAsync(a => a.FullName.ToLower().Equals(dto.FullName.ToLower()));
            if (newNameAlreadyExists)
            {
                throw new EntityByNameAlreadyExistsException(dto.FullName);
            }
        }

        author.FullName = dto.FullName;
        author.Nationality = dto.Nationality;
        author.Biography = dto.Biography;
        
        await dbContext.SaveChangesAsync();

        return await GetAllAsync();
    }

    public async Task<List<Author>> DeleteAsync(Guid id)
    {
        var author = await dbContext.Authors.FirstOrDefaultAsync(a => a.Id.Equals(id));
        if (author is null)
        {
            throw new EntityNotFoundException(EntityName);
        }
        
        var authorHasBooks = await dbContext.Books.AnyAsync(b => b.AuthorId.Equals(id));
        if (authorHasBooks)
        {
            throw new AuthorHasBookException(author.FullName);
        }
        
        dbContext.Authors.Remove(author);
        await dbContext.SaveChangesAsync();

        return await GetAllAsync();
    }
}