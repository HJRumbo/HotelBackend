using Hotel.Core.Application.Exceptions;
using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Application.Models;
using MediatR;

namespace Hotel.Core.Application.Features.Hotels.Commands.UpdateHotelCommand
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateHotelCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Response<int>> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            await ValidateCity(request.CityId);

            var oldHotel = await _unitOfWork.Hotels.GetByIdAsync(request.Id) ?? throw new KeyNotFoundException($"El hotel con el id {request.Id} no existe");

            oldHotel.Name = request.Name;
            oldHotel.Address = request.Address;
            oldHotel.Available = request.Available;
            oldHotel.CityId = request.CityId;

            _unitOfWork.Hotels.Update(oldHotel);

            await _unitOfWork.SaveChangesAsync();

            return new Response<int>(oldHotel.Id, "Hotel modificado correctamente. ");
        }

        private async Task ValidateCity(int cityId)
        {
            List<string> errors = new();

            var city = await _unitOfWork.Cities.GetByIdAsync(cityId);

            if (city == null)
            {
                errors.Add($"La ciudad con el id {cityId} no existe, consulte las ciudades disponibles. ");

                throw new ValidationException(errors);
            }
        }
    }
}
