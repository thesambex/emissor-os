using Emissor.Application.Factory;
using Emissor.Application.Repository;
using Emissor.Application.Services;
using Emissor.Domain.DTOs.Mercadorias;
using Emissor.Domain.DTOs.Standard;
using Emissor.Domain.Entities;
using Emissor.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Services;

public class MercadoriasServiceImpl : IMercadoriasService
{

    private readonly IMercadoriaRepository _mercadoriaRepository;
    private readonly ILogger _logger;

    public MercadoriasServiceImpl(ILogger<MercadoriasServiceImpl> logger, IAbstractRepositoryFactory abstractRepositoryFactory)
    {
        _logger = logger;
        _mercadoriaRepository = abstractRepositoryFactory.CreateMercadoriaRepository();
    }

    public async Task<IActionResult> CriarMercadoria(MercadoriaDTO body)
    {
        try
        {

            if (await _mercadoriaRepository.IssetMercadoriaByReferencia(body.Referencia))
            {
                return new ObjectResult(new ErrorResponseDTO("Já existe uma mercadoria com esta referência", "referencia", null))
                {
                    StatusCode = StatusCodes.Status409Conflict
                };
            }

            if (body.CodigoBarra != null && await _mercadoriaRepository.IssetMercadoriaByCodigoBarra(body.CodigoBarra))
            {
                return new ObjectResult(new ErrorResponseDTO("Já existe uma mercadoria com este código de barras", "referencia", null))
                {
                    StatusCode = StatusCodes.Status409Conflict
                };
            }

            var mercadoria = new Mercadoria()
            {
                Descricao = body.Descricao,
                Referencia = body.Referencia,
                CodigoBarra = body.CodigoBarra,
                Preco = body.Preco!.Value
            };

            switch (body.Unidade)
            {
                case "UNIDADE": mercadoria.Unidade = TipoUnidades.UNIDADE; break;
                case "METRO": mercadoria.Unidade = TipoUnidades.METRO; break;
                case "LITRO": mercadoria.Unidade = TipoUnidades.LITRO; break;
                case "KILO": mercadoria.Unidade = TipoUnidades.KILO; break;
            }

            mercadoria = await _mercadoriaRepository.CriarMercadoria(mercadoria);

            var response = new MercadoriaDTO(mercadoria.Id, mercadoria.Descricao, mercadoria.Referencia, mercadoria.CodigoBarra, mercadoria.Preco, mercadoria.Unidade.ToString());
            return new CreatedAtActionResult("GetMercadoria", null, new { id = response.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao criar a mercadoria {ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<IActionResult> GetMercadoria(Guid id)
    {
        try
        {
            var mercadoria = await _mercadoriaRepository.GetMercadoria(id);
            if (mercadoria == null)
            {
                return new NotFoundResult();
            }

            var response = new MercadoriaDTO(mercadoria.Id, mercadoria.Descricao, mercadoria.Referencia, mercadoria.CodigoBarra, mercadoria.Preco, mercadoria.Unidade.ToString());
            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao obter a mercadoria {ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<IActionResult> GetMercadoriaCodigoBarra(string codigoBarra)
    {
        try
        {
            var mercadoria = await _mercadoriaRepository.GetMercadoriaCodigoBarra(codigoBarra);
            if (mercadoria == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(new MercadoriaDTO(mercadoria.Id, mercadoria.Descricao, mercadoria.Referencia, mercadoria.CodigoBarra, mercadoria.Preco, mercadoria.Unidade.ToString()));
        }
        catch(Exception ex)
        {
            _logger.LogError($"Falha ao buscar a mercadoria {ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<IActionResult> DeletarMercadoria(Guid id)
    {
        try
        {
            if (!await _mercadoriaRepository.DeletarMercadoria(id))
            {
                return new NotFoundResult();
            }

            return new NoContentResult();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao deletar a mercadoria {ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<IActionResult> BuscarMercadoria(string query, string tipo = "")
    {
        try
        {
            var data = new List<Mercadoria>();
            if (!string.IsNullOrEmpty(tipo) && tipo.Equals("BARRA", StringComparison.OrdinalIgnoreCase))
            {
                var mercadoria = await _mercadoriaRepository.GetMercadoriaCodigoBarra(query);
                if (mercadoria != null) data.Add(mercadoria);
            }
            else
            {
                data = await _mercadoriaRepository.BuscarMercadoria(query);
            }

            return new OkObjectResult(data.Select(e => new MercadoriaBuscaDTO(e.Id, e.Descricao, e.Referencia, e.Preco)));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao buscar a mercadoria {ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

}
