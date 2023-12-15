using Emissor.Domain.DTOs.Auth;
using Emissor.Domain.DTOs.Usuarios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Application.Services;

public interface IAuthService
{
    Task<IActionResult> SignUp(CriarUsuarioDTO body);
    Task<IActionResult> SignIn(SignInDTO body);
}
