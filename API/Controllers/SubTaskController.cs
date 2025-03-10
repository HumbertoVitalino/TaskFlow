using Core.UseCases.SubTasksUseCases.CreateSubTask.Boundaries;
using Core.UseCases.SubTasksUseCases.DeleteSubTask;
using Core.UseCases.SubTasksUseCases.DeleteSubTask.Boundaries;
using Core.UseCases.SubTasksUseCases.Output;
using Core.UseCases.SubTasksUseCases.UpdateSubTask.Boundaries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubTaskController : ControllerBase
{
    private readonly IMediator _mediatr;

    public SubTaskController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost("CreateSubTask")]
    public async Task<ActionResult<SubTaskOutput>> Create(CreateSubTaskInput input, CancellationToken cancellationToken)
    {
        var output = await _mediatr.Send(input, cancellationToken);
        return Ok(output);
    }

    [HttpPut("UpdateSubTask")]
    public async Task<ActionResult<SubTaskOutput>> Update(UpdateSubTaskInput input, CancellationToken  cancellationToken)
    {
        var output = await _mediatr.Send(input, cancellationToken);
        return Ok(output);
    }

    [HttpDelete("DeleteSubTask/{id}")]
    public async Task<ActionResult<SubTaskOutput>> DeleteSubTask(int? id, CancellationToken cancellationToken)
    {
        if (id is null)
            return BadRequest();

        var deleteSubTaskInput = new DeleteSubTaskInput(id.Value);

        var output = await _mediatr.Send(deleteSubTaskInput, cancellationToken);
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
