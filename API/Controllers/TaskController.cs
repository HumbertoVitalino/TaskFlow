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
        var userId = GetUserId();
        var output = await _mediatr.Send(new GetAllTasksInput(userId), cancellationToken);
        return Ok(output);
    }

    [HttpGet("GetTasksByTag/{id}")]
    public async Task<ActionResult<List<TaskOutput>>> GetTasksByTag(int id, CancellationToken cancellationToken)
    {        
        var userId = GetUserId();
        var output = await _mediatr.Send(new GetTasksByTagInput(id, userId), cancellationToken);
        return Ok(output);
    }

    [HttpPost("CreateTask")]
    public async Task<ActionResult<TaskOutput>> Create([FromBody] CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var input = new CreateTaskInput(request.Title, request.Description, userId, request.DueDate, request.TagId);

        var output = await _mediatr.Send(input, cancellationToken);
        return Ok(output);
    }


    [HttpPut("UpdateTask/{id}")]
    public async Task<ActionResult<TaskOutput>> UpdateById(int id, [FromBody]UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var input = new UpdateTaskInput(id, request.Title, request.Description, request.DueDate, userId);

        var output = await _mediatr.Send(input, cancellationToken);
        return Ok(output);
    }

    [HttpPut("AddTagToTask/{id}/{idTag}")]
    public async Task<ActionResult<TaskOutput>> AddTagToTask(int id, int idTag, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var input = new AddTagToTaskInput(id, idTag, userId);

        var output = await _mediatr.Send(input, cancellationToken);
        return Ok(output);
    }

    [HttpDelete("DeleteTask/{id}")]
    public async Task<ActionResult<TaskOutput>> DeleteTaskById(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
            return BadRequest();

        var userId = GetUserId();
        var input = new DeleteTaskInput(id.Value, userId);

        var output = await _mediatr.Send(input, cancellationToken);
        return Ok(output);
    }

    private int GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim is null)
            throw new UnauthorizedAccessException("User ID not found in token.");

        return int.Parse(userIdClaim.Value);
    }
}
