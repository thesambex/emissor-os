using Emissor.Application.Services;
using Emissor.Domain.DTOs.Mercadorias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Emissor.API.Controller.Mercadorias.v1;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/mercadorias")]
[Produces("application/json")]
public class MercadoriasController : ControllerBase
{

    private readonly IMercadoriasService _mercadoriasService;

    public MercadoriasController(IMercadoriasService mercadoriasService)
    {
        _mercadoriasService = mercadoriasService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MercadoriaDTO))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CriarMercadoria(MercadoriaDTO body) => await _mercadoriasService.CriarMercadoria(body);

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MercadoriaDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMercadoria(Guid id) => await _mercadoriasService.GetMercadoria(id);

    [HttpGet]
    [Route("buscar/codigoBarra")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MercadoriaDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMercadoriaCodigoBarra(string codigo) => await _mercadoriasService.GetMercadoriaCodigoBarra(codigo);

    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeletarMercadoria(Guid id) => await _mercadoriasService.DeletarMercadoria(id);

    [HttpGet]
    [Route("buscar")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MercadoriaBuscaDTO>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> BuscarMercadoria(string query, string tipo = "") => await _mercadoriasService.BuscarMercadoria(query, tipo);

}
