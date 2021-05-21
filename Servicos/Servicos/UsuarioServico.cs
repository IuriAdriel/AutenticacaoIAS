using AutoMapper;
using Dominio.Entidades;
using Infra.Interfaces;
using Nucleo.Exceptions;
using Aplicacao.DTO;
using Aplicacao.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Aplicacao.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IMapper _mapeador;
        private readonly IUsuarioRepositorio _repositorio;

        public UsuarioServico(IMapper mapeador, IUsuarioRepositorio repositorio)
        {
            _mapeador = mapeador;
            _repositorio = repositorio;
        }

        public async Task<UsuarioDTO> Adicionar(UsuarioDTO usuarioDTO)
        {
            var usuarioEmailExiste = await _repositorio.ObterPorEmail(usuarioDTO.Email);

            if (usuarioEmailExiste != null)
                throw new DominioExcecao("Já existe um usuário cadastrado com o e-mail informado.");

            var usuario = _mapeador.Map<Usuario>(usuarioDTO);
            usuario.EncriptarSenha();
            usuario.Validar();

            var usuarioCriado = await _repositorio.Adicionar(usuario);

            return _mapeador.Map<UsuarioDTO>(usuarioCriado);
        }

        public async Task<UsuarioDTO> Atualizar(UsuarioDTO usuarioDTO)
        {
            var usuarioEmailExiste = await _repositorio.ObterPorEmail(usuarioDTO.Email);

            if (usuarioEmailExiste != null)
                throw new DominioExcecao("Já existe um usuário cadastrado com o e-mail informado.");

            var usuarioExiste = await _repositorio.Obter(usuarioDTO.Id);

            if (usuarioExiste == null)
                throw new DominioExcecao("Nenhum usuário encontrado com o Id informado.");

            var usuario = _mapeador.Map<Usuario>(usuarioDTO);
            usuario.EncriptarSenha();
            usuario.Validar();

            var usuarioCriado = await _repositorio.Atualizar(usuario);

            return _mapeador.Map<UsuarioDTO>(usuarioCriado);
        }

        public async Task<List<UsuarioDTO>> BuscarPorEmail(string email)
        {
            var usuarios = await _repositorio.BuscarPorEmail(email);

            return _mapeador.Map<List<UsuarioDTO>>(usuarios);
        }

        public async Task<List<UsuarioDTO>> BuscarPorNome(string nome)
        {
            var usuarios = await _repositorio.BuscarPorNome(nome);

            return _mapeador.Map<List<UsuarioDTO>>(usuarios);
        }

        public async Task Excluir(long id)
        {
            await _repositorio.Excluir(id);
        }

        public List<UsuarioDTO> Listar()
        {
            var usuarios = _repositorio.Listar().ToList();

            return _mapeador.Map<List<UsuarioDTO>>(usuarios);
        }

        public async Task<UsuarioDTO> Obter(long id)
        {
            var usuario = await _repositorio.Obter(id);

            return _mapeador.Map<UsuarioDTO>(usuario);
        }

        public async Task<UsuarioDTO> ObterPorEmail(string email)
        {
            var usuario = await _repositorio.ObterPorEmail(email);

            return _mapeador.Map<UsuarioDTO>(usuario);
        }

        public async Task<long> ValidarApelidoESenha(string apelido, string senha)
        {
            var usuario = await _repositorio.ObterPorApelido(apelido);

            if (usuario == null)
                throw new DominioExcecao("Usuário ou senha inválidos");

            if (usuario.Senha != senha.HashSHA256())
                throw new DominioExcecao("Usuário ou senha inválidos");

            return usuario.Id;
        }

        public async Task<UsuarioDTO> AtualizarRefreshToken(long id, string refreshToken, DateTime? dataExpiracaoToken)
        {
            var usuario = await _repositorio.Obter(id);
            usuario.GerarRefreshToken(refreshToken, dataExpiracaoToken);

            return _mapeador.Map<UsuarioDTO>(await _repositorio.Atualizar(usuario));
        }

        public async Task<UsuarioDTO> ObterPorApelido(string apelido)
        {
            var usuario = await _repositorio.ObterPorApelido(apelido);

            return _mapeador.Map<UsuarioDTO>(usuario);
        }
    }
}
