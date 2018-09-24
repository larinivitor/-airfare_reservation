using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PassagensAereas.Dominio.Contratos;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Infra.Repository
{
    public class ReservaRepository : IReservaRepository 
    {
        private PassagensAereasContext context;

        public ReservaRepository(PassagensAereasContext context)
        {
            this.context = context;
        }

        public void Delete(int id)
        {
            var reservaOpcional = context.ReservaOpcional
                .Include(p => p.Opcional)
                .Include(p => p.Reserva)
                .Where(p => p.Reserva.Id == id)
                .ToList();

            context.ReservaOpcional.RemoveRange(reservaOpcional);

            var reserva = context.Reservas.FirstOrDefault(p => p.Id == id);
            
            context.Reservas.Remove(reserva);
        }

        public Reserva GetReserva(int id)
        {
            var reservaOpcional = context.ReservaOpcional
                .Include(p => p.Opcional)
                .Include(p => p.Reserva)
                .AsNoTracking()
                .Where(p => p.Reserva.Id == id)
                .ToList();

            var reserva = context.Reservas
                .Include(p => p.ClasseVoo)
                .Include(p => p.Trecho)
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);

            var opcionais = new List<Opcional>();

            foreach(var rO in reservaOpcional)
                opcionais.Add(rO.Opcional);

            reserva?.AtualizarOpcionais(opcionais);

            return reserva;
        }

        public List<Reserva> GetReservas(int idUsuario)
        {
            var usuario = context.Usuarios
                .Include(p => p.Reservas)
                .FirstOrDefault(p => p.Id == idUsuario);

            var reservas = new List<Reserva>();
            
            foreach(var reserva in usuario.Reservas)
                reservas.Add(
                    context.Reservas
                        .Include(p => p.ClasseVoo)
                        .Include(p => p.Trecho)
                        .AsNoTracking()
                        .FirstOrDefault(p => p.Id == reserva.Id)
                );

            foreach(var reserva in reservas)
            {
                var reservaOpcional = context.ReservaOpcional
                    .Include(p => p.Opcional)
                    .Include(p => p.Reserva)
                    .AsNoTracking()
                    .Where(p => p.Reserva.Id == reserva.Id)
                    .ToList();

                var opcionais = new List<Opcional>();

                if (reservaOpcional.Any())
                    foreach(var opcional in reservaOpcional)
                        opcionais.Add(opcional.Opcional);
                
                reserva.AtualizarOpcionais(opcionais);
            }
             
            return reservas;
        }

        public void Salvar(int idUsuario, Reserva reserva)
        {
            var usuario = context.Usuarios.Include(p => p.Reservas).FirstOrDefault(p => p.Id == idUsuario);

            if (reserva.Opcionais.Any())
                foreach(var opcional in reserva.Opcionais)
                    if(opcional == null)
                        reserva.AtualizarOpcionais(null);
                        
            usuario.Reservas.Add(reserva);

            if (reserva.Opcionais != null)
                if (reserva.Opcionais.Any())
                    foreach(var opcional in reserva.Opcionais)
                        if(opcional != null)
                            context.ReservaOpcional.Add(new ReservaOpcional(reserva, opcional));            
        }
    }
}