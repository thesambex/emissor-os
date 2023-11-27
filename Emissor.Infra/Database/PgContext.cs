using Emissor.Infra.Database.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Application.Database;

public class PgContext : DbContext
{

    public PgContext(DbContextOptions<PgContext> options) 
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresExtension("uuid-osp");
        builder.ApplyConfiguration(new UsuarioMapping());
    }

}
