using FluentValidation;

namespace Hotel.Core.Application.Features.Account.Commands.SignInCommand
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("El nombre de usuario o email es requerido");
            RuleFor(x => x.Password).NotEmpty().WithMessage("La contraseña es requerida");
        }
    }
}
