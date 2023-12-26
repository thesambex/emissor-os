using Emissor.Application.Services;
using Emissor.Domain.DTOs.Auth;
using Emissor.Domain.DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace Emissor.API.Controller.Auth.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
[Produces("application/json")]
public class AuthController
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("signup")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CriarUsuarioDTO))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignUp(CriarUsuarioDTO body)
    {
        return await _authService.SignUp(body);
    }

    [HttpPost]
    [Route("signin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> SignIn(SignInDTO body)
    {
        return await _authService.SignIn(body);
    }

}
