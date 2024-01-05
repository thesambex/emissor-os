using Emissor.Application.Database;
using Emissor.Application.Repository;
using Emissor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Repository;

internal class OrdemServicoMercadoriaRepositoryImpl : IOrdemServicoMercadoriasRepository
{

    private readonly PgContext _pgContext;

    public OrdemServicoMercadoriaRepositoryImpl(PgContext pgContext)
    {
        _pgContext = pgContext;
    }

    public async Task CriarOrdemServicoMercadoria(OrdemServicoMercadoria mercadoria)
    {
        await _pgContext.OrdemServicoMercadorias.AddAsync(mercadoria);
    }

}
