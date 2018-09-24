using System.Collections.Generic;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Dominio.Contratos
{
    public interface IClasseVooRepository
    {
        void Salvar(ClasseVoo classeVoo);
        void Editar(int id, ClasseVoo classeVooAlterada);
        bool PodeDeletar(int id);
        void Delete(int id);
        ClasseVoo GetClasseVoo(int id);
        List<ClasseVoo> GetClasses();
    }
}