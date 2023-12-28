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

    public async Task<Cliente?> GetClienteById(Guid id) => await _pgContext.Clientes.FindAsync(id);

    public async Task<bool> IssetClienteByDocumento(string documento) => await _pgContext.Clientes.CountAsync(e => e.Documento.Equals(documento)) > 0;

}
