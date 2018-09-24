using System.Collections.Generic;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.WebApi.Models.Request
{
    public class ReservaDto
    {
        public int IdTrecho { get; set; }
        public int IdClasseVoo { get; set; }
        public List<int> IdOpcionais { get; set; }
    }
}