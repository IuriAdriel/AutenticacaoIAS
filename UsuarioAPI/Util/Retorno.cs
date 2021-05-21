using UsuarioAPI.ViewModels;
using System.Collections.Generic;

namespace UsuarioAPI.Util
{
    public static class Retorno
    {
        public static ResultadoViewModel MensagemDeErro()
        {
            return new ResultadoViewModel
            {
                Mensagem = "Ocorreu algum erro interno na aplicação, por favor tente novamente.",
                Sucesso = false,
                Model = null
            };
        }

        public static ResultadoViewModel DominioExcecaoErro(string mensagem)
        {
            return new ResultadoViewModel
            {
                Mensagem = mensagem,
                Sucesso = false,
                Model = null
            };
        }

        public static ResultadoViewModel DominioExcecaoErro(string mensagem, IReadOnlyCollection<string> erros)
        {
            return new ResultadoViewModel
            {
                Mensagem = mensagem,
                Sucesso = false,
                Model = erros
            };
        }

        public static ResultadoViewModel NaoAutorizado()
        {
            return new ResultadoViewModel
            {
                Mensagem = "A combinação de login e senha está incorreta.",
                Sucesso = false,
                Model = null
            };
        }
    }
}
