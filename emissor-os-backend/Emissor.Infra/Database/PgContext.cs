using Emissor.Domain.Entities;
using Emissor.Domain.Enums;
using Emissor.Infra.Database.Mapping;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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
    public DbSet<Mercadoria> Mercadorias { get; set; }
    public DbSet<OrdemServico> OrdensServico { get; set; }
    public DbSet<OrdemServicoMercadoria> OrdemServicoMercadorias { get; set; }

    public PgContext(DbContextOptions<PgContext> options) 
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasPostgresExtension("uuid-ossp");
        builder.HasSequence<long>("numero_os_seq");
        builder.HasPostgresEnum<TipoUnidades>();
        
        builder.ApplyConfiguration(new UsuarioMapping());
        builder.ApplyConfiguration(new ClienteMapping());
        builder.ApplyConfiguration(new MercadoriaMapping());
        builder.ApplyConfiguration(new OrdemServicoMapping());
        builder.ApplyConfiguration(new OrdemServicoMercadoriaMapping());
    }

}
