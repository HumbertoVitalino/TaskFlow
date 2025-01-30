using Core.UseCases.UserUseCases.CreateUser.Boundaries;
using Core.UseCases.UserUseCases.LoginUser.Boundaries;
using Core.UseCases.UserUseCases.Output;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediatr;

    public UserController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpPost("CreateUser")]
    public async Task<ActionResult<UserOutput>> CreateUser(CreateUserInput input, CancellationToken cancellationToken)
    {
        var output = await _mediatr.Send(input, cancellationToken);
        return Ok(output);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<UserOutput>> Login(LoginUserInput input, CancellationToken cancellationToken)
    {
        var output = await _mediatr.Send(input, cancellationToken);
        return Ok(output);
    }
}
