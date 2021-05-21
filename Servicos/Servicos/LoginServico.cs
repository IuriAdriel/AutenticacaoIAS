using Aplicacao.Autenticacao;
using Aplicacao.DTO;
using Aplicacao.Interfaces;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Servicos
{
    public class LoginServico : ILoginServico
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private readonly ConfiguracaoToken _configuracaoToken;
        private readonly IUsuarioServico _usuarioServico;
        private readonly ITokenAutenticacao _tokenAutenticacao;

        public LoginServico(ConfiguracaoToken configuracaoToken, ITokenAutenticacao tokenAutenticacao, IUsuarioServico usuarioServico)
        {
            _configuracaoToken = configuracaoToken;
            _tokenAutenticacao = tokenAutenticacao;
            _usuarioServico = usuarioServico;
        }

        public async Task<TokenDTO> ValidarCredencial(UsuarioDTO usuarioDTO)
        {
            long idUsuario = await _usuarioServico.ValidarApelidoESenha(usuarioDTO.Apelido, usuarioDTO.Senha);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, usuarioDTO.Apelido)
            };

            var token = _tokenAutenticacao.GerarToken(claims);
            var refreshToken = _tokenAutenticacao.GerarRefreshToken();

            DateTime? dataExpiracaoToken = DateTime.Now.AddDays(_configuracaoToken.DaysToExpiry);
            await _usuarioServico.AtualizarRefreshToken(idUsuario, refreshToken, dataExpiracaoToken);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuracaoToken.Minutes);

            return new TokenDTO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                token,
                refreshToken);
        }

        public async Task<TokenDTO> ValidarCredencial(TokenDTO tokenDTO)
        {
            var token = tokenDTO.Token;
            var refreshToken = tokenDTO.RefreshToken;
            var principal = _tokenAutenticacao.ObterExpiracaoToken(token);
            var apelido = principal.Identity.Name;
            var usuarioDTO = await _usuarioServico.ObterPorApelido(apelido);

            if (usuarioDTO == null || usuarioDTO.RefreshToken != refreshToken || usuarioDTO.DataExpiracaoToken <= DateTime.Now)
            {
                return null;
            }

            token = _tokenAutenticacao.GerarToken(principal.Claims);
            refreshToken = _tokenAutenticacao.GerarRefreshToken();

            await _usuarioServico.AtualizarRefreshToken(usuarioDTO.Id, refreshToken, DateTime.Now.AddDays(_configuracaoToken.DaysToExpiry));

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_configuracaoToken.Minutes);

            return new TokenDTO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                token,
                refreshToken);
        }

        public async Task<bool> RevogarRefreshToken(string apelido)
        {
            var usuarioDTO = await _usuarioServico.ObterPorApelido(apelido);

            if (usuarioDTO == null)
                return false;

            await _usuarioServico.AtualizarRefreshToken(usuarioDTO.Id, null, null);

            return true;
        }
    }
}
