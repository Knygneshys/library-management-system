using backend.Dtos.Task;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController(ITaskService taskService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAllTasks(CancellationToken cancellationToken)
    {
        var tasks = await taskService.GetAllAsync(cancellationToken);
        return Ok(tasks);
    }
}