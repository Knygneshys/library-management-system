using backend.Dtos.Author;
using backend.Models;

namespace backend.Services.Interfaces;

public interface IAuthorServices
{
    Task<List<Author>> CreateAsync(AuthorCreateDto dto);

    Task<List<Author>> GetAllAsync();
    
    Task<List<Author>> UpdateAsync(Guid id, AuthorUpdateDto dto);
    
    Task<List<Author>> DeleteAsync(Guid id);
}