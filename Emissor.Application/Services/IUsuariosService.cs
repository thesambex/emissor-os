using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emissor.Domain.DTOs.Usuarios;

namespace Emissor.Application.Services;

public interface IUsuariosService
{
    Task<IActionResult> CriarUsuario(CriarUsuarioDTO body);
    Task<IActionResult> GetUsuarioById(Guid id);
    Task<IActionResult> GetUsuarioByNomeUsuario(string username);
    Task<IActionResult> Atualizar(Guid id, AtualizarUsuarioDTO body);
    Task<IActionResult> Deletar(Guid id);
}
