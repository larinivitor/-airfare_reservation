using System;
using Geolocation;

namespace PassagensAereas.Dominio.Entidades
{
    public class Trecho
    {
        private Trecho() { }
        public Trecho(string nome, Local localA, Local localB)
        {
            this.Nome = nome;
            this.LocalA = localA;
            this.LocalB = localB;
            this.Distancia = GeoCalculator.GetDistance(
                LocalA.LatitudeLocal, LocalA.LongitudeLocal, 
                LocalB.LatitudeLocal, LocalB.LongitudeLocal, 
                1);
        }
        public int Id { get; private set; }
        public double Distancia { get; private set; }
        public string Nome { get; private set; }
        public Local LocalA { get; private set; }
        public Local LocalB { get; private set; }

        public void Atualizar(Trecho trechoAlterado)
        {
            this.Nome = trechoAlterado.Nome;
            this.Distancia = GeoCalculator.GetDistance(
                LocalA.LatitudeLocal, LocalA.LongitudeLocal, 
                LocalB.LatitudeLocal, LocalB.LongitudeLocal, 
                1);
            this.LocalA = trechoAlterado.LocalA;
            this.LocalB = trechoAlterado.LocalB;
        }
    }
}