
using FluentValidation;

namespace Hotel.Core.Application.Features.Travelers.Commands.CreateTravelerCommand
{
    public class CreateTravelerCommandValidator : AbstractValidator<CreateTravelerCommand>
    {
        public CreateTravelerCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es requerido");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("El apellido es requerido");
            RuleFor(x => x.Birthday).NotEmpty().WithMessage("La fecha de nacimiento es requerida");
            RuleFor(x => x.GenderId).NotEmpty().WithMessage("El género es requerido");
            RuleFor(x => x.DocumentTypeId).NotEmpty().WithMessage("El tipo de documento de identidad es requerido");
            RuleFor(x => x.DocumentNumber).NotEmpty().WithMessage("El número de documento de identidad es requerido");
            RuleFor(x => x.Email).NotEmpty().WithMessage("El email es requerido");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("El nombre de usuario es requerido");
            RuleFor(x => x.Phone).NotEmpty().WithMessage("El número de teléfono es requerido");
            RuleFor(x => x.Password).NotEmpty()
                                    .WithMessage("La contraseña es requerida")
                                    .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{6,}$")
                                    .WithMessage(@"La contraseña debe contener un caracter en mayúcula, un caracter en minúcula, un dígito, un caracter no alfanumérico, " +
                                    "y ser de al menos 6 caracteres de longitud.");
            RuleFor(x => x.ConfirmPassword).Must(
                (rootObject, list, context) => { return rootObject.Password == rootObject.ConfirmPassword; }
                ).WithMessage("Las contraseñas no coinciden");
        }
    }
}
