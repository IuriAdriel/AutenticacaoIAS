using System;
using System.Text.Json.Serialization;

namespace Aplicacao.DTO
{
    public class UsuarioDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Apelido { get; set; }

        [JsonIgnore]
        public string RefreshToken { get; set; }

        [JsonIgnore]
        public DateTime? DataExpiracaoToken { get; set; }

        [JsonIgnore]
        public string Senha { get; set; }

        public UsuarioDTO()
        {

        }
        public UsuarioDTO(long id, string nome, string email, string apelido, string senha)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Apelido = apelido;
            Senha = senha;
        }


    }
}
