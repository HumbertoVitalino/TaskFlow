using Core.Requests;
using Core.UseCases.TaskUseCases.AddTagToTask.Boundaries;
using Core.UseCases.TaskUseCases.Boundaries;
using Core.UseCases.TaskUseCases.DeleteTask.Boundaries;
using Core.UseCases.TaskUseCases.GetAllTasks.Boundaries;
using Core.UseCases.TaskUseCases.GetTasksByTag.Boundaries;
using Core.UseCases.TaskUseCases.Output;
using Core.UseCases.TaskUseCases.UpdateTask.Boundaries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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

    [HttpGet("GetTasksByTag/{id}")]
    public async Task<ActionResult<List<TaskOutput>>> GetTasksByTag(int id, CancellationToken cancellationToken)
    {
        var output = await _mediatr.Send(new GetTasksByTagInput(id), cancellationToken);
        return Ok(output);
    }

    [HttpPost("CreateTask")]
    public async Task<ActionResult<TaskOutput>> Create([FromBody] CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim is null)
            return Unauthorized("User ID not found in token.");

        var userId = int.Parse(userIdClaim.Value);

        var input = new CreateTaskInput(request.Title, request.Description, userId, request.DueDate, request.TagId);

        var output = await _mediatr.Send(input, cancellationToken);
        return Ok(output);
    }


    [HttpPut("UpdateTask/{id}")]
    public async Task<ActionResult<TaskOutput>> UpdateById(int? id, UpdateTaskInput input, CancellationToken cancellationToken)
    {
        if (id is null || id != input.Id) return BadRequest();

        var output = await _mediatr.Send(input, cancellationToken);
        return Ok(output);
    }

    [HttpPut("AddTagToTask/{id}/{idTag}")]
    public async Task<ActionResult<TaskOutput>> AddTagToTask(int id, int idTag, CancellationToken cancellationToken)
    {
        var input = new AddTagToTaskInput(id, idTag);

        if(input == null) return BadRequest();

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
