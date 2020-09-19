using ApiImdb.Models;
using ApiImdb.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiImdbTeste
{
    [TestClass]
    public class FilmeTest
    {
        [TestMethod]
        public void ObterMediaPontuacaoPorFilme()
        {
            #region Setup
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
            filme1.FilmeId = 1;
            filme1.Nome = "Stars Wars - Guerra nas Estrlas";
            filme1.Genero = Genero.Ficcao;
            filme1.Diretor = diretor1;
            filme1.ListaDeAtores = listAtores1;

            Filme filme2 = new Filme();
            filme2.FilmeId = 2;
            filme2.Nome = "Os Vingadores";
            filme2.Genero = Genero.Ficcao;
            filme2.Diretor = diretor2;
            filme2.ListaDeAtores = listAtores2;

            List<Filme> filmes = new List<Filme>();
            filmes.Add(filme1);
            filmes.Add(filme2);

            #region Mock: Tabela de Usuarios
            Criptografia criptografia = new Criptografia();
            Usuario usuario1 = new Usuario()
            {
                Nome = "Gerente01",
                Senha = criptografia.GerarHashMd5("Gerente01"),
                Tipo = "administrador",
                Status = "Ativo"
            };
            Usuario usuario2 = new Usuario()
            {
                Nome = "Gerente02",
                Senha = criptografia.GerarHashMd5("Gerente02"),
                Tipo = "administrador",
                Status = "Desativado"
            };
            Usuario usuario3 = new Usuario()
            {
                Nome = "Atendente01",
                Senha = criptografia.GerarHashMd5("Atendente01"),
                Tipo = "funcionario",
                Status = "Ativo"
            };
            Usuario usuario4 = new Usuario()
            {
                Nome = "Atendente02",
                Senha = criptografia.GerarHashMd5("Atendente02"),
                Tipo = "funcionario",
                Status = "Ativo"
            };
            Usuario usuario5 = new Usuario()
            {
                Nome = "Atendente03",
                Senha = criptografia.GerarHashMd5("Atendente03"),
                Tipo = "funcionario",
                Status = "Desativado"
            };
            List<Usuario> usuarios = new List<Usuario>();
            usuarios.Add(usuario1);
            usuarios.Add(usuario2);
            usuarios.Add(usuario3);
            usuarios.Add(usuario4);
            usuarios.Add(usuario5);
            #endregion

            #region Mock: Votos
            Voto voto1 = new Voto()
            {
                VotoId = 1,
                Classificacao = Classificacao.MuitoRuim,
                Filme = filme1,
                Usuario = usuario2
            };
            Voto voto2 = new Voto()
            {
                VotoId = 1,
                Classificacao = Classificacao.Bom,
                Filme = filme1,
                Usuario = usuario3
            };
            Voto voto3 = new Voto()
            {
                VotoId = 1,
                Classificacao = Classificacao.Otimo,
                Filme = filme1,
                Usuario = usuario4
            };
            Voto voto4 = new Voto()
            {
                VotoId = 1,
                Classificacao = Classificacao.Ruim,
                Filme = filme1,
                Usuario = usuario3
            };
            Voto voto5 = new Voto()
            {
                VotoId = 2,
                Classificacao = Classificacao.Otimo,
                Filme = filme2,
                Usuario = usuario1
            };
            Voto voto6 = new Voto()
            {
                VotoId = 2,
                Classificacao = Classificacao.MuitoBom,
                Filme = filme2,
                Usuario = usuario1
            };
            List<Voto> votos = new List<Voto>();
            votos.Add(voto1);
            votos.Add(voto2);
            votos.Add(voto3);
            votos.Add(voto4);
            votos.Add(voto5);
            votos.Add(voto6);
            votos.Add(voto1);
            votos.Add(voto3);
            #endregion

            #endregion

            var mediaVotos = votos.Where(x => x.VotoId == 1).Average(x => (int)x.Classificacao);
            MediaVoto mediaFilme1 = new MediaVoto();
            mediaFilme1.Pontuacao = Math.Round(mediaVotos, 2);
            mediaFilme1.Filme = filmes.FirstOrDefault(f => f.FilmeId == 1);

            Assert.AreEqual(mediaFilme1.Pontuacao, 1.83);

        }
    }
}