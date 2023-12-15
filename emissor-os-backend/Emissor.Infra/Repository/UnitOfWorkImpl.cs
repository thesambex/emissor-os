using Emissor.Application.Database;
using Emissor.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Repository;

public class UnitOfWorkImpl : IUnitOfWork
{

    private readonly PgContext _pgContext;

    public UnitOfWorkImpl(PgContext pgContext)
    {
        _pgContext = pgContext;
    }

    public async Task Begin()
    {
        await _pgContext.Database.BeginTransactionAsync();
    }

    public async Task Commit()
    {
        await _pgContext.SaveChangesAsync();
        await _pgContext.Database.CommitTransactionAsync();
    }

    public async Task Rollback()
    {
        await _pgContext.Database.RollbackTransactionAsync();
    }
}
