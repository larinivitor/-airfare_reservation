using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Infra.Mappings
{
    public class ClasseVooMapping : IEntityTypeConfiguration<ClasseVoo>
    {
        public void Configure(EntityTypeBuilder<ClasseVoo> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();
            builder.Property(p => p.ValorFixo).IsRequired();
            builder.Property(p => p.ValorMilha).IsRequired();
        }
    }
}