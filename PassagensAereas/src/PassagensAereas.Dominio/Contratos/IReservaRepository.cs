using System.Collections.Generic;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Dominio.Contratos
{
    public interface IReservaRepository
    {
        void Salvar(int idUsuario, Reserva reserva);
        void Delete(int id);
        Reserva GetReserva(int id);
        List<Reserva> GetReservas(int idUsuario);
    }
}