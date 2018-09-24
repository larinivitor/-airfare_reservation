using System.Collections.Generic;
using Geolocation;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Dominio.Servicos
{
    public static class TrechoService
    {
        public static List<string> Validar(Trecho trecho)
        {
            List<string> inconsistencias = new List<string>();

            if (string.IsNullOrEmpty(trecho.Nome?.Trim()))
                inconsistencias.Add($"O campo {nameof(trecho.Nome)} não pode ser nulo.");

            if (GeoCalculator.GetDistance(
                trecho.LocalA.LatitudeLocal, trecho.LocalA.LongitudeLocal, 
                trecho.LocalB.LatitudeLocal, trecho.LocalB.LongitudeLocal, 
                1) == 0)
                inconsistencias.Add($"Os campos {nameof(trecho.LocalA)} e {nameof(trecho.LocalB)} não podem ser o mesmo.");
            
            return inconsistencias;
        }
    }
}