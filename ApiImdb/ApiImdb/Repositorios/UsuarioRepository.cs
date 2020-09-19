using ApiImdb.Models;
using ApiImdb.Services;
using System.Linq;

namespace ApiImdb.Repositorios
{
    public class UsuarioRepository
    {
        private readonly Contexto _contexto;

        public UsuarioRepository(Contexto contexto)
        {
            _contexto = contexto;
        }
        #region GetUsuario
        public Usuario Get(string usuario, string senha)
        {
            var usuarios = _contexto.Usuarios.ToList();
            Criptografia criptografia = new Criptografia();
            var senhaCriptografada = string.IsNullOrEmpty(senha) ? senha : criptografia.GerarHashMd5(senha);

            return usuarios.Where(x => x.Nome.ToLower() == usuario.ToLower() && x.Senha == senhaCriptografada).FirstOrDefault();
        }
        #endregion
    }
}
