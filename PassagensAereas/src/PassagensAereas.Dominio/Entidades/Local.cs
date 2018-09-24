using System;
using Geolocation;

namespace PassagensAereas.Dominio.Entidades
{
    public class Local
    {
        private Local() { }

        public Local(string nome, double latitude, double longitude)
        {

            this.Nome = nome;
            this.LatitudeLocal = latitude;
            this.LongitudeLocal = longitude;
        }
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public double LatitudeLocal { get; private set; }
        public double LongitudeLocal { get; private set; }

        public void Atualizar(Local localAlterado)
        {
            this.Nome = localAlterado.Nome;
            this.LatitudeLocal = localAlterado.LatitudeLocal;
            this.LongitudeLocal = localAlterado.LongitudeLocal;
        }
    }
}