using Emissor.Application.Factory;
using Emissor.Application.Repository;
using Emissor.Application.Services;
using Emissor.Domain.DTOs.Standard;
using Emissor.Domain.DTOs.Usuarios;
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

    private readonly PasswordHashingManager passwordHashing = new PasswordHashingManager();
    private readonly IUsuariosRepository _usuariosRepository;
    private readonly ILogger _logger;

    public UsuariosServiceImpl(ILogger<UsuariosServiceImpl> logger, IAbstractRepositoryFactory abstractRepositoryFactory)
    {
        _usuariosRepository = abstractRepositoryFactory.CreateUsuariosRepository();
        _logger = logger;
    }

    public async Task<IActionResult> CriarUsuario(CriarUsuarioDTO body)
    {
        try
        {
            if (await _usuariosRepository.IssetUsuarioByNomeUsuario(body.NomeUsuario))
            {
                return new ObjectResult(new ErrorResponseDTO("Já existe um usuário com este nome de usuário", "nome_usuario", null))
                {
                    StatusCode = StatusCodes.Status409Conflict
                };
            }

            var usuario = new Usuario();
            usuario.Nome = body.Nome;
            usuario.NomeUsuario = body.NomeUsuario;
            usuario.Senha = passwordHashing.GenerateHash(body.Senha!);
            usuario = await _usuariosRepository.CriarUsuario(usuario);

            var response = new CriarUsuarioDTO(usuario.Id, usuario.Nome, usuario.NomeUsuario, null);

            return new CreatedAtActionResult("GetUsuario", null, new { id = response.Id }, response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao criar o usuário: ${ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<IActionResult> Atualizar(Guid id, AtualizarUsuarioDTO body)
    {
        var usuario = await _usuariosRepository.GetUsuarioById(id);
        if (usuario == null)
        {
            return new NotFoundResult();
        }

        if (!body.NomeUsuario.Equals(usuario.NomeUsuario))
        {
            if (await _usuariosRepository.IssetUsuarioByNomeUsuario(body.NomeUsuario))
            {
                return new ConflictObjectResult(new ErrorResponseDTO("Já existe um usuário com este nome de usuário", "nome_usuario", null));
            }
        }

        try
        {
            var data = new Usuario()
            {
                Nome = body.Nome,
                NomeUsuario = body.NomeUsuario,
                Senha = passwordHashing.GenerateHash(usuario.Nome)
            };

            await _usuariosRepository.AtualizarUsuario(id, data);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao atualizar o usuário: ${ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        return new OkResult();
    }

    public async Task<IActionResult> Deletar(Guid id)
    {
        try
        {
            await _usuariosRepository.DeletarUsuario(id);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao deletar o usuário ${id} {ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        return new NoContentResult();
    }

    public async Task<IActionResult> GetUsuarioById(Guid id)
    {
        var usuario = await _usuariosRepository.GetUsuarioById(id);
        if (usuario == null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(new UsuarioDTO(usuario.Id, usuario.Nome, usuario.NomeUsuario));
    }

    public async Task<IActionResult> GetUsuarioByNomeUsuario(string username)
    {
        var usuario = await _usuariosRepository.GetUsuarioByNomeUsuario(username);
        if (usuario == null)
        {
            return new NotFoundResult();
        }

        return new OkObjectResult(new UsuarioDTO(usuario.Id, usuario.Nome, usuario.NomeUsuario));
    }

}
