using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Repositories;
using backend.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        UsuarioRepository _repositorio = new UsuarioRepository();

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> Get(){
            List<Usuario> usuarios = await _repositorio.Listar();

            if (usuarios == null)
            {
                return NotFound();
            }

            return usuarios;
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> Get(int id){
            Usuario usuario = _repositorio.BuscarPorID(id);

            if (usuario == null)
            {
                return NotFound(new {mensagem = "Nenhum usuário encontrado para o ID informado"});
            }

            return usuario;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuario){
            try
            {
                await _repositorio.Salvar(usuario);
            }
            catch (System.Exception)
            {
                throw;
            }

            return usuario;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Usuario usuario){
            // Se o ID do objeto não existir, retorna erro 400 - BadRequest
            if (id != usuario.UsuarioId)
            {
                return BadRequest();
            }

            try
            {
                await _repositorio.Alterar(usuario);
            }
            catch (System.Exception)
            {
                // Verifica se o objeto inserido existe no banco
                Usuario usuario_valido = _repositorio.BuscarPorID(id);

                if (usuario_valido == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            // NoContent retorna 204 - Sem conteúdo
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id){
            Usuario usuario_buscado = _repositorio.BuscarPorID(id);

            if (usuario_buscado == null)
            {
                return NotFound(new {mensagem = "Nenhum usuário encontrado para o ID informado"});
            }

            await _repositorio.Excluir(usuario_buscado);

            return usuario_buscado;
        }

        [HttpGet("FiltrarPorNome")]
        public ActionResult<List<Usuario>> GetFiltrar(FiltroViewModel filtro){

            List<Usuario> usuarios_filtrados = _repositorio.FiltrarPorNome(filtro);

            return usuarios_filtrados;
        }

        [HttpGet("Ordenar")]
        public ActionResult<List<Usuario>> GetOrdenar(){

            List<Usuario> usuarios_ordenados = _repositorio.Ordenar();

            return usuarios_ordenados;
        }
    }
}