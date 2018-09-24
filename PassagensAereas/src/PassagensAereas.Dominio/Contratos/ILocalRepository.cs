using System.Collections.Generic;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Dominio.Contratos
{
    public interface ILocalRepository
    {
        void Salvar(Local local);
        void Editar(int id, Local localAlterado);
        bool PodeDeletar(int id);
        void Delete(int id);
        Local GetLocal(int id);
        List<Local> GetLocais();
    }
}