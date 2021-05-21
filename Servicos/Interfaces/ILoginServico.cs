using Aplicacao.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface ILoginServico
    {
        Task<TokenDTO> ValidarCredencial(UsuarioDTO usuario);
        Task<TokenDTO> ValidarCredencial(TokenDTO token);
        Task<bool> RevogarRefreshToken(string apelido);
    }
}
