using Microsoft.EntityFrameworkCore;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Infra.Mappings
{
    public class ReservaMapping : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Reserva> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ValorTotal);
            builder.HasOne(p => p.ClasseVoo).WithMany();
            builder.HasOne(p => p.Trecho).WithMany();
        }
    }
}