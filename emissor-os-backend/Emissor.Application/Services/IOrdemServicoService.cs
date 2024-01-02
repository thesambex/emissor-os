using Emissor.Domain.DTOs.OrdemServico;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Application.Services;

public interface IOrdemServicoService
{
    Task<IActionResult> AbrirOS(JwtSecurityToken token, AbrirOSDTO body);
    Task<IActionResult> GetOS(Guid id);
}
