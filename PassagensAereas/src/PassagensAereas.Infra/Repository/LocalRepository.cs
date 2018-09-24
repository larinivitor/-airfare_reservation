using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PassagensAereas.Dominio.Contratos;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Infra.Repository
{
    public class LocalRepository : ILocalRepository
    {
        private PassagensAereasContext context;

        public LocalRepository(PassagensAereasContext context)
        {
            this.context = context;
        }

        public bool PodeDeletar(int id)
        {
            var trechosDependentes = context.Trechos
                .Include(p => p.LocalA)
                .Include(p => p.LocalB)
                .AsNoTracking()
                .ToList();

            foreach(var trecho in trechosDependentes)
                if(trecho.LocalA.Id == id || trecho.LocalB.Id == id)
                    return false;
            
            return true;
        }

        public void Delete(int id)
        {
            var local = context.Locais.FirstOrDefault(p => p.Id == id);
            context.Locais.Remove(local);
        }

        public void Editar(int id, Local localAlterado)
        {
            var local = context.Locais.FirstOrDefault(p => p.Id == id);
            local.Atualizar(localAlterado);
        }

        public List<Local> GetLocais()
        {
            return context.Locais.AsNoTracking().ToList();
        }

        public Local GetLocal(int id)
        {
            return context.Locais.FirstOrDefault(p => p.Id == id);
        }

        public void Salvar(Local local)
        {
            context.Locais.Add(local);
        }
    }
}