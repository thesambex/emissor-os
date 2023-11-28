using Emissor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Database.Mapping;

public class ClienteMapping : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("clientes", "clientes");
        builder.Property(e => e.Id).HasColumnName("id").HasDefaultValueSql("uuid_generate_v4()");
        builder.Property(e => e.Nome).HasColumnName("nome").HasMaxLength(60).IsRequired();
        builder.Property(e => e.Documento).HasColumnName("documento").HasMaxLength(14).IsRequired();
        builder.HasIndex(e => e.Documento).IsUnique();
        builder.Property(e => e.Endereco).HasColumnName("endereco").HasMaxLength(60).IsRequired();
        builder.Property(e => e.EnderecoNumero).HasColumnName("endereco_numero");
        builder.Property(e => e.Bairro).HasColumnName("bairro");
        builder.Property(e => e.Municipio).HasColumnName("municipio");
        builder.Property(e => e.IsPJ).HasColumnName("is_pj");
    }

}
