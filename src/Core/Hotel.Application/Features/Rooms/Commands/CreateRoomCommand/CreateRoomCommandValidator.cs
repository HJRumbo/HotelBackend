using FluentValidation;

namespace Hotel.Core.Application.Features.Rooms.Commands.CreateRoomCommand
{
    public class CreateRoomCommandValidator : AbstractValidator<CreateRoomCommand>
    {
        public CreateRoomCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre es requerido");
            RuleFor(x => x.BaseCost).NotEmpty().WithMessage("El costo base es requerido");
            RuleFor(x => x.Taxes).NotEmpty().WithMessage("Los impuestos son requeridos");
            RuleFor(x => x.RoomTypeId).NotEmpty().WithMessage("El tipo de habitación es requerido");
            RuleFor(x => x.Floor).NotEmpty().WithMessage("El piso es requerido");
            RuleFor(x => x.RoomNumber).NotEmpty().WithMessage("El número de la habitación es requerido");
            RuleFor(x => x.Capacity).NotEmpty().WithMessage("La capacidad de la habitación es requerida");
            RuleFor(x => x.HotelId).NotEmpty().WithMessage("El id del hotel es requerido");
        }
    }
}
