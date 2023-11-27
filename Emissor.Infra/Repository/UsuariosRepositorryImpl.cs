using Emissor.Application.Database;
using Emissor.Application.Repository;
using Emissor.Domain.Entities;
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

    public Task<Usuario> CriarUsuario()
    {
        throw new NotImplementedException();
    }

}
