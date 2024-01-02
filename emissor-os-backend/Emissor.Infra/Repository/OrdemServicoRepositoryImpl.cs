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

public class OrdemServicoRepositoryImpl : IOrdemServicoRepository
{

    private readonly PgContext _pgContext;

    public OrdemServicoRepositoryImpl(PgContext pgContext)
    {
        _pgContext = pgContext;
    }

    public async Task<OrdemServico> CriarOS(OrdemServico ordemServico)
    {
        await _pgContext.OrdemServicos.AddAsync(ordemServico);
        await _pgContext.SaveChangesAsync();
        return ordemServico;
    }

    public async Task<OrdemServico?> GetOSById(Guid id) => await _pgContext.OrdemServicos.Include(e => e.Cliente).FirstOrDefaultAsync(e => e.Id == id);

}
