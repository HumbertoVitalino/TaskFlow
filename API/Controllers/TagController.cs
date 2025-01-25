using Core.UseCases.TagUseCases.CreateTag.Boundaries;
using Core.UseCases.TagUseCases.GetAllTags.Boundaries;
using Core.UseCases.TagUseCases.Output;
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

}
