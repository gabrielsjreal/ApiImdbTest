using ApiImdb.Models;
using ApiImdb.Repositorios;
using ApiImdb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiImdb.Controllers
{    
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly Contexto _contexto;

        public UsuarioController(Contexto contexto)
        {
            _contexto = contexto;
        }
       
        [HttpPost]
        [Route("cadastrar")]
        public IActionResult PostCadastrar(Usuario usuario)
        {
            UsuarioService serviceUsuario = new UsuarioService(_contexto);
            serviceUsuario.CadastrarUsuario(usuario);
            return Ok();
        }       

        [HttpPost]
        [Route("login")]
        public ActionResult<dynamic> Authenticate(string usuario, string senha)
        {
            UsuarioRepository userRepository = new UsuarioRepository(_contexto);
            var user = userRepository.Get(usuario, senha);
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });            
            var token = TokenService.GerarToken(user);
            user.Senha = "";

            return new
            {
                user = user,
                token = token
            };
        }

        [HttpPut]
        [Route("editar")]
        [Authorize]
        public IActionResult PutEditar(string usuarioAtual, string senhaAtual, Usuario usuarioAlterado)
        {
            UsuarioService usuarioService = new UsuarioService(_contexto);
            usuarioService.EditarUsuario(usuarioAlterado, usuarioAtual, senhaAtual);
            return Ok();
        }       

        [HttpPost]
        [Route("excluir-desativar")]
        [Authorize(Roles = "administrador")]
        public IActionResult PostExcluir(string nomeUser, bool status)
        {
            UsuarioService usuarioService = new UsuarioService(_contexto);
            usuarioService.DesativacaoLogica(nomeUser, status);
            return Ok();
        }
        
        [HttpGet]
        [Route("listar-usuarios-ativos")]
        [Authorize(Roles = "administrador")]
        public List<Usuario> GetListarUsuarios(Paginacao paginacao, bool usuariosAtivos)
        {
            PaginacaoService services = new PaginacaoService(_contexto);

            return services.RetornarPaginacaoUsuario(paginacao, usuariosAtivos);
        }      
    
}
}
