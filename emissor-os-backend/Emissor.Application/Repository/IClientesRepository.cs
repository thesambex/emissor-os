using Emissor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Application.Repository;

public interface IClientesRepository
{
    Task<Cliente> CriarCliente(Cliente cliente);
    Task<Cliente?> GetClienteById(Guid id);
    Task<bool> IssetClienteByDocumento(string documento);
}
