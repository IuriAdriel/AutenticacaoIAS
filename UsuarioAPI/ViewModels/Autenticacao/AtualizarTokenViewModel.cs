using System.ComponentModel.DataAnnotations;

namespace UsuarioAPI.ViewModels.Autenticacao
{
    public class AtualizarTokenViewModel
    {
        [Required(ErrorMessage = "O token é obrigatório.")]
        public string Token { get; set; }

        [Required(ErrorMessage = "O refresh token é obrigatório.")]
        public string RefreshToken { get; set; }
    }
}
