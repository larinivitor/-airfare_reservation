using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Infra.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Admin).IsRequired();
            builder.Property(p => p.PrimeiroNome).HasMaxLength(100).IsRequired();
            builder.Property(p => p.UltimoNome).HasMaxLength(100).IsRequired();
            builder.Property(p => p.CPF).HasMaxLength(11).IsRequired();
            builder.Property(p => p.DataNascimento).HasMaxLength(10).IsRequired();
            builder.Property(p => p.Login).HasMaxLength(128).IsRequired();
            builder.Property(p => p.Senha).HasMaxLength(128).IsRequired();

            builder.HasMany(p => p.Reservas).WithOne();
        }
    }
}