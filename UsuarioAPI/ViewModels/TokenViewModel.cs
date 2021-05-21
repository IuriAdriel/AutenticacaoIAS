using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsuarioAPI.ViewModels
{
    public class TokenViewModel
    {
        [Required(ErrorMessage = "O token é obrigatório.")]
        public string Token { get; set; }

        [Required(ErrorMessage = "O refresh token é obrigatório.")]
        public string RefreshToken { get; set; }
    }
}
