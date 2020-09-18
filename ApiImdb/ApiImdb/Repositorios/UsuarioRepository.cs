using ApiImdb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiImdb.Repositorios
{
    public class UsuarioRepository
    {
        private readonly Contexto _contexto;

        public UsuarioRepository(Contexto contexto)
        {
            _contexto = contexto;
        }
        public Usuario Get(string usuario, string senha)
        {
            var usuarios = _contexto.Usuarios.ToList();

            return usuarios.Where(x => x.Nome.ToLower() == usuario.ToLower() && x.Senha == senha).FirstOrDefault();
        }
    }
}
