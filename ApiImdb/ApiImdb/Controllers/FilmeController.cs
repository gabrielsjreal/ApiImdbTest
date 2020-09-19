using ApiImdb.Models;
using ApiImdb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiImdb.Controllers
{
    [Route("api/[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly Contexto _contexto;
        private FilmeService serviceFilme;

        public FilmeController(Contexto contexto)
        {
            _contexto = contexto;
        }

        [HttpPost("preencher-tabelas")]
        public IActionResult PostPreencherTabelaFilmes()
        {
            serviceFilme = new FilmeService(_contexto);
            serviceFilme.PreencherTabelas();

            return Ok();
        }

        [HttpDelete("excluir-dados-tabelas")]
        public IActionResult DeleteLimparTabelas()
        {
            serviceFilme = new FilmeService(_contexto);
            serviceFilme.LimparTabelas();

            return Ok();
        }        

        [HttpPost("cadastrar")]
        [Authorize]
        public IActionResult PostCadastrar(Filme filme)
        {
            if (filme != null)
            {
                serviceFilme = new FilmeService(_contexto);
                serviceFilme.CadastrarFilme(filme);
            }
                return Accepted();           
        }

        [HttpGet]
        [Route("obter-filmes")]
        [Authorize]
        public List<Filme> GetFilmes()
        {
            serviceFilme = new FilmeService(_contexto);
            List<Filme> listafilmes = serviceFilme.ObterFilmes();

            return listafilmes;

        }

    }
}
