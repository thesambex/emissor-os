using Emissor.Application.Factory;
using Emissor.Application.Repository;
using Emissor.Application.Services;
using Emissor.Domain.DTOs.Auth;
using Emissor.Domain.DTOs.Standard;
using Emissor.Domain.DTOs.Usuarios;
using Emissor.Domain.Entities;
using Emissor.Infra.Security.Password;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Services;

public class AuthServiceImpl : IAuthService
{

    private readonly PasswordHashingManager passwordHashing = new PasswordHashingManager();
    private readonly IUsuariosRepository _usuariosRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public AuthServiceImpl(IConfiguration configuration, ILogger<AuthServiceImpl> logger, IAbstractRepositoryFactory abstractRepositoryFactory)
    {
        _configuration = configuration;
        _usuariosRepository = abstractRepositoryFactory.CreateUsuariosRepository();
        _logger = logger;
    }

    public async Task<IActionResult> SignIn(SignInDTO body)
    {
        try
        {
            var usuario = await _usuariosRepository.GetUsuarioByNomeUsuario(body.NomeUsuario);
            if (usuario == null)
            {
                return new NotFoundResult();
            }

            if (!passwordHashing.VerifyHash(body.Senha, usuario.Senha))
            {
                return new UnauthorizedResult();
            }

            var securityKeys = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));
            var credentials = new SigningCredentials(securityKeys, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                null,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials!
                );

            return new OkObjectResult(new TokenDTO(new JwtSecurityTokenHandler().WriteToken(token)));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao gerar o token de autenticação: {ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    public async Task<IActionResult> SignUp(CriarUsuarioDTO body)
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

        try
        {
            usuario.Senha = passwordHashing.GenerateHash(body.Senha!.Trim());
            usuario = await _usuariosRepository.CriarUsuario(usuario);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Falha ao criar o usuário: ${ex.InnerException}", ex);
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        return new StatusCodeResult(StatusCodes.Status201Created);
    }

}
