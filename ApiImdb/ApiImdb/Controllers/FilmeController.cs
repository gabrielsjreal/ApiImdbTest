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
        private readonly FilmeService _serviceFilme;

        public FilmeController(Contexto contexto, FilmeService filmeService)
        {
            _contexto = contexto;
            _serviceFilme = filmeService;
        }

        [HttpPost("filme/preencher-tabela-filmes")]
        public IActionResult PostPreencherTabelaFilmes()
        {
            #region Mock
            Ator ator1 = new Ator()
            {               
                Nome = "Harrison Ford"
            };
            Ator ator2 = new Ator()
            {
                Nome = "Carrie Fisher"
            };
            Ator ator3 = new Ator()
            {
                Nome = "Robert Downey Jr."
            };
            Ator ator4 = new Ator()
            {
                Nome = "Scarlett Johansson"
            };

            List<Ator> listAtores1 = new List<Ator>();
            listAtores1.Add(ator1);
            listAtores1.Add(ator2);

            List<Ator> listAtores2 = new List<Ator>();
            listAtores2.Add(ator3);
            listAtores2.Add(ator4);

            Diretor diretor1 = new Diretor()
            {
                Nome = "George Lucas"
            };

            Diretor diretor2 = new Diretor()
            {
                Nome = "Joss Whedon"
            };

            Filme filme1 = new Filme();
            filme1.Nome = "Stars Wars - Guerra nas Estrlas";
            filme1.Genero = Genero.Ficcao;
            filme1.Diretor = diretor1;
            filme1.ListaDeAtores = listAtores1;

            Filme filme2 = new Filme();
            filme2.Nome = "Os Vingadores";
            filme2.Genero = Genero.Ficcao;
            filme2.Diretor = diretor2;
            filme2.ListaDeAtores = listAtores2;
            #endregion
            try
            {
                if(_contexto.Filmes.Count() == 0)
                {
                    _contexto.Filmes.Add(filme1);
                    _contexto.Filmes.Add(filme2);
                    _contexto.SaveChanges();
                }                

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpDelete("filme/excluir-dados-tabelas")]
        public IActionResult DeleteLimparTabelas()
        {
            try
            {
                var removeAllFilmes = _contexto.Filmes.ToList();
                _contexto.RemoveRange(removeAllFilmes);
                var removeAllAtores = _contexto.Atores.ToList();
                _contexto.RemoveRange(removeAllAtores);
                var removeAllDiretores = _contexto.Diretores.ToList();
                _contexto.RemoveRange(removeAllDiretores);

                _contexto.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
               
        [HttpPost("filme/cadastrar")]
        [Authorize]
        public IActionResult PostCadastrar(Filme filme)
        {
            if (filme != null)
            {
                FilmeService service = new FilmeService();
                service.CadastrarFilme(filme);

                return Accepted();
            }
            else
            {
                return BadRequest();
            }         

        }

        [HttpGet]
        [Route("filme/obter-filmes")]
        [Authorize]
        public List<Filme> GetFilmes()
        {
            List<Filme> listafilmes = _serviceFilme.ObterFilmes();

            return listafilmes;

        }

        
    }
}
