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
    public class PresencasController : ControllerBase
    {
        PresencaRepository _repositorio = new PresencaRepository();

        [HttpGet]
        public async Task<ActionResult<List<Presenca>>> Get(){
            List<Presenca> presencas = await _repositorio.Listar();

            if (presencas == null)
            {
                return NotFound();
            }

            return presencas;
        }

        [HttpGet("{id}")]
        public ActionResult<Presenca> Get(int id){
            Presenca presenca = _repositorio.BuscarPorID(id);

            if (presenca == null)
            {
                return NotFound(new {mensagem = "Nenhuma presença encontrado para o ID informado"});
            }

            return presenca;
        }

        [HttpPost]
        public async Task<ActionResult<Presenca>> Post(Presenca presenca){
            try
            {
                await _repositorio.Salvar(presenca);
            }
            catch (System.Exception)
            {
                throw;
            }

            return presenca;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Presenca presenca){
            // Se o ID do objeto não existir, retorna erro 400 - BadRequest
            if (id != presenca.PresencaId)
            {
                return BadRequest();
            }

            try
            {
                await _repositorio.Alterar(presenca);
            }
            catch (System.Exception)
            {
                // Verifica se o objeto inserido existe no banco
                Presenca presenca_valida = _repositorio.BuscarPorID(id);

                if (presenca_valida == null)
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
        public async Task<ActionResult<Presenca>> Delete(int id){
            Presenca presenca_buscada = _repositorio.BuscarPorID(id);

            if (presenca_buscada == null)
            {
                return NotFound(new {mensagem = "Nenhuma presença encontrada para o ID informado"});
            }

            await _repositorio.Excluir(presenca_buscada);

            return presenca_buscada;
        }

        [HttpGet("FiltrarPorNome")]
        public ActionResult<List<Presenca>> GetFiltrar(FiltroViewModel filtro){

            List<Presenca> presencas_filtradas = _repositorio.FiltrarPorNome(filtro);

            return presencas_filtradas;
        }

        [HttpGet("Ordenar")]
        public ActionResult<List<Presenca>> GetOrdenar(){

            List<Presenca> presencas_ordenadas = _repositorio.Ordenar();

            return presencas_ordenadas;
        }
    }
}