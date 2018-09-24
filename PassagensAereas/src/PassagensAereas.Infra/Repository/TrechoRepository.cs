using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PassagensAereas.Dominio.Contratos;
using PassagensAereas.Dominio.Entidades;

namespace PassagensAereas.Infra.Repository
{
    public class TrechoRepository : ITrechoRepository
    {
        private PassagensAereasContext context;

        public TrechoRepository(PassagensAereasContext context)
        {
            this.context = context;
        }

        public bool PodeDeletar(int id)
        {
            var reservasDependentes = context.Reservas
                .Include(p => p.Trecho)
                .AsNoTracking()
                .ToList()
                .Where(p => p.Trecho.Id == id);

            if(reservasDependentes.Any())
                return false;
            
            return true;
        }

        public void Delete(int id)
        {
            var trecho = context.Trechos.FirstOrDefault(p => p.Id == id);
            context.Trechos.Remove(trecho);
        }

        public void Editar(int id, Trecho trechoAlterado)
        {
            var trecho = context.Trechos.FirstOrDefault(p => p.Id == id);
            trecho.Atualizar(trechoAlterado);
        }

        public Trecho GetTrecho(int id)
        {
            return context.Trechos
                .Include(p => p.LocalA)
                .Include(p => p.LocalB)
                .FirstOrDefault(p => p.Id == id);
        }

        public List<Trecho> GetTrechos()
        {
            return context.Trechos
                .Include(p => p.LocalA)
                .Include(p => p.LocalB)
                .AsNoTracking().ToList();
        }

        public void Salvar(Trecho trecho)
        {
            context.Trechos.Add(trecho);
        }
    }
}