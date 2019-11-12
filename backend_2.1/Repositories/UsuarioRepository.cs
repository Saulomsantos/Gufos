using Microsoft.EntityFrameworkCore;
using senai_2s2019_CodeXP_Gufos.Contexts;
using senai_2s2019_CodeXP_Gufos.Domains;
using senai_2s2019_CodeXP_Gufos.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_2s2019_CodeXP_Gufos.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        GufosContext _contexto = new GufosContext();

        public async Task<Usuario> Alterar(Usuario usuario)
        {
            _contexto.Entry(usuario).State = EntityState.Modified;
            await _contexto.SaveChangesAsync();
            return usuario;
        }

        public Usuario BuscarPorID(int id)
        {
            Usuario usuarioBuscado = _contexto.Usuario
                .Select(u => new Usuario()
                {
                    UsuarioId = u.UsuarioId,
                    Nome = u.Nome,
                    Email = u.Email,
                    TipoUsuarioId = u.TipoUsuarioId,

                    TipoUsuario = new TipoUsuario()
                    {
                        TipoUsuarioId = u.TipoUsuario.TipoUsuarioId,
                        Titulo = u.TipoUsuario.Titulo,
                    }
                })
                .ToList().Find(u => u.UsuarioId == id);
            return usuarioBuscado;
        }

        public async Task<Usuario> Excluir(Usuario usuario)
        {
            _contexto.Usuario.Remove(usuario);
            await _contexto.SaveChangesAsync();
            return usuario;
        }

        public List<Usuario> FiltrarPorNome(string filtro)
        {
            List<Usuario> usuarios = _contexto.Usuario
                .Select(u => new Usuario()
                {
                    UsuarioId = u.UsuarioId,
                    Nome = u.Nome,
                    Email = u.Email,
                    TipoUsuarioId = u.TipoUsuarioId,

                    TipoUsuario = new TipoUsuario()
                    {
                        TipoUsuarioId = u.TipoUsuario.TipoUsuarioId,
                        Titulo = u.TipoUsuario.Titulo,
                    }
                })
                .Where(u => u.Nome.Contains(filtro)).ToList();

            return usuarios;
        }

        public async Task<List<Usuario>> Listar()
        {
            return await _contexto.Usuario
                //.Include("TipoUsuario")
                .Select(u => new Usuario()
                {
                    UsuarioId = u.UsuarioId,
                    Nome = u.Nome,
                    Email = u.Email,
                    TipoUsuarioId = u.TipoUsuarioId,
                    
                    TipoUsuario = new TipoUsuario()
                    {
                        TipoUsuarioId = u.TipoUsuario.TipoUsuarioId,
                        Titulo = u.TipoUsuario.Titulo,
                    }
                })
                .ToListAsync();
        }

        public List<Usuario> Ordenar()
        {
            List<Usuario> usuarios = _contexto.Usuario
                .Select(u => new Usuario()
                {
                    UsuarioId = u.UsuarioId,
                    Nome = u.Nome,
                    Email = u.Email,
                    TipoUsuarioId = u.TipoUsuarioId,

                    TipoUsuario = new TipoUsuario()
                    {
                        TipoUsuarioId = u.TipoUsuario.TipoUsuarioId,
                        Titulo = u.TipoUsuario.Titulo,
                    }
                })
                .OrderByDescending(u => u.Nome).ToList();

            return usuarios;
        }

        public Usuario RealizarLogin(string email, string senha)
        {
            Usuario usuarioLogado = _contexto.Usuario
                .Select(u => new Usuario()
                {
                    UsuarioId = u.UsuarioId,
                    Nome = u.Nome,
                    Email = u.Email,
                    Senha = u.Senha,
                    TipoUsuarioId = u.TipoUsuarioId,

                    TipoUsuario = new TipoUsuario()
                    {
                        TipoUsuarioId = u.TipoUsuario.TipoUsuarioId,
                        Titulo = u.TipoUsuario.Titulo,
                    }
                })
                .ToList()
                .Find(u => u.Email == email && u.Senha == senha);

            return usuarioLogado;
        }

        public async Task<Usuario> Salvar(Usuario usuario)
        {
            await _contexto.AddAsync(usuario);

            await _contexto.SaveChangesAsync();

            return usuario;
        }
    }
}
