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

internal class ProdutoMapping : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("produtos", "estoque");
        builder.Property(e => e.Id).HasColumnName("id").HasDefaultValueSql("uuid_generate_v4()");
        builder.Property(e => e.Descricao).HasColumnName("descricao").HasMaxLength(60).IsRequired();
        builder.Property(e => e.Referencia).HasColumnName("referencia").HasMaxLength(10).IsRequired();
        builder.Property(e => e.CodigoBarra).HasColumnName("codigo_barra").HasMaxLength(13);
        builder.HasIndex(e => e.Referencia).IsUnique();
        builder.Property(e => e.Unidade).HasColumnName("unidade").HasColumnType("tipo_unidades");
    }

}
