using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PassagensAereas.Dominio.Contratos;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Infra.Repository
{
    public class ClasseVooRepository : IClasseVooRepository
    {
        private PassagensAereasContext context;

        public ClasseVooRepository(PassagensAereasContext context)
        {
            this.context = context;
        }

        public bool PodeDeletar(int id)
        {
            var reservasDependentes = context.Reservas
                .Include(p => p.ClasseVoo)
                .AsNoTracking()
                .ToList()
                .Where(p => p.ClasseVoo.Id == id);

            if(reservasDependentes.Any())
                return false;
            
            return true;
        }

        public void Delete(int id)
        {
            var classeVoo = context.ClassesVoo.FirstOrDefault(p => p.Id == id);
            context.ClassesVoo.Remove(classeVoo);
        }

        public void Editar(int id, ClasseVoo classeVooAlterada)
        {
            var classeVoo = context.ClassesVoo.FirstOrDefault(p => p.Id == id);
            classeVoo.Atualizar(classeVooAlterada);
        }

        public List<ClasseVoo> GetClasses()
        {
            return context.ClassesVoo.AsNoTracking().ToList();
        }

        public ClasseVoo GetClasseVoo(int id)
        {
            return context.ClassesVoo.FirstOrDefault(p => p.Id == id);
        }

        public void Salvar(ClasseVoo classeVoo)
        {
            context.ClassesVoo.Add(classeVoo);
        }
    }
}