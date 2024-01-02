﻿using Castle.Core.Logging;
using Emissor.Application.Database;
using Emissor.Domain.DTOs.OrdemServico;
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
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Test.Services;

public class OrdensServicoTest : IDisposable
{

    private readonly PgContext pgContext;
    private readonly OrdemServicoServiceImpl ordemServicoService;

    public OrdensServicoTest()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var dbOptions = new DbContextOptionsBuilder<PgContext>()
            .UseNpgsql(configuration.GetConnectionString("Postgresql"));

        pgContext = new PgContext(dbOptions.Options);

        var logger = new Mock<ILogger<OrdemServicoServiceImpl>>();
        ordemServicoService = new OrdemServicoServiceImpl(logger.Object, new AbstractRepositoryFactoryImpl(pgContext));
    }

    public void Dispose()
    {
        pgContext.Dispose();
    }

    [Fact]
    public async void Deve_Abrir_OS()
    {
        var claims = new List<Claim>() { new Claim("sub", "fbec4a0b-c31f-4567-bee4-d3a830f14c52") };
        var os = new AbrirOSDTO(null, null, Guid.Parse("efd78fff-9df1-4f46-b1dd-e917052f4d18"), Guid.Parse("fbec4a0b-c31f-4567-bee4-d3a830f14c52"), "Teste abertura de OS", "Teste", 5.39, DateTimeOffset.UtcNow);
        var response = await ordemServicoService.AbrirOS(new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(claims: claims), os);

        Assert.NotNull(response);
        Assert.IsType<CreatedAtActionResult>(response);
    }

}