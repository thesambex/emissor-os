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
        await _pgContext.OrdensServico.AddAsync(ordemServico);
        await _pgContext.SaveChangesAsync();
        return ordemServico;
    }

    public async Task<OrdemServico?> GetOSById(Guid id) => await _pgContext.OrdensServico.Include(e => e.Cliente).FirstOrDefaultAsync(e => e.Id == id);

    public async Task<OrdemServico?> Finalizar(Guid id, OrdemServico input)
    {
        var ordemServico = await _pgContext.OrdensServico.Include(e => e.Cliente).FirstOrDefaultAsync(e => e.Id == id);
        if (ordemServico == null) { return null; }

        ordemServico.DtFim = input.DtFim;
        ordemServico.ValorFinal = input.ValorFinal;

        await _pgContext.SaveChangesAsync();
        return ordemServico;
    }

    public async Task<bool> DeletarOS(Guid id)
    {
        var ordemServico = await _pgContext.OrdensServico.FindAsync(id);
        if(ordemServico == null) { return false; }

        _pgContext.OrdensServico.Remove(ordemServico);
        await _pgContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExisteOS(Guid id) => await _pgContext.OrdensServico.CountAsync(e => e.Id == id) > 0;

}
