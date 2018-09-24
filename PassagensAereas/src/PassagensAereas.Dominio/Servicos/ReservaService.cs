using System.Collections.Generic;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Dominio.Servicos
{
    public static class ReservaService
    {
        public static List<string> Validar(Reserva reserva)
        {
            List<string> inconsistencias = new List<string>();
                
            if (reserva.Trecho == null)
                inconsistencias.Add($"O campo {nameof(reserva.Trecho)} não pode ser nulo.");

            if (reserva.ClasseVoo == null)
                inconsistencias.Add($"O campo {nameof(reserva.ClasseVoo)} não pode ser nulo.");
            
            return inconsistencias;
        }
        
    }
}