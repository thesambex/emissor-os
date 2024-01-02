using Emissor.Application.Factory;
using Emissor.Application.Repository;
using Emissor.Application.Services;
using Emissor.Domain.DTOs.OrdemServico;
using Emissor.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Services;

public class OrdemServicoServiceImpl : IOrdemServicoService
{

    private readonly IOrdemServicoRepository _ordemServicoRepository;
    private readonly ILogger _logger;

    public OrdemServicoServiceImpl(ILogger<OrdemServicoServiceImpl> logger, IAbstractRepositoryFactory abstractRepositoryFactory)
    {
        _logger = logger;
        _ordemServicoRepository = abstractRepositoryFactory.CreateOrdemServicoRepository();
    }

    public async Task<IActionResult> AbrirOS(JwtSecurityToken token, AbrirOSDTO body)
    {
        try
        {
            var atendenteId = Guid.Parse(token?.Claims.FirstOrDefault(claim => claim.Type.Equals("sub"))?.Value!);
            var ordemServico = new OrdemServico()
            {
                AtendenteId = atendenteId,
                ClienteId = body.ClienteId!.Value,
                Descricao = body.Descricao,
                Observacoes = body.Observacoes,
                DtInicio = body.DtInicio!.Value.UtcDateTime,
                ValorHora = body.ValorHora!.Value
            };

            ordemServico = await _ordemServicoRepository.CriarOS(ordemServico);
            var response = new AbrirOSDTO(
                ordemServico.Id,
                ordemServico.Numero,
                ordemServico.ClienteId,
                ordemServico.AtendenteId,
                ordemServico.Descricao,
                ordemServico.Observacoes,
                ordemServico.ValorHora,
                ordemServico.DtInicio.LocalDateTime
                );

            return new CreatedAtActionResult("GetOrdemServico", null, new { id = response.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao abrir a OS {ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

}
