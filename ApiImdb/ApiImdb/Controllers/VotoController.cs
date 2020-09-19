using System.Collections.Generic;
using ApiImdb.Models;
using ApiImdb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiImdb.Controllers
{
    [Route("api/[controller]")]
    public class VotoController : ControllerBase
    {
        private VotoService ServiceVoto;
        private readonly Contexto _contexto;

        public VotoController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpPost("votar")]
        public IActionResult PostVotar(string nomeUsuario, string senhaUsuario, int idFilme, Classificacao classificacao)
        {
            ServiceVoto = new VotoService(_contexto);
            ServiceVoto.CadastrarVoto(idFilme, classificacao, nomeUsuario, senhaUsuario);
            return Ok();
        }

        [HttpGet("filme-votos")]
        [AllowAnonymous]
        public List<Voto> GetVotosDeUmFilme(int idFilme)
        {
            ServiceVoto = new VotoService(_contexto);
            List<Voto> votos = ServiceVoto.ObterVotoDeFilme(idFilme);
            return votos;
        }

        [HttpGet("filme-votos-paginacao")]
        [Authorize]
        public List<Voto> GetVotosPaginacao(Paginacao paginacao)
        {
            PaginacaoService ServicePaginacao = new PaginacaoService(_contexto);
            return ServicePaginacao.RetornarPaginacaoVoto(paginacao);
        }

        [HttpGet("media-filme")]
        public MediaVoto GetMediaPorFilme(int idFilme)
        {
            ServiceVoto = new VotoService(_contexto);
            MediaVoto media = ServiceVoto.ObterMediaPontuacaoPorFilme(idFilme);
            return media;
        }

        [HttpGet("media-filmes")]
        [Authorize]
        public List<MediaVoto> GetMediaGeralFilmes()
        {
            ServiceVoto = new VotoService(_contexto);
            return ServiceVoto.ObterMediaPontuacaoFilmes();
        }
    }
}
