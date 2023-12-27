using Emissor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Database.Mapping;

internal class OrdemServicoMercadoriaMapping : IEntityTypeConfiguration<OrdemServicoMercadoria>
{
    public void Configure(EntityTypeBuilder<OrdemServicoMercadoria> builder)
    {
        builder.ToTable("ordens_servico_mercadoias", "ordens_servico");
        builder.Property(e => e.Id).HasColumnName("id").HasDefaultValueSql("uuid_generate_v4()");
        builder.Property(e => e.OrdemServicoId).HasColumnName("ordem_servico_id").IsRequired();
        builder.Property(e => e.ProdutoId).HasColumnName("produto_id").IsRequired();
        builder.Property(e => e.Quantidade).HasColumnName("quantidade").HasColumnType("DECIMAL(7,2)").IsRequired();
        builder.HasOne(e => e.OrdemServico).WithMany(e => e.OrdemServicoMercadorias).HasForeignKey(e => e.OrdemServicoId);
        builder.HasOne(e => e.Produto).WithOne(e => e.OrdemServicoMercadoria).HasForeignKey<OrdemServicoMercadoria>(e => e.ProdutoId);
    }
    
}
