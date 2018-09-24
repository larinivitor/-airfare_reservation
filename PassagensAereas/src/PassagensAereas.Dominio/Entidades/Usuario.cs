using System;
using System.Collections.Generic;

namespace PassagensAereas.Dominio.Entidades
{
    public class Usuario
    {
        private Usuario() { }
        public Usuario(string primeiroNome, string ultimoNome, string cPF, 
                        DateTime dataNascimento, string login, string senha, 
                        bool admin, List<Reserva> reservas)
        {
            this.PrimeiroNome = primeiroNome;
            this.UltimoNome = ultimoNome;
            this.CPF = cPF;
            this.DataNascimento = dataNascimento;
            this.Login = login;
            this.Senha = senha;
            this.Admin = admin;
            this.Reservas = reservas;
        }
        public int Id { get; private set; }
        public string PrimeiroNome { get; private set; }
        public string UltimoNome { get; private set; }
        public string CPF { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public string Login { get; private set; }
        public string Senha { get; private set; }
        public bool Admin { get; private set; } //para saber se usuário é administrador
        public List<Reserva> Reservas { get; private set; }

        public void Atualizar(Usuario usuarioAlterado)
        {
            this.PrimeiroNome = usuarioAlterado.PrimeiroNome;
            this.UltimoNome = usuarioAlterado.UltimoNome;
            this.CPF = usuarioAlterado.CPF;
            this.DataNascimento = usuarioAlterado.DataNascimento;
            this.Login = usuarioAlterado.Login;
            this.Admin = usuarioAlterado.Admin;
            this.Reservas = usuarioAlterado.Reservas;
            this.Senha = usuarioAlterado.Senha;
        }

        public void AlterarSenha(string senha)
        {
            this.Senha = senha;
        }
    }
}