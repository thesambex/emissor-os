using Emissor.Application.Services;
using Emissor.Domain.DTOs.Usuarios;
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
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarUsuario(CriarUsuarioDTO body)
    {
        return await _usuariosService.CriarUsuario(body);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsuario(Guid id)
    {
        return await _usuariosService.GetUsuarioById(id);
    }

    [HttpGet]
    [Route("username/{username}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUsuarioByUsername(string username)
    {
        return await _usuariosService.GetUsuarioByNomeUsuario(username);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeletarUsuarioById(Guid id)
    {
        return await _usuariosService.Deletar(id);
    }

}
