using Emissor.Application.Providers;
using Emissor.Application.Services;
using Emissor.Domain.DTOs.OrdemServico;
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
    private readonly IJwtProvider _jwtProvider;

    public OrdemServicoController(IOrdemServicoService ordemServicoService, IJwtProvider jwtProvider)
    {
        _ordemServicoService = ordemServicoService;
        _jwtProvider = jwtProvider;
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
            var token = _jwtProvider.DecodeJwt(Request.Headers["Authorization"]);
            if(token != null)
            {
                return await _ordemServicoService.AbrirOS(token.Sub, body);
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

    [HttpPatch]
    [Route("finalizar/{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OSDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> FinalizarServico(Guid id) => await _ordemServicoService.FinalizarServico(id);

    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeletarServico(Guid id) => await _ordemServicoService.DeletarOS(id);

    [HttpPatch]
    [Route("{id:guid}/mercadorias")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType (StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AdicionarMercadorias(Guid id, List<MercadoriaOSDTO> mercadorias) => await _ordemServicoService.AdicionarMercadorias(id, mercadorias);

    [HttpGet]
    [Route("listar")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OSDTO>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ListarOS(int pageIndex) => await _ordemServicoService.ListarOS(pageIndex);

}
