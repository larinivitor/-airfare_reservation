using System;
using System.Collections.Generic;
using System.Linq;

namespace PassagensAereas.Dominio.Entidades
{
    public class Reserva
    {
        private Reserva() { }

        public Reserva(Trecho trecho, ClasseVoo classeVoo, List<Opcional> opcionais)
        {
            this.Trecho = trecho;
            this.ClasseVoo = classeVoo;
            this.Opcionais = opcionais;
            this.ValorTotal = this.CalcularvalorTotal();
        }
        public int Id { get; private set; }
        public Trecho Trecho { get; private set; }
        public ClasseVoo ClasseVoo { get; private set; }
        public double ValorTotal { get; private set; }
        public List<Opcional> Opcionais { get; private set; }
        private double CalcularvalorTotal()
        {
            double valorBase = ClasseVoo.ValorFixo + (Trecho.Distancia * ClasseVoo.ValorMilha);
            double valorOpcionais = 0;

            if(Opcionais.Any())
                foreach(var opcional in Opcionais)
                    if(opcional != null)
                        valorOpcionais += valorBase * opcional.Valor;

            return (valorBase + valorOpcionais);
        }

        public void AtualizarOpcionais(List<Opcional> opcionais)
        {
            Opcionais = opcionais;
        }
    }
}