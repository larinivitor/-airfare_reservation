using System.Collections.Generic;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Dominio.Servicos
{
    public static class ClasseVooService
    {
        public static List<string> Validar(ClasseVoo classeVoo)
        {
            List<string> inconsistencias = new List<string>();
                
            if (string.IsNullOrEmpty(classeVoo.Nome?.Trim()))
                inconsistencias.Add($"O campo {nameof(classeVoo.Nome)} não pode ser nulo.");

            if (classeVoo.ValorFixo == 0)
                inconsistencias.Add($"O campo {nameof(classeVoo.ValorFixo)} não pode ser 0.");

            if (classeVoo.ValorMilha == 0)
                inconsistencias.Add($"O campo {nameof(classeVoo.ValorMilha)} não pode ser 0.");
            
            return inconsistencias;
        }
    }
}