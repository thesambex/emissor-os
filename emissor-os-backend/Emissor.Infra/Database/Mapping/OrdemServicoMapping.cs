using Emissor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Database.Mapping;

internal class OrdemServicoMapping : IEntityTypeConfiguration<OrdemServico>
{
    public void Configure(EntityTypeBuilder<OrdemServico> builder)
    {
        builder.ToTable("ordens_servico", "ordens_servico");
        builder.Property(e => e.Id).HasColumnName("id").HasDefaultValueSql("uuid_generate_v4()");
        builder.Property(e => e.Numero).HasColumnName("numero").HasColumnType("SERIAL NOT NULL");
        builder.Property(e => e.ClienteId).HasColumnName("cliente_id").IsRequired();
        builder.Property(e => e.AtendenteId).HasColumnName("atendente_id").IsRequired();
        builder.Property(e => e.Descricao).HasColumnName("descricao").IsRequired();
        builder.Property(e => e.Observacoes).HasColumnName("observacoes");
        builder.Property(e => e.ValorHora).HasColumnName("valor_hora").HasColumnType("DECIMAL(8,2)").IsRequired();
        builder.Property(e => e.ValorFinal).HasColumnName("valor_final").HasColumnType("DECIMAL(10,2)");
        builder.Property(e => e.DtInicio).HasColumnName("dt_inicio").HasColumnType("TIMESTAMPTZ").IsRequired();
        builder.Property(e => e.DtFim).HasColumnName("dt_fim").HasColumnType("TIMESTAMPTZ");
    }

}
