using ApiImdb.Models;
using System.Collections.Generic;
using System.Linq;

namespace ApiImdb.Services
{
    public class FilmeService
    {
        private readonly Contexto _contexto;

        public void CadastrarFilme(Filme filme)
        {
            Ator ator1 = new Ator()
            {
                Nome = "AAA"
            };
            Ator ator2 = new Ator()
            {
                Nome = "BB"
            };

            List<Ator> listAtor = new List<Ator>();
            listAtor.Add(ator1);
            listAtor.Add(ator2);

            filme.ListaDeAtores = listAtor;

            _contexto.Add(filme);
            _contexto.SaveChanges();
        }

        public List<Filme> ObterFilmes()
        {
            List<Filme> filmes = new List<Filme>();
            var listafilmes = _contexto.Filmes.ToList();
            _contexto.Atores.ToList();
            _contexto.Diretores.ToList();
            return listafilmes;
        }
    }
}
