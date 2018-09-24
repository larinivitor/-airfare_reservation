using System;

namespace PassagensAereas.WebApi.Models.Request
{
    public class UsuarioDto
    {
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool Admin { get; set; }
    }
}