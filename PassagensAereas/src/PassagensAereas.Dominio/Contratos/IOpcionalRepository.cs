using System.Collections.Generic;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Dominio.Contratos
{
    public interface IOpcionalRepository
    {
        void Salvar(Opcional opcional);
        void Editar(int id, Opcional opcionalAlterado);
        bool PodeDeletar(int id);
        void Delete(int id);
        Opcional GetOpcional(int id);
        List<Opcional> GetOpcionais();
    }
}