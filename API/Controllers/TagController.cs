using Core.UseCases.TagUseCases.CreateTag.Boundaries;
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

    [HttpPost("CreateTag")]
    public async Task<ActionResult<TagOutput>> Create(CreateTagInput input, CancellationToken cancellationToken)
    {
        var output = await _mediatr.Send(input, cancellationToken);
        return Ok(output);
    }

}
