using Emissor.Application.Providers;
using Emissor.Domain.Entities;
using Emissor.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Auth;

public class JwtProviderImpl : IJwtProvider
{

    private readonly IConfiguration _configuration;

    public JwtProviderImpl(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwt(Usuario usuario)
    {
        var securityKeys = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));
        var credentials = new SigningCredentials(securityKeys, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString())
            };

        var token = new JwtSecurityToken(
            _configuration["JwtSettings:Issuer"],
            _configuration["JwtSettings:Audience"],
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials!
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public UsuarioJwt? DecodeJwt(string? authorization)
    {
        if (string.IsNullOrEmpty(authorization)) return null;

        var bearer = authorization.Substring("Bearer ".Length).Trim();
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadToken(bearer) as JwtSecurityToken;
        if (token == null) return null;

        var claims = token.Claims;
        return new UsuarioJwt(
            Guid.Parse(claims.Single(e => e.Type == JwtRegisteredClaimNames.Sub).Value)
            );
    }

}
