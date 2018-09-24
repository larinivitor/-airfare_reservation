using System.Collections.Generic;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Dominio.Servicos
{
    public static class LocalService
    {
        public static List<string> Validar(Local local)
        {
            List<string> inconsistencias = new List<string>();
                
            if (string.IsNullOrEmpty(local.Nome?.Trim()))
                inconsistencias.Add($"O campo {nameof(local.Nome)} não pode ser nulo.");

            if (local.LatitudeLocal == 0)
                inconsistencias.Add($"O campo {nameof(local.LatitudeLocal)} não pode ser 0.");

            if (local.LongitudeLocal == 0)
                inconsistencias.Add($"O campo {nameof(local.LongitudeLocal)} não pode ser 0.");
            
            return inconsistencias;
        }
        
    }
}