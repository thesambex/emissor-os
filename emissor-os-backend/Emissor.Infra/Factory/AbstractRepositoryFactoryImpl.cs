using Emissor.Application.Database;
using Emissor.Application.Factory;
using Emissor.Application.Repository;
using Emissor.Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Factory;

public class AbstractRepositoryFactoryImpl : IAbstractRepositoryFactory
{

    private readonly PgContext _pgContext;

    public AbstractRepositoryFactoryImpl(PgContext pgContext)
    {
        _pgContext = pgContext;
    }

    public IUsuariosRepository CreateUsuariosRepository() => new UsuariosRepositorryImpl(_pgContext);

    public IClientesRepository CreateClientesRepository() => new ClientesRepositoryImpl(_pgContext);

    public IOrdemServicoRepository CreateOrdemServicoRepository() => new OrdemServicoRepositoryImpl(_pgContext);

    public IMercadoriaRepository CreateMercadoriaRepository() => new MercadoriaRepositoryImpl(_pgContext);

}
