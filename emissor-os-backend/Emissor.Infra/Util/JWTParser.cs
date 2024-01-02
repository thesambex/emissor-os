using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Util;

public class JWTParser
{

    private readonly string? _authorization;

    public JWTParser(string? authorization)
    {
        _authorization = authorization;
    }

    public JwtSecurityToken? GetJWT()
    {
        if (string.IsNullOrEmpty(_authorization)) return null;
        
        var bearer = _authorization.Substring("Bearer ".Length).Trim();
        var handler = new JwtSecurityTokenHandler();
        return handler.ReadToken(bearer) as JwtSecurityToken;
    }

}
