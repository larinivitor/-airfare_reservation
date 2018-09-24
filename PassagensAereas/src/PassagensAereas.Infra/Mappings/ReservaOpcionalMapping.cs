using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Infra.Mappings
{
    public class ReservaOpcionalMapping : IEntityTypeConfiguration<ReservaOpcional>
    {

        public void Configure(EntityTypeBuilder<ReservaOpcional> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Opcional).WithMany();
            builder.HasOne(p => p.Reserva).WithMany();
            
        }
    }
}