using Emissor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Application.Repository;

public interface IUsuariosRepository
{
    Task<Usuario> CriarUsuario(Usuario usuario);
    Task<Usuario?> GetUsuarioById(Guid id);
    Task<Usuario?> GetUsuarioByNomeUsuario(string username);
    Task<bool> IssetUsuarioByNomeUsuario(string username);
    Task DeletarUsuario(Guid id);
}
