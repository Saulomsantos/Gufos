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
    public class TiposUsuarioController : ControllerBase
    {
        TipoUsuarioRepository _repositorio = new TipoUsuarioRepository();

        [HttpGet]
        public async Task<ActionResult<List<TipoUsuario>>> Get(){
            List<TipoUsuario> tiposUsuario = await _repositorio.Listar();

            if (tiposUsuario == null)
            {
                return NotFound();
            }

            return tiposUsuario;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoUsuario>> Get(int id){
            TipoUsuario tipoUsuario = await _repositorio.BuscarPorID(id);

            if (tipoUsuario == null)
            {
                return NotFound();
            }

            return tipoUsuario;
        }

        [HttpPost]
        public async Task<ActionResult<TipoUsuario>> Post(TipoUsuario tipoUsuario){
            try
            {
                await _repositorio.Salvar(tipoUsuario);
            }
            catch (System.Exception)
            {
                throw;
            }

            return tipoUsuario;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, TipoUsuario tipoUsuario){
            // Se o ID do objeto não existir, retorna erro 400 - BadRequest
            if (id != tipoUsuario.TipoUsuarioId)
            {
                return BadRequest();
            }

            try
            {
                await _repositorio.Alterar(tipoUsuario);
            }
            catch (System.Exception)
            {
                // Verifica se o objeto inserido existe no banco
                TipoUsuario tipoUsuario_valido = await _repositorio.BuscarPorID(id);

                if (tipoUsuario_valido == null)
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
        public async Task<ActionResult<TipoUsuario>> Delete(int id){
            TipoUsuario tipoUsuario_buscado = await _repositorio.BuscarPorID(id);

            if (tipoUsuario_buscado == null)
            {
                return NotFound(new {mensagem = "Nenhum tipo usuário encontrado para o ID informado"});
            }

            await _repositorio.Excluir(tipoUsuario_buscado);

            return tipoUsuario_buscado;
        }

        [HttpGet("FiltrarPorNome")]
        public ActionResult<List<TipoUsuario>> GetFiltrar(FiltroViewModel filtro){

            List<TipoUsuario> tiposUsuario_filtrados = _repositorio.FiltrarPorNome(filtro);

            return tiposUsuario_filtrados;
        }

        [HttpGet("Ordenar")]
        public ActionResult<List<TipoUsuario>> GetOrdenar(){

            List<TipoUsuario> tiposUsuario_ordenados = _repositorio.Ordenar();

            return tiposUsuario_ordenados;
        }
    }
}