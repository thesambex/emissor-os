using Emissor.Application.Factory;
using Emissor.Application.Repository;
using Emissor.Application.Services;
using Emissor.Domain.DTOs.Clientes;
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

    public async Task<IActionResult> CriarCliente(CriarClienteDTO body)
    {
        try
        {
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

            var response = new CriarClienteDTO()
            {
                Nome = cliente.Nome,
                Documento = cliente.Documento,
                Endereco = cliente.Endereco,
                EnderecoNumero = body.EnderecoNumero,
                Bairro = cliente.Bairro,
                Municipio = cliente.Municipio,
                IsPJ = cliente.IsPJ
            };

            return new CreatedAtActionResult("GetCliente", null, new { id = response.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao cadastrar o cliente ${ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

}
