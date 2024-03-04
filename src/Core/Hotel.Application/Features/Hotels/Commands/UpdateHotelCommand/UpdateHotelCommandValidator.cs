
using FluentValidation;

namespace Hotel.Core.Application.Features.Hotels.Commands.UpdateHotelCommand
{
    public class UpdateHotelCommandValidator : AbstractValidator<UpdateHotelCommand>
    {
        public UpdateHotelCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es requerido");
            RuleFor(x => x.Address).NotEmpty().WithMessage("La dirección es requerida");
            RuleFor(x => x.CityId).NotEmpty().WithMessage("El id de la ciudad es requerido");
        }
    }
}
