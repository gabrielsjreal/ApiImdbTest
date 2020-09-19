using ApiImdb.Models;
using System.Linq;

namespace ApiImdb.Services
{
    public class UsuarioService
    {
        private readonly Contexto _contexto;

        public UsuarioService(Contexto contexto)
        {
            _contexto = contexto;
        }

        #region CadastrarUsuario
        public void CadastrarUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                Criptografia criptografia = new Criptografia();
                var senhaCriptografada = string.IsNullOrEmpty(usuario.Senha) ? usuario.Senha : criptografia.GerarHashMd5(usuario.Senha);
                usuario.Senha = senhaCriptografada;
                _contexto.Usuarios.Add(usuario);
                _contexto.SaveChanges();
            }
        }
        #endregion

        #region EditarUsuario
        public void EditarUsuario(Usuario usuarioAlterado, string usuarioAtual, string senhaAtual)
        {
            Criptografia criptografia = new Criptografia();
            var senhaCriptografada = string.IsNullOrEmpty(senhaAtual) ? senhaAtual : criptografia.GerarHashMd5(senhaAtual);
            if (usuarioAlterado != null && !string.IsNullOrEmpty(usuarioAtual) && !string.IsNullOrEmpty(senhaCriptografada))
            {
                var atualizarUser = (from u in _contexto.Usuarios
                                     where u.Nome == usuarioAtual && u.Senha == senhaCriptografada
                                     select u).SingleOrDefault();
                if (atualizarUser != null)
                {
                    atualizarUser.Nome = usuarioAlterado.Nome;
                    atualizarUser.Senha = criptografia.GerarHashMd5(usuarioAlterado.Senha);
                    atualizarUser.Tipo = usuarioAlterado.Tipo;
                    atualizarUser.Status = usuarioAlterado.Status;

                    _contexto.SaveChanges();
                }

            }
        }
        #endregion

        #region DesativacaoLogica
        public void DesativacaoLogica(string nomeUser, bool status)
        {
            if (!string.IsNullOrEmpty(nomeUser))
            {
                var user = _contexto.Usuarios.FirstOrDefault(x => x.Nome == nomeUser);
                user.Status = status ? "Ativo" : "Desativado";

                if (user != null)
                {
                    _contexto.Usuarios.Update(user);
                    _contexto.SaveChanges();
                }
            }
        }
        #endregion
    }
}
