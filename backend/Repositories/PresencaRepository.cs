using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Context;
using backend.Domains;
using backend.Interfaces;
using backend.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class PresencaRepository : IPresencaRepository
    {
        GufosContext _contexto = new GufosContext();
        public async Task<Presenca> Alterar(Presenca presenca)
        {
            _contexto.Entry(presenca).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            return presenca;
        }

        public Presenca BuscarPorID(int id)
        {
            Presenca presencaBuscada = _contexto.Presenca
                .Include("Evento")
                .Include("Usuario")
                .ToList().Find(p => p.PresencaId == id) ;
            return presencaBuscada;
        }

        public async Task<Presenca> Excluir(Presenca presenca)
        {
            _contexto.Presenca.Remove(presenca);
            await _contexto.SaveChangesAsync();
            return presenca;
        }

        public List<Presenca> FiltrarPorNome(FiltroViewModel filtro)
        {
            List<Presenca> presencas = _contexto.Presenca
                .Include("Evento")
                .Include("Usuario")
                .Where(p => p.Usuario.Nome.Contains(filtro.Palavra)).ToList();

            return presencas;
        }

        public async Task<List<Presenca>> Listar()
        {
            return await _contexto.Presenca
                .Include("Evento")
                .Include("Usuario")
                .ToListAsync();
        }

        public List<Presenca> Ordenar()
        {
            List <Presenca> presenca = _contexto.Presenca
                .Include("Evento")
                .Include("Usuario")
                .OrderByDescending(p => p.Status).ToList();

            return presenca;
        }

        public async Task<Presenca> Salvar(Presenca presenca)
        {
            await _contexto.AddAsync(presenca);

            await _contexto.SaveChangesAsync();

            return presenca;
        }
    }
}