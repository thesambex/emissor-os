using Emissor.Application.Database;
using Emissor.Application.Services;
using Emissor.Domain.DTOs.Mercadorias;
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

public class MercadoriasServiceTest : IDisposable
{

    private readonly PgContext pgContext;
    private readonly IMercadoriasService mercadoriasService;

    public MercadoriasServiceTest()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var dbOptions = new DbContextOptionsBuilder<PgContext>()
            .UseNpgsql(configuration.GetConnectionString("Postgresql"));

        pgContext = new PgContext(dbOptions.Options);

        var logger = new Mock<ILogger<MercadoriasServiceImpl>>();
        mercadoriasService = new MercadoriasServiceImpl(logger.Object, new AbstractRepositoryFactoryImpl(pgContext));
    }

    public void Dispose()
    {
        pgContext.Dispose();
    }

    [Fact]
    public async void Deve_Cadastrar_Uma_Nova_Mercadoria()
    {
        var mercadoria = new MercadoriaDTO(null, "Pino 3/4", "123", null, 50.60, "UNIDADE");
        var response = await mercadoriasService.CriarMercadoria(mercadoria);

        Assert.NotNull(response);
        Assert.IsType<CreatedAtActionResult>(response);
    }

    [Fact]
    public async void Deve_Obter_Uma_Nova_Mercadoria()
    {
        var response = await mercadoriasService.GetMercadoria(Guid.Parse("3233b112-7a22-48a9-aff0-600b873c5c31"));

        Assert.NotNull(response);
        Assert.IsType<OkObjectResult>(response);
    }

    [Fact(Skip = "Não é necessário no momento")]
    public async void Deve_Deletar_Uma_Mercadoria()
    {
        var response = await mercadoriasService.DeletarMercadoria(Guid.Parse("cde454d2-d968-4cb7-b272-6924667a60a3"));

        Assert.NotNull(response);
        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async void Deve_Buscar_Uma_Mercadoria()
    {
        var response = await mercadoriasService.BuscarMercadoria("Pi");

        Assert.NotNull(response);
        Assert.IsType<OkObjectResult>(response);
    }

    [Fact]
    public async void Deve_Buscar_Uma_Mercadoria_Pelo_Codigo_De_Barra()
    {
        var response = await mercadoriasService.GetMercadoriaCodigoBarra("12345695");

        Assert.NotNull(response);
        Assert.IsType<OkObjectResult>(response);

        var payload = ((OkObjectResult)response).Value as MercadoriaDTO;
        Assert.Equal("D1020", payload?.Referencia);
    }

}
