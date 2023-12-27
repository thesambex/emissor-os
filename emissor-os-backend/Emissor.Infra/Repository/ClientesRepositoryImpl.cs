using Emissor.Application.Database;
using Emissor.Application.Repository;
using Emissor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Repository;

internal class ClientesRepositoryImpl : IClientesRepository
{

    private readonly PgContext _pgContext;

    public ClientesRepositoryImpl(PgContext pgContext)
    {
        _pgContext = pgContext;
    }

    public async Task<Cliente> CriarCliente(Cliente cliente)
    {
        await _pgContext.Clientes.AddAsync(cliente);
        await _pgContext.SaveChangesAsync();
        return cliente;
    }

}
