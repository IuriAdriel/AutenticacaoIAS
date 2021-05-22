using System.ComponentModel.DataAnnotations;

namespace UsuarioAPI.ViewModels.Autenticacao
{
    public class EntrarViewModel
    {
        [Required(ErrorMessage = "O usuário deve ser preenchido.")]
        [MinLength(2, ErrorMessage = "O usuário deve ter no mínimo 2 caracteres.")]
        [MaxLength(30, ErrorMessage = "O nome deve ter no máximo 30 caracteres.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "A senha deve ser preenchida.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        public string Senha { get; set; }
    }
}
