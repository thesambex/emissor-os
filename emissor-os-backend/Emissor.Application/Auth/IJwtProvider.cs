using Emissor.Domain.Entities;
using Emissor.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Application.Providers;

public interface IJwtProvider
{
    string GenerateJwt(Usuario usuario);
    UsuarioJwt? DecodeJwt(string? token);
}
