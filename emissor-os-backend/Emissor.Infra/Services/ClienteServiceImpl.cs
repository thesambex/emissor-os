using Emissor.Application.Factory;
using Emissor.Application.Repository;
using Emissor.Application.Services;
using Emissor.Domain.DTOs.Clientes;
using Emissor.Domain.DTOs.Standard;
using Emissor.Domain.Entities;
using Emissor.Infra.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Services;

public class ClienteServiceImpl : IClientesService
{

    private readonly IClientesRepository _clientesRepository;
    private readonly ILogger _logger;

    public ClienteServiceImpl(ILogger<ClienteServiceImpl> logger, IAbstractRepositoryFactory abstractRepositoryFactory)
    {
        _logger = logger;
        _clientesRepository = abstractRepositoryFactory.CreateClientesRepository();
    }

    public async Task<IActionResult> CriarCliente(ClienteDTO body)
    {
        try
        {
            var documentoValidator = PessoaDocumentoValidatorFactory.Create(body.Documento, body.IsPJ);
            if (!documentoValidator.IsValido())
            {
                return new ObjectResult(new ErrorResponseDTO("Documento inválido", null, null))
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            if (await _clientesRepository.IssetClienteByDocumento(documentoValidator.GetDocumentoValido()))
            {
                return new ObjectResult(new ErrorResponseDTO("Já existe um cliente cadastrado com este documento", "documento", null))
                {
                    StatusCode = StatusCodes.Status409Conflict
                };
            }

            var cliente = new Cliente()
            {
                Nome = body.Nome,
                Documento = documentoValidator.GetDocumentoValido(),
                Endereco = body.Endereco,
                EnderecoNumero = body.EnderecoNumero,
                Bairro = body.Bairro,
                Municipio = body.Municipio,
                IsPJ = body.IsPJ
            };

            cliente = await _clientesRepository.CriarCliente(cliente);

            var response = new ClienteDTO(
                cliente.Id,
                cliente.Nome,
                cliente.Documento,
                cliente.Endereco,
                cliente.EnderecoNumero,
                cliente.Bairro,
                cliente.Municipio,
                cliente.IsPJ
            );

            return new CreatedAtActionResult("GetCliente", null, new { id = response.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao cadastrar o cliente {ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<IActionResult> GetClienteById(Guid id)
    {
        try
        {
            var cliente = await _clientesRepository.GetClienteById(id);
            if (cliente == null)
            {
                return new NotFoundResult();
            }

            return new OkObjectResult(new ClienteDTO(cliente.Id, cliente.Nome, cliente.Documento, cliente.Endereco, cliente.EnderecoNumero, cliente.Bairro, cliente.Municipio, cliente.IsPJ));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao obter o cliente {ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<IActionResult> DeletarCliente(Guid id)
    {
        try
        {
            if(!await _clientesRepository.DeletarCliente(id))
            {
                return new NotFoundResult();
            }
            
            return new NoContentResult();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao deletar o cliente {id} - {ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<IActionResult> BuscarCliente(string query)
    {
        try
        {
            var data = await _clientesRepository.BuscarCliente(query);
            return new OkObjectResult(data.Select(e => new ClienteBuscaDTO(e.Id, e.Nome, e.Documento)));
        }
        catch(Exception ex)
        {
            _logger.LogError($"Falha ao buscar o cliente {ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

}
