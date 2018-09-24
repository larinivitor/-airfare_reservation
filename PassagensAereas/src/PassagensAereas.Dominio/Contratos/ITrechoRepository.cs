using System.Collections.Generic;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Dominio.Contratos
{
    public interface ITrechoRepository
    {
        void Salvar(Trecho trecho);
        void Editar(int id, Trecho trechoAlterado);
        bool PodeDeletar(int id);
        void Delete(int id);
        Trecho GetTrecho(int id);
        List<Trecho> GetTrechos();
    }
}