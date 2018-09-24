using Microsoft.EntityFrameworkCore;
using PassagensAereas.Dominio.Entidades;
using PassagensAereas.Infra.Mappings;

namespace PassagensAereas.Infra
{
    public class PassagensAereasContext : DbContext
    {
        public PassagensAereasContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ClasseVoo> ClassesVoo { get; set; }
        public DbSet<Local> Locais { get; set; }
        public DbSet<Opcional> Opcionais { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Trecho> Trechos { get; set; }
        public DbSet<ReservaOpcional> ReservaOpcional { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClasseVooMapping());
            modelBuilder.ApplyConfiguration(new LocalMapping());
            modelBuilder.ApplyConfiguration(new OpcionalMapping());
            modelBuilder.ApplyConfiguration(new ReservaMapping());
            modelBuilder.ApplyConfiguration(new TrechoMapping());
            modelBuilder.ApplyConfiguration(new ReservaOpcionalMapping());
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
        }
    }
}