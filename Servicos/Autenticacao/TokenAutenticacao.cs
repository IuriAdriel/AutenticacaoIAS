using Aplicacao.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Autenticacao
{
    public class TokenAutenticacao : ITokenAutenticacao
    {
        private readonly ConfiguracaoToken _configuracaoToken;

        public TokenAutenticacao(ConfiguracaoToken configuracaoToken)
        {
            _configuracaoToken = configuracaoToken;
        }
        public string GerarToken(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuracaoToken.Secret));
            var credencial = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuracaoToken.Issuer,
                audience: _configuracaoToken.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_configuracaoToken.Minutes),
                signingCredentials: credencial);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public string GerarRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal ObterExpiracaoToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuracaoToken.Secret)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCulture))
                throw new SecurityTokenException("Token inválido.");

            return principal;
        }
    }
}
