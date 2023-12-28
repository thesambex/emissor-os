using Emissor.Application.Database;
using Emissor.Application.Services;
using Emissor.Domain.DTOs.Clientes;
using Emissor.Infra.Factory;
using Emissor.Infra.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Test.Services;

public class ClientesServiceTest : IDisposable
{

    private readonly PgContext pgContext;
    private readonly IClientesService clienesService;

    public ClientesServiceTest()
    {

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var dbOptions = new DbContextOptionsBuilder<PgContext>()
            .UseNpgsql(configuration.GetConnectionString("Postgresql"));

        pgContext = new PgContext(dbOptions.Options);

        var logger = new Mock<ILogger<ClienteServiceImpl>>();
        clienesService = new ClienteServiceImpl(logger.Object, new AbstractRepositoryFactoryImpl(pgContext));
    }

    public void Dispose()
    {
        pgContext.Dispose();
    }

    [Fact]
    public async void Deve_Cadastrar_Um_Novo_Cliente()
    {
        var dto = new ClienteDTO(
                null,
                "Roberto J Doe",
                "123.456.789-16",
                "Rua 1",
                10,
                "Alamedas",
                "São Paulo",
                false
        );

        var result = await clienesService.CriarCliente(dto);

        Assert.NotNull(result);
        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async void Deve_Obter_Um_Cliente()
    {
        var response = await clienesService.GetClienteById(Guid.Parse("6b357767-0064-4fe3-bdaa-795bbfe7563e"));

        Assert.NotNull(response);
        Assert.IsType<OkObjectResult>(response);

        var payload = ((OkObjectResult)response).Value as ClienteDTO;
        Assert.NotNull(payload);
    }

}
