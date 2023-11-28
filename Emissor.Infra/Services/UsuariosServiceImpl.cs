using Emissor.Application.Factory;
using Emissor.Application.Repository;
using Emissor.Application.Security.Password;
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
    private readonly IUnitOfWork _unitOfWork;

    public UsuariosServiceImpl(IAbstractRepositoryFactory abstractRepositoryFactory, IUnitOfWork unitOfWork)
    {
        _usuariosRepository = abstractRepositoryFactory.CreateUsuariosRepository();
        _unitOfWork = unitOfWork;
    }

    public async Task<IActionResult> CriarUsuario(CriarUsuarioDTO body)
    {
        return new OkResult();
    }
}
