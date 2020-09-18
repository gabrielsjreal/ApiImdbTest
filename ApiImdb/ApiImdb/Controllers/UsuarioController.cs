using ApiImdb.Models;
using ApiImdb.Repositorios;
using ApiImdb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

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
            if (usuario != null)
            {
                _contexto.Usuarios.Add(usuario);
                _contexto.SaveChanges();

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<dynamic> Authenticate(string usuario, string senha)
        {
            UsuarioRepository userRepository = new UsuarioRepository(_contexto);
            // Recupera o usuário
            var user = userRepository.Get(usuario, senha);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GerarToken(user);

            // Oculta a senha
            user.Senha = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpPut]
        [Route("editar")]
        [Authorize]
        public IActionResult PutEditar(Usuario usuario)
        {
            if (usuario != null)
            {
                var user = _contexto.Usuarios.FirstOrDefault(x => x.Nome == usuario.Nome
                                                             && x.Senha == usuario.Senha
                                                             && x.Tipo == usuario.Tipo);

                if (user != null)
                {
                    _contexto.Usuarios.Update(user);
                    _contexto.SaveChanges();
                }
                return Ok();
            }
            else
            {
                return BadRequest();
            }
           
        }

        [HttpPost]
        [Route("excluir-desativar")]
        [Authorize(Roles = "administrador")]
        public IActionResult PostExcluir(string nomeUser, bool status)
        {
            if (string.IsNullOrEmpty(nomeUser))
            {
                var user = _contexto.Usuarios.FirstOrDefault(x => x.Nome == nomeUser);
                user.Status = status ? "Ativo" : "Desativado";

                if (user != null)
                {
                    _contexto.Usuarios.Update(user);
                    _contexto.SaveChanges();
                }
                return Ok();
            }
            else
            {
                return BadRequest();
            }
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
