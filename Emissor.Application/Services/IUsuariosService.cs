using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emissor.Domain.DTOs;

namespace Emissor.Application.Services;

public interface IUsuariosService
{

    Task<IActionResult> CriarUsuario(CriarUsuarioDTO body);

}
