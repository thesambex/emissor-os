﻿using Emissor.Application.Database;
using Emissor.Application.Services;
using Emissor.Domain.DTOs.OrdemServico;
using Emissor.Infra.Factory;
using Emissor.Infra.Repository;
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

public class OrdensServicoServiceTest : IDisposable
{

    private readonly PgContext pgContext;
    private readonly IOrdemServicoService ordemServicoService;

    public OrdensServicoServiceTest()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var dbOptions = new DbContextOptionsBuilder<PgContext>()
            .UseNpgsql(configuration.GetConnectionString("Postgresql"));

        pgContext = new PgContext(dbOptions.Options);

        var logger = new Mock<ILogger<OrdemServicoServiceImpl>>();
        ordemServicoService = new OrdemServicoServiceImpl(logger.Object, new AbstractRepositoryFactoryImpl(pgContext), new UnitOfWorkImpl(pgContext));
    }

    public void Dispose()
    {
        pgContext.Dispose();
    }

    [Fact]
    public async void Deve_Abrir_OS()
    {
        var os = new AbrirOSDTO(null, null, Guid.Parse("367b640f-f748-4173-9e05-c999e0f6ef93"), Guid.Parse("ff420a1c-08f8-48e3-a2a4-fce2dc8320a8"), "Teste abertura de OS", "Teste", 5.39, DateTimeOffset.UtcNow);
        var response = await ordemServicoService.AbrirOS(Guid.Parse("ff420a1c-08f8-48e3-a2a4-fce2dc8320a8"), os);

        Assert.NotNull(response);
        Assert.IsType<CreatedAtActionResult>(response);
    }

    [Fact]
    public async void Deve_Obter_Uma_Ordem_De_Servico()
    {
        var response = await ordemServicoService.GetOS(Guid.Parse("6fd88023-ff2a-4311-a12c-91a8dbeff77b"));

        Assert.NotNull(response);
        Assert.IsType<OkObjectResult>(response);

        var payload = ((OkObjectResult)response).Value as OSDTO;
        Assert.NotNull(payload);
    }

    [Fact]
    public async void Deve_Finalizar_Uma_Ordem_De_Servico()
    {
        var response = await ordemServicoService.FinalizarServico(Guid.Parse("203aa950-3356-4d54-88f4-67edb027038e"));

        Assert.NotNull(response);
        Assert.IsType<OkObjectResult>(response);

        var payload = ((OkObjectResult)response).Value as OSDTO;
        Assert.NotNull(payload);
    }

    [Fact(Skip = "Não é necessário no momento")]
    public async void Deve_Deletar_Uma_Ordem_De_Servico()
    {
        var response = await ordemServicoService.DeletarOS(Guid.Parse("203aa950-3356-4d54-88f4-67edb027038e"));

        Assert.NotNull(response);
        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async void Deve_Adicionar_Mercadorias_A_Uma_Ordem_De_Servico()
    {
        var mercadorias = new List<MercadoriaOSDTO>()
        {
            new MercadoriaOSDTO(null, null, Guid.Parse("da69e8fd-24aa-44e8-a38a-d17731245af6"), null, null, 3.0)
        };

        var response = await ordemServicoService.AdicionarMercadorias(Guid.Parse("6dd45f32-baff-46e0-bfdf-8083b9d30b77"), mercadorias);

        Assert.NotNull(response);
        Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async void Deve_Listar_Ordens_De_Servico()
    {
        var response = await ordemServicoService.ListarOS(0);

        Assert.NotNull(response);
        Assert.IsType<OkObjectResult>(response);

        var payload = ((OkObjectResult)response).Value as List<OSDTO>;
        Assert.NotNull(payload);
    }
        
}
