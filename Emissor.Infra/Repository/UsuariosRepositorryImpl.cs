﻿using Emissor.Application.Database;
using Emissor.Application.Repository;
using Emissor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Repository;

public class UsuariosRepositorryImpl : IUsuariosRepository
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

    public async Task<Usuario?> GetUsuarioByNomeUsuario(string username) => await _pgContext.Usuarios.FirstAsync(e => e.NomeUsuario == username);

    public async Task<bool> IssetUsuarioByNomeUsuario(string username) => await _pgContext.Usuarios.CountAsync(e => e.NomeUsuario == username) > 0;

    public async Task DeletarUsuario(Guid id)
    {
        var usuario = await _pgContext.Usuarios.FindAsync(id);
        if(usuario != null)
        {
            _pgContext.Usuarios.Remove(usuario);
            await _pgContext.SaveChangesAsync();
        }
    }

}
