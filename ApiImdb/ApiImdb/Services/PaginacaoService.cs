using ApiImdb.Models;
using System.Collections.Generic;
using System.Linq;

namespace ApiImdb.Services
{
    public class PaginacaoService
    {
        private readonly Contexto _contexto;

        public PaginacaoService(Contexto contexto)
        {
            _contexto = contexto;
        }

        #region RetornarPaginacaoVoto
        public List<Voto> RetornarPaginacaoVoto(Paginacao paginacao)
        {
            var votos = _contexto.Votos.ToList();
            _contexto.Filmes.ToList();
            _contexto.Atores.ToList();
            _contexto.Diretores.ToList();
            _contexto.Usuarios.ToList();
            int paginaAtual = paginacao.NumeroDaPagina;
            int tamanhoDaPagina = paginacao.TamanhoDaPagina;
            var itens = votos.Skip((paginaAtual - 1) * tamanhoDaPagina).Take(tamanhoDaPagina).ToList();

            return itens;
        }
        #endregion

        #region RetornarPaginacaoUsuario
        public List<Usuario> RetornarPaginacaoUsuario(Paginacao paginacao, bool usuariosAtivos)
        {
            int paginaAtual = paginacao.NumeroDaPagina;
            int tamanhoDaPagina = paginacao.TamanhoDaPagina;
            if (!usuariosAtivos)
            {
                var usuarios = _contexto.Usuarios.ToList();
                var itens = usuarios.Skip((paginaAtual - 1) * tamanhoDaPagina).Take(tamanhoDaPagina).ToList();
                return itens;
            }
            else
            {
                var usuarioAtivosOrdenados = _contexto.Usuarios.Where(x => x.Status == "Ativo").OrderBy(x => x.Nome).ToList();
                var itens = usuarioAtivosOrdenados.Skip((paginaAtual - 1) * tamanhoDaPagina).Take(tamanhoDaPagina).ToList();
                return itens;
            }
        }
        #endregion
    }
}
