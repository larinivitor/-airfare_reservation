using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.WebApi.Models.Request
{
    public class TrechoDto
    {
        public string Nome { get; set; }
        public int IdLocalA { get; set; }
        public int IdLocalB { get; set; }
    }
}