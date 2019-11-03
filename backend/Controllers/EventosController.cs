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
    public class EventosController : ControllerBase
    {
        EventoRepository _repositorio = new EventoRepository();

        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get(){
            List<Evento> eventos = await _repositorio.Listar();

            if (eventos == null)
            {
                return NotFound();
            }

            return eventos;
        }

        [HttpGet("{id}")]
        public ActionResult<Evento> Get(int id){
            Evento evento = _repositorio.BuscarPorID(id);

            if (evento == null)
            {
                return NotFound(new {mensagem = "Nenhum usuário encontrado para o ID informado"});
            }

            return evento;
        }

        [HttpPost]
        public async Task<ActionResult<Evento>> Post(Evento evento){
            try
            {
                await _repositorio.Salvar(evento);
            }
            catch (System.Exception)
            {
                throw;
            }

            return evento;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Evento evento){
            // Se o ID do objeto não existir, retorna erro 400 - BadRequest
            if (id != evento.EventoId)
            {
                return BadRequest();
            }

            try
            {
                await _repositorio.Alterar(evento);
            }
            catch (System.Exception)
            {
                // Verifica se o objeto inserido existe no banco
                Evento evento_valido = _repositorio.BuscarPorID(id);

                if (evento_valido == null)
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
        public async Task<ActionResult<Evento>> Delete(int id){
            Evento evento_buscado = _repositorio.BuscarPorID(id);

            if (evento_buscado == null)
            {
                return NotFound(new {mensagem = "Nenhum usuário encontrado para o ID informado"});
            }

            await _repositorio.Excluir(evento_buscado);

            return evento_buscado;
        }

        [HttpGet("FiltrarPorNome")]
        public ActionResult<List<Evento>> GetFiltrar(FiltroViewModel filtro){

            List<Evento> eventos_filtrados = _repositorio.FiltrarPorNome(filtro);

            return eventos_filtrados;
        }

        [HttpGet("Ordenar")]
        public ActionResult<List<Evento>> GetOrdenar(){

            List<Evento> eventos_ordenados = _repositorio.Ordenar();

            return eventos_ordenados;
        }
    }
}