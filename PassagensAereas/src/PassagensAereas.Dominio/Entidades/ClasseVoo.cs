using System;

namespace PassagensAereas.Dominio.Entidades
{
    public class ClasseVoo
    {
        private ClasseVoo() { }

        public ClasseVoo(string nome, double valorFixo, double valorMilha)
        {
            this.Nome = nome;
            this.ValorFixo = valorFixo;
            this.ValorMilha = valorMilha;
        }
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public double ValorFixo { get; private set; }
        public double ValorMilha { get; private set; }

        public void Atualizar(ClasseVoo classeVooAlterada)
        {
            this.Nome = classeVooAlterada.Nome;
            this.ValorFixo = classeVooAlterada.ValorFixo;
            this.ValorMilha = classeVooAlterada.ValorMilha;
        }
    }
}