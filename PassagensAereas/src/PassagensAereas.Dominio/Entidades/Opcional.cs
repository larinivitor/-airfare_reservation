using System;

namespace PassagensAereas.Dominio.Entidades
{
    public class Opcional
    {
        private Opcional() { }
        public Opcional(string nome, string descricao, double valor)
        {
            this.Nome = nome;
            this.Descricao = descricao;
            this.Valor = valor;
        }
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double Valor { get; private set; }

        public void Atualizar(Opcional opcionalAlterado)
        {
            this.Nome = opcionalAlterado.Nome;
            this.Descricao = opcionalAlterado.Descricao;
            this.Valor = opcionalAlterado.Valor;
        }
    }
}