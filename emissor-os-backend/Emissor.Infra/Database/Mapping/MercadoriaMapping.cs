using Emissor.Domain.Entities;
using Emissor.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Database.Mapping;

internal class MercadoriaMapping : IEntityTypeConfiguration<Mercadoria>
{
    public void Configure(EntityTypeBuilder<Mercadoria> builder)
    {
        builder.ToTable("mercadorias", "estoque");
        builder.Property(e => e.Id).HasColumnName("id").HasDefaultValueSql("uuid_generate_v4()");
        builder.Property(e => e.Descricao).HasColumnName("descricao").HasMaxLength(60).IsRequired();
        builder.Property(e => e.Referencia).HasColumnName("referencia").HasMaxLength(10).IsRequired();
        builder.Property(e => e.CodigoBarra).HasColumnName("codigo_barra").HasMaxLength(13);
        builder.Property(e => e.Preco).HasColumnName("preco").HasColumnType("DECIMAL(8,2)").IsRequired();
        builder.HasIndex(e => e.Referencia).IsUnique();
        builder.Property(e => e.Unidade).HasColumnName("unidade").IsRequired();
    }

}
