using Aplicacao.DTO;
using Aplicacao.Interfaces;
using UsuarioAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuarioAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace UsuarioAPI.Controllers
{
    [ApiController]
    public class AutenticacaoController : Controller
    {
        private readonly ILoginServico _loginServico;
        private readonly IUsuarioServico _usuarioServico;

        public AutenticacaoController(ILoginServico loginServico, IUsuarioServico usuarioServico)
        {
            _loginServico = loginServico;
            _usuarioServico = usuarioServico;
        }

        [HttpPost]
        [Route("entrar")]
        public async Task<IActionResult> Entrar([FromBody] AutenticacaoEntrarViewModel entrar)
        {
            if (string.IsNullOrEmpty(entrar.Usuario) || string.IsNullOrEmpty(entrar.Senha))
                return BadRequest("Requisição inválida.");

            var token = await _loginServico.ValidarCredencial(new UsuarioDTO
            {
                Apelido = entrar.Usuario,
                Senha = entrar.Senha
            });

            if (token == null)
                return Unauthorized();

            return Ok(new ResultadoViewModel
            {
                Mensagem = "Usuário autenticado.",
                Sucesso = true,
                Model = token
            });
        }

        [HttpPost]
        [Route("atualizar-token")]
        public async Task<IActionResult> AtualizarToken([FromBody] TokenViewModel tokenViewModel)
        {
            if (tokenViewModel == null)
                return BadRequest("Requisição inválida.");

            var token = await _loginServico.ValidarCredencial(new TokenDTO
            {
                Token = tokenViewModel.Token,
                RefreshToken = tokenViewModel.RefreshToken
            });

            if (token == null)
                return Unauthorized();

            return Ok(new ResultadoViewModel
            {
                Mensagem = "Token atualizado.",
                Sucesso = true,
                Model = token
            });
        }

        [HttpGet]
        [Route("revogar-refresh-token")]
        [Authorize("Bearer")]
        public async Task<IActionResult> RevogarRefreshToken()
        {
            var apelido = User.Identity.Name;
            bool sucesso = await _loginServico.RevogarRefreshToken(apelido);

            if(!sucesso)
                return BadRequest("Requisição inválida.");

            return Ok(new ResultadoViewModel
            {
                Mensagem = "Token revogado com sucesso.",
                Sucesso = true,
                Model = null
            });
        }
    }
}
