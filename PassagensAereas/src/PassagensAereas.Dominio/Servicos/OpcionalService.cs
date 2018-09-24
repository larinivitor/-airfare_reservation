using System.Collections.Generic;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Dominio.Servicos
{
    public static class OpcionalService
    {
        public static List<string> Validar(Opcional opcional)
        {
            List<string> inconsistencias = new List<string>();
                
            if (string.IsNullOrEmpty(opcional.Nome?.Trim()))
                inconsistencias.Add($"O campo {nameof(opcional.Nome)} não pode ser nulo.");

            if (string.IsNullOrEmpty(opcional.Descricao?.Trim()))
                inconsistencias.Add($"O campo {nameof(opcional.Descricao)} não pode ser nulo.");

            if (opcional.Valor == 0)
                inconsistencias.Add($"O campo {nameof(opcional.Valor)} não pode ser 0.");
            
            return inconsistencias;
        }
    }
}