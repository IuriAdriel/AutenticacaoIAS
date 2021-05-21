using System.ComponentModel.DataAnnotations;

namespace UsuarioAPI.ViewModels
{
    public class AdicionarUsuarioViewModel
    {
        [Required(ErrorMessage = "O nome deve ser preenchido.")]
        [MinLength(2, ErrorMessage = "O nome deve ter no mínimo 2 caracteres.")]
        [MaxLength(80, ErrorMessage = "O nome deve ter no máximo 80 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O e-mail deve ser preenchido.")]
        [MinLength(6, ErrorMessage = "O e-mail deve ter no mínimo 6 caracteres.")]
        [MaxLength(180, ErrorMessage = "O e-mail deve ter no máximo 180 caracteres.")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "O e-mail é inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha deve ser preenchida.")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
        [MaxLength(80, ErrorMessage = "A senha deve ter no máximo 30 caracteres.")]
        public string Senha { get; set; }
    }
}
