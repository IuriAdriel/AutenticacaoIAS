using Dominio.Entidades;
using FluentValidation;

namespace Dominio.Validadores
{
    public class UsuarioValidador : AbstractValidator<Usuario>
    {
        public UsuarioValidador()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade não pode ser vazia.")

                .NotNull()
                .WithMessage("A entidade não pode ser nula.");

            RuleFor(x => x.Nome)
                .NotNull()
                .WithMessage("O nome não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O nome não pode ser vazio.")

                .MinimumLength(2)
                .WithMessage("O nome deve ter no mínimo 2 caracteres.")

                .MaximumLength(80)
                .WithMessage("O nome deve ter no máximo 80 caracteres.");

            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("O e-mail não pode ser nulo.")

                .NotEmpty()
                .WithMessage("O e-mail não pode ser vazio.")

                .MinimumLength(6)
                .WithMessage("O e-mail deve ter no mínimo 6 caracteres.")

                .MaximumLength(180)
                .WithMessage("O e-mail deve ter no máximo 180 caracteres.")
                
                .Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
                .WithMessage("O e-mail é inválido.");

            RuleFor(x => x.Senha)
                .NotNull()
                .WithMessage("A senha não pode ser nula.")

                .NotEmpty()
                .WithMessage("A senha não pode ser vazia.");

                //.MinimumLength(6)
                //.WithMessage("A senha deve ter no mínimo 6 caracteres.")

                //.MaximumLength(30)
                //.WithMessage("A senha deve ter no máximo 30 caracteres.");
        }
    }
}
