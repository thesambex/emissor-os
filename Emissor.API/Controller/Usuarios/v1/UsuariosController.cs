using Emissor.Application.Services;
using Emissor.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Emissor.API.Controller.Usuarios.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/users")]
[Produces("application/json")]
public class UsuariosController : ControllerBase
{

    private readonly IUsuariosService _usuariosService;

    public UsuariosController(IUsuariosService usuariosService)
    {
        _usuariosService = usuariosService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CriarUsuarioDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarUsuario(CriarUsuarioDTO body)
    {
        return await _usuariosService.CriarUsuario(body);
    }

}
