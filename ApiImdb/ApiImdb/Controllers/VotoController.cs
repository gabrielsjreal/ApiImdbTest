using System.Collections.Generic;
using ApiImdb.Models;
using ApiImdb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiImdb.Controllers
{
    [Route("api/filme/[controller]")]
    public class VotoController : ControllerBase
    {
        private readonly VotoService _serviceVoto;
        private readonly PaginacaoService _servicePaginacao;

        public VotoController(VotoService votoService, PaginacaoService servicePaginacao)
        {
            _serviceVoto = votoService;
            _servicePaginacao = servicePaginacao;
        }

        [HttpPost("votar")]
        public IActionResult PostVotar(int idFilme, Classificacao classificacao)
        {
            _serviceVoto.CadastrarVoto(idFilme, classificacao);
            return Ok();
        }

        [HttpGet("filme-votos")]
        [AllowAnonymous]
        public List<Voto> GetVotosDeUmFilme(int idFilme)
        {
            List<Voto> votos = _serviceVoto.ObterVotoDeFilme(idFilme);
            return votos;
        }

        [HttpGet("filme-votos-paginacao")]
        [Authorize]
        public List<Voto> GetVotosPaginacao(Paginacao paginacao)
        {
            return _servicePaginacao.RetornarPaginacaoVoto(paginacao);
        }

        [HttpGet("media-filme")]
        public MediaVoto GetMediaPorFilme(int idFilme)
        {
            MediaVoto media = _serviceVoto.ObterMediaPontuacaoPorFilme(idFilme);
            return media;
        }

        [HttpGet("media-filmes")]
        [Authorize]
        public List<MediaVoto> GetMediaGeralFilmes()
        {
            return _serviceVoto.ObterMediaPontuacaoFilmes();
        }
    }
}
