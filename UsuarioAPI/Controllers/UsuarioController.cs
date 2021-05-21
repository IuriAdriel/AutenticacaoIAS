using AutoMapper;
using UsuarioAPI.Util;
using UsuarioAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Nucleo.Exceptions;
using Aplicacao.DTO;
using Aplicacao.Interfaces;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace UsuarioAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;
        private readonly IMapper _mapeador;

        public UsuarioController(IUsuarioServico usuarioServico, IMapper mapeador)
        {
            _usuarioServico = usuarioServico;
            _mapeador = mapeador;
        }

        [HttpPost]
        [Route("/api/v1/usuario/adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarUsuarioViewModel model)
        {
            try
            {
                var usuarioDTO = _mapeador.Map<UsuarioDTO>(model);
                var usuarioCriado = await _usuarioServico.Adicionar(usuarioDTO);

                return Ok(new ResultadoViewModel
                {
                    Mensagem = "Usuário adicionado com sucesso.",
                    Sucesso = true,
                    Model = usuarioCriado
                });
            }
            catch (DominioExcecao e)
            {
                return BadRequest(Retorno.DominioExcecaoErro(e.Message, e.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Retorno.MensagemDeErro());
            }
        }

        [HttpPut]
        [Route("/api/v1/usuario/atualizar")]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarUsuarioViewModel model)
        {
            try
            {
                var usuarioDTO = _mapeador.Map<UsuarioDTO>(model);
                var usuarioCriado = await _usuarioServico.Atualizar(usuarioDTO);

                return Ok(new ResultadoViewModel
                {
                    Mensagem = "Usuário atualizado com sucesso.",
                    Sucesso = true,
                    Model = usuarioCriado
                });
            }
            catch (DominioExcecao e)
            {
                return BadRequest(Retorno.DominioExcecaoErro(e.Message, e.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Retorno.MensagemDeErro());
            }
        }

        [HttpDelete]
        [Route("/api/v1/usuario/excluir/{id}")]
        public async Task<IActionResult> Excluir(long id)
        {
            try
            {
                await _usuarioServico.Excluir(id);

                return Ok(new ResultadoViewModel
                {
                    Mensagem = "Usuário excluído com sucesso.",
                    Sucesso = true,
                    Model = null
                });
            }
            catch (DominioExcecao e)
            {
                return BadRequest(Retorno.DominioExcecaoErro(e.Message, e.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Retorno.MensagemDeErro());
            }
        }

        [HttpGet]
        [Route("/api/v1/usuario/obter/{id}")]
        public async Task<IActionResult> Obter(long id)
        {
            try
            {
                var usuario = await _usuarioServico.Obter(id);

                if(usuario == null)
                {
                    return Ok(new ResultadoViewModel
                    {
                        Mensagem = $"Nenhum usuário encontrado com o Id {id}.",
                        Sucesso = true,
                        Model = null
                    });
                }

                return Ok(new ResultadoViewModel
                {
                    Mensagem = "Usuário encontrado com sucesso.",
                    Sucesso = true,
                    Model = usuario
                });
            }
            catch (DominioExcecao e)
            {
                return BadRequest(Retorno.DominioExcecaoErro(e.Message, e.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Retorno.MensagemDeErro());
            }
        }

        [HttpGet]
        [Route("/api/v1/usuario/listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var usuarios = _usuarioServico.Listar();

                if (usuarios == null || usuarios.Count == 0)
                {
                    return Ok(new ResultadoViewModel
                    {
                        Mensagem = "Nenhum usuário encontrado.",
                        Sucesso = true,
                        Model = null
                    });
                }

                return Ok(new ResultadoViewModel
                {
                    Mensagem = "Usuários encontrados com sucesso.",
                    Sucesso = true,
                    Model = usuarios
                });
            }
            catch (DominioExcecao e)
            {
                return BadRequest(Retorno.DominioExcecaoErro(e.Message, e.Errors));
            }
            catch (Exception e)
            {
                return StatusCode(500, Retorno.MensagemDeErro());
            }
        }

        [HttpGet]
        [Route("/api/v1/usuario/obter-por-email")]
        public async Task<IActionResult> ObterPorEmail([FromQuery] string email)
        {
            try
            {
                var usuario = await _usuarioServico.ObterPorEmail(email);

                if (usuario == null)
                {
                    return Ok(new ResultadoViewModel
                    {
                        Mensagem = $"Nenhum usuário encontrado com o e-mail: {email}.",
                        Sucesso = true,
                        Model = null
                    });
                }

                return Ok(new ResultadoViewModel
                {
                    Mensagem = "Usuário encontrado com sucesso.",
                    Sucesso = true,
                    Model = usuario
                });
            }
            catch (DominioExcecao e)
            {
                return BadRequest(Retorno.DominioExcecaoErro(e.Message, e.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Retorno.MensagemDeErro());
            }
        }

        [HttpGet]
        [Route("/api/v1/usuario/buscar-por-email")]
        public async Task<IActionResult> BuscarPorEmail([FromQuery] string email)
        {
            try
            {
                var usuarios = await _usuarioServico.BuscarPorEmail(email);

                if (usuarios == null || usuarios.Count == 0)
                {
                    return Ok(new ResultadoViewModel
                    {
                        Mensagem = $"Nenhum usuário encontrado.",
                        Sucesso = true,
                        Model = null
                    });
                }

                return Ok(new ResultadoViewModel
                {
                    Mensagem = "Usuários encontrados com sucesso.",
                    Sucesso = true,
                    Model = usuarios
                });
            }
            catch (DominioExcecao e)
            {
                return BadRequest(Retorno.DominioExcecaoErro(e.Message, e.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Retorno.MensagemDeErro());
            }
        }

        [HttpGet]
        [Route("/api/v1/usuario/buscar-por-nome")]
        public async Task<IActionResult> BuscarPorNome([FromQuery] string nome)
        {
            try
            {
                var usuarios = await _usuarioServico.BuscarPorNome(nome);

                if (usuarios == null || usuarios.Count == 0)
                {
                    return Ok(new ResultadoViewModel
                    {
                        Mensagem = $"Nenhum usuário encontrado.",
                        Sucesso = true,
                        Model = null
                    });
                }

                return Ok(new ResultadoViewModel
                {
                    Mensagem = "Usuários encontrados com sucesso.",
                    Sucesso = true,
                    Model = usuarios
                });
            }
            catch (DominioExcecao e)
            {
                return BadRequest(Retorno.DominioExcecaoErro(e.Message, e.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, Retorno.MensagemDeErro());
            }
        }
    }
}
