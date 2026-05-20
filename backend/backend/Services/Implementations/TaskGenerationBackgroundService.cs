using backend.Services.Interfaces;

namespace backend.Services.Implementations;

public class TaskGenerationBackgroundService(
    IServiceScopeFactory scopeFactory,
    ILogger<TaskGenerationBackgroundService> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<ITaskGenerationServices>();
                await service.GenerateTasksAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred during task generation.");
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}