using Emissor.Application.Services;
using Emissor.Domain.DTOs.OrdemServico;
using Emissor.Infra.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Emissor.API.Controller.OrdemServico.v1;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/os")]
[Produces("application/json")]
public class OrdemServicoController : ControllerBase
{

    private readonly IOrdemServicoService _ordemServicoService;

    public OrdemServicoController(IOrdemServicoService ordemServicoService)
    {
        _ordemServicoService = ordemServicoService;
    }

    [HttpPost]
    [Route("abrir")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AbrirOSDTO))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AbrirOS(AbrirOSDTO body)
    {
        try
        {
            var token = new JWTParser(Request.Headers["Authorization"]).GetJWT();
            if(token != null)
            {
                return await _ordemServicoService.AbrirOS(token, body);
            }
            else
            {
                return BadRequest();
            }
        }
        catch (Exception ex)
        {
            return BadRequest("Token inválido");
        }
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OSDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrdemServico(Guid id) => await _ordemServicoService.GetOS(id);

}
