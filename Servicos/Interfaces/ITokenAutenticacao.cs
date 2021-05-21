using System.Collections.Generic;
using System.Security.Claims;

namespace Aplicacao.Interfaces
{
    public interface ITokenAutenticacao
    {
        string GerarToken(IEnumerable<Claim> claims);
        string GerarRefreshToken();
        ClaimsPrincipal ObterExpiracaoToken(string token);
    }
}
