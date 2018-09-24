using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PassagensAereas.Dominio.Contratos;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Infra.Repository
{
    public class OpcionalRepository : IOpcionalRepository
    {
        private PassagensAereasContext context;

        public OpcionalRepository(PassagensAereasContext context)
        {
            this.context = context;
        }

        public bool PodeDeletar(int id)
        {
            var reservasDependentes = context.Reservas
                .Include(p => p.Opcionais)
                .AsNoTracking()
                .ToList();


            foreach(var reserva in reservasDependentes)
                foreach(var opcional in reserva.Opcionais)
                    if(opcional.Id == id)
                        return false;

            return true;
        }

        public void Delete(int id)
        {
            var opcional = context.Opcionais.FirstOrDefault(p => p.Id == id);
            context.Opcionais.Remove(opcional);
        }

        public void Editar(int id, Opcional opcionalAlterado)
        {
            var opcional = context.Opcionais.FirstOrDefault(p => p.Id == id);
            opcional.Atualizar(opcionalAlterado);
        }

        public List<Opcional> GetOpcionais()
        {
            return context.Opcionais.AsNoTracking().ToList();
        }

        public Opcional GetOpcional(int id)
        {
            return context.Opcionais.FirstOrDefault(p => p.Id == id);
        }

        public void Salvar(Opcional opcional)
        {
            context.Opcionais.Add(opcional);
        }
    }
}