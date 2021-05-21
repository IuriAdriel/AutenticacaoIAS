using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.DTO
{
    public class TokenDTO
    {
        public TokenDTO()
        {

        }
        public TokenDTO(bool autenticado, string criado, string expiracao, string token, string refreshToken)
        {
            Autenticado = autenticado;
            Criado = criado;
            Expiracao = expiracao;
            Token = token;
            RefreshToken = refreshToken;
        }

        public bool Autenticado { get; set; }
        public string Criado { get; set; }
        public string Expiracao { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
