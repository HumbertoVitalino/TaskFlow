using Core.Requests;
using Core.UseCases.TagUseCases.CreateTag.Boundaries;
using Core.UseCases.TagUseCases.DeleteTag.Boundaries;
using Core.UseCases.TagUseCases.GetAllTags.Boundaries;
using Core.UseCases.TagUseCases.Output;
using Core.UseCases.TagUseCases.UpdateTag.Boundaries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TagController : ControllerBase
{
    private readonly IMediator _mediatr;

    public TagController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpGet("GetAllTags")]
    public async Task<ActionResult<List<TagOutput>>> GetAllTags(CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var output = await _mediatr.Send(new GetAllTagsInput(userId), cancellationToken);
        return Ok(output);
    }

    [HttpPost("CreateTag")]
    public async Task<ActionResult<TagOutput>> Create(CreateTagRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var input = new CreateTagInput(request.Name, request.Description, userId);
        var output = await _mediatr.Send(input, cancellationToken);
        return Ok(output);
    }

    [HttpPut("UpdateTag/{id}")]
    public async Task<ActionResult<TagOutput>> Update(int id, UpdateTagRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var input = new UpdateTagInput(id, request.Name, request.Description, userId);
        var output = await _mediatr.Send(input, cancellationToken);
        return Ok(output);
    }

    [HttpDelete("DeleteTag/{id}")]
    public async Task<ActionResult<TagOutput>> DeleteById(int id, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var input = new DeleteTagInput(id, userId);

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
