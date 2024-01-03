using Emissor.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Application.Factory;

public interface IAbstractRepositoryFactory
{
    IUsuariosRepository CreateUsuariosRepository();
    IClientesRepository CreateClientesRepository();
    IOrdemServicoRepository CreateOrdemServicoRepository();
    IMercadoriaRepository CreateMercadoriaRepository();
}
