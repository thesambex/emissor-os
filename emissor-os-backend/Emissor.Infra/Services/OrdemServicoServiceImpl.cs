using Emissor.Application.Factory;
using Emissor.Application.Repository;
using Emissor.Application.Services;
using Emissor.Domain.DTOs.Clientes;
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
    private readonly IOrdemServicoMercadoriasRepository _ordemServicoMercadorsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public OrdemServicoServiceImpl(ILogger<OrdemServicoServiceImpl> logger, IAbstractRepositoryFactory abstractRepositoryFactory, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _ordemServicoRepository = abstractRepositoryFactory.CreateOrdemServicoRepository();
        _ordemServicoMercadorsRepository = abstractRepositoryFactory.CreateOrdemServicoMercadoriaRepository();
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> AbrirOS(Guid atendenteId, AbrirOSDTO body)
    {
        try
        {
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

    public async Task<IActionResult> GetOS(Guid id)
    {
        try
        {
            var ordemServico = await _ordemServicoRepository.GetOSById(id);
            if (ordemServico == null)
            {
                return new NotFoundResult();
            }

            var servicoCliente = ordemServico.Cliente;
            var mercadorias = ordemServico.OrdemServicoMercadorias?.Select(m => new MercadoriaOSDTO(m.Id, m.OrdemServicoId, m.MercadoriaId, m.Mercadoria?.Descricao, m.Mercadoria?.Preco, m.Quantidade)).ToList();
            
            ordemServico.ValorFinal = ordemServico.ValorTotalHoras() + mercadorias?.Sum(e => e.Quantidade * e.Valor) ?? 0;

            var response = new OSDTO(
                ordemServico.Id,
                ordemServico.Numero,
                ordemServico.AtendenteId,
                ordemServico.Descricao,
                ordemServico.Observacoes,
                ordemServico.ValorHora,
                ordemServico.ValorFinal,
                ordemServico.DtInicio.LocalDateTime,
                ordemServico.DtFim?.LocalDateTime,
                new ClienteDTO(
                    servicoCliente!.Id,
                    servicoCliente.Nome,
                    servicoCliente.Documento,
                    servicoCliente.Endereco,
                    servicoCliente.EnderecoNumero,
                    servicoCliente.Bairro,
                    servicoCliente.Municipio,
                    servicoCliente.IsPJ
                    ),
                mercadorias
                );

            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao obter a OS {ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<IActionResult> FinalizarServico(Guid id)
    {
        try
        {
            var ordemServico = await _ordemServicoRepository.GetOSById(id);
            if (ordemServico == null)
            {
                return new NotFoundResult();
            }

            ordemServico = await _ordemServicoRepository.Finalizar(id, ordemServico);
            if (ordemServico == null)
            {
                return new NotFoundResult();
            }

            var servicoCliente = ordemServico.Cliente;
            var mercadorias = ordemServico.OrdemServicoMercadorias?.Select(m => new MercadoriaOSDTO(m.Id, m.OrdemServicoId, m.MercadoriaId, m.Mercadoria?.Descricao, m.Mercadoria?.Preco, m.Quantidade)).ToList();


            ordemServico.DtFim = DateTime.Now.ToUniversalTime();
            ordemServico.ValorFinal = ordemServico.ValorTotalHoras() + mercadorias?.Sum(e => e.Quantidade * e.Valor) ?? 0;

            var response = new OSDTO(
                ordemServico.Id,
                ordemServico.Numero,
                ordemServico.AtendenteId,
                ordemServico.Descricao,
                ordemServico.Observacoes,
                ordemServico.ValorHora,
                ordemServico.ValorFinal,
                ordemServico.DtInicio.LocalDateTime,
                ordemServico.DtFim?.LocalDateTime,
                new ClienteDTO(
                    servicoCliente!.Id,
                    servicoCliente.Nome,
                    servicoCliente.Documento,
                    servicoCliente.Endereco,
                    servicoCliente.EnderecoNumero,
                    servicoCliente.Bairro,
                    servicoCliente.Municipio,
                    servicoCliente.IsPJ
                    ),
                mercadorias
                );

            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao finalizar o serviço {ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<IActionResult> DeletarOS(Guid id)
    {
        try
        {
            if (!await _ordemServicoRepository.DeletarOS(id))
            {
                return new NotFoundResult();
            }

            return new NoContentResult();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao deletar a ordem de serviço {ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<IActionResult> AdicionarMercadorias(Guid ordemServicoId, List<MercadoriaOSDTO> mercadorias)
    {
        try
        {
            if (!await _ordemServicoRepository.ExisteOS(ordemServicoId))
            {
                return new NotFoundResult();
            }

            if (mercadorias.Count > 0)
            {
                await _unitOfWork.Begin();

                var tasks = mercadorias.Select(async dto =>
                {
                    var mercadoria = new OrdemServicoMercadoria
                    {
                        OrdemServicoId = ordemServicoId,
                        MercadoriaId = dto.MercadoriaId,
                        Quantidade = dto.Quantidade,
                    };

                    await _ordemServicoMercadorsRepository.CriarOrdemServicoMercadoria(mercadoria);
                });

                await Task.WhenAll(tasks);

                await _unitOfWork.Commit();
            }

            return new NoContentResult();
        }
        catch (Exception ex)
        {
            await _unitOfWork.Rollback();
            _logger.LogError($"Falha ao adicionar mercadorias a ordem de serviço {ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

}
