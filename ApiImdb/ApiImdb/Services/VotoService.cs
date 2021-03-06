﻿using ApiImdb.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiImdb.Services
{
    public class VotoService
    {
        private readonly Contexto _contexto;

        public VotoService(Contexto contexto)
        {
            _contexto = contexto;
        }

        #region CadastrarVoto
        public void CadastrarVoto(int idFilme, Classificacao classificacao, string nomeUsuario, string senhaUsuario)
        {
            Criptografia criptografia = new Criptografia();
            var senhaCriptografada = string.IsNullOrEmpty(senhaUsuario) ? senhaUsuario : criptografia.GerarHashMd5(senhaUsuario);
            var filme = _contexto.Filmes.FirstOrDefault(f => f.FilmeId == idFilme);
            var usuario = _contexto.Usuarios.FirstOrDefault(x => x.Nome == nomeUsuario && x.Senha == senhaCriptografada && x.Status == "Ativo");
            Voto voto = new Voto();
            voto.Usuario = usuario;
            voto.Filme = filme;
            voto.Classificacao = classificacao;

            if (filme != null && usuario != null)
            {
                _contexto.Votos.Add(voto);
                _contexto.SaveChanges();
            }
        }
        #endregion

        #region ObterVotoDeFilme
        public List<Voto> ObterVotoDeFilme(int idFilme)
        {
            var votos = _contexto.Votos
                .Where(f => f.Filme.FilmeId == idFilme).ToList();
            _contexto.Filmes.ToList();
            _contexto.Atores.ToList();
            _contexto.Diretores.ToList();
            _contexto.Usuarios.ToList();
            return votos;
        }
        #endregion

        #region ObterMediaPontuacaoFilme
        public MediaVoto ObterMediaPontuacaoPorFilme(int idFilme)
        {
            var votos = _contexto.Votos
                .Where(f => f.Filme.FilmeId == idFilme).ToList();

            var mediaVotos = votos.Average(x => (int)x.Classificacao);
            _contexto.Atores.ToList();
            _contexto.Diretores.ToList();

            MediaVoto media = new MediaVoto();
            media.Pontuacao = Math.Round(mediaVotos);
            media.Filme = _contexto.Filmes
                .FirstOrDefault(f => f.FilmeId == idFilme);
            return media;
        }
        #endregion

        #region ObterMediaPontuacaoFilmes
        public List<MediaVoto> ObterMediaPontuacaoFilmes()
        {
            var votos = _contexto.Votos.ToList();
            _contexto.Atores.ToList();
            _contexto.Diretores.ToList();
            List<MediaVoto> listaDeMedia = new List<MediaVoto>();
            MediaVoto media;
            var filmes = _contexto.Filmes.ToList();
            foreach (var filme in filmes)
            {
                var mediaVotos = votos
                    .Where(x => x.Filme.FilmeId == filme.FilmeId)
                    .Average(x => (int)x.Classificacao);
                media = new MediaVoto();
                media.Filme = filme;
                media.Pontuacao = Math.Round(mediaVotos);

                listaDeMedia.Add(media);
            }

            var listaOrdenada = listaDeMedia.GroupBy(x => new { x.Filme.Nome, x.Pontuacao })
                                             .Select(group => new
                                             {
                                                 group.Key.Nome,
                                                 group.Key.Pontuacao
                                             })
                                             .OrderBy(ord => new { ord.Nome, ord.Pontuacao });
            return listaDeMedia;
        }
        #endregion
    }
}
