using Emissor.Application.Database;
using Emissor.Application.Repository;
using Emissor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Repository;

internal class UsuariosRepositorryImpl : IUsuariosRepository
{

    private readonly PgContext _pgContext;

    public UsuariosRepositorryImpl(PgContext pgContext)
    {
        _pgContext = pgContext;
    }

    public async Task<Usuario> CriarUsuario(Usuario usuario)
    {
        await _pgContext.Usuarios.AddAsync(usuario);
        await _pgContext.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario?> GetUsuarioById(Guid id) => await _pgContext.Usuarios.FindAsync(id);

    public async Task<Usuario?> GetUsuarioByNomeUsuario(string username) => await _pgContext.Usuarios.FirstOrDefaultAsync(e => e.NomeUsuario == username);

    public async Task<bool> IssetUsuarioByNomeUsuario(string username) => await _pgContext.Usuarios.CountAsync(e => e.NomeUsuario == username) > 0;

    public async Task AtualizarUsuario(Guid id, Usuario usuario)
    {
        var usu = await _pgContext.Usuarios.FindAsync(id);
        if (usu == null) { return; }

        usu.Nome = usuario.Nome;
        usu.Senha = usuario.Senha;

        if (!usu.NomeUsuario.Equals(usuario.NomeUsuario))
        {
            usu.NomeUsuario = usuario.NomeUsuario;
        }

        await _pgContext.SaveChangesAsync();
    }

    public async Task<bool> DeletarUsuario(Guid id)
    {
        var usuario = await _pgContext.Usuarios.FindAsync(id);
        if (usuario == null) { return false; }

        _pgContext.Usuarios.Remove(usuario);
        await _pgContext.SaveChangesAsync();
        return true;
    }

}
