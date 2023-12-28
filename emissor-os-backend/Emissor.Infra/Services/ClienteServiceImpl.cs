using Emissor.Application.Factory;
using Emissor.Application.Repository;
using Emissor.Application.Services;
using Emissor.Domain.DTOs.Clientes;
using Emissor.Domain.DTOs.Standard;
using Emissor.Domain.Entities;
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
            if(await _clientesRepository.IssetClienteByDocumento(body.Documento)) 
            {
                return new ObjectResult(new ErrorResponseDTO("Já existe um cliente cadastrado com este documento", "documento", null))
                {
                    StatusCode = StatusCodes.Status409Conflict
                };
            }

            var cliente = new Cliente()
            {
                Nome = body.Nome,
                Documento = body.Documento,
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
            _logger.LogError($"Falha ao cadastrar o cliente ${ex.InnerException}", ex);
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
            _logger.LogError($"Falha ao obter o cliente ${ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

}
