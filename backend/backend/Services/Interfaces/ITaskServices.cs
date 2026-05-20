using backend.Dtos.Task;

namespace backend.Services.Interfaces;

public interface ITaskService
{
    Task<List<TaskDto>> GetAllAsync(CancellationToken cancellationToken);
}