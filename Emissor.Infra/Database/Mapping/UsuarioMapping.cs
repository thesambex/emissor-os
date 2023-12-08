using Emissor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emissor.Infra.Database.Mapping;

internal class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{

    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios", "usuarios");
        builder.Property(e => e.Id).HasColumnName("id").HasDefaultValueSql("uuid_generate_v4()");
        builder.Property(e => e.Nome).HasColumnName("nome").HasMaxLength(60).IsRequired();
        builder.Property(e => e.NomeUsuario).HasColumnName("nome_usuario").HasMaxLength(20).IsRequired();
        builder.HasIndex(e => e.NomeUsuario).IsUnique();
        builder.Property(e => e.Senha).HasColumnName("senha").HasMaxLength(72).IsRequired();
    }

}
