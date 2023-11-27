using Emissor.Application.Services;
using Emissor.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Emissor.API.Controller.Usuarios.v1;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/users")]
public class UsuariosController : ControllerBase
{

    private readonly IUsuariosService _usuariosService;

    public UsuariosController(IUsuariosService usuariosService)
    {
        _usuariosService = usuariosService;
    }

    [HttpPost]
    public async Task<IActionResult> CriarUsuario(CriarUsuarioDTO body)
    {
        return await _usuariosService.CriarUsuario(body);
    }

}
