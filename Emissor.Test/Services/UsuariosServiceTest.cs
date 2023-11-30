using Emissor.Application.Database;
using Emissor.Application.Services;
using Emissor.Domain.DTOs.Usuarios;
using Emissor.Infra.Factory;
using Emissor.Infra.Services;
using Microsoft.AspNetCore.Http;
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

public class UsuariosServiceTest : IDisposable
{

    private readonly PgContext pgContext;
    private readonly IUsuariosService usuariosService;

    public UsuariosServiceTest() 
    {

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var dbOptions = new DbContextOptionsBuilder<PgContext>()
            .UseNpgsql(configuration.GetConnectionString("Postgresql"));

        pgContext = new PgContext(dbOptions.Options);

        var logger = new Mock<ILogger<UsuariosServiceImpl>>();
        usuariosService = new UsuariosServiceImpl(logger.Object, new AbstractRepositoryFactoryImpl(pgContext));
    }

    public void Dispose()
    {
        pgContext.Dispose();
    }

    [Fact]
    public async void Deve_Atualizar_Um_Usuario_No_Banco()
    {

        var usuarioDTO = new AtualizarUsuarioDTO()
        {
            Nome = "Ives",
            NomeUsuario = "ivl",
            Senha = "163"
        };

        var result = await usuariosService.Atualizar(Guid.Parse("5c958852-b81b-4d36-86b6-ead7708b8444"), usuarioDTO);
        
        Assert.NotNull(result);
        Assert.IsType<OkResult>(result);
    }

}
