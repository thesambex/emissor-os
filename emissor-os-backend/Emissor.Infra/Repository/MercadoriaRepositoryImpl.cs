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

internal class MercadoriaRepositoryImpl : IMercadoriaRepository
{

    private readonly PgContext _pgContext;

    public MercadoriaRepositoryImpl(PgContext pgContext)
    {
        _pgContext = pgContext;
    }

    public async Task<Mercadoria> CriarMercadoria(Mercadoria mercadoria)
    {
        await _pgContext.Mercadorias.AddAsync(mercadoria);
        await _pgContext.SaveChangesAsync();
        return mercadoria;
    }

    public async Task<Mercadoria?> GetMercadoria(Guid id) => await _pgContext.Mercadorias.FindAsync(id);

    public async Task<bool> IssetMercadoriaByReferencia(string referencia) => await _pgContext.Mercadorias.CountAsync(e => e.Referencia.Equals(referencia)) > 0;
    
    public async Task<bool> IssetMercadoriaByCodigoBarra(string codigoBarra) => await _pgContext.Mercadorias.CountAsync(e => e.CodigoBarra!.Equals(codigoBarra)) > 0;

    public async Task<bool> DeletarMercadoria(Guid id)
    {
        var mercadoria = await _pgContext.Mercadorias.FindAsync(id);
        if (mercadoria == null) { return false; }

        _pgContext.Mercadorias.Remove(mercadoria);
        await _pgContext.SaveChangesAsync();
        return true;
    }
}
