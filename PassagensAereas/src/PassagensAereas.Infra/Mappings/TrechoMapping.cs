using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Infra.Mappings
{
    public class TrechoMapping : IEntityTypeConfiguration<Trecho>
    {
        public void Configure(EntityTypeBuilder<Trecho> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Distancia);
            builder.HasOne(p => p.LocalA).WithMany();
            builder.HasOne(p => p.LocalB).WithMany();
        }
    }
}