using Emissor.Application.Factory;
using Emissor.Application.Repository;
using Emissor.Application.Security.Password;
using Emissor.Application.Services;
using Emissor.Domain.DTOs;
using Emissor.Domain.DTOs.Standard;
using Emissor.Domain.Entities;
using Emissor.Infra.Security.Password;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Services;

public class UsuariosServiceImpl : IUsuariosService
{

    private readonly IUsuariosRepository _usuariosRepository;
    private readonly ILogger _logger;

    public UsuariosServiceImpl(ILogger<UsuariosServiceImpl> logger, IAbstractRepositoryFactory abstractRepositoryFactory)
    {
        _usuariosRepository = abstractRepositoryFactory.CreateUsuariosRepository();
        _logger = logger;
    }

    public async Task<IActionResult> CriarUsuario(CriarUsuarioDTO body)
    {

        if(await _usuariosRepository.IssetUsuarioByNomeUsuario(body.NomeUsuario))
        {
            return new ObjectResult(new ErrorResponseDTO() { Message = "Já existe um usuário com este nome de usuário", Field = "nome_usuario" })
            {
                StatusCode = StatusCodes.Status409Conflict
            };
        }

        var passwordHashing = new PasswordHashing(new BCryptPasswordHashStrategy());

        var usuario = new Usuario();
        usuario.Nome = body.Nome;
        usuario.NomeUsuario = body.NomeUsuario;

        try
        {
            usuario.Senha = passwordHashing.Hash(usuario.Nome);
            usuario = await _usuariosRepository.CriarUsuario(usuario);
        } catch (Exception ex) {
            _logger.LogError($"Falha ao criar o usuário: ${ex.InnerException}");
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        var response = new CriarUsuarioDTO() {
            Id = usuario.Id,
            Nome = usuario.Nome,
            NomeUsuario = usuario.NomeUsuario,
        };

        return new CreatedAtActionResult("ObterUsuarioById", null, new {id = response.Id}, response);
    }
}
