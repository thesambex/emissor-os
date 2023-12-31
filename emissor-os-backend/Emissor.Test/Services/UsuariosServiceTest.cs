﻿using Emissor.Application.Database;
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
    public async void Deve_Cadastrar_Um_Novo_Usuario()
    {
        var usuario = new CriarUsuarioDTO(null, "José Dores", "dosed", "J@s123");
        var result = await usuariosService.CriarUsuario(usuario);

        Assert.NotNull(result);
        Assert.IsType<CreatedAtActionResult>(result);
    }

    [Fact]
    public async void Deve_Obter_Um_Usuario()
    {
        var response = await usuariosService.GetUsuarioById(Guid.Parse("3ceb9fa1-2db8-4a36-8745-42b6c51f4375"));

        Assert.NotNull(response);
        Assert.IsType<OkObjectResult>(response);

        var payload = ((OkObjectResult)response).Value as UsuarioDTO;
        Assert.NotNull(payload);
    }

    [Fact(Skip = "Não é necessário atualizar no momento")]
    public async void Deve_Atualizar_Um_Usuario()
    {

        var usuarioDTO = new AtualizarUsuarioDTO(null, "Bufalo Bill", "manoel", "man@6l");
        var result = await usuariosService.Atualizar(Guid.Parse("3ceb9fa1-2db8-4a36-8745-42b6c51f4375"), usuarioDTO);
        
        Assert.NotNull(result);
        Assert.IsType<OkResult>(result);
    }

    [Fact(Skip = "Não deve deletar um usuário no momento")]
    public async void Deve_Deletar_Um_Usuario()
    {
        var result = await usuariosService.Deletar(Guid.Parse("3ceb9fa1-2db8-4a36-8745-42b6c51f4375"));

        Assert.NotNull(result);
        Assert.IsType<NoContentResult>(result);
    }

}
