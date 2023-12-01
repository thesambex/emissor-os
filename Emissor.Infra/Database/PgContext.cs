using Emissor.Domain.Entities;
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

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Cliente> Clientes { get; set; }

    public PgContext(DbContextOptions<PgContext> options) 
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresExtension("uuid-ossp");
        builder.ApplyConfiguration(new UsuarioMapping());
        builder.ApplyConfiguration(new ClienteMapping());
    }

}
