using Core.UseCases.TagUseCases.CreateTag.Boundaries;
using Core.UseCases.TagUseCases.DeleteTag.Boundaries;
using Core.UseCases.TagUseCases.GetAllTags.Boundaries;
using Core.UseCases.TagUseCases.Output;
using Core.UseCases.TagUseCases.UpdateTag.Boundaries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
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
        var output = await _mediatr.Send(new GetAllTagsInput(), cancellationToken);
        return Ok(output);
    }

    [HttpPost("CreateTag")]
    public async Task<ActionResult<TagOutput>> Create(CreateTagInput input, CancellationToken cancellationToken)
    {
        var output = await _mediatr.Send(input, cancellationToken);
        return Ok(output);
    }

    [HttpPut("UpdateTag/{id}")]
    public async Task<ActionResult<TagOutput>> Update(int? id, UpdateTagInput input, CancellationToken cancellationToken)
    {
        if (id is null || id != input.Id) return BadRequest();

        var output = await _mediatr.Send(input, cancellationToken);
        return Ok(output);
    }

    [HttpDelete("DeleteTag/{id}")]
    public async Task<ActionResult<TagOutput>> DeleteById(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return BadRequest();

        var deleteTagInput = new DeleteTagInput(id.Value);

        var output = await _mediatr.Send(deleteTagInput, cancellationToken);
        return Ok(output);
    }

}
