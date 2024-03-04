using AutoMapper;
using Hotel.Core.Application.Exceptions;
using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Core.Application.Models;
using Hotel.Core.Domain.Entities;
using MediatR;

namespace Hotel.Core.Application.Features.Rooms.Commands.CreateRoomCommand
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Response<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRoomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            await ValidateRoomData(request.HotelId, request.RoomTypeId);

            var room = _mapper.Map<Room>(request);

            var registredRoom = await _unitOfWork.Rooms.SaveAsync(room);

            await _unitOfWork.SaveChangesAsync();

            return new Response<int>(registredRoom.Id);
        }

        private async Task ValidateRoomData(int hotelId, int roomTypeId)
        {
            List<string> errors = new();

            var hotel = await _unitOfWork.Hotels.GetByIdAsync(hotelId);
            var roomType = await _unitOfWork.RoomTypes.GetByIdAsync(hotelId);

            if (hotel == null)
            {
                errors.Add($"El hotel con el id {hotelId} no existe, consulte los hoteles disponibles. ");
            }

            if (roomType == null)
            {
                errors.Add($"El tipo de abitción con el id {roomTypeId} no existe, consulte los tipos de habitaciones disponibles. ");
            }

            if (errors.Count > 0) throw new ValidationException(errors);
        }
    }
}
