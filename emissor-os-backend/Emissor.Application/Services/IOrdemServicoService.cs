using Emissor.Domain.DTOs.OrdemServico;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Application.Services;

public interface IOrdemServicoService
{
    Task<IActionResult> AbrirOS(Guid atendenteId, AbrirOSDTO body);
    Task<IActionResult> GetOS(Guid id);
    Task<IActionResult> FinalizarServico(Guid id);
    Task<IActionResult> DeletarOS(Guid id);
    Task<IActionResult> AdicionarMercadorias(Guid ordemServicoId, List<MercadoriaOSDTO> mercadorias);
}
