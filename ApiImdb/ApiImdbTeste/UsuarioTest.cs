using ApiImdb.Models;
using ApiImdb.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ApiImdbTeste
{
    [TestClass]
    public class UsuarioTest
    {
        [TestMethod]
        public void SenhaCriptografada()
        {
            #region Setup
            Criptografia criptografia = new Criptografia();
            var senhaCriptografada = criptografia.GerarHashMd5("UsuarioTest01");
            Usuario usuario = new Usuario()
            {
                Nome = "UsuarioTest01",
                Senha = "2237eae4dc6398f78cae058636e01728",
                Tipo = "Funcionario",
                Status = "Ativado"
            };
            #endregion

            Assert.AreEqual(usuario.Senha, senhaCriptografada);

        }
    }
}