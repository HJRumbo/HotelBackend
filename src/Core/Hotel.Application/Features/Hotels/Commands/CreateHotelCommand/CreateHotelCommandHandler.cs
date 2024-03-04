using AutoMapper;
using Hotel.Core.Application.Exceptions;
using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Application.Models;
using Hotel.Core.Domain.Entities;
using MediatR;

namespace Hotel.Core.Application.Features.Hotels.Commands.CreateHotelCommand
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateHotelCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            await ValidateCity(request.CityId);

            var hotel = _mapper.Map<HotelClass>(request);

            var hotelRegistred = await _unitOfWork.Hotels.SaveAsync(hotel);

            await _unitOfWork.SaveChangesAsync();

            return new Response<int>(hotelRegistred.Id);
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
