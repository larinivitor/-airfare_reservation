namespace PassagensAereas.Dominio.Entidades
{
    public class ReservaOpcional
    {
        private ReservaOpcional(){}
        public ReservaOpcional(Reserva reserva, Opcional opcional)
        {
            this.Reserva = reserva;
            this.Opcional = opcional;
        }
        public int Id { get; private set; }
        public Reserva Reserva { get; private set; }
        public Opcional Opcional { get; private set; }
    }
}