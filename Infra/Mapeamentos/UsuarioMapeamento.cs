using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapeamentos
{
    public class UsuarioMapeamento : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).UseIdentityColumn().HasColumnType("BIGINT");
            builder.Property(d => d.Nome).IsRequired().HasMaxLength(80).HasColumnName("nome").HasColumnType("VARCHAR(80)");
            builder.Property(d => d.Apelido).HasMaxLength(30).HasColumnName("apelido").HasColumnType("VARCHAR(30)");
            builder.Property(d => d.Senha).IsRequired().HasMaxLength(100).HasColumnName("senha").HasColumnType("VARCHAR(100)");
            builder.Property(d => d.Email).IsRequired().HasMaxLength(180).HasColumnName("email").HasColumnType("VARCHAR(180)");
            builder.Property(d => d.RefreshToken).HasMaxLength(100).HasColumnName("refresh_token").HasColumnType("VARCHAR(100)");
            builder.Property(d => d.DataExpiracaoToken).HasColumnName("data_expiracao_token");
        }
    }
}
