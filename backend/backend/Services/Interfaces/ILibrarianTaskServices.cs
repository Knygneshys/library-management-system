using backend.Dtos.Task;

namespace backend.Services.Interfaces;

public interface ILibrarianTaskService
{
    Task<List<TaskDto>> GetAllAsync(CancellationToken cancellationToken);
}