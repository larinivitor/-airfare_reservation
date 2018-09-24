using System;
using System.Collections.Generic;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.WebApi.Models.Response
{
    public class UsuarioResponseDto
    {
        private UsuarioResponseDto() { }
        public UsuarioResponseDto(string primeiroNome, string ultimoNome, string cPF, 
                        DateTime dataNascimento, string login, 
                        bool admin, List<Reserva> reservas)
        {
            this.PrimeiroNome = primeiroNome;
            this.UltimoNome = ultimoNome;
            this.CPF = cPF;
            this.DataNascimento = dataNascimento;
            this.Login = login;
            this.Admin = admin;
            this.Reservas = reservas;
        }
        public int Id { get; private set; }
        public string PrimeiroNome { get; private set; }
        public string UltimoNome { get; private set; }
        public string CPF { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Login { get; private set; }
        public bool Admin { get; private set; } //para saber se usuário é administrador
        public List<Reserva> Reservas { get; private set; }
    }
}