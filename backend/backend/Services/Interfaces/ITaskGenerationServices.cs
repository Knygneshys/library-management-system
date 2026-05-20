namespace backend.Services.Interfaces
{
    public interface ITaskGenerationServices
    {
        Task GenerateTasksAsync(CancellationToken cancellationToken);
    }
}
