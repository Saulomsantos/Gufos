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
    public class LocalizacoesController : ControllerBase
    {
        LocalizacaoRepository _repositorio = new LocalizacaoRepository();

        [HttpGet]
        public async Task<ActionResult<List<Localizacao>>> Get(){
            List<Localizacao> localizacoes = await _repositorio.Listar();

            if (localizacoes == null)
            {
                return NotFound();
            }

            return localizacoes;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Localizacao>> Get(int id){
            Localizacao localizacao = await _repositorio.BuscarPorID(id);

            if (localizacao == null)
            {
                return NotFound(new {mensagem = "Nenhuma localização encontrado para o ID informado"});
            }

            return localizacao;
        }

        [HttpPost]
        public async Task<ActionResult<Localizacao>> Post(Localizacao localizacao){
            try
            {
                await _repositorio.Salvar(localizacao);
            }
            catch (System.Exception)
            {
                throw;
            }

            return localizacao;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Localizacao localizacao){
            // Se o ID do objeto não existir, retorna erro 400 - BadRequest
            if (id != localizacao.LocalizacaoId)
            {
                return BadRequest();
            }

            try
            {
                await _repositorio.Alterar(localizacao);
            }
            catch (System.Exception)
            {
                // Verifica se o objeto inserido existe no banco
                Localizacao localizacao_valida = await _repositorio.BuscarPorID(id);

                if (localizacao_valida == null)
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
        public async Task<ActionResult<Localizacao>> Delete(int id){
            Localizacao localizacao_buscada = await _repositorio.BuscarPorID(id);

            if (localizacao_buscada == null)
            {
                return NotFound(new {mensagem = "Nenhuma localização encontrada para o ID informado"});
            }

            await _repositorio.Excluir(localizacao_buscada);

            return localizacao_buscada;
        }

        [HttpGet("FiltrarPorNome")]
        public ActionResult<List<Localizacao>> GetFiltrar(FiltroViewModel filtro){

            List<Localizacao> localizacoes_filtradas = _repositorio.FiltrarPorNome(filtro);

            return localizacoes_filtradas;
        }

        [HttpGet("Ordenar")]
        public ActionResult<List<Localizacao>> GetOrdenar(){

            List<Localizacao> localizacoes_ordenadas = _repositorio.Ordenar();

            return localizacoes_ordenadas;
        }
    }
}