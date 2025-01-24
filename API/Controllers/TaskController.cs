using Core.UseCases.TaskUseCases.Boundaries;
using Core.UseCases.TaskUseCases.DeleteTask.Boundaries;
using Core.UseCases.TaskUseCases.GetAllTasks.Boundaries;
using Core.UseCases.TaskUseCases.Output;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly IMediator _mediatr;

    public TaskController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpGet("GetAllTasks")]
    public async Task<ActionResult<List<TaskOutput>>> GetAll(CancellationToken cancellationToken)
    {
        var output = await _mediatr.Send(new GetAllTasksInput(), cancellationToken);
        return Ok(output);
    }

    [HttpPost("CreateTask")]
    public async Task<ActionResult<TaskOutput>> Create(CreateTaskInput input, CancellationToken cancellationToken)
    {
        var output = await _mediatr.Send(input, cancellationToken);
        return Ok(output);
    }

    [HttpDelete("DeleteTask/{id}")]
    public async Task<ActionResult<TaskOutput>> DeleteTaskById(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
            return BadRequest();

        var deleteTaskInput = new DeleteTaskInput(id.Value);

        var output = await _mediatr.Send(deleteTaskInput, cancellationToken);
        return Ok(output);
    }
}
