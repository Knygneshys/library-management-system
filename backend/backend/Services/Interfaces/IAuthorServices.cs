using backend.Dtos.Author;
using backend.Models;

namespace backend.Services.Interfaces;

public interface IAuthorServices
{
    Task<Guid> CreateAsync(AuthorCreateDto dto);

    Task<List<Author>> GetAllAsync();
}