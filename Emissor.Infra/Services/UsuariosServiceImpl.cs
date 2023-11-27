using Emissor.Application.Factory;
using Emissor.Application.Repository;
using Emissor.Application.Services;
using Emissor.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Services;

public class UsuariosServiceImpl : IUsuariosService
{

    private readonly IUsuariosRepository _usuariosRepository;

    public UsuariosServiceImpl(IAbstractRepositoryFactory abstractRepositoryFactory)
    {
        _usuariosRepository = abstractRepositoryFactory.CreateUsuariosRepository();
    }

    public async Task<IActionResult> CriarUsuario(CriarUsuarioDTO body)
    {
        return new OkResult();
    }
}
