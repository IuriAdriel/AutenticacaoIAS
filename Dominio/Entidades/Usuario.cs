using Dominio.Validadores;
using Nucleo.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace Dominio.Entidades
{
    public class Usuario : Base
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Apelido { get; private set; }
        public string Senha { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTime? DataExpiracaoToken { get; private set; }

        // necessário para o entity framework
        protected Usuario() { }

        public Usuario(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            _erros = new List<string>();
        }

        public void AlterarNome(string nome)
        {
            Nome = nome;
            Validar();
        }

        public void AlterarEmail(string email)
        {
            Email = email;
            Validar();
        }

        public void AlterarSenha(string senha)
        {
            Senha = senha;
            Validar();
        }

        public void EncriptarSenha()
        {
            if (!string.IsNullOrEmpty(Senha))
                Senha = Senha.HashSHA256();
        }
        public void GerarRefreshToken(string refreshToken, DateTime? dataExpiracaoToken)
        {
            RefreshToken = refreshToken;
            DataExpiracaoToken = dataExpiracaoToken;
            Validar();
        }

        public override bool Validar()
        {
            var validador = new UsuarioValidador();
            var validacao = validador.Validate(this);

            if (!validacao.IsValid)
            {
                foreach (var erro in validacao.Errors)
                {
                    _erros.Add(erro.ErrorMessage);
                }

                throw new DominioExcecao($"Alguns campos estão inválidos. Corrija para prosseguir.", _erros);
            }

            return true;
        }
    }
}
